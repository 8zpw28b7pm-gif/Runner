using System.Collections;
using UnityEngine;

namespace RF.UI
{
    public class Fader : MonoBehaviour
    {
        [Range(0.5f, 10f)]
        [SerializeField] private float fadeSpeedFactor;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            UIHandler.Instance.Fader = this;
        }

        private void Start()
        {
            if (canvasGroup == null)
            {
                Debug.LogWarning("Canvas Group in FadeCanvas is null!");
                return;
            }

            StartCoroutine(FadeInRoutine());
        }


        public IEnumerator FadeInRoutine()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeSpeedFactor * Time.deltaTime;
                yield return null;
            }
        }

        public IEnumerator FadeOutRoutine()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += fadeSpeedFactor * Time.deltaTime;
                yield return null;
            }
        }
    }
}
