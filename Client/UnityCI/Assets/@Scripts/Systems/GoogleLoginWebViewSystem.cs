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
            WebClientId = "550559090082-5fchrj8tj3arltl2ktv615hla7f3veat.apps.googleusercontent.com",
#if UNITY_EDITOR || UNITY_STANDALONE
            ClientSecret = "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"
#endif
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
