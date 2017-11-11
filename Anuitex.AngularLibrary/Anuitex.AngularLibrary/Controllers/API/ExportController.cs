using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Helpers;
using Anuitex.AngularLibrary.Models.IO.Export.Books;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class ExportController : BaseApiController
    {        
        [System.Web.Mvc.Route("api/Export/GetExportableBooks")]
        [System.Web.Mvc.HttpGet]
        public ExportBooksModel GetExportableBooks()
        {
            return new ExportBooksModel()
            {
                Books = DataContext.Books.Select(book => new ExportableBookModel(book)).ToList(),
                IsXml = true
            };
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("api/Export/TryExportBooks")]
        public IHttpActionResult TryExportBooks(ExportBooksModel model)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ExportHelper.ExportBooks(model, DataContext))
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "exp-" + DateTime.Now + "-books" + (model.IsXml ? ".xml" : ".txt")
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = ResponseMessage(result);

            return response;
        }
        /*
        [HttpGet]
        [Route("api/Export/GetJournals")]
        public IQueryable<ExportableJournalModel> GetExportableJournals()
        {
            return DataContext.Journals.Select(j => new ExportableJournalModel(j));
        }
        [HttpGet]
        [Route("api/Export/GetNewspapers")]
        public IQueryable<ExportableNewspaperModel> GetExportableNewspapers()
        {
            return DataContext.Newspapers.Select(n => new ExportableNewspaperModel(n));
        }

        

        [HttpPost]
        [Route("api/Export/Journals")]
        public FileResult TryExportJournals(ExportJournalsModel model)
        {
            FileContentResult res = new FileContentResult(ExportHelper.ExportJournals(model, DataContext),
                System.Net.Mime.MediaTypeNames.Application.Octet)
            {
                FileDownloadName = "exp-" + DateTime.Now + "-journals" + (model.IsXml ? ".xml" : ".txt")
            };
            return res;
        }

        [HttpPost]
        [Route("api/Export/Newspapers")]
        public FileResult TryExportNewspapers(ExportNewspapersModel model)
        {
            FileContentResult res = new FileContentResult(ExportHelper.ExportNewspapers(model, DataContext),
                System.Net.Mime.MediaTypeNames.Application.Octet)
            {
                FileDownloadName = "exp-" + DateTime.Now + "-newspapers" + (model.IsXml ? ".xml" : ".txt")
            };
            return res;
        }*/
    }
}
