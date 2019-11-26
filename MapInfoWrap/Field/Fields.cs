using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public class Fields : IEnumerable<Field>
    {
        private List<Field> fields;

        public Fields(List<Field> _fields)
        {
            fields = _fields;
        }

        public Field this[int index]
        {
            get => fields[index];
        }

        public Field this[string name]
        {
            get => fields.FirstOrDefault(p => p.Name.Equals(name));
        }

        #region IEnumerable
        public IEnumerator<Field> GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return fields.GetEnumerator();
        }
        #endregion
    }
}
