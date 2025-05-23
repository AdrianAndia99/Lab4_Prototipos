﻿using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameEventProt
{
    public class GameIntEventListener : MonoBehaviour
    {
        [SerializeField] private GameIntEvent gameEvent;

        [SerializeField] private UnityEvent<int> response;

        private void OnEnable()
        {
            gameEvent.Register(this);
        }

        private void OnDisable()
        {
            gameEvent.Unregister(this);
        }

        public void OnEventRaised(int value)
        {
            response?.Invoke(value);
        }
    }
}