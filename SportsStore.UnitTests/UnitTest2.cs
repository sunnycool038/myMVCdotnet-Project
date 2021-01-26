using System;
using System.Text;
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
    public class UnitTest2
    {
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID=1,Name="P1" },
                new Product {ProductID=2,Name="P2" },
                 new Product {ProductID=3,Name="P3" },
                  new Product {ProductID=4,Name="P4" },
                   new Product {ProductID=5,Name="P5" }
            });
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null, 2).Model;

            //Assert
            PagingInfo pagingInfo= result.pagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage,3);
            Assert.AreEqual(pagingInfo.TotalItems,5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}
