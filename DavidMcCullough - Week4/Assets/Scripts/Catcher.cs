using UnityEngine;

public class Catcher : MonoBehaviour {

	public int scoreValue = 1;
	private PachinkoGame controller;

	void Start()
	{
		controller = GameObject.Find ("Controller").GetComponent<PachinkoGame>();
	}

	void CatchBall(GameObject ballCaught)
	{
		controller.score += scoreValue;
		Destroy(ballCaught);
	}
}
