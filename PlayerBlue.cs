using System;
using SplashKitSDK;

public class PlayerBlue : Player
{
    public PlayerBlue(Window window) : base(window)
    {
        _frontImage = SplashKit.LoadBitmap("BlueFront", "Blue_front.png");
        _backImage = SplashKit.LoadBitmap("BlueBack", "Blue_back.png");
        _leftImage = SplashKit.LoadBitmap("BlueLeft", "Blue_left.png");
        _rightImage = SplashKit.LoadBitmap("BlueRight", "Blue_right.png");

        _playerBitmap = _frontImage;
        X = (window.Width - PlayerWidth) / 4 * 3;
        Y = (window.Height - PlayerHeight) / 2;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        int speed = 5;
        if (SplashKit.KeyDown(KeyCode.UpKey)) { Y -= speed; Direction = "back"; }
        else if (SplashKit.KeyDown(KeyCode.DownKey)) { Y += speed; Direction = "front"; }
        else if (SplashKit.KeyDown(KeyCode.LeftKey)) { X -= speed; Direction = "left"; }
        else if (SplashKit.KeyDown(KeyCode.RightKey)) { X += speed; Direction = "right"; }
    }

    public override void DrawLives()
    {
        Bitmap heartBlue = new Bitmap("HeartBlue", "Heart_blue.png");
        Bitmap heartGrey = new Bitmap("HeartGrey", "Heart_grey.png");

        for (int i = 0; i < 5; i++)
        {
            if (i < _lives)
            {
                SplashKit.DrawBitmap(heartBlue, 750 + i * 60, 10);
            }
            else
            {
                SplashKit.DrawBitmap(heartGrey, 750 + i * 60, 10);
            }
        }
    }
}