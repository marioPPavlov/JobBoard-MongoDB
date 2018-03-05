using JobBoard.Services.Candidates.Models.Cvs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Services.Candidates
{
    public interface ICvService
    {
        Task Add(CvCreateModel model, string userId);
        //Task<IEnumerable<CvListingModel>> CvList(string userId);
        //Task<CvDetailsModel> DetailsById(int id);
        //Task<CvEditModel> FindById(int id);
        //Task<bool> Edit(int id, CvEditModel form);
        //string GetUserIdByCvId(int id);

        //Task<PersonalInfoEditModel> PersonalInfoById(int id);
        //Task<bool> PersonalInfoEditSave(int id, PersonalInfoEditModel form);

        //Task<bool> WorkCreate(WorkCreateModel model, int cvId);
        //Task<WorkEditModel> WorkById(int id);
        //Task<bool> WorkEditSave(int id, WorkEditModel form);
    }
}
