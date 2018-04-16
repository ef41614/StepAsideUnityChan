using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheckController : MonoBehaviour {
	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//Unityちゃんとカメラの位置
//	private float difference;
//	private Rigidbody myRigidbody;
	private GameObject Goal;


	// Use this for initialization
	void Start () {
		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		this.Goal = GameObject.Find("Goal");
		//Unityちゃんとカメラの位置（ｚ座標）の差を求める
	//	this.difference = unitychan.transform.position.z - this.transform.position.z;

	}

	// Update is called once per frame
	void Update () {
		if ((unitychan.transform.position.z > Goal.transform.position.z)&&(this.transform.position.z > 120)) {
//			if ((unitychan.transform.position.z > Goal.transform.position.z)) {
			//Unityちゃんの位置に合わせてカメラの位置を移動
	//		this.transform.position = new Vector3 (0, 0, -0.01f);
//			this.myRigidbody.AddForce (0, 0, -10000f);
			this.transform.position =transform.position + transform.forward * (-0.1f);

//★			this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,Mathf.Sin(Time.time * 0.5f)*6+130);

//			this.transform.Rotate(0,1,0);
		}
	}

}
