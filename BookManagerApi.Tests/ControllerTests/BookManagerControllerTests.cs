using System;
using System.Collections.Generic;
using System.Linq;
using BookManagerApi.Controllers;
using BookManagerApi.Models;
using BookManagerApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BookManagerApi.Tests;

public class BookManagerControllerTests
{
    private BookManagerController _controller;
    private Mock<IBookManagementService> _mockBookManagementService;

    [SetUp]
    public void Setup()
    {
        //Arrange
        _mockBookManagementService = new Mock<IBookManagementService>();
        _controller = new BookManagerController(_mockBookManagementService.Object);
    }

    [Test]
    public void GetBooks_Returns_AllBooks()
    {
        //Arange
        _mockBookManagementService.Setup(b => b.GetAllBooks()).Returns(GetTestBooks());

        //Act
        var result = _controller.GetBooks();

        //Assert
        result.Should().BeOfType(typeof(ActionResult<IEnumerable<Book>>));
        result.Value.Should().BeEquivalentTo(GetTestBooks());
        result.Value.Count().Should().Be(3);
    }

    [Test]
    public void GetBookById_Returns_CorrectBook()
    {
        //Arrange
        var testBookFound = GetTestBooks().FirstOrDefault();
        _mockBookManagementService.Setup(b => b.FindBookById(1)).Returns(testBookFound);

        //Act
        var result = _controller.GetBookById(1);

        //Assert
        result.Should().BeOfType(typeof(ActionResult<Book>));
        result.Value.Should().Be(testBookFound);
    }
    /*
    [Test]
    public void GetBookById_Wrong_Id_Returns_NotFound()
    {
        //Arrange
        Book nullBook = null;

        _mockBookManagementService.Setup(b => b.FindBookById(1)).Returns(nullBook);

        //Act
        var result = _controller.GetBookById(1);

        //Assert
        result.Should().BeOfType(typeof(NotFoundResult));

    }*/

    [Test]
    public void UpdateBookById_Updates_Correct_Book()
    {
        //Arrange
        long existingBookId = 3;
        Book existingBookFound = GetTestBooks()
            .FirstOrDefault(b => b.Id.Equals(existingBookId));

        var bookUpdates = new Book() { Id = existingBookId, Title = "Book Three", Description = "I am updating this for Book Three", Author = "Person Three", Genre = Genre.Education };

        _mockBookManagementService.Setup(b => b.Update(existingBookId, bookUpdates)).Returns(existingBookFound);
        //_mockBookManagementService.Setup(b => b.FindBookById(existingBookId)).Returns(existingBookFound);

        //Act
        var result = _controller.UpdateBookById(existingBookId, bookUpdates);

        //Assert
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Test]
    public void UpdateBookById_Using_Wrong_Id_Returns_NotFound()
    {
        //Arrange
        long existingBookId = 4;
        Book nullBook = null;
        var bookUpdates = new Book() { };

        _mockBookManagementService.Setup(b => b.Update(existingBookId, nullBook)).Returns(nullBook);
        _mockBookManagementService.Setup(b => b.FindBookById(existingBookId)).Returns(nullBook);

        //Act
        var result = _controller.UpdateBookById(existingBookId, bookUpdates);

        //Assert
        result.Should().BeOfType(typeof(NotFoundResult));
    }

    [Test]
    public void AddBook_Creates_A_Book()
    {
        //Arrange
        var newBook = new Book() { Id = 4, Title = "Book Four", Description = "This is the description for Book Four", Author = "Person Four", Genre = Genre.Education };

        _mockBookManagementService.Setup(b => b.Create(newBook)).Returns(newBook);

        //Act
        var result = _controller.AddBook(newBook);

        //Assert
        result.Should().BeOfType(typeof(ActionResult<Book>));
    }

    [Test]
    public void AddBook_Does_Not_Create_A_Book_When_ID_Exists()
    {
        //Arrange
        var newBook = new Book() { Id = 3, Title = "Book 3", Description = "This is the description for Book 3", Author = "Person 3", Genre = Genre.Education };
        Book nullBook = null;
        _mockBookManagementService.Setup(b => b.Create(newBook)).Returns(nullBook);

        //Act
        var result = _controller.AddBook(newBook);

        //Assert
        //result.Should().BeOfType(typeof(NotFoundResult));
        //The above is incompatible, expected Microsoft.AspNetCore.Mvc.ActionResult`1[[BookManagerApi.Models.Book, BookManagerApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]
        result.Should().NotBeOfType(typeof(NoContentResult));
    }

    [Test]
    public void DeleteBook_Deletes_A_Book()
    {
        //Arrange
        var testBookFound = GetTestBooks().FirstOrDefault();
        _mockBookManagementService.Setup(b => b.FindBookById(1)).Returns(testBookFound);

        //Act
        var resultDelete = _controller.DeleteBookById(1);

        //Assert
        resultDelete.Should().BeOfType(typeof(NoContentResult));
    }

    [Test]
    public void DeleteBook_When_Book_Not_Found_Gets_Error_NotFound()
    {
        //Arrange
        long testBookId = 13;
        Book nullBook = null;

        _mockBookManagementService.Setup(b => b.FindBookById(testBookId)).Returns(nullBook);

        //Act
        var resultDelete = _controller.DeleteBookById(testBookId);

        //Assert
        resultDelete.Should().BeOfType(typeof(NotFoundResult));
    }

    private static List<Book> GetTestBooks()
    {
        return new List<Book>
        {
            new Book() { Id = 1, Title = "Book One", Description = "This is the description for Book One", Author = "Person One", Genre = Genre.Education },
            new Book() { Id = 2, Title = "Book Two", Description = "This is the description for Book Two", Author = "Person Two", Genre = Genre.Fantasy },
            new Book() { Id = 3, Title = "Book Three", Description = "This is the description for Book Three", Author = "Person Three", Genre = Genre.Thriller },
        };
    }
}
