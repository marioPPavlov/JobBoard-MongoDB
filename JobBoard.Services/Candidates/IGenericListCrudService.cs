using JobBoard.Common.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobBoard.Services.Candidates
{
    public interface IGenericListCrudService<L>
    {
        L GetListModel(string id);

        void Add(string id);

        void Delete(string id, int number);

        void Update(string id, L form);
    }
}
