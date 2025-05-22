using System;
using SplashKitSDK;

public abstract class Equipment
{
    protected double X, Y;
    protected Bitmap _image;
    protected bool _active = true;
    protected Player _owner;
    protected double _spawnTime;
    public Player Owner => _owner;

    public bool Active => _active;

    protected Equipment()
    {
        _spawnTime = SplashKit.TimerTicks("Global");
    }

    protected void ResetTimer()
    {
        _spawnTime = SplashKit.TimerTicks("Global");
    }

    public abstract void Update(Player red, Player blue);

    public virtual void Draw()
    {
        if (_active)
            SplashKit.DrawBitmap(_image, X, Y);
    }

    public bool CollidesWith(Player player)
    {
        return SplashKit.BitmapCollision(_image, X, Y, player.PlayerBitmap, player.X, player.Y);
    }

    public bool CollidesWith(Equipment other)
    {
        return SplashKit.BitmapCollision(_image, X, Y, other._image, other.X, other.Y);
    }

    public bool ShouldBeRemoved()
    {
        int time = 10 * 1000;
        // Check if time/1000 seconds have passed since spawn
        bool shouldRemove = (SplashKit.TimerTicks("Global") - _spawnTime) > time;
        
        if (shouldRemove && _active)
        {
            _active = false;
            
            // If this is a shield, we need to remove it from the player
            if (this is Shield && _owner != null)
            {
                _owner.RemoveShield();
                _owner = null;
            }
        }
        
        return shouldRemove;
    }
}
