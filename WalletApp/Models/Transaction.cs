
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static WalletProject.Models.TransactionEnum;

namespace WalletProject.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        [Display(Name = "TransactionId")]
        public int TransactionId { get; set; }
        public int WalletId { get; set; }
        public string TransactionName { get; set; } 
        public string VendorName { get; set; }
        public int TransactionCost { get; set; }
        public int  TransactionCostCharge { get; set; }
        public ModeOfPayment ModeOfPayment { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public Categories Category { get; set; }
        public int TotalAmount {
            get
            {
                return TransactionCostCharge + TransactionCost;
            }
            set { }
        
        }
        [JsonIgnore]
        public virtual Wallet Wallets { get; set; }
            

    }
}
