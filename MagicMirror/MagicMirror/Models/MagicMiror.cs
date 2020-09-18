using System;
using System.Collections.Generic;
using System.Text;

namespace MagicMirror.Models
{
    public class MagicMiror
    {
        public string Name { get; set; }        

        public string Ip { get; set; }

        public string SelectedNetwork { get; set; }

        public string MacAddress { get; set; }

        public MagicMiror()
        {
            Name = "MagicMirror";
        }
    }
}
