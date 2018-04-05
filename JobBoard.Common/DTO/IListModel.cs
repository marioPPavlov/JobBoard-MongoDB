using JobBoard.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobBoard.Common.DTO
{
    public interface IListModel<M>
        where M : class, IModel, new()
    {
        List<M> GetList();
    }
}
