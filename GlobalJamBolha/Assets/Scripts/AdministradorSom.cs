using UnityEngine;
using UnityEngine.UI;

public class AdministradorSom : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider sliderSom;

    [SerializeField] private AudioClip[] dolphinSounds;
    [SerializeField] private AudioClip waterAudioClip;
    [SerializeField] private AudioClip bubbleAudioClip;
    [SerializeField] private AudioClip filterAudioClip;
    [SerializeField] private AudioClip buttonAudioClip;

    public static AdministradorSom Instance;

    private void Start()
    {
        Instance = this;

        if (sliderSom == null) return;

        if (PlayerPrefs.HasKey("Som"))
        {
            sliderSom.value = PlayerPrefs.GetFloat("Som");
            AudioListener.volume = PlayerPrefs.GetFloat("Som");
        }
        else
            PlayerPrefs.SetFloat("Som", 0.25f);
    }

    public void MudaVolume()
    {
        AudioListener.volume = sliderSom.value;
        PlayerPrefs.SetFloat("Som", sliderSom.value);
    }

    public void PlayDolphinSound() => audioSource.PlayOneShot(dolphinSounds[Random.Range(0, dolphinSounds.Length)]);

    public void PlayBubbleSound() => audioSource.PlayOneShot(bubbleAudioClip);
    
    public void PlayWaterSound() => audioSource.PlayOneShot(waterAudioClip);

    public void PlayFilterSound() => audioSource.PlayOneShot(filterAudioClip);

    public void PlayButtonSound() => audioSource.PlayOneShot(buttonAudioClip);
}
