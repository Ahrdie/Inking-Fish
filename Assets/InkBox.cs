using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum avaliableColors
{
    RED,
    YELLOW,
    BLUE,
    ORANGE,
    GREEN,
    MAGENTA
}

public class InkBox : MonoBehaviour
{
    public Dictionary<string, Color> colors = new Dictionary<string, Color>();

    void Awake()
    {
        colors.Add("RED", Color.red);
        colors.Add("YELLOW", Color.yellow);
        colors.Add("BLUE", Color.blue);
        colors.Add("ORANGE", new Color(.5f,.5f,0f));
        colors.Add("GREEN", Color.green);
        colors.Add("MAGENTA", Color.magenta);
    }

    public avaliableColors MixColors(avaliableColors colorA, avaliableColors colorB){
        //TODO: Implement Mixing of Colors
        return avaliableColors.BLUE;
    }
}

