using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PchanController : MonoBehaviour {
	private Animator myAnimator;
	private Vector3 velocity;
	private Rigidbody myRigidbody;
	private bool towardGoal = false;
	//前進するための力
	private float forwardForce = 795.0f;

	public float wait_time = 10.5f;

	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//UnityちゃんとPちゃんの位置
	private float difference;
	private int mode =0; //0:歩く、1:ぶつかった直後、2:Unityちゃんの背中めがけて加速、3:一定の距離維持
	private float kasoku =0.1f;
	private float kasoku2 =0.1f;
	private float myPosZ = 0;
	private float myPosX = 0;
	private float GoalJump =0;
	private GameObject Goal;
	private GameObject GoalCheck;
	private GameObject GoalCheck2;
	private GameObject pivot;
	private Transform target;
	private bool WinBool = false;

	// Use this for initialization
	void Start () {
		//呼び出し直後の角度を設定
		this.transform.Rotate (0, Random.Range (0, 360), 0);
		myRigidbody = GetComponent<Rigidbody>();

		//Animatorコンポーネント取得
		this.myAnimator = GetComponent<Animator>();

		//停止状態→歩くアニメーションに切り替え
		this.myAnimator.SetFloat("Speed",1.5f);

		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");
		//UnityちゃんとPちゃんの位置（ｚ座標）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;

		myPosZ = Random.Range (-5f, 0f);
		myPosX = Random.Range (-0.05f, 0.05f);
		GoalJump = Random.Range (0, 10);

		this.Goal = GameObject.Find("Goal");
		this.GoalCheck = GameObject.Find("GoalCheck");
		this.GoalCheck2 = GameObject.Find("GoalCheck2");
		this.pivot = GameObject.Find("pivot");
		target = GameObject.Find ("pivot").transform;

	}
	
	// Update is called once per frame
		void Update () {

		//UnityちゃんとPちゃんの位置（ｚ座標）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;

		if (mode == 0) { //0:歩く
			//向いている方向に、一定速度で前に進む(歩く)

			this.transform.position = transform.position + transform.forward * 0.01f;

		} else if (mode == 1) {   //1:ぶつかった直後
			if (difference < 8) {

			} else if (difference >= 8) {

				mode = 2;
			}

		} else if (mode == 2) { //2:Unityちゃんの背中めがけて加速

			if (this.transform.position.z < this.unitychan.transform.position.z - 3 + myPosZ) {

				this.transform.position = new Vector3 (this.transform.position.x + myPosX, 0.2f, this.unitychan.transform.position.z - 3 - 5 + kasoku);
				kasoku += 0.1f;

			} else {
				mode = 3;

			}

		} else if (mode == 3) { //3:一定の距離維持

			//Unityちゃんの位置に合わせてPちゃんの位置を移動
			this.transform.position = new Vector3 (this.transform.position.x, 0.2f, this.unitychan.transform.position.z - 3 + myPosZ);

		} else if (mode == 4) { //4:pivotの位置まで移動する

			if (this.transform.position.z < target.position.z + myPosZ +3) {
				//pivotの位置に合わせてPちゃんの位置を移動
//			this.transform.position = new Vector3 (this.transform.position.x, 0.2f, target.position.z +myPosZ);
//				this.transform.position = new Vector3 (this.transform.position.x + myPosX, 0.2f, target.position.z + myPosZ + kasoku2);
//				kasoku2 += 0.01f;
				this.transform.position = transform.position + transform.forward * 0.1f;
			}else{
				mode = 5;
			}
			Debug.Log("現在mode4です");

		} else if (mode == 5) { //5:Unityちゃんの方を向く
			Debug.Log("mode5へ移行した！");

//			Vector3 targetPositon = target.position;
			// 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
//			if (transform.position.y != target.position.y) {
//				targetPositon = new Vector3 (target.position.x, transform.position.y, target.position.z);
//			}
//			Quaternion targetRotation = Quaternion.LookRotation (targetPositon - transform.position);
//			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime);


			Vector3 targetPositon = unitychan.transform.position;
			// 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
			if (transform.position.y != unitychan.transform.position.y) {
				targetPositon = new Vector3 (unitychan.transform.position.x, unitychan.transform.position.y, unitychan.transform.position.z);
			}
			Quaternion targetRotation = Quaternion.LookRotation (targetPositon - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime);

		}

		//走る→歩くアニメーションに切り替え

		if (unitychan.transform.position.z - Goal.transform.position.z > 5) {
			WinBool = true;
			this.myAnimator.SetBool ("WinBool", true);
//			this.myAnimator.SetFloat("Speed",2);
			mode = 4;

		}
//★		Debug.Log ("winbool:" + Winbool);
//★		Debug.Log ("GoalJump:" + GoalJump);

		if (this.transform.position.z > GoalCheck.transform.position.z) {
			GoalJump = Random.Range (0, 10);
			this.myAnimator.SetFloat("GoalJump",GoalJump);
//			Debug.Log ("GoalJump:" + GoalJump);
		}

		if (this.transform.position.z > GoalCheck2.transform.position.z) {
			mode = 5;
		}

	}

	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other){

		//障害物に衝突した場合
		if (other.gameObject.tag == "Player") {


			//一気にゴールに向かせる
			var direction = new Vector3 (0, 0, 1);
			transform.localRotation = Quaternion.LookRotation (direction);



			//走るアニメーションに切り替え
			this.myAnimator.SetFloat("Speed",4f);
			//向きに関係なく、ゴールめがけて移動するフラグを立てる
			mode =  1; //0:歩く、1:向き切り替え時、2:ゴールへ走る
		}

		if (other.gameObject.tag == "GoalCheck2Tag") {

		}
	}

}