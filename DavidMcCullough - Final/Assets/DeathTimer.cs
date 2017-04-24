using UnityEngine;

public class DeathTimer : MonoBehaviour {

	public float alarm = 1f;


	// Update is called once per frame
	void Update () {
		alarm-=Time.deltaTime;
		if (alarm <= 0f)
		{
			Destroy(this.gameObject);
		}
	}
}
