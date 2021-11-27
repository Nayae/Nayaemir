using Silk.NET.Core.Native;
using Silk.NET.OpenGL;
using Silk.NET.SDL;

namespace Nayaemir.Core;

public class Debugger
{
    public unsafe Debugger(GL api)
    {
        api.GetInteger(GetPName.ContextFlags, out var flags);
        if (((GLcontextFlag)flags).HasFlag(GLcontextFlag.GLContextDebugFlag))
        {
            api.Enable(EnableCap.DebugOutput);
            api.Enable(EnableCap.DebugOutputSynchronous);
            api.DebugMessageCallback(OnDebugMessage, null);
            api.DebugMessageControl(GLEnum.DontCare, GLEnum.DontCare, GLEnum.DontCare, 0, null, true);
        }
    }

    private void OnDebugMessage(
        GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam
    )
    {
        // ignore non-significant error/warning codes
        if (id is 131169 or 131185 or 131218 or 131204) return;

        var debugSource = (DebugSource)source switch
        {
            DebugSource.DebugSourceApi => "API",
            DebugSource.DebugSourceWindowSystem => "Window System",
            DebugSource.DebugSourceShaderCompiler => "Shader Compiler",
            DebugSource.DebugSourceThirdParty => "Third Party",
            DebugSource.DebugSourceApplication => "Application",
            DebugSource.DebugSourceOther => "Other",
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };

        var debugType = (DebugType)type switch
        {
            DebugType.DebugTypeError => "Error",
            DebugType.DebugTypeDeprecatedBehavior => "Deprecated Behaviour",
            DebugType.DebugTypeUndefinedBehavior => "Undefined Behaviour",
            DebugType.DebugTypePortability => "Portability",
            DebugType.DebugTypePerformance => "Performance",
            DebugType.DebugTypeOther => "Other",
            DebugType.DebugTypeMarker => "Marker",
            DebugType.DebugTypePushGroup => "Push Group",
            DebugType.DebugTypePopGroup => "Pop Group",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        var debugSeverity = (DebugSeverity)severity switch
        {
            DebugSeverity.DebugSeverityNotification => "Notification",
            DebugSeverity.DebugSeverityHigh => "High",
            DebugSeverity.DebugSeverityMedium => "Medium",
            DebugSeverity.DebugSeverityLow => "Low",
            _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null)
        };

        Console.WriteLine(
            "--------------------\n" +
            $"Debug message ({id}): {SilkMarshal.PtrToString(message)}\n" +
            $"Source: {debugSource}\n" +
            $"Type: {debugType}\n" +
            $"Severity: {debugSeverity}"
        );
    }
}