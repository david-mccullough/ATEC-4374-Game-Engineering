using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableBlock))]

public class MaterialSwap : MonoBehaviour {

	public Gradient colorGradient;
	public Material defaultMat;
	public float minValue = 5f;
	public float maxValue = 10f;
	float nextUpdate;

	private void Awake()
	{
		
	}

	void OnBlockDamaged()
	{
		if (nextUpdate > Time.time)
			return;

		nextUpdate = Time.time + Mathf.Lerp(minValue, maxValue, .9f);

		Material mat = GetComponent<Renderer>().material;
		float healthPercent = GetComponent<DestructableBlock>().GetPercentRemaining();
		mat.color = colorGradient.Evaluate(healthPercent);
		Debug.Log("Damaged");
	}

	void OnBlockReset()
	{
		Material mat = GetComponent<Renderer>().material;
		mat.color = defaultMat.color;
		Debug.Log("Reset");
	}
}
