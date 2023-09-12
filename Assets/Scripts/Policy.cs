using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy : MonoBehaviour
{
    private string policyKey = "policy";
    void Start()
    {
        var accepted = PlayerPrefs.GetInt(policyKey, 0) == 1;
        if(accepted)
            return;

        SimpleGDPR.ShowDialog( new TermsOfServiceDialog().
            SetTermsOfServiceLink( "https://listinfoworld.blogspot.com/2023/01/galaxy-strike-space-shooter-terms-and.html" ).
            SetPrivacyPolicyLink( "https://listinfoworld.blogspot.com/2023/01/galaxy-shooter-privacy-policy.html" ),
            OnMenuClosed);
    }

    private void OnMenuClosed()
    {
        Debug.LogWarning("Policy Accepted");
        PlayerPrefs.SetInt(policyKey, 1);
    }
}
