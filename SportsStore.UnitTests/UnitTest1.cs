﻿using System;
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
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1",Category="Cat1" },
                new Product {ProductID=2,Name="P2",Category="Cat2" },
                 new Product {ProductID=3,Name="P3",Category="Cat1" },
                  new Product {ProductID=4,Name="P4" ,Category="Cat2"},
                   new Product {ProductID=5,Name="P5",Category="Cat3" }
            });
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
    }
}
