using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoSingleton<CameraCtrl>
{


    // 震动幅度
    public float shakeLevel = 3f;
    // 震动时间
    public float setShakeTime = 0.2f;
    // 震动的FPS
    public float shakeFps = 45f;
    private bool isshakeCamera = false;
    private float fps;
    private float shakeTime = 0.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    private Camera selfCamera;
    private Rect changeRect;

    [SerializeField]
    private float x_Soomth = 1;

    [SerializeField]
    private float y_Soomth = 1;

    private Platform curP;

    void Start()
    {
        selfCamera = GetComponentInChildren<Camera>();
        changeRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;

    }


    public void Update()
    {

        Shakeing();
        if (GlobalInit.Instance.CurRoleCtrl == null)
        {
            return;
        }
        if (GlobalInit.Instance.CurRoleCtrl.CurPlatform == null)
        {
            return;
        }
        if (GameLevelSceneCtrl.Instance.GameStatus == GameStatus.Playing)
        {
            if (GlobalInit.Instance.CurRoleCtrl.CurPlatform != null)
            {
                //二次插值控制
                //float x1 = GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position.x;
                //float y1 = GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position.y;

                //float x2 = Mathf.Lerp(transform.position.x, x1, 0.01f);
                //float y2 = Mathf.Lerp(transform.position.y, y1, 0.01f);

                //transform.position = new Vector3(x2, y2, -5);

                //float x3 = Mathf.Lerp(transform.position.x, x1, Time.smoothDeltaTime * x2 * 0.01f);
                //float y3 = Mathf.Lerp(transform.position.y, y1, Time.smoothDeltaTime * y2 * 0.01f);

                //transform.position = new Vector3(x3, y3, -5);

                ////普通插值
                //transform.position = Vector3.Lerp(transform.position,
                // GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position, Time.smoothDeltaTime * 1.75f);

                //普通插值 x,y 分开
                float x1 = GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position.x;
                float y1 = GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position.y;

                float x2 = Mathf.Lerp(transform.position.x, x1, Time.deltaTime * x_Soomth);
                float y2 = Mathf.Lerp(transform.position.y, y1, Time.deltaTime * y_Soomth);

                transform.position = new Vector3(x2, y2, transform.position.z);

                //平滑阻尼
                //transform.position = Vector3.SmoothDamp(transform.position, GlobalInit.Instance.CurRoleCtrl.CurPlatform.transform.position, ref velocity, 0.2f);
            }


        }
    }


    public void Shake()
    {
        isshakeCamera = true;
    }

    private void Shakeing()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    changeRect.xMin = 0.0f;
                    changeRect.yMin = 0.0f;
                    selfCamera.rect = changeRect;
                    isshakeCamera = false;
                    shakeTime = setShakeTime;
                    fps = shakeFps;
                    frameTime = 0.03f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;
                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        changeRect.xMin = shakeDelta * (-1.0f + shakeLevel * Random.value);
                        changeRect.yMin = shakeDelta * (-1.0f + shakeLevel * Random.value);
                        selfCamera.rect = changeRect;
                    }
                }
            }
        }
    }


}
