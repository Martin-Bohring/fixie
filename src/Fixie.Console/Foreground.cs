﻿using System;

namespace Fixie.ConsoleRunner
{
    public class Foreground : IDisposable
    {
        readonly ConsoleColor before;

        public Foreground(ConsoleColor color)
        {
            before = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = before;
        }

        public static Foreground Red => new Foreground(ConsoleColor.Red);

        public static Foreground Yellow => new Foreground(ConsoleColor.Yellow);
    }
}