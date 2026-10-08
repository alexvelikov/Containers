using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers.src
{
    public static class Window
    {
        public static int Width = Console.WindowWidth;
        public static int Height = Console.WindowHeight;
        public static int DefaultWidth = 120;
        public static int DefaultHeight = 30;
        public static bool FullScreen { get { return fullScreen; } }
        public static bool fullScreen = false;

        public static void SetDefaultSizes()
        {
            Width = DefaultWidth;
            Height = DefaultHeight;
            fullScreen = false;
            SetSize();
        }

        public static void SetCustomSizes(int x, int y)
        {
            Width = x;
            Height = y;
            SetSize();
        }

        public static void SetLargestSizes()
        {
            Width = Console.LargestWindowWidth;
            Height = Console.LargestWindowHeight;
            fullScreen = true;
            SetSize();
        }

        private static void SetSize()
        {
            if (Height > Console.BufferHeight)
            {
                Console.SetWindowPosition(Console.WindowLeft, 0);
                Console.BufferHeight = Height;
                Console.WindowHeight = Height;
            }
            else
            {
                Console.WindowHeight = Height;
                Console.BufferHeight = Height;
            }

            if (Width > Console.BufferWidth)
            {
                Console.SetWindowPosition(0, Console.WindowTop);
                Console.BufferWidth = Width;
                Console.WindowWidth = Width;
            }
            else
            {
                Console.WindowWidth = Width;
                Console.BufferWidth = Width;
            }
        }
    }
}
