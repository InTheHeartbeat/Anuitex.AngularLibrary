using System;

namespace Anuitex.AngularLibrary.Data.Models
{
    [Serializable]
    public class NewspaperModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Periodicity { get; set; }        
        public string Date { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int? PhotoId { get; set; }
        public string PhotoPath { get; set; }        
        public NewspaperModel(Newspaper baseNewspaper)
        {
            Id = baseNewspaper.Id;
            Title = baseNewspaper.Title;
            Periodicity = baseNewspaper.Periodicity;            
            Date = baseNewspaper.Date;
            Amount = baseNewspaper.Amount;
            Price = baseNewspaper.Price;
            PhotoId = baseNewspaper.PhotoId;
            PhotoPath = baseNewspaper.Image?.Path;            
        }

        public NewspaperModel()
        {            
        }
    }
}