using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagerApi.Controllers;
using BookManagerApi.Models;
using BookManagerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookManagerApi.Tests.ServiceTests
{
    public class BookManagerServiceTest
    {
        // Have a BookRepository class (contains DBContext) and then mock it
        private BookManagementService _service;

        [SetUp]
        public void Setup()
        {
            //Arrange
        }

        [Test]
        public void GetAllBooks_Returns_AllBooks()
        {
            //Arange

            //Act
            //var result = _service.GetAllBooks();

            //Assert
        }
    }
}



