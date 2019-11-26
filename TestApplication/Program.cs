using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfoWrap;
using MapInfo;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            map.TablesShow();

            int index = Convert.ToInt32(Console.ReadLine());

            Table table = map.tables[index];

            foreach (var row in table.Rows)
            {
                var temp = row.RowID * 10000 / Math.PI;               //row.RowID / 100;
                row[1] = temp;


                var q = row[8]; 
                if (q != null)
                    Console.WriteLine("{0} - {1}",row[8], q.GetType());
            }


            Console.Read();
        }
    }
}
