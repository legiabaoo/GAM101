using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPModel 
{
    public ResetPModel(string username, string newpassword, int otp)
    {
        this.username = username;
        this.newpassword = newpassword;
        this.otp = otp;
    }

    public string username { get; set; }
    public string newpassword { get;set; }
    public int otp { get; set; }
}
