using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vucongcoder.Models
{
    public class MessageViewModel
    {
        public string RawMessage { get; set; }
        public decimal HeSo { get; set; }

        public List<string> De { get; set; } = new List<string>();
        public List<string> Lo { get; set; } = new List<string>();
        public List<string> Xien { get; set; } = new List<string>();
    }
}