using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioDJManager.Messages;

namespace RadioDJManager.Events
{
    public class DeleteRotatorMsg : UIMessage<int>
    {
        public DeleteRotatorMsg(int content) : base(content)
        {
        }
    }
}
