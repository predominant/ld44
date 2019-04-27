using UnityEngine;

namespace LD44
{
    public class InterfaceController : MonoBehaviour
    {
        public static InterfaceController Instance;

        public GameObject DoorPanel;
        
        private void Awake()
        {
            if (Instance != null)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            Instance = this;
        }
    }
}