using System;

namespace Zakar.Common.Filters
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SMSNotificationJob : Attribute
    {
    }
}