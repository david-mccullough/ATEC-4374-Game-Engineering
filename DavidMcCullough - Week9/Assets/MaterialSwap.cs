using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestructableBlock))]

public class MaterialSwap : MonoBehaviour {

	public Gradient colorGradient;
	public Material defaultMat;
	public float rate = .25f;
	float nextUpdate;

	private void Awake()
	{
		
	}

	void OnBlockDamaged()
	{
		if (nextUpdate > Time.time)
			return;

		nextUpdate = Time.time + rate;

		Material mat = GetComponent<Renderer>().material;
		float healthPercent = GetComponent<DestructableBlock>().GetPercentRemaining();
		mat.color = colorGradient.Evaluate(healthPercent);
	}

	void OnBlockReset()
	{
		Material mat = GetComponent<Renderer>().material;
		mat.color = defaultMat.color;
	}
}
