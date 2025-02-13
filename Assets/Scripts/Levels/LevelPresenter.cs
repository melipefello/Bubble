using TMPro;
using UnityEngine;

class LevelPresenter : MonoBehaviour
{
    [SerializeField] TMP_Text _movesText;
    [SerializeField] TMP_Text _scoreText;

    public void UpdateMoves(LevelModel model)
    {
        _movesText.SetText($"{model.RemainingMoves}");
    }

    public void UpdateScore(LevelModel model)
    {
        _scoreText.SetText($"{model.Score}/{model.RequiredScore}");
    }
}