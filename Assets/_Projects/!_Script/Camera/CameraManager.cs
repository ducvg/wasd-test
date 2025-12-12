using System.Collections;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineBrain cmBrain;
    [SerializeField] CinemachineVirtualCamera playerCam, ballCam;

    private Coroutine followPlayerCoroutine;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 69420;
    }

    public async void FollowPlayer()
    {
        if(followPlayerCoroutine != null) StopCoroutine(followPlayerCoroutine);
        
        ballCam.enabled = false;
        playerCam.enabled = true;
        await UniTask.Delay(500, cancellationToken: destroyCancellationToken).SuppressCancellationThrow();
        InputController.SetMovementInputActive(true);
        UIManager.Instance.SetBallCanvasActive(true);
        UIManager.Instance.SetAutoKickBtnActive(true);
    }

    public void FollowBall(Ball followBall)
    {
        ballCam.Follow = followBall.transform;        

        ballCam.enabled = true;
        playerCam.enabled = false;
        InputController.SetMovementInputActive(false);
        UIManager.Instance.SetBallCanvasActive(false);
        UIManager.Instance.SetAutoKickBtnActive(false);
        
        followPlayerCoroutine = StartCoroutine(FollowPlayerFallback());
    }

    private WaitForSeconds wait7s = new(7f);
    private IEnumerator FollowPlayerFallback()
    {
        yield return wait7s;
        FollowPlayer();
    }
}