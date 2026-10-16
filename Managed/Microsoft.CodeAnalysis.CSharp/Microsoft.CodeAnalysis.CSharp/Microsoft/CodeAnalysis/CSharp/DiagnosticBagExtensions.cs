using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000135 RID: 309
	internal static class DiagnosticBagExtensions
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x000854E0 File Offset: 0x000836E0
		internal static CSDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code);
			CSDiagnostic csdiagnostic = new CSDiagnostic(csdiagnosticInfo, location, false);
			diagnostics.Add(csdiagnostic);
			return csdiagnosticInfo;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00085504 File Offset: 0x00083704
		internal static CSDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location, params object[] args)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code, args);
			CSDiagnostic csdiagnostic = new CSDiagnostic(csdiagnosticInfo, location, false);
			diagnostics.Add(csdiagnostic);
			return csdiagnosticInfo;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00085528 File Offset: 0x00083728
		internal static CSDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location, ImmutableArray<Symbol> symbols, params object[] args)
		{
			CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(code, args, symbols, ImmutableArray<Location>.Empty);
			CSDiagnostic csdiagnostic = new CSDiagnostic(csdiagnosticInfo, location, false);
			diagnostics.Add(csdiagnostic);
			return csdiagnosticInfo;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00085554 File Offset: 0x00083754
		internal static void Add(this DiagnosticBag diagnostics, DiagnosticInfo info, Location location)
		{
			CSDiagnostic csdiagnostic = new CSDiagnostic(info, location, false);
			diagnostics.Add(csdiagnostic);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00085571 File Offset: 0x00083771
		internal static bool Add(this DiagnosticBag diagnostics, SyntaxNode node, HashSet<DiagnosticInfo> useSiteDiagnostics)
		{
			return !useSiteDiagnostics.IsNullOrEmpty<DiagnosticInfo>() && diagnostics.Add(node.Location, useSiteDiagnostics);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0008558A File Offset: 0x0008378A
		internal static bool Add(this DiagnosticBag diagnostics, SyntaxToken token, HashSet<DiagnosticInfo> useSiteDiagnostics)
		{
			return !useSiteDiagnostics.IsNullOrEmpty<DiagnosticInfo>() && diagnostics.Add(token.GetLocation(), useSiteDiagnostics);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000855A4 File Offset: 0x000837A4
		internal static bool Add(this DiagnosticBag diagnostics, Location location, IReadOnlyCollection<DiagnosticInfo> useSiteDiagnostics)
		{
			if (useSiteDiagnostics.IsNullOrEmpty<DiagnosticInfo>())
			{
				return false;
			}
			bool flag = false;
			foreach (DiagnosticInfo diagnosticInfo in useSiteDiagnostics)
			{
				if (diagnosticInfo.Severity == DiagnosticSeverity.Error)
				{
					flag = true;
				}
				diagnostics.Add(new CSDiagnostic(diagnosticInfo, location, false));
			}
			return flag;
		}
	}
}
