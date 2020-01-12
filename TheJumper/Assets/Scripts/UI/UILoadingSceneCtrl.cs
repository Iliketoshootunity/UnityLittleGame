using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILoadingSceneCtrl : UISceneViewBase {

    [SerializeField]
    private Slider  sliderProgress;
    [SerializeField]
    private Text labProgress;
    [SerializeField]
    private Image sprFrame;

    public void SetProgressValue(float process)
    {
        if (sprFrame == null || labProgress == null || sliderProgress == null) return;
        sliderProgress.value = process;
        labProgress.text = string.Format("{0}%", process * 100);
        sprFrame.transform.localPosition = new Vector3(1000 * process, 0, 0);
    }


}
