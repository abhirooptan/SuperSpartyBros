using UnityEngine;

public class EnemyStun : MonoBehaviour {

    // if Player hits the stun point of the enemy, then call Stunned on the enemy
    void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// tell the enemy to be stunned
			this.GetComponentInParent<Enemy>().Stunned();
            other.gameObject.GetComponent<CharacterController2D>().SlowMotion();
            //camera.ShakeCamera(0.1f, 0.5f);
            GameObject.Find("Main Camera").GetComponent<UnityStandardAssets._2D.Camera2DFollow>().ShakeCamera(0.1f,0.1f);

            // make the player bounce of the enemies head
            other.gameObject.GetComponent<CharacterController2D>().EnemyBounce();
        }
	}
}
