using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public static int countMelon = 0;
    private float gameTime = 0f;
    [SerializeField] private TMP_Text melonText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeTextTotal;
    public TextMeshProUGUI scoreTextLose;
    public TextMeshProUGUI timeTextTotalLose;
    [SerializeField] private AudioSource collectionAudio;

    public GameObject particleSystemm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            collectionAudio.Play();
            Destroy(collision.gameObject);
            countMelon++;
            melonText.text = ": " + countMelon;
            GameObject pS = Instantiate(particleSystemm, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(pS, 2);
        }
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        timeText.text = TimeGame(gameTime);
        timeTextTotal.text = "Time: " + TimeGame(gameTime);
        timeTextTotalLose.text = "Time: " + TimeGame(gameTime);
        scoreText.text = "Score: " + countMelon;
        scoreTextLose.text = "Score: " + countMelon;

    }
    private string TimeGame(float gametime)
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Score()
    {
        var user = Login.loginModel.username;
        var currentScore = Login.loginModel.score;
        SaveScoreModel saveScore = new SaveScoreModel(currentScore+ countMelon, user);
        StartCoroutine(SaveScore(saveScore));
    }
    IEnumerator SaveScore(SaveScoreModel saveScore)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(saveScore);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-score", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            var jsonString = request.downloadHandler.text.ToString();
            MessageModel message = JsonConvert.DeserializeObject<MessageModel>(jsonString);

        }
        request.Dispose();
    }
}
