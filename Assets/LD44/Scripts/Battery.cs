using System.Collections;
using System.Collections.Generic;
using LD44.Events;
using UnityEngine;
using UnityEngine.Events;

namespace LD44
{
    public class Battery : MonoBehaviour
    {
        public float Capacity = 10f;

        [SerializeField]
        private float _currentCharge = 0f;

        public float CurrentCharge
        {
            get { return this._currentCharge; }
            set
            {
                this._currentCharge = Mathf.Clamp(value, 0, this.Capacity);
                this.OnChargeChanged.Invoke(this._currentCharge);
            }
        }

        [SerializeField]
        private BatteryChargeChangedEvent OnChargeChanged;

        private void Start()
        {
            this.OnChargeChanged.Invoke(this._currentCharge);
        }

        public void UseCharge(float amount)
        {
            this.CurrentCharge -= amount;
        }

        public void ChargeFrom(Battery other)
        {
            var toCharge = this.Capacity - this.CurrentCharge;
            var transfer = Mathf.Min(toCharge, other.CurrentCharge);
            this.CurrentCharge += transfer;
            other.UseCharge(transfer);
        }
    }
}