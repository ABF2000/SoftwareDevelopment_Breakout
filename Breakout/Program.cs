﻿using DIKUArcade.GUI;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Galaga v0.1" };
            var game = new Game(windowArgs);
            game.Run();
        }
    }
}