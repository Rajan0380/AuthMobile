using System.Threading.Tasks;

namespace AuthMobile.Data;

public interface IAuthMobileDbSchemaMigrator
{
    Task MigrateAsync();
}
