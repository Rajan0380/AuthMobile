using Volo.Abp.Modularity;

namespace AuthMobile;

/* Inherit from this class for your domain layer tests. */
public abstract class AuthMobileDomainTestBase<TStartupModule> : AuthMobileTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
