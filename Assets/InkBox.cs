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
    PURPLE,
    WHITE
}

public class InkBox : MonoBehaviour
{
    public Dictionary<string, Color> colors = new Dictionary<string, Color>();

    void Awake()
    {
        colors.Add("RED", Color.red);
        colors.Add("YELLOW", Color.yellow);
        colors.Add("BLUE", Color.blue);
        colors.Add("ORANGE", new Color(1.0f,.5f,0f));
        colors.Add("GREEN", Color.green);
        colors.Add("PURPLE", new Color(.7f, 0f, .7f));
        colors.Add("WHITE", Color.white);
    }

    public avaliableColors MixColors(avaliableColors colorA, avaliableColors colorB){
        if(colorA == colorB){
            return colorA;
        } else {
            List<avaliableColors> colorsToMix = new List<avaliableColors>();
            colorsToMix.Add(colorA);
            colorsToMix.Add(colorB);

            if (colorsToMix.Contains(avaliableColors.RED)){
                colorsToMix.Remove(avaliableColors.RED);
                switch (colorsToMix[0])
                {
                    case avaliableColors.BLUE:
                        return avaliableColors.PURPLE;
                    case avaliableColors.YELLOW:
                        return avaliableColors.ORANGE;
                    default:
                        return avaliableColors.WHITE;
                }
            }
            if (colorsToMix.Contains(avaliableColors.BLUE))
            {
                colorsToMix.Remove(avaliableColors.BLUE);
                switch (colorsToMix[0])
                {
                    case avaliableColors.YELLOW:
                        return avaliableColors.GREEN;
                    default:
                        return avaliableColors.WHITE;
                }
            }
            else
            {
                return avaliableColors.WHITE;
            }
        }
    }
}

