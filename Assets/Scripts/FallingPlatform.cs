using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {

    public GameObject platform;
    public bool isPlayerTouching;

    public Vector3 startPos;

    void Start()
    {
        startPos = this.transform.position;
    }


    void Update()
    {
        if (isPlayerTouching)
        {
            platform.GetComponent<Rigidbody2D>().isKinematic = false;
            platform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
            
        else
        {
            platform.GetComponent<BoxCollider2D>().isTrigger = false;
            platform.transform.position = startPos;
            //Debug.Log("Expected " + transform.position + " Getting " + platform.transform.position);
            platform.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine (Fall ());
        }
    }

    // coroutine to make the platform fall
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(1);

        // no longer stunned
        isPlayerTouching = true;
        
    }
}
