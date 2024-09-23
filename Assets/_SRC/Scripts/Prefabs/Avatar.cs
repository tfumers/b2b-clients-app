using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    int id;
    [SerializeField] Texture texture;
    bool created = false;

    public int Id { get => id;}
    public Texture Image { get => texture; }
    public bool Created { get => created;}

    public Avatar CreateAvatar(int id, Texture avatarTexture)
    {
        this.id = id;
        this.texture = avatarTexture;
        this.created = true;
        return this;
    }
}
