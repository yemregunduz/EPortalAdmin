using AutoMapper;
using EPortalAdmin.Core.Domain.Entities;
using EPortalAdmin.Core.Persistence.Repositories;
using EPortalAdmin.Core.Utilities.Extensions.Claims;
using EPortalAdmin.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EPortalAdmin.Application
{
    public abstract class ApplicationFeatureBase<TEntity>
          where TEntity : BaseEntity
    {
        protected IMapper Mapper =>
            _mapper ??= HttpContextAccessorSingleton.Current.HttpContext!.RequestServices.GetRequiredService<IMapper>();
        private IMapper? _mapper;

        protected IRepository<TEntity> Repository =>
            _repository ??= HttpContextAccessorSingleton.Current.HttpContext!.RequestServices.GetRequiredService<IRepository<TEntity>>();

        private IRepository<TEntity>? _repository;

        protected int CurrentUserId => HttpContextAccessorSingleton.Current.HttpContext!.User.GetUserId();

    }

    public static class HttpContextAccessorSingleton
    {
        private static IHttpContextAccessor? _httpContextAccessor;
        private static readonly object _lock = new();

        public static void Configure(IServiceProvider serviceProvider)
        {
            lock (_lock)
            {
                if (_httpContextAccessor != null)
                {
                    throw new InvalidOperationException(Messages.Configuration.HttpContextAccessorAlreadyConfigured);
                }

                _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            }
        }

        public static IHttpContextAccessor Current
        {
            get
            {
                if (_httpContextAccessor == null)
                {
                    throw new InvalidOperationException(Messages.Configuration.HttpContextAccessorNotConfigured);
                }

                return _httpContextAccessor;
            }
        }
    }
}
