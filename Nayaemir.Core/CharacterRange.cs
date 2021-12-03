namespace Nayaemir.Core;

internal readonly struct CharacterRange
{
    public static readonly CharacterRange BasicLatin = new(0x0020, 0x007F);
    public static readonly CharacterRange Latin1Supplement = new(0x00A0, 0x00FF);
    public static readonly CharacterRange LatinExtendedA = new(0x0100, 0x017F);
    public static readonly CharacterRange LatinExtendedB = new(0x0180, 0x024F);
    public static readonly CharacterRange Cyrillic = new(0x0400, 0x04FF);
    public static readonly CharacterRange CyrillicSupplement = new(0x0500, 0x052F);
    public static readonly CharacterRange Hiragana = new(0x3040, 0x309F);
    public static readonly CharacterRange Katakana = new(0x30A0, 0x30FF);
    public static readonly CharacterRange Greek = new(0x0370, 0x03FF);
    public static readonly CharacterRange CjkSymbolsAndPunctuation = new(0x3000, 0x303F);
    public static readonly CharacterRange CjkUnifiedIdeographs = new(0x4e00, 0x9fff);
    public static readonly CharacterRange HangulCompatibilityJamo = new(0x3130, 0x318f);
    public static readonly CharacterRange HangulSyllables = new(0xac00, 0xd7af);

    public int Start { get; }

    public int End { get; }

    public int Size => End - Start + 1;

    public CharacterRange(int start, int end)
    {
        Start = start;
        End = end;
    }

    public CharacterRange(int single) : this(single, single)
    {
    }
}