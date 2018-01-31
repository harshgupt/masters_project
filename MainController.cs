using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gGenesis;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    private bool audioFlag;
    [SerializeField]
    private double timeOfDay;
    [SerializeField]
    private Light light1;
    [SerializeField]
    private Light light2;
    [SerializeField]
    private AudioSource bgmAudioSource;
    [SerializeField]
    private AudioSource sfxAudioSource;
    [SerializeField]
    private AudioSource weatherAudioSource;
    [SerializeField]
    private AudioSource thunderAudioSource;
    [SerializeField]
    private AudioSource windAudioSource;
    [SerializeField]
    private ParticleSystem rainParticles;
    [SerializeField]
    private ParticleSystem snowFlake;
    [SerializeField]
    private ParticleSystem fog;
    [SerializeField]
    private AudioClip rain1;
    [SerializeField]
    private AudioClip rain2;
    [SerializeField]
    private AudioClip thunder;
    [SerializeField]
    private AudioClip wind1;
    [SerializeField]
    private AudioClip wind2;
    [SerializeField]
    private AudioClip wind;
    [SerializeField]
    private AudioClip daySFX;
    [SerializeField]
    private AudioClip nightSFX;
    [SerializeField]
    private AudioClip dayBGM;
    [SerializeField]
    private AudioClip nightBGM;
    [SerializeField]
    private AudioClip sunriseBGM;
    [SerializeField]
    private AudioClip sunsetBGM;
    [SerializeField]
    private AudioClip rain1BGM;
    [SerializeField]
    private AudioClip rain2BGM;
    [SerializeField]
    private AudioClip fogBGM;
    [SerializeField]
    private AudioClip snowBGM;
    [SerializeField]
    GameObject timeController;
    DayNightController timeScript;


    void Start()                                                                           //Used to assign variables and to set environment to default state
    {
        timeScript = timeController.GetComponent<DayNightController>();
        thunderAudioSource.enabled = false;
        weatherAudioSource.enabled = false;
        sfxAudioSource.enabled = true;
        sfxAudioSource.clip = daySFX;
        sfxAudioSource.volume = 0.3f;
        sfxAudioSource.Play();
        bgmAudioSource.enabled = false;
        bgmAudioSource.clip = dayBGM;
        bgmAudioSource.volume = 0.5f;
        bgmAudioSource.Play();
        windAudioSource.enabled = true;
        windAudioSource.clip = wind;
        windAudioSource.Play();
        audioFlag = false;
        light1.enabled = false;
        light2.enabled = false;
    }
    void Update()
    {
        timeOfDay = timeScript.currentTime;
        if (Application.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.B))                    //Light Rain
            {
                Rainfall();
            }

            if (Input.GetKeyDown(KeyCode.N))                    //Heavy Rain
            {
                Thunderstorm();
            }

            if (Input.GetKeyDown(KeyCode.L))                    //Snow
            {
                Snowfall();
            }

            if (Input.GetKeyDown(KeyCode.K))                    //Fog
            {
                Foggy();
            }

            if (Input.GetKeyDown(KeyCode.M))                    //Stop Particles and Sound Effects (RUN THIS BEFORE SWITCHING TO OTHER WEATHER)
            {
                StopWeather();
            }

            if (Input.GetKeyDown(KeyCode.A))                    //Play Day Background Music (BGM)
            {
                PlayDayBGM();
            }

            if (Input.GetKeyDown(KeyCode.S))                    //Play Night BGM
            {
                PlayNightBGM();
            }

            if (Input.GetKeyDown(KeyCode.D))                    //Play Sunrise BGM
            {
                PlaySunriseBGM();
            }

            if (Input.GetKeyDown(KeyCode.F))                    //Play Sunset BGM
            {
                PlaySunsetBGM();
            }

            if (Input.GetKeyDown(KeyCode.G))                    //Play Rain1 BGM
            {
                PlayRain1BGM();
            }

            if (Input.GetKeyDown(KeyCode.H))                    //Play Rain2 BGM
            {
                PlayRain2BGM();
            }

            if (Input.GetKeyDown(KeyCode.J))                    //Play Snow BGM
            {
                PlaySnowBGM();
            }

            if (Input.GetKeyDown(KeyCode.I))                    //Play Fog BGM
            {
                PlayFogBGM();
            }

            if (timeOfDay >= 6.0f && timeOfDay <= 6.01f && !weatherAudioSource.enabled)         //These functions are used to switch on and off the lights of the lamp according to time of day
            {
                light1.enabled = false;
                light2.enabled = false;
                sfxAudioSource.enabled = false;
                sfxAudioSource.enabled = true;
                sfxAudioSource.clip = daySFX;
                sfxAudioSource.volume = 1.0f;
                sfxAudioSource.Play();
            }
            if (timeOfDay >= 12.0f && timeOfDay <= 12.01f && !weatherAudioSource.enabled)
            {
                light1.enabled = false;
                light2.enabled = false;
                sfxAudioSource.enabled = false;
                sfxAudioSource.enabled = true;
                sfxAudioSource.clip = daySFX;
                sfxAudioSource.volume = 1.0f;
                sfxAudioSource.Play();
            }
            if (timeOfDay >= 18.0f && timeOfDay <= 18.01f && !weatherAudioSource.enabled)
            {
                light1.enabled = true;
                light2.enabled = true;
                sfxAudioSource.enabled = false;
                sfxAudioSource.enabled = true;
                sfxAudioSource.clip = nightSFX;
                sfxAudioSource.volume = 0.3f;
                sfxAudioSource.Play();
            }
            if (timeOfDay >= 0.0f && timeOfDay <= 0.01f && !weatherAudioSource.enabled)
            {
                light1.enabled = true;
                light2.enabled = true;
                sfxAudioSource.enabled = false;
                sfxAudioSource.enabled = true;
                sfxAudioSource.clip = nightSFX;
                sfxAudioSource.volume = 0.3f;
                sfxAudioSource.Play();
            }

        }
    }

    public void StopWeather()                                                       //Used to stop particle effects of weather, and to manage audio sources
    {
        ParticleSystem.EmissionModule pSystem = rainParticles.emission;             
        pSystem.enabled = false;
        pSystem = snowFlake.emission;
        pSystem.enabled = false;
        pSystem = fog.emission;
        pSystem.enabled = false;
        weatherAudioSource.enabled = false;
        sfxAudioSource.enabled = true;
        thunderAudioSource.enabled = false;
        windAudioSource.enabled = true;
        windAudioSource.clip = wind;
        windAudioSource.Play();
    }

    public void Rainfall()                                                          //Weather functions to trigger them follow the same format
    {
        ParticleSystem.EmissionModule pSystem = rainParticles.emission;             //Set particle system to specified weather particle and start emission
        pSystem.enabled = true;
        var Rate = pSystem.rateOverTime;                                            //Set the rate of emission
        Rate.constantMax = 500f;
        pSystem.rateOverTime = Rate;
        sfxAudioSource.enabled = false;                                             //Disable default background sound effects
        weatherAudioSource.enabled = true;                                          //Enable and play weather sound effects
        weatherAudioSource.clip = rain1;
        weatherAudioSource.volume = 1.0f;
        weatherAudioSource.Play();
        windAudioSource.enabled = true;                                             //Enable wind sound effect
        windAudioSource.clip = wind1;
        windAudioSource.Play();
    }

    public void Thunderstorm()
    {
        ParticleSystem.EmissionModule pSystem = rainParticles.emission;
        pSystem.enabled = true;
        var Rate = pSystem.rateOverTime;
        Rate.constantMax = 10000f;                                                  //Same as Rainfall(), with increased particle emission rate
        pSystem.rateOverTime = Rate;
        sfxAudioSource.enabled = false;
        weatherAudioSource.enabled = true;
        weatherAudioSource.clip = rain2;
        weatherAudioSource.volume = 1.0f;
        weatherAudioSource.Play();
        thunderAudioSource.enabled = true;                                          //Play thunder sound effects
        thunderAudioSource.Play();
        windAudioSource.enabled = true;
        windAudioSource.clip = wind2;
        windAudioSource.Play();
    }

    public void Snowfall()                                                          //Same as Rainfall() but with snow particles instead
    {
        ParticleSystem.EmissionModule pSystem = snowFlake.emission;
        pSystem.enabled = true;
        var Rate = pSystem.rateOverTime;
        Rate.constantMax = 1000;
        pSystem.rateOverTime = Rate;
        sfxAudioSource.enabled = false;
        windAudioSource.enabled = true;
        windAudioSource.clip = wind;
        windAudioSource.Play();
    }

    public void Foggy()                                                             //Same as Rainfall() but with fog particles instead
    {
        ParticleSystem.EmissionModule pSystem = fog.emission;
        pSystem.enabled = true;
        var Rate = pSystem.rateOverTime;
        Rate.constantMax = 10000;
        pSystem.rateOverTime = Rate;
        sfxAudioSource.enabled = false;
        windAudioSource.enabled = false;
    }

    public void PlayDayBGM()                                                        //All the following functions to play different background music follow the same format
    {
        bgmAudioSource.enabled = false;                                             //Disable and re-enable the background music audio source to reset it
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = dayBGM;                                               //Select corresponding clip to be played
        bgmAudioSource.volume = 0.5f;                                               //Set volume if required
        bgmAudioSource.Play();                                                      //Play it
    }

    public void PlayNightBGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = nightBGM;
        bgmAudioSource.volume = 1.0f;
        bgmAudioSource.Play();
    }

    public void PlaySunriseBGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = sunriseBGM;
        bgmAudioSource.volume = 1.0f;
        bgmAudioSource.Play();
    }

    public void PlaySunsetBGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = sunsetBGM;
        bgmAudioSource.volume = 0.75f;
        bgmAudioSource.Play();
    }

    public void PlayRain1BGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = rain1BGM;
        bgmAudioSource.volume = 1.0f;
        bgmAudioSource.Play();
    }

    public void PlayRain2BGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = rain2BGM;
        bgmAudioSource.volume = 0.75f;
        bgmAudioSource.Play();
    }

    public void PlaySnowBGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = snowBGM;
        bgmAudioSource.volume = 0.75f;
        bgmAudioSource.Play();
    }

    public void PlayFogBGM()
    {
        bgmAudioSource.enabled = false;
        bgmAudioSource.enabled = true;
        bgmAudioSource.clip = fogBGM;
        bgmAudioSource.volume = 0.75f;
        bgmAudioSource.Play();
    }
}