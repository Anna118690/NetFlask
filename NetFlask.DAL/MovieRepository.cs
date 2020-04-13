
using NetFlask.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Database;
namespace NetFlask.DAL
{
    public class MovieRepository
    {
        static Random rnd = new Random();
        private Connection _oconn;
        private string _cnstr = @"Data Source=LAPTOP-A106PIQF\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public MovieRepository()
        {
            _oconn = new Connection(_cnstr);
        }

        public List<string> getFirstSix()
        {
            IEnumerable<string> images = new List<string>();
            Command cmd = new Command("SELECT TOP 6 Picture FROM MOVIE");
            images =_oconn.ExecuteReader<string>(cmd, d=>d["Picture"].ToString());

            return images.ToList();
        }

        public List<Movie> getAll()
        {
            IEnumerable<Movie> movies = new List<Movie>();
            Command cmd = new Command($"SELECT top 10 * FROM MOVIE");
            movies = _oconn.ExecuteReader<Movie>(cmd,
                d =>
                new Movie()
                {
                    IdMovie = (int)d["IdMovie"],
                    Picture = d["Picture"].ToString(),
                    Title = d["Title"].ToString(),
                    Duration = (int)d["Duration"],
                    Summary = d["Summary"].ToString(),
                    ReleaseDate = (DateTime?)d["ReleaseDate"],
                    Trailer = (string)d["Trailer"]

                }
                );

            return movies.ToList();
        }

        public List<Movie> getFromTitle(string search)
        {
            IEnumerable<Movie> movies = new List<Movie>();
            Command cmd = new Command($"SELECT * FROM MOVIE where title like '%{search}%'");
            movies = _oconn.ExecuteReader<Movie>(cmd,
                d =>
                new Movie()
                {
                     IdMovie = (int)d["IdMovie"],
                     Picture = d["Picture"].ToString(),
                     Title = d["Title"].ToString(),
                     Duration= (int)d["Duration"],
                     Summary = d["Summary"].ToString() ,
                     ReleaseDate=(DateTime?)d["ReleaseDate"],
                     Trailer = (string)d["Trailer"]
                     
                }            
                );

            return movies.ToList();
        }

        public List<string> getRandom()
        {
            int skip = rnd.Next(0, 990);

            IEnumerable<string> images = new List<string>();
            Command cmd = new Command($"SELECT   Picture FROM MOVIE order by IdMovie  OFFSET ({skip}) ROWS FETCH NEXT (6) ROWS ONLY");
            images = _oconn.ExecuteReader<string>(cmd, d => d["Picture"].ToString());

            return images.ToList();
        }

        public int getNbMovies()
        {
             
            Command cmd = new Command("SELECT count(*)   FROM MOVIE");
            return (int)_oconn.ExecuteScalar(cmd); 
        }

        public List<Movie> Paginate(int page, int nb=9)
        {
            int toSkip = (page - 1) * nb;
            

            IEnumerable<Movie> movies = new List<Movie>();
            Command cmd = new Command($"SELECT   * FROM MOVIE order by IdMovie  OFFSET ({toSkip}) ROWS FETCH NEXT ({nb}) ROWS ONLY");
            movies = _oconn.ExecuteReader<Movie>(cmd,
               d =>
               new Movie()
               {
                   IdMovie = (int)d["IdMovie"],
                   Picture = d["Picture"].ToString(),
                   Title = d["Title"].ToString(),
                   Duration = (int)d["Duration"],
                   Summary = d["Summary"].ToString(),
                   ReleaseDate = (DateTime?)d["ReleaseDate"],
                   Trailer = (string)d["Trailer"]

               }
               );

           

            return movies.ToList();
        }
    }
}
