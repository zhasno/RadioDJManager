namespace RadioDJManager.Messages
{
    public class ShowUiNotificationMsg
    {
        public string Message { get; set; }

        public ShowUiNotificationMsg(string message)
        {
            Message = message;
        }

    }
}
