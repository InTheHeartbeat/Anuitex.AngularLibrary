using System;
using System.Collections.Generic;

namespace Anuitex.AngularLibrary.Models.IO.Export.Newspapers
{    
    public class ExportNewspapersModel
    {
        public List<ExportableNewspaperModel> Newspapers { get; set; }
        public bool IsXml { get; set; }

        public ExportNewspapersModel()
        {
            
        }
    }
}