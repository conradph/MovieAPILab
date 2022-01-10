using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MovieAPILab
{
    public class MovieDAL
    {
        public List<Movie> GetAllMovies()
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = "select * from movies";
                connect.Open();
                List<Movie> cats = connect.Query<Movie>(sql).ToList();
                connect.Close();
                return cats;
            }
        }
        public Movie GetMovie(int id)
        {
            try
            {
                using (var connect = new MySqlConnection(Secret.connection))
                {
                    var sql = $"select * from movies where id = {id}";
                    connect.Open();
                    Movie movie = connect.Query<Movie>(sql).ToList().First();
                    connect.Close();
                    return movie;
                }
            }
            catch
            {
                Movie error = new Movie();
                error.Title = "No movie found at " + id;
                return error;
            }
        }

        public void DeleteMovie(int id)
        {
                using (var connect = new MySqlConnection(Secret.connection))
                {
                    var sql = $"delete from movies where id = {id}";
                    connect.Open();
                    connect.Query<Movie>(sql);
                    connect.Close();
                }
        }

        public void CreateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"insert into movies values(0, "+
                    $"{m.Title}, {m.Genre}, {m.Year}, {m.Runtime}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        public void UpdateMovie(int id, Movie updatedMovie)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"update movies " +
                    $"set title = '{updatedMovie.Title}', " +
                    $"genre = '{updatedMovie.Genre}' " +
                    $"year = {updatedMovie.Year} " +
                    $"runtime = {updatedMovie.Runtime}" +
                    $" where id = {id};";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        public List<Movie> GetMoviesByGenre(string genre)
        {
            try
            {
                using (var connect = new MySqlConnection(Secret.connection))
                {
                    var sql = $"select * from movies where genre = {genre}";
                    connect.Open();
                    List<Movie> movies = connect.Query<Movie>(sql).ToList();
                    connect.Close();
                    return movies;
                }
            }
            catch
            {
                Movie error = new Movie();
                error.Title = $"No movie found at in {genre}";
                List<Movie> movies = new List<Movie>();
                movies.Add(error);
                return movies;
            }
        }
        public List<int> GetIds()
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"select id from movies;";
                connect.Open();
                List<int> ids = connect.Query<int>(sql).ToList();
                connect.Close();
                return ids;
            }
        }
        public List<int> GetIdsByGenre(string genre)
        {
            using (var connect = new MySqlConnection(Secret.connection))
            {
                var sql = $"select id from movies " +
                    $"where genre = '{genre}';";
                connect.Open();
                List<int> ids = connect.Query<int>(sql).ToList();
                connect.Close();
                return ids;
            }
        }
    }
}
