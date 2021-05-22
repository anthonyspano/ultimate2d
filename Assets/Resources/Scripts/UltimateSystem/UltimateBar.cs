using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
	public Slider slider;
	
	public void SetMaxUlt(float max)
	{
		slider.maxValue = max;
		slider.value = 0;
		
	}
	
	public void SetUlt(float ult)
	{
		slider.value = ult;
	}

	public float GetUlt()
	{
		return slider.value;
	}
}
