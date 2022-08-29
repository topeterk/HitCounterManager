using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LiveSplit.ASL
{
    public class ASLCompilerException : Exception
    {
        public ASLMethod Method { get; }

        public CompilerErrorCollection CompilerErrors { get; }

        public ASLCompilerException(ASLMethod method, CompilerErrorCollection errors)
            : base(GetMessage(method, errors))
        {
            Method = method;
            CompilerErrors = errors;
        }

        static string GetMessage(ASLMethod method, CompilerErrorCollection errors)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            var sb = new StringBuilder($"'{method.Name ?? "(no name)"}' method compilation errors:");
            foreach (CompilerError error in errors)
            {
                error.Line = error.Line + method.LineOffset;
                sb.Append($"\nLine {error.Line}, Col {error.Column}: {(error.IsWarning ? "warning" : "error")} {error.ErrorNumber}: {error.ErrorText}");
            }
            return sb.ToString();
        }
    }

    public class ASLRuntimeException : Exception
    {
        public ASLRuntimeException(ASLMethod method, Exception inner_exception)
            : base(GetMessage(method, inner_exception), inner_exception)
        { }

        static string GetMessage(ASLMethod method, Exception inner_exception)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            if (inner_exception == null)
                throw new ArgumentNullException(nameof(inner_exception));

            var stack_trace = new StackTrace(inner_exception, true);
            var stack_trace_sb = new StringBuilder();
            foreach (var frame in stack_trace.GetFrames())
            {
                var frame_method = frame.GetMethod();
                var frame_module = frame_method.Module;

                var frame_asl_method = method;
                if (method.ScriptMethods != null)
                {
                    frame_asl_method = method.ScriptMethods.FirstOrDefault(m => frame_module == m.Module);
                    if (frame_asl_method == null)
                        continue;
                }
                else if (frame_module != method.Module)
                    continue;

                var frame_line = frame.GetFileLineNumber();
                if (frame_line > 0)
                {
                    var line = frame_line + frame_asl_method.LineOffset;
                    stack_trace_sb.Append($"\n   at ASL line {line} in '{frame_asl_method.Name}'");
                }
            }

            var exception_name = inner_exception.GetType().FullName;
            var method_name = method.Name ?? "(no name)";
            var exception_message = inner_exception.Message;
            return $"Exception thrown: '{exception_name}' in '{method_name}' method:\n{exception_message}\n{stack_trace_sb.ToString()}";
        }
    }
}
