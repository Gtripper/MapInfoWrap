using MapInfo;
using System.Text;

namespace MapInfoWrap
{
    public class Row
    {
        public DMapInfo Application { get; private set; }
        public string Parent { get; private set; }
        public int RowID { get; private set; }
        private Fields Fields { get; set; }

        public Row(DMapInfo mapInfo, string parent, int rowId, Fields fields)
        {
            Application = mapInfo;
            Parent = parent;
            RowID = rowId;
            Fields = fields;
        }

        public dynamic this[int index]
        {
            get
            {
                Application.Do(@"Fetch Rec " + RowID + " From " + Parent);
                var field = this.Fields[index];
                var stringValue = Application.Eval(Parent + ".COL" + field.Id);
                return Evaluator.GetValue(field.Type, stringValue);
            }
            set
            {
                var field = this.Fields[index];
                

                string valueString = Evaluator.SetValue(field.Type, value);
                string updCom = CommandHelper.Update(Parent, field.Name, valueString, RowID);
                Application.Do(updCom);
            }
        }

        public dynamic this[string columnName]
        {
            get
            {
                Application.Do(@"Fetch Rec " + RowID + " From " + Parent);
                var field = this.Fields[columnName];
                var stringValue = Application.Eval(Parent + ".COL" + field.Id);
                return Evaluator.GetValue(field.Type, stringValue);
            }
            set
            {
                var field = this.Fields[columnName];

                string valueString = Evaluator.SetValue(field.Type, value);
                string updCom = CommandHelper.Update(Parent, field.Name, valueString, RowID);
                Application.Do(updCom);
            }
        }

        public dynamic GetValue(Field field)
        {
            Application.Do(@"Fetch Rec " + RowID + " From " + Parent);
            var stringValue = Application.Eval(Parent + ".COL" + field.Id);
            return Evaluator.GetValue(field.Type, stringValue);
        }

        public void SetValue(Field field, dynamic value)
        {
            var updCom = @"Update " + Parent + " Set " + field.Name + @" = """ + value + @""" Where RowID = " + RowID;
            Application.Do(updCom);
        }
    }
}
