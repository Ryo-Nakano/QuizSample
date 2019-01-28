using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AspectRatioManager : MonoBehaviour
{

    public float x_aspect = 1242f;
    public float y_aspect = 2208f;
    public CanvasScaler[] canvasScaler = new CanvasScaler[1];
    public Camera mainCamera;
    public Camera[] subCameras = new Camera[1];

    void Awake()
    {
        //Cameraのアスペクト比を設定する
        Camera camera = mainCamera.GetComponent<Camera>();
        Rect rect = calcAspect(x_aspect, y_aspect);
        camera.rect = rect;

        //Canvasのアスペクト比を設定する
        for (int i = 0; i < canvasScaler.Length; i++)
        {
            canvasScaler[i].matchWidthOrHeight = CheckScreenRatio();
        }

        foreach (Camera subCamera in subCameras)
        {
            //subCamera.rect = calcCameraViewRect(subCamera.rect);
        }

    }

    private Rect calcAspect(float width, float height)
    {
        float target_aspect = width / height;
        float window_aspect = (float)Screen.width / (float)Screen.height;
        float scale_height = window_aspect / target_aspect;
        Rect rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        if (1.0f > scale_height)
        {
            rect.x = 0;
            rect.y = (1.0f - scale_height) / 2.0f;
            rect.width = 1.0f;
            rect.height = scale_height;
        }
        else
        {
            float scale_width = 1.0f / scale_height;
            rect.x = (1.0f - scale_width) / 2.0f;
            rect.y = 0.0f;
            rect.width = scale_width;
            rect.height = 1.0f;
        }
        return rect;
    }


    /// <summary>
    /// Checks the screen ratio. Return 0 when width, 1 when height
    /// </summary>
    private int CheckScreenRatio()
    {
        if (Screen.width * y_aspect / x_aspect < Screen.height)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private Rect calcCameraViewRect(Rect currentRect)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float ratio = y_aspect / x_aspect;
        if (CheckScreenRatio() == 0)
        {
            screenHeight = screenWidth * ratio;
        }
        else
        {
            screenWidth = screenHeight / ratio;
        }
        Rect pixelRectOfTargetScreen = new Rect(currentRect.x * screenWidth, currentRect.y * screenHeight, currentRect.width * screenWidth, currentRect.height * screenHeight);
        Vector2 targetScreenDiff = new Vector2((Screen.width - screenWidth) / 2, (Screen.height - screenHeight) / 2);
        Rect pixelRectOfCurrentScreen = new Rect(pixelRectOfTargetScreen.x + targetScreenDiff.x, pixelRectOfTargetScreen.y + targetScreenDiff.y, pixelRectOfTargetScreen.width, pixelRectOfTargetScreen.height);
        Rect rectOfCurrentScreen = new Rect(pixelRectOfCurrentScreen.x / Screen.width, pixelRectOfCurrentScreen.y / Screen.height, pixelRectOfCurrentScreen.width / Screen.width, pixelRectOfCurrentScreen.height / Screen.height);
        return rectOfCurrentScreen;
    }
}