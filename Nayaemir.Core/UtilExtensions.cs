using System.Drawing;
using System.Numerics;

namespace Nayaemir.Core;

public static class UtilExtensions
{
    public static Vector4 ToNormalizedVector(this Color color)
    {
        return new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
    }
}