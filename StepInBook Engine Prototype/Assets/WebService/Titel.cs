using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titel 
{
    public List<Texture2D> texture2Ds;
    public Dictionary<string, string> titles;
    public Dictionary<string, string> descriptions;

    public Titel(List<Texture2D> texture2Ds, Dictionary<string, string> descriptions, Dictionary<string, string> titles)
    {
        this.texture2Ds = texture2Ds;
        this.titles = titles;
        this.descriptions = descriptions;
    }
}
