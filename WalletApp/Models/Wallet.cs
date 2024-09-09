using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletProject.Models
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName ="int")]
        [Display(Name ="WalletId")]
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public virtual User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
