using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject target;
	public Vector3 cameraOffset = new Vector3(0f, 10f, -20f);
	public float speed = 2f;

	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		float xInput = Input.GetAxis("Mouse X");
		float yInput = Input.GetAxis("Mouse Y");


		//check for null ref
		if (camera != null && target != null)
		{
			Vector3 targetPos = target.transform.position;
			Vector3 offset = cameraOffset;

			float cameraAngle = camera.transform.eulerAngles.y;
			float targetAngle = target.transform.eulerAngles.y;

			targetAngle = Mathf.LerpAngle(cameraAngle, targetAngle, speed*Time.deltaTime);
			offset = Quaternion.Euler(0f, targetAngle, 0f) * offset;

			camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos + offset, speed * Time.deltaTime);
			camera.transform.LookAt(targetPos);
		}
	}
}
