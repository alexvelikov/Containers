using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Containers.src
{
    public abstract class ButtonsLayout
    {

        public Button[][] ButtonsArray = new Button[0][];
        public int LastCol = 0;
        public int LastRow = 0;
        private sbyte _actionIdAwaiting = -1;
        public sbyte ActionIdAwaiting { get { return _actionIdAwaiting; } }

        public virtual void ResetAwaitingResponse() { _actionIdAwaiting = -1; }

        public virtual void MoveSelectorToCoords(int newRow, int newCol)
        {
            ButtonsArray[LastRow][LastCol].Selected = false;
            ButtonsArray[newRow][newCol].Selected = true;
        }

        public void MoveSelectorToID(int buttonID)
        {
            ButtonsArray[LastRow][LastCol].Selected = false;
            bool found = false;
            for (int i = 0; i < ButtonsArray.Length && !found; i++)
            {
                for (int j = 0; j < ButtonsArray[i].Length && !found; j++)
                {
                    if (ButtonsArray[i][j].ID == buttonID)
                    {
                        LastRow = i;
                        LastCol = j;
                        ButtonsArray[i][j].Selected = true;
                        found = true;
                    }
                }
            }
        }

        public abstract void Setup();

        public virtual string GetButtonFontCode(byte buttonID, ButtonStatesFonts font)
        {
            var buttonFonts = font;
            foreach (var button in ButtonsArray.SelectMany(buttons => buttons))
            {
                if (button.ID == buttonID)
                {
                    ConsoleColor foregroundColor = buttonFonts.Default.f;
                    ConsoleColor backgroundColor = buttonFonts.Default.b;
                    if (button.Selected)
                    {
                        foregroundColor = buttonFonts.Selected.f;
                        backgroundColor = buttonFonts.Selected.b;
                    }
                    if (button.Unactive)
                    {
                        foregroundColor = buttonFonts.Unactive.f;
                        backgroundColor = buttonFonts.Unactive.b;
                    }

                    return ColorsManager.BuildTextCode(
                        text: string.Empty,
                        foregroundColor: foregroundColor,
                        backgroundColor: backgroundColor);
                }
            }

            return "<Default>";
        }

        public virtual void ProcessInput(ConsoleKeyInfo input)
        {
            int col = LastCol;
            int row = LastRow;
            ButtonNavigationDirections directions = new ButtonNavigationDirections();

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    directions = ButtonNavigationDirections.Top;
                    row--;
                    break;
                case ConsoleKey.DownArrow:
                    directions = ButtonNavigationDirections.Bottom;
                    row++;
                    break;
                case ConsoleKey.LeftArrow:
                    directions = ButtonNavigationDirections.Left;
                    col--;
                    break;
                case ConsoleKey.RightArrow:
                    directions = ButtonNavigationDirections.Right;
                    col++;
                    break;
                case ConsoleKey.Enter:
                    _actionIdAwaiting = ButtonsArray[row][col].ID;
                    break;
            }

            while (BordersNotCrossed(ref row, ref col)) { }

            while (ButtonsArray[row][col].Unactive)
            {
                switch (directions)
                {
                    case ButtonNavigationDirections.Top:
                        row--;
                        break;
                    case ButtonNavigationDirections.Bottom:
                        row++;
                        break;
                    case ButtonNavigationDirections.Left:
                        col--;
                        break;
                    case ButtonNavigationDirections.Right:
                        col++;
                        break;
                }
                if (BordersNotCrossed(ref row, ref col))
                {
                    row = LastRow;
                    col = LastCol;
                    break;
                }
            }

            ButtonsArray[LastRow][LastCol].Selected = false;
            LastCol = col;
            LastRow = row;
            ButtonsArray[row][col].Selected = true;
        }

        protected virtual bool BordersNotCrossed(ref int row, ref int col)
        {
            bool borderCrossed = false;
            if (row >= ButtonsArray.Length)
            {
                borderCrossed = true;
                row--;
            }
            if (row <= -1)
            {
                borderCrossed = true;
                row = 0;
            }
            if (col >= ButtonsArray[row].Length)
            {
                borderCrossed = true;
                col--;
            }
            if (col <= -1)
            {
                borderCrossed = true;
                col = 0;
            }
            return borderCrossed;
        }
    }

    public struct Button
    {
        public sbyte ID;
        public bool Unactive;
        public bool Selected;
    }

    public enum ButtonNavigationDirections
    {
        Left, Top, Right, Bottom
    }
}
