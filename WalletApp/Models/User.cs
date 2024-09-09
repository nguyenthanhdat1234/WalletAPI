using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName ="int")]
        [Display(Name ="UserId")]
        public int UsertId { get; set; }

        public string UserName { get; set; }    
        public string Password { get; set; } 
        
        public virtual Wallet Wallets { get; set; }

    }
}
