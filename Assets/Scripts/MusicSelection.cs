using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelection : MonoBehaviour
{
    // Private audio source file grabbed from camera object
	private AudioSource audio;

    // Public audio clip files
    public AudioClip map1, map2, map3, map4, map5, map6;

	void Start ()
    {
        // Selects random song
        System.Random rand = new System.Random();
        int songIndex = rand.Next(1, 6);

        // Sets selected clip
        AudioClip selectedClip;

        switch(songIndex)
        {
            case 1:
                selectedClip = map1;
                break;
            case 2:
                selectedClip = map2;
                break;
            case 3:
                selectedClip = map3;
                break;
            case 4:
                selectedClip = map4;
                break;
            case 5:
                selectedClip = map5;
                break;
            case 6:
                selectedClip = map6;
                break;
            default:
                selectedClip = map1;
                break;
        }

        // Gets AudioSource component and sets clip
        audio = GetComponent<AudioSource>();
        audio.clip = selectedClip;

        // Play audio
        audio.Play();
	}
}
