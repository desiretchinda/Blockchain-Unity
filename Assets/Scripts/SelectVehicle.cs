using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectVehicle : MonoBehaviour
{

    public Button btnNext;

    public Button btnPrev;

    public bool keyDown;

    bool isPressNext, isPressPrev, isPressConfirm;

    public RectTransform rectTransform;

    int VerticalMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (btnNext)
            btnNext.onClick.AddListener(OnBtnNext);

        if (btnPrev)
            btnPrev.onClick.AddListener(OnBtnPrev);

        isPressNext = isPressPrev = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressNext)
        {
            rectTransform.position += new Vector3(2, 0, 0);
            Debug.Log("Next");
        }
        
        if (isPressPrev)
        {
            rectTransform.position -= new Vector3(2, 0, 0);
            Debug.Log("Prev");
        }

    }

    public void OnBtnNext()
    {
        rectTransform.position -= new Vector3(2, 0, 0);
    }

    public void OnBtnPrev()
    {
        rectTransform.position += new Vector3(2, 0, 0);
    }

    public void onPressNext()
    {
        isPressNext = true;
        Debug.Log("Press up");
    }

    public void onReleaseNext()
    {
        isPressNext = false;
        Debug.Log("Release up");
    }

    public void onPressPrev()
    {
        isPressPrev = true;
    }

    public void onReleasePrev()
    {
        isPressPrev = false;
    }
}
