using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioDJManager.Messages;
using RadioDJManager.Models;

namespace RadioDJManager.Events
{
    public class ActionCheckedMsg : UIMessage<EventAction>
    {
        public ActionCheckedMsg(EventAction content) : base(content)
        {
        }
    }
}
