using System;
using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;
    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private Entity _owner;


    //[SerializeField] private AnimationCurve _flashCurve;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        GetOwner();
    }

    private void GetOwner()
    {
        var attempt = GetComponent<Entity>();
        if (attempt == null)
        {
            attempt = GetComponentInParent<Entity>();
        }

        if (attempt == null)
        {
            Debug.Log("You are probably missing an entity component");
        }
        else
        {
            _owner = attempt;
        }
    }

    private void OnEnable()
    {
        _owner.onDamage += StartFlash;
    }

    private void OnDisable()
    {
        _owner.onDamage -= StartFlash;
    }

    private void StartFlash()
    {
        StartCoroutine("Flash");
        Debug.Log("Starting flash");
    }

    IEnumerator Flash()
    {
        _material.color = _flashColor;

        float currentFlash = 0;
        float elapsedTime = 0;

        while (elapsedTime <= _flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlash = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
            _material.SetFloat("_FlashAmt", currentFlash);

            yield return null;
        }
    }
}
