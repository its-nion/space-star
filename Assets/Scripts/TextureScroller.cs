using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public float speed = 1;
    public SpriteRenderer sr;


    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        sr.material.mainTextureOffset = offset;
    }
}
