//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class film_actor
    {
        public int actor_id { get; set; }
        public int film_id { get; set; }
        public System.DateTime last_update { get; set; }
    
        public virtual actor actor { get; set; }
        public virtual film film { get; set; }
    }
}
