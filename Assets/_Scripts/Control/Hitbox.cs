using System;
using UnityEngine;

namespace RF.Control
{
    public class Hitbox : MonoBehaviour
    {
        public event Action onHit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Obstacle")) return;

            Debug.Log("HIT!");
            onHit?.Invoke();
        }
    }
}