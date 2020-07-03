
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCont : MonoBehaviour
{
    float time;
    public Text[] panelText = new Text[3];
    public Image[] sceneImage = new Image[3];
    public Image[] panel = new Image[4];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AlphaCoroutine", 0);
        StartCoroutine("SendLobbyCoroutine");
    }
    IEnumerator AlphaCoroutine(int a)
    {
        yield return new WaitForSeconds(time);
        sceneImage[a].color = new Color(0, 0, 0, sceneImage[a].color.a + 0.01f);
        sceneImage[a+1].color = new Color(255, 255, 255, sceneImage[a+1].color.a + 0.01f);
        if (sceneImage[a].color.a < 255)
        StartCoroutine("AlphaCoroutine", a);
    }

    IEnumerator SendLobbyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
