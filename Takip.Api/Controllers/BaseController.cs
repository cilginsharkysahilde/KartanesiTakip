﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Takip.Repository;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected RepositoryWrapper repo;
        protected IMemoryCache cache;
        public BaseController(RepositoryWrapper repo, IMemoryCache cache)
        {
            this.repo = repo;
            this.cache = cache;
        }
    }
}
