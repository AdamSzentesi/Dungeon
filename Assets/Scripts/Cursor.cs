using System.Collections;
using UnityEngine;

public class Cursor : Tileable
{
    public float PulseFrequency = 1.0f;

    private Coroutine _PulsateCoroutine;

    private void Start()
    {
        _PulsateCoroutine = StartCoroutine(Pulsate());
    }

    private IEnumerator Pulsate()
    {
        Color spriteColor = TileSpriteRenderer.color;
        spriteColor.a = 1.0f;
        TileSpriteRenderer.color = spriteColor;

        while (true)
        {
            while (spriteColor.a >= 0.5f)
            {
                spriteColor.a -= Time.deltaTime * PulseFrequency;
                TileSpriteRenderer.color = spriteColor;
                yield return null;
            }
            while (spriteColor.a <= 1.0f)
            {
                spriteColor.a += Time.deltaTime * PulseFrequency;
                TileSpriteRenderer.color = spriteColor;
                yield return null;
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(_PulsateCoroutine);
    }

}
