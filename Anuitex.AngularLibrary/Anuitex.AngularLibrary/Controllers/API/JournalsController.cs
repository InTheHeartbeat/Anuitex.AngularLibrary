using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class JournalsController : BaseApiController
    {
        public IQueryable<JournalModel> GetJournals()
        {
            return DataContext.Journals.Select(b => new JournalModel(b));
        }

        [ResponseType(typeof(JournalModel))]
        [HttpPost]
        public IHttpActionResult AddJournal(JournalModel journal)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            try
            {
                DataContext.Journals.InsertOnSubmit(new Journal()
                {
                    Title = journal.Title,
                    Date = journal.Date,
                    Periodicity = journal.Periodicity,
                    Amount = journal.Amount,
                    Price = journal.Price,
                    Subjects = journal.Subjects,
                    PhotoId = journal?.PhotoId
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
        public IHttpActionResult EditJournal(JournalModel journalModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Journal journal = DataContext.Journals.FirstOrDefault(b => b.Id == journalModel.Id);

            if (journal == null) { return NotFound(); }

            try
            {
                journal.Title = journalModel.Title;
                journal.Date = journalModel.Date;
                journal.Periodicity = journalModel.Periodicity;
                journal.Subjects = journalModel.Subjects;
                journal.Price = journalModel.Price;
                journal.Amount = journalModel.Amount;
                journal.PhotoId = journalModel?.PhotoId;

                DataContext.SubmitChanges();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteJournal(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (CurrentUser == null || !CurrentUser.IsAdmin) { return Unauthorized(); }

            Journal forDelete = DataContext.Journals.FirstOrDefault(b => b.Id == id);

            if (forDelete == null) { return NotFound(); }
            try
            {
                DataContext.Journals.DeleteOnSubmit(forDelete);
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
