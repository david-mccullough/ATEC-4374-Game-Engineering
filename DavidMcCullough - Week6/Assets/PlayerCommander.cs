using UnityEngine;

public class PlayerCommander : MonoBehaviour {

	public ControllableCharacter currentSelection;

	void Update()
	{
		//If left click, change/deselect current selection
		CheckForCharacterSelection();
		//If right click, issue command
		if (currentSelection != null)
		{
			CheckForCommandIssue();
		}
	}

	void CheckForCharacterSelection()
	{
		
		if (Input.GetButtonDown("Fire1"))
		{
			RaycastHit hit;
			if (MouseCast(out hit))
			{
				//If hit character, select it, else deselects cc
				currentSelection = hit.transform.GetComponent<ControllableCharacter>();
			}
		}
	}


	void CheckForCommandIssue()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			RaycastHit hit;
			if (MouseCast(out hit))
			{
				currentSelection.AddCommand(new MoveCommand(hit.point));
			}
		}
				
	}

	bool MouseCast(out RaycastHit hit)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		return Physics.Raycast(ray, out hit, 9999f);
	}
}
