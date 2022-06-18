using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_Project.Core.Dtos
{
    public class UpDateMovieDto : BaseMovieDto
    {
        public IFormFile Poster { get; set; }
    }
}
