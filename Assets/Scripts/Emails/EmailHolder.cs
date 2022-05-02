//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailHolder : MonoBehaviour
{

    ///////// PUBLIC /////////
    //Scriptable Object
    public Email_Scriptable holder;

    //UI
    public Image backgound;
    public Sprite backgroundWhenSelected;
    public Sprite backgroundWhenUnselected;

    public Image logo;
    public Text sender;
    public Text senderAddress;
    public Text tittle;
    public Text content;
    public Text timeHour;
    public Text timeMin;

    // Set by ArchiveTabManager if created by it.
    [HideInInspector]
    public bool archiveEmail = false;
    [HideInInspector]
    public ArchiveTabManager archiveTabManager;
    ///////// PUBLIC /////////

    ///////// PRIVATE /////////
    private Main mainScript;
    private EmailButtons answerButton;
    private EmailButtons ignoreButton;

    private SoundsHolder _audioScript;
    ///////// PRIVATE /////////


    private void Start()
    {
        //GetAudio Source
        GameObject speakers = GameObject.FindGameObjectWithTag("Speakers");
        _audioScript = speakers.GetComponent<SoundsHolder>();

        #region Email Related

        if (holder.logo)
        {
            logo.sprite = holder.logo;
            logo.enabled = true;
        }
        sender.text = holder.sender;
        StartTittle();
        StartContent();
        #endregion

        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();
        
        #endregion

        #region Buttons Related
        
        //Answer
        GameObject answer = GameObject.Find("=Answer=");
        answerButton = answer.GetComponent<EmailButtons>();

        //Refuse
        GameObject refuse = GameObject.Find("=Refuse=");
        ignoreButton = refuse.GetComponent<EmailButtons>();

        #endregion
    }

    private void StartTittle()
    {
        #region Simulation of OutLook Behavior

        if (holder.tittle.Length > 24)
        {
            //New Tittle Empty
            string NewTittle = "";

            for (int i = 0; i < holder.tittle.Length; i++)
            {

                if (i < 21)
                {
                    NewTittle += holder.tittle[i];
                }
                else
                {
                    NewTittle += "...";
                    tittle.text = NewTittle;
                    break;
                }
            }
        }
        else
        {
            tittle.text = holder.tittle;
        }

        #endregion
    }

    private void StartContent()
    {
        #region Simulation of OutLook Behavior

        if (holder.content.Length > 32)
        {
            //New Content Empty
            string NewContent = "";

            for (int i = 0; i < holder.content.Length; i++)
            {

                if (i < 21)
                {
                    NewContent += holder.content[i];
                }
                else
                {
                    NewContent += "...";
                    content.text = NewContent;
                    break;
                }
            }
        }
        else
        {
            content.text = holder.content;
        }

        #endregion
    }

    public void ClickEmail()
    {
        if(!mainScript.dayEnded)
        {
            //SetActive Issue fixed
            //make sure email is active so it can be updated
            if (!archiveEmail)
            {
                mainScript.selected.SetActive(true);

                mainScript.selectedEmail = holder;
            }
            else
            {
                archiveTabManager.selectedEmailDisplay.SetActive(true);

                archiveTabManager.selectedEmail = holder;
            }

            ClickChangeInfo();

            _audioScript.PlayClick();

            ClickUpdateBackground();
        }
    }

    private void ClickUpdateBackground()
    {
        //Find all emails and set to unselected
        GameObject emailHolderParent;
        if (!archiveEmail)
        {
            emailHolderParent = GameObject.Find("Content");
        }
        else
        {
            emailHolderParent = GameObject.Find("ArchiveContent");
        }

        EmailHolder[] emails = emailHolderParent.GetComponentsInChildren<EmailHolder>();

        foreach (EmailHolder email in emails)
        {
            email.backgound.sprite = email.backgroundWhenUnselected;
        }


        //Set this email to selected
        backgound.sprite = backgroundWhenSelected;
    }

    public void ClickChangeInfo()
    {
        if (!archiveEmail)
        {
            #region Change Button References

            answerButton.holderCopy = holder;

            ignoreButton.holderCopy = holder;

            #endregion

            GameObject.Find("==Selected==").GetComponent<EmailDisplayScript>().UpdateDisplay(holder);
        }
        else
        {

            GameObject.Find("==SelectedArchive==").GetComponent<EmailDisplayScript>().UpdateDisplay(holder);
        }

    }
}
