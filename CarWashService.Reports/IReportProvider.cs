namespace CarWashService.Reports
{
    public interface IReportProvider
    {
        InDesign.Document GetReportDocument(string templatePath);
    }
}