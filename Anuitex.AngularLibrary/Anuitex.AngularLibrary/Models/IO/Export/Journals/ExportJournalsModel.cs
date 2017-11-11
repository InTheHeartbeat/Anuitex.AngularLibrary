using System.Collections.Generic;

namespace Anuitex.AngularLibrary.Models.IO.Export.Journals
{
    public class ExportJournalsModel
    {
        public List<ExportableJournalModel> Journals { get; set; }
        public bool IsXml { get; set; }

        public ExportJournalsModel()
        {
            
        }
    }
}