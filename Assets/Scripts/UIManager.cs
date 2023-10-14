using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UserRecording UserRecording;
    public GameObject UIParent;
    public Text RecordingText;
    public Text VotesText;
    public LeanToggle Upvote;
    public LeanToggle Downvote;

    void Start() {
        RemoveUserRecording();
    }

    public void UpdateUserRecording(UserRecording userRecording)
    {
        UserRecording = userRecording;
        UIParent.SetActive(true);

        RecordingText.text = UserRecording.TextRecording;
        UpdateVotes();
    }

    public void UpvoteRecording()
    {
        if (UserRecording != null)
        {
            UserRecording.vote = UserRecordingVote.Upvote;
            UpdateVotes();
        }
    }

    public void DownvoteRecording()
    {
        if (UserRecording != null)
        {
            UserRecording.vote = UserRecordingVote.Downvote;
            UpdateVotes();
        }
    }

    public void CheckNoVoteRecording()
    {
        if (UserRecording != null && !Upvote.On && !Downvote.On)
        {
            UserRecording.vote = UserRecordingVote.NoVote;
            UpdateVotes();
        }
    }

    public void UpdateVotes()
    {
        switch (UserRecording.vote)
        {
            case UserRecordingVote.Upvote:
                Upvote.TurnOn();
                Upvote.TurnOffSiblingsNow();
                VotesText.text = (UserRecording.TextVotes + 1).ToString();
                break;
            case UserRecordingVote.Downvote:
                Downvote.TurnOn();
                Downvote.TurnOffSiblingsNow();
                VotesText.text = (UserRecording.TextVotes - 1).ToString();
                break;
            case UserRecordingVote.NoVote:
                Upvote.TurnOff();
                Downvote.TurnOff();
                VotesText.text = UserRecording.TextVotes.ToString();
                break;
        }
    }

    public void RemoveUserRecording()
    {
        RecordingText.text = "";
        VotesText.text = "-";
        Upvote.TurnOff();
        Downvote.TurnOff();
        UserRecording = null;
        UIParent.SetActive(false);
    }
}
