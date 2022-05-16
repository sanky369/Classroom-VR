using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

//
//This script connects to PHOTON servers and creates a room if there is none, then automatically joins
//
namespace Networking.Pun2
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        bool triesToConnectToMaster = false;
        bool triesToConnectToRoom = false;

        //public bool isStudent;

        private void Start()
        {
            //PhotonNetwork.AutomaticallySyncScene = true;
        }
        private void Update()
        {
            if (!PhotonNetwork.IsConnected && !triesToConnectToMaster)
            {
                ConnectToMaster();
            }
            if (PhotonNetwork.IsConnected && !triesToConnectToMaster && !triesToConnectToRoom)
            {
                StartCoroutine(WaitFrameAndConnect());
            }
        }

        //public void SelectGenderAndConnect(string gender)
        //{
            //PlayerPrefs.SetString("gender", gender);
            //ConnectToMaster();
        //}

        public void SaveStudentName(Text name)
        {
            PlayerPrefs.SetString("name", name.text);
            PhotonNetwork.NickName = name.text;
        }

        public void ConnectToMaster()
        {
            PhotonNetwork.OfflineMode = false; //true would "fake" an online connection
            PhotonNetwork.NickName = "PlayerName"; //we can use a input to change this 
            PhotonNetwork.AutomaticallySyncScene = true; //To call PhotonNetwork.LoadLevel()
            PhotonNetwork.GameVersion = "v1"; //only people with the same game version can play together

            triesToConnectToMaster = true;
            //PhotonNetwork.ConnectToMaster(ip, port, appid); //manual connection
            PhotonNetwork.ConnectUsingSettings(); //automatic connection based on the config file
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            triesToConnectToMaster = false;
            triesToConnectToRoom = false;
            Debug.Log(cause);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            triesToConnectToMaster = false;
            Debug.Log("Connected to master!");
        }

        IEnumerator WaitFrameAndConnect()
        {
            triesToConnectToRoom = true;
            yield return new WaitForEndOfFrame();
            Debug.Log("Connecting");
            //ConnectToRoom();
        }

        public void OnRoom1Clicked()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", "room1" } };
            //PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
            //PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties, 0);
            ConnectToRoom("room1");
        }

        public void OnRoom2Clicked()
        {
            if (!PhotonNetwork.IsConnected)
                return; 
            
            //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", "room2" } };
            //PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
            //PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties, 0);
            ConnectToRoom("room2");
        }

        public void OnRoom3Clicked()
        {
            if (!PhotonNetwork.IsConnected)
                return; 
            
            //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", "room3" } };
            //PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
            //PhotonNetwork.JoinRandomOrCreateRoom(expectedCustomRoomProperties, 0);
            ConnectToRoom("room3");
        }

        public void ConnectToRoom(string rName)
        {
            if (!PhotonNetwork.IsConnected)
                return;

            triesToConnectToRoom = true;
            //PhotonNetwork.CreateRoom("name"); //Create a specific room - Callback OnCreateRoomFailed
            //PhotonNetwork.JoinRoom("name"); //Join a specific room - Callback OnJoinRoomFailed
            //string randomRoomName = "Room" + Random.Range(0, 10000);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 20;
            string[] roomPropsInLobby = { "map" };

            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", rName } };

            roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
            roomOptions.CustomRoomProperties = customRoomProperties;

            //PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
            PhotonNetwork.JoinOrCreateRoom(rName, roomOptions, TypedLobby.Default);
            //PhotonNetwork.JoinRandomRoom(); // Join a random room - Callback OnJoinRandomRoomFailed
        }

        public override void OnJoinedRoom()
        {
            //Go to next scene after joining the room
            base.OnJoinedRoom();
            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name + " Region: " + PhotonNetwork.CloudRegion);

            //SceneManager.LoadScene("Classroom"); //go to the room scene
            //SceneManager.LoadScene("Classroom_New"); //go to the room scene
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
            {
                object mapType;
                if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("map", out mapType))
                {
                    if ((string)mapType == "room1")
                    {
                        PhotonNetwork.LoadLevel("Classroom_New");
                        //SceneManager.LoadScene("Classroom_New");
                    }else if((string)mapType == "room2")
                    {
                        PhotonNetwork.LoadLevel("Classroom_New_2");
                        //SceneManager.LoadScene("Classroom_New_2");
                    }
                    else if((string)mapType == "room3")
                    {
                        PhotonNetwork.LoadLevel("Classroom_New_3");
                        //SceneManager.LoadScene("Classroom_New_3");
                    }
                }

                
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            //no room available
            //create a room (null as a name means "does not matter")
            //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 15 });
            ConnectToRoom("room1");
        }
    }
}