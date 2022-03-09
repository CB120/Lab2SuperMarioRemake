using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] private AudioClip mainBGM;
    [SerializeField] private AudioClip deathBGM;
    [SerializeField] private AudioClip starBGM;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetMusic()
    {
        music.clip = mainBGM;
        music.loop = true;
        music.Play();
    }

    public void ChangeMusic(string musicType, float starManTime)
    {
        if(musicType == "Death")
        {
            music.Stop();
            music.clip = deathBGM;
            music.loop = false;
            music.Play();
        }
        else if (musicType== "Star")
        {
            music.Stop();
            music.clip = starBGM;
            music.Play();
            Invoke("ResetMusic", starManTime);
        }
    }
}
