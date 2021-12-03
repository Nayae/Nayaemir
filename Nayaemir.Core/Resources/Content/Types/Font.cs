using System.Numerics;
using System.Runtime.InteropServices;
using FreeTypeSharp.Native;
using Nayaemir.Core.Resources.Graphics.Types;
using SixLabors.ImageSharp.PixelFormats;

namespace Nayaemir.Core.Resources.Content.Types;

using static FT;

public class Font : Resource
{
    private const int ImageSize = 1024;

    public record GlyphInfo(
        uint Width,
        uint Height,
        int Advance,
        Vector2 Bearing,
        Vector2 TextureOffset,
        Vector2 UvStart,
        Vector2 UvEnd
    );

    private readonly TextureObject _texture;
    private readonly Dictionary<char, GlyphInfo> _glyphs;

    public unsafe Font(string fontFile, uint size)
    {
        _glyphs = new Dictionary<char, GlyphInfo>();

        FT_Init_FreeType(out var pLibrary);
        FT_New_Face(pLibrary, fontFile, 0, out var pFace);
        var face = Marshal.PtrToStructure<FT_FaceRec>(pFace);

        FT_Set_Pixel_Sizes(pFace, 0, size);

        var offsetY = 0u;
        var offsetX = 0u;
        var tallestCharacter = 0u;
        var data = stackalloc Rgba32[ImageSize * ImageSize];

        for (var c = (char)CharacterRange.BasicLatin.Start; c <= CharacterRange.BasicLatin.End; c++)
        {
            var glyphIndex = FT_Get_Char_Index(pFace, c);
            FT_Load_Glyph(pFace, glyphIndex, FT_LOAD_TARGET_NORMAL);
            FT_Render_Glyph(new IntPtr(face.glyph), FT_Render_Mode.FT_RENDER_MODE_NORMAL);

            var width = face.glyph->bitmap.width;
            var height = face.glyph->bitmap.rows;
            var buffer = (byte*)face.glyph->bitmap.buffer;

            if (offsetX + width > ImageSize)
            {
                offsetX = 0;
                offsetY += tallestCharacter;
                tallestCharacter = 0;
            }

            if (height > tallestCharacter)
            {
                tallestCharacter = height;
            }

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var r = buffer[y * face.glyph->bitmap.pitch + x];
                var g = buffer[y * face.glyph->bitmap.pitch + x];
                var b = buffer[y * face.glyph->bitmap.pitch + x];
                var average = (byte)((r + g + b) / 3);
                var weighted = (byte)(average != 0 ? 255 : average);

                var pixel = new Rgba32(weighted, weighted, weighted, average);
                data[(y + offsetY) * ImageSize + x + offsetX] = pixel;
            }

            _glyphs[c] = new GlyphInfo(
                width,
                height,
                face.glyph->advance.x.ToInt32(),
                new Vector2(face.glyph->bitmap_left, face.glyph->bitmap_top),
                new Vector2(offsetX, offsetY),
                new Vector2(offsetX / (float)ImageSize, offsetY / (float)ImageSize),
                new Vector2((offsetX + width) / (float)ImageSize, (offsetY + height) / (float)ImageSize)
            );

            offsetX += width;
        }

        FT_Done_Face(pFace);
        FT_Done_Library(pLibrary);

        _texture = new TextureObject(new Span<Rgba32>(data, ImageSize * ImageSize), ImageSize, ImageSize);
    }

    public void Bind()
    {
        _texture.Bind();
    }

    public GlyphInfo GetGlyphInfo(char character)
    {
        return _glyphs[character];
    }
}