using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixStartScreen : MonoBehaviour
{
    [SerializeField] GameObject background, titleImage, warningMessageText, playerNameInputField, playButton, quitButton;
    [SerializeField] RectTransform canvasRectTransform;

    float canvasWidth, canvasHeight;

    private void Awake()
    {
        canvasWidth = canvasRectTransform.rect.width;
        canvasHeight = canvasRectTransform.rect.height;

        background.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);

        titleImage.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 469 * 250, canvasHeight / 264 * 50);
        titleImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, canvasHeight / 264 * 60);

        warningMessageText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 10 * 9, canvasHeight / 264 * 30);

        playerNameInputField.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 469 * 160, canvasHeight / 264 * 30);
        playerNameInputField.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -canvasHeight / 264 * 40);

        playButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 469 * 100, canvasHeight / 264 * 30);
        playButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -canvasHeight / 264 * 80);

        quitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 469 * 50, canvasHeight / 264 * 20);
    }
}
