using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI loadingPercentage;
    [SerializeField] Image spinner;
    [SerializeField] LoadingManager loadingManager;
    [SerializeField] Animator slashInPizzaAnimator;

    [Header("Settings")]
    public float spinSpeed = 3.5f;
    public float loadingTestDelay = 5f;


    private AsyncOperation loadingOperation;
    private float animationTime = 0;

    private void Awake() {
        StartCoroutine(LoadingTestDelay());
    }

    private void Start() {
        slashInPizzaAnimator.Play("main");
        animationTime = slashInPizzaAnimator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(OnSlashInFinished());
    }

    private void Update() {
        if (spinner.gameObject.activeInHierarchy) {
            spinner.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * spinSpeed);
        }

        if(loadingOperation != null) {
            loadingPercentage.text = "Loading... " + Mathf.RoundToInt(loadingOperation.progress * 10) + "%";
        }
    }

    IEnumerator OnSlashInFinished() {
        yield return new WaitForSeconds(animationTime);
        spinner.gameObject.SetActive(true);
        Destroy(slashInPizzaAnimator.gameObject);
    }

    IEnumerator LoadingTestDelay() {
        yield return new WaitForSeconds(loadingTestDelay);
        loadingOperation = SceneManager.LoadSceneAsync(loadingManager.calledScene);
    }
}
