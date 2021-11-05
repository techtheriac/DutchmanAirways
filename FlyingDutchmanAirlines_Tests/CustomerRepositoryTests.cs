using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void CreateCustomer_Success()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("Franklin Jezreel");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateCustomer_Failure_NameIsNull()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateCustomer_Failure_NameIsEmpty()
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("");
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('%')]
        [DataRow('&')]
        [DataRow('*')]
        public void CreateCustomer_Failure_NameContainsInvalidCharacter(char invalidCharacter)
        {
            CustomerRepository repository = new CustomerRepository();
            Assert.IsNotNull(repository);

            bool result = repository.CreateCustomer("Franklin Jezreel " + invalidCharacter);
            Assert.IsFalse(result);
        }
    }
}
