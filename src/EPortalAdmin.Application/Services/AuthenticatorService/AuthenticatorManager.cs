using EPortalAdmin.Application.Repositories;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Domain.Enums;
using EPortalAdmin.Core.Exceptions;
using EPortalAdmin.Core.Mailing;
using EPortalAdmin.Core.Security.EmailAuthenticator;
using EPortalAdmin.Core.Security.OtpAuthenticator;
using MimeKit;

namespace EPortalAdmin.Application.Services.AuthenticatorService
{
    public class AuthenticatorManager(IEmailAuthenticatorHelper emailAuthenticatorHelper, IEmailAuthenticatorRepository emailAuthenticatorRepository,
        IMailService mailService, IOtpAuthenticatorHelper otpAuthenticatorHelper, IOtpAuthenticatorRepository otpAuthenticatorRepository) : IAuthenticatorService
    {
        public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
        {
            string result = await otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
            return result;
        }

        public async Task<EmailAuthenticator> CreateEmailAuthenticator(User user)
        {
            EmailAuthenticator emailAuthenticator =
                new()
                {
                    UserId = user.Id,
                    ActivationKey = await emailAuthenticatorHelper.CreateEmailActivationKey(),
                    IsVerified = false
                };
            return emailAuthenticator;
        }

        public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
        {
            OtpAuthenticator otpAuthenticator =
                new()
                {
                    UserId = user.Id,
                    SecretKey = await otpAuthenticatorHelper.GenerateSecretKey(),
                    IsVerified = false
                };
            return otpAuthenticator;
        }

        public async Task SendAuthenticatorCode(User user)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email)
                await SendAuthenticatorCodeWithEmail(user);
        }

        public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email)
                await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
            else if (user.AuthenticatorType is AuthenticatorType.Otp)
                await VerifyAuthenticatorCodeWithOtp(user, authenticatorCode);
        }

        private async Task SendAuthenticatorCodeWithEmail(User user)
        {
            EmailAuthenticator? emailAuthenticator = await emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id)
                ?? throw new NotFoundException("Email Authenticator not found.", ExceptionCode.AuthenticatorNotFound);

            if (!emailAuthenticator.IsVerified)
                throw new BusinessException("Email Authenticator must be is verified.", ExceptionCode.AuthenticatorMustBeVerified);

            string authenticatorCode = await emailAuthenticatorHelper.CreateEmailActivationCode();
            emailAuthenticator.ActivationKey = authenticatorCode;
            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Authenticator Code - EPortalAdmin",
                    TextBody = $"Enter your authenticator code: {authenticatorCode}"
                }
            );
        }

        private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
        {
            EmailAuthenticator? emailAuthenticator = await emailAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id)
                ?? throw new NotFoundException("Email Authenticator not found.", ExceptionCode.AuthenticatorNotFound);

            if (emailAuthenticator.ActivationKey != authenticatorCode)
                throw new BusinessException("Authenticator code is invalid.", ExceptionCode.InvalidAuthenticatorCode);

            emailAuthenticator.ActivationKey = null;
            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }

        private async Task VerifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
        {
            OtpAuthenticator? otpAuthenticator = await otpAuthenticatorRepository.GetAsync(predicate: e => e.UserId == user.Id)
                ?? throw new NotFoundException("Otp Authenticator not found.", ExceptionCode.AuthenticatorNotFound);
            bool result = await otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);
            if (!result)
                throw new BusinessException("Authenticator code is invalid.", ExceptionCode.InvalidAuthenticatorCode);
        }
    }
}
