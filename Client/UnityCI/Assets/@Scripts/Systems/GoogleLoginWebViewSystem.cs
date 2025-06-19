using Google;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class GoogleLoginWebViewSystem
{
    public Action<string> OnGetGoogleAccount;

    public void ShowUrl()
    {


    }

    public void SignIn()
    {
        GoogleSignIn.Configuration = new GoogleSignInConfiguration
        {
            RequestEmail = true,
            RequestProfile = true,
            RequestIdToken = true,
            RequestAuthCode = true,
            WebClientId = "650096840978-m9bh5a2qtu9btm5k1v97vjdg7b86srhq.apps.googleusercontent.com",
#if UNITY_EDITOR || UNITY_STANDALONE
            ClientSecret = "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"
#endif
            //androidclient id
            //650096840978-vqp24ti8aov35rv0rcr0t9khviuld7e9.apps.googleusercontent.com
        };

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished, TaskScheduler.FromCurrentSynchronizationContext());
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.Log("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
        }
        else
        {
            Debug.Log("Welcome: " + task.Result.DisplayName + "!");
            Debug.Log("Welcome: " + task.Result.UserId + "!");

            if (OnGetGoogleAccount == null)
            {
                Debug.Log("null");
            }

            OnGetGoogleAccount.Invoke(task.Result.UserId);
        }
    }
}
