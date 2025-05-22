using System;
using SplashKitSDK;

public class Shield : Equipment
{
    public Shield(Window window)
    {
        _image = SplashKit.LoadBitmap("Shield", "Shield.png");

        X = SplashKit.Rnd(window.Width - _image.Width);
        Y = SplashKit.Rnd(70, window.Height - _image.Height);
    }

    public override void Update(Player red, Player blue)
    {
        if (!_active) return;

        // Pick up the shield
        if (_owner == null)
        {
            if (CollidesWith(red) && !red.HasShield)
            {
                _owner = red;
                red.AssignShield();
                ResetTimer();
            }
            else if (CollidesWith(blue) && !blue.HasShield)
            {
                _owner = blue;
                blue.AssignShield();
                ResetTimer();
            }
            else return;
        }

        // Follow the _owner if shield is still active
        if (_owner != null && _active)
        {
            X = _owner.X - (_image.Width - _owner.PlayerWidth) / 2;
            Y = _owner.Y - (_image.Height - _owner.PlayerHeight) / 2;
        }
    }

    public void TryAbsorb(Sword sword)
    {
        if (_active && sword.Active && _owner != null && sword.Owner != null && CollidesWith(sword))
        {
            _active = false;
            _owner.RemoveShield();
            _owner = null;
            sword.Deactivate();
        }
    }
}