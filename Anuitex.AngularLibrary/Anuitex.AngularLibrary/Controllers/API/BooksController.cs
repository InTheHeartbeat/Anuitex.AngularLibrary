﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Extensions;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class BooksController :BaseApiController
    {       

        public IQueryable<BookModel> GetBooks()
        {
            return DataContext.Books.Select(b=>new BookModel(b));
        }

        [ResponseType(typeof(BookModel))]
        [HttpPost]
        public IHttpActionResult AddBook(BookModel book)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}
            if (CurrentUser == null || !CurrentUser.IsAdmin){return Unauthorized();}

            DataContext.Books.InsertOnSubmit(new Book()
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Amount = book.Amount,
                Pages = book.Pages,
                Price = book.Price,
                Year = book.Year,
                PhotoId = book?.PhotoId
            });
            DataContext.SubmitChanges();

            return Ok();
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult EditBook(BookModel bookModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Book book = DataContext.Books.FirstOrDefault(b => b.Id == bookModel.Id);

            if (book == null){return NotFound();}

            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.Genre = bookModel.Genre;
            book.Pages = bookModel.Pages;
            book.Year = bookModel.Year;
            book.Price = bookModel.Price;
            book.Amount = bookModel.Amount;
            book.PhotoId = bookModel?.PhotoId;

            DataContext.SubmitChanges();

            return Ok();
        }
        
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Book forDelete = DataContext.Books.FirstOrDefault(b => b.Id == id);

            if (forDelete == null){return NotFound();}

            DataContext.Books.DeleteOnSubmit(forDelete);
            DataContext.SubmitChanges();

            return Ok();
        }
    }
}
