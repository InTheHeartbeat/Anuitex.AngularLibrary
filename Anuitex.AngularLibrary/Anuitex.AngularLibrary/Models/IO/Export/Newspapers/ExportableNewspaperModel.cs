using System;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Models.IO.Export.Newspapers
{    
    public class ExportableNewspaperModel : NewspaperModel
    {
        public bool Selected { get; set; }
        public ExportableNewspaperModel(Newspaper baseNewspaper) : base(baseNewspaper)
        {
        }

        public ExportableNewspaperModel()
        {
            
        }
    }
}