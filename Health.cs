using System;
using SplashKitSDK;

public class Health : Equipment
{
    public Health(Window window)
    {
        _image = SplashKit.LoadBitmap("Health", "Health.png");

        X = SplashKit.Rnd(window.Width - _image.Width);
        Y = SplashKit.Rnd(70, window.Height - _image.Height);
    }

    public override void Update(Player red, Player blue)
    {
        if (!_active) return;

        // Pick up the health
        if (CollidesWith(red) && red.Lives < 5)
        {
            _owner = red;
            red.Heal(1);
            ResetTimer();
            _active = false;
        }
        else if (CollidesWith(blue) && blue.Lives < 5)
        {
            _owner = blue;
            blue.Heal(1);
            ResetTimer();
            _active = false;
        }
    }
}
