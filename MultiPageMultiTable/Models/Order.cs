using System.ComponentModel.DataAnnotations;

namespace MultiPageMultiTable.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }

}
