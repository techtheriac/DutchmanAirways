using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private FlyingDutchmanAirlinesContext _context;
        private CustomerRepository _repository;


        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions =
                new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;

            _context = new FlyingDutchmanAirlinesContext(dbContextOptions);

            Customer testCustomer = new Customer("Linus Torvalds");
            _context.Customers.Add(testCustomer);
            await _context.SaveChangesAsync();

            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task CreateCustomer_Success()
        {
            bool result = await _repository.CreateCustomer("Franklin Jezreel");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsNull()
        {           
            bool result = await _repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public async Task CreateCustomer_Failure_NameIsEmpty()
        {            
            bool result = await _repository.CreateCustomer("");
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
            bool result = await _repository.CreateCustomer("Franklin Jezreel " + invalidCharacter);
            Assert.IsFalse(result);
        }
       
        [TestMethod]
        public async Task CreateCustomer_Failure_DatabaseAccessError()
        {
            CustomerRepository repository = new CustomerRepository(null);
            Assert.IsNotNull(repository);

            bool result = await repository.CreateCustomer("Franklin Jezreel");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetCustomerByName_Success()
        {
            Customer customer = await _repository.GetCustomerByName("Linus Torvalds");
            Assert.IsNotNull(customer);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("#")]
        [DataRow("$")]
        [DataRow("%")]
        [DataRow("&")]
        [DataRow("*")]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task GetCustomerByName_Failure_InvalidName(string name)
        {
            await _repository.GetCustomerByName(name);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public async Task GetCustomerByName_Failure_CustomerNotFound()
        {
            await _repository.GetCustomerByName("Pius Trovan");
        }
    }
}