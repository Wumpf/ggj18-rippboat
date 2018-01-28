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

        float fadeInDuration = 2F;
        StartCoroutine(FadeIn(fadeInDuration));
    }

    public void ResetWinnerIcon()
    {
        renderer.sprite = DefaultIcon;

        float fadeInDuration = 2F;
        StartCoroutine(FadeIn(fadeInDuration));
    }

        // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = DefaultIcon;
    }

    IEnumerator FadeIn(float time)
    {
        float countdown = time;
        Color c = renderer.material.color;

        while (time > 0F)
        {
            renderer.material.color = new Color(c.r, c.g, c.b, 1F - countdown / time);
            countdown -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        renderer.material.color = new Color(c.r, c.g, c.b, 1F);
    }
}
