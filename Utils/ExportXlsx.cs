using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;

namespace LIcensesPO.Utils;

public static class ExportXlsx
{
    public static String Export<T>(IEnumerable<T> table)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(typeof(T).Name);
            // Создаем заголовок
            int col = 1;
            foreach (PropertyInfo property in GetProperties<T>())
            {
                worksheet.Cells[1, col].Value = property.Name;
                col++;
            }

            // Добавляем данные
            int row = 2;
            foreach (T item in table)
            {
                col = 1;
                foreach (PropertyInfo property in GetProperties<T>())
                {
                    object val = property.GetValue(item);
                    object stringVal;
                    if (val.GetType() == typeof(DateTime)) stringVal = ((DateTime)val).ToString("yy-MM-dd");
                    else stringVal = val.ToString();
                    worksheet.Cells[row, col].Value = stringVal;
                    col++;
                }

                row++;
            }
            //сохраняем
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), typeof(T).Name + "_table.xlsx");
            FileInfo file = new FileInfo(filePath);
            package.SaveAs(file);
            return filePath;
        }
    }
    
    private static IEnumerable<PropertyInfo> GetProperties<T>()
    {
        return typeof(T)
            .GetProperties()
            .Where(property => IsPrimitive(property.PropertyType));
    }
    
    private static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }
    
    private static object GetPrimitiveValue(object value)
    {
        if (value == null)
            return null;
        Type type = value.GetType();
        if (IsPrimitive(type))
            return value;
        return null;
    }
}