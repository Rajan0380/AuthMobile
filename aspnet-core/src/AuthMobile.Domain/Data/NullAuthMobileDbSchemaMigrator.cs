using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AuthMobile.Data;

/* This is used if database provider does't define
 * IAuthMobileDbSchemaMigrator implementation.
 */
public class NullAuthMobileDbSchemaMigrator : IAuthMobileDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
