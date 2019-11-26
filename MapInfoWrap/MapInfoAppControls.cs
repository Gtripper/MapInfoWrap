using MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapInfoWrap
{
    public class MapInfoAppControls
    {
        public DMapInfo instance;
        public IMapInfoApp MapinfoApp;
        public List<Table> tables;

        public static MapInfoAppControls Create()
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            return map;
        }

        public MapInfoAppControls(IMapInfoApp _MapinfoApp)
        {
            MapinfoApp = _MapinfoApp;
            instance = MapinfoApp.CreateInstance();
            tables = new List<Table>();
            GetTableList();            
        }
        

        public void GetTableList()
        {             

            if (int.TryParse(instance.Eval("NumTables()"), out var amount))
            {
                var it = 1;
                while (it <= amount)
                {
                    var tableName = instance.Eval(string.Format("TableInfo({0}, 1)", it));
                    if (!String.IsNullOrEmpty(tableName))
                        tables.Add(Table.CreateTable(instance, tableName, it));
                    it++;
                }
            }
        }

        public void TablesShow()
        {
            var it = 0;
            foreach (var table in tables)
            {
                Console.WriteLine("{0}) {1}", it, table.Name);
                it++;
            }
        }

        public Table GetTable()
        {
            Console.WriteLine("Введите номер выбранной таблицы: ");
            var key = Console.ReadLine();
            if (int.TryParse(key, out var n))
                return tables[n];
            else
                return tables[0];
        }

        public Table GetTable(int index)
        {
            RefreshTables();
            try
            {
                return tables[index];
            }
            catch(ArgumentOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException(String.Format("Table with number {0} doesn't exist", index), e);
            }
        }

        public Table GetTable(string tableName)
        {
            RefreshTables();
            try
            {
                return tables.FirstOrDefault(p => p.Name.Equals(tableName));
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException(String.Format("Table with name {0} doesn't exist", tableName), e);
            }
        }

        public void Do(string command)
        {
            instance.Do(command);
        }

        public string Eval(string command)
        {
            return instance.Eval(command);
        }

        public Table Select(Table table, string columns = "*", string condition = "", string resultTable = "Selection")
        { 
            string command = @"Select " + columns + @" From """ + table.Name + @""" " + condition + @" Into " + resultTable;
            try
            {
                instance.Do(command);
                return Table.CreateTable(instance, resultTable);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Table.CreateTable(instance, resultTable);
            }
        }

        public event Action<int> TickTockHandler;
        public void Cycle(Table table, Action<int> action)
        {
            instance.Do(@"Fetch First From " + table.Name);
            int it = 0;

            while (it < table.Lenght)
            {
                action(it);
                instance.Do(@"Fetch Next From " + table.Name);
                it++;
                if (it % 1000 == 0)
                {
                    TickTockHandler?.Invoke(it);
                }
            }

        }

        private void RefreshTables()
        {
            tables.Clear();
            GetTableList();
        }
    }
}
