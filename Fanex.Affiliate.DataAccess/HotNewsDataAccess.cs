namespace Fanex.Affiliate.DataAccess
{
    using System;
    using System.Data;
    using System.Linq;
    using Criteria;
    using Data;
    using DTO;

    /// <summary>
    /// Hot News Data Access.
    /// </summary>
    public class HotNewsDataAccess : IHotNewsDataAccess
    {
        /// <summary>
        /// Get hot news.
        /// </summary>
        /// <param name="criteria">Hot News Criteria.</param>
        /// <returns>Hot news object.</returns>
        public HotNews GetHotNews(HotNewsCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var dataSet = objectDb.ExecuteDataSet(criteria.GetSpParams());

                if (dataSet.Tables.Count <= 0)
                {
                    return new HotNews();
                }

                var hotnews = new HotNews();
                foreach (var column in dataSet.Tables[0].Columns.Cast<DataColumn>().Where(column => column.ColumnName.Contains("-")))
                {
                    hotnews.Message.Add(
                        column.ColumnName,
                        dataSet.Tables[0].Rows.Count > 0 ? dataSet.Tables[0].Rows[0][column].ToString() : string.Empty);
                }

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    hotnews.Important = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["important"]);
                }

                return hotnews;
            }
        }
    }
}
