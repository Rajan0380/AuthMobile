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
            var uniquePhoneNumberIdentityUserRepository = context.HttpContext.RequestServices
                .GetRequiredService<IUniquePhoneNumberIdentityUserRepository>();
            var scopeManager = context.HttpContext.RequestServices.GetRequiredService<IOpenIddictScopeManager>();
            var abpOpenIddictClaimsPrincipalManager = context.HttpContext.RequestServices
              .GetRequiredService<AbpOpenIddictClaimsPrincipalManager>();
            var phoneNumber = context.Request.GetParameter("phone_number")?.ToString();
            var identityUser =
               await uniquePhoneNumberIdentityUserRepository.GetByConfirmedPhoneNumberAsync(phoneNumber);
            var principal = await signInManager.CreateUserPrincipalAsync(identityUser);
            // Fetch permissions specific to the user
            var userId = identityUser.Id.ToString();
            var roles = await IdentityUserManager.GetRolesAsync(identityUser);
            principal.AddClaim("role", string.Join(",", roles));
            
            principal.SetScopes(context.Request.GetScopes());
            principal.SetResources(await GetResourcesAsync(context.Request.GetScopes(), scopeManager));

            await abpOpenIddictClaimsPrincipalManager.HandleAsync(context.Request, principal);

            //await identitySecurityLogManager.SaveAsync(
            //    new IdentitySecurityLogContext
            //    {
            //        Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
            //        Action = OpenIddictSecurityLogActionConsts.LoginSucceeded,
            //        UserName = context.Request.Username,
            //        ClientId = context.Request.ClientId
            //    }
            //);

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
