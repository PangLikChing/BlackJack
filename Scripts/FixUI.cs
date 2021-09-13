using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixUI : MonoBehaviour
{
    [SerializeField] RectTransform canvasRectTransform;
    [SerializeField] GameObject background;
    
    //card is the card templete showing as the card deck
    [SerializeField] GameObject card, playerCards, dealerCards, hitButton, standButton, doubleDownButton, playerNameText, playerCoinsText, playerPointText, dealerPointText;
    [SerializeField] GameObject gameOverPanel, bustImage, tryAgainButton, quitButton;

    float canvasWidth, canvasHeight;

    void Awake()
    {
        canvasWidth = canvasRectTransform.rect.width;
        canvasHeight = canvasRectTransform.rect.height;

        background.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);

        card.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 11, canvasHeight / 9 * 2);
        card.GetComponent<RectTransform>().anchoredPosition = new Vector2(canvasWidth * 3 / 10, 0);

        hitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 5, canvasHeight / 9);
        hitButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-canvasWidth * 170 / 487, 0);

        standButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 5, canvasHeight / 9);
        standButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-canvasWidth * 20 / 487, 0);

        doubleDownButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 487 * 60, canvasHeight / 274 * 70);
        doubleDownButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(canvasWidth * 80 / 487, 0);

        playerNameText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 488 * 80, canvasHeight / 9);
        playerNameText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, canvasHeight / 264 * 60);

        playerCoinsText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 488 * 80, canvasHeight / 9);
        playerCoinsText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, canvasHeight / 264 * 30);

        playerPointText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 488 * 160, canvasHeight / 9);

        dealerPointText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 488 * 160, canvasHeight / 9);

        gameOverPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);

        bustImage.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 487 * 200, canvasHeight / 274 * 100);
        bustImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, canvasHeight / 274 * 50);

        tryAgainButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 5, canvasHeight / 9);
        tryAgainButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -canvasHeight / 274 * 80);

        quitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 469 * 50, canvasHeight / 264 * 20);

        for (int i = 0; i < playerCards.transform.childCount; i++)
        {
            RectTransform cardRectTransform = playerCards.transform.GetChild(i).GetComponent<RectTransform>();

            cardRectTransform.sizeDelta = new Vector2(canvasWidth / 11, canvasHeight / 9 * 2);
            cardRectTransform.anchoredPosition = new Vector2(-canvasWidth * 3 / 10 + (cardRectTransform.rect.width / 2) * i, -canvasHeight / 274 * 80);
        }

        for (int i = 0; i < playerCards.transform.childCount; i++)
        {
            RectTransform cardRectTransform = dealerCards.transform.GetChild(i).GetComponent<RectTransform>();

            cardRectTransform.sizeDelta = new Vector2(canvasWidth / 11, canvasHeight / 9 * 2);
            cardRectTransform.anchoredPosition = new Vector2(-canvasWidth * 3 / 10 + (cardRectTransform.rect.width / 2) * i, canvasHeight / 274 * 80);
        }
    }
}
