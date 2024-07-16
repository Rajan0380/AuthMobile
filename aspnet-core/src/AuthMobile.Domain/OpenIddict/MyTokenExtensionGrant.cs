using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Server;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using SignInResult = Microsoft.AspNetCore.Mvc.SignInResult;
using System.Security.Principal;
using AuthMobile;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Domain.Repositories;
using System.Security.Claims;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Security.Claims;

namespace Demo.Auth.OpenIddict
{
    //Newly Added TokenExtensionGrant
    public class MyTokenExtensionGrant : ITokenExtensionGrant
    {
        public const string ExtensionGrantName = "MyTokenExtensionGrant";
       
        public MyTokenExtensionGrant()
        {
        }

       
        public string Name => ExtensionGrantName;

        public async Task<IActionResult> HandleAsync(ExtensionGrantContext context)
        {
           
            var IdentityUserManager = context.HttpContext.RequestServices.GetRequiredService<IdentityUserManager>();
            var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<Volo.Abp.Identity.IdentityUser>>();
            var scopeManager = context.HttpContext.RequestServices.GetRequiredService<IOpenIddictScopeManager>();
            var abpOpenIddictClaimsPrincipalManager = context.HttpContext.RequestServices
              .GetRequiredService<AbpOpenIddictClaimsPrincipalManager>();
            var identitySecurityLogManager = context.HttpContext.RequestServices.GetRequiredService<IdentitySecurityLogManager>();

            // get phone number from request
            var phoneNumber = context.Request.GetParameter("phone_number")?.Value?.ToString();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ForbidResult(new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                                        properties: new AuthenticationProperties(new Dictionary<string, string>
                                        {
                                            [OpenIddictServerAspNetCoreConstants.Properties.Error] =
                                                OpenIddictConstants.Errors.InvalidGrant
                                        }!));
            }

            // retrieve user
            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IRepository<Volo.Abp.Identity.IdentityUser, Guid>>();
            var user = await userRepository.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return new ForbidResult(new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                                        properties: new AuthenticationProperties(new Dictionary<string, string>
                                        {
                                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant
                                        }!));
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);

            //Get Roles for user and add claim
            var roles = await IdentityUserManager.GetRolesAsync(user);
            principal.AddClaim("role", string.Join(",", roles));
            
            principal.SetScopes(context.Request.GetScopes());
            principal.SetResources(await GetResourcesAsync(context.Request.GetScopes(), scopeManager));
            await abpOpenIddictClaimsPrincipalManager.HandleAsync(context.Request, principal);
            await identitySecurityLogManager.SaveAsync(
                new IdentitySecurityLogContext
                {
                    Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
                    Action = OpenIddictSecurityLogActionConsts.LoginSucceeded,
                    UserName = context.Request.Username,
                    ClientId = context.Request.ClientId
                }
            );

            return new Microsoft.AspNetCore.Mvc.SignInResult(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                principal);
        }

        protected virtual async Task<IEnumerable<string>> GetResourcesAsync(ImmutableArray<string> scopes,
           IOpenIddictScopeManager scopeManager)
        {
            var resources = new List<string>();
            if (!scopes.Any())
            {
                return resources;
            }

            await foreach (var resource in scopeManager.ListResourcesAsync(scopes))
            {
                resources.Add(resource);
            }

            return resources;
        }
    }
}
