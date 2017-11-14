using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Anuitex.AngularLibrary.Data;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class UploadController : BaseApiController
    {
        [HttpPost]
        [Route("api/Upload/LoadImage")]
        public async Task<IHttpActionResult> LoadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            try
            {
                MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();

                string relPath = "/Upload/Images/";
                string root = System.Web.HttpContext.Current.Server.MapPath("~/" + relPath);
                int id = -1;

                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (HttpContent content in provider.Contents)
                {
                    string fileName = content.Headers.ContentDisposition.FileName.Trim('\"');
                    string customFileName = Guid.NewGuid().ToString() + new FileInfo(fileName).Extension;
                    byte[] fileBytes = await content.ReadAsByteArrayAsync();

                    using (FileStream fs = new FileStream(root + customFileName, FileMode.Create))
                    {
                        await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }
                    DataContext.Images.InsertOnSubmit(new Image() {Path = relPath + customFileName});
                    DataContext.SubmitChanges();
                    id = DataContext.Images.FirstOrDefault(img => img.Path == relPath + customFileName).Id;
                }
                return Ok(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }            
        }
    }
}
