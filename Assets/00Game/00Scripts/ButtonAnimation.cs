using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonAnimation : MonoBehaviour
{
    Button BTN;
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1);
    private void Awake()
    {
        BTN=gameObject.GetComponent<Button>();
        BTN.onClick.AddListener(Anim);
    }
    void Anim()
    {
        LeanTween.scale(gameObject, upScale,0.1f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }
}
