using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DwarfExcellenceGame : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject playScreen;
    [SerializeField] private Text scoreLable;
    [SerializeField] private Image lifeLine;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Text resultLable;

    [SerializeField] private GameObject ingameRoot;

    [SerializeField] private ParticleSystem[] golds;

    [SerializeField] private Transform character;
    [SerializeField] private Animator move;
    [SerializeField] private Transform stone;

    private int score;
    private bool isPlaying;
    private float lifeTime;

    private int Best
    {
        get => PlayerPrefs.GetInt("Best", 0);
        set => PlayerPrefs.SetInt("Best", value);
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        DwarfExcellenceTrigger.OnLose += Lose;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            lifeTime -= Time.deltaTime * (1f + score / 30f);
            lifeLine.fillAmount = lifeTime / 5f;

            if(lifeTime <= 0f)
            {
                Lose();
            }
        }
    }

    public void StartGame()
    {
        DwarfExcellenceSounds.Instance.Click();

        ingameRoot.SetActive(true);

        startScreen.SetActive(false);
        playScreen.SetActive(true);
        endScreen.SetActive(false);

        score = 0;
        scoreLable.text = score.ToString();

        lifeTime = 5f;
        lifeLine.fillAmount = 1f;

        stone.position = new Vector2(Random.Range(0, 2) == 1 ? 4f : -4f, 10f);
    }

    public void Home()
    {
        DwarfExcellenceSounds.Instance.Click();

        isPlaying = false;

        startScreen.SetActive(true);
        playScreen.SetActive(false);
        endScreen.SetActive(false);

        ingameRoot.SetActive(false);
    }

    public void Dig(int i)
    {
        DwarfExcellenceSounds.Instance.Dig();

        move.Play("Dig");
        character.localScale = new Vector2(i, 1f);

        isPlaying = true;
        lifeTime = Mathf.Min(5f, lifeTime + 1f);

        score++;
        scoreLable.text = score.ToString();

        foreach (var gold in golds) gold.Play();

        stone.Translate(Vector2.down);

        if(stone.position.y < -10f)
        {
            stone.position = new Vector2(Random.Range(0, 2) == 1 ? 4f : -4f, 10f);
        }
    }

    private void Lose()
    {
        stone.Translate(Vector2.up);

        DwarfExcellenceSounds.Instance.Lose();

        isPlaying = false;

        endScreen.SetActive(true);

        if(score > Best)
        {
            Best = score;
            resultLable.text = $"NEW RECORD: {Best}";
        }
        else
        {
            resultLable.text = $"RECORD: {Best}";
        }
    }
}
