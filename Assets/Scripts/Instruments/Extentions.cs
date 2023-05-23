using UnityEngine;

public static partial class Extentions
{
	public static float Remap(this float Value, float oldMin, float oldMax, float newMin, float newMax)
	{
		float t = Mathf.InverseLerp(oldMin, oldMax, Value);
		Value = Mathf.Lerp(newMin, newMax, t);
		return Value;
	}
}
