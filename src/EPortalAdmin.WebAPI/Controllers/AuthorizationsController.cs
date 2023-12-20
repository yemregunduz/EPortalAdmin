using EPortalAdmin.Application.Features.Authorizations.Commands;
using EPortalAdmin.Application.ViewModels.Authorization;
using EPortalAdmin.Application.Wrappers.Results;
using EPortalAdmin.Core.Attributes;
using EPortalAdmin.Core.Domain.Configurations;
using EPortalAdmin.Core.Domain.Dtos;
using EPortalAdmin.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EPortalAdmin.WebAPI.Controllers
{
    [Route("api/authorization-management")]
    [ApiController]
    public class AuthorizationsController : BaseController
    {
        private readonly WebApiConfiguration _webApiConfiguration;
        public AuthorizationsController(IConfiguration configuration)
        {
            _webApiConfiguration = configuration.GetSection(WebApiConfiguration.AppSettingsKey).Get<WebApiConfiguration>()
                ?? throw new NullReferenceException($"\"{WebApiConfiguration.AppSettingsKey}\" section cannot found in configuration.");
        }

        [HttpPost("register")]
        [ExplorableEndpoint(Description = "Kullanıcı Kayıt Etme")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };
            var result = await Mediator.Send(registerCommand);

            if (result.Data.RefreshToken is not null)
                SetRefreshTokenToCookie(result.Data.RefreshToken);

            var httpResponse = new SuccessDataResult<TokenDto>(result.Data.TokenDto, result.Message);

            return Created("",httpResponse);
        }

        [HttpPost("login")]
        [ExplorableEndpoint(Description = "Kullanıcı Girişi")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new()
            {
                UserForLoginDto = userForLoginDto,
                IpAddress = GetIpAddress()
            };
            var result = await Mediator.Send(loginCommand);

            if (result.Data.RefreshToken is not null)
                SetRefreshTokenToCookie(result.Data.RefreshToken);

            var httpResponse = new SuccessDataResult<LoggedHttpResponse>(result.Data.LoggedHttpResponse, result.Message);
            return Ok(httpResponse);
        }

        [HttpPost("change-password")]
        [ExplorableEndpoint(Description = "Şifre Değiştirme")]
        public async Task<IActionResult> ChangePasswordWithCredentials([FromBody] UserForChangePasswordDto userForChangePasswordDto)
        {
            ChangePasswordWithCredentialsCommand changePasswordWithCredentialsCommand = new()
            {
                UserForChangePasswordDto = userForChangePasswordDto,
            };

            var result = await Mediator.Send(changePasswordWithCredentialsCommand);
            return Ok(result);
        }

        [HttpGet("refresh-token")]
        [ExplorableEndpoint(Description = "Token Yenileme")]
        public async Task<IActionResult> RefreshToken()
        {
            RefreshTokenCommand refreshTokenCommand = new() { RefreshToken = GetRefreshTokenFromCookies(), IpAddress = GetIpAddress() };
            var result = await Mediator.Send(refreshTokenCommand);
            SetRefreshTokenToCookie(result.Data.RefreshToken);

            var httpResponse = new SuccessDataResult<TokenDto>(result.Data.TokenDto, result.Message);

            return Ok(httpResponse);
        }

        [HttpPut("revoke-token")]
        [ExplorableEndpoint(Description = "Token İptal Etme")]
        public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
        {
            RevokeRefreshTokenCommand revokeTokenCommand = new() { Token = refreshToken ?? GetRefreshTokenFromCookies(), IpAddress = GetIpAddress() };
            var result = await Mediator.Send(revokeTokenCommand);
            return Ok(result);
        }

        [HttpGet("enable-email-authenticator")]
        [ExplorableEndpoint(Description = "Email Doğrulamasını Aktif Etme")]
        public async Task<IActionResult> EnableEmailAuthenticator()
        {
            EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand =
                new() { VerifyEmailUrlPrefix = $"{_webApiConfiguration.ApiDomain}/authorization-management/verify-email-authenticator" };

            await Mediator.Send(enableEmailAuthenticatorCommand);
            return Ok();
        }

        [HttpGet("verify-email-authenticator")]
        [ExplorableEndpoint(Description = "Email Doğrulaması")]
        public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand)
        {
            var result = await Mediator.Send(verifyEmailAuthenticatorCommand);
            return Ok(result);
        }

        [HttpGet("enable-otp-authenticator")]
        [ExplorableEndpoint(Description = "OTP Doğrulamasını Aktif Etme")]
        public async Task<IActionResult> EnableOtpAuthenticator()
        {
            EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new();
            var result = await Mediator.Send(enableOtpAuthenticatorCommand);
            return Ok(result);
        }

        [HttpPost("verify-otp-authenticator")]
        [ExplorableEndpoint(Description = "OTP Doğrulaması")]
        public async Task<IActionResult> VerifyOtpAuthenticator([FromQuery] string authenticatorCode)
        {
            VerifyOtpAuthenticatorCommand verifyEmailAuthenticatorCommand =
                new() { ActivationCode = authenticatorCode };

            var result = await Mediator.Send(verifyEmailAuthenticatorCommand);
            return Ok(result);
        }

        private string GetRefreshTokenFromCookies() =>
            Request.Cookies["refreshToken"] ?? throw new ArgumentException("Refresh token is not found in request cookies.");

        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7), Secure = true };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
