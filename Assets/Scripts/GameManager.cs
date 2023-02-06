using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<Emotion> CURRENT_EMOTIONS;

    public static Emotion FAILED_EMOTION;

    public static int COMMISERATE_SUCCESSES;

    public static int ROUNDS;

    public static int FAILS = 0;

    private void Awake()
    {
        EventManager.COMMISERATE_LOSE += handleCommiserateFail;
    }

    // Start is called before the first frame update
    void Start()
    {
        ROUNDS = 0;
        COMMISERATE_SUCCESSES = 0;

        CURRENT_EMOTIONS = new List<Emotion>();
        
        nextInterlocutor();
        EventManager.StartRoom();

    }

    public static void commiserateWin()
    {
        if (CURRENT_EMOTIONS.Count <= 0)
            generateNewRoom();
    }

    public static void generateNewRoom() {
        EventManager.GenerateRoom();
        nextInterlocutor();
    }

    public static void nextInterlocutor()
    {
        CURRENT_EMOTIONS.Clear();
        FAILS = 0;

        if (ROUNDS == 0) {

            CURRENT_EMOTIONS.Add(Emotion.Sadness);

        } else if (ROUNDS == 1) {

            CURRENT_EMOTIONS.Add(Emotion.Sadness);
            CURRENT_EMOTIONS.Add(Emotion.Anticipation);

        } else if (ROUNDS == 2) {

            CURRENT_EMOTIONS.Add(Emotion.Anxiety);

        } else {

            for (int i = 0; i < Random.Range(1, 5); i++) {
                CURRENT_EMOTIONS.Add((Emotion) Random.Range(1, 11));
            }

        }

        ROUNDS++;

    }

    public static void tryCommiserateEmotion(Emotion emotion)
    {
        if (CURRENT_EMOTIONS.Contains(emotion))
            EventManager.StartCommiserate(emotion);
        else
            handleIncorrectChoice(emotion);

    }

    public static void handleIncorrectChoice(Emotion e) {

        FAILED_EMOTION = e;
        EventManager.Insanify(0.1f);
        EventManager.CommiserateLose();
    }

    public static void handleCommiserateFail()
    {
        ThoughtBoard.playHurtSound();
        FAILS++;
        if (FAILS >= 3)
        {
            generateNewRoom();
        }
    }

}