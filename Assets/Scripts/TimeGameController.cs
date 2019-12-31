using System.Collections;
using TMPro;
using UnityEngine;

public class TimeGameController : GameController {
				public TextMeshProUGUI timeText;
				private float elapsedTime = 0;

				public Transform checkpointHolder;
				public Vector2 PlayerStartPosition { get; private set; }

				private Player player;

				//Respawning
				public float waitTimeForRespawn;

				private void TryCountTime()
				{
								if (!gameOver)
												CountTime();
				}

				private void CountTime()
				{
								elapsedTime += Time.deltaTime;
								UpdateTimeText();
				}

				private void UpdateTimeText()
				{
								timeText.text = string.Format("{0:F}", elapsedTime);
				}

				protected override void ProcessGameOver()
				{
								foreach(Transform checkpointTransform in checkpointHolder) {
												if(checkpointTransform.GetComponent<Checkpoint>().Claimed)
																PlayerStartPosition = checkpointTransform.position;
								}
								GameOver = false;
								player.Kill();
								StartCoroutine(WaitBeforeRespawning());
				}

				private IEnumerator WaitBeforeRespawning()
				{
								yield return new WaitForSeconds(waitTimeForRespawn);
								player.Respawn();
				}

				protected override void ProcessGameWon()
				{
								player.Won();
								Time.timeScale = 0;
				}

				private void Start()
				{
								player = FindObjectOfType<Player>();
								UpdateTimeText();
				}

				private void Update()
				{
								TryCountTime();
				}

}
