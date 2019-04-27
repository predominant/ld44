using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class RobotController : MonoBehaviour
    {
        public Battery Battery;

        [SerializeField]
        private float _moveMultiplier = 1f;
        [SerializeField]
        private float _turnMultiplier = 1f;
        [SerializeField]
        private List<ActionCost> _actionCosts;
        
        [Header("Debugging")]
        [SerializeField]
        private bool _debugForward;
        [SerializeField]
        private bool _debugBackward;
        [SerializeField]
        private bool _debugLeft;
        [SerializeField]
        private bool _debugRight;
        
        
        private Vector3Int _forward = new Vector3Int(0, 0, 1);
        private Rigidbody _rigidbody;

        private void Awake()
        {
            this._rigidbody = this.GetComponent<Rigidbody>();
            if (this.Battery == null)
                this.Battery = this.GetComponent<Battery>();
        }

        private void Update()
        {
            var move = this.MoveInput();
            if (move.magnitude <= 0.05f)
                return;

            
            // Check what action we want to do
            // Is there a cost associated with that action?
            // What is the cost?
            // Do we have enough charge to do the action?
            //   -- Yes: Use the charge, and do the action
            //   --  No: Can we do part of the action? Reduce move magnitude, for example.
            
            
            var action = "move";
            var actionCost = this.HasAction(action);
            if (actionCost == null)
                return;
            
            if (this.HasChargeFor(action, move))
            {
                this.Battery.UseCharge(this.ActionCostFor(action, actionCost, move));
                this._rigidbody.AddForce(
                    this.transform.forward * move.z * this._moveMultiplier,
                    ForceMode.Acceleration);
                this._rigidbody.AddTorque(
                    this.transform.up * move.x * this._turnMultiplier,
                    ForceMode.Impulse);
            }
        }

        private ActionCost HasAction(string type)
        {
            foreach (var actionCost in this._actionCosts)
            {
                if (actionCost.name != type)
                    continue;
                return actionCost;
            }

            return null;
        }
        
        private bool HasChargeFor(string type, params object[] data)
        {
            var actionCost = this.HasAction(type);
            if (actionCost == null)
                return false;

            if (type == "move" && this.Battery.CurrentCharge > 0f)
                return true;
            
            return this.Battery.CurrentCharge >= this.ActionCostFor(type, actionCost, data);
        }

        private float ActionCostFor(string type, ActionCost actionCost, params object[] data)
        {
            var scale = 1f;
            switch (type)
            {
                case "move":
                    scale = ((Vector3Int) data[0]).magnitude;
                    break;
            }

            return scale * actionCost.cost;
        }


        private Vector3Int MoveInput()
        {
            var move = Vector3Int.zero;
            if (Input.GetKey(KeyCode.W) || this._debugForward)
                move += this._forward;
            if (Input.GetKey(KeyCode.S) || this._debugBackward)
                move -= this._forward;
            
            if (Input.GetKey(KeyCode.A) || this._debugLeft)
                move += Vector3Int.left;
            if (Input.GetKey(KeyCode.D) || this._debugRight)
                move += Vector3Int.right;

            return move;
        }
    }
}