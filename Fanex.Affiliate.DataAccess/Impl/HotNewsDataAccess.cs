// ----------------------------------------------------------------------
// <copyright file="HotNewsDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Brian.Dinh" date="2013/07/15">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.DataAccess.Impl
{
    using System;
    using System.Data;
    using System.Linq;
    using Criterias;
    using Data;
    using DTO;
    using Interface;

    /// <summary>
    /// TODO: Update summary.
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
                var dataset = objectDb.ExecuteDataSet(criteria.GetSpParams());

                if (dataset.Tables.Count > 0)
                {
                    var hotnews = new HotNews();

                    foreach (var column in dataset.Tables[0].Columns.Cast<DataColumn>().Where(column => column.ColumnName.Contains("-")))
                    {
                        hotnews.Message.Add(
                            column.ColumnName,
                            dataset.Tables[0].Rows.Count > 0 ? dataset.Tables[0].Rows[0][column].ToString() : string.Empty);
                    }

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        hotnews.Important = Convert.ToBoolean(dataset.Tables[0].Rows[0]["important"]);
                    }

                    return hotnews;
                }
            }

            return new HotNews();
        }
    }
}
