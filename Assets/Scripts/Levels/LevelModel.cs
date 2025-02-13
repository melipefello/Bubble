class LevelModel
{
    readonly LevelInfo _levelInfo;
    public int RequiredScore => _levelInfo.RequiredScore;

    public int RemainingMoves { get; private set; }
    public int Score { get; private set; }

    public LevelModel(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;
        RemainingMoves = _levelInfo.MovesLimit;
    }

    public void DecreaseMove(int amount) => RemainingMoves -= amount;
    public void SetScore(int score) => Score = score;
}