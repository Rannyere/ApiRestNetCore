using System;
using System.Collections.Generic;
using IO.Business.Notifications;

namespace IO.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotifier();

        List<Notification> GetNotifications();

        void Handle(Notification notificacion);
    }
}
