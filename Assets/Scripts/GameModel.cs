class GameModel
{
    public int LevelProgress { get; private set; }

    public void IncreaseProgress() => LevelProgress++;
    public void ResetProgress() => LevelProgress = 0;
}