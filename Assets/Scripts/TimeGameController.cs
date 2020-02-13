using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeGameController : GameController {

				public float darkModeTime;

				public TextMeshProUGUI timeText;
				private float elapsedTime = 0;

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

				protected override void Start()
				{
								base.Start();
								UpdateTimeText();
				}

				private void Update()
				{
								TryCountTime();
				}

				protected override void ProcessPostGameOver()
				{
								FreeCamera();
								StartCoroutine(WaitBeforeRespawning());
				}

				protected override void ProcessPostGameWon()
				{
								print("Game Won");
				}

				protected override void ProcessPostDarkMode()
				{
								StopCoroutine("DarkModeLifespan");
								StartCoroutine(DarkModeLifespan());
				}

				private IEnumerator DarkModeLifespan()
				{
								yield return new WaitForSeconds(darkModeTime);
								DisableDarkMode();
				}
}
