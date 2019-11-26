using MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    class Column
    {
        int id;
        string name;
        string type;

        public static Column CreateColumn(int id, string columnName, string columnType)
        {
            Column column = new Column();
            column.name = columnName;
            column.type = GetType(columnType);
            return column;
        }

        public int Id { get => id; private set => _ = id; }
        public string Name { get => name; private set => _ = name; }
        public string Type { get => type; private set => _ = type; }
        private static string GetType(string strType)
        {
            var columnType = "";
            switch (strType)
            {
                case "1":
                    columnType = "CHAR";
                    break;
                case "2":
                    columnType = "DECIMAL";
                    break;
                case "3":
                    columnType = "INTEGER";
                    break;
                case "4":
                    columnType = "SMALLINT";
                    break;
                case "5":
                    columnType = "DATE";
                    break;
                case "6":
                    columnType = "LOGICAL";
                    break;
                case "7":
                    columnType = "GRAPHIC";
                    break;
                case "8":
                    columnType = "DOUBLE";
                    break;
                case "37":
                    columnType = "TIME";
                    break;
                case "38":
                    columnType = "DATETIME";
                    break;
            }
            return columnType;
        }
    }
}
