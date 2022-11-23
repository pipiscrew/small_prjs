                object excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                object objBooks_Late;
                object objBook_Late;
                object objSheets_Late;

                objBooks_Late = excelApp.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, excelApp, null);
                objBook_Late = objBooks_Late.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objBooks_Late, null);
                objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, objBook_Late, null);
                objSheet_Late = objSheets_Late.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, objSheets_Late, new object[] { 1 });

                string[] columnNames = new string[dt.Columns.Count];
                for (int c = 0; c <= dt.Columns.Count - 1; c++)
                    columnNames[c] = dt.Columns[c].ColumnName;

                //write COLUMN NAMES
                WriteArray2Row(columnNames, "A1");

                //BG COLOR or COLUMN NAMES - https://learn.microsoft.com/en-us/office/vba/api/excel.colorindex
                object interior;
                objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, new object[] { "A1", ColumnAdress(dt.Columns.Count) + "1" });
                interior = objRange_Late.GetType().InvokeMember("Interior", BindingFlags.GetProperty, null, objRange_Late, null);
                objRange_Late.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, null, interior, new object[] { 27 });


                //for each row add to excel - starting by 2nd row
                for (int row = 0; row < dt.Rows.Count; row++)
                    WriteArray2Row(dt.Rows[row].ItemArray, "A" + (row + 2));


                //set all cells as format-type TEXT
                Object Range = objSheets_Late.GetType().InvokeMember("Cells", BindingFlags.GetProperty, null, objSheet_Late, null);
                Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, null, Range, new object[] { "@" });

                //set all columns AUTOFIT
                Object EntireColumn = Range.GetType().InvokeMember("EntireColumn", BindingFlags.GetProperty, null, Range, null);
                EntireColumn.GetType().InvokeMember("Autofit", BindingFlags.InvokeMethod, null, EntireColumn, null);

                //set sheet name
                objRange_Late = objSheet_Late.GetType().InvokeMember("Name", BindingFlags.SetProperty, null, objSheet_Late, new object[] { sheetname });

                //make EXCEL window visible
                excelApp.GetType().InvokeMember("Visible", System.Reflection.BindingFlags.SetProperty, null, excelApp, new object[] { true });

                objRange_Late = null; objSheet_Late = null;