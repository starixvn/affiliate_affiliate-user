namespace Fanex.Affiliate.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using Fanex.Data.SqlServer;

    /// <summary>
    /// Query service.
    /// </summary>
    public class QueryService : ServiceBase
    {
        private readonly IDynamicRepository _repository;

        public QueryService(IDynamicRepository repository = null)
        {
            _repository = repository ?? new DynamicRepository();
        }

        public virtual IEnumerable<T> GetData<T>(ICriteria criteria)
        {
            return _repository.Fetch<T>(criteria);
        }

        public IEnumerable<T> GetData<T>(ICriteria criteria, ref int totalPages)
        {
            using (var objectDb = new SqlObjectDb(criteria.GetSettingKey()))
            {
                var list = objectDb.Query<T>(criteria);
                totalPages = Convert.ToInt32(objectDb.GetOutputParams()["totalpages"]);

                return list;
            }
        }
    }
}