using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Install_OS_UI : MonoBehaviour
{
    [SerializeField] private Install_OS_UI NextPage;
    [SerializeField] private Button NextButtom;
    [SerializeField] private TextMeshProUGUI LabelOSName, LabelOSDescription;
    [SerializeField] private Image panel;
    [SerializeField] private RawImage Background;
    
    private Server server;
    void Start()
    {
        CameraFollow.LockRotate();
    }

    public void setServer(Server server)
    {
        this.server = server;
        OperatingSystem os = server.serverStatus.os;
        if (LabelOSName != null)
        {
            LabelOSName.text = os.DisplayName;
            LabelOSName.color = os.TextColor;
        }
        if (LabelOSDescription != null)
        {
            LabelOSDescription.text = os.Description;
            LabelOSDescription.color = os.TextColor;
        }
        if (panel != null) panel.color = os.ThirdColor;
        if (NextButtom != null)
        {
            ColorBlock colorBlock = NextButtom.colors;
            colorBlock.normalColor = os.PrimaryColor;
            NextButtom.colors = colorBlock;
        }
        if (Background != null)
        {
            Background.color = os.PrimaryColor;
            Debug.Log("Background color changed");
        }
    }

    public void NextButtomFunc()
    {
        if (NextPage == null)
        {
            CameraFollow.UnlockRotate();
            Destroy(this.gameObject);
            return;
        }
        Install_OS_UI UI = Instantiate(NextPage,Vector3.zero,Quaternion.identity);
        UI.setServer(server);
    }
}
