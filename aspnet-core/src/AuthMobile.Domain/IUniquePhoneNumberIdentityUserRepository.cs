using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AuthMobile
{
    // Newly Added IUniquePhoneNumberIdentityUserRepository
    public interface IUniquePhoneNumberIdentityUserRepository : IRepository<IdentityUser, Guid>, IIdentityUserRepository
    {
        Task<IdentityUser> FindByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<IdentityUser> GetByConfirmedPhoneNumberAsync([NotNull] string phoneNumber, bool includeDetails = true, CancellationToken cancellationToken = default);
    }
}
