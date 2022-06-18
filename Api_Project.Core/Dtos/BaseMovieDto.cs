using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.Dtos
{
    public class BaseMovieDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Rete { get; set; }

        [Required]
        [MaxLength(2500)]
        public string StoreLine { get; set; }

        [Required]
        public byte GenreId { get; set; }
    }
}
