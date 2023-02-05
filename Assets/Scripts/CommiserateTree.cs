using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommiserateTree : MonoBehaviour
{
    [SerializeField] RawImage darkOverlay;
    private Color32 overlayColor;
    Transform background;
    Transform roots;

    // Start is called before the first frame update
    void Start()
    {
        background = gameObject.transform.GetChild(0);
        roots = gameObject.transform.GetChild(1);

        background.transform.position += new Vector3(0, 800, 0);
        roots.transform.position += new Vector3(0, -1080, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
            easeTreeIn();
        
    }

    void easeTreeIn() {

        LeanTween.move(background.gameObject, background.position + new Vector3(0f, -800f, 0f), 2.0f).setEase( LeanTweenType.easeOutSine );
        LeanTween.move(roots.gameObject, roots.position + new Vector3(0f, 1080f, 0f), 2.0f).setEase( LeanTweenType.easeOutSine );

    }

    private void onCommiserateStart(Emotion e)
    {
        darkOverlay.gameObject.SetActive(true);
        
    }

    private void setOverlayActive(bool active)
    {
        if (active)
        {
            darkOverlay.color = Color.clear;
            darkOverlay.gameObject.SetActive(true);
            LeanTween.alpha(darkOverlay.gameObject, 0.4f, 2f)
                .setEaseOutQuad();
        }
        else
        {
            LeanTween.alpha(darkOverlay.gameObject, 0.4f, 2f)
                .setEaseOutQuad()
                .setOnComplete(e => darkOverlay.gameObject.SetActive(false));
        }
    }
}
