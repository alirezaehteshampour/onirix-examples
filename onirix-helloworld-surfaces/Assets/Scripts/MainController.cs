using System.Collections;
using System.Collections.Generic;
using Onirix.Core.Model;
using Onirix.MobileSDK;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour, IDynamicLoadListener 
{
    private const string TargetOidPlaceholder = "<TARGET_OID_HERE>";

    [SerializeField] private string _targetOid = TargetOidPlaceholder;
    [SerializeField] private Button _loadTargetButton;
    [SerializeField] private Text   _statusText;

    public string TargetOid { get; private set; }

    public void OnTargetAssetsDownloaded(Target target)
    {
        _statusText.text = "Target assets downloaded!";
    }

    public void OnTargetAssetsLoaded(Target target)
    {
        _statusText.text = "Target assets loaded!";
        OnirixMobileManager.Instance.ShowCrosshair();
    }

    public void OnTargetAssetsStartDownloading(Target target)
    {
        _statusText.text = "Target assets started download!";
    }

    public void OnTargetAssetsStartLoading(Target target)
    {
        _statusText.text = "Target assets started loading!";
    }

    void Awake ()
    {
        TargetOid = (_targetOid.Equals(TargetOidPlaceholder) ? null : _targetOid);
    }

    // Use this for initialization
    void Start () 
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        OnirixDynamicLoader.Instance.Init(this);

        _loadTargetButton.interactable = false;

        StartCoroutine
        (
            WaitForAR
            (
                () =>
                {
                    OnirixMobileManager.Instance.StartSurfaceDetection
                    (
                        () =>
                        {
                            _statusText.text = "Click on the button to place the target";
                            _loadTargetButton.interactable = true;
                        },
                        () =>
                        {
                            _statusText.text = "Move around to detect a surface";
                            _loadTargetButton.interactable = false;
                        }
                    );
                }
            )
        );

        _loadTargetButton.onClick.AddListener(() =>
        {
            OnirixDynamicLoader.Instance.LoadTarget(TargetOid);
            _statusText.text = "Loading target, please wait ...";
            _loadTargetButton.interactable = false;
            OnirixMobileManager.Instance.HideCrosshair();
        });

		
	}

    private IEnumerator WaitForAR(System.Action onReady)
    {
        yield return new WaitUntil(() => OnirixMobileManager.Instance.IsReady);
        onReady();
    }

}
