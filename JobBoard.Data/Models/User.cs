using JobBoard.Data.Models.Cvs;
using JobBoard.Data.Models.MongoDB.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static JobBoard.Data.Models.Constants.Constraints;

namespace JobBoard.Data.Models
{
    public class User : MongoUser
    {
        [Required]
        [StringLength(NameMax, MinimumLength = NameMin)]
        public string Name { get; set; }

        public List<Cv> Cvs { get; set; } = new List<Cv>();
      //  public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
