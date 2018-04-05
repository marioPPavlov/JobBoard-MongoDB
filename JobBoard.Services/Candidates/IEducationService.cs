using JobBoard.Services.Candidates.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Candidates
{
    public interface IEducationService : IGenericListCrudService<EducationListModel>
    {
    }
}
