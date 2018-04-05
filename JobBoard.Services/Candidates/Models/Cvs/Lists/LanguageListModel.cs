using JobBoard.Common.DTO;
using System.Collections.Generic;

namespace JobBoard.Services.Candidates.Models.Cvs.Lists
{
    public class LanguageListModel : IListModel<LanguageModel>
    {
        public List<LanguageModel> Languages { get; set; } = new List<LanguageModel>();

        public List<LanguageModel> GetList()
        {
            return this.Languages;
        }
    }
}
