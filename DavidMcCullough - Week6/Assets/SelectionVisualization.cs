using UnityEngine;

public class SelectionVisualization : MonoBehaviour {

	public GameObject selectionIndicator;

	PlayerCommander pc;

	void Awake () {
		pc = GameObject.Find("PlayerCommander").GetComponent<PlayerCommander>();
	}

	void Update () 
	{
		if (pc.currentSelection != null)
		{
			selectionIndicator.SetActive(true);
			selectionIndicator.transform.position = pc.currentSelection.transform.position + Vector3.up;
		}
		else
		{
			selectionIndicator.SetActive(false);
		}
	}
}
