using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UltimateMove))]
public class UltimateBar : MonoBehaviour
{
	public Slider slider;
	
	public event EventHandler OnUltFull;
	
	public void SetMaxUlt(float max)
	{
		slider.maxValue = max;
		slider.value = 0;
		
	}

	public void AddUlt(int charge)
	{
		slider.value += charge;
		if ((int) slider.value >= (int) slider.maxValue)
		{
			if (OnUltFull != null) OnUltFull(this, EventArgs.Empty);
		}
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
