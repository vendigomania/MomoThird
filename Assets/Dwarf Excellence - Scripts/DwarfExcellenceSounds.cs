using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfExcellenceSounds : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource lose;
    [SerializeField] private AudioSource dig;

    public static DwarfExcellenceSounds Instance;

    private void Start()
    {
        Instance = this;
    }

    public void Click()
    {
        if(enabled) click.Play();
    }

    public void Lose()
    {
        if(enabled) lose.Play();
    }

    public void Dig()
    {
        if(enabled) dig.Play();
    }
}
