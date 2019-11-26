using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfo;

namespace MapInfoWrap
{
    public interface IMapTable
    {

    }

    public class Table : IMapTable
    {
        DMapInfo instance;
        int tableId;
        string name;
        int lenght;

        public Rows Rows { get; set; }
        public Fields Fields { get; set; }

        #region Fabrica
        public static Table CreateTable(DMapInfo _instance, string _tabName, int _tableId)
        {
            Table table = new Table();
            table.instance = _instance;
            table.name = _tabName;
            table.tableId = _tableId;
            table.lenght = table.GetLenght();
            table.Fields = new Fields(table.GetFields());
            table.Rows = new Rows(table.instance, table.name, table.lenght, table.Fields);
            
            return table;
        }

        public static Table CreateTable(DMapInfo _instance, string _tabName)
        {
            Table table = new Table();
            table.instance = _instance;
            table.name = _tabName;
            table.tableId = table.GetTableID(_tabName);
            table.lenght = table.GetLenght();
            table.Fields = new Fields(table.GetFields());
            table.Rows = new Rows(table.instance, table.Name, table.Lenght, table.Fields);
            return table;
        }
        #endregion

        #region TableMethods
        public string Name { get => name; private set => _ = name; }

        public int Lenght { get => lenght; private set => _ = lenght; }

        public int TableID { get => tableId; private set => _ = tableId; }

        public int GetTableID()
        {
            try
            {
                return Convert.ToInt32(instance.Eval(@"TableInfo(""" + name + @""", 39 )"));
            }
            catch
            {
                return -1;
            }

        }

        public int GetTableID(string name)
        {
            try
            {
                return Convert.ToInt32(instance.Eval(@"TableInfo(""" + name + @""", 39 )"));
            }
            catch
            {
                return -1;
            }
        }
        #endregion



        public void CreateBufferDataBase(string tempFolder = @"D:\work\Mapinfo\программы\Classifier\tempBD\")
        {
            //instance.Do(@"Open Table """ + tablePath + @""" As currentTable");

            var savePath1 = tempFolder + "bufferDB.TAB";
            var savePath2 = tempFolder + "bufferDB.accdb";

            var sqlCom = @"SELECT UId, VRI_DOC, BTICodes, lo_lvl, mid_lvl, hi_lvl, VRI, Matches, Type, Kind, Maintenance, Landscape, FedSearch, Int(Area(obj, ""sq m"")) FROM " + name + " Into rrr";
            instance.Do(sqlCom);

            var saveToDBCommand = @"Commit Table rrr As """ + savePath1 + @""" Type ACCESS Database """ + savePath2 + @""" Table ""bufferDB""";
            instance.Do(saveToDBCommand);
        }

        public void save()
        {
            instance.Do(@"Register Table ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.accdb""  Type ACCESS Table ""bufferDB"" Into ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.TAB""");
            instance.Do(@"Open Table ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.TAB"" As Modify");
        }

        public void UniqueID()
        {
            //if (!columns.ContainsKey("UId")) instance.Do("Alter Table " + name + "( Add UId Integer)");
            //instance.Do("Update " + name + " Set UId = RowID");
            //instance.Do("Commit Table " + name);
        }

        private int GetLenght()
        {
            var tableLenght = instance.Eval(@"TableInfo(" + name + ", " + "8)");
            return Convert.ToInt32(tableLenght);
        }

        private List<Field> GetFields()
        {
            var _cols = new List<Field>();
            if (int.TryParse(instance.Eval("TableInfo(" + name + ", 4)"), out var amount))
            {
                var it = 1;
                while (it <= amount)
                {
                    var columnName = instance.Eval("ColumnInfo(" + name + @", COL" + it.ToString() + ", 1)");
                    var columnType = instance.Eval("ColumnInfo(" + name + @", COL" + it.ToString() + ", 3)");


                    _cols.Add(Field.CreateField(it, columnName, columnType));
                    it++;
                }
            }
            return _cols;
        }

        public dynamic GetValues(int[] numberOfColumns)
        {
            List<object> result = new List<object>();
            var temp = "";

            foreach (var val in numberOfColumns)
            {
                switch (instance.Eval("ColumnInfo(" + name + @", COL" + val.ToString() + ", 3)"))
                {
                    case "1":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToChar(temp));
                        break;
                    case "2":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToDecimal(temp));
                        break;
                    case "3":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToInt32(temp));
                        break;
                    case "4":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToInt16(temp));
                        break;
                    case "5":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToDateTime(temp));
                        break;
                    case "6":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToBoolean(temp));
                        break;
                    case "7":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToChar(temp));
                        break;
                    case "8":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToDouble(temp));
                        break;
                    case "37":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToDateTime(temp));
                        break;
                    case "38":
                        temp = instance.Eval(name + ".COL" + val.ToString());
                        result.Add(Convert.ToDateTime(temp));
                        break;
                }
            }
            return result;
        }
    }
}
