namespace Factory.PL.ViewModels.Settings
{
    public class ExportViewModel
    {
        public List<string> SupportedFormats { get; set; } = new List<string>();
        public string SelectedFormat { get; set; } = string.Empty;
    }
}
