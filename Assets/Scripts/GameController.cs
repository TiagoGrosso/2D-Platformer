using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameController : MonoBehaviour
{
				protected bool gameOver = false;
				public bool GameOver {
								get { return gameOver = false; }
								set {
												gameOver = value;
												if (gameOver)
																ProcessGameOver();
								}
				}

				protected bool gameWon = false;
				public bool GameWon {
								get { return gameWon = false; }
								set {
												gameWon = value;
												if (gameWon)
																ProcessGameWon();
								}
				}

				protected abstract void ProcessGameOver();

				protected abstract void ProcessGameWon();
}
