using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSO", menuName = "ScriptableObjects/GameState/GameStateSO")]
public class GameStateSO : ScriptableObject {
    #region Variables
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    #endregion Variables
}

