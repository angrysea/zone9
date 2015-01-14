using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SFAdmin.Models;
using SFAdmin.Aspects;
using StorefrontModel;

namespace SFAdmin.Controllers
{
    [HandleError]
    public class CompanyController : StorefrontController
    {
        public ActionResult Company()
        {
            CompanyViewData viewData = new CompanyViewData();
            AddMasterData(viewData);
            viewData.update = true;
            viewData.themes = context.SiteTheme.Select();
            viewData.shippingMethods = context.ShippingMethod.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CompanyAction(
                                    string name,
                                    string description,
                                    string address1,
                                    string address2,
                                    string address3,
                                    string city,
                                    string state,
                                    string zipcode,
                                    string countrycode,
                                    string primarytelephone,
		                            string customerService,
                                    string fax,
                                    string eMail1,
                                    string eMail2,
                                    string eMail3,
                                    string baseURL,
                                    string baseSecureURL,
                                    string url,
                                    string passWord,
                                    string defaultshipping,
                                    string salesordercoupon,
                                    string keyword,
                                    string keyword1,
                                    string keyword2,
                                    string keyword3,
                                    string keyword4,
                                    string keyword5,
                                    string theme,
                                    string instockonly
                            )
        {
            Company company = context.Company.Where(name);
            company.Name = name;
            company.Description = description;
            company.Address1 = address1;
            company.Address2 = address2;
            company.Address3 = address3;
            company.City = city;
            company.State = state;
            company.Zip = zipcode;
            company.Country = countrycode;
            company.Phone = primarytelephone;
            company.CustomerService = customerService;
            company.Fax = fax;
            company.EMail1 = eMail1;
            company.EMail2 = eMail2;
            company.EMail3 = eMail3;
            company.BaseURL = baseURL;
            company.BaseSecureURL = baseSecureURL;
            company.CompanyURL = url;
            company.PassWord = passWord;
            company.DefaultShipping = defaultshipping;
            company.SalesOrderCoupon = salesordercoupon;
            company.Keyword2 = keyword;
            company.Keyword1 = keyword1;
            company.Keyword2 = keyword2;
            company.Keyword3 = keyword3;
            company.Keyword4 = keyword4;
            company.Keyword5 = keyword5;

            if(theme!=null && theme.Length>0)
                company.Theme = theme;
            
            if (instockonly != null && instockonly.Length > 0 && instockonly.Equals("1"))
                company.InStockOnly = 1;
            else
                company.InStockOnly = 0;

            context.Company.Update();
            Session["company"] = company;
            return RedirectToAction("Index", "Home");
        }

        [LogMethodCall]
        public ActionResult Themes()
        {
            CompanyViewData viewData = new CompanyViewData();
            AddMasterData(viewData);
            viewData.themes = context.SiteTheme.Select();
            return View(viewData);
        }


        [LogMethodCall]
        public ActionResult ThemeMaint(string id)
        {
            ThemeViewData viewData = new ThemeViewData();
            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.theme = null;
                viewData.update = false;
            }
            else
            {
                viewData.theme = context.SiteTheme.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ThemeAction(
                                    string mode,
                                    string themename,
                                    string css1,
                                    string css2,
                                    string css3,
                                    string css4,
                                    string css5,
                                    string image1,
                                    string image2,
                                    string image3,
                                    string image4,
                                    string image5,
                                    string heading1,
                                    string heading2,
                                    string heading3,
                                    string heading4,
                                    string heading5,
                                    string titleinfo,
                                    string metacontenttype,
                                    string metakeywords,
                                    string metadescription,
                                    string mostpopularcount,
                                    string searchresultcol,
                                    string searchresultrow,
                                    string featuredproductcount )
        {
            SiteTheme theme = null;
            if (mode.ToUpper().Equals("I"))
            {
                theme = new SiteTheme();
            }
            else
            {
                theme = context.SiteTheme.Where(themename);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.SiteTheme.Delete();
            }
            else
            {
                theme.Name = themename;
                theme.CSS1 = css1;
                theme.CSS2 = css2;
                theme.CSS3 = css3;
                theme.CSS4 = css4;
                theme.CSS5 = css5;
                theme.Image1 = image1;
                theme.Image2 = image2;
                theme.Image3 = image3;
                theme.Image4 = image4;
                theme.Image5 = image5;
                theme.Heading1 = heading1;
                theme.Heading2 = heading2;
                theme.Heading3 = heading3;
                theme.Heading4 = heading4;
                theme.Heading5 = heading5;
                theme.Titleinfo = titleinfo;
                theme.Metacontenttype = metacontenttype;
                theme.Metakeywords = metakeywords;
                theme.Metadescription = metadescription;
                if (mostpopularcount != null && mostpopularcount.Length > 0)
                    theme.Mostpopularcount = Int32.Parse(mostpopularcount);
                else
                    theme.Mostpopularcount = 0;
                if (searchresultcol != null && searchresultcol.Length > 0)
                    theme.Searchresultcol = Int32.Parse(searchresultcol);
                else
                    theme.Searchresultcol = 0;
                if (searchresultrow != null && searchresultrow.Length > 0)
                    theme.Searchresultrow = Int32.Parse(searchresultrow);
                else
                    theme.Searchresultrow = 0;
                if (featuredproductcount != null && featuredproductcount.Length > 0)
                    theme.Featuredproductcount = Int32.Parse(featuredproductcount);
                else
                    theme.Featuredproductcount = 0;

                if (mode.ToUpper().Equals("I"))
                {
                    context.SiteTheme.Insert(theme);
                }
                else
                {
                    context.SiteTheme.Update();
                }
            }
            Session["theme"] = theme;
            return RedirectToAction("Themes", "Company");
        }

        [LogMethodCall]
        public ActionResult Catalogs()
        {
            CatalogsViewData viewData = new CatalogsViewData();
            AddMasterData(viewData);
            viewData.catalogs = context.Catalog.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CatalogMaint(string id)
        {
            CatalogViewData viewData = new CatalogViewData();
            AddMasterData(viewData);
            viewData.themes = context.SiteTheme.Select();

            if (id == null || id.Length == 0)
            {
                viewData.catalog = null;
                viewData.update = false;
            }
            else
            {
                viewData.catalog = context.Catalog.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CatalogAction(
                                        string mode,
                                        string name,
                                        string description,
                                        string address1,
                                        string address2,
                                        string address3,
                                        string city,
                                        string state,
                                        string zip,
                                        string country,
                                        string phone,
                                        string customerService,
                                        string support,
                                        string fax,
                                        string email1,
                                        string email2,
                                        string email3,
                                        string baseurl,
                                        string basesecureurl,
                                        string theme,
                                        string url
                                        )
        {
            Catalog catalog = null;
            if (mode.ToUpper().Equals("I"))
            {
                catalog = new Catalog();
            }
            else
            {
                catalog = context.Catalog.Where(name);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Catalog.Delete();
            }
            else
            {
                catalog.Name = name;
                catalog.Description = description;
                catalog.Address1 = address1;
                catalog.Address2 = address2;
                catalog.Address3 = address3;
                catalog.City = city;
                catalog.State = state;
                catalog.Zip = zip;
                catalog.Country = country;
                catalog.Phone = phone;
                catalog.CustomerService = customerService;
                catalog.Support = support;
                catalog.Fax = fax;
                catalog.Email1 = email1;
                catalog.Email2 = email2;
                catalog.Email3 = email3;
                catalog.Baseurl = baseurl;
                catalog.Basesecureurl = basesecureurl;
                catalog.Theme = theme;
                catalog.Url = url;
                if (mode.ToUpper().Equals("I"))
                {
                    context.Catalog.Insert(catalog);
                }
                else
                {
                    context.Catalog.Update();
                }
            }
            return RedirectToAction("Catalogs", "Company");
        }

        [LogMethodCall]
        public ActionResult Carriers()
        {
            CarriersViewData viewData = new CarriersViewData();
            AddMasterData(viewData);
            viewData.carriers = context.Carrier.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CarrierMaint(string id)
        {
            CarrierViewData viewData = new CarrierViewData();
            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.carrier = null;
                viewData.update = false;
            }
            else
            {
                viewData.carrier = context.Carrier.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CarrierAction(
                                        string mode,
                                        string code,
                                        string name,
                                        string description,
                                        string license,
                                        string userid,
                                        string password,
                                        string version,
                                        string pickuptype,
                                        string url )
        {
            Carrier carrier = null;
            if (mode.ToUpper().Equals("I"))
            {
                carrier = new Carrier();
            }
            else
            {
                carrier = context.Carrier.Where(code);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Carrier.Delete(carrier);
            }
            else
            {
                carrier.Code = code;
                carrier.License = license;
                carrier.Name = name;
                carrier.Description = description;
                carrier.UserId = userid;
                carrier.PassWord = password;
                carrier.Version = version;
                carrier.PickupType = pickuptype;
                carrier.URL = url;

                if (mode.ToUpper().Equals("I"))
                {
                    context.Carrier.Insert(carrier);
                }
                else
                {
                    context.Carrier.Update();
                }
            }
            return RedirectToAction("Carriers", "Company");
        }

        [LogMethodCall]
        public ActionResult ShippingMethods()
        {
            ShippingMethodsViewData viewData = new ShippingMethodsViewData();
            AddMasterData(viewData);
            viewData.shippingMethods = context.ShippingMethod.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShippingMethodMaint(string id)
        {
            ShippingMethodViewData viewData = new ShippingMethodViewData();
            AddMasterData(viewData);
            viewData.carriers = context.Carrier.Select();

            if (id == null || id.Length == 0)
            {
                viewData.shippingMethod = null;
                viewData.update = false;
            }
            else
            {
                viewData.shippingMethod = context.ShippingMethod.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShippingMethodAction(
                                        string mode,
                                        string code,
                                        string carrier,
                                        string country,
                                        string fixedPrice,
                                        string freeShippingAmount,
                                        string description,
                                        string notes
                    )
        {
            ShippingMethod shippingMethod = null;
            if (mode.ToUpper().Equals("I"))
            {
                shippingMethod = new ShippingMethod();
            }
            else
            {
                shippingMethod = context.ShippingMethod.Where(code);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.ShippingMethod.Delete(shippingMethod);
            }
            else
            {
                shippingMethod.Code = code;
                if (carrier != null && carrier.Length > 0)
                    shippingMethod.Carrier = carrier;
                shippingMethod.Country = country;
                if (fixedPrice != null && fixedPrice.Length > 0)
                    shippingMethod.FixedPrice = float.Parse(fixedPrice);
                if (freeShippingAmount != null && freeShippingAmount.Length > 0)
                    shippingMethod.FreeShippingAmount = float.Parse(freeShippingAmount);
                shippingMethod.Description = description;
                shippingMethod.Notes = notes;
                if (mode.ToUpper().Equals("I"))
                {
                    context.ShippingMethod.Insert(shippingMethod);
                }
                else
                {
                    context.ShippingMethod.Update();
                }
            }
            return RedirectToAction("ShippingMethods", "Company");
        }

        [LogMethodCall]
        public ActionResult Availabilities()
        {
            AvailabilitiesViewData viewData = new AvailabilitiesViewData();
            AddMasterData(viewData);
            viewData.availabilities = context.Availability.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult AvailabilityMaint(string id)
        {
            AvailabilityViewData viewData = new AvailabilityViewData();
            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.availability = null;
                viewData.update = false;
            }
            else
            {
                viewData.availability = context.Availability.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult AvailabilityAction(
                                        string mode,
                                        string code,
                                        string description,
                                        string expectedwait,
                                        string priority)
        {
            Availability availability = null;
            if (mode.ToUpper().Equals("I"))
            {
                availability = new Availability();
            }
            else
            {
                availability = context.Availability.Where(code);

            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Availability.Delete(availability);
            }
            else
            {
                availability.Code = code;
                availability.Description = description;
                availability.ExpectedWait = int.Parse(expectedwait);
                availability.Priority = priority;
                if (mode.ToUpper().Equals("I"))
                {
                    context.Availability.Insert(availability);
                }
                else
                {
                    context.Availability.Update();
                }
            }

            return RedirectToAction("Availabilities", "Company");
        }

        [LogMethodCall]
        public ActionResult EMailLists()
        {
            EMailListsViewData viewData = new EMailListsViewData();
            AddMasterData(viewData);
            viewData.emailLists = context.EMailList.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult EMailListMaint(string id)
        {
            EMailListViewData viewData = new EMailListViewData();
            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.emailList = null;
                viewData.update = false;
            }
            else
            {
                viewData.emailList = context.EMailList.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult EMailListAction(
                            string mode,
                            string email,
                            string optout )
        {
            EMailList emailList = null;
            if (mode.ToUpper().Equals("I"))
            {
                emailList = new EMailList();
            }
            else
            {
                emailList = context.EMailList.Where(email);

            }

            if (mode.ToUpper().Equals("D"))
            {
                context.EMailList.Delete(emailList);
            }
            else
            {
                emailList.Email = email;
                emailList.Optout = (short)(optout != null && optout.Equals("on") ? 1 : 0);
                emailList.Creationdate = DateTime.Now;
                if (mode.ToUpper().Equals("I"))
                {
                    context.EMailList.Insert(emailList);
                }
                else
                {
                    context.EMailList.Update();
                }
            }
            return RedirectToAction("EMailLists", "Company");
        }

        [LogMethodCall]
        public ActionResult Links()
        {
            LinksViewData viewData = new LinksViewData();
            AddMasterData(viewData);
            viewData.links = context.Link.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult LinkMaint(string id)
        {
            LinkViewData viewData = new LinkViewData();
            AddMasterData(viewData);

            if (id == null || id.Length == 0)
            {
                viewData.link = null;
                viewData.update = false;
            }
            else
            {
                viewData.link = context.Link.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult LinkAction(
                            string mode,
		                    string url,
		                    string header,
		                    string description,
		                    string email,
		                    string linkback )
        {
            Link link = null;
            if (mode.ToUpper().Equals("I"))
            {
                link = new Link();
            }
            else
            {
                link = context.Link.Where(url);

            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Link.Delete(link);
            }
            else
            {
                link.Url = url;
                link.Header = header;
                link.Description = description;
                link.Email = email;
                link.Linkback = (short)(linkback!=null&&linkback.Equals("on") ? 1 : 0);
                if (mode.ToUpper().Equals("I"))
                {
                    context.Link.Insert(link);
                }
                else
                {
                    context.Link.Update();
                }
            }
            return RedirectToAction("Links", "Company");
        }
    }
}
