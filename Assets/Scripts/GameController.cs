using Cinemachine;
using Pathfinding.Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.LWRP;

public abstract class GameController : MonoBehaviour {

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

				protected Player player;
				private CinemachineVirtualCamera vCamera;

				private int lastCheckpointIndex = -1;
				public Vector2 PlayerStartPosition { get; private set; }
				public float waitTimeForRespawn;

				private Transform perishable;
				private GameObject snapshot;

				public void EnableDarkMode()
				{
								player.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
								GameObject.FindGameObjectWithTag("Global Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
								ProcessPostDarkMode();
				}
				public void DisableDarkMode()
				{
								player.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
								GameObject.FindGameObjectWithTag("Global Light").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
				}

				protected void FreeCamera()
				{
								vCamera.Follow = null;
								vCamera.LookAt = null;
				}

				protected void FollowPlayer()
				{
								vCamera.Follow = player.transform;
								vCamera.LookAt = player.transform;
				}

				protected void ProcessGameOver()
				{

								GameOver = false;

								ProcessPostGameOver();
				}

				protected IEnumerator WaitBeforeRespawning()
				{
								yield return new WaitForSecondsRealtime(waitTimeForRespawn);
								RespawnPlayer();
				}

				protected void ProcessGameWon()
				{
								player.Won();
								Time.timeScale = 0;

								ProcessPostGameWon();
				}

				protected virtual void RespawnPlayer()
				{
								FollowPlayer();
								ResetWorld();
								player.Respawn();
				}

				protected virtual void Start()
				{
								player = FindObjectOfType<Player>();
								vCamera = FindObjectOfType<CinemachineVirtualCamera>();
								perishable = GameObject.FindGameObjectWithTag("Perishable").transform;
								CreateSnapshot();
				}

				private void CreateSnapshot()
				{
								if (snapshot)
												Destroy(snapshot);

								GameObject resetableWorld = GameObject.FindGameObjectWithTag("ResetableWorld");

								snapshot = Instantiate(resetableWorld, resetableWorld.transform.parent);
								snapshot.SetActive(false);
				}

				private void ResetWorld()
				{
								Destroy(GameObject.FindGameObjectWithTag("ResetableWorld"));
								foreach (Transform child in perishable)
												Destroy(child.gameObject);

								Instantiate(snapshot, snapshot.transform.parent).SetActive(true);
				}

				protected abstract void ProcessPostGameOver();

				protected abstract void ProcessPostGameWon();

				protected abstract void ProcessPostDarkMode();

				// Messages 

				private void CheckpointClaimed(Checkpoint.CheckpointInfo info)
				{
								if (info.index > lastCheckpointIndex) {
												PlayerStartPosition = info.position;
												CreateSnapshot();
								}
				}
}
