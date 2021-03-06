//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScottybonsStylist.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.ForgotPasswords = new HashSet<ForgotPassword>();
            this.Orders = new HashSet<Order>();
            this.OrderShippings = new HashSet<OrderShipping>();
            this.OrderTransactions = new HashSet<OrderTransaction>();
        }
    
        public int CustomerID { get; set; }
        public int MemberId { get; set; }
        public int CustomerstatusID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrePosition { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SaltKey { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string LocaleSetting { get; set; }
        public int Active { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public System.DateTime CreatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForgotPassword> ForgotPasswords { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderShipping> OrderShippings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTransaction> OrderTransactions { get; set; }
        public virtual PasswordLog PasswordLog { get; set; }
    }
}
