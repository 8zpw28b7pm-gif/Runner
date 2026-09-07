using UnityEngine;

namespace RF.Control
{
    public class GroundCheck : MonoBehaviour
    {
        public bool isGrounded;

        public bool IsGrounded()
        {
            return isGrounded;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            isGrounded = false;
        }
    }
}
