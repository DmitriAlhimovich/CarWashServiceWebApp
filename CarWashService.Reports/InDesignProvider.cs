using System;
using System.Collections.Generic;
using System.Text;

namespace CarWashService.Reports
{
    public class InDesignProvider : IReportProvider
    {
        public InDesign.Document GetReportDocument(string templatePath)
        {
            InDesign.Application app =
               (InDesign.Application)COMCreateObject("InDesign.Application");

            // get a reference to the current active document
            return (InDesign.Document)app.Open(templatePath);            
        }

        private object COMCreateObject(string sProgID)
        {            
            Type oType = Type.GetTypeFromProgID(sProgID);
            return oType != null ? Activator.CreateInstance(oType) : null;
        }
    }
}
