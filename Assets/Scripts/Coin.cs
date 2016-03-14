using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int coinValue = 1;
	public bool taken = false;
	public GameObject explosion;
	GameObject player;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// if the player touches the coin, it has not already been taken, and the player can move (not dead or victory)
	// then take the coin
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.tag == player.tag ) && (!taken) && (player.GetComponent<CharacterController2D>().playerCanMove))
		{
			// mark as taken so doesn't get taken multiple times
			taken=true;
			Pickup ();
		}
	}

	private void Pickup()
	{
		transform.parent = Camera.main.transform;
		StartCoroutine (MoveCoin());


	}

	IEnumerator MoveCoin()
	{
		while (true) {

			var worldPoint = Camera.main.ScreenToWorldPoint (new Vector2 (0, Screen.height));

			transform.position = Vector3.Lerp (transform.position, worldPoint, 3 * Time.deltaTime);

			if ((worldPoint - transform.position).magnitude < 2) {
				// do the player collect coin thing
				player.GetComponent<CharacterController2D>().CollectCoin(coinValue);

				// if explosion prefab is provide, then instantiate it
				if (explosion)
				{
					Instantiate(explosion,transform.position,transform.rotation);
				}

				// destroy the coin
				DestroyObject(this.gameObject);

				yield break;
			}

			yield return null;
		}
	}
}
