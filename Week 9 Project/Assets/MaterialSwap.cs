using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableBlock))]

public class MaterialSwap : MonoBehaviour {

	public Gradient colorGradient;
	public float minValue = 0.5f;
	public float maxValue = 1f;
	float nextUpdate;

	private void Awake()
	{
		
	}

	void OnBlockDamaged()
	{
		if (nextUpdate > Time.time)
			return;

		nextUpdate = Time.time + Mathf.Lerp(minValue, maxValue, 0.5f);

		Material mat = GetComponent<Renderer>().material;

		float healthPercent = GetComponent<DestructableBlock>().GetPercentRemaining();
		mat.color = colorGradient.Evaluate(healthPercent);
	}
}
