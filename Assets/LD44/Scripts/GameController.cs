using UnityEngine;

namespace LD44
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public RobotController Player;
        
        private void Awake()
        {
            if (Instance != null)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            Instance = this;
            this.Setup();
        }

        private void Setup()
        {
            this.Player = GameObject.FindObjectOfType<RobotController>();
        }
    }
}