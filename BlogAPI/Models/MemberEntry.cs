using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class MemberEntry
    {
        public string MemberId { get; set; } = "";
        public int EntryId { get; set; }

        [ForeignKey(nameof(MemberId))]  
        public AppUser? Member { get; set; }

        [ForeignKey(nameof(EntryId))]
        public Entry? Entry { get; set; }

    }
}
