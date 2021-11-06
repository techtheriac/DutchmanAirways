using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        public async Task<bool> CreateCustomer(string name)
        {
            if (IsInvalidCustomerName(name))
                return false;

            Customer newCustomer = new Customer(name);

            using(FlyingDutchmanAirlinesContext context = new FlyingDutchmanAirlinesContext())
            {
                context.Customers.Add(newCustomer);
                await context.SaveChangesAsync();
            }

            return true;
        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}