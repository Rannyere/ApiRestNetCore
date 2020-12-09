using System;
using System.Collections.Generic;
using System.Linq;
using IO.Business.Interfaces;

namespace IO.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotifier()
        {
            return _notifications.Any();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notificacion)
        {
            _notifications.Add(notificacion);
        }  
    }
}
