using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Helpers;
using Anuitex.AngularLibrary.Models.IO.Export.Books;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;
using Anuitex.AngularLibrary.Models.IO.Import;
using Anuitex.AngularLibrary.Models.IO.Import.Books;
using Anuitex.AngularLibrary.Models.IO.Import.Journals;
using Anuitex.AngularLibrary.Models.IO.Import.Newspapers;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class ImportController : BaseApiController
    {
        [Route("api/Import/UploadImportFile")]
        public async Task<ImportResultModel> UploadImportFile()
        {
            ImportResultModel result = new ImportResultModel();
            MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();  

            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (HttpContent content in provider.Contents)
            {
                byte[] fileBytes = await content.ReadAsByteArrayAsync();

                try
                {
                    FileInfo info = new FileInfo(content.Headers.ContentDisposition.FileName.Trim('\"'));
                    result = ImportHelper.Import(new MemoryStream(fileBytes), info.Extension == ".xml");                    
                }
                catch (Exception e)
                {
                    result.Message = e.Message;
                    return result;
                }                
            }
            return result;
        }
        [Route("api/Import/ConfirmImport")]
        public IHttpActionResult ConfirmImport(ImportResultModel model)
        {
            if (model != null)
            {
                if (model.Books != null && model.Books.Any())
                {
                    DataContext.Books.InsertAllOnSubmit(model.Books.Where(bs => bs.Selected)
                        .Select(book => new Book()
                        {
                            Amount = book.Amount,
                            Author = book.Author,
                            Genre = book.Genre,
                            Pages = book.Pages,
                            PhotoId = book.PhotoId,
                            Price = book.Price,
                            Title = book.Title,
                            Year = book.Year
                        }));
                    DataContext.SubmitChanges();
                }
                if (model.Journals != null && model.Journals.Any())
                {
                    DataContext.Journals.InsertAllOnSubmit(model.Journals.Where(jour => jour.Selected)
                        .Select(journal => new Journal()
                        {
                            Amount = journal.Amount,
                            Periodicity = journal.Periodicity,
                            Subjects = journal.Subjects,
                            Date = journal.Date,
                            PhotoId = journal.PhotoId,
                            Price = journal.Price,
                            Title = journal.Title
                        }));
                    DataContext.SubmitChanges();
                }
                if (model.Newspapers != null && model.Newspapers.Any())
                {
                    DataContext.Newspapers.InsertAllOnSubmit(model.Newspapers.Where(np => np.Selected)
                        .Select(newspaper => new Newspaper()
                        {
                            Amount = newspaper.Amount,
                            Periodicity = newspaper.Periodicity,
                            Date = newspaper.Date,
                            PhotoId = newspaper.PhotoId,
                            Price = newspaper.Price,
                            Title = newspaper.Title
                        }));
                    DataContext.SubmitChanges();
                }
            }
            return Ok();
        }
    }
}
