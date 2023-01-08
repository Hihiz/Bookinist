using Bookinist.DAL.Entityes.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookinist.DAL.Entityes
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //public virtual ICollection<Book> Books { get; set; }      
        public virtual Book Books { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Buyer Buyer { get; set; }

        public override string ToString() => $"Сделка по продаже {Books}: {Seller}, {Buyer}, {Price:C}";
    }
}
