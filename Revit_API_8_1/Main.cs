using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_8_1
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string expFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //TaskDialog.Show("1", expFolderPath);

            IFCExportOptions ifcExpOpts = new IFCExportOptions();

            using (Transaction ts = new Transaction(doc, "Export Transaction"))
            {
                ts.Start();

                doc.Export(expFolderPath, doc.Title, ifcExpOpts);
                TaskDialog.Show("Выполнено", $"Проект {doc.Title} экспортирован в IFC {Environment.NewLine}(см. \"Рабочий стол\")");

                ts.Commit();
            }
            return Result.Succeeded;
        }
    }
}
