namespace RadioDJManager.Messages
{
    public class CloseFlyoutMsg
    {
        public string Name { get; set; }

        public CloseFlyoutMsg(string name)
        {
            Name = name;
        }
    }
}
