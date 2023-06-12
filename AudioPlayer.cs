using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource music_1;
    public AudioSource music_2;
    public float fadeInTime = 1f; // The time it takes for the audio to fade in
    public float fadeOutTime = 1f; // The time it takes for the audio to fade out
    public float minVolume = 0f; // The minimum volume for the audio
    private float volume = 0.4f; // The current volume of the audio
    //old_man
    public AudioSource old_man;
    //old_man
    public AudioSource eye_patch;
    //crickets
    public AudioSource crickets;
    //walk planet
    public AudioSource walkPlanet;
    //walk office
    public AudioSource walkOffice;
    //wind
    public AudioSource wind;
    private int windCount;
    //marcelo
    public AudioSource marcelo;
    //doctor
    public AudioSource doctor;

    

    private void Start() {
        windCount = 0;
        wind.volume = 0;
        
    }




    public void Music_1(bool play)
    {
        
        if(play){
            music_1.Play();
        }else{
            music_1.Stop();
        }
    }


    public void Music_2(bool play)
    {
        if(play){
            music_2.Play();
        }else{
            music_2.Stop();
        }
    }

    public void Crickets(bool play)
    {
        if(play){
            crickets.Play();
        }else{
            crickets.Stop();
        }
    }    

    public void WalkPlanet(bool play)
    {
        if(play){
            walkPlanet.Play();
        }else{
            walkPlanet.Stop();
        }
    }    


    public void OldMan()
    {
        // Generate random pitch shift factor between minPitch and maxPitch
        float pitchFactor = Random.Range(0.8f, 1.0f);

        // Apply pitch shift
        old_man.pitch = pitchFactor;

        // Play audio
        old_man.Play();
    }

    public void EyePatch()
    {
        // Generate random pitch shift factor between minPitch and maxPitch
        float pitchFactor = Random.Range(1.2f, 1.5f);

        // Apply pitch shift
        eye_patch.pitch = pitchFactor;

        // Play audio
        eye_patch.Play();
    }    

    public void Marcelo()
    {
        // Generate random pitch shift factor between minPitch and maxPitch
        float pitchFactor = Random.Range(1.0f, 1.6f);

        // Apply pitch shift
        marcelo.pitch = pitchFactor;

        // Play audio
        marcelo.Play();
    }

    public void Doctor()
    {
        // Generate random pitch shift factor between minPitch and maxPitch
        float pitchFactor = Random.Range(1.2f, 1.6f);

        // Apply pitch shift
        doctor.pitch = pitchFactor;

        // Play audio
        doctor.Play();
    }

    public float GetVolumeMusic_2(float playerY)
    {
        if (playerY <= -208f)
        {
            return 0f;
        }
        else if (playerY >= -128f)
        {
            return 0.4f;
        }
        else
        {
            float volumePercentage = (playerY - -208f) / (-128f - -208f);
            return volumePercentage * 0.4f;
        }
    }

    public float GetVolumeCrickets(float playerY)
    {
        if (playerY <= -282f)
        {
            return 1f;
        }
        else if (playerY >= -110f)
        {
            return 0f;
        }
        else
        {
            float volumePercentage = (playerY - -282f) / (-110f - -282f);
            return (1 - volumePercentage);
        }
    }    

    public void SetVolumeMusic_2(float volume)
    {
        music_2.volume = volume;
    }

    public void SetVolumeCrickets(float volume)
    {
        crickets.volume = volume;
    }    

    public void SetVolumeWalkPlanet(float volume)
    {
        walkPlanet.volume = volume;
    }   

    public void SetVolumeWalkOffice(float volume)
    {
        walkOffice.volume = volume;
    }   

    public void Wind(){
        windCount++;
        if(windCount == 1){
            FadeInWind();
        }else{
            FadeOutWind();
        }
    }

    public void FadeOutWind()
    {
        StartCoroutine(FadeOutAudio());
    }

    public void FadeInWind()
    {
        StartCoroutine(FadeInAudio());
    }

    private IEnumerator FadeOutAudio()
    {
        float timer = 0f;
        float startVolume = wind.volume;

        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            volume = Mathf.Lerp(startVolume, 0f, timer / fadeOutTime);
            wind.volume = volume;
            yield return null;
        }

        wind.volume = minVolume;
    }

    private IEnumerator FadeInAudio()
    {
        float timer = 0f;

        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            volume = Mathf.Lerp(0f, 1f, timer / fadeInTime);
            wind.volume = volume;
            yield return null;
        }
    }    
}