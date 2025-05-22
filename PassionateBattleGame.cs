using System;
using SplashKitSDK;

public class PassionateBattleGame
{
    private Window _gameWindow;
    private Bitmap _background;

    private Bitmap _winRed;
    private Bitmap _winBlue;

    private Player _playerRed;
    private Player _playerBlue;

    List<Equipment> _equipments = new List<Equipment>();

    public bool Quit => _playerRed.Quit || _playerBlue.Quit;

    public PassionateBattleGame(Window window)
    {
        _gameWindow = window;
        _background = SplashKit.LoadBitmap("Background", "GameWindowBackground.png");
        _winRed = SplashKit.LoadBitmap("WinRed", "Win_red.png");
        _winBlue = SplashKit.LoadBitmap("WinBlue", "Win_blue.png");

        // Create and start the global timer
        SplashKit.CreateTimer("Global");
        SplashKit.StartTimer("Global");

        _playerRed = new PlayerRed(_gameWindow);
        _playerBlue = new PlayerBlue(_gameWindow);
    }

    public void HandleInput()
    {
        _playerRed.HandleInput();
        _playerBlue.HandleInput();

        _playerRed.StayOnWindow();
        _playerBlue.StayOnWindow();
    }

    public void Draw()
    {
        SplashKit.ClearScreen(Color.White);
        SplashKit.DrawBitmap(_background, 0, 0);

        // Draw the equipments
        foreach (Equipment e in _equipments)
        {
            e.Draw();
        }

        // Draw the players
        _playerRed.Draw();
        _playerRed.DrawLives();
        _playerBlue.Draw();
        _playerBlue.DrawLives();

        // Draw the win screen
        if (_playerRed.Result)
        {
            SplashKit.DrawBitmap(_winBlue, 0, 0);
        }
        else if (_playerBlue.Result)
        {
            SplashKit.DrawBitmap(_winRed, 0, 0);
        }

        SplashKit.RefreshScreen(60);
    }

    public void Update()
    {
        // Each frame has a 0.01 chance to add a Equipment in the _equipments list
        if (SplashKit.Rnd() < 0.01)
        {
            _equipments.Add(RandomEquipment());
        }

        // Create a list to store equipment that should be removed
        List<Equipment> equipmentsToRemove = new List<Equipment>();

        // Move Equipments in the _equipments list
        foreach (Equipment e in _equipments)
        {
            e.Update(_playerRed, _playerBlue);

            // Try to absorb the sword
            if (e is Shield shield)
            {
                foreach (Equipment other in _equipments)
                {
                    if (other is Sword sword)
                    {
                        shield.TryAbsorb(sword);
                    }
                }
            }

            // Check if equipment should be removed due to time
            if (e.ShouldBeRemoved())
            {
                equipmentsToRemove.Add(e);
            }
        }

        // Remove expired equipment
        foreach (Equipment e in equipmentsToRemove)
        {
            _equipments.Remove(e);
        }
    }

    public Equipment RandomEquipment()
    {
        double random = SplashKit.Rnd();

        if (random < 0.6)
        {
            return new Sword(_gameWindow);
        }
        else if (random < 0.9)
        {
            return new Shield(_gameWindow);
        }
        else
        {
            return new Health(_gameWindow);
        }
    }
}