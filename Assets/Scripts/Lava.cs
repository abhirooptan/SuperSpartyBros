using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

    public AudioClip fireSFX;
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (audio == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            audio = gameObject.AddComponent<AudioSource>();
        }
    }

    // Burn player
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (player.playerCanMove)
            {
                audio.PlayOneShot(fireSFX);
                player.ApplyDamage(10);
            }
        }
    }
}
