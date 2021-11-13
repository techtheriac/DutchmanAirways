using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.Stubs
{
    public class FlyingDutchmanAirlinesContext_Stub : FlyingDutchmanAirlinesContext
    {
        public FlyingDutchmanAirlinesContext_Stub(DbContextOptions<FlyingDutchmanAirlinesContext> options) : base(options)
        {

        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Exceptions are devoid of type signatures
            Func<Task<int>>[] functions =
            {
                () => throw new Exception("Database Error!"),
                async () => await base.SaveChangesAsync(cancellationToken),
            };

            return await functions[(int)base.Bookings.First().CustomerId].Invoke();
        }
    }
}
