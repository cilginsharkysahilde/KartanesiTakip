﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Takip.Model;
using Takip.Model.Tables;
using Takip.Repository;

namespace Takip.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache) { }

        [HttpPost("Login")]
        public dynamic Login([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            string eMail = json.EMail;
            string password = json.Password;

            Personnel item = repo.PersonnelRepository.FindByCondition(p => p.EMail == eMail && p.Password == password).SingleOrDefault<Personnel>();

            if (item != null)
            {
                Role role = repo.RoleRepository.FindByCondition(r => r.Id == item.RoleId).SingleOrDefault<Role>();

                Dictionary<string, object> claims = new Dictionary<string, object>();
                if (role != null)
                    claims.Add(ClaimTypes.Role, role.Name);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("TakipAPIJSONLoginTokenKey");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                    Claims = claims
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new
                {
                    success = true,
                    data = tokenHandler.WriteToken(token),
                    name = item.Name,
                    role = role?.Name
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Kullanıcı adı veya şifre hatalı"
                };
            }
        }

    }
}
