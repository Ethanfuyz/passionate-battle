using System;
using SplashKitSDK;

public class PlayerRed : Player
{
    public PlayerRed(Window window) : base(window)
    {
        _frontImage = SplashKit.LoadBitmap("RedFront", "Red_front.png");
        _backImage = SplashKit.LoadBitmap("RedBack", "Red_back.png");
        _leftImage = SplashKit.LoadBitmap("RedLeft", "Red_left.png");
        _rightImage = SplashKit.LoadBitmap("RedRight", "Red_right.png");

        _playerBitmap = _frontImage;
        X = (window.Width - PlayerWidth) / 4;
        Y = (window.Height - PlayerHeight) / 2;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        int speed = 5;
        if (SplashKit.KeyDown(KeyCode.WKey)) { Y -= speed; Direction = "back"; }
        else if (SplashKit.KeyDown(KeyCode.SKey)) { Y += speed; Direction = "front"; }
        else if (SplashKit.KeyDown(KeyCode.AKey)) { X -= speed; Direction = "left"; }
        else if (SplashKit.KeyDown(KeyCode.DKey)) { X += speed; Direction = "right"; }
    }

    public override void DrawLives()
    {

        Bitmap heartRed = new Bitmap("HeartRed", "Heart_red.png");
        Bitmap heartGrey = new Bitmap("HeartGrey", "Heart_grey.png");

        for (int i = 0; i < 5; i++)
        {
            if (i < _lives)
            {
                SplashKit.DrawBitmap(heartRed, 10 + i * 60, 10);
            }
            else
            {
                SplashKit.DrawBitmap(heartGrey, 10 + i * 60, 10);
            }
        }
    }
}
