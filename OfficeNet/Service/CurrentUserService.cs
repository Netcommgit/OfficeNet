﻿using System.Security.Claims;

namespace OfficeNet.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService() { }

        public CurrentUserService(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }
        public string? GetUserId()
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
