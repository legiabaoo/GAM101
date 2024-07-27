using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class Login : MonoBehaviour
{

    public TMP_InputField edtUser, edtPass, edtUserR, edtPassR, edtUserRP, edtNewPass, edtOTP;
    public TMP_Text txtError, txtErrorR, txtErrorRP;
    public Selectable inputSelectFisrt;
    private EventSystem eventSystem;
    public Button btnLogin;
    public static LoginModel loginModel;

    void Start()
    {
        eventSystem = EventSystem.current;
        inputSelectFisrt.Select();
        /*// Kiểm tra tình trạng kết nối mạng
        NetworkReachability reachability = Application.internetReachability;

        if (reachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Không có kết nối mạng.");
        }
        else if (reachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Đang kết nối qua mạng dữ liệu di động.");
        }
        else if (reachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("Đang kết nối qua mạng WiFi hoặc LAN.");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            btnLogin.onClick.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
            else
            {
                inputSelectFisrt.Select();
            }
        }
    }
    public void checkLogin()
    {
        string user = edtUser.text;
        string pass = edtPass.text;
        //User userModel = new User("lgb@gmail.com", "123456");
        User userModel = new User(user, pass);
        StartCoroutine(loginAPI(userModel));
    }
    public void checkRegister()
    {
        string user = edtUserR.text;
        string pass = edtPassR.text;
        User userModel = new User(user, pass);
        StartCoroutine(RegisterAPI(userModel));
    }
    public void ResetPassword()
    {
        string user = edtUserRP.text;
        string newpass = edtNewPass.text;
        int otp = int.Parse(edtOTP.text);
        ResetPModel resetPModel = new ResetPModel(user, newpass, otp);
        StartCoroutine(ResetPassword(resetPModel));
    }
    public void SendOTP()
    {
        string user = edtUserRP.text;
        User userModel = new User(user);
        StartCoroutine(SendOTP(userModel));
    }

    IEnumerator loginAPI(User userModel)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/login", "POST");
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
            loginModel = JsonConvert.DeserializeObject<LoginModel>(jsonString);
            if (loginModel.status ==1)
            {
                SceneManager.LoadScene("menu");
                Debug.Log("Login Success!");
            }
            else
            {
                txtError.text = loginModel.notification;  
            }
        }
    }
    IEnumerator RegisterAPI(User userModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/register", "POST");
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
            txtErrorR.text = message.notification;
        }
        request.Dispose();
    }

    IEnumerator ResetPassword(ResetPModel resetPModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(resetPModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/reset-password", "POST");
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
            txtErrorRP.text = message.notification;
        }
        request.Dispose();
    }
    IEnumerator SendOTP(User userModel)
    {
        //…
        string jsonStringRequest = JsonConvert.SerializeObject(userModel);

        var request = new UnityWebRequest("https://hoccungminh.dinhnt.com/fpt/send-otp", "POST");
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
            txtErrorRP.text = message.notification;
        }
        request.Dispose();
    }
}
