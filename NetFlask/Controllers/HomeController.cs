using NetFlask.DAL;
using NetFlask.Entities;
using NetFlask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetFlask.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Current = "Index";            

            return View(new HomeViewModel());
        }

        [HttpPost]
        public ActionResult SimpleSearch(string txtSearch)
        {
            MovieRepository rm = new MovieRepository();
            //Mapping
            List<Movie> ModelDb = rm.getFromTitle(txtSearch);
            //tranférer les données du modeldb vers MovieReview
            List<MovieReview> lm = new List<MovieReview>();
            foreach (Movie item in ModelDb)
            {
                lm.Add(new MovieReview() 
                {
                  Title = item.Title,
                  Picture = item.Picture  ,
                  Duration = item.Duration,
                  ShortDescription= item.Summary,
                  DateReview = item.ReleaseDate.Value,
                  Trailer= item.Trailer
                });
            }
            ViewBag.Word = txtSearch;
            ViewBag.HiddenForm = true;
            return View("resultat", lm);
        }


        public ViewResult Videos(int id = 1)
        {
            ViewBag.Current = "Videos";
            ViewBag.CurrentPage = id;
            MovieRepository rm = new MovieRepository();
            //Mapping
            List<Movie> ModelDb = rm.Paginate(id, 9);
            ViewBag.MaxPages = Math.Round((double)rm.getNbMovies() / 9);
            //tranférer les données du modeldb vers MovieReview
            List<MovieInfo> lm = new List<MovieInfo>();
            foreach (Movie item in ModelDb)
            {
                lm.Add(new MovieInfo()
                {
                    Title = item.Title,
                    Picture = item.Picture,
                    Video = item.Trailer.Replace("watch?v=", "embed/")
                });
            }
            return View(lm);
        }

        public ViewResult Reviews()
        {
            ViewBag.Current = "Reviews";
            List<MovieReview> movrev = new List<MovieReview>();
            movrev = FakeDb.LoadData();
            //string CastString = convertToString(movrev.Cast);
            //ViewBag.CastString = CastString;
            //BERK BERK BERK BERK!!!!!!!!!!!!!! 
            return View(movrev);
        }

        private string convertToString(List<string> casts)
        {if (casts.Count() == 0) return "";
            string retour = "";
            foreach (string item in casts)
            {
                retour += item + ",";

            }
            return retour.Substring(0, retour.Length - 1);
        }
    }
}