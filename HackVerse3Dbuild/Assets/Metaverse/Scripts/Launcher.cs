using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using TMPro;
using Photon.Realtime;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_Text debug_text;
    public TMP_InputField roomNameIF;
    public TMP_InputField userNameIF;
    public TMP_InputField pronounsIF;
    public TMP_InputField aboutIF;
    public TMP_InputField timezoneIF;

    public TMP_Dropdown stackDropdown;
    public TMP_Dropdown roleDropdown;
    public Transform roomListContent;
    public GameObject roomlistPrefab;
    public static Launcher Instance;
    public Transform playerListContent;
    public GameObject playerListItemPrefab;
    public GameObject startGameButton;

    public Slider hatSlider;
    public Slider faceslider;
    public Slider torsoslider;

      public GameObject[] hats;
    public GameObject[] torsos;
    public GameObject[] faces;
    public GameObject head;
    public GameObject face;
    public GameObject torso;
    public GameObject avatarPlaceholder;

    public TMP_Text panelText;

    private PhotonHashtable hash = new PhotonHashtable();
    public bool isUsername;
    // Start is called before the first frame update
    void Start()
    {
        isUsername = false;
        debug_text.text = "Connecting to Master";
        PhotonNetwork.ConnectUsingSettings();
        SetAvatarinMenu();
        avatarPlaceholder.SetActive(false);
    }
    void Awake()
    {
        Instance = this;
    }

    public override void OnConnectedToMaster()
    {
        if (!isUsername)
        {
            MenuManager.Instance.OpenMenu("Username");
            avatarPlaceholder.SetActive(true);

        }
        else
        {
            MenuManager.Instance.OpenMenu("Title");
        }
        debug_text.text = "Connected to Master";
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;

    }

  
    public void SetAvatarinMenu()
    {
        

        for (int i = 0; i < hats.Length; i++)
        {
            if (i == hatSlider.value)
            {
                hats[i].SetActive(true);
            }
            else
            {
                hats[i].SetActive(false);
            }
        }
        for (int i = 0; i < faces.Length; i++)
        {
            if (i == faceslider.value)
            {
                faces[i].SetActive(true);
            }
            else
            {
                faces[i].SetActive(false);
            }
        }
        for (int i = 0; i < torsos.Length; i++)
        {
            if (i == torsoslider.value)
            {
                torsos[i].SetActive(true);
            }
            else
            {
                torsos[i].SetActive(false);
            }
        }

    }
    void SetAvatarHash()
    {
        hash["hat"] = hatSlider.value;
        hash["face"] = faceslider.value;
        hash["torso"] = torsoslider.value;
        hash["pronouns"] = pronounsIF.text;
        hash["role"] = roleDropdown.options[roleDropdown.value].text.ToString();
        hash["about"] = aboutIF.text;
        hash["stack"] = stackDropdown.options[stackDropdown.value].text.ToString();
        hash["timezone"] = timezoneIF.text;


        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
   
    public override void OnJoinedLobby()
    {
        debug_text.text = "Joined Lobby";

    }
    public void CreateUsername()
    {
        if (string.IsNullOrEmpty(userNameIF.text))
        {
            return;
        }
        isUsername = true;
        PhotonNetwork.NickName = userNameIF.text;
        MenuManager.Instance.OpenMenu("Title");
        SetAvatarHash();
        avatarPlaceholder.SetActive(false);
    }

    public void StartScene()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameIF.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameIF.text);
        MenuManager.Instance.OpenMenu("Loading");

    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room");
        debug_text.text = "you joined " + PhotonNetwork.CurrentRoom.Name;
        AssignPlayerListItems();
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void AssignPlayerListItems()
    {
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        AssignPlayerListItems();

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("Error");
        debug_text.text = "Error Cant Create Room";

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");

    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomlistPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    private void Update()
    {
        if(avatarPlaceholder.activeInHierarchy)
        {
            SetAvatarinMenu();
        }
    }
}
