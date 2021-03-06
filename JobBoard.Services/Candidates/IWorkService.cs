﻿using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs.Lists;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Candidates
{
    public interface IWorkService : IGenericListCrudService<WorkListModel>
    {
    }
}
