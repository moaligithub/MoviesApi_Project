using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.Dtos
{
    public class CreateMovieDto : BaseMovieDto
    {
        [Required]
        public IFormFile Poster { get; set; }
    }
}
