using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogManager : MonoBehaviour
{
    private Player player;

    private TextMeshPro text;
    private string[] messages;
    private int messagesLength;
    private int currentMessage;
    private bool dialog;

    private void Update()
    {
        if (dialog)
        {
            CheckPlayerInput();
            if (currentMessage >= messagesLength)
            {
                EndDialog();
            }
            text.text = messages[currentMessage];
        }
    }

    private void CheckPlayerInput()
    {
        if (player.InputHandler.DialogInput)
        {
            currentMessage++;
        }
    }

    public void TriggerDialog(TextMeshPro text,Player player, string[] msgs, int length)
    {
        this.player = player;
        this.text = text;
        messages = msgs;
        messagesLength = length;
        currentMessage = -1;
        player.InputHandler.ChangeCurrentActionMapToDialog();
        dialog = true;
    }

    private void EndDialog()
    {
        dialog = false;
        player.InputHandler.ChangeCurrentActionMapToGameplay();
        player = null;
        messages = null;
        text = null;
        messagesLength = 0;
        currentMessage = 0;
    }

}
