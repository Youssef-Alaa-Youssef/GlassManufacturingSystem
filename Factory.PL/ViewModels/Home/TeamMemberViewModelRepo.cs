using Factory.DAL.Models.Home;

namespace Factory.PL.ViewModels.Home
{
    public class TeamMemberRepViewModel
    {
        public IEnumerable<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
