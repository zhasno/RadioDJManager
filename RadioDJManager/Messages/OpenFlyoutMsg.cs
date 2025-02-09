namespace RadioDJManager.Messages
{
    public class OpenFlyoutMsg
    {
        public string Name { get; set; }

        public OpenFlyoutMsg(string name)
        {
            Name = name;
        }
    }
}
