using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogManager))]
public class DialogManagerEditor : Editor
{
    private bool warningDisplayed = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogManager dialogManager = (DialogManager)target;

        if (dialogManager.dialogues.Length != dialogManager.scoreThresholds.Length && !warningDisplayed)
        {
            warningDisplayed = true;
            Debug.LogWarning("Number of dialogue entries does not match number of score thresholds. Click to focus on the DialogManager component.");
        }
        else if (dialogManager.dialogues.Length == dialogManager.scoreThresholds.Length)
        {
            warningDisplayed = false;
            RemoveWarning();
        }
    }

    private void RemoveWarning()
    {
        Debug.ClearDeveloperConsole(); // Clear the console
    }
}
