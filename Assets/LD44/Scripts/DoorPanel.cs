using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD44
{
    public class DoorPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _openButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Button _chargeButton;
        [SerializeField]
        private Button _dischargeButton;
        [SerializeField]
        private List<Image> _indicators;
        [SerializeField]
        private Color _okColor = Color.green;
        [SerializeField]
        private Color _warnColor = Color.yellow;
        [SerializeField]
        private Color _badColor = Color.red;

        private int _indicatorCount;

        private DoorController _controller;

        private void Awake()
        {
            this._indicatorCount = this._indicators.Count;
        }

        public void Setup(DoorController controller)
        {
            this._controller = controller;
            this._openButton.onClick.AddListener(this.OpenDoor);
            this._closeButton.onClick.AddListener(this.CloseDoor);
            this._chargeButton.onClick.AddListener(this.Charge);
            this._dischargeButton.onClick.AddListener(this.Discharge);

            this.UpdateIndicator();
        }

        private void OnDisable()
        {
            this._openButton.onClick.RemoveAllListeners();
            this._closeButton.onClick.RemoveAllListeners();
            this._chargeButton.onClick.RemoveAllListeners();
            this._dischargeButton.onClick.RemoveAllListeners();
        }

        public void UpdateIndicator()
        {
            var battery = this._controller.Battery;
            var step = battery.Capacity / (float)this._indicatorCount;
            for (var i = 0; i < this._indicatorCount; i++)
            {
                if (battery.CurrentCharge >= step * ((float) i + 1f))
                    this._indicators[i].color = this._okColor;
                else if (battery.CurrentCharge > step * (float) i)
                    this._indicators[i].color = this._warnColor;
                else
                    this._indicators[i].color = this._badColor;
            }
        }

        private void OpenDoor()
        {
            this._controller.Open(true);
            this.UpdateIndicator();
        }

        private void CloseDoor()
        {
            this._controller.Open(false);
            this.UpdateIndicator();
        }

        private void Charge()
        {
            this._controller.Battery.ChargeFrom(GameController.Instance.Player.Battery);
            this.UpdateIndicator();
        }

        private void Discharge()
        {
            GameController.Instance.Player.Battery.ChargeFrom(this._controller.Battery);
            this.UpdateIndicator();
        }
    }
}