using System;
using UnityEngine;

namespace RF.Core
{
    public class WorldSpeedManager : MonoBehaviour
    {
        [SerializeField] private float _worldSpeed = 0f;
        [SerializeField] private float _worldSpeedMax = 15f;
        [SerializeField] private float _timeToMax = 10f;

        [SerializeField] private float _baseSpeed = 5f;

        private void Awake()
        {
            GameManager.Instance.WorldSpeedManager = this;
            _worldSpeed = _baseSpeed;
        }

        private void Start()
        {
            _worldSpeed = _baseSpeed;
        }

        private void Update()
        {
            if (GameManager.Instance.State != GameState.Running) return;

            IncreaseWorldSpeedOverTime();
        }

        public float GetWorldSpeed()
        {
            return _worldSpeed;
        }

        public float GetMaxSpeed()
        {
            return _worldSpeedMax;
        }

        public float GetSpeedFraction()
        {
            return _worldSpeed / _worldSpeedMax;
        }

        private void IncreaseWorldSpeedOverTime()
        {
            if (_worldSpeed < _worldSpeedMax)
            {
                float speedIncreasePerSecond = _worldSpeedMax / _timeToMax;
                _worldSpeed += speedIncreasePerSecond * Time.deltaTime;
            }

            _worldSpeed = Mathf.Min(_worldSpeed, _worldSpeedMax);
        }


        private void ResetWorldSpeed()
        {
            _worldSpeed = _baseSpeed;
        }
    }
}
