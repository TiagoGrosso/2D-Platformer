using System;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

				public Data data;
				public LayerMask laserHitLayers;

				private LineRenderer lineRenderer;
				private EdgeCollider2D edgeCollider;
				private Vector2 hitPoint = Vector2.positiveInfinity;


				// Start is called before the first frame update
				void Start()
				{
								lineRenderer = gameObject.GetComponent<LineRenderer>();
								lineRenderer.positionCount = 2;
								lineRenderer.SetPosition(0, transform.position);
								lineRenderer.SetPosition(1, transform.position);
								lineRenderer.enabled = false;
								StartCoroutine(StartDelay());
				}

				private IEnumerator StartDelay()
				{
								yield return new WaitForSeconds(data.startDelay);
								StartCoroutine(Cooldown());
				}

				private IEnumerator Cooldown()
				{
								ShutOff();
								yield return new WaitForSeconds(data.cooldown);
								StartCoroutine(Firing());
				}

				private IEnumerator Firing()
				{
								Coroutine laserUpdate = StartCoroutine(UpdateLaser());
								yield return new WaitForSeconds(data.firingTime);
								StopCoroutine(laserUpdate);
								StartCoroutine(Cooldown());
				}

				private IEnumerator UpdateLaser()
				{
								lineRenderer.enabled = true;
								while (true) {
												Fire();
												yield return new WaitForFixedUpdate();
								}
				}

				public void Fire()
				{
								RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserHitLayers);

								if (hit.point == hitPoint)
												return;
								hitPoint = hit.point;

								lineRenderer.SetPosition(1, hit.point);
								UpdateCollider();
				}

				private void UpdateCollider()
				{
								if (edgeCollider)
												Destroy(edgeCollider);
								edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
								edgeCollider.isTrigger = true;

								Vector3[] points = new Vector3[lineRenderer.positionCount];
								lineRenderer.GetPositions(points);

								Vector2[] convertedPoints = new Vector2[lineRenderer.positionCount];
								for (int i = 0; i < lineRenderer.positionCount; ++i)
												convertedPoints[i] = transform.InverseTransformPoint(points[i]);

								edgeCollider.points = convertedPoints;
								edgeCollider.edgeRadius = 0.03f;
				}

				public void ShutOff()
				{
								Destroy(edgeCollider);
								hitPoint = Vector2.positiveInfinity;

								lineRenderer.SetPosition(1, transform.position);
								lineRenderer.enabled = false;
				}

				[Serializable]
				public struct Data {
								public float startDelay;
								public float cooldown;
								public float firingTime;
				}
}
