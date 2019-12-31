using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
				public Player player;

				//Abilities
				public Dash dash;

				private PlayerControls playerControls;
				private PlayerMovement playerMovement;
				

				private void Awake()
    {
								Initialize();
								SetControls();
    }

				private void Initialize()
				{
								playerControls = new PlayerControls();
								playerMovement = gameObject.GetComponent<PlayerMovement>();
				}

				private void SetControls()
				{
								playerControls.Abilities.Dash.performed += context => TryDash();
				}

				private void TryDash()
				{
								if(CanDash())
												dash.TryCast(Mathf.Sign(playerMovement.Movement));
				}

				private bool CanDash()
				{
								return player.CanBeControlled && !playerMovement.StuckToWall && playerMovement.Movement != 0;
				}

				private void OnEnable()
				{
								playerControls.Abilities.Enable();
				}

				private void OnDisable()
				{
								playerControls.Abilities.Disable();
				}

				void Update()
    {
        
    }
}
