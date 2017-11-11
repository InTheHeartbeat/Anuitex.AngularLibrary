using System.Collections.Generic;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.AngularLibrary.Models.IO.Import.Newspapers
{
    public class ImportNewspapersResultModel
    {
        public List<ExportableNewspaperModel> Newspapers { get; set; }        
        public ImportNewspapersResultModel()
        {
            
        }
    }
}