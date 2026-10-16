using System;
using System.IO;
using System.Linq;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000356 RID: 854
	internal static class AnalyzerExceptionDescriptionBuilder
	{
		// Token: 0x06002485 RID: 9349 RVA: 0x00071D0C File Offset: 0x0006FF0C
		public static string CreateDiagnosticDescription(this Exception exception)
		{
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				AggregateException ex2 = ex.Flatten();
				return string.Join(AnalyzerExceptionDescriptionBuilder.s_separator, Enumerable.Select<Exception, string>(ex2.InnerExceptions, (Exception e) => AnalyzerExceptionDescriptionBuilder.GetExceptionMessage(e)));
			}
			if (exception != null)
			{
				return string.Join(AnalyzerExceptionDescriptionBuilder.s_separator, new string[]
				{
					AnalyzerExceptionDescriptionBuilder.GetExceptionMessage(exception),
					exception.InnerException.CreateDiagnosticDescription()
				});
			}
			return string.Empty;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00071D90 File Offset: 0x0006FF90
		private static string GetExceptionMessage(Exception exception)
		{
			FileNotFoundException ex = exception as FileNotFoundException;
			string text = ((ex != null) ? ex.FusionLog : null);
			if (text == null)
			{
				return exception.ToString();
			}
			return string.Join(AnalyzerExceptionDescriptionBuilder.s_separator, new string[] { exception.Message, text });
		}

		// Token: 0x04000F98 RID: 3992
		private static readonly string s_separator = Environment.NewLine + "-----" + Environment.NewLine;
	}
}
