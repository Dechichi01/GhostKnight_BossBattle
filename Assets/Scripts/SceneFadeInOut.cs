using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;

    GUITexture GUITexture;

    void Awake()
    {
        GUITexture = GetComponent<GUITexture>();
        GUITexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    void Update()
    {
        if (sceneStarting) StartScene();
    }

    void FadeToClear()
    {
        GUITexture.color = Color.Lerp(GUITexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        GUITexture.color = Color.Lerp(GUITexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (GUITexture.color.a <=0.05f)
        {
            GUITexture.color = Color.clear;
            GUITexture.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        GUITexture.enabled = true;
        FadeToBlack();

        if (GUITexture.color.a >= 0.95f) SceneManager.LoadScene(0);
    }

}
