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
								Claimed = true;
								BroadcastMessage("CheckpointClaimed", SendMessageOptions.DontRequireReceiver);
				}
}
