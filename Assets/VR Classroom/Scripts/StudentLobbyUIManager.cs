using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Pun2;

public class StudentLobbyUIManager : MonoBehaviour
{
    public NetworkManager networkManager;
    public GameObject instructionText, buttonPanel, roomPanel;

    public void SetAsBoy()
    {
        //networkManager.SelectGenderAndConnect("boy");
        PlayerPrefs.SetString("gender", "boy");
        buttonPanel.SetActive(false);
        instructionText.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void SetAsGirl()
    {
        //networkManager.SelectGenderAndConnect("girl");
        PlayerPrefs.SetString("gender", "girl");
        buttonPanel.SetActive(false);
        instructionText.SetActive(false);
        roomPanel.SetActive(true);
    }
}
