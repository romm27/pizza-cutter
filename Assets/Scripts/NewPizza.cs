using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPizza : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] AudioClip swap;
    [SerializeField] Image gfx;

    [Header("Settings")]
    public float delay = 0.75f;

    //Methods
    public void GenerateNewPizza(Sprite sprite) {
        gfx.sprite = sprite;
        StartCoroutine(AnimatePizza());
    }
    public void OnMainAnimationFinished() {
        animator.Play("default");
        //gfx.enabled = false;
    }
    public void HideSprite() {
        gfx.enabled = false;
    }

    private IEnumerator AnimatePizza() {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.PlayClip(swap, 2f);
        animator.Play("main");
        gfx.enabled = true;
    }
}
