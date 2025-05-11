using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraLook : MonoBehaviour
{

	//public Slider sensSlider;
	//public Text sensSliderValue;

	private float sensitivityY = 6f;
	public float minimumY = -70f;
	public float maximumY = 70f;
	private float rotationY = 0f;
	public float aimSens = 2f;
	public float normalSens = 6f;

	public float smooth = 0.5f;


	float offsetY = 0f;

	float totalOffsetY = 0f;

	float resetSpeed = 1f;
	float resetDelay = 0f;

	float maxKickback = 0f;

	float smoothFactor = 2f;

	private Quaternion tRotation;

	private float idleSway = 0.01f;



	private Quaternion originalRotation;

	private Quaternion[] temp;
	private Quaternion smoothRotation;

	
	void Start()
	{
		//if(PlayerMovement.LimitMouseLook == 1)
		//{
			minimumY = -20;
			maximumY = 20;
		//}
	
		originalRotation = transform.localRotation;



		//normalSens = PlayerPrefs.GetFloat("SensVal");

		//sensSliderValue.text = normalSens.ToString("F1");
	}

	
	
public void SetSensitivity_Value()
{
	//sensSliderValue.text = sensSlider.value.ToString("F1");
	//normalSens = sensSlider.value;

	//PlayerPrefs.SetFloat("SensVal", sensSlider.value);

	//snormalSens = PlayerPrefs.GetFloat("SensVal");
}

void Update()
	{


		//if (WeaponBehaviour.isAiming && idleSway > 0)
		//{
		//aimSens = normalSens / 3;
		//sensitivityY = aimSens;
		//idleSway = 0f;
		//}
		//else
		//{
		sensitivityY = normalSens;
			idleSway = 0.01f;
		//}

		rotationY += ControlFreak2.CF2Input.GetAxis("Mouse Y") * sensitivityY;
		
		float yDecrease = Mathf.Clamp(resetSpeed * Time.deltaTime, 0, totalOffsetY);

		if (resetDelay > 0)
		{
			yDecrease = 0;
			resetDelay = Mathf.Clamp(resetDelay - Time.deltaTime, 0, resetDelay);

		}

		if (totalOffsetY < maxKickback)
		{
			totalOffsetY += offsetY;

		}
		else
		{
			offsetY = 0;

			resetDelay *= .5f;

		}

		rotationY = ClampAngle(rotationY, minimumY, maximumY) + offsetY - yDecrease;

		if ((ControlFreak2.CF2Input.GetAxis("Mouse Y") * sensitivityY) < 0)
		{

			totalOffsetY += ControlFreak2.CF2Input.GetAxis("Mouse Y") * sensitivityY;

		}

		rotationY += Mathf.Sin(Time.time) * idleSway;

		totalOffsetY -= yDecrease;


		if (totalOffsetY < 0)
		{
			totalOffsetY = 0;
		}

		Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

		tRotation = originalRotation * yQuaternion;

		float offsetVal = Mathf.Clamp(totalOffsetY * smoothFactor, 1, smoothFactor);

		//temp here
		float tempfloat = Quaternion.Slerp(transform.localRotation, tRotation, Time.deltaTime * 25 / smoothFactor * offsetVal).eulerAngles.x;
		Vector3 tempVector = new Vector3(tempfloat, transform.localEulerAngles.y, transform.localEulerAngles.z);

		transform.localEulerAngles = tempVector;

		//rotationY = transform.localEulerAngles.x- originalRotation.eulerAngles.x;

	}

	public void dorecoil(float recoil)
	{
		rotationY += recoil * Time.deltaTime * 20f;
	}

	float ClampAngle(float angle, float min, float max)
	{

		if (angle < -360F)

			angle += 360F;

		if (angle > 360F)

			angle -= 360F;

		return Mathf.Clamp(angle, min, max);

	}
	//public float turnSpeed = 4.0f;
	//public float moveSpeed = 2.0f;

	//public float minTurnAngle = -90.0f;
	//public float maxTurnAngle = 90.0f;
	//private float rotX;

	//public Transform PlayerBody;
	//void Update()
	//{
	//    MouseAiming();
	//}

	//void MouseAiming()
	//{
	//    // get the mouse inputs
	//    float y = ControlFreak2.CF2Input.GetAxis("Mouse X") * turnSpeed;
	//    rotX += ControlFreak2.CF2Input.GetAxis("Mouse Y") * turnSpeed;

	//    // clamp the vertical rotation
	//    rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

	//    // rotate the camera
	//    transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y, 0);
	//    PlayerBody.Rotate(Vector3.up * y);
	//}


}
