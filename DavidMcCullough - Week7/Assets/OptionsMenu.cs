using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public GameObject mainMenu;
	public Toggle isWindowed;
	public Toggle isInverted;
	public Dropdown resolution;
	public Slider mouseSlider;
	public Slider volumeMaster;
	public Slider volumeGame;
	public Slider volumeMusic;

	Dictionary<string, string> settings = new Dictionary<string, string>();

	public void BackPressed()
	{
		ReturnToMainMenu();
	}

	public void ConfirmPressed()
	{
		NotificationCenter.Default.PostNotification("SaveSettings", settings);
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
		NotificationCenter.Default.PostNotification("LoadSettings");
	}

	void onDisable()
	{
		NotificationCenter.Default.RemoveObserver("PassSettings", LoadSettings);
	}

	void LoadSettings(object prefs)
	{
		settings = (Dictionary<string, string>)prefs;

		string value;
		/*if (settings.TryGetValue ("isWindowed", out value)) {
			isWindowed.isOn = Convert.ToBoolean(int.Parse (value));
		}
		if (settings.TryGetValue ("isInverted", out value)) {
			isInverted.isOn = Convert.ToBoolean(int.Parse (value));
		}*/
	}
}
