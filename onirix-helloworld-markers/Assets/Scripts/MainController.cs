using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Onirix.MobileSDK;
using Onirix.Core.Model;

public class MainController : MonoBehaviour, IDynamicLoadListener
{
    // Label for messages that will show the status of the application.
    [SerializeField] private Text _statusText;

    // Fires when all target's assets are downloaded from Onirix Studio
    public void OnTargetAssetsDownloaded(Target target)
    {
        _statusText.text = "Assets downloaded";
    }

    // Fires when all target's assets are loaded in app.
    public void OnTargetAssetsLoaded(Target target)
    {
        _statusText.text = "Assets loaded";
    }

    // Fires when the target's assets download start
    public void OnTargetAssetsStartDownloading(Target target)
    {
        _statusText.text = "Start downloading assets";
    }

    // Fires when target's assets load start
    public void OnTargetAssetsStartLoading(Target target)
    {
        _statusText.text = "Start loading assets";
    }

    // Use this for initialization
    void Start ()
    {

        // Prevent the screen from switching to energy saving mode
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Initialize the DynamicLoadManager, which will load our Target.
        OnirixDynamicLoader.Instance.Init(this);

        StartCoroutine
        (
            WaitForAR
            (
                () =>
                {
                    // The OnirixMobileManager is in charge of managing everything related to AR tasks in our app.

                    // Start markers detection
                    OnirixMobileManager.Instance.StartMarkerDetection
                    (
                        (detectedTarget) =>
                        {
                            // Hide the default crosshair
                            OnirixMobileManager.Instance.HideCrosshair();

                            // When a marker is detected. Let's load it's assets .
                            _statusText.text = "Loading target {detectedTarget}, please wait ...";
                            OnirixDynamicLoader.Instance.LoadTarget(detectedTarget);
                        }
                    );

                }
            )
        );
    }

    // Wait until ARKit / ARCore are initialized.
    private IEnumerator WaitForAR(System.Action onReady)
    {
        yield return new WaitUntil(() => OnirixMobileManager.Instance.IsReady);
        onReady();
    }

}
