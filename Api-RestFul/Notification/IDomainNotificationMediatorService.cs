namespace Api_RestFul.Notification
{
    public interface IDomainNotificationMediatorService
    {
        void Notify(DomainNotification notify);
    }
}
