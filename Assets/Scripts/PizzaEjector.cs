using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PizzaEjector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image circle;
    [SerializeField] Image spatula;
    [SerializeField] Transform spatulaTransform;
    [SerializeField] Sprite smallPieceSpatulaSprite;
    [SerializeField] Sprite fullPieceSpatulaSprite;

    [Header("Settings")]
    public float resetAtDistance = 10f;
    public float movementSpeed = 0.15f;
    public float wrenchAtDistance = 0.1f;
    public int updatesPerFrame = 30;

    [Header("Events")]
    public UnityEvent OnSpatulaReachPizza;
    public UnityEvent OnSpatulaReset;

    [SerializeField]private Vector3 direction;
    private Vector3 startingPos;
    private int stage = 0;  //0 - off; 1 spatula moving in; 2 spatula moving out

    private void Start() {
        startingPos = transform.position;
        gameObject.SetActive(false);
    }

    //Methods
    public void Initialise(float angle) {
        spatula.sprite = angle > 180 ? fullPieceSpatulaSprite : smallPieceSpatulaSprite;

        stage = 1;
        direction = UtilityClass.AngleToVector(angle / 2f, transform);
        spatulaTransform.up = -direction;

        Vector3 pos = startingPos + direction.normalized * resetAtDistance * 0.9f;
        transform.position = pos;

        //Debug.Log(transform.position + " " + direction);

        circle.fillAmount = angle / 360f;
        gameObject.SetActive(true);
        StartCoroutine(MoveLoop());
        //Debug.Log("Called");
    }


    
    private void Reset() {
        OnSpatulaReset.Invoke();
        stage = 0;
        transform.gameObject.SetActive(false);
        //transform.position = startingPos;
    }

    public IEnumerator MoveLoop() {
        //Debug.Log(Vector3.Distance(transform.position, startingPos) + " " + startingPos + " " + transform.position);
        Vector3 dir = new Vector3(direction.x, direction.y, 0);
        while (Vector3.Distance(transform.position, startingPos) < resetAtDistance) {
            if (stage == 1) {
                circle.gameObject.SetActive(false);
                transform.position -= dir * movementSpeed * Time.deltaTime;
                spatulaTransform.position = circle.transform.position;

                if (Vector2.Distance(startingPos, transform.position) <= wrenchAtDistance) {
                    stage = 2;
                    OnSpatulaReachPizza.Invoke();
                }
            }
            else if(stage == 2) {
                circle.gameObject.SetActive(true);
                transform.position += dir * movementSpeed * Time.deltaTime;
                spatulaTransform.position = circle.transform.position;
            }
            yield return null;
        }
        Reset();
    }
}
