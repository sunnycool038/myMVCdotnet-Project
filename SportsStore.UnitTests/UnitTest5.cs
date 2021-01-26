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
    
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void Indicate_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1",Category="Apples" },
                  new Product {ProductID=4,Name="P4" ,Category="Orange"},

            });
            //Arrange
            NavController Target = new NavController(mock.Object);
            string CategoryToSelect = "Apples";

            //Act
            string result = Target.Menu(CategoryToSelect).ViewBag.SelectedCategory;

            //Assert
            
            Assert.AreEqual(CategoryToSelect, result);
        }
    }
}
