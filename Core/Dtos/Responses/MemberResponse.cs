using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Responses
{
    public class MemberResponse
    {
        public string MembershipId { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
    }
}
