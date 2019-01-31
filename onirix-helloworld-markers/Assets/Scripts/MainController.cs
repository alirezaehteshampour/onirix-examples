using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Onirix.MobileSDK;
using Onirix.Core.Model;

public class MainController : MonoBehaviour, IDynamicLoadListener
{

    // Token that allows us to access the Onirix project from the SDK.
    [SerializeField] private string _projectToken;

    // Oid of the project that we want to use.
    [SerializeField] private string _projectOid;

    // Label for messages that will show the status of the application.
    [SerializeField] private Text _statusText;

    /* Instance of the OnirixMobileManager. It is in charge of managing everything
     * that has to do with the AR in our app. */
    private OnirixMobileManager _onirixMobileManager;

    // A public getter property to get that reference.
    public OnirixMobileManager MobileManager
    {
        get
        {
            if (!_onirixMobileManager)
        {
            _onirixMobileManager = gameObject.GetComponent<OnirixMobileManager>();
        }
        return _onirixMobileManager;
        }
    }

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
        DynamicLoadManager.Instance.Init(MobileManager, _projectToken, this);

        StartCoroutine
            (
                WaitForAR
                (
                    () =>
                    {
                        // Start markers detection
                        MobileManager.StartDetection(_projectOid, _projectToken,
                            (detectedTarget) =>
                            {
                                // Hide the default crosshair
                                MobileManager.HideCrosshair();

                                // When a marker is detected. Let's load it's assets .
                                _statusText.text = "Loading target {detectedTarget}, please wait ...";
                                DynamicLoadManager.Instance.LoadTarget(detectedTarget);
                            }
                        );

                    }
                )
            );
    }

    // Wait until ARKit / ARCore are initialized.
    private IEnumerator WaitForAR(System.Action onReady)
    {
        yield return new WaitUntil(() => MobileManager.IsReady);
        onReady();
    }

}
