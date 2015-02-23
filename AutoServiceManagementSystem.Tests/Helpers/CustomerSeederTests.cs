using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using AutoServiceManagementSystem.Helpers;
using AutoServiceManagementSystem.Models;

namespace AutoServiceManagementSystem.Tests.Helpers
{
    [TestClass]
    public class CustomerSeederTests
    {
        [TestMethod]
        public void PhoneNumberGeneratorShouldGenerateRandomNumbers()
        {
            var uniqueNumbers = new HashSet<string>();
            int count = 9999;

            for (int i = 0; i < count; i++)
            {
                string number = CustomerSeeder.GeneratePhoneNumber();
                uniqueNumbers.Add(number);
            }

            Assert.IsTrue(9500 < uniqueNumbers.Count);
        }

        [TestMethod]
        public void GenerateSexShouldGenerateRandomSexes()
        {
            int maleCount   = 0;
            int femaleCount = 0;
            int count = 9999;

            for (int i = 0; i < count; i++)
            {
                var sex = CustomerSeeder.GenerateSex();
                switch (sex)
                {
                    case CustomerSeeder.Sex.Male:
                        maleCount++;
                        break;
                    case CustomerSeeder.Sex.Female:
                        femaleCount++;
                        break;
                    default:
                        break;
                }
            }

            Debug.WriteLine("Male count: {0}, Female count {1}",
                maleCount, femaleCount);

            Assert.IsTrue(femaleCount > count / 3);
        }

        [TestMethod]
        public void GenerateNamesShouldGenerateRandomNames()
        {
            var uniqueNames = new HashSet<string>();
            int count = 9999;

            for (int i = 0; i < count; i++)
            {
                var sex = CustomerSeeder.GenerateSex();
                var name = CustomerSeeder.GenerateName(sex);
                string firstName = name["first"];
                string lastName = name["last"];
                string fullName = firstName + " " + lastName;
                // Debug.WriteLine(fullName);
                uniqueNames.Add(fullName);
            }
            // 944 = maximum different combinations
            Assert.AreEqual(944, uniqueNames.Count);
        }

        [TestMethod]
        public void NextCustomerShouldGenerateRandomCustomers()
        {
            var uniqueCustomers = new HashSet<Customer>();
            int count = 1000;

            for (int i = 0; i < count; i++)
            {
                var customer = CustomerSeeder.NextCustomer();
                Debug.WriteLine(customer.FirstName);
                Debug.WriteLine(customer.LastName);
                Debug.WriteLine(customer.PhoneNumber);
                uniqueCustomers.Add(customer);
            }

            Assert.AreEqual(count, uniqueCustomers.Count);
        }
    }
}
