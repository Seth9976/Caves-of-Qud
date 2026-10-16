using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000172 RID: 370
	[NullableContext(1)]
	[Nullable(0)]
	public class DiagnosticFormatter
	{
		// Token: 0x06000F60 RID: 3936 RVA: 0x0002D550 File Offset: 0x0002B750
		public virtual string Format(Diagnostic diagnostic, [Nullable(2)] IFormatProvider formatter = null)
		{
			if (diagnostic == null)
			{
				throw new ArgumentNullException("diagnostic");
			}
			CultureInfo cultureInfo = formatter as CultureInfo;
			LocationKind kind = diagnostic.Location.Kind;
			if (kind == LocationKind.SourceFile || kind - LocationKind.XmlFile <= 1)
			{
				FileLinePositionSpan lineSpan = diagnostic.Location.GetLineSpan();
				FileLinePositionSpan mappedLineSpan = diagnostic.Location.GetMappedLineSpan();
				if (lineSpan.IsValid && mappedLineSpan.IsValid)
				{
					string text;
					string text2;
					if (mappedLineSpan.HasMappedPath)
					{
						text = mappedLineSpan.Path;
						text2 = lineSpan.Path;
					}
					else
					{
						text = lineSpan.Path;
						text2 = null;
					}
					return string.Format(formatter, "{0}{1}: {2}: {3}", new object[]
					{
						this.FormatSourcePath(text, text2, formatter),
						this.FormatSourceSpan(mappedLineSpan.Span, formatter),
						this.GetMessagePrefix(diagnostic),
						diagnostic.GetMessage(cultureInfo)
					});
				}
			}
			return string.Format(formatter, "{0}: {1}", this.GetMessagePrefix(diagnostic), diagnostic.GetMessage(cultureInfo));
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002D63C File Offset: 0x0002B83C
		internal virtual string FormatSourcePath(string path, [Nullable(2)] string basePath, [Nullable(2)] IFormatProvider formatter)
		{
			return path;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002D640 File Offset: 0x0002B840
		internal virtual string FormatSourceSpan(LinePositionSpan span, [Nullable(2)] IFormatProvider formatter)
		{
			return string.Format("({0},{1})", span.Start.Line + 1, span.Start.Character + 1);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002D684 File Offset: 0x0002B884
		internal string GetMessagePrefix(Diagnostic diagnostic)
		{
			string text;
			switch (diagnostic.Severity)
			{
			case DiagnosticSeverity.Hidden:
				text = "hidden";
				break;
			case DiagnosticSeverity.Info:
				text = "info";
				break;
			case DiagnosticSeverity.Warning:
				text = "warning";
				break;
			case DiagnosticSeverity.Error:
				text = "error";
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(diagnostic.Severity);
			}
			return string.Format("{0} {1}", text, diagnostic.Id);
		}

		// Token: 0x04000436 RID: 1078
		internal static readonly DiagnosticFormatter Instance = new DiagnosticFormatter();
	}
}
