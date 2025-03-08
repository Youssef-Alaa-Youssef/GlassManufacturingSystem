namespace Factory.DAL.Models
{
    public class SettingGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LinkNameEn { get; set; } = string.Empty;
        public string LinkNameAr { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public bool Visable { get; set; }
        public string ranking { get; set; } = string.Empty;
        public string place { get; set; } = string.Empty;
        public string Permission { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
