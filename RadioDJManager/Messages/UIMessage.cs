using System;

namespace RadioDJManager.Messages
{
    public class UIMessage<T> //where T : class
    {
        public T Content { get; set; }
        public Type OriginType { get; set; }

        public UIMessage(T content/*, Type originType = null*/)
        {
            Content = content;
            //OriginType = originType;
        }
    }
}
