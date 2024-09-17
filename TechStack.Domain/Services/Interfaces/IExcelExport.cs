using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Shared;

namespace TechStack.Domain.Services.Interfaces
{
    public interface IExcelExport
    {
        //ExecuteResult<FileResult> ExcelExport<T>(List<T> source, string fileName = "");
        byte[] ExcelExport(DataTable exportData, string fileName, Tuple<string, Dictionary<string, string>> reportFilter = null, bool appendDateTimeInFileName = false,
            string sheetName = Constants.DEFAULT_SHEET_NAME);
        //byte[] Export<T>(List<T> exportData, string fileName,
        //   bool appendDateTimeInFileName = false, string sheetName = Constants.DEFAULT_SHEET_NAME);
    }
}
