using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WTTechPortal.Services
{
    public static class PdfHelper
    {
        public static void ImportEntityList<TSource>(this Aspose.Pdf.Table table, IList<TSource> data)
        {
            var headRow = table.Rows.Add();
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var dd = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                headRow.Cells.Add(dd != null ? dd.Name : prop.Name);
            }
            foreach (TSource item in data)
            {
                // Add row to table 
                var row = table.Rows.Add();
                // Add table cells 
                for (int i = 0; i < props.Length; i++)
                {
                    var dataItem = props[i].GetValue(item, null);
                    if (props[i].GetCustomAttribute(typeof(DataTypeAttribute)) is DataTypeAttribute dataType)
                        switch (dataType.DataType)
                        {

                            case DataType.Currency:
                                row.Cells.Add(string.Format("{0:C}", dataItem));
                                break;
                            case DataType.Date:
                                var dateTime = (DateTime)dataItem;
                                if (props[i].GetCustomAttribute(typeof(DisplayFormatAttribute)) is DisplayFormatAttribute df)
                                {
                                    if (string.IsNullOrEmpty(df.DataFormatString))
                                        row.Cells.Add(dateTime.ToShortDateString());
                                    else
                                        row.Cells.Add(string.Format(df.DataFormatString, dateTime));
                                }
                                break;
                            default:
                                row.Cells.Add(dataItem.ToString());
                                break;
                        }
                    else
                    {
                        row.Cells.Add(dataItem.ToString());
                    }
                }
            }
        }
    }

}
