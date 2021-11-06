using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;
        public async Task<bool> CreateCustomer(string name)
        {
            if (IsInvalidCustomerName(name))
                return false;

            try
            {
                Customer newCustomer = new Customer(name);

                using (_context)
                {
                    _context.Customers.Add(newCustomer);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {

                return false;
            }
            

            return true;
        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = { '!', '@', '#', '$', '%', '&', '*' };
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }

        public CustomerRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;                
        }
    }
}