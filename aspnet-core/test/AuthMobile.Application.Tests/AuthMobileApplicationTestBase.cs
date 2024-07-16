using Volo.Abp.Modularity;

namespace AuthMobile;

public abstract class AuthMobileApplicationTestBase<TStartupModule> : AuthMobileTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
