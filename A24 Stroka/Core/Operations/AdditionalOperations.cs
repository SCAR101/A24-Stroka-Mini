using A24_Stroka.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A24_Stroka.Core.Operations
{
    public class AdditionalOperations
    {
        public static ObservableCollection<NewsModel> ConvertDataTableNewsToObservableCollection(DataTable table)
        {
            var tempCollection = new ObservableCollection<NewsModel>();
            foreach (DataRow row in table.Rows)
            {
                var obj = new NewsModel
                {
                    ID = (int)row["ID"],
                    Text = (string)row["Text"],
                    In_Work = (bool)row["In_Work"],
                    Order_By = (int)row["Order_By"],
                    View = (bool)row["View"]
                };
                

                tempCollection.Add(obj);
            }
            return tempCollection;
        }

        public static List<string> ConvertDataTableToList(DataTable dt, string field)
        {
            List<string> list = dt.AsEnumerable()
                           .Select(r => r.Field<string>(field))
                           .ToList();
            return list;
        }
    }
}
