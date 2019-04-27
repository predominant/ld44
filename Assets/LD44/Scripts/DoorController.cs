using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class DoorController : MonoBehaviour
    {
        public Battery Battery;

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private List<ActionCost> _actionCosts;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            
            InterfaceController.Instance.DoorPanel.SetActive(true);
            InterfaceController.Instance.DoorPanel.GetComponent<DoorPanel>().Setup(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;
            
            InterfaceController.Instance.DoorPanel.SetActive(false);
        }

        public void Open(bool value)
        {
            Debug.Log($"DoorController.Open({value})");
            var actionCost = this.HasAction("openclose");
            if (actionCost == null)
                return;

            var currentState = this._animator.GetBool("Open");
            // Dont go any further if we're already in the desired state
            if (currentState == value)
                return;

            if (this.Battery.CurrentCharge < actionCost.cost)
            {
                Debug.Log("Not enough charge for action.");
                return;
            }

            this.Battery.UseCharge(actionCost.cost);
            this._animator.SetBool("Open", value);
        }

        private ActionCost HasAction(string type)
        {
            foreach (var actionCost in this._actionCosts)
            {
                if (actionCost.name != type)
                    continue;
                return actionCost;
            }

            Debug.Log($"Action not found: {type}");
            return null;
        }
    }
}