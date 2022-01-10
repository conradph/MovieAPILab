using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPILab.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        MovieDAL mdb = new MovieDAL();
        [HttpGet]
        public List<Movie> GetMovies()
        {
            List<Movie> movies = mdb.GetAllMovies();
            return movies;
        }
        [HttpGet("GetMovie/{id}")]
        public Movie GetMovie(int id)
        {
            Movie mov = mdb.GetMovie(id);
            return mov;
        }
        [HttpPost("CreateMovie")]
        public string CreateMovie(Movie m)
        {
            mdb.CreateMovie(m);
            return $"{m.Title} has been added to the database.";
        }
        [HttpDelete("DeleteMovie/{id}")]
        public string DeleteMovie(int id)
        {
            mdb.DeleteMovie(id);
            return $"Movie at id {id} has been deleted from the database.";
        }
        [HttpPut("UpdateMovie/{id}")]
        public string UpdateMovie(int id, Movie updatedMovie)
        {
            Movie oldMovie = mdb.GetMovie(id);
            if(updatedMovie.Title == null)
            {
                updatedMovie.Title = oldMovie.Title;
            }
            if(updatedMovie.Genre == null)
            {
                updatedMovie.Genre = oldMovie.Genre;
            }
            if(updatedMovie.Year == 0)
            {
                updatedMovie.Year = oldMovie.Year;
            }
            if(updatedMovie.Runtime == 0)
            {
                updatedMovie.Runtime = oldMovie.Runtime;
            }
            mdb.UpdateMovie(id, updatedMovie);
            return $"{updatedMovie.Title} at id {id} has been updated.";
        }
        [HttpGet("GenreSearch/{genre}")]
        public List<Movie> GetMoviesByGenre(string genre)
        {
            List<Movie> movies = mdb.GetMoviesByGenre(genre);
            return movies;
        }
        [HttpGet("GetRandomMovie")]
        public Movie GetRandomMovie()
        {
            Random random = new Random();
            List<int> ids = mdb.GetIds();
            int id = ids[random.Next(ids.Count)];
            Movie m = mdb.GetMovie(id);
            return m;
        }
        [HttpGet("GetRandomFromGenre/{genre}")]
        public Movie GetRandomMovieFromGenre(string genre)
        {
            Random random = new Random();
            List<int> ids = mdb.GetIdsByGenre(genre);
            int id = ids[random.Next(ids.Count)];
            Movie m = mdb.GetMovie(id);
            return m;
        }
    }
}
