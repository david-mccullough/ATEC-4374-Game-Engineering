using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemsSettings : MonoBehaviour {

	private void Awake()
	{
		NotificationCenter.Default.AddObserver("SaveOurValue", SaveSetting);
		NotificationCenter.Default.AddObserver("FetchSettings", LoadSetting);
	}

	//Normally would be passing around a strcut containing all the data (not just one bool)
	void SaveSetting(object value)
	{
		PlayerPrefs.SetInt("Value",(bool)value ? 1 : 0);
		PlayerPrefs.Save();
	}

	void LoadSetting(object value)
	{
		bool ourSetting = PlayerPrefs.GetInt("Value", 1) == 1;
		NotificationCenter.Default.PostNotification("PassSettings", ourSetting);
	}
}
