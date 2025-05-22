using System;
using SplashKitSDK;

public class Sword : Equipment
{
    private double _angle;

    public Sword(Window window)
    {
        _image = SplashKit.LoadBitmap("Sword", "Sword.png");

        X = SplashKit.Rnd(window.Width - _image.Width);
        Y = SplashKit.Rnd(70, window.Height - _image.Height);
    }

    public void Deactivate()
    {
        _active = false;
        _owner = null;  // Clear owner when sword is deactivated
    }

    public override void Update(Player red, Player blue)
    {
        if (!_active) return;

        // Pickup the Sword
        if (_owner == null)
        {
            if (CollidesWith(red))
            {
                _owner = red;
                ResetTimer();
            }
            else if (CollidesWith(blue))
            {
                _owner = blue;
                ResetTimer();
            }
            else return;
        }

        // Rotate around _owner
        if (_owner != null && _active)
        {
            _angle += 0.1;
            X = _owner.X + Math.Cos(_angle) * 100 + _owner.PlayerWidth / 2 - _image.Width / 2;
            Y = _owner.Y + Math.Sin(_angle) * 100 + _owner.PlayerHeight / 2 - _image.Height / 2;
        }

        // Use the ternary operator to select the opposing player
        Player target = (_owner == red) ? blue : red;

        if (CollidesWith(target))
        {
            target.LoseLife(1);
            _active = false;
            _owner = null; 
        }
    }

    public override void Draw()
    {
        if (_active)
        {
            if (_owner != null)
                _image.Draw(X, Y, SplashKit.OptionRotateBmp(((_angle * 360 / (2 * Math.PI)) % 360) + 90));
            else
                _image.Draw(X, Y, SplashKit.OptionRotateBmp(0));
        }
    }
}