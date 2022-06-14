using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DebugTools
{
    public class WindowStats : MonoBehaviour
    {
        public static float Value = 0;
        public Text ValueText;
        private void Update()
        {
            ValueText.text = Value.ToString();
        }
    }
}