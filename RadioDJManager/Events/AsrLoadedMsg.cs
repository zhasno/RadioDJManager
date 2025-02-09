using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioDJManager.Messages;

namespace RadioDJManager.Events
{
    public class AsrLoadedMsg : UIMessage<string>
    {
        public AsrLoadedMsg(string content) : base(content)
        {
        }
    }
}
