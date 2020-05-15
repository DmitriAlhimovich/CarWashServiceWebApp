using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashService.Reports.WinApp.Reports
{
    public class InDesignProvider
    {
        public void WriteRows(IEnumerable<string> rows)
        {
            InDesign.Application app =
               (InDesign.Application)COMCreateObject("InDesign.Application");

            // get a reference to the current active document
            InDesign.Document doc = app.ActiveDocument;

            // get the first page
            InDesign.Page page = (InDesign.Page)doc.Pages[1]; //1e pagina

            // get the first textframe
            InDesign.TextFrame frame = (InDesign.TextFrame)page.TextFrames[1];

            // set contents of textframe
            //frame.Contents = "Andere content";

            InDesign.TextFrame frame2 = (InDesign.TextFrame)page.TextFrames[2];
            frame.Contents = string.Join("\n", rows);

            var idPDFType = 1952403524;
            doc.Export(idPDFType, "c:/Downloads/carwashservice_weekly.pdf");
        }

        public static object COMCreateObject(string sProgID)
        {
            // We get the type using just the ProgID
            Type oType = Type.GetTypeFromProgID(sProgID);
            if (oType != null)
            {
                return Activator.CreateInstance(oType);
            }

            return null;
        }
    }
}
