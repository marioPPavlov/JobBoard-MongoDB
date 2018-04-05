using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Services.Employers.Models.Jobs
{
    public class JobListModel
    {
        public IEnumerable<JobModel> Jobs { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages
            : this.CurrentPage + 1;
    }
}
