using RF.Core;
using UnityEngine;

namespace RF.Control
{
    public class MoveLeft : MonoBehaviour
    {
        [SerializeField] private float speedModifier = 0f;

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;
            
            float worldSpeed = GameManager.Instance.WorldSpeedManager.GetWorldSpeed();

            transform.Translate(Vector3.left * (worldSpeed + speedModifier) * Time.deltaTime);
        }
    }
}
