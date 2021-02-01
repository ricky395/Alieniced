using UnityEngine;

public class AudioController : MonoBehaviour
{
    /// <summary>
    /// Fuente de audio
    /// </summary>
    AudioSource audioSource;
    
    /// <summary>
    /// Audio de portal
    /// </summary>
    public AudioClip portal;

    /// <summary>
    /// Audio de meta
    /// </summary>
    public AudioClip goal;

    /// <summary>
    /// Audio de roca resquebrajada
    /// </summary>
    public AudioClip rockBreak;

    /// <summary>
    /// Audio de roca rota
    /// </summary>
    public AudioClip rockFinalBreak;

    public AudioClip key;
    public AudioClip death;
    public AudioClip clickBip;
    public AudioClip redButton;
    public AudioClip greenButton;

    /// <summary>
    /// Obtiene el componente de audio y evita que se destruya el objeto
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    /// <summary>
    /// Reproduce el audio de los portales
    /// </summary>
    public void PlayPortal()
    {
        audioSource.PlayOneShot(portal);
    }

    /// <summary>
    /// Reproduce el audio de meta
    /// </summary>
    public void PlayGoal()//Llegada al portal final
    {
        audioSource.PlayOneShot(goal, 0.5f);
    }

    /// <summary>
    /// Reproduce el audio de rota resquebrajada
    /// </summary>
    public void PlayRockBreak()//Choque con una roca que se resquebraja
    {
        audioSource.PlayOneShot(rockBreak, 0.7f);
    }

    /// <summary>
    /// Reproduce el audio de roca rota
    /// </summary>
    public void PlayRockFinalBreak()//Choque con una roca que se rompe del todo
    {
        audioSource.PlayOneShot(rockFinalBreak, 0.7f);
    }

    public void PlayKey()
    {
        audioSource.PlayOneShot(key, 0.8f);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(death, 0.5f);
    }

    public void PlayClickBip()
    {
        audioSource.PlayOneShot(clickBip, 0.9f);
    }

    public void PlayRedButton()
    {
        audioSource.PlayOneShot(redButton, 0.8f);
    }

    public void PlayGreenButton()
    {
        audioSource.PlayOneShot(greenButton, 0.6f);
    }
}
