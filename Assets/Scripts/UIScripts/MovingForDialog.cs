using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Needed for accessing the Button component

public class MovingForDialog : MonoBehaviour
{
    public RectTransform dogRectTransform;
    public RectTransform dialogBoxRectTransform;
    public Vector2 dogEndPosition;
    public Vector2 dialogEndPosition;
    public float movementSpeed = 1f;  // Lower this speed for a smoother transition

    private Vector2 dogStartPosition;
    private Vector2 dialogStartPosition;
    private bool dialogInProgress = false;
    private bool isInterruptible = false;
    private Button dialogButton;

    void Start()
    {
        // Store initial positions
        dogStartPosition = dogRectTransform.anchoredPosition;
        dialogStartPosition = dialogBoxRectTransform.anchoredPosition;

        // Setup the button to control interruption
        dialogButton = GetComponent<Button>();
        if (dialogButton != null)
        {
            dialogButton.onClick.AddListener(InterruptAndEndDialog);
        }
    }

    public void StartDialog()
    {
        if (!dialogInProgress)
        {
            StartCoroutine(DialogCoroutine());
        }
    }

    IEnumerator DialogCoroutine()
    {
        dialogInProgress = true;
        isInterruptible = false;

        Coroutine moveDog = StartCoroutine(SmoothMoveUIElement(dogRectTransform, dogEndPosition));
        Coroutine moveDialog = StartCoroutine(SmoothMoveUIElement(dialogBoxRectTransform, dialogEndPosition));

        yield return moveDog;
        yield return moveDialog;

        isInterruptible = true;

        yield return new WaitWhile(() => dialogInProgress);

        StartCoroutine(SmoothMoveUIElement(dialogBoxRectTransform, dialogStartPosition));
        StartCoroutine(SmoothMoveUIElement(dogRectTransform, dogStartPosition));
        dialogInProgress = false;
        isInterruptible = false;
    }

    IEnumerator SmoothMoveUIElement(RectTransform rectTransform, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startingPos = rectTransform.anchoredPosition;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * movementSpeed;
            float t = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime);
            rectTransform.anchoredPosition = Vector2.Lerp(startingPos, targetPosition, t);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
    }

    public void InterruptAndEndDialog()
    {
        if (dialogInProgress && isInterruptible)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothMoveUIElement(dialogBoxRectTransform, dialogStartPosition));
            StartCoroutine(SmoothMoveUIElement(dogRectTransform, dogStartPosition));
            dialogInProgress = false;
            isInterruptible = false;
        }
    }
}
