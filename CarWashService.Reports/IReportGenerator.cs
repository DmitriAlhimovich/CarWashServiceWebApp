using System.Collections.Generic;
using System.Text;

namespace CarWashService.Reports
{
    public interface IReportGenerator
    {
        void FillReport(Dictionary<string, object> paramsDict);
    }
}
