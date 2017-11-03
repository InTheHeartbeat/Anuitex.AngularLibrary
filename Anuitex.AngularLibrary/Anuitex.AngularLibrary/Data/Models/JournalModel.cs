using System;

namespace Anuitex.AngularLibrary.Data.Models
{
    [Serializable]
    public class JournalModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Periodicity { get; set; }
        public string Subjects { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public int? PhotoId { get; set; }
        public string PhotoPath { get; set; }        

        public JournalModel(Journal baseJournal)
        {
            Id = baseJournal.Id;
            Title = baseJournal.Title;
            Periodicity = baseJournal.Periodicity;
            Subjects = baseJournal.Subjects;
            Date = baseJournal.Date;
            Amount = baseJournal.Amount;
            Price = baseJournal.Price;
            PhotoId = baseJournal.PhotoId;
            PhotoPath = baseJournal.Image?.Path;            
        }

        public JournalModel()
        {}
    }
}