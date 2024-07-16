using AuthMobile.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AuthMobile.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AuthMobileEntityFrameworkCoreModule),
    typeof(AuthMobileApplicationContractsModule)
)]
public class AuthMobileDbMigratorModule : AbpModule
{
}
