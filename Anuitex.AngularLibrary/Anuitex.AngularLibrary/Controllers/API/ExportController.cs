using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Anuitex.AngularLibrary.Helpers;
using Anuitex.AngularLibrary.Models.IO.Export.Books;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class ExportController : BaseApiController
    {
        [Route("api/Export/GetExportableBooks")]
        [HttpGet]
        public IHttpActionResult GetExportableBooks()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {
                return Unauthorized();
            }
            try
            {
                return Ok(new ExportBooksModel()
                {
                    Books = DataContext.Books.Select(book => new ExportableBookModel(book)).ToList(),
                    IsXml = true
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/Export/GetExportableJournals")]
        public IHttpActionResult GetExportableJournals()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            try
            {
                return Ok(new ExportJournalsModel()
                {
                    Journals = DataContext.Journals.Select(j => new ExportableJournalModel(j)).ToList(),
                    IsXml = true
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [Route("api/Export/GetExportableNewspapers")]
        public IHttpActionResult GetExportableNewspapers()
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {
                return Unauthorized();
            }

            try
            {
                return Ok(new ExportNewspapersModel()
                {
                    Newspapers = DataContext.Newspapers.Select(n => new ExportableNewspaperModel(n)).ToList(),
                    IsXml = true
                });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
        [Route("api/Export/TryExportBooks")]
        public IHttpActionResult TryExportBooks(ExportBooksModel model)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {
                return Unauthorized();
            }
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ExportHelper.ExportBooks(model, DataContext))
                };
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "exp-" + DateTime.Now + "-books" + (model.IsXml ? ".xml" : ".txt")
                    };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = ResponseMessage(result);

                return response;
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
       
        [Route("api/Export/TryExportNewspapers")]
        public IHttpActionResult TryExportNewspapers(ExportNewspapersModel model)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {
                return Unauthorized();
            }

            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ExportHelper.ExportNewspapers(model, DataContext))
                };
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "exp-" + DateTime.Now + "-newspapers" + (model.IsXml ? ".xml" : ".txt")
                    };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = ResponseMessage(result);

                return response;
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
        [Route("api/Export/TryExportJournals")]
        public IHttpActionResult TryExportJournals(ExportJournalsModel model)
        {
            if (CurrentUser == null || !CurrentUser.IsAdmin)
            {
                return Unauthorized();
            }

            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(ExportHelper.ExportJournals(model, DataContext))
                };
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "exp-" + DateTime.Now + "-journals" + (model.IsXml ? ".xml" : ".txt")
                    };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = ResponseMessage(result);

                return response;
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }        
    }
}
