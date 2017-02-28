using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsSettings : MonoBehaviour {

	Dictionary<string, string> settings;

	private void Awake()
	{
		settings = new Dictionary<string, string>();
		settings.Add ("isWindowed", "0");
		settings.Add ("isInverted", "0");
		NotificationCenter.Default.AddObserver("SaveSettings", SaveSettings);
		NotificationCenter.Default.AddObserver("LoadSettings", LoadSettings);

	}

	//Normally would be passing around a struct containing all the data (not just one bool)
	void SaveSettings(object saved)
	{
		Debug.Log ("Saved");
		settings = (Dictionary<string, string>)saved;

		string value;
		if (settings.TryGetValue ("isWindowed", out value)) {
			PlayerPrefs.SetInt("isWindowed", int.Parse (value));
		}
		if (settings.TryGetValue ("isInverted", out value)) {
			PlayerPrefs.SetInt("isInverted", int.Parse (value));
		}
		PlayerPrefs.Save();
	}

	void LoadSettings(object loaded)
	{
		Debug.Log ("Loaded");
		settings = (Dictionary<string, string>)loaded;

		//settings["isWindowed"] = Convert.ToString(PlayerPrefs.GetInt("isWindowed", 1) == 1);
		//settings["isInverted"] = Convert.ToString(PlayerPrefs.GetInt("isInverted", 1) == 1);
		NotificationCenter.Default.PostNotification("PassSettings", settings);
	}
}
