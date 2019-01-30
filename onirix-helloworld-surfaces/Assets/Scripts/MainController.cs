using System.Collections;
using System.Collections.Generic;
using Onirix.Core.Model;
using Onirix.MobileSDK;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour, IDynamicLoadListener {

    [SerializeField] private string _projectToken;
    [SerializeField] private string _targetOid;
    [SerializeField] private Button _loadTargetButton;
    [SerializeField] private Text _statusText;

    private OnirixMobileManager _onirixMobileManager;

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

    public void OnTargetAssetsDownloaded(Target target)
    {
        _statusText.text = "Target assets downloaded!";
    }

    public void OnTargetAssetsLoaded(Target target)
    {
        _statusText.text = "Target assets loaded!";
        MobileManager.ShowCrosshair();
    }

    public void OnTargetAssetsStartDownloading(Target target)
    {
        _statusText.text = "Target assets started download!";
    }

    public void OnTargetAssetsStartLoading(Target target)
    {
        _statusText.text = "Target assets started loading!";
    }

    // Use this for initialization
    void Start () {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        DynamicLoadManager.Instance.Init(MobileManager, _projectToken, this);

        _loadTargetButton.interactable = false;

        StartCoroutine
        (
            WaitForAR
            (
                () =>
                {
                    MobileManager.StartSurfaceTarget
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
            DynamicLoadManager.Instance.LoadTarget(_targetOid);
            _statusText.text = "Loading target, please wait ...";
            _loadTargetButton.interactable = false;
            MobileManager.HideCrosshair();
        });

		
	}

    private IEnumerator WaitForAR(System.Action onReady)
    {
        yield return new WaitUntil(() => MobileManager.IsReady);
        onReady();
    }

}
