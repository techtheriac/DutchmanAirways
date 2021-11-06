using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlyingDutchmanAirlines.DatabaseLayer;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);
        }

        [TestMethod]
        public async Task CreateCustomer_Success()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("Franklin Jezreel");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsNull()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsEmpty()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('%')]
        [DataRow('&')]
        [DataRow('*')]
        public async Task CreateCustomer_Failure_NameContainsInvalidCharacter(char invalidCharacter)
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("Franklin Jezreel " + invalidCharacter);
            Assert.IsFalse(result);
        }
    }
}