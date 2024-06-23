using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class sc : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject enemies;
    void Start() {
        if (enemies == null) {
            Debug.Log("BUG");
        }



        StartCoroutine(DisplayLines());

    }
    IEnumerator DisplayLines() {
        string[] lines =
       {
            "Chào mừng đến The land of AH !!!",
            "Bắt đầu luyện tập !!!",
        };
        for (int i = 0; i < lines.Length; i++) {
            textMeshProUGUI.text = lines[i]; // Cập nhật văn bản của TextMeshProUGUI
            yield return new WaitForSeconds(3.0f); // Đợi 2 giây trước khi hiển thị dòng tiếp theo
        }

        StartCoroutine(CheckMove());
    }
    // Update is called once per frame

    void Update() {
        // Kiểm tra nếu phím Shift được nhấn

    }

    IEnumerator CheckMove() {
        textMeshProUGUI.text = "Ấn A / D hoặc  < / > để di chuyển";
        while (!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f); // Ví dụ delay 2 giây
        StartCoroutine(CheckSurf());
    }
    IEnumerator CheckSurf() {
        textMeshProUGUI.text = "Ấn Shift để lướt";
        while (!(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))) {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f); // Ví dụ delay 2 giây
        StartCoroutine(checkRoll());
    }

    IEnumerator checkRoll() {
        textMeshProUGUI.text = "Ấn F để lộn";
        while (!(Input.GetKeyDown(KeyCode.F))) {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f); // Ví dụ delay 2 giây
        StartCoroutine(checkJump());
    }

    IEnumerator checkJump() {
        textMeshProUGUI.text = "Ấn Space để nhảy";
        while (!(Input.GetKeyDown(KeyCode.Space))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        StartCoroutine(checkDefend());
    }

    IEnumerator checkDefend() {
        textMeshProUGUI.text = "Giữ S để dùng lá chắn";
        while (!(Input.GetKeyDown(KeyCode.S))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        enemies.SetActive(true);
        StartCoroutine(checkSkill());
    }
    IEnumerator checkSkill() {
        textMeshProUGUI.text = "Ấn Q W E R để tấn công quái vật";
        while (true) {
            if (!enemies.activeSelf) {
                textMeshProUGUI.text = "Tìm cách cửa để qua màn mới!!";
                yield return null;
            }
        }
    }
}
