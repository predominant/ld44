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

        private DoorController _controller;
        
        public void Setup(DoorController controller)
        {
            this._controller = controller;
            this._openButton.onClick.AddListener(this.OpenDoor);
            this._closeButton.onClick.AddListener(this.CloseDoor);
            this._chargeButton.onClick.AddListener(this.Charge);
            this._dischargeButton.onClick.AddListener(this.Discharge);
        }

        private void OnDisable()
        {
            this._openButton.onClick.RemoveAllListeners();
            this._closeButton.onClick.RemoveAllListeners();
            this._chargeButton.onClick.RemoveAllListeners();
            this._dischargeButton.onClick.RemoveAllListeners();
        }


        private void OpenDoor()
        {
            this._controller.Open(true);
        }

        private void CloseDoor()
        {
            this._controller.Open(false);
        }

        private void Charge()
        {
            this._controller.Battery.ChargeFrom(GameController.Instance.Player.Battery);
        }

        private void Discharge()
        {
            GameController.Instance.Player.Battery.ChargeFrom(this._controller.Battery);
        }
    }
}