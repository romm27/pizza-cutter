using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement/Create New Reach Question Achievement")]
public class AchievementGetToQuestion : Achievement
{
    [Header("References")]
    public GameObjectContainer gameManagerContainer;

    [Header("Get To Question Settings")]
    public int unlockAt = 10;

    GameManager gameManager;

    public override void UpdateReferences() {
        if (gameManagerContainer.content != null) {
            gameManager = gameManagerContainer.content.GetComponent<GameManager>();
        }
    }

    //Methods
    public override bool CheckCompletion() {
        if(gameManager == null) {
            UpdateReferences();
        }
        return gameManager.currentQuestion >= unlockAt;
    }
}
