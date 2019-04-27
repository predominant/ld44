using System;
using UnityEngine.Events;

namespace LD44.Events
{
    [Serializable]
    public class BatteryChargeChangedEvent : UnityEvent<float>
    {
    }
}