using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability<T> : MonoBehaviour
{
				public float cooldown;
				public float duration; // Not all need to have a duration
				protected bool onCooldown = false;
				protected bool active = false;
				protected T value;

				protected GameObject playerObject;

				public void TryCast()
				{
								if (!CanCast())
												return;

								active = true;
								Cast();
								StartCoroutine(Cooldown());
								StartCoroutine(Duration());
				}

				public void TryCast(T value)
				{
								this.value = value;
								TryCast();
				}

				protected virtual bool CanCast()
				{
								return !onCooldown;
				}

				protected abstract void Cast();

				protected virtual void EndAbility()
				{
								active = false;
				}

				protected IEnumerator Cooldown()
				{
								onCooldown = true;
								yield return new WaitForSeconds(cooldown);
								onCooldown = false;
				}

				protected IEnumerator Duration()
				{
								yield return new WaitForSeconds(duration);
								EndAbility();
				}

				protected virtual void Start()
				{
								playerObject = GameObject.FindGameObjectWithTag("Player");
				}
}
 