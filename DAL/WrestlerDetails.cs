//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WrestlerDetails
    {
        public int Id { get; set; }
        public int WrestlerId { get; set; }
        public string LanguageCode { get; set; }
        public string WrestlingName { get; set; }
        public string RealName { get; set; }
        public string BilledFrom { get; set; }
        public string HomeTown { get; set; }
    
        public virtual Wrestler Wrestler { get; set; }
    }
}
