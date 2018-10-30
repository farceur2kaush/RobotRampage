using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed = 30f;
    public int damage = 10;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("deathTimer");           // coroutine call
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  // speed of missile per frame
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
