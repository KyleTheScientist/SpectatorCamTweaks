using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq.Expressions;

namespace SpectatorCamTweaks.Patches
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("FixedUpdate", MethodType.Normal)]

    class ShoulderCamPatch
    {
        public static Cinemachine.Cinemachine3rdPersonFollow thirpy;
        public static Vector3 baseOffset, baseScale;
        public static float baseDistance, baseRadius, baseArmLength;
        public static GameObject shoulderCam;
        public static bool enabled = true;

        private static void GetThirpy()
        {
            foreach (var camera in GameObject.FindObjectsOfType<Camera>())
            {
                if (camera.name == "Shoulder Camera")
                {
                    shoulderCam = camera.gameObject;
                    thirpy = camera.GetComponentInChildren<Cinemachine.Cinemachine3rdPersonFollow>();
                    baseDistance = thirpy.CameraDistance;
                    baseOffset = thirpy.ShoulderOffset;
                    baseScale = thirpy.FollowTarget.localScale;
                    baseRadius = thirpy.CameraRadius;
                    baseArmLength = thirpy.VerticalArmLength;
                }
            }
        }

        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            if (!enabled) return;
            if (!thirpy)
                GetThirpy();
            thirpy.CameraDistance = baseDistance * __instance.scale;
            thirpy.CameraRadius = baseRadius * __instance.scale;
            thirpy.VerticalArmLength = baseArmLength * __instance.scale;
            thirpy.ShoulderOffset = baseOffset * __instance.scale;
        }

        public static void Enable()
        {
            enabled = true;
        }

        public static void Disable()
        {
            enabled = false;
            thirpy.CameraDistance = baseDistance;
            thirpy.CameraRadius = baseRadius;
            thirpy.VerticalArmLength = baseArmLength;
            thirpy.ShoulderOffset = baseOffset;
        }
    }
}
