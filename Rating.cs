//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CPP2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        public int Id { get; set; }
        public int RaterId { get; set; }
        public int RatedUserId { get; set; }
        public string Comment { get; set; }
        public int InterestingScore { get; set; }
        public int ComprehensibleScore { get; set; }
        public int PolitenessScore { get; set; }
        public decimal AverageScore { get; set; }
    
        public virtual CppUser CppUser { get; set; }
    }
}
