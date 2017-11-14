using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Anuitex.AngularLibrary.Data;
using Anuitex.AngularLibrary.Data.Models;
using Anuitex.AngularLibrary.Extensions;
using Anuitex.AngularLibrary.Models;

namespace Anuitex.AngularLibrary.Controllers.API
{
    public class SellController : BaseApiController
    {
        [System.Web.Http.Route("api/Sell/GetBasket")]
        [ResponseType(typeof(BasketModel))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Basket()
        {
            BasketModel model = new BasketModel();

            if (CurrentUser != null)
            {
                try
                {
                    AccountOrder order = CurrentUser.AccountOrders.LastOrDefault(ord => !ord.Completed);

                    if (order != null)
                    {
                        model.SumPrice = order.Sum;
                        model.OrderId = order.Id;
                        foreach (AccountOrderRecord record in order.AccountOrderRecords)
                        {
                            if (record.ProductType == typeof(Book).Name)
                            {
                                model.BookProducts.Add(
                                    new BookModel(
                                        DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)));
                            }
                            if (record.ProductType == typeof(Journal).Name)
                            {
                                model.JournalProducts.Add(
                                    new JournalModel(
                                        DataContext.Journals.FirstOrDefault(
                                            journal => journal.Id == record.ProductId)));
                            }
                            if (record.ProductType == typeof(Newspaper).Name)
                            {
                                model.NewspaperProducts.Add(
                                    new NewspaperModel(
                                        DataContext.Newspapers.FirstOrDefault(
                                            newspaper => newspaper.Id == record.ProductId)));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            if (CurrentVisitor != null)
            {
                try
                {
                    VisitorOrder order = CurrentVisitor.VisitorOrders.LastOrDefault(ord => !ord.Completed);

                    if (order != null)
                    {
                        model.SumPrice = order.Sum;
                        model.OrderId = order.Id;
                        foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
                        {
                            if (record.ProductType == typeof(Book).Name)
                            {
                                model.BookProducts.Add(
                                    new BookModel(
                                        DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)));
                            }
                            if (record.ProductType == typeof(Journal).Name)
                            {
                                model.JournalProducts.Add(
                                    new JournalModel(
                                        DataContext.Journals.FirstOrDefault(
                                            journal => journal.Id == record.ProductId)));
                            }
                            if (record.ProductType == typeof(Newspaper).Name)
                            {
                                model.NewspaperProducts.Add(
                                    new NewspaperModel(
                                        DataContext.Newspapers.FirstOrDefault(
                                            newspaper => newspaper.Id == record.ProductId)));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }                        
            return Ok(model);
        }

        [System.Web.Http.Route("api/Sell/SellProduct")]        
        [System.Web.Http.HttpPost]
        public IHttpActionResult SellProduct(SellProductModel model)
        {
            try
            {
                SellIsAccount(model.Code, model.Type, model.Count);
                SellIsVisitor(model.Code, model.Type, model.Count);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }           
        }

        [System.Web.Http.Route("api/Sell/AcceptSellOrder")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult AcceptSellOrder(AcceptSellOrderModel orderId)
        {
            if (CurrentUser != null)
            {
                try
                {
                    AccountOrder order = DataContext.AccountOrders.FirstOrDefault(ord => ord.Id == orderId.OrderId);
                    if (order != null)
                    {
                        foreach (AccountOrderRecord record in order.AccountOrderRecords)
                        {
                            if (record.ProductType == typeof(Book).Name)
                            {
                                DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                            if (record.ProductType == typeof(Journal).Name)
                            {
                                DataContext.Journals.FirstOrDefault(jor => jor.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                            if (record.ProductType == typeof(Newspaper).Name)
                            {
                                DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                        }

                        order.Completed = true;

                        DataContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            if (CurrentVisitor != null)
            {
                try
                {
                    VisitorOrder order = DataContext.VisitorOrders.FirstOrDefault(ord => ord.Id == orderId.OrderId);
                    if (order != null)
                    {
                        foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
                        {
                            if (record.ProductType == typeof(Book).Name)
                            {
                                DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                            if (record.ProductType == typeof(Journal).Name)
                            {
                                DataContext.Journals.FirstOrDefault(jor => jor.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                            if (record.ProductType == typeof(Newspaper).Name)
                            {
                                DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId).Amount -=
                                    record.Count;
                            }
                        }

                        order.Completed = true;

                        DataContext.SubmitChanges();
                    }
                }
                catch (Exception e)
                {
                    return InternalServerError(e);
                }
            }

            return Ok();
        }

        [System.Web.Http.Route("api/Sell/RemoveProductFromBasket")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult RemoveProductFromBasket(RemoveProductFromBasketModel model)
        {
            try
            {
                AccountOrder currentAccountOrder = CurrentUser?.AccountOrders.LastOrDefault(ord => !ord.Completed);
                AccountOrderRecord accountOrderRecord = currentAccountOrder?.AccountOrderRecords.FirstOrDefault(
                    rec => rec.ProductId == model.ProductId && rec.ProductType == model.ProductType);
                if (accountOrderRecord != null)
                {
                    DataContext.AccountOrderRecords.DeleteOnSubmit(accountOrderRecord);
                    DataContext.SubmitChanges();
                    CalcAccountOrderSum(currentAccountOrder, accountOrderRecord.Count);
                    return Ok();
                }

                VisitorOrder currentVisitorOrder = CurrentVisitor?.VisitorOrders.LastOrDefault(ord => !ord.Completed);
                VisitorOrderRecord visitorOrderRecord =
                    currentVisitorOrder?.VisitorOrderRecords.FirstOrDefault(
                        rec => rec.ProductId == model.ProductId && rec.ProductType == model.ProductType);
                if (visitorOrderRecord != null)
                {
                    DataContext.VisitorOrderRecords.DeleteOnSubmit(visitorOrderRecord);
                    DataContext.SubmitChanges();
                    CalcVisitorOrderSum(currentVisitorOrder, visitorOrderRecord.Count);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }

        private void SellIsAccount(int code, string type, int count)
        {
            if (CurrentUser != null)
            {
                AccountOrderRecord record = new AccountOrderRecord()
                {
                    Count = count,
                    ProductId = code,
                    ProductType = type,
                };

                AccountOrder order = CurrentUser.AccountOrders.LastOrDefault(ord => !ord.Completed);
                if (order != null)
                {
                    record.AccountOrder = order;
                    order.AccountOrderRecords.Add(record);
                }
                else
                {
                    order = new AccountOrder()
                    {
                        Account = CurrentUser,
                        AccountId = CurrentUser.Id,
                        Completed = false
                    };

                    record.AccountOrder = order;
                    order.AccountOrderRecords.Add(record);
                    DataContext.AccountOrders.InsertOnSubmit(order);

                    CurrentUser.AccountOrders.Add(order);
                }

                CalcAccountOrderSum(order, count);

                DataContext.SubmitChanges();
            }
        }
        private void CalcAccountOrderSum(AccountOrder order, int count)
        {
            if (order == null)
            {
                return;
            }
            order.Sum = 0;

            foreach (AccountOrderRecord record in order.AccountOrderRecords)
            {
                if (record.ProductType == typeof(Book).Name)
                {
                    order.Sum += (float)DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;

                }

                if (record.ProductType == typeof(Journal).Name)
                {
                    order.Sum += (float)DataContext.Journals
                                     .FirstOrDefault(journal => journal.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Newspaper).Name)
                {
                    order.Sum += (float)DataContext.Newspapers.FirstOrDefault(np => np.Id == record.ProductId)
                                     .Price * count;
                }
            }
            DataContext.SubmitChanges();
        }

        private void SellIsVisitor(int code, string type, int count)
        {
            if (CurrentVisitor != null)
            {
                VisitorOrderRecord record = new VisitorOrderRecord()
                {
                    Count = count,
                    ProductId = code,
                    ProductType = type,
                };

                VisitorOrder order = CurrentVisitor.VisitorOrders.LastOrDefault(ord => !ord.Completed);
                if (order != null)
                {
                    record.VisitorOrder = order;
                    order.VisitorOrderRecords.Add(record);
                }
                else
                {
                    order = new VisitorOrder()
                    {
                        Visitor = CurrentVisitor,
                        VisitorId = CurrentVisitor.Id,
                        Completed = false
                    };

                    record.VisitorOrder = order;
                    order.VisitorOrderRecords.Add(record);

                    CurrentVisitor.VisitorOrders.Add(order);
                }

                CalcVisitorOrderSum(order, count);

                DataContext.SubmitChanges();
            }
        }
        private void CalcVisitorOrderSum(VisitorOrder order, int count)
        {
            if (order == null) { return; }
            order.Sum = 0;

            foreach (VisitorOrderRecord record in order.VisitorOrderRecords)
            {
                if (record.ProductType == typeof(Book).Name)
                {
                    order.Sum += (float)DataContext.Books.FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Journal).Name)
                {

                    order.Sum += (float)DataContext.Journals
                                     .FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }

                if (record.ProductType == typeof(Newspaper).Name)
                {
                    order.Sum += (float)DataContext.Newspapers
                                     .FirstOrDefault(book => book.Id == record.ProductId)
                                     .Price * count;
                }
            }
            DataContext.SubmitChanges();
        }
    }
}
