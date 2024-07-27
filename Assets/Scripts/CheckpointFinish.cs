using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CheckpointFinish : MonoBehaviour
{
    [SerializeField] private AudioSource finishAudio;
    private bool finish = false;
    public GameObject win;
    private ItemCollector collector;
    public GameObject gold;
    public GameObject silver;
    public GameObject bronze;
    public GameObject player;
    public Login login;
    private void Start()
    {
        collector = FindAnyObjectByType<ItemCollector>();
        player = GameObject.FindGameObjectWithTag("Player");
        login = GetComponent<Login>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !finish)
        {
            finishAudio.Play();
            finish = true;
            win.SetActive(true);
            Time.timeScale = 0f;
            if (collector != null)
            {
                int melon = ItemCollector.countMelon;
                Rank(melon);
            }
            else
            {
                Debug.LogError("Collector is null");
            }
            savePosition();
        }
        
    }
    private void Rank(int melon)
    {
        if (melon <= 1)
        {
            bronze.SetActive(true);
        }
        else if (melon <= 2)
        {
            silver.SetActive(true);
        }
        else if (melon >= 3)
        {
            gold.SetActive(true);
        }
    }
    public void savePosition()
    {
        string positionX = player.transform.position.x.ToString();
        string positionY = player.transform.position.y.ToString();
        string positionZ = player.transform.position.z.ToString();
        string user = Login.loginModel.username;
        SavePositionModel savePositionModel = new SavePositionModel(user, positionX, positionY, positionZ);
        StartCoroutine(savePosi(savePositionModel));
    }

    IEnumerator savePosi(SavePositionModel savePositionModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(savePositionModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/save-position", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            string jsonString = request.downloadHandler.text;
            MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(jsonString);
            
        }
    }

}
