using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; } // if null then it's entry's comment 

        public string Text { get; set; } = "";

        public string MemberId { get; set; } = "";

        public int EntryId { get; set; }

        [ForeignKey(nameof(EntryId))]
        public Entry? Entry { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Comment? Parent { get; set; }
    }
}
