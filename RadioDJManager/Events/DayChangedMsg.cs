using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioDJManager.Messages;

namespace RadioDJManager.Events
{
    public class DayChangedMsg : UIMessage<DateTime>
    {
        public DayChangedMsg(DateTime content) : base(content)
        {
        }
    }
}
