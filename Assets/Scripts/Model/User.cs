using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public User(string username)
    {
        this.username = username;
    }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public string username { get; set; }
    public string password { get; set; }

}
