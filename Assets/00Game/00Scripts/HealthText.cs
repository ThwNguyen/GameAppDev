using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{

    public Vector3 moveSpeed;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;


    public float timeToFade = 1f;
    private float timeElapsed;
    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = new Vector3(0, 75, 0);
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (timeElapsed / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
