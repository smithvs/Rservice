using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RServiceRecordBot.Models
{
    internal class MessageInfo
    {
        public Command Key { get; set; }
        public string Value { get; set; }
        public DateTime Time { get; set; }
    }
}
