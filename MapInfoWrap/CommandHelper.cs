using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public static class CommandHelper
    {
        public static string Update(string tableName, string columnName, string value, int rowID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update ").Append(tableName)
                .Append(" Set ").Append(columnName).Append(" = ").Append(value)
                .Append(" Where RowID = ").Append(rowID);
            return builder.ToString();
        }
    }
}
