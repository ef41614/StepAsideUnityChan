using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCar : MonoBehaviour {

	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//Unityちゃんとカメラの位置
	private float difference;

	// Use this for initialization
	void Awake () {
		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		//Unityちゃんとカメラの位置（ｚ座標）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;

	}

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//Unityちゃんの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.unitychan.transform.position.z-difference);
	}
}