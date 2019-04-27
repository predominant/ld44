using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class RobotBatteryIndicator : MonoBehaviour
    {
        [SerializeField]
        private Battery _battery;
        [SerializeField]
        private Material _okMaterial;
        [SerializeField]
        private Material _warnMaterial;
        [SerializeField]
        private Material _badMaterial;
        [SerializeField]
        private List<MeshRenderer> _indicators;

        private int _indicatorCount;
        
        private void Awake()
        {
            this._indicatorCount = this._indicators.Count;
        }
        
        public void UpdateIndicator(float value)
        {
            var step = this._battery.Capacity / (float)this._indicatorCount;
            for (var i = 0; i < this._indicatorCount; i++)
            {
                if (this._battery.CurrentCharge >= step * ((float) i + 1f))
                    this._indicators[i].material = this._okMaterial;
                else if (this._battery.CurrentCharge > step * (float) i)
                    this._indicators[i].material = this._warnMaterial;
                else
                    this._indicators[i].material = this._badMaterial;
            }
        }
    }
}