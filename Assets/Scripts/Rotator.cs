using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour
{
    private bool _isClicking;

    [Header("References")]
    public GameManager gameManager;
    public Image circle;

    [Header("Settings")]
    public float rotationRange = 5f;

    [Header("Imported")]
    [SerializeField] private Transform _rudderTransform;
    [SerializeField] private Canvas _canvas;

    private Vector3 _rudderScreenPosition;
    private Vector3 startingPos;
    private float frameAngleChange;
    private float prePreviusFrame;
    private float previusFrame;

    public float FrameAngleChange {
        get {
            //Debug.Log(frameAngleChange);
            return frameAngleChange;
        }
    }

    public float Angle {
        get {
            return 360 - transform.eulerAngles.z;
        }
        set {
            Vector3 rot = transform.eulerAngles;
            rot.z = 360 - value;
            transform.eulerAngles = rot;
        }
    }

    private void Start() {
        RecalculatePosition();
    }

    private void Update() {
        if(transform.position != startingPos) {
            RecalculatePosition();
        }

        if (Input.GetMouseButtonDown(0)) {
            _isClicking = true;
        }
        else if (Input.GetMouseButtonUp(0)) {
            _isClicking = false;
        }
        if (!_isClicking) {
            return;
        }

        //Debug.Log($"mousePosition: {Input.mousePosition} rudder: {_rudderScreenPosition}");

        Quaternion tempRot = _rudderTransform.rotation;
        Vector3 v3angle = (Input.mousePosition - _rudderScreenPosition).normalized;
        if (gameManager.Waiting && MouseDistanceFromCenter() < rotationRange) {

            Quaternion currentAngle = Quaternion.LookRotation(Vector3.forward, v3angle);
            _rudderTransform.rotation = currentAngle;

            prePreviusFrame = previusFrame;
            previusFrame = _rudderTransform.transform.eulerAngles.z; //float.Parse(_rudderTransform.transform.eulerAngles.z.ToString());

            //full circle corrections
            frameAngleChange = previusFrame - prePreviusFrame;
            if (frameAngleChange > 100f) {
                frameAngleChange -= 360f;
            }
            else if (frameAngleChange < -100f) {
                frameAngleChange += 360f;
            }

            //if(frameAngleChange > pullAngleLimit) {
            //   _rudderTransform.rotation = tempRot;
            //   frameAngleChange = 0;
            //}
            UpdateCircle();
        }

    }

    //Methods
    public void UpdateCircle() {
        circle.fillAmount = Angle / 360f;
    }

    public float MouseDistanceFromCenter() {
        float multiplier = _canvas.scaleFactor;
        return Vector2.Distance(_rudderScreenPosition, Input.mousePosition) / multiplier;
    }

    public void AddToRotation(float value) {
        Angle += value;
        UpdateCircle();
    }

    private void RecalculatePosition() {
        startingPos = transform.position;
        switch (_canvas.renderMode) {
            case RenderMode.ScreenSpaceOverlay:
                _rudderScreenPosition = _rudderTransform.position;
                break;
            case RenderMode.ScreenSpaceCamera:
                Vector3 pos = new Vector3(_rudderTransform.position.x,
                    _rudderTransform.position.y,
                    -_canvas.planeDistance
                );
                _rudderScreenPosition = Camera.main.WorldToScreenPoint(pos);
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

}
