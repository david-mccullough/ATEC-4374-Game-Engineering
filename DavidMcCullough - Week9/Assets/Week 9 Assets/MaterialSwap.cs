using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableBlock))]

public class MaterialSwap : MonoBehaviour {

	public Gradient colorGradient;
	public float minValue = .05f;
	public float maxValue = .15f;
	float nextUpdate;

	private void Awake()
	{
		
	}

	void OnBlockDamaged()
	{
		if (nextUpdate > Time.time)
			return;

		nextUpdate = Time.time + Mathf.Lerp(minValue, maxValue, Random.value);

		Material mat = GetComponent<Renderer>().material;

		float healthPercent = GetComponent<DestructableBlock>().GetRemainingHealth();
		mat.color = colorGradient.Evaluate(healthPercent);
	}
}
