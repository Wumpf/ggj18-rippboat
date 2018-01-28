using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WinnerIconDisplayer : MonoBehaviour {

    [SerializeField]
    Sprite DefaultIcon;
    [SerializeField]
    Sprite[] PlayerIcons;
    
    SpriteRenderer renderer;

    public void DisplayWinnerIcon(Assets.Scripts.Player winner)
    {
        switch (winner)
        {
            case Assets.Scripts.Player.ONE:
                renderer.sprite = PlayerIcons[0];
                break;

            case Assets.Scripts.Player.TWO:
                renderer.sprite = PlayerIcons[1];
                break;

            case Assets.Scripts.Player.THREE:
                renderer.sprite = PlayerIcons[2];
                break;

            case Assets.Scripts.Player.FOUR:
                renderer.sprite = PlayerIcons[3];
                break;

            default:
                renderer.sprite = DefaultIcon;
                break;
        }
    }

    public void ResetWinnerIcon()
    {
        renderer.sprite = DefaultIcon;
    }

        // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = DefaultIcon;
    }
}
