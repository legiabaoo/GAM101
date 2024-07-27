using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManeger : MonoBehaviour
{
    public TMP_Text txtCountMelon;
    public int countMelon;
    void Start()
    {
        if (Login.loginModel.score >= 0)
        {
            countMelon = Login.loginModel.score;
            txtCountMelon.text = ": "+countMelon.ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Login.loginModel.score >= 0)
        {
            countMelon = Login.loginModel.score;
            txtCountMelon.text = ": " + countMelon.ToString();
        }
    }
}
