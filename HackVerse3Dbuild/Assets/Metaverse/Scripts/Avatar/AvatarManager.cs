using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class AvatarManager : MonoBehaviourPunCallbacks, IHoverable
{
    public GameObject[] hats;
    public GameObject[] faces;
    public GameObject[] torsos;
    public GameObject head;
    public GameObject torso;
    public GameObject particleffect;
    float hatindex;
    float torsoindex;
    float faceindex;
    public string playerpronouns;
    public string playerrole;
    public string stack;
    public string about;
    public string timezone;


    public TMP_Text username;
    public TMP_Text role;
    public TMP_Text expertise;
    public TMP_Text aboutme;
    public TMP_Text timezonetext;
    public TMP_Dropdown rolerdropdown;
    public TMP_Dropdown skillsdropdown;
    public GameObject infoPanel;
    public TMP_Text wholookinat;
    public TMP_Text whoexpertise;

    public GameObject smileyhead;
    public GameObject AvatarUI;
    public GameObject filterPanel;
    public bool IsSelected => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        InitialiseAvatar();
        infoPanel.SetActive(false);
        filterPanel.SetActive(false);
        AvatarUI.SetActive(true);

    }

    public void InitialiseAvatar()
    {
        //  R = (float)photonView.Owner.CustomProperties["R"];
        //G = (float)photonView.Owner.CustomProperties["G"];
        //B = (float)photonView.Owner.CustomProperties["B"];
        hatindex = (float)photonView.Owner.CustomProperties["hat"];
        torsoindex = (float)photonView.Owner.CustomProperties["torso"];
        faceindex = (float)photonView.Owner.CustomProperties["face"];
        playerpronouns = (string)photonView.Owner.CustomProperties["pronouns"];
        playerrole = (string)photonView.Owner.CustomProperties["role"];
        stack = (string)photonView.Owner.CustomProperties["stack"];
        about = (string)photonView.Owner.CustomProperties["about"];
        timezone = (string)photonView.Owner.CustomProperties["timezone"];
        timezonetext.text = "Timezone: " + timezone;
        username.text = photonView.Owner.NickName + " (" + playerpronouns + ")";
        role.text = "(" + playerrole + ")";
        aboutme.text = about;
        expertise.text = "Skillset:  " + stack;
        SetHat(hatindex);
    }

    public void InfoAssigner(string name, string stack, string playerrole, bool state)
    {
        infoPanel.SetActive(state);
        if (state)
        {
            wholookinat.text = "You are looking at " + name;
            whoexpertise.text = name + " is a " + playerrole + " and " + name + "'s expertise is in " + stack;
        }



    }
    public void SetHat(float hatindex)
    {

        for (int i = 0; i < hats.Length; i++)
        {
            if (i == hatindex)
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
            if (i == faceindex)
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
            if (i == torsoindex)
            {
                torsos[i].SetActive(true);
            }
            else
            {
                torsos[i].SetActive(false);
            }
        }
    }
    public void OnGazeEnter(GazeData data)
    {
        print("you are looking at " + this.gameObject.name);

        if (looker != null)
        {
            looker.GetComponent<AvatarManager>().InfoAssigner(photonView.Owner.NickName, stack, playerrole, true);
        }
    }

    public GameObject[] allplayers;
    int roles;
    int skills;

    public void ClearFilters()
    {
        allplayers = GameObject.FindGameObjectsWithTag("User");
        rolerdropdown.value = 0;
        skillsdropdown.value = 0;
        foreach (GameObject user in allplayers)
        {


            user.GetComponent<PlayerController>().avatarmanager.AvatarUI.gameObject.SetActive(true);

            user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(false);




        }
    }
    public void Filtering(int roles, int skills)
    {
        string ab = rolerdropdown.options[roles].text;
        string cd = skillsdropdown.options[skills].text;

        print(cd.CompareTo("No Filter"));
        print(ab.CompareTo("No Filter"));

        allplayers = GameObject.FindGameObjectsWithTag("User");
        foreach (GameObject user in allplayers)
        {
        
            string a = rolerdropdown.options[roles].text;
            string b = user.GetComponent<PlayerController>().avatarmanager.playerrole;
    
            string c = skillsdropdown.options[skills].text;
            string d = user.GetComponent<PlayerController>().avatarmanager.stack;
           
          
                if (c.CompareTo(d) != 0)
                {
                    user.GetComponent<PlayerController>().avatarmanager.AvatarUI.gameObject.SetActive(false);

                    user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(false);
                }
                else
                {
                    user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(true);
                    user.GetComponent<PlayerController>().avatarmanager.AvatarUI.gameObject.SetActive(true);


                }

           
            
          
        }







        }
        public void SetRole(int value)
        {
            Filtering(value, skillsdropdown.value);
        }
        public void SetSkills(int value)
        {
            Filtering(rolerdropdown.value, value);

        }
        public void SetFilter(int value)
        {

            allplayers = GameObject.FindGameObjectsWithTag("User");
            if (rolerdropdown.options[value].text != "No Filter")
            {


                foreach (GameObject user in allplayers)
                {

                    string a = rolerdropdown.options[value].text;
                    string b = user.GetComponent<PlayerController>().avatarmanager.playerrole;
                    if (a.CompareTo(b) != 0)

                    {
                        user.GetComponent<PlayerController>().avatarmanager.username.gameObject.SetActive(false);
                        user.GetComponent<PlayerController>().avatarmanager.role.gameObject.SetActive(false);
                        user.GetComponent<PlayerController>().avatarmanager.expertise.gameObject.SetActive(false);
                        user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(false);

                    }
                    else
                    {
                        user.GetComponent<PlayerController>().avatarmanager.username.gameObject.SetActive(true);
                        user.GetComponent<PlayerController>().avatarmanager.role.gameObject.SetActive(true);
                        user.GetComponent<PlayerController>().avatarmanager.expertise.gameObject.SetActive(true);
                        user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(true);

                    }


                }
            }
            else
            {

                foreach (GameObject user in allplayers)
                {


                    user.GetComponent<PlayerController>().avatarmanager.particleffect.gameObject.SetActive(false);
                    user.GetComponent<PlayerController>().avatarmanager.username.gameObject.SetActive(true);
                    user.GetComponent<PlayerController>().avatarmanager.role.gameObject.SetActive(true);



                }
            }
        }
        public void OnGazeExit(GazeData data)
        {
            if (looker != null)
            {

                looker.GetComponent<AvatarManager>().InfoAssigner(photonView.Owner.NickName, stack, playerrole, false);
                looker = null;
            }
        }

        public void OnClick(GazeData data)
        {
            print("you said hi");

        }

    public GameObject looker;
    public void SetLooker(GameObject gameObject)
    {
        looker = gameObject;
    }
}
