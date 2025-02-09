namespace RadioDJManager.Messages
{
    public class ConfirmationResponseMsg
    {
        public bool IsConfirmed { get; set; }
        public ConfirmationTypes Type { get; set; }
    }
}
