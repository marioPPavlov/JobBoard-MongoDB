using JobBoard.Common.DTO;
using JobBoard.Data.Models.Cvs;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Candidates.Models.Cvs.Lists
{
    public class EducationListModel : IListModel<EducationModel>
    {
        public List<EducationModel> Educations { get; set; } = new List<EducationModel>();

        public List<EducationModel> GetList()
        {
            return this.Educations;
        }
    }
}
