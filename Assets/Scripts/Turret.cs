﻿using Pathfinding.Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
				private enum TurretState {
								ready, shooting, cooldown, reloading
				}

				private Dictionary<TurretState, Action> stateActions = new Dictionary<Turret.TurretState, Action>();

				public GameObject bulletPrefab;

				public float reloadTime;
				public int magCapacity;
				private int magSize;

				public int burstSize;
				public float burstShotDelay;

				public float cooldown;
				public float range;
				public bool fixedTarget;

				private Vector3 target;
				private Transform playerTransform;
				private TurretState state;

				private Transform perishable;


				private void Start()
				{
								LoadBullets();

								playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
								perishable = GameObject.FindGameObjectWithTag("Perishable").transform;

								stateActions.Add(TurretState.ready, OnReady);
								stateActions.Add(TurretState.shooting, OnShooting);
								stateActions.Add(TurretState.cooldown, OnCooldown);
								stateActions.Add(TurretState.reloading, OnReloading);
				}

				private void Update()
				{
								stateActions[state]();
				}

				private bool CanSeePlayer()
				{
								Debug.DrawRay(transform.position, (playerTransform.position - transform.position).normalized * range, Color.black);

								RaycastHit2D hit;
								if (hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, range)) {
												return hit.collider.CompareTag("Player");
								}
								return false;
				}

				private void OnReady()
				{
								if (CanSeePlayer()) {
												if (fixedTarget)
																target = playerTransform.position;
												state = TurretState.shooting;
								}
				}

				private bool triggeredCooldown = false;
				private void OnCooldown()
				{
								if (magSize == 0)
												state = TurretState.reloading;
								else if (!triggeredCooldown)
												StartCoroutine(Cooldown());
				}

				private IEnumerator Cooldown()
				{
								triggeredCooldown = true;
								yield return new WaitForSeconds(cooldown);
								state = Turret.TurretState.ready;
								triggeredCooldown = false;
				}

				private int shotCount = 0;
				private bool triggeredShot = false;
				private void OnShooting()
				{
								if (!triggeredShot)
												Shoot();

								if (shotCount == burstSize || magSize == 0) {
												triggeredShot = false;
												shotCount = 0;
												state = TurretState.cooldown;
								}
				}

				private bool waitingForReload = false;
				private void OnReloading()
				{
								if (!waitingForReload)
												StartCoroutine(Reload());
				}

				private IEnumerator Reload()
				{
								waitingForReload = true;
								yield return new WaitForSeconds(reloadTime);
								state = TurretState.ready;
								LoadBullets();
								waitingForReload = false;
				}

				private void LoadBullets()
				{
								if (magCapacity > 0)
												magSize = magCapacity;
								else
												magSize = 10000;
				}


				private void Shoot()
				{
								triggeredShot = true;

								for (int i = 0; i < burstSize && i < magSize; ++i)
												StartCoroutine(WaitThenFireBullet(i * burstShotDelay));
				}

				private IEnumerator WaitThenFireBullet(float delay)
				{
								yield return new WaitForSeconds(delay);
								FireBullet();
				}

				private void ShotFired()
				{
								++shotCount;

								if(magCapacity > 0) //Easy way to make infinite ammo
												--magSize;
				}

				private void FireBullet()
				{
								if (!fixedTarget)
												target = playerTransform.position;

								Vector2 direction = target - transform.position;
								GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, perishable);
								bullet.transform.up = direction; //FIX ME
								bullet.GetComponent<Bullet>().Fire();
								ShotFired();
				}
}
