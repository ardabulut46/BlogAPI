using BlogAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Entry
    {
        public int Id { get; set; }

        public string MemberId { get; set; } = "";

        [StringLength(100, MinimumLength = 1)]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; } = "";

        public string Text { get; set; } = "";

        public int LikedCount { get; set; }

        public int DislikeCount {  get; set; }  

        [ForeignKey(nameof(MemberId))]
        public AppUser? Member { get; set; }

        public List<Comment>? Comments { get; set; }

    }
    


}