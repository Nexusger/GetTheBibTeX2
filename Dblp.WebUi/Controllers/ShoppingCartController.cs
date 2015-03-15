using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dblp.Domain;
using Dblp.Domain.Interfaces;
using Dblp.Domain.Interfaces.Entities;
using Dblp.WebUi.Models;

namespace Dblp.WebUi.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IDblpRepository _repo;
        private IBibTeXContentProvider _bibTeXContentProvider;

        public ShoppingCartController(IDblpRepository repo, IBibTeXContentProvider bibTeXContentProvider)
        {
            _repo = repo;
            _bibTeXContentProvider = bibTeXContentProvider;
        }

        public void AddKey(string dblpKey)
        {
            SearchResult searchResult;
            if (IsConferenceKey(dblpKey))
            {
                if (_repo.ConferenceExists(dblpKey))
                {
                    searchResult = _repo.GetConferenceEventsByKeyAsSearchResult(dblpKey);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (!_repo.PublicationExists(dblpKey))
                    return;
                searchResult = _repo.GetPublicationByKeyAsSearchResult(dblpKey);
            }
            if (searchResult != null)
                GetCart().AddItem(searchResult);
        }

        public void DeleteKey(string dblpKey)
        {
            GetCart().RemoveItem(new SearchResult(dblpKey, "", 0, SearchResultSourceType.Conference, ""));
        }

        private static bool IsConferenceKey(string dblpKey)
        {
            return dblpKey.StartsWith("db");
        }

        private ShoppingCart GetCart()
        {
            var cart = (ShoppingCart)Session["ShoppingCart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["ShoppingCart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        {
            var cartIndexViewModel = new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            };
            return View(cartIndexViewModel);
        }

        public FileResult Download()
        {
            var eventSearchResults =
                GetCart().SearchResults.Where(t => t.SearchResultSourceType == SearchResultSourceType.Event);

            var extendedSearchResults = new List<SearchResult>();
            foreach (var eventSearchResult in eventSearchResults)
            {
                extendedSearchResults.AddRange(_repo.GetPublicationsForEventsAsSearchResults(eventSearchResult.Key));

            }
            extendedSearchResults.AddRange(GetCart().SearchResults);
            byte[] fileBytes = _bibTeXContentProvider.GetBibTexFileBytes(extendedSearchResults, BibTeXContentOptions.None);
            const string fileName = "myfile.bib";
            return File(fileBytes, "text/x-bibtex", fileName);
        }

    }
}