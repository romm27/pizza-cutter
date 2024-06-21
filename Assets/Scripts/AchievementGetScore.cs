using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement/Create New Score Achievement")]
public class AchievementGetScore : Achievement
{
    [Header("References")]
    public GameObjectContainer gameManagerContainer;

    public enum CheckType { PointsEqualTo, MorePointsThan}
    public enum ScopeType { ThisQuestion, EntireScore}

    [Header("Get Pointd Question Settings")]
    public ScopeType scopeType = ScopeType.EntireScore;
    public CheckType checkType = CheckType.MorePointsThan;
    public int unlockAt = 1000;

    private GameManager gameManager;

    //Methods
    public override void UpdateReferences() {
        if (gameManagerContainer.content != null) {
            gameManager = gameManagerContainer.content.GetComponent<GameManager>();
        }
    }
    public override bool CheckCompletion() {
        if(gameManager == null) {
            UpdateReferences();
        }

        switch (scopeType) {
            case (ScopeType.EntireScore):
                switch (checkType) {
                    case CheckType.PointsEqualTo:
                        return unlockAt == gameManager.currentScore;
                    case CheckType.MorePointsThan:
                        return unlockAt <= gameManager.currentScore;
                    default:
                        Debug.Log("Something went wrong");
                        return false;
                }
            case (ScopeType.ThisQuestion):
                switch (checkType) {
                    case CheckType.PointsEqualTo:
                        return unlockAt == gameManager.lastAswnerScore;
                    case CheckType.MorePointsThan:
                        return unlockAt <= gameManager.lastAswnerScore;
                    default:
                        Debug.Log("Something went wrong");
                        return false;
                }
            default:
                Debug.Log(("Something went wrong"));
                return false;
        }
    }
}
