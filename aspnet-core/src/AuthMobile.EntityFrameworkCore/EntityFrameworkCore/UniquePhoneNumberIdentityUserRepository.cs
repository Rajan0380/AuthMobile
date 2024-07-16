using AuthMobile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Demo.Auth
{
    //Newly Added UniquePhoneNumberIdentityUserRepository
    public class UniquePhoneNumberIdentityUserRepository : EfCoreIdentityUserRepository,
        IUniquePhoneNumberIdentityUserRepository
    {
        public UniquePhoneNumberIdentityUserRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public virtual async Task<IdentityUser> FindByConfirmedPhoneNumberAsync(string phoneNumber, bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return includeDetails
                ? await (await WithDetailsAsync()).FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber,
                    GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<IdentityUser> GetByConfirmedPhoneNumberAsync(string phoneNumber, bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            var identityUser =
                await FindByConfirmedPhoneNumberAsync(phoneNumber, includeDetails, GetCancellationToken(cancellationToken));

            if (identityUser == null)
            {
                throw new EntityNotFoundException(typeof(IdentityUser));
            }

            return identityUser;
        }
    }
}
