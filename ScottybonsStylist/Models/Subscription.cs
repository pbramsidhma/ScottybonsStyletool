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
    
    public partial class Subscription
    {
        public int Id { get; set; }
        public Nullable<int> old_subscription { get; set; }
        public Nullable<int> new_subscription { get; set; }
        public int order_id { get; set; }
        public Nullable<bool> is_previousSubscription { get; set; }
        public Nullable<bool> is_currentSubscription { get; set; }
        public Nullable<System.DateTime> subscription_modified { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Order Order1 { get; set; }
    }
}