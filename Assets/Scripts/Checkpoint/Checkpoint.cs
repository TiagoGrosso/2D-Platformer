using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
				public bool Claimed { get; private set; }

				private void Awake()
				{
								Claimed = false;
				}

				public void Claim()
				{
								if (!Claimed) {
												BroadcastMessage("CheckpointClaimed", SendMessageOptions.DontRequireReceiver);
												GameObject.FindGameObjectWithTag("GameController").SendMessage("CheckpointClaimed", new CheckpointInfo(transform.GetSiblingIndex(), transform.position));
								}
								
								Claimed = true;
				}

				public struct CheckpointInfo {
								public int index;
								public Vector3 position;

								public CheckpointInfo(int index, Vector3 position) : this()
								{
												this.index = index;
												this.position = position;
								}
				}
}
