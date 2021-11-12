using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float    maxHP = 10;
    private float currentHP;
    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHp => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if ( currentHP <= 0) {
            Debug.Log("Player HP: 0.. Die");
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }

}
