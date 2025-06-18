using UnityEngine;

public class SampleScene : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Systems.GoogleLoginWebView.OnGetGoogleAccount += (account) =>
        {
            _text.text = account;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_GoogleLogin()
    {

        Systems.GoogleLoginWebView.SignIn();
    }
}
