using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private Canvas ballCanvas;
    [SerializeField] private Button kickBtn, autoKickBtn;

    void Update()
    {
        SetKickBtnFacingCamera();
    }

    void SetKickBtnFacingCamera()
    {
        Vector3 dir = kickBtn.transform.position - playerCam.transform.position;
        kickBtn.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void SetBallCanvasActive(bool isActive)
    {
        ballCanvas.enabled = isActive;
    }

    public void SetKickBtnActive(bool isActive)
    {
        kickBtn.gameObject.SetActive(isActive);
    }

    public void SetAutoKickBtnActive(bool isActive)
    {
        autoKickBtn.gameObject.SetActive(isActive);
    }

    public void SetKickBtnOnBall(Ball ball)
    {
        var camForward = Vector3.ProjectOnPlane(playerCam.forward, Vector3.up);
        kickBtn.transform.position = ball.transform.position + Vector3.up + camForward*2;
    }

    public void Reset()
    {
        SceneManager.LoadScene("location soccer field", LoadSceneMode.Single);
    }
}