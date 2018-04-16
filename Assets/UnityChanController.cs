using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	//アニメーションするためのコンポートを入れる
	private Animator myAnimator;
	//Unityちゃんを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;
	//前進するための力
	private float forwardForce = 800.0f;
	//左右に移動するための力
	private float turnForce = 500.0f;
	//ジャンプするための力
	private float upForce = 500.0f;
	//左右の移動できる範囲
	private float movableRange = 3.4f;
	//動きを減速させる係数
	private float coefficient = 0.98f;
	private float coefficientG = 0.95f;

	//ゲーム終了の判定
	private bool isEnd = false;

	//ゲーム終了時に表示するテキスト
	private GameObject stateText;
	//スコアを表示するテキスト
	private GameObject scoreText;
	//得点
	private int score = 0;

	//左ボタン押下の判定
	private bool isLButtonDown = false;
	//右ボタン押下の判定
	private bool isRButtonDown = false;
	public bool WinBool = false;

	private GameObject Goal;
	private GameObject GoalCheck;
//	private GameObject pivot;
	private	int count = 0;
	private int WinRunPhase = 0;
//	private float angle = 50.0f;
//	private Transform target;
	private float keika = 0;
	Vector3 finalPoz = new Vector3(0, 0.2f, 136);
	// ++++++++++++++++++++++++++++++++++++ 勝利ポーズの追加 開始 ++++++++++++++++++++++++++++++++++++
//	static int winState = Animator.StringToHash("Base Layer.Winner");
	// ++++++++++++++++++++++++++++++++++++ 勝利ポーズの追加 終了 ++++++++++++++++++++++++++++++++++++


	// Use this for initialization
	void Start () {

		//Animatorコンポーネント取得
		this.myAnimator = GetComponent<Animator>();

		//走るアニメーションを開始
		this.myAnimator.SetFloat("Speed",1.0f);

		//Righdbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();

		//シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		//シーン中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");

		this.Goal = GameObject.Find("Goal");
		this.GoalCheck = GameObject.Find("GoalCheck");
//		this.pivot = GameObject.Find("pivot");
//		target = GameObject.Find ("pivot").transform;

	}
	
	// Update is called once per frame
	void Update () {

		//ゲーム終了ならUnityちゃんの動きを減衰する
		// ゲームオーバーの時
		if (this.isEnd && WinBool ==false) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		// ゴールした時
		if (this.isEnd && WinBool ==true) {
			this.forwardForce *= this.coefficientG;
			this.turnForce *= this.coefficientG;
		}

		//Unityちゃんに前方向の力を加える
		//道中なら前方へ加速
		if (WinBool == false) {
			this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//ゴールしたなら徐々に減速 (＝ゴール位置より数メートル進んで止まる)
		} else {
			float atai = 1;
			this.myRigidbody.AddForce (0,0, (this.forwardForce-atai));
			atai+=0.1f;
		}


		//Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			//左に移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
			//右に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		//Jumpステートの場合はJumpにfalseをセットする
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		//ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			//ジャンプアニメを再生
			this.myAnimator.SetBool ("Jump", true);
			//Unityちゃんに上方向の力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}


		var d = Time.deltaTime*1;

		//UnityちゃんWinningRun (1)_A グランドを回るように中心点から一定距離をグルッと走る
		// (1)_A 「方向転換」...GoalCheck衝突時に右方向を向き、WinRunPhase = 1 へと進む

		// (1)_A 「回転」+「前進」...回り始めてから時間経過で場所固定（停止）
		if (WinRunPhase == 1) {
			if (keika < 36.03f) {
//				keika += Time.deltaTime;
				keika += 0.1f;
				transform.position += transform.forward * 0.05f;
				transform.Rotate (new Vector3 (0, -1.0f, 0));

//				transform.RotateAround (target.position, Vector3.up, angle * keika * (-1));
//				transform.RotateAround (target.position, Vector3.up, angle * Time.deltaTime * (-1));
			} else {
				WinRunPhase = 2;
			}
		}

		//UnityちゃんWinningRun (2):その場でジャンプ＆スピン
		if (WinBool == true && WinRunPhase == 2) {

			if (this.transform.position.z - Goal.transform.position.z > 6) {
				transform.position = Vector3.MoveTowards(transform.position, finalPoz, Time.deltaTime*1);
			}

			if (this.transform.position.z - Goal.transform.position.z > 5) {
				if(count < 250){
				this.transform.Rotate (0, 2, 0);

				count++;

				Debug.Log("count..."+count);
				}
			}
		} else {

		}
			
	}

	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other){

		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//stateTextにGAME OVERを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}

		//ゴール地点に到達した場合
		if (other.gameObject.tag == "GoalTag") {

			this.isEnd = true;
			//stateTextにCLEARを表示
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
			//歩くアニメーションを開始
			this.myAnimator.SetFloat("Speed",0.6f);

			this.myAnimator.SetBool ("WinBool", true);

			this.myRigidbody.AddForce (0,0,1000);
			WinBool = true;

		}

		//コインに衝突した場合
		if (other.gameObject.tag == "CoinTag") {

			//スコアを加算
			this.score += 10;

			//scoreText獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = "Score" + this.score +"pt";

			//パーティクルを再生
			GetComponent<ParticleSystem>().Play();

			//接触したコインのオブジェクトを破棄
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "GoalCheckTag") {
			//一気に右方向に向かせる
			var direction = new Vector3 (1, 0, 0);
			transform.localRotation = Quaternion.LookRotation (direction);
			WinRunPhase = 1;
		}
	}


	//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown(){
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown(){
		this.isLButtonDown = true;
	}

	//左ボタンを離した場合の処理
	public void GetMyLeftButtonUp(){
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown(){
		this.isRButtonDown = true;
	}

	//右ボタンを離した場合の処理
	public void GetMyRightButtonUp(){
		this.isRButtonDown = false;
	}





}


//******* ボツ案 ***********


//ゴールした後の回転
//	public void Kaiten(){
//		if(LastBool==false){
//			for (float plus = 0.001f; plus < 0.5; plus += 0.001f) {
//				this.transform.Rotate (0, plus, 0);
//			Debug.Log ("plus:" + plus);
//			}
//		}
//	}

//1秒待つ処理
//	IEnumerator SampleCoroutine(){
//		this.myAnimator.SetBool ("WinBool", true);
//		yield return new WaitForSeconds(3.0f);
//		this.myAnimator.SetBool ("WinBool", false);
//	}