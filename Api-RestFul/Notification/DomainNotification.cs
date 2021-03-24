﻿using MediatR;
using System;

namespace Api_RestFul.Notification
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }
        public DomainNotification(string key, string value)
        {
            this.DomainNotificationId = Guid.NewGuid();
            this.Key = key;
            this.Value = value;
        }
    }
}
