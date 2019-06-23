using System;
namespace TFL.RoadStatus.Models
{
    public class RoadStatusModel
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string StatusSeverity { get; set; }
        public string StatusSeverityDescription { get; set; }
    }
}
