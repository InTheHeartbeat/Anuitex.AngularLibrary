using System;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Models.IO.Export.Journals
{
    public class ExportableJournalModel : JournalModel
    {
        public bool Selected { get; set; }

        public ExportableJournalModel(Journal baseJournal) : base(baseJournal)
        {
        }

        public ExportableJournalModel()
        {
            
        }
    }
}