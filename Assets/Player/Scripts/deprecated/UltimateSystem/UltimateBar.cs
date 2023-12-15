using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// subscribe event if multiple threshold triggers occur in separate places
[RequireComponent(typeof(UltimateMove))]
public class UltimateBar : MonoBehaviour
{
	public Slider slider;
	
	public event EventHandler OnUltReady;
	
	

	public void SetMaxUlt(float max)
	{
		slider.maxValue = max;
		slider.value = 0;
		
	}

	public void AddUlt(int charge)
	{
		slider.value += charge;
		// if ((int) slider.value >= (int) slider.maxValue)
		// {
		// 	if (OnUltFull != null) OnUltFull(this, EventArgs.Empty);
		// }
		if((int) slider.value > PlayerManager.Instance.ultCost)
		{
			if (OnUltReady != null) OnUltReady(this, EventArgs.Empty);
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
