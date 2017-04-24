using UnityEngine;

public class DeathTimer : MonoBehaviour {

	public float alarm = 1f;

	void Update () {
		alarm-=Time.deltaTime;
		if (alarm <= 0f)
		{
			Destroy(this.gameObject);
		}
	}
}
