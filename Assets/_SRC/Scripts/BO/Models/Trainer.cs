using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer
{
    private long id;

    private string firstname;

    private string lastname;

    private string username;

    private string avatar;

    private Avatar avatarImage;

    private string description;

    private string phone;

    public Trainer()
    {
    }

    public Trainer(long id, string firstname, string lastname, string username, string avatar, string description)
    {
        this.id = id;
        this.firstname = firstname;
        this.lastname = lastname;
        this.username = username;
        this.avatar = avatar;
        this.description = description;
    }

    public Trainer(SimpleJSON.JSONObject json)
    {
        this.id = json["id"];
        this.firstname = json["firstname"];
        this.lastname = json["lastname"];
        this.username = json["username"];
        this.avatar = json["avatar"];
        this.description = json["description"];
        this.phone = json["phone"];
    }

    public long Id { get => id;}
    public string Firstname { get => firstname;}
    public string Lastname { get => lastname;}
    public string Username { get => username;}
    public string Description { get => description;}
    public string Avatar { get => avatar; set => avatar = value; }
    public Avatar AvatarImage { get => avatarImage; set => avatarImage = value; }
    public string Phone { get => phone; set => phone = value; }
}
