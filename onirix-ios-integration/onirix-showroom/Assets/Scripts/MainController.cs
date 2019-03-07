using UnityEngine;
using Onirix.MobileSDK;
using Onirix.Core.Model;
using Onirix.Core.Service;
using UnityEngine.UI;
using System.Collections;

public class MainController : MonoBehaviour, IDynamicLoadListener
{

    private string _projectToken = "<YOUR_PROJECT_TOKEN_HERE>";
    private string _targetOid = "<YOUT_TARGET_OID_HERE>";

    [SerializeField] private Button _loadTargetButton;
    [SerializeField] private Text _statusText;


    public void OnTargetAssetsDownloaded(Target target)
    {
        _statusText.text = "Content downloaded!";
    }

    public void OnTargetAssetsLoaded(Target target)
    {
        _statusText.text = "Onirix Showroom";
        OnirixMobileManager.Instance.ShowCrosshair();
    }

    public void OnTargetAssetsStartDownloading(Target target)
    {
        _statusText.text = "Contents started download!";
    }

    public void OnTargetAssetsStartLoading(Target target)
    {
        _statusText.text = "Contents started loading!";
    }

    // Use this for initialization
    void Start () {

        OnirixAuthManager.Instance.SetToken(_projectToken);

        // Prevent the screen from switching to energy saving mode
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Disable button until surface will be detected
        _loadTargetButton.interactable = false;

        // Set the load button click handler.
        _loadTargetButton.onClick.AddListener(LoadTarget);


        // Initialize the DynamicLoadManager, which will load our Target.
        OnirixDynamicLoader.Instance.Init(this);

        StartCoroutine
        (
            WaitForAR(ReadyAR)
        );
    }
	

    private void LoadTarget()
    {
        OnirixDynamicLoader.Instance.LoadTarget(_targetOid);
        _statusText.text = "Loading, please wait ...";
        _loadTargetButton.interactable = false;
        OnirixMobileManager.Instance.HideCrosshair();
    }

    // Wait for ARCore / ARKit
    private IEnumerator WaitForAR(System.Action onReady)
    {
        yield return new WaitUntil(() => OnirixMobileManager.Instance.IsReady);
        onReady();
    }

    private void ReadyAR()
    {
        OnirixMobileManager.Instance.StartSurfaceDetection
        (
            () =>
            {
                _statusText.text = "Click on the button to place the content";
                _loadTargetButton.interactable = true;
            },
            () =>
            {
                _statusText.text = "Move around to detect a surface";
                _loadTargetButton.interactable = false;
            }
        );
    }

}
