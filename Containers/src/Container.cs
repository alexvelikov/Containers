using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Containers.src
{
    public class Container
    {
        public List<Text>[] Content;
        public int Width;
        public int Height;

        public List<Text>[] DefaultContent
        {
            get
            {
                List<Text>[] content = new List<Text>[Height];
                for(int i = 0; i < Height; i++)
                {
                    content[i] = new List<Text>();
                    Text text = new Text
                    {
                        Content = new string(' ', Width),
                        ForegroundColor = ColorsManager.DefaultForegroundColor,
                        BackgroundColor = ColorsManager.DefaultBackgroundColor
                    };
                    content[i].Add(text);
                }

                return content;
            }
        }

        public List<Text>[] DefaultContentWithColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            List<Text>[] content = new List<Text>[Height];
            for (int i = 0; i < Height; i++)
            {
                content[i] = new List<Text>();
                Text text = new Text
                {
                    Content = new string(' ', Width),
                    ForegroundColor = foregroundColor,
                    BackgroundColor = backgroundColor
                };
                content[i].Add(text);
            }

            return content;
        }

        public List<Text>[] DefaultContentWithBackground(ConsoleColor backgroundColor)
        {
            List<Text>[] content = new List<Text>[Height];
            for (int i = 0; i < Height; i++)
            {
                content[i] = new List<Text>();
                Text text = new Text
                {
                    Content = new string(' ', Width),
                    ForegroundColor = ColorsManager.DefaultForegroundColor,
                    BackgroundColor = backgroundColor
                };
                content[i].Add(text);
            }

            return content;
        }

        public List<Text>[] DefaultContentWithTextColor(ConsoleColor foreground)
        {
            List<Text>[] content = new List<Text>[Height];
            for (int i = 0; i < Height; i++)
            {
                content[i] = new List<Text>();
                Text text = new Text
                {
                    Content = new string(' ', Width),
                    ForegroundColor = foreground,
                    BackgroundColor = ColorsManager.DefaultForegroundColor
                };
                content[i].Add(text);
            }

            return content;
        }


        public void AlterContent(int row, string text)
        {
            if (row < Content.Length)
            {
                var substrings = ColorsManager.GetSubstrings(text);

                Content[row].Clear();
                foreach(var substring in substrings)
                {
                    Content[row].Add(substring);
                }
            }
        }

        public void AlterContent(int row, List<Text> content)
        {
            if (row < Content.Length)
            {
                Content[row].Clear();
                foreach (var textItem in content)
                {
                    Content[row].Add(textItem);
                }
            }
        }

        public void AppendContent(int row, string text)
        {
            if (row < Content.Length)
            {
                var substrings = ColorsManager.GetSubstrings(text);

                foreach (var substring in substrings)
                {
                    Content[row].Add(substring);
                }
            }
        }

        public void AppendContent(int row, List<Text> content)
        {
            if (row < Content.Length)
            {
                foreach (var textItem in content)
                {
                    Content[row].Add(textItem);
                }
            }
        }

        public void PrintLine(int row)
        {
            Print(row);
            Console.WriteLine();
        }

        public void Print(int row)
        {
            int currentLength = 0;

            foreach (var item in Content[row])
            {
                Console.ForegroundColor = item.ForegroundColor;
                Console.BackgroundColor = item.BackgroundColor;
                string contentToPrint = item.Content;

                if(currentLength + contentToPrint.Length > Width)
                {
                    contentToPrint = contentToPrint.Substring(0, Width - currentLength);
                    Console.Write(contentToPrint);
                    break;
                }

                Console.Write(contentToPrint);
                currentLength += contentToPrint.Length;
            }

            if(currentLength < Width)
            {
                Console.Write(new string(' ', Width - currentLength));
            }
        }
    }
}