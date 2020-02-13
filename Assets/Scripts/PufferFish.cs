using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFish : MonoBehaviour
{
    public Vector2 inflatedScale;
    public float cooldown;
    public float inflatedDuration;
    public float startDelay;
    public float touchDelay;
    public float inflateTime;
    private KillOnTouch killOnTouch;
    private float velocity;
    private Coroutine currentAction;

    private void Start()
    {
        velocity = Vector2.Distance(Vector2.one, inflatedScale) / inflateTime;
        killOnTouch = gameObject.GetComponent<KillOnTouch>();
        killOnTouch.kill = false;
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(Deflated());
    }

    private IEnumerator TouchDelay()
    {
        yield return new WaitForSeconds(touchDelay);
        StartInflate();
    }

    private IEnumerator Deflated()
    {
        yield return new WaitForSeconds(cooldown);
        StartInflate();
    }

    private IEnumerator Inflated()
    {
        yield return new WaitForSeconds(inflatedDuration);
        StartDeflate();
    }

    private void StartDeflate()
    {
        killOnTouch.kill = false;
        transform.localScale = inflatedScale;
        currentAction = StartCoroutine(Deflate());
        StartCoroutine(Deflated());
    }

    private void StartInflate()
    {
        killOnTouch.kill = true;
        transform.localScale = Vector2.one;
        currentAction = StartCoroutine(Inflate());
        StartCoroutine(Inflated());
    }

    private IEnumerator Deflate()
    {
        StartCoroutine(StopFlating());
        while (true) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, Vector2.one, velocity * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Inflate()
    {
        StartCoroutine(StopFlating());
        while (true) {
            transform.localScale = Vector2.MoveTowards(transform.localScale, inflatedScale, velocity * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator StopFlating()
    {
        yield return new WaitForSeconds(inflateTime);
        StopCoroutine(currentAction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player")) {
            StopAllCoroutines();
            StartCoroutine(TouchDelay());
        }
    }
}
