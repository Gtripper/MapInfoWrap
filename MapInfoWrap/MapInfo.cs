using MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    class MapInfo
    {
        public DMapInfo instance;
        public IMapInfoApp MapinfoApp;
        private DMIMapGen mapGen;
        public List<DMITable> Tables { get; private set; }

        public MapInfo(IMapInfoApp _MapinfoApp)
        {
            MapinfoApp = _MapinfoApp;
            instance = MapinfoApp.CreateInstance();
            mapGen = instance.MIMapGen;
            GetTableList();
        }

        public void GetTableList()
        {
            Tables = new List<DMITable>();
            if (int.TryParse(instance.Eval("NumTables()"), out var amount))
            {
                var it = 1;
                while (it <= amount)
                {
                    var tableName = instance.Eval(string.Format("TableInfo({0}, 1)", it));
                    if (!String.IsNullOrEmpty(tableName))
                        Tables.Add(mapGen.GetTable(tableName));
                    it++;
                }
            }
        }
    }
}
