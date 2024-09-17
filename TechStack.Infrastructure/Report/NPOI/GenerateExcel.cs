using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Shared;

namespace TechStack.Infrastructure.Report.NPOI
{
    public class GenerateExcel : ExcelExport
    {
        public GenerateExcel()
        {
            _headers = new List<string>();
            _type = new List<string>();
        }
        public sealed override void WriteExcelData(DataTable table, Tuple<string, Dictionary<string, string>> reportFilter = null)
        {
            var headerStyle = _workbook.CreateCellStyle();
            var headerFont = _workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);
            int lastRowUsed = _sheet.LastRowNum;
            int count = 1;

            if (reportFilter != null)
            {
                #region Report heading and filter details.
                var row = _sheet.CreateRow(0);
                var rCell = row.CreateCell(0);
                rCell.SetCellValue(Convert.ToString(reportFilter.Item1));
                rCell.CellStyle = headerStyle;


                foreach (var item in reportFilter.Item2)
                {
                    row = _sheet.CreateRow(lastRowUsed + count);
                    rCell = row.CreateCell(0);
                    rCell.SetCellValue(item.Key);
                    rCell = row.CreateCell(1);
                    rCell.SetCellValue(item.Value);
                    count++;
                }
                #endregion 
            }

            if (table != null && table.Rows.Count > 0)
            {
                #region get column names to generate cell header
                foreach (DataColumn column in table.Columns)
                {
                    _type.Add(column.DataType.Name);
                    //string name = Regex.Replace(column.ColumnName, "([A-Z])", " $1").Trim(); //space seperated name by caps for header
                    string name = column.ColumnName;
                    _headers.Add(name);
                }
                #endregion

                lastRowUsed = _sheet.LastRowNum;

                #region Generating Header Cells 
                //var header = _sheet.CreateRow(lastRowUsed + 2);  commented since no filter
                var header = _sheet.CreateRow(lastRowUsed);
                for (var i = 0; i < _headers.Count; i++)
                {
                    var cell = header.CreateCell(i);
                    cell.SetCellValue(_headers[i]);
                    cell.CellStyle = headerStyle;

                    // It's heavy, it slows down your Excel if you have large data
                    _sheet.AutoSizeColumn(i);
                }
                #endregion

                #region Generating SheetRow based on datatype
                lastRowUsed = _sheet.LastRowNum;
                IRow sheetRow = null;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    sheetRow = _sheet.CreateRow(lastRowUsed + 1 + i);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        // TODO: Below commented code is for Wrapping and Alignment of cell
                        // Row1.CellStyle = CellCentertTopAlignment;
                        // Row1.CellStyle.WrapText = true;
                        // ICellStyle CellCentertTopAlignment = _workbook.CreateCellStyle();
                        // CellCentertTopAlignment = _workbook.CreateCellStyle();
                        // CellCentertTopAlignment.Alignment = HorizontalAlignment.Center;
                        ICell row1 = sheetRow.CreateCell(j);
                        string cellvalue = Convert.ToString(table.Rows[i][j]);

                        // TODO: move it to switch case
                        if (string.IsNullOrWhiteSpace(cellvalue))
                        {
                            row1.SetCellValue(string.Empty);
                        }
                        else if (_type[j].ToLower() == Constants.STRING)
                        {
                            row1.SetCellValue(cellvalue);
                        }
                        else if (_type[j].ToLower() == Constants.INT32)
                        {
                            row1.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == Constants.INT64)
                        {
                            row1.SetCellValue(Convert.ToInt64(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == Constants.DOUBLE)
                        {
                            row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == Constants.DECIMAL)
                        {
                            row1.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                        }
                        else if (_type[j].ToLower() == Constants.DATETIME)
                        {
                            row1.SetCellValue(Convert.ToDateTime(
                                 table.Rows[i][j]).ToString(Constants.DATETIME_FORMAT));
                        }
                        else if (_type[j].ToLower() == Constants.BOOLEAN)
                        {
                            row1.SetCellValue(Convert.ToBoolean(table.Rows[i][j]));
                        }
                        else
                        {
                            row1.SetCellValue(string.Empty);
                        }
                    }
                }
            }
            #endregion
        }
    }
}
