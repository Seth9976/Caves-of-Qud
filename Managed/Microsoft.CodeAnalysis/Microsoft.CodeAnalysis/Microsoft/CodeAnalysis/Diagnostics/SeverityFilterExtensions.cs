using System;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000354 RID: 852
	internal static class SeverityFilterExtensions
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x00071CB8 File Offset: 0x0006FEB8
		internal static bool Contains(this SeverityFilter severityFilter, ReportDiagnostic severity)
		{
			bool flag;
			if (severity != ReportDiagnostic.Info)
			{
				flag = severity == ReportDiagnostic.Hidden && (severityFilter & SeverityFilter.Hidden) > SeverityFilter.None;
			}
			else
			{
				flag = (severityFilter & SeverityFilter.Info) > SeverityFilter.None;
			}
			return flag;
		}
	}
}
