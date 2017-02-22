using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public GameObject mainMenu;
	public Toggle valueToggle;

	public void BackPressed()
	{
		ReturnToMainMenu();
	}

	public void ConfirmPressed()
	{
		NotificationCenter.Default.PostNotification("SaveOurVale", valueToggle.isOn);
		ReturnToMainMenu();
	}

	void ReturnToMainMenu()
	{
		mainMenu.SetActive(true);
		gameObject.SetActive(false);
	}

	void OnEnable()
	{
		NotificationCenter.Default.AddObserver("PassSettings", LoadSettings);
		NotificationCenter.Default.PostNotification("FetchSettings");
	}

	void onDisable()
	{
		NotificationCenter.Default.RemoveObserver("PassSettings", LoadSettings);
	}

	void LoadSettings(object setting)
	{
		valueToggle.isOn = (bool)setting;
	}
}
