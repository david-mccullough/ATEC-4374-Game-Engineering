using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject target;
	public Vector3 cameraOffset = new Vector3(0f, 10f, -20f);
	public float speed = 5f;

	private Camera cam;
	private float cameraAngleX;
	private float cameraAngleY;
	private float targetAngleX;
	private float targetAngleY;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		targetAngleY = cam.transform.eulerAngles.y;
		targetAngleX = cam.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		//check for null ref
		if (cam != null && target != null)
		{
			Vector3 targetPos = target.transform.position;
			Vector3 offset = cameraOffset;

			float xInput = Input.GetAxis("Mouse Y");
			float yInput = Input.GetAxis("Mouse X");

			Debug.Log(xInput);


			cameraAngleX = cam.transform.eulerAngles.x;
			cameraAngleY = cam.transform.eulerAngles.y;
			targetAngleX += (xInput*2) % 360;
			targetAngleY += Mathf.Clamp((yInput*2) % 360, -15f, 60f);

			targetAngleY = Mathf.LerpAngle(cameraAngleY, targetAngleY, 1f);
			targetAngleX = Mathf.LerpAngle(cameraAngleX, targetAngleX, 1f);
			offset = Quaternion.Euler(0f, targetAngleY, 0f) * offset;


			cam.transform.position = Vector3.Lerp(cam.transform.position, targetPos + offset, speed * Time.deltaTime);
			cam.transform.LookAt(targetPos);
		}
	}
}
