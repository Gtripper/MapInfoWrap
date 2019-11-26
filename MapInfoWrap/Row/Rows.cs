using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapInfo;

namespace MapInfoWrap
{
    public class Rows : IEnumerable<Row>
    {
        private List<Row> rows;

        public Rows(DMapInfo Application, string Parent, int nCols, Fields fields)
        {
            rows = new List<Row>(); int i = 0;
            while (i < nCols)
            {
                rows.Add(new Row(Application, Parent, i + 1, fields));
                i++;
            }
        }

        public Row this[int index]
        {
            get => rows[index];            
        }


        #region IEnumerable
        public IEnumerator<Row> GetEnumerator()
        {
            return rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return rows.GetEnumerator();
        }
        #endregion

    }
}
