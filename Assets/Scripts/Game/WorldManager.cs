using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Game;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Game
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] Vaus vaus;
        [SerializeField] Ball ball;

        public void Awake()
        {
            Assert.IsNotNull(vaus, "ERROR: vaus is empty");
            Assert.IsNotNull(ball, "ERROR: ball is empty");

            vaus.OnBallReleaseEvent += OnBallReleaseCallback;
        }

        private void OnBallReleaseCallback()
        {
            ball.Release();
        }
    }
}