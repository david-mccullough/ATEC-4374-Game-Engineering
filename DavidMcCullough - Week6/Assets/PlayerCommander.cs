using UnityEngine;

public class PlayerCommander : MonoBehaviour {

	public ControllableCharacter currentSelection;

	public bool action1 = false;

	void Update()
	{
		//If left click, change/deselect current selection
		CheckForCharacterSelection();
		//If right click, issue command
		if (currentSelection != null)
		{
			CheckForCommandIssue();
			//Clear queue for selected character - Sims style
			if (Input.GetButtonDown ("Jump")) {
				currentSelection.ClearCommands ();
			}
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
				if (!action1)
					currentSelection = hit.transform.GetComponent<ControllableCharacter>();
			}
		}
	}

	void CheckForCommandIssue()
	{
		if (Input.GetButtonDown ("Fire2")) {
			RaycastHit hit;
			if (MouseCast (out hit)) {
				if (hit.transform.tag == "Floor") {
					currentSelection.AddCommand (new MoveCommand (hit.point));
				}
			}
		}

		//Toggle action1 command issuing
		if (Input.GetKeyDown (KeyCode.Keypad1) || Input.GetKeyDown (KeyCode.Alpha1)) {
			action1 = !action1;
		}

		if (action1) {
			if (Input.GetButtonDown ("Fire1")) {
				RaycastHit hit;
				if (MouseCast (out hit)) {
					if (hit.transform.tag == "Item") {
						currentSelection.AddCommand (new MoveCommand (hit.point));
						currentSelection.AddCommand (new PickupCommand (hit.transform.gameObject));
						action1 = false;
					} else if (hit.transform.tag == "Floor") {
						currentSelection.AddCommand (new MoveCommand (hit.point));
						currentSelection.AddCommand (new DropCommand ());
						action1 = false;
					}
				}
			}
		}
	}

	bool MouseCast(out RaycastHit hit)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		return Physics.Raycast(ray, out hit, 9999f);
	}
}
