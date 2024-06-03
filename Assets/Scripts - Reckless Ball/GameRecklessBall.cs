using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRecklessBall : MonoBehaviour
{
    [SerializeField] private Camera curCamera;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject playScreen;
    [SerializeField] private Text scoreLable;

    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Text resultScoreLable;

    [Header("Ingame")]
    [SerializeField] private GameObject gameRoot;

    [SerializeField] private Rigidbody2D ballRgb;

    [SerializeField] private Transform[] parts;

    int score = 0;
    bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        curCamera.orthographicSize = 5 * ((float)Screen.height / Screen.width / (1920f / 1080f));

        LoseTrigger.OnLose += Lose;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if(ballRgb.transform.position.y < 1f)
            {
                var stepSize = 1f - ballRgb.transform.position.y;

                ballRgb.transform.Translate(Vector2.up * stepSize * Time.deltaTime);

                for(int i = 0; i < parts.Length; i++)
                {
                    parts[i].transform.Translate(Vector2.up * stepSize * Time.deltaTime);

                    if (parts[i].gameObject.activeSelf && parts[i].transform.position.y > 7f)
                    {
                        parts[i].gameObject.SetActive(false);

                        var newPart = GetFreePart();
                        newPart.position = new Vector2(score % 2 == 0 ? -1f : 1f, parts[i].transform.position.y - 12f);
                        newPart.localScale = new Vector2(score % 2 == 0 ? 1f : -1f, 1f);
                        newPart.gameObject.SetActive(true);

                        score++;
                        UpdateUI();
                    }
                }
            }


        }
    }

    public void Play()
    {
        SoundsRecklessBall.Instance.Click();

        startScreen.SetActive(false);
        playScreen.SetActive(true);
        loseScreen.SetActive(false);

        score = 0;

        UpdateUI();

        foreach(var part in parts) part.gameObject.SetActive(false);

        for(int i = 0; i < 4; i++)
        {
            var part = GetFreePart();
            part.gameObject.SetActive(true);
            part.position = new Vector2(i % 2 == 0 ? -1f : 1f, 2f - 3f * i);
            part.localScale = new Vector2(i % 2 == 0 ? 1f : -1f, 1f);
        }

        ballRgb.transform.position = new Vector2(-1f, 3f);
        ballRgb.velocity = Vector2.zero;

        ballRgb.simulated = true;
        CollisionChecker.IsGrounded = false;

        isPlaying = true;
        gameRoot.SetActive(true);
    }

    public void Jump()
    {
        if(CollisionChecker.IsGrounded)
        {
            ballRgb.AddForce(Vector2.up * 200f);
            SoundsRecklessBall.Instance.Jump();
        }
    }

    public void BackToMenu()
    {
        playScreen.SetActive(false);
        startScreen.SetActive(true);
        loseScreen.SetActive(false);

        gameRoot.SetActive(false);
    }

    private Transform GetFreePart()
    {
        int index = Random.Range(0, parts.Length);
        while(parts[index].gameObject.activeSelf)
        {
            index = (index + 1) % parts.Length;
        }

        return parts[index];
    }

    private void UpdateUI()
    {
        scoreLable.text = "SCORE: " + score.ToString();
        resultScoreLable.text = "SCORE: " + score.ToString();
    }

    private void Lose()
    {
        SoundsRecklessBall.Instance.Lose();

        ballRgb.simulated = false;
        isPlaying = false;

        loseScreen.SetActive(true);
    }
}
