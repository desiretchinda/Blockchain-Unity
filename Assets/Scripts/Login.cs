using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class Login : MonoBehaviour
{

    public Button btnConnect;
    public Button btnGetStart;
    public Button btnSkip;

    public GameObject goConnect;
    public GameObject goGetStart;

    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    private void Start()
    {
        if (btnConnect)
            btnConnect.onClick.AddListener(OnLogin);

        if (btnGetStart)
            btnGetStart.onClick.AddListener(OnGetStart);

        if (btnSkip)
            btnSkip.onClick.AddListener(OnGetStart);

        if (goConnect)
            goConnect.SetActive(true);

        if (goGetStart)
            goGetStart.SetActive(false);
    }

    public void OnLogin()
    {
        if (goConnect)
            goConnect.SetActive(false);

        if (goGetStart)
            goGetStart.SetActive(true);

        if (btnSkip)
            btnSkip.gameObject.SetActive(true);

        if (btnGetStart)
            btnGetStart.interactable = false;
        Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");
        if (btnGetStart)
            btnGetStart.interactable = true;
    }

    public void OnGetStart()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
#endif