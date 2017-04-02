using UnityEngine;

public class Math {

	//Approach moves a toward b at a fixed rate of delta
	public float Approach(float a, float b, float delta)
	{
		if (a < b) {
			return Mathf.Min (a + delta, b);
		} else {
			return Mathf.Max (a - delta, b);
		}
	}
}
