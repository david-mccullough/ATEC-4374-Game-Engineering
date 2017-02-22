using UnityEngine;

public class NewGameMenu : MonoBehaviour {

	public GameObject mainMenu;

	public void BackPressed()
	{
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
	}
		
	//TODO
}
