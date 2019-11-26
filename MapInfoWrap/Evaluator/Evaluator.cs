using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public static class Evaluator
    {
        #region GetSomething
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>Nullable dynamic type</returns>
        public static dynamic GetValue(Type type, string value)
        {
            switch (type)
            {
                case Type.CHAR:
                    return value;
                case Type.DECIMAL:
                    return Convert.ToDecimal(value.ToDouble());
                case Type.DOUBLE:
                    return Convert.ToDouble(value.ToDouble());
                case Type.INTEGER:
                    return Convert.ToInt32(value);
                case Type.SMALLINT:
                    return Convert.ToInt16(value);
                case Type.LOGICAL:
                    return value.GetBoolean();
                case Type.DATE:
                    return value.GetDate();
                case Type.TIME:
                    return value.GetTime();
                case Type.DATETIME:
                    return value.GetDateTime();
                case Type.GRAPHIC:
                    return null;
                default:
                    return null;
            }
        }
        #endregion
        public static dynamic SetValue(Type type, dynamic value)
        {
            string partOfUpdCom = " = " + value + @" Where RowID = ";
            switch (type)
            {
                case Type.CHAR:
                    string result;
                    if (value is string)                    
                        result = value;                    
                    else                    
                        result = value.ToString();
                    
                    return result.QuoteWrapper();
                case Type.DECIMAL:
                    try
                    {
                        var data = (decimal)value;
                        return data.ToMapinfo();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to decimal", e);
                    }
                case Type.DOUBLE:
                    try
                    {
                        var data = (Double)value;
                        return data.ToMapinfo();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to double", e);
                    }
                case Type.INTEGER:
                    try
                    {
                        var data = (Int32)value;
                        return data.ToString();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to int", e);
                    }
                case Type.SMALLINT:
                    try
                    {
                        var data = (Int16)value;
                        return data.ToString();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to smallInt", e);
                    }
                case Type.LOGICAL:
                    try
                    {
                        var data = (bool)value;
                        return data.SetBoolean().QuoteWrapper();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to bool", e);
                    }
                case Type.DATE:
                    try
                    {
                        var data = (DateTime)value;
                        return data.SetDate().QuoteWrapper();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to DateTime", e);
                    }
                case Type.TIME:
                    try
                    {
                        var data = (DateTime)value;
                        return data.SetTime().QuoteWrapper();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to DateTime", e);
                    }
                case Type.DATETIME:
                    try
                    {
                        var data = (DateTime)value;
                        return data.SetDateTime().QuoteWrapper();
                    }
                    catch (Exception e)
                    {
                        throw new InvalidCastException("Can't cast to DateTime", e);
                    }
                default:
                    return null;
            }
        }

        #region Extension Methods
        private static string ToDouble(this string value)
        {
            var result = value.Replace(".", ",");
            return result;
        }

        private static string ToMapinfo(this double value)
        {
            var result = value.ToString().Replace(",", ".");
            return result;
        }

        private static string ToMapinfo(this decimal value)
        {
            var result = value.ToString().Replace(",", ".");
            return result;
        }

        private static DateTime? GetDate(this string value)
        {
            if (DateTime.TryParseExact(value, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                return date;
            else
                return null;
        }

        private static string SetDate(this DateTime value)
        {
            return value.Date.ToShortDateString();
        }

        private static DateTime? GetTime(this string value)
        {
            if (DateTime.TryParseExact(value, "HHmmssfff", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                return date;
            else
                return null;
        }

        private static string SetTime(this DateTime value)
        {
            return value.ToShortTimeString();
        }

        private static DateTime? GetDateTime(this string value)
        {
            if (DateTime.TryParseExact(value, "yyyyMMddHHmmssfff", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                return date;
            else
                return null;
        }

        private static string SetDateTime(this DateTime value)
        {
            return value.ToString();
        }

        private static bool GetBoolean(this string value)
        {
            return value == "T";
        }

        private static string SetBoolean(this bool value)
        {
            return value ? "T" : "F";
        }

        private static string QuoteWrapper(this string value)
        {
            StringBuilder builder = new StringBuilder().Append(@"""").Append(value).Append(@"""");
            return builder.ToString();
        }
        #endregion
    }
}
