using AutoMapper;
using JobBoard.Common.DTO;
using JobBoard.Common.Extensions;
using JobBoard.Data;
using JobBoard.Data.Models.Cvs;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JobBoard.Services.Candidates
{
    public abstract class GenericListCrudService<D, M, L> : IGenericListCrudService<L>
        where D : class, IData, new()
        where M : class, IModel, new()
        where L : class, IListModel<M>, new()
    {
        protected readonly JobBoardDbContext db;

        public GenericListCrudService(JobBoardDbContext db)
        {
            this.db = db;
        }

        public L GetListModel(string id)
        {
            List<D> collection = GetCollectionById(id);
            L listModel = new L();

            this.GetDbSetName();

            listModel.GetList().AddRange(Mapper.Map<List<M>>(collection));
            return listModel;
        }

        public void Add(string id)
        {
            List<D> collection = GetCollectionById(id);

            collection.Add(new D());
            SaveChanges(id, collection);
        }

        public void Update(string id, L form)
        {
            var collection = Mapper.Map<List<D>>(form.GetList());
            SaveChanges(id, collection);
        }

        public void Delete(string id, int number)
        {
            List<D> collection = GetCollectionById(id);

            collection.RemoveAt(number);
            SaveChanges(id, collection);
        }

        private UpdateResult SaveChanges(string id,List<D> collection)
        {
            var updateResult = this.db.Cvs.UpdateOne(
               Builders<Cv>.Filter.Eq("_id", id.ToObjectId()),
               Builders<Cv>.Update.Set(GetDbSetName(), collection));

            return updateResult;
        }

        protected abstract List<D> GetCollectionById(string id);
        protected abstract string GetDbSetName();
    }
}
