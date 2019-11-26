using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public enum Type {  CHAR = 1,
                        DECIMAL = 2,
                        INTEGER = 3,
                        SMALLINT = 4,
                        DATE = 5,
                        LOGICAL = 6,
                        GRAPHIC = 7,
                        DOUBLE = 8,
                        TIME = 37,
                        DATETIME = 38 }
    public sealed class Field
    {
        private int id;
        private string name;
        private Type type;
        private Int16 width;
        private Int16 decPlaces;
        private bool isIndexed;
        private bool isEditable;


        public static Field CreateField(int id, string fieldName, string fieldType)
        {
            Field field = new Field();
            field.id = id;
            field.name = fieldName;
            field.type = GetType(fieldType);
            return field;
        }

        public int Id { get => id; private set => _ = id; }
        public string Name { get => name; private set => _ = name; }
        public Type Type { get => type; private set => _ = type; }
        public dynamic Value { get
            {
                switch (Type)
                {
                    case Type.CHAR:
                        break;
                    case Type.DECIMAL:
                        break;
                    case Type.INTEGER:
                        break;
                }
                return 0;
            } }
        private static Type GetType(string strType)
        {   
            return (Type) Int32.Parse(strType);
        }
    }
}
