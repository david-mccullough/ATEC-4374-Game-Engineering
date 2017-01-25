using UnityEngine;

public class GroupMove : MonoBehaviour {

	public float selectRadius = 3f;
	LayerMask mask;

	Collider[] currentSelection;
	void Update ()
	{
		if (currentSelection == null) {
			SelectOnClick ();
		} else {
			MoveOnClick ();
		}
	}

	void SelectOnClick()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 10000f))
			{
				SelectInArea(hit.point);
			}
		}
	}

	void SelectInArea(Vector3 selectionPosition)
	{
		currentSelection = Physics.OverlapSphere (selectionPosition, selectRadius);

		if (currentSelection.Length == 0)
			return;

		foreach (Collider hit in currentSelection)
		{
			print (hit.name);
		}
	}

	void MoveOnClick()
	{
			//wow
	}

	void MoveToPosition(Vector3 movePos)
	{
		foreach (Collider collider in currentSelection)
		{
			collider.transform.position = movePos;
		 }
	}
}
