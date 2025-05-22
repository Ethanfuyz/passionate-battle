using System;
using SplashKitSDK;

namespace PassionateBattle
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Passionate Battle", 1080, 720);
            PassionateBattleGame game = new PassionateBattleGame(gameWindow);

            while (!gameWindow.CloseRequested && !game.Quit)
            {
                SplashKit.ProcessEvents();

                game.HandleInput();
                game.Update();
                game.Draw();
            }

            gameWindow.Close();
        }
    }
}
