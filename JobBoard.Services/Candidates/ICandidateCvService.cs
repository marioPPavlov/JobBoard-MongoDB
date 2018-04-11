using JobBoard.Data.Models.Cvs;
using JobBoard.Services.Candidates.Models.Cvs;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Services.Candidates
{
    public interface ICandidateCvService
    {
        string GetUserIdByCvId(string id);
        ObjectId GetLoggedUser();
        bool CvBelongsToLoggedUser(string cvId);

        string Add(CvCreateModel model);
        CvPreviewModel GetCvDetails(string id);
        IEnumerable<CvOverviewModel> GetAllCvsOfLoggedUser();

        PersonalInfoEditModel GetPersonalInfoById(string id);

        void UpdatePersonalInfo(string id, PersonalInfoEditModel form);    
    }
}
