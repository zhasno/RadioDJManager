using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioDJManager.Messages
{
    public class ShowUiMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public ShowUiMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
