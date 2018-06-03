using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxController : MonoBehaviour {
    
    [SerializeField] private AudioClip sfx_Flip;
    [SerializeField] private AudioClip sfx_SelectShape;
    [SerializeField] private AudioClip sfx_IllegalMove;
    [SerializeField] private AudioClip sfx_LevelComplete;
    [SerializeField] private AudioClip sfx_UI_Back;
    [SerializeField] private AudioClip sfx_UI_Select;
    //[SerializeField] private AudioClip[] backgroundMusic; // The music clip you want to play. The [SerializeField] tag specifies that this variable is viewable in Unity's inspector. I prefer not to use public variables if I can get away with using private ones.

    private AudioSource _audio; // The reference to my AudioSource (look in the Start() function for more details)



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

    }

    /*********************/
    /* Public Interface */
    /*********************/

    public void Play_SFX_Flip()
    {
        _audio.clip = sfx_Flip;
        _audio.Play();
    }

    public void Play_SFX_SelectShape()
    {
        _audio.clip = sfx_SelectShape;
        _audio.Play();
    }

    public void Play_SFX_IllegalMove()
    {
        _audio.clip = sfx_IllegalMove;
        _audio.Play();
    }

    public void Play_SFX_LevelComplete()
    {
        _audio.clip = sfx_LevelComplete;
        _audio.Play();
    }

    public void Play_SFX_UI_Back()
    {
        _audio.clip = sfx_UI_Back;
        _audio.Play();
    }

    public void Play_SFX_UI_Select()
    {
        _audio.clip = sfx_UI_Select;
        _audio.Play();
    }
  
}
