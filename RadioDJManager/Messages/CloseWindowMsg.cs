using System;

namespace RadioDJManager.Messages
{
    public class CloseWindowMsg
    {
        public Type TargetWindowType { get; set; }

        public CloseWindowMsg(Type targetWindowType)
        {
            TargetWindowType = targetWindowType;
        }
    }
}
