using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game
	{
	// score for killing a Basic enemy
	public int scoreKillBasic;

	private GameController gameController;
	private int enemyCount;
	private int round;
	public bool isOn;

	private HashSet<GameObject> enemies = new HashSet<GameObject> ();
	private GameObject player;

	public Game (GameController controller)
		{
		gameController = controller;
		isOn = false;
		}

	public bool IsOn ()
		{
		return isOn;
		}

	public void Begin ()
		{
		enemyCount = 0;
		round = 0;
		isOn = true;
		ClearEnemies();
		SpawnPlayer();
		SpawnEnemies();
		gameController.gameText.text = "Kill'em all";
		gameController.UpdateTexts();
		}

	public void Reset ()
		{
		if (player != null)
			{
			MonoBehaviour.Destroy(player);
			player = null;
			}
		Begin();
		}

	public void ClearEnemies ()
		{
		foreach (GameObject enemy in enemies)
			MonoBehaviour.Destroy(enemy);
		enemies.Clear();
		}

	public void SpawnPlayer ()
		{
		int spawnPointIndex = 3;
		GameObject area = gameController.spawnPoints [spawnPointIndex];
		player = MonoBehaviour.Instantiate(gameController.playerType, area.transform.position, area.transform.rotation) as GameObject;
		}

	public void SpawnEnemies ()
		{
		round += 1;
		int count = (int) Random.Range(3 + round, 3 + round * 2);
		for (int i = 0; i < count; i++)
			{
			int spawnPointIndex = i % gameController.spawnPoints.Count;
			int enemyTypeIndex = i % gameController.enemyTypes.Count;
			GameObject enemyType = gameController.enemyTypes [enemyTypeIndex];
			GameObject area = gameController.spawnPoints [spawnPointIndex];
			Vector3 pos = new Vector3 (area.transform.position.x + i * enemyType.transform.localScale.x, area.transform.position.y, area.transform.position.z);
			GameObject enemy = MonoBehaviour.Instantiate(enemyType, pos, area.transform.rotation) as GameObject;
			enemies.Add(enemy);
			enemyCount++;
			}
		}

	public void PlayerKilled (GameObject player)
		{
		gameController.gameText.text = "GAME OVER";
		gameController.UpdateTexts();
		isOn = false;
		}

	public void EnemyKilled (GameObject enemy)
		{
		enemies.Remove(enemy);
		AddScore(scoreKillBasic);
		gameController.UpdateTexts();
		enemyCount--;
		if (enemyCount == 0)
			SpawnEnemies();
		}

	// Handle scoring
	private int	score;

	public void AddScore (int inAmount)
		{
		score += inAmount;
		gameController.UpdateTexts();
		}

	public int GetScore ()
		{
		return score;
		}
	}

public class GameController : MonoBehaviour
	{
	static public Game game;

	public Text gameText;
	public Text scoreText;
	public List<GameObject> spawnPoints;
	public List<GameObject> enemyTypes;
	public GameObject playerType;
	public string ButtonRestart, ButtonReset;

	public GameObject	boundCenter, boundOuter;

	// Use this for initialization
	void Start ()
		{
		GameController.game = new Game (this);
		gameText.text = "Press <Start> to play";
		UpdateTexts();
		game.Begin();
		}
	
	// Update is called once per frame
	private bool isResetting = false;

	public void Update ()
		{
		if (game.IsOn() == false && Input.GetButton(ButtonRestart))
			game.Begin();
		
		if (Input.GetButton(ButtonReset))
			isResetting = true;
		
		if (isResetting == true && Input.GetButton(ButtonReset) == false)
			{
			isResetting = false;
			game.Reset();
			}
		}

	public void UpdateTexts ()
		{
		scoreText.text = "Score: " + GameController.game.GetScore();
		}
	}
