using Volo.Abp.Modularity;

namespace AuthMobile;

[DependsOn(
    typeof(AuthMobileDomainModule),
    typeof(AuthMobileTestBaseModule)
)]
public class AuthMobileDomainTestModule : AbpModule
{

}
