using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private Animator anim;
    public bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] UnityEvent healthReduced;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) { Debug.Log("invul"); return; }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        healthReduced.Invoke();

        if (currentHealth > 0)
        {          
            SoundManager.Instance.PlaySoundWithRandomValues(hitSound);
            //anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                SoundManager.Instance.PlaySoundWithRandomValues(deathSound);
                if (anim != null)
                {
                    anim.SetTrigger("die");
                }

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
            }
        }
    }


    public void ReduceHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, startingHealth);
        if (!dead && currentHealth <= 0)
        {
            SoundManager.Instance.PlaySound(deathSound);
            if (anim != null)
            {
                anim.SetTrigger("die");
            }
          

            //Deactivate all attached component classes
            foreach (Behaviour component in components)
                component.enabled = false;

            dead = true;
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void RestoreHealth()
    {
        currentHealth = startingHealth;
    }
    private IEnumerator Invunerability()
    {
        //invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        //invulnerable = false;
    }
}
