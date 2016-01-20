using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

    bool isActive = false;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().enabled = false;
	}

    // if Player hits the stun point of the enemy, then call Stunned on the enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().enabled = true;

            // start coroutine to stand up eventually
            StartCoroutine (ShowDialogue ());
        }
    }

    // coroutine for slow motion
    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(4.0f);

        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
