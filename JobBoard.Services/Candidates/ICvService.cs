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
    public interface ICvService
    {
        string GetUserIdByCvId(string id);
        string GetLoggedUser();
        bool CvBelongsToLoggedUser(string cvId);

        string Add(CvCreateModel model, string userId);


        PersonalInfoEditModel GetPersonalInfoById(string id);

        void UpdatePersonalInfo(string id, PersonalInfoEditModel form);    
    }
}
