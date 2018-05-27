using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(AudioSource))] // This tag is not compulsory, but I like to use it because it makes sense. Basically, it will force any Game Object with this script attached to also have an AudioSource component. I want to do this because I will be referencing an AudioSource later, so I want to make sure that one exists.
public class MusicController : MonoBehaviour
{





    [SerializeField] private AudioClip mainMenuMusic; 
    [SerializeField] private AudioClip[] backgroundMusic; // The music clip you want to play. The [SerializeField] tag specifies that this variable is viewable in Unity's inspector. I prefer not to use public variables if I can get away with using private ones.



    static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;

        }
        else
        {
            Destroy(gameObject);
        }

    }





    private AudioSource _audio; // The reference to my AudioSource (look in the Start() function for more details)

    /*********************/
    /* Protected Mono Methods */
    /*********************/
    protected void Start()
    {
        // Get my AudioSource component and store a reference to it in _audio
        // The point of doing this is because GetComponent() is expensive for computer resources
        // So if we can get away with only calling it one time at the start, then let's do that.
        // From this point on, we can refer to our AudioSource through _audio, which makes the computer happier than GetComponent.
        _audio = GetComponent<AudioSource>();

        // We set the audio clip to play as your background music.
        PlayMenuMusic();
    }




    /*********************/
    /* Public Interface */
    /*********************/

    public void PlayMenuMusic()
    {
        _audio.clip = mainMenuMusic;
        _audio.Play();
        Invoke("PlayNextSong", _audio.clip.length);
    }


    public void PlayNextSong()
    {
        _audio.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        _audio.Play();
        Invoke("PlayNextSong", _audio.clip.length);
    }

    //public void PlayPauseMusic()
    //{
    //    // Check if the music is currently playing.
    //    if (_audio.isPlaying)
    //        _audio.Pause(); // Pause if it is
    //    else
    //        _audio.Play(); // Play if it isn't
    //}

    //public void PlayStop()
    //{
    //    if (_audio.isPlaying)
    //        _audio.Stop();
    //    else
    //        _audio.Play();
    //}

    //public void PauseMusic()
    //{
    //    _audio.Pause();
    //}

    public void PlayMusic()
    {
        PlayNextSong();
    }

    public void StopMusic()
    {
        _audio.Stop();
        CancelInvoke();
    }


}