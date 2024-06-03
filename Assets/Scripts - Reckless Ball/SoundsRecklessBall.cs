using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsRecklessBall : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource lose;

    public static SoundsRecklessBall Instance;

    void Start()
    {
        Instance = this;
    }

    public void SetVolume(float volume)
    {
        click.volume = volume;
        jump.volume = volume;
        lose.volume = volume;
    }

    public void Click()
    {
        click.Play();
    }

    public void Jump()
    {
        jump.Play();
    }

    public void Lose()
    {
        lose.Play();
    }
}
