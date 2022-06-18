using Api_Project.Core.Const;
using Api_Project.Core.Dtos;
using Api_Project.Core.IUnitOfWork;
using Api_Project.Core.Logic;
using Api_Project.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Api_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private List<string> _ExtenstionsFiles = new List<string> { ".JPG", ".PNG" };
        private long _MaxSize = 1048576;
        FilesManager _filesManager = new FilesManager();

        public MoviesController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unit.Movies.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Movie movie = await _unit.Movies.FindAsync(movie => movie.Id == id, movie => movie.Genre);

            if (movie == null)
                return NotFound("Not Found Movie For This Id");

            return Ok(movie);
        }

        [HttpGet("GetByGenreIdAsync/{id}")]
        public async Task<IActionResult> GetByGenreIdAsync(byte id)
        {
            if (!await _unit.Genres.AnyAsync(genre => genre.Id == id))
                return NotFound("Not Found Genre For This Id");

            return Ok(await _unit.Movies.FindAllAsync(movie => movie.GenreId == id, movie => movie.Id, Order.ByDescending, movie => movie.Genre));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMovieDto movieDto)
        {
            if (!_filesManager.IsExtenstions(_ExtenstionsFiles, movieDto.Poster))
                return BadRequest("Only .jpg And .png Images Are Allowed!");

            if (!_filesManager.IsSizeSucceeded(_MaxSize, movieDto.Poster))
                return BadRequest("Max Allowed Size For Poster Is 1MB!");

            var IsValidGenre = await _unit.Genres.AnyAsync(genre => genre.Id == movieDto.GenreId);

            if (!IsValidGenre)
                return BadRequest("GenreId Is Bad");

            Movie movie = new Movie
            {
                Title = movieDto.Title,
                Year = movieDto.Year,
                Poster = _filesManager.UploadInDb(movieDto.Poster),
                Rete = movieDto.Rete,
                StoreLine = movieDto.StoreLine,
                GenreId = movieDto.GenreId
            };

            await _unit.Movies.AddAsync(movie);
            _unit.Complete();

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpDateAsync(int id, [FromForm]UpDateMovieDto movieDto)
        {
            Movie movie = await _unit.Movies.GetByIdAsync(id);

            if (movie == null)
                return NotFound($"No Movies Was Found With Id {id}");

            var IsValidGenre = await _unit.Genres.AnyAsync(genre => genre.Id == movieDto.GenreId);
            if (!IsValidGenre)
                return BadRequest("GenreId Is Bad");

            if(movieDto.Poster != null)
            {
                if (!_filesManager.IsExtenstions(_ExtenstionsFiles, movieDto.Poster))
                    return BadRequest("Only .jpg And .png Images Are Allowed!");

                if (!_filesManager.IsSizeSucceeded(_MaxSize, movieDto.Poster))
                    return BadRequest("Max Allowed Size For Poster Is 1MB!");

                movie.Poster = _filesManager.UploadInDb(movieDto.Poster);
            }

            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.GenreId = movieDto.GenreId;
            movie.StoreLine = movieDto.StoreLine;
            movie.Rete = movieDto.Rete;

            _unit.Movies.UpDate(movie);
            _unit.Complete();

            return Ok(await _unit.Movies.FindAsync(m => m.Id == id, m => m.Genre));
        }

        [HttpDelete("Delet/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Movie movie = await _unit.Movies.GetByIdAsync(id);

            if (movie == null)
                return NotFound($"No Movies Was Found With Id {id}");

            _unit.Movies.Delete(movie);
            _unit.Complete();

            return Ok(movie);
        }
    }
}
