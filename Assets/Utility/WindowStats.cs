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
        public static bool IsBusy;
        public Text ValueText;
        public Text IsBusyText;
        public static bool IsRetreating;
        private void Update()
        {
            ValueText.text = Value.ToString();
            //IsBusyText.text = IsBusy.ToString();
            IsBusyText.text = IsRetreating.ToString();
        }
    }
}