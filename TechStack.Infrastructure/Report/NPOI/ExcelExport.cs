using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStack.Domain.Services.Interfaces;
using TechStack.Domain.Shared;

namespace TechStack.Infrastructure.Report.NPOI
{
    public abstract class ExcelExport : IExcelExport
    {
        protected string _sheetName;
        protected string _fileName;
        protected List<string> _headers;
        protected List<string> _type;
        protected IWorkbook _workbook;
        protected ISheet _sheet;
        byte[] IExcelExport.ExcelExport(DataTable exportData, string fileName, Tuple<string, Dictionary<string, string>> reportFilter = null, bool appendDateTimeInFileName = false,
            string sheetName = Constants.DEFAULT_SHEET_NAME)
        {
            _sheetName = sheetName;

            _fileName = appendDateTimeInFileName
                ? $"{fileName}_{DateTime.Now.ToString(Constants.DEFAULT_FILE_DATETIME)}"
                : fileName;

            #region Generation of Workbook, Sheet and General Configuration
            _workbook = new XSSFWorkbook();
            _sheet = _workbook.CreateSheet(_sheetName);
            #endregion

            WriteExcelData(exportData, reportFilter);

            #region Generating and Returning byte for Excel
            byte[] result = null;
            using (var memoryStream = new MemoryStream())
            {
                _workbook.Write(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
            #endregion
        }

        public abstract void WriteExcelData(DataTable exportData, Tuple<string, Dictionary<string, string>> reportFilter = null);
    }
}
