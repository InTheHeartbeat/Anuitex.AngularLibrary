using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class NewspapersController : BaseApiController
    {
        public IQueryable<NewspaperModel> GetNewspapers()
        {
            return DataContext.Newspapers.Select(b => new NewspaperModel(b));
        }

        [ResponseType(typeof(NewspaperModel))]
        [HttpPost]
        public IHttpActionResult AddNewspaper(NewspaperModel newspaper)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            try
            {
                DataContext.Newspapers.InsertOnSubmit(new Newspaper()
                {
                    Title = newspaper.Title,
                    Date = newspaper.Date,
                    Periodicity = newspaper.Periodicity,
                    Amount = newspaper.Amount,
                    Price = newspaper.Price,
                    PhotoId = newspaper?.PhotoId
                });
                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult EditNewspaper(NewspaperModel newspaperModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Newspaper newspaper = DataContext.Newspapers.FirstOrDefault(b => b.Id == newspaperModel.Id);

            if (newspaper == null) { return NotFound(); }

            try
            {
                newspaper.Title = newspaperModel.Title;
                newspaper.Date = newspaperModel.Date;
                newspaper.Periodicity = newspaperModel.Periodicity;
                newspaper.Price = newspaperModel.Price;
                newspaper.Amount = newspaperModel.Amount;
                newspaper.PhotoId = newspaperModel?.PhotoId;

                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteNewspaper(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Newspaper forDelete = DataContext.Newspapers.FirstOrDefault(b => b.Id == id);

            if (forDelete == null) { return NotFound(); }

            try
            {
                DataContext.Newspapers.DeleteOnSubmit(forDelete);
                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }
    }
}
