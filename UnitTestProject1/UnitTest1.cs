using CaseStudy_WPF.Configuration;
using CaseStudy_WPF.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAllEmployee()
        {
            var pageNo = 1;
            var result = EmployeeService.GetAllEmployee(pageNo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            try
            {
                var employeeId = 1;
                EmployeeService.DeleteEmployee(employeeId);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AddEmployee()
        {
            try
            {
                EmployeeDetails employeeDetails = new EmployeeDetails();
                employeeDetails.Name = "test";
                employeeDetails.Email = "test@yopmail.com";
                employeeDetails.Status = Common.inactive;
                employeeDetails.Gender = Common.male;
                var response = EmployeeService.AddEmployee(employeeDetails);
                Assert.AreSame(response.IsSuccessStatusCode, true);
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void UpdateEmployeeAsync()
        {
            try
            {

                EmployeeDetails employeeDetails = new EmployeeDetails();
                employeeDetails.Id = 1;
                employeeDetails.Name = "test";
                employeeDetails.Email = "test@yopmail.com";
                employeeDetails.Status = Common.inactive;
                employeeDetails.Gender = Common.male;
                EmployeeService.UpdateEmployeeAsync(employeeDetails);
            }
            catch
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void GetEmployeeDetails()
        {
            try
            {
                var employeeId = 1;
                var result = EmployeeService.GetEmployeeDetails(employeeId);
                Assert.IsNotNull(result);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
