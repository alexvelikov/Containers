namespace Containers.src
{
    public class Characters
    {
        public record struct BoxStyle(
        char Horizontal,   // ─
        char Vertical,     // │

        char TopLeft,      // ┌
        char TopRight,     // ┐
        char BottomLeft,   // └
        char BottomRight,  // ┘

        char TeeRight,     // ├
        char TeeLeft,      // ┤
        char TeeDown,      // ┬
        char TeeUp,        // ┴

        char Cross         // ┼
    )
        {
            // ───────── Single line ─────────
            public static readonly BoxStyle Single = new(
                Horizontal: '─',
                Vertical: '│',

                TopLeft: '┌',
                TopRight: '┐',
                BottomLeft: '└',
                BottomRight: '┘',

                TeeRight: '├',
                TeeLeft: '┤',
                TeeDown: '┬',
                TeeUp: '┴',

                Cross: '┼'
            );

            // ═════════ Double line ═════════
            public static readonly BoxStyle Double = new(
                '═', '║',
                '╔', '╗', '╚', '╝',
                '╠', '╣', '╦', '╩',
                '╬'
            );

            // ━━━━━━━━━ Heavy line ━━━━━━━━━
            public static readonly BoxStyle Heavy = new(
                '━', '┃',
                '┏', '┓', '┗', '┛',
                '┣', '┫', '┳', '┻',
                '╋'
            );

            // +--------- ASCII fallback ---------+
            public static readonly BoxStyle Ascii = new(
                '-', '|',
                '+', '+', '+', '+',
                '+', '+', '+', '+',
                '+'
            );
        }

        public record struct BlockStyle
            (
            char Full,
            char Dark,
            char Medium,
            char Light
        )
        {
            public static readonly BlockStyle Solid = new('█', '▓', '▒', '░');

            public static readonly BlockStyle Ascii = new('#', '#', '.', ' ');
        }

        public sealed record ArrowStyle(
            char Left,
            char Right,
            char Up,
            char Down,
            char Bullet
        )
        {
            public static readonly ArrowStyle Unicode = new('◄', '►', '▲', '▼', '•');

            public static readonly ArrowStyle Ascii = new('<', '>', '^', 'v', '*');
        }

        public sealed record StatusStyle(
            char Ok,
            char Error,
            char Warning,
            char Info
        )
        {
            public static readonly StatusStyle Unicode = new('✔', '✖', '⚠', 'ℹ');

            public static readonly StatusStyle Ascii = new('V', 'X', '!', 'i');
        }

        public static char Scale = '□';
    }
}