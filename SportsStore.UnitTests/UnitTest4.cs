using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    /// <summary>
    /// Summary description for UnitTest4
    /// </summary>
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1",Category="Apples" },
                new Product {ProductID=2,Name="P2",Category="Apples" },
                 new Product {ProductID=3,Name="P3",Category="Plums" },
                  new Product {ProductID=4,Name="P4" ,Category="Orange"},
                   
            });
            //Arrange
            NavController Target = new NavController(mock.Object);

            //Act
            string[] result = ((IEnumerable<string>)Target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Orange");
            Assert.AreEqual(result[2], "Plums");
        }
    }
}
