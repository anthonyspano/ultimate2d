using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateBar : MonoBehaviour
{
	public Slider slider;
	
	public void SetMaxUlt(int max)
	{
		slider.maxValue = max;
		slider.value = 0;
		
	}
	
	public void SetUlt(int ult)
	{
		slider.value = ult;
	}
}
