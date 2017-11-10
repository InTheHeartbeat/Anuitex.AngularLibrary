using System;

namespace Anuitex.AngularLibrary.Data.Models
{    
    public partial class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }        
        public string PhotoPath { get; set; }
        public int PhotoId { get; set; }        
        public BookModel(Book baseBook)
        {
            Id = baseBook.Id;
            Title = baseBook.Title;
            Year = baseBook.Year;
            Pages = baseBook.Pages;
            Author = baseBook.Author;
            Genre = baseBook.Genre;
            Amount = baseBook.Amount;
            Price = baseBook.Price;            
            PhotoPath = baseBook.Image?.Path;
            if (baseBook.PhotoId != null) PhotoId = (int) baseBook.PhotoId;
        }

        public BookModel()
        {}
    }
}