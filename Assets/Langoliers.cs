using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Langoliers : MonoBehaviour {
	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//Unityちゃんとパックンの位置
	private float difference;

	// Use this for initialization
	void Start () {
		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		//Unityちゃんとパックンの位置（ｚ座標）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;

	}

	// Update is called once per frame
	void Update () {

		//Unityちゃんの位置に合わせてパックンの位置を移動
		this.transform.position = new Vector3(0,this.transform.position.y,this.unitychan.transform.position.z-difference -10);

		//時間経過に応じて左右に揺れる（肩を揺らす感じ）
		float speed = 5.0f;
		transform.Rotate(new Vector3(0,Mathf.Sin(Time.time * speed),0));
	}

	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other){

			//接触したオブジェクトを破棄
			Destroy (other.gameObject);
		}
}
