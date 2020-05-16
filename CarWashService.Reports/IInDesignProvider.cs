namespace CarWashService.Reports
{
    public interface IInDesignProvider
    {
        InDesign.Document GetReportDocument(string templatePath);
    }
}