using JobBoard.Common.DTO;
using System.Collections.Generic;

namespace JobBoard.Services.Candidates.Models.Cvs.Lists
{
    public class WorkListModel : IListModel<WorkModel>
    {
        public List<WorkModel> Works { get; set; } = new List<WorkModel>();

        public List<WorkModel> GetList()
        {
            return this.Works;
        }
    }
}
