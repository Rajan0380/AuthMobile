using Demo.Auth.OpenIddict;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace AuthMobile
{
    //Newly Added Service
    public class AuthMobileService: AuthMobileAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        protected AuthMobileOptions Options { get; }
        public AuthMobileService(IHttpClientFactory httpClientFactory, IOptionsSnapshot<AuthMobileOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            Options = options.Value;
        }

        [Authorize("AbpIdentity.Users.Delete")]
        public string GetCheckToken()
        {
            return "Hello";
        }

        [Authorize]
        
        public string GetCheckTokenusers()
        {
            return "Hello";
        }

        public async Task<object> GetTokenAsync(string phoneNumber, string code)
        {
            var client = _httpClientFactory.CreateClient("AuthMobile");

            var request = new TokenRequest
            {
                Address = Options.AuthServer+"/connect/token",
                GrantType = MyTokenExtensionGrant.ExtensionGrantName,

                ClientId = Options.ClientId,
          
                Parameters =
                {
                    { "phone_number", phoneNumber },
                    {"scope",Options.Scope}
                }
            };

            request.Headers.Add(GetTenantHeaderName(), CurrentTenant.Id?.ToString());

            return await client.RequestTokenAsync(request);
        }
        protected virtual string GetTenantHeaderName()
        {
            return "__tenant";
        }
    }
}
