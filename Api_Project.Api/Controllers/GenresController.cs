using Api_Project.Core.Const;
using Api_Project.Core.Dtos;
using Api_Project.Core.IUnitOfWork;
using Api_Project.Core.Models;
using Api_Project.Ef;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public GenresController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Result = await _unit.Genres.GetAllAsync(x=>x.Movies);
            return Ok(Result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]GenreDto genreDto)
        {
            Genre genre = new Genre { Name = genreDto.Name };
            
            await _unit.Genres.AddAsync(genre);
            _unit.Complete();

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpDateAsync(byte id , [FromBody]GenreDto genreDto)
        {
            Genre genre = await _unit.Genres.GetByIdAsync(id);

            if (genre == null)
                return NotFound();

            genre.Name = genreDto.Name;
            
            _unit.Genres.UpDate(genre);
            _unit.Complete();

            return Ok(genre);
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            Genre genre = await _unit.Genres.GetByIdAsync(id);

            if (genre == null)
                return NotFound();

            _unit.Genres.Delete(genre);
            _unit.Complete();

            return Ok(genre);
        }
    }
}
