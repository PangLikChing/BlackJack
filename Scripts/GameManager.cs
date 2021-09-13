using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] RectTransform canvasRectTransform;
    //playerCards is the card holder for player in canvas; dealerCards is the card holder for dealer in canvas
    [SerializeField] GameObject cardDeck, playerCards, dealerCards;
    [SerializeField] GameObject hitButton, standButton, doubleDownButton, playerPointText, dealerPointText, gameOverUI, tryAgainButton, titleButton, quitButton;
    [SerializeField] Text playerNameText, playerCoinsText;
    [SerializeField] Card cardShuffler;
    [SerializeField] Player player;

    //I am too lazy, can call the sprite by name actually...
    //Something like card_getCardNumber()_getCardSuit()
    [SerializeField] Sprite[] cardImages = new Sprite[52];
    [SerializeField] Sprite[] gameOverImages = new Sprite[6];

    //playerCards is the card list for player; dealerCards is the card list for dealer
    [SerializeField] List<Card> playerHand, dealerHand;

    Card[] cards;
    int currentCard, bet;
    [SerializeField] int playerPoints, dealerPoints;

    void Awake()
    {
        //Initialize
        player.playerName = PlayerPrefs.GetString("PlayerName");
        player.playerCoins = PlayerPrefs.GetInt("PlayerCoins");
        playerNameText.text = player.playerName;
        playerCoinsText.text = "Coins: " + player.playerCoins.ToString();

        //just for now
        bet = 100;

        playerHand = new List<Card>();
        dealerHand = new List<Card>();
        //I am too lazy, can do better than this for sure
        cards = new Card[52];

        for(int i = 0; i < 52; i++)
        {
            cards[i] = cardDeck.transform.GetChild(i).gameObject.GetComponent<Card>();
            //The + 1 is just for convenient sake, do not really need it
            cards[i].setCardSuit(i % 4 + 1);
            cards[i].setCardNumber(i / 4 + 1);
        }
    }

    //test
    private void Start()
    {
        Shuffle(cards.Length, cards);

        //Give out first 2 rounds of cards
        giveCard(cards[currentCard++], dealerHand, dealerCards);
        giveCard(cards[currentCard++], playerHand, playerCards);
        giveCard(cards[currentCard++], dealerHand, dealerCards);
        giveCard(cards[currentCard++], playerHand, playerCards);

        playerPoints = CountPoints(playerHand, 0);
        dealerPoints = CountPoints(dealerHand, 0);

        playerPointText.GetComponent<Text>().text = "Player: " + playerPoints;
        if (dealerHand[0].getCardNumber() > 10)
        {
            dealerPointText.GetComponent<Text>().text = "Dealer: " + (dealerPoints - 10);
        }
        else
        {
            dealerPointText.GetComponent<Text>().text = "Dealer: " + (dealerPoints - dealerHand[0].getCardNumber());
        }
    }
    //test

    void Shuffle(int arrayChildCount, Card[] cards)
    {
        int n = arrayChildCount;
        //If n == 0, it's the first card of the deck
        if (n == 0)
        {
            Debug.Log("Done! POGGERS");
        }
        else
        {
            //Shuffling from the bottom of the deck to the first, deck size amount of times
            Shuffle(--n, cards);
            int toBeShuffle = Random.Range(0, arrayChildCount - 1);
            SwapCard(toBeShuffle, n);
        }
    }

    void SwapCard(int toBeShuffle, int currentCard)
    {
        //Card swap
        cardShuffler.setCardSuit(cards[toBeShuffle].getCardSuit());
        cardShuffler.setCardNumber(cards[toBeShuffle].getCardNumber());

        cards[toBeShuffle].setCardSuit(cards[currentCard].getCardSuit());
        cards[toBeShuffle].setCardNumber(cards[currentCard].getCardNumber());

        cards[currentCard].setCardSuit(cardShuffler.getCardSuit());
        cards[currentCard].setCardNumber(cardShuffler.getCardNumber());
    }

    void flipCard(Card card)
    {
        //The + 1 is just for convenient sake, do not really need it if there is no plus 1 in the card suit/ card number
        card.gameObject.GetComponent<Image>().sprite = cardImages[(card.getCardNumber() - 1) * 4 + (card.getCardSuit() - 1)];
    }

    void giveCard(Card card, List<Card> hand, GameObject cards)
    {
        //Add the card to hand
        hand.Add(card);
        //Modify the card value in cards GameObject
        cards.transform.GetChild(hand.Count - 1).GetComponent<Card>().setCardSuit(card.getCardSuit());
        cards.transform.GetChild(hand.Count - 1).GetComponent<Card>().setCardNumber(card.getCardNumber());
        if (cards == dealerCards && hand.Count == 1)
        {
            //Do nothing
        }
        else
        {
            flipCard(cards.transform.GetChild(hand.Count - 1).GetComponent<Card>());
        }
    }

    //numOfAce is the number of ace that will be checked by the code
    int CountPoints(List<Card> hand, int numOfAce)
    {
        //points is the number to return
        int points = 0;
        //aceCount is the number of Ace counted during the whole hand scan
        int aceCount = 0;

        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].getCardNumber() > 10)
            {
                points += 10;
            }
            else if (hand[i].getCardNumber() == 1)
            {
                //if there are Aces counting as 10 and previous CountPoints > 21
                if(aceCount < numOfAce)
                {
                    points += 1;
                }
                //count as 1
                else
                {
                    points += 10;
                    aceCount += 1;
                }
            }
            else
            {
                points += hand[i].getCardNumber();
            }
        }

        if (points > 21)
        {
            //CountPoints again with the number of ace in mind
            if(aceCount != 0)
            {
                points = CountPoints(hand, aceCount);
            }
            else

            {
                //Debug.Log("bust");
                PlayerLose(bet);
            }
        }else if (points == 21)
        {
            //black jack
            PlayerWin(bet);
        }

        return points;
    }

    void DealerAction()
    {
        if (dealerPoints < 21)
        {
            if (dealerPoints <= 0)
            {
                Debug.Log("Error");
            }
            else if (dealerPoints <= 16 && dealerPoints > 0)
            {
                giveCard(cards[currentCard++], dealerHand, dealerCards);
                flipCard(dealerCards.transform.GetChild(dealerHand.Count - 1).GetComponent<Card>());
                dealerCards.transform.GetChild(dealerHand.Count - 1).GetComponent<Card>().gameObject.SetActive(true);

                dealerPoints = CountPoints(dealerHand, 0);
                dealerPointText.GetComponent<Text>().text = "Dealer: " + dealerPoints;

                if(dealerPoints < 21)
                {
                    StartCoroutine("Wait1Second");
                    //dealerAction();
                }
                else if (dealerPoints > 21)
                {
                    //player wins
                    //hardcoding, can't solve the problem
                    player.playerCoins += bet;
                    //problem here
                    PlayerWin(bet);
                }
                else if (dealerPoints == 21)
                {
                    //dealer balck jack
                    //hardcoding, can't solve the problem
                    player.playerCoins -= bet;
                    //problem here
                    PlayerLose(bet);
                }
                else if (playerPoints == dealerPoints)
                {
                    //push
                    Push();
                }
            }
            //dealerPoints >= 17
            else
            {
                if (dealerPoints > 21)
                {
                    //player win
                    PlayerWin(bet);
                }
                else if (dealerPoints == 21)
                {
                    //dealer black jack
                    PlayerLose(bet);
                }
                else if(playerPoints > dealerPoints)
                {
                    //player win
                    PlayerWin(bet);
                }
                else if(dealerPoints > playerPoints)
                {
                    //player lose
                    PlayerLose(bet);
                }
                else if (playerPoints == dealerPoints)
                {
                    //push
                    Push();
                }
                else
                {
                    Debug.Log("Error");
                }
            }
        }
        else
        {
            //calling this function when dealerPoints > 21 somehow
            Debug.Log("Error");
        }
    }

    IEnumerator Wait1Second()
    {
        //Wait for better visual
        //second scales with Time.deltaTime
        yield return new WaitForSeconds(1.0f);
        DealerAction();
    }

    void PlayerWin(int bet)
    {
        //Player win
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameOverImages[1];
        gameOverUI.SetActive(true);
        //Show player, please change this later
        playerCoinsText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.rect.width / 488 * 160, canvasRectTransform.rect.height / 9);
        player.playerCoins += bet;
        playerCoinsText.text = "Coins: " + player.playerCoins.ToString();
        //Please use database instead
        PlayerPrefs.SetInt("PlayerCoins", player.playerCoins);
    }

    void PlayerLose(int bet)
    {
        //Player lose
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameOverImages[2];
        gameOverUI.SetActive(true);
        //Show player, please change this later
        playerCoinsText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.rect.width / 488 * 160, canvasRectTransform.rect.height / 9);
        player.playerCoins -= bet;
        playerCoinsText.text = "Coins: " + player.playerCoins.ToString();
        //Please use database instead
        PlayerPrefs.SetInt("PlayerCoins", player.playerCoins);

        if (player.getPlayerCoins() <= 0)
        {
            //Game Over, force reset
            gameOverUI.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameOverImages[5];
            PlayerPrefs.SetString("PlayerName", "");
            PlayerPrefs.SetInt("PlayerCoins", 0);
            tryAgainButton.SetActive(false);
            titleButton.SetActive(true);
        }
    }

    void Push()
    {
        //Push
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameOverImages[3];
        gameOverUI.SetActive(true);
        //No changes for text size
    }

    void PlayerBlackjack(int bet)
    {
        //Player blackjack
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = gameOverImages[4];
        gameOverUI.SetActive(true);
        //Show player, please change this later
        playerCoinsText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRectTransform.rect.width / 488 * 160, canvasRectTransform.rect.height / 9);
        player.playerCoins += bet;
        playerCoinsText.text = "Coins: " + player.playerCoins.ToString();
        //Please use database instead
        PlayerPrefs.SetInt("PlayerCoins", player.playerCoins);
    }

    //Buttons
    public void Hit()
    {
        giveCard(cards[currentCard++], playerHand, playerCards);
        flipCard(playerCards.transform.GetChild(playerHand.Count - 1).GetComponent<Card>());
        playerCards.transform.GetChild(playerHand.Count - 1).GetComponent<Card>().gameObject.SetActive(true);

        playerPoints = CountPoints(playerHand, 0);
        playerPointText.GetComponent<Text>().text = "Player: " + playerPoints;
    }

    public void Stand()
    {
        //disable the hit, stand and double down button for player
        hitButton.SetActive(false);
        standButton.SetActive(false);

        doubleDownButton.SetActive(false);
        //show first card to player
        flipCard(dealerCards.transform.GetChild(0).GetComponent<Card>());
        dealerPointText.GetComponent<Text>().text = "Dealer: " + dealerPoints;

        StartCoroutine("Wait1Second");
        //dealerAction();
    }

    public void DoubleDown()
    {
        bet *= 2;
        Hit();
        if (playerPoints < 21)
        {
            Stand();
        }
    }

    public void TryAgain()
    {
        //hard code for now, should be resetting
        SceneManager.LoadScene(1);
    }

    public void Title()
    {
        //load start scene
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
