using System;
using SplashKitSDK;

public abstract class Player
{
    public double X { get; protected set; }
    public double Y { get; protected set; }

    protected int _gameWindowWidth, _gameWindowHeight;

    protected string Direction;
    protected Bitmap _frontImage, _backImage, _leftImage, _rightImage;
    protected Bitmap _playerBitmap;

    public Bitmap PlayerBitmap => _playerBitmap;

    public int PlayerWidth => _playerBitmap.Width;
    public int PlayerHeight => _playerBitmap.Height;

    // Detect if there is a shield
    protected bool _hasShield = false;
    public bool HasShield => _hasShield;
    public void AssignShield() => _hasShield = true;
    public void RemoveShield() => _hasShield = false;

    public bool Quit { get; protected set; }
    public bool Result { get; protected set; }

    // Life functions
    protected int _lives = 5;
    public int Lives => _lives;

    private delegate void LifeChangeHandler(int amount);
    private readonly LifeChangeHandler _loseLife;
    private readonly LifeChangeHandler _heal;

    public Player(Window window)
    {
        Direction = "front";
        _gameWindowWidth = window.Width;
        _gameWindowHeight = window.Height;

        _loseLife = (amount) => {
            _lives -= amount;
            if (_lives <= 0) Result = true;
        };

        _heal = (amount) => {
            if (_lives < 5) _lives += amount;
        };
    }

    public virtual void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.EscapeKey))
            Quit = true;
    }

    public void Draw()
    {
        switch (Direction)
        {
            case "front":
                _playerBitmap = _frontImage;
                break;
            case "back":
                _playerBitmap = _backImage;
                break;
            case "left":
                _playerBitmap = _leftImage;
                break;
            case "right":
                _playerBitmap = _rightImage;
                break;
        }

        _playerBitmap.Draw(X, Y);
    }

    public void StayOnWindow()
    {
        // if (X < 0 - PlayerWidth) X = _gameWindowWidth;
        // else if (X > _gameWindowWidth) X = -PlayerWidth;
        // else if (Y < 0 - PlayerHeight) Y = _gameWindowHeight;
        // else if (Y > _gameWindowHeight) Y = -PlayerHeight;

        if (X < 0) X = 0;
        else if (X > _gameWindowWidth - PlayerWidth) X = _gameWindowWidth - PlayerWidth;
        else if (Y < 0) Y = 0;
        else if (Y > _gameWindowHeight - PlayerHeight) Y = _gameWindowHeight - PlayerHeight;
    }

    public void LoseLife(int amount) => _loseLife(amount);
    public void Heal(int amount) => _heal(amount);

    public abstract void DrawLives();
}