using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Calendar
{
    public static class ExcelExport
    {
        private static object objRange_Late;
        private static object objSheet_Late;

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/694fad12-fcbb-4995-89c5-092e8bb9114a/write-to-excel-multiple-sheet-using-late-binding?forum=vsto
        public static bool GenerateExcel(string sheetname, DataTable dt)
        {
            try
            {
                object excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                object objBooks_Late;
                object objBook_Late;
                object objSheets_Late;

                objBooks_Late = excelApp.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, excelApp, null);
                objBook_Late = objBooks_Late.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objBooks_Late, null);
                objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, objBook_Late, null);
                objSheet_Late = objSheets_Late.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, objSheets_Late, new object[] { 1 });

                //------- FORMATTING

                //set all cells as format-type TEXT ( must be on start, before text written )
                Object Range = objSheets_Late.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null, objSheet_Late, null);
                Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, null, Range, new object[] { "@" });

                //

                //FIRST LINE is HEADER
                string[] columnNames = new string[dt.Columns.Count];
                for (int c = 0; c <= dt.Columns.Count - 1; c++)
                    columnNames[c] = dt.Columns[c].ColumnName;

                //write COLUMN NAMES to HEADER
                WriteArray2Row(columnNames, "A1");


                //for each row add it to excel - starting by 2nd row
                for (int row = 0; row < dt.Rows.Count; row++)
                    WriteArray2Row(dt.Rows[row].ItemArray, "A" + (row + 2));



                //------- FORMATTING

                //BG COLOR of HEADER - https://learn.microsoft.com/en-us/office/vba/api/excel.colorindex
                object interior;
                objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, new object[] { "A1", ColumnAdress(dt.Columns.Count) + "1" });
                interior = objRange_Late.GetType().InvokeMember("Interior", BindingFlags.GetProperty, null, objRange_Late, null);
                objRange_Late.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, null, interior, new object[] { 27 });

                //set all columns AUTOFIT
                object EntireColumn = Range.GetType().InvokeMember("EntireColumn", BindingFlags.GetProperty, null, Range, null);
                EntireColumn.GetType().InvokeMember("Autofit", BindingFlags.InvokeMethod, null, EntireColumn, null);

                //set sheet name
                objSheet_Late.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, objSheet_Late, new object[] { sheetname });

                //


                //make EXCEL window visible
                excelApp.GetType().InvokeMember("Visible", System.Reflection.BindingFlags.SetProperty, null, excelApp, new object[] { true });
				
				// Save the workbook ( tested & working )
				//objSheet_Late.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, objSheet_Late, new object[] { filePath });

                //release the objects - when user closes the EXCEL remove it also the process (othewrwise stays) - https://stackoverflow.com/a/59639192
                Marshal.ReleaseComObject(objBooks_Late);
                Marshal.ReleaseComObject(excelApp);

                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine("Excel cannot be found, operation aborted! \r\n\r\n Error log : \r\n" + x.Message);
                return false;
            }


        }


        private static void WriteArray2Row(object[] vals, string startingCell)
        {
            //the plain method to add it cell by cell avoided (benchmark : plain 173 sec | this 49 sec, for 16k lines with 7 cols -- +253% speed) 
            //instead use the elegant (see the 1st reply of url on the top of this file)
            //ws.Range["A2"].Offset[Idx].Resize[1, dsSP.Tables[i].Columns.Count].Value = dsSP.Tables[i].Rows[Idx].ItemArray;
            //ref -  https://stackoverflow.com/a/14069029

            //get a range object
            objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, new object[] { startingCell, System.Reflection.Missing.Value });

            //set a offset of range - if this is 1 start from 2 row, and looks good to implement COL_NAMES on row1
            objRange_Late = objRange_Late.GetType().InvokeMember("Offset", BindingFlags.GetProperty, null, objRange_Late, new object[] { 0 });

            //set a Resize of offset
            objRange_Late = objRange_Late.GetType().InvokeMember("Resize", BindingFlags.GetProperty, null, objRange_Late, new object[] { 1, vals.Length });

            //Write value in cells
            objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objRange_Late, new object[] { vals });
        }

        private static string ColumnAdress(int col)
        {
            if (col <= 26)
            {
                return Convert.ToChar(col + 64).ToString();
            }
            int div = col / 26;
            int mod = col % 26;
            if (mod == 0) { mod = 26; div--; }
            return ColumnAdress(div) + ColumnAdress(mod);
        }

        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> source)
        {   // https://stackoverflow.com/a/6784997
            DataTable dt = new DataTable();

            var props = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in props)
            {
                DataColumn dc = dt.Columns.Add(prop.Name, prop.PropertyType);
                dc.Caption = prop.DisplayName;
                dc.ReadOnly = prop.IsReadOnly;
            }

            foreach (T item in source)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyDescriptor prop in props)
                    dr[prop.Name] = prop.GetValue(item);

                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
