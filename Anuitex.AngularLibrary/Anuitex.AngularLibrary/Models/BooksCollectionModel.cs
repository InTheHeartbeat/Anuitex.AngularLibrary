using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Models
{
    public class BooksCollectionModel : BaseModel
    {
        public BookModel[] Books { get; set; }
    }
}