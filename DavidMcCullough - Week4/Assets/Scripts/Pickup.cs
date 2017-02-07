using UnityEngine;

public class Pickup : MonoBehaviour {

	public int ballValue = 1;
	private PachinkoGame controller;

	void Start()
	{
		controller = GameObject.Find ("Controller").GetComponent<PachinkoGame>();
	}

	void BallGet(GameObject ballCaught)
	{
		Debug.Log ("wow");
		controller.ballCount++;
		Destroy (this.gameObject);
	}
}
