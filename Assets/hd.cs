using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class hd : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject enemies;
    private Damageable game;
    public Damageable player;
    void Start()
    {
        if(enemies == null) {
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
            yield return new WaitForSeconds(2.0f); // Đợi 2 giây trước khi hiển thị dòng tiếp theo
        }

        StartCoroutine(CheckMove());
    }
    // Update is called once per frame

    void Update() {
        if (enemies != null) {
            game = enemies.GetComponent<Damageable>();
            if(game.Health <= 0 ) {
                textMeshProUGUI.text = "Tìm cánh cửa để qua màn mới!!";
            } 
        }
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
        StartCoroutine(checkHeal());
    }

    IEnumerator checkHeal() {
        textMeshProUGUI.text = "Tìm bình máu để hồi phục";
        while (player.Health <= 50) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        enemies.SetActive(true);
        StartCoroutine(checkQ());
    }
    IEnumerator checkQ() {
        textMeshProUGUI.text = "Dùng Q để tấn công cận chiến";
        while (!(Input.GetKeyDown(KeyCode.Q))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        enemies.SetActive(true);
        StartCoroutine(checkW());
    }

    IEnumerator checkW() {
        textMeshProUGUI.text = "Dùng W để tấn công tầm xa";
        while (!(Input.GetKeyDown(KeyCode.W))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        enemies.SetActive(true);
        StartCoroutine(checkE());
    }

    IEnumerator checkE() {
        textMeshProUGUI.text = "Dùng E để tấn công diện rộng";
        while (!(Input.GetKeyDown(KeyCode.E))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
        enemies.SetActive(true);
        StartCoroutine(checkR());
    }

    IEnumerator checkR() {
        textMeshProUGUI.text = "Chiêu R có sát thương lớn";
        while (!(Input.GetKeyDown(KeyCode.R))) {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

    }
}
