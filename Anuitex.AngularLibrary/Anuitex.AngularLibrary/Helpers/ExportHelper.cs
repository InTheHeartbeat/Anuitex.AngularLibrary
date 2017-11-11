using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Models.IO.Export.Books;
using Anuitex.AngularLibrary.Models.IO.Export.Journals;
using Anuitex.AngularLibrary.Models.IO.Export.Newspapers;

namespace Anuitex.AngularLibrary.Helpers
{
    public static class ExportHelper
    {
        public static byte[] ExportBooks(ExportBooksModel model, LibraryDataContext dataContext)
        {
            if (model.IsXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<BookModel>));
                MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, model.Books.Where(bk => bk.Selected)
                    .Select(bk => new BookModel(dataContext.Books.First(book => book.Id == bk.Id)))
                    .ToList());
                return stream.ToArray();
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine("Books");

                foreach (BookModel book in model.Books.Where(bk => bk.Selected)
                    .Select(sbk => new BookModel(dataContext.Books.First(book => book.Id == sbk.Id)))
                    .ToList())
                {
                    writer.WriteLine(book.Id);
                    writer.WriteLine(book.Title);
                    writer.WriteLine(book.Year);
                    writer.WriteLine(book.Pages);
                    writer.WriteLine(book.Author);
                    writer.WriteLine(book.Genre);
                    writer.WriteLine(book.Amount);
                    writer.WriteLine(book.Price);
                    writer.WriteLine(book.PhotoId);
                    writer.WriteLine(book.PhotoPath);
                }
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public static byte[] ExportJournals(ExportJournalsModel model, LibraryDataContext dataContext)
        {
            if (model.IsXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<JournalModel>));
                MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, model.Journals.Where(jour => jour.Selected)
                    .Select(jj => new JournalModel(dataContext.Journals.First(journal => journal.Id == jj.Id)))
                    .ToList());
                return stream.ToArray();
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.Default);

                writer.WriteLine("Journals");

                foreach (JournalModel journal in model.Journals.Where(jm => jm.Selected)
                    .Select(jour => new JournalModel(dataContext.Journals.First(journal => journal.Id == jour.Id)))
                    .ToList())
                {
                    writer.WriteLine(journal.Id);
                    writer.WriteLine(journal.Title);
                    writer.WriteLine(journal.Subjects);
                    writer.WriteLine(journal.Periodicity);
                    writer.WriteLine(journal.Date);
                    writer.WriteLine(journal.Amount);
                    writer.WriteLine(journal.Price);
                    writer.WriteLine(journal.PhotoId);
                    writer.WriteLine(journal.PhotoPath);
                }
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        public static byte[] ExportNewspapers(ExportNewspapersModel model, LibraryDataContext dataContext)
        {
            if (model.IsXml)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<NewspaperModel>));
                MemoryStream stream = new MemoryStream();
                serializer.Serialize(stream, model.Newspapers.Where(np => np.Selected)
                    .Select(
                        snp => new NewspaperModel(dataContext.Newspapers.First(newspaper => newspaper.Id == snp.Id)))
                    .ToList());
                return stream.ToArray();
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream, Encoding.Default);

                writer.WriteLine("Newspapers");

                foreach (NewspaperModel newspaper in model.Newspapers.Where(npm => npm.Selected)
                    .Select(np => new NewspaperModel(dataContext.Newspapers.First(newspaper => newspaper.Id == np.Id)))
                    .ToList())
                {
                    writer.WriteLine(newspaper.Id);
                    writer.WriteLine(newspaper.Title);
                    writer.WriteLine(newspaper.Periodicity);
                    writer.WriteLine(newspaper.Date);
                    writer.WriteLine(newspaper.Amount);
                    writer.WriteLine(newspaper.Price);
                    writer.WriteLine(newspaper.PhotoId);
                    writer.WriteLine(newspaper.PhotoPath);
                }
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }
    }
}