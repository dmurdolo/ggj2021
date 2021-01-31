using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject Panel;

    void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += EndReached;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<VideoPlayer>().Pause();
            Panel.GetComponent<FadeCanvasGroup>().StartFadeOut();
            gameObject.SetActive(false);
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Panel.GetComponent<FadeCanvasGroup>().StartFadeOut();
        gameObject.SetActive(false);
    }
}
