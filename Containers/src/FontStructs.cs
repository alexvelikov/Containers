using System;
using System.Collections.Generic;
using System.Text;
using c = System.ConsoleColor;

namespace Containers.src
{
    public readonly record struct Font(ConsoleColor f, ConsoleColor b);

    public class ButtonStatesFonts
    {
        public Font Default { get; set; }
        public Font Selected { get; set; }
        public Font Unactive { get; set; }
    }

    public class UIFonts<TKey> where TKey : Enum
    {
        public Dictionary<TKey, Font> UIElementsFontsDict = new();
        public string ReturnFontCodeColorFromElement(TKey element)
        {
            return ColorsManager.BuildTextCode(string.Empty, UIElementsFontsDict[element]);
        }
    }

    public enum DefaultUIElements
    {
        Text, Header, Value,
        HeavyLine, DoubleLine, Line,
        Background
    }
}
