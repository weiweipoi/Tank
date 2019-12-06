using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	//属性值
	public int hp = 3;
	public int playerScore = 0;
	public bool isDead;
	public bool isDefeat;
	
	
	//引用
	public GameObject born;
	public Text playerScoreText;
	public Text playerHpText;
	public GameObject isDefeatUI;
	
	//单例
	private static PlayerManager instance;

	public static PlayerManager Instance
	{
		get { return instance; }
		set { instance = value; }
	}

	private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDefeat)
		{
			isDefeatUI.SetActive(true);
			Invoke("ReturnToTheMainMEnu", 3);
			return;
		}
		if (isDead)
		{
			Recover();
		}

		playerScoreText.text = playerScore.ToString();
		playerHpText.text = hp.ToString();
	}

	private void Recover()
	{
		if (hp<=0)
		{
			//游戏失败，返回主界面
			isDefeat = true;
			Invoke("ReturnToTheMainMEnu", 3);
		}
		else
		{
			hp--;
			GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
			go.GetComponent<Born>().creatPlayer = true;
			isDead = false;
		}
	}

	private void ReturnToTheMainMEnu()
	{
		SceneManager.LoadScene(0);
	}
}
