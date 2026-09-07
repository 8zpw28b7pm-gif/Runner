using UnityEngine;

namespace RF.Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ScrollRepeater : MonoBehaviour
    {
        private Vector3 startPosition;
        private float repeatWidth = 0;

        private void Start()
        {
            startPosition = transform.position;
            repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
        }
        private void Update()
        {
            if (transform.position.x < startPosition.x - repeatWidth)
            {
                transform.position = startPosition;
            }
        }

    }
}
