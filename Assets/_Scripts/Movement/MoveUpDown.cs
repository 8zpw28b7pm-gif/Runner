using RF.Core;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

namespace RF.Movement
{
    public class MoveUpDown : MonoBehaviour
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float speed;

        float sinCenterY;
        float timer = 0;

        private void Start()
        {
            sinCenterY = transform.position.y;
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;
            
            timer += Time.deltaTime;

            Vector2 pos = transform.position;

            float sin = Mathf.Sin(timer * speed);
            pos.y = sinCenterY + (sin * amplitude);

            transform.position = pos;
        }
    }
}
