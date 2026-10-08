# Console UI Framework — A brief documentation on Containers project

## 1. `Container` — the core drawing surface

A `Container` represents a rectangular block of console output — think of it as a tile in a grid-based UI.

**Fields**
- `int Width`, `int Height` — the intended dimensions of the tile.
- `List<Text>[] Content` — an array sized to `Height`; each index is one row, holding a list of colored text segments for that row.

`Text` is an internal building block (`string Content`, `ConsoleColor Foreground`, `ConsoleColor Background`) — it's never constructed or edited directly. All content goes in through `Container`'s methods.

### Entering content — tagged strings

Content is typically written as a string with inline color tags:

```csharp
container.AlterContent(0, "<Red_White>Error: <Default>file not found");
```

- A tag is written `<...>` and colors everything from the end of that tag to the next tag (or end of string).
- `<Default>` — fixed global White foreground / Black background.
- `<White>` (foreground only) — foreground = White, background always defaults to **Black**.
- `<Red_White>` (foreground `_` background) — both explicit, using exact `ConsoleColor` enum names.
- **A string must start with a tag.** Anything before the first tag is discarded — no `Text` is built until a tag is encountered.

### Population methods

All operate on a single row:

| Method | Effect |
|---|---|
| `AlterContent(int row, string content)` | Parses tagged string, **replaces** row |
| `AlterContent(int row, List<Text> content)` | Replaces row with raw content directly (e.g. copying a row from another container: `a.AlterContent(0, b.Content[0]);`) |
| `AppendContent(int row, string content)` | Parses tagged string, **appends** to row |
| `AppendContent(int row, List<Text> content)` | Appends raw content to row |

**Width enforcement**: Not checked at insertion — you can insert content longer than `Width`. Overflow is only truncated at print time.

### Printing

- `Print(int row)` — writes a row's content with correct colors, no trailing newline.
- `PrintLine(int row)` — same, with a trailing newline.
- Both are just `Console.Write`/`Console.WriteLine` under the hood, with color switching per segment.

**No built-in layout engine.** Positioning multiple containers relative to each other (side-by-side or stacked) is entirely manual:

- **Side-by-side**: either interleave `Print` calls row-by-row (since `Print` doesn't newline), or use `Console.SetCursorPosition()` before each row.
- **Stacked**: print all rows of one container, then all rows of the next.
- **Known trap**: side-by-side containers with different `Height`s — printing in naive lockstep causes the taller one's overflow rows to wrap back to column 0 instead of staying in their column. Fix with one of: a filler container to occupy the gap, padding the shorter container with a blank row, or explicit `Console.SetCursorPosition()` for the overflow rows.

---

## 2. `Characters` — glyph sets for visuals

A static class providing character sets for building borders, bars, and indicators, meant to be embedded into the tagged strings fed into `Container`.

- **`BoxStyle`** (record struct): `Horizontal`, `Vertical`, `TopLeft/Right`, `BottomLeft/Right`, `TeeRight/Left/Down/Up`, `Cross`. Presets: `Single`, `Double`, `Heavy`, `Ascii`.
- **`BlockStyle`** (record struct): `Full`, `Dark`, `Medium`, `Light`. Presets: `Solid`, `Ascii`.
- **`ArrowStyle`** (sealed record): `Left`, `Right`, `Up`, `Down`, `Bullet`. Presets: `Unicode`, `Ascii`.
- **`StatusStyle`** (sealed record): `Ok`, `Error`, `Warning`, `Info`. Presets: `Unicode`, `Ascii`.
- **`Scale`** — a single standalone char (`'□'`).

Each family has an ASCII fallback variant for environments without extended character support.

---

## 3. `Button` / `ButtonsLayout` — arrow-key navigation

`Button` is a simple struct: `sbyte ID`, `bool Selected`, `bool Unactive`.

`ButtonsLayout` is an **abstract base class** — a pure state/logic manager for keyboard navigation. It does **no rendering** of its own. To use it, you create a subclass per screen (e.g. a menu), implement `Setup()` to populate the button grid, and the base class handles moving a "selector" around in response to input.

**Fields**
- `Button[][] ButtonsArray` — jagged grid of buttons for this screen.
- `int LastRow`, `int LastCol` — position of the currently selected button.

**Methods**

| Member | Purpose |
|---|---|
| `abstract Setup()` | Subclass-specific; populates `ButtonsArray` for that screen's layout |
| `virtual ProcessInput(ConsoleKeyInfo input)` | Reads arrow-key input, checks whether moving from `(LastRow, LastCol)` in that direction lands on a valid button (exists, not `Unactive`); if so, deselects the current button and selects the new one, updating `LastRow`/`LastCol` |
| `virtual MoveSelectorToCoords(int newRow, int newCol)` | Jumps selection to explicit coordinates, same select/deselect logic |
| `virtual MoveSelectorToID(int buttonID)` | Jumps selection to the button with the given `ID` |
| `virtual ResetAwaitingResponse()` | Resets an "awaiting confirmation" state after a button action has been processed |
| `virtual GetButtonFontCode(int buttonID, ButtonStatesFonts font)` | Returns the color/font code appropriate to a button's current state (e.g. selected vs. normal vs. unactive), for use when rendering that button |

**Key point**: navigation state changes (selection moving, etc.) don't automatically redraw anything. The developer must manually re-render the affected UI (typically via `Container.AlterContent`/`AppendContent` with tagged strings), using `GetButtonFontCode` to determine the correct colors per button's current state.

---

## 4. `Font` / `UIFonts<TKey>` — centralized theming

- **`Font`** — a readonly record struct: `ConsoleColor f` (foreground), `ConsoleColor b` (background). The atomic color pairing used throughout.
- **`UIFonts<TKey>`** (generic, `TKey : Enum`) — holds a `Dictionary<TKey, Font>` mapping semantic UI roles (e.g. "Header", "Line") to a `Font`. `ReturnFontCodeColorFromElement(TKey element)` looks up the role's `Font` and converts it into a tag-string color code (via `ColorsManager.BuildTextCode`), ready to prepend to content passed into `AlterContent`/`AppendContent`.
- **`DefaultUIElements`** — a general-purpose enum of common roles: `Text`, `Header`, `Value`, `HeavyLine`, `DoubleLine`, `Line`, `Background`. Custom enums can be used instead per project, since `UIFonts<TKey>` is generic.

**Purpose**: centralizing colors in one `UIFonts` instance means a single dictionary edit restyles every element using that role, consistently, across the whole screen. Code should always resolve colors through `ReturnFontCodeColorFromElement` on a configured instance, never hardcode tag strings.

---

## 5. Typical screen-building convention

Screens are generally built with a consistent three-method pattern operating on private static state:

- `Rescale()` — determines/creates the `Container`(s) and their sizes.
- `FillContainers()` — orchestrator; calls smaller per-element fill methods (e.g. `FillButtons()`), all of which populate content via `AlterContent`/`AppendContent`.
- `Render()` — the actual `Print`/`PrintLine` calls that output everything to the console.

This keeps sizing, content-building, and drawing as clearly separated, repeatable steps.
