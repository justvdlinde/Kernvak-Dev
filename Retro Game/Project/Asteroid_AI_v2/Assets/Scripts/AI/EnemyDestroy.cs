using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDestroy : MonoBehaviour {

    public GameObject player;
    public Transform center;
    public AudioSource explosion;

    private void OnCollisionEnter2D(Collision2D other) {
    AudioSource explosion = GetComponent<AudioSource>();
    if (other.gameObject.tag == "Player") {
        ScoreScript.scoreValue += 1;
        Destroy(other.gameObject);
        explosion.Play();
        StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn() {
        Debug.Log("PLAYER DIED");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("RESPAWNED");
        Instantiate(player, center.position, center.rotation);
    }
}


