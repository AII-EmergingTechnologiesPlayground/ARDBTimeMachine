using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AccountButtons : MonoBehaviour
{
    public GameObject EmailInput;
    public GameObject PasswordInput;

    public async void SignIn()
    {
        var email = ((TMPro.TMP_InputField)EmailInput.GetComponent("TMPro.TMP_InputField")).text;
        var pass = ((TMPro.TMP_InputField)PasswordInput.GetComponent("TMPro.TMP_InputField")).text;

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:7030/api/Accounts/Login"))
            {
                request.Headers.TryAddWithoutValidation("accept", "text/plain");

                request.Content = new StringContent($"{{\n  \"email\": \"{email}\",\n  \"passwordHash\": \"{pass}\"\n}}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await httpClient.SendAsync(request);
            }
        }
    }
    public async void Register()
    {
        var email = ((TMPro.TMP_InputField)EmailInput.GetComponent("TMPro.TMP_InputField")).text;
        var pass = ((TMPro.TMP_InputField)PasswordInput.GetComponent("TMPro.TMP_InputField")).text;

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:7030/api/Accounts"))
            {
                request.Headers.TryAddWithoutValidation("accept", "text/plain");

                request.Content = new StringContent($"{{\n  \"email\": \"{email}\",\n  \"passwordHash\": \"{pass}\"\n}}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = await httpClient.SendAsync(request);
            }
        }
    }
}
