using UnityEngine;

public class SelectionVisualization : MonoBehaviour {

	public GameObject selectionIndicator;
	Renderer rend;
	Color colStart;

	PlayerCommander pc;

	void Awake () {
		pc = GameObject.Find("PlayerCommander").GetComponent<PlayerCommander>();
		rend = selectionIndicator.GetComponent<Renderer> ();
		colStart = rend.material.color;
	}

	void Update () 
	{
		if (pc.currentSelection != null)
		{
			selectionIndicator.SetActive(true);
			selectionIndicator.transform.position = pc.currentSelection.transform.position + Vector3.up;
			if (pc.action1) {
				rend.material.color = Color.Lerp (rend.material.color, Color.green, 15f*Time.deltaTime);
			} else {
				rend.material.color = Color.Lerp (rend.material.color, colStart, 3f*Time.deltaTime);
			}

		}
		else
		{
			selectionIndicator.SetActive(false);
		}
	}
}
