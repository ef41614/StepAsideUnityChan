using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
	public static int GameMode  = 0; // 0:Normal、 1:Rovky
	public static int AudioPlay = 0; // 0:off、 1:on

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//スタートボタンを押した
//	public void PushStartButton () {
//		SceneManager.LoadScene ("StageSelectScene");	//ステージ選択シーンへ
//	}


	public void PushNormalButton () {
		GameMode = 0;
		AudioPlay = 0;
		SceneManager.LoadScene ("GameScene");	//ゲーム開始

	}

	public void PushRockyButton () {
		GameMode = 1;
		AudioPlay = 1;
		SceneManager.LoadScene ("GameScene");	//ゲーム開始

	}

	public static int getSelectMode(){
		return GameMode;
	}

}


//****************************

/*

	void Rocky_AudioOffボタン 押した時(){
		GameMode=1;
		AudioPlay = 0;
		Game Sceneに遷移
	}

	void Rocky_AudioOnボタン 押した時(){
		GameMode=1;
		AudioPlay = 1;
		Game Sceneに遷移
	}

}
```*/