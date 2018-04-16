using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	//carPrefabを入れる
	public GameObject carPrefab;
	//coinPrefabを入れる
	public GameObject coinPrefab;
	//conePrefabを入れる
	public GameObject conePrefab;
	//PchanPrefabを入れる
	public GameObject PchanPrefab;
	//StreetLight_L_Prefabを入れる
	public GameObject StreetLight_L_Prefab;
	//アイテム生成_スタート地点
	private int startPos = -160;
	//ゴール地点
	private int goalPos = 120;
	//アイテムを出すｘ方向の範囲
	private float posRange = 3.4f;
	//Unityちゃんのオブジェクト
	private GameObject unitychan;
	//時間表示用
	private string datetimeStr;
	//アイテム生成したかチェック
	private bool putAlready = false;
	//private bool RockyMode = true;
	public static int sGameMode = 0; //0:Normal, 1:Rocky

	void Awake(){
		sGameMode = TitleManager.GameMode;
//		int sGameMode = TitleManager.getSelectMode();
	}

	// Use this for initialization
	void Start () {
		//現在時刻表示
		datetimeStr = System.DateTime.Now.ToString();
		Debug.Log ("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■課題開始■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
		Debug.Log(datetimeStr);

		//Unityちゃんのオブジェクトを取得
		this.unitychan = GameObject.Find("unitychan");

		// まずはじめに5列分（60ｍ先まで）アイテムを生成する
		for (float i = startPos; i <= (startPos+60); i+=15) {

			//街灯を設置する（無条件で毎回）
			GameObject SLight = Instantiate (StreetLight_L_Prefab) as GameObject;
			SLight.transform.position = new Vector3 (-7.5f, SLight.transform.position.y, i);

			//アイテム生成&設置
			putItem (i);
		}
//		GameMode = 0; //0:Normal, 1:Rocky
//		int sGameMode = TitleManager.GameMode;
//		sGameMode = TitleManager.GameMode;
		//  int resultHitpoint = MainGameController. getHitPoint()
	}




	void Update () {
		//UnityちゃんのZ軸座標_整数（四捨五入して float型→int型へ変換した）
		int UniZ = Mathf.RoundToInt(unitychan.transform.position.z); 

		if ((UniZ % 5) == 0) {
//			Debug.Log ("現在、UniityちゃんのZ座標は" + UniZ);
		}
		// 次に6列目（アイテム生成start地点から60ｍ先）以降は、Unityちゃんが進むにつれて一列ずつアイテムを生成していく（15ｍ間隔）
		// （つまり Unityちゃんがz = -130 に来た時、 +45ｍ先の i=-85 から生成再開）
		int i = UniZ +45;
		int d = i +85; //計算用変数

		if (((d%15)==0 || (d%15)==1 ) && (-87 < i)  && (i < 120)) {
			// dが「15 の倍数」(=アイテム設置予定場所) の時の処理

			//同じ位置において、アイテム生成したかチェック
			if (putAlready == false) {
				//アイテム生成&設置
				putItem (i);
				Debug.Log ("アイテム生成!!");
				Debug.Log ("現在、iは" + i);
				putAlready = true; //アイテム生成したので済みチェック
			}

		}else {
			// そうでない時の処理 →アイテム設置はしない（何もしない）
		}

		if (((d%15)==13 || (d%15)==14 ) && (-79 < i)  && (i < 120)) {
			//次、+1ｍ先でアイテム生成するための準備
			putAlready = false;
		}
	}




	// アイテム生成して設置する
	void putItem (float posZ) {

		//街灯を設置する（無条件で毎回）
		GameObject SLight = Instantiate (StreetLight_L_Prefab) as GameObject;
		SLight.transform.position = new Vector3 (-7.5f, SLight.transform.position.y, posZ);

		if (sGameMode == 0) {
			//どのアイテムを出すのかをランダムに設定
			int num = Random.Range (0, 10);
			if (num <= 1) {
				//コーンをx軸方向に一直線に生成（計6つ）
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, posZ);
				}
			} else {

				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range(-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin = Instantiate (coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, posZ + offsetZ);
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car = Instantiate (carPrefab) as GameObject;
						car.transform.position = new Vector3 (posRange * j, car.transform.position.y, posZ + offsetZ);
					}
				}
			}



		} else if (sGameMode == 1) {
			//どのアイテムを出すのかをランダムに設定
			int num = Random.Range (0, 11);
			if (num <= -1) {
				//コーンをx軸方向に一直線に生成（計6つ）
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, posZ);
				}
			} else if (num >= 2) {
				//プロ生ちゃんを呼び出す
				int offsetX = Random.Range (-3, 3);
				int offsetZ = Random.Range (-5, 6);
				GameObject Pchan = Instantiate (PchanPrefab) as GameObject;
				Pchan.transform.position = new Vector3 (offsetX, Pchan.transform.position.y, posZ + offsetZ);
				//ついでにコインも生成
				GameObject coin = Instantiate (coinPrefab) as GameObject;
				coin.transform.position = new Vector3 (-offsetX, coin.transform.position.y, posZ + offsetZ);

			} else {
				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range (-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin = Instantiate (coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, posZ + offsetZ);
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car = Instantiate (carPrefab) as GameObject;
						car.transform.position = new Vector3 (posRange * j, car.transform.position.y, posZ + offsetZ);
					}
				}
			}
		}
	}
}
