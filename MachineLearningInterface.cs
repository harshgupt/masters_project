using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearningInterface : MonoBehaviour {

    [SerializeField]
    GameObject mainController;
    MainController controllerScript;
    [SerializeField]
    GameObject timeController;
    DayNightController timeScript;
    [SerializeField]
    GameObject animationController;
    KayaController animationScript;
    [SerializeField]
    private int timeType;                           //To identify type of element
    [SerializeField]
    private int weatherType;
    [SerializeField]
    private int musicType;
    [SerializeField]
    private int animationType;
    [SerializeField]
    private int timeScore0;                          //Global scores
    [SerializeField]
    private int timeScore1;
    [SerializeField]
    private int weatherScore0;
    [SerializeField]
    private int weatherScore1;
    [SerializeField]
    private int musicScore0;
    [SerializeField]
    private int musicScore1;
    [SerializeField]
    private int animScore0;
    [SerializeField]
    private int animScore1;
    delegate void Actions();                                    
    private List<Actions> actionList = new List<Actions>();       //List of Element Change Functions
    [SerializeField]
    private int listCount;

    // Use this for initialization
    void Start ()
    {
        controllerScript = mainController.GetComponent<MainController>();
        timeScript = timeController.GetComponent<DayNightController>();
        animationScript = animationController.GetComponent<KayaController>();
        timeScore0 = 0;
        timeScore1 = 0;
        weatherScore0 = 0;
        weatherScore1 = 0;
        musicScore0 = 0;
        musicScore1 = 0;
        actionList.Add(changeTime);
        actionList.Add(changeWeather);
        actionList.Add(changeMusic);
        actionList.Add(changeAnimation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        listCount = actionList.Count;
        if (Input.GetKeyDown(KeyCode.RightShift))                                       //To Start Experiment
        {
            timeScript.SetNoon();                                                       //Initial Configuration
            controllerScript.StopWeather();
            controllerScript.PlayDayBGM();
            animationScript.PeacefulAnimation();
            timeType = 0;
            weatherType = 0;
            musicType = 0;
            animationType = 0;
        }

        if(actionList.Count > 0)                                                     //Repeat until final configuration is achieved
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))                                   //Continue algorithm for positive response
            {
                if (timeType == 0)
                {
                    timeScore0 += 10;
                }
                else
                {
                    timeScore1 += 10;
                }

                if (timeScore0 >= 20 || timeScore1 >= 20)                           //Do not change time if score is greater than chosen threshold
                {
                    actionList.Remove(changeTime);
                }

                if (weatherType == 0)
                {
                    weatherScore0 += 10;
                }
                else
                {
                    weatherScore1 += 10;
                }

                if (weatherScore0 >= 20 || weatherScore1 >= 20)        //Do not change weather if score is >= 20
                {
                    actionList.Remove(changeWeather);
                }

                if (musicType == 0)
                {
                    musicScore0 += 10;
                }
                else
                {
                    musicScore1 += 10;
                }

                if (musicScore0 >= 20 || musicScore1 >= 20)        //Do not change music if score is >= 20
                {
                    actionList.Remove(changeMusic);
                }

                if (animationType == 0)
                {
                    animScore0 += 10;
                }
                else
                {
                    animScore1 += 10;
                }

                if (animScore0 >= 20 || animScore1 >= 20)        //Do not change time if score is >= 20
                {
                    actionList.Remove(changeAnimation);
                }

                changeN1Elements(actionList);                    //To modify n-1 elements out of n
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))            //Continue algorithm for negative response
            {
                if (timeType == 0)
                {
                    timeScore0 -= 10;
                    if(timeScore0 == -20)                       //Insert the negative of the chosen threshold
                    {  
                        changeTime();
                        actionList.Remove(changeTime);
                    }
                }
                else
                {
                    timeScore1 -= 10;
                    if (timeScore1 == -20)
                    {
                        changeTime();
                        actionList.Remove(changeTime);
                    }
                }

                if (weatherType == 0)
                {
                    weatherScore0 -= 10;
                    if (weatherScore0 == -20)
                    {
                        changeWeather();
                        actionList.Remove(changeWeather);
                    }
                }
                else
                {
                    weatherScore1 -= 10;
                    if (weatherScore1 == -20)
                    {
                        changeWeather();
                        actionList.Remove(changeWeather);
                    }
                }

                if (musicType == 0)
                {
                    musicScore0 -= 10;
                    if (musicScore0 == -20)
                    {
                        changeMusic();
                        actionList.Remove(changeMusic);
                    }
                }
                else
                {
                    musicScore1 -= 10;
                    if (musicScore1 == -20)
                    {
                        changeMusic();
                        actionList.Remove(changeMusic);
                    }
                }

                if (animationType == 0)
                {
                    animScore0 -= 10;
                    if (animScore0 == -20)
                    {
                        changeAnimation();
                        actionList.Remove(changeAnimation);
                    }
                }
                else
                {
                    animScore1 -= 10;
                    if (animScore1 == -20)
                    {
                        changeAnimation();
                        actionList.Remove(changeAnimation);
                    }
                }
                
                changeN1Elements(actionList);                    //To modify n-1 elements out of n
            }

            if (Input.GetKeyDown(KeyCode.Space))                //To make n-1 modifications (Randomized Algorithm)
            {
                changeN1Elements(actionList);
            }
        }
    }

    void changeN1Elements(List<Actions> actions)                                        //Function to perform n-1 out of n elements in list of actions
    {
        int count = actions.Count;
        if(count == 1)
        {
            actions[0]();
        }
        else
        {
            List<int> indexList = new List<int>();
            for(int i = 0; i < count; i++)
            {
                indexList.Add(i);
            }
            int randIndex = Random.Range(0, indexList.Count - 1);
            indexList.RemoveAt(randIndex);
            for (int index = 0; index <= indexList.Count - 1; index++)
            {
                int currIndex = indexList[index];
                actions[currIndex]();
            }
        }
        

    }

    void changeTime()                                                           //Change time of day to other state
    {
        if(timeScript.currentTime >= 6.0f && timeScript.currentTime <= 18.0f)
        {
            timeScript.SetMidnight();
            timeType = 1;
        }
        else
        {
            timeScript.SetNoon();
            timeType = 0;
        }
    }

    void changeWeather()                                                           //Change weather to other state
    {
        if(weatherType == 0)
        {
            controllerScript.Rainfall();
            weatherType = 1;
        }
        else
        {
            controllerScript.StopWeather();
            weatherType = 0;
        }
    }

    void changeMusic()                                                           //Change music to other state
    {
        if(musicType == 0)
        {
            controllerScript.PlaySunriseBGM();
            musicType = 1;
        }
        else
        {
            controllerScript.PlayDayBGM();
            musicType = 0;
        }
    }

    void changeAnimation()                                                            //Change animation to other state
    {
        if(animationType == 0)
        {
            animationScript.DanceAnimation();
            animationType = 1;
        }
        else
        {
            animationScript.PeacefulAnimation();
            animationType = 0;
        }
    }
}
