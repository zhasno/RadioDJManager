namespace RadioDJManager.Messages
{
    public enum ConfirmationTypes
    {
        BuyerDelete,
        CLAccountDelete,
        ParcelDelete,
        UserDelete,
        CampaignDelete,
        AdDelete
    }
    public class ConfirmationRequestMsg
    {
        public ConfirmationTypes Type { get; set; }
    }
}
