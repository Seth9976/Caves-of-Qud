using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000106 RID: 262
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class CSharpDiagnosticFilter
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0006FA64 File Offset: 0x0006DC64
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Diagnostic Filter(Diagnostic d, int warningLevelOption, NullableContextOptions nullableOption, ReportDiagnostic generalDiagnosticOption, IDictionary<string, ReportDiagnostic> specificDiagnosticOptions, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeOptionsProvider syntaxTreeOptions, CancellationToken cancellationToken)
		{
			if (d == null)
			{
				return d;
			}
			if (d.IsNotConfigurable())
			{
				if (d.IsEnabledByDefault)
				{
					return d;
				}
				return null;
			}
			else
			{
				if (d.Severity == (DiagnosticSeverity)(-2))
				{
					return null;
				}
				bool flag;
				ReportDiagnostic reportDiagnostic;
				if (CSharpDiagnosticFilter.s_alinkWarnings.Contains((ErrorCode)d.Code, null) && specificDiagnosticOptions.Keys.Contains(MessageProvider.Instance.GetIdForErrorCode(1607)))
				{
					reportDiagnostic = CSharpDiagnosticFilter.GetDiagnosticReport(ErrorFacts.GetSeverity(ErrorCode.WRN_ALinkWarn), d.IsEnabledByDefault, MessageProvider.Instance.GetIdForErrorCode(1607), ErrorFacts.GetWarningLevel(ErrorCode.WRN_ALinkWarn), d.Location, d.Category, warningLevelOption, nullableOption, generalDiagnosticOption, specificDiagnosticOptions, syntaxTreeOptions, cancellationToken, out flag);
				}
				else
				{
					reportDiagnostic = CSharpDiagnosticFilter.GetDiagnosticReport(d.Severity, d.IsEnabledByDefault, d.Id, d.WarningLevel, d.Location, d.Category, warningLevelOption, nullableOption, generalDiagnosticOption, specificDiagnosticOptions, syntaxTreeOptions, cancellationToken, out flag);
				}
				if (flag)
				{
					d = d.WithIsSuppressed(true);
				}
				return d.WithReportDiagnostic(reportDiagnostic);
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0006FB58 File Offset: 0x0006DD58
		internal static ReportDiagnostic GetDiagnosticReport(DiagnosticSeverity severity, bool isEnabledByDefault, string id, int diagnosticWarningLevel, Location location, string category, int warningLevelOption, NullableContextOptions nullableOption, ReportDiagnostic generalDiagnosticOption, IDictionary<string, ReportDiagnostic> specificDiagnosticOptions, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTreeOptionsProvider syntaxTreeOptions, CancellationToken cancellationToken, out bool hasPragmaSuppression)
		{
			CSharpDiagnosticFilter.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.severity = severity;
			hasPragmaSuppression = false;
			CSharpSyntaxTree csharpSyntaxTree = location.SourceTree as CSharpSyntaxTree;
			int start = location.SourceSpan.Start;
			if (ErrorFacts.NullableWarnings.Contains(id))
			{
				NullableContextState.State? state = ((csharpSyntaxTree != null) ? new NullableContextState.State?(csharpSyntaxTree.GetNullableContextState(start).WarningsState) : default(NullableContextState.State?));
				bool flag;
				if (state != null)
				{
					switch (state.GetValueOrDefault())
					{
					case NullableContextState.State.Unknown:
						flag = (csharpSyntaxTree == null || !csharpSyntaxTree.IsGeneratedCode(syntaxTreeOptions, cancellationToken)) && nullableOption.WarningsEnabled();
						break;
					case NullableContextState.State.Disabled:
						flag = false;
						break;
					case NullableContextState.State.Enabled:
						flag = true;
						break;
					case NullableContextState.State.ExplicitlyRestored:
						flag = nullableOption.WarningsEnabled();
						break;
					default:
						throw ExceptionUtilities.UnexpectedValue(state);
					}
				}
				else
				{
					flag = nullableOption.WarningsEnabled();
				}
				if (!flag)
				{
					return ReportDiagnostic.Suppress;
				}
			}
			if (diagnosticWarningLevel > warningLevelOption)
			{
				return ReportDiagnostic.Suppress;
			}
			CS$<>8__locals1.isSpecified = false;
			bool flag2 = false;
			ReportDiagnostic reportDiagnostic;
			if (specificDiagnosticOptions.TryGetValue(id, ref reportDiagnostic))
			{
				CS$<>8__locals1.isSpecified = true;
				if (reportDiagnostic == ReportDiagnostic.Default)
				{
					flag2 = true;
				}
			}
			ReportDiagnostic reportDiagnostic2;
			if (syntaxTreeOptions != null && (!CS$<>8__locals1.isSpecified || flag2) && ((csharpSyntaxTree != null && syntaxTreeOptions.TryGetDiagnosticValue(csharpSyntaxTree, id, cancellationToken, out reportDiagnostic2)) || syntaxTreeOptions.TryGetGlobalDiagnosticValue(id, cancellationToken, out reportDiagnostic2)) && (!flag2 || CS$<>8__locals1.severity != DiagnosticSeverity.Warning || reportDiagnostic2 != ReportDiagnostic.Error))
			{
				CS$<>8__locals1.isSpecified = true;
				reportDiagnostic = reportDiagnostic2;
				if (!flag2 && reportDiagnostic == ReportDiagnostic.Warn && generalDiagnosticOption == ReportDiagnostic.Error)
				{
					reportDiagnostic = ReportDiagnostic.Error;
				}
			}
			if (!CS$<>8__locals1.isSpecified)
			{
				reportDiagnostic = (isEnabledByDefault ? ReportDiagnostic.Default : ReportDiagnostic.Suppress);
			}
			if (reportDiagnostic == ReportDiagnostic.Suppress)
			{
				return ReportDiagnostic.Suppress;
			}
			PragmaWarningState pragmaWarningState = ((csharpSyntaxTree != null) ? csharpSyntaxTree.GetPragmaDirectiveWarningState(id, start) : PragmaWarningState.Default);
			if (pragmaWarningState == PragmaWarningState.Disabled)
			{
				hasPragmaSuppression = true;
			}
			if (pragmaWarningState == PragmaWarningState.Enabled)
			{
				switch (reportDiagnostic)
				{
				case ReportDiagnostic.Default:
					if (generalDiagnosticOption == ReportDiagnostic.Error && CSharpDiagnosticFilter.<GetDiagnosticReport>g__promoteToAnError|2_0(ref CS$<>8__locals1))
					{
						return ReportDiagnostic.Error;
					}
					return ReportDiagnostic.Default;
				case ReportDiagnostic.Error:
				case ReportDiagnostic.Warn:
				case ReportDiagnostic.Info:
				case ReportDiagnostic.Hidden:
					return reportDiagnostic;
				case ReportDiagnostic.Suppress:
					return ReportDiagnostic.Default;
				default:
					throw ExceptionUtilities.UnexpectedValue(reportDiagnostic);
				}
			}
			else
			{
				if (reportDiagnostic == ReportDiagnostic.Suppress)
				{
					return ReportDiagnostic.Suppress;
				}
				if (reportDiagnostic == ReportDiagnostic.Default)
				{
					if (generalDiagnosticOption != ReportDiagnostic.Error)
					{
						if (generalDiagnosticOption == ReportDiagnostic.Suppress)
						{
							if (CS$<>8__locals1.severity == DiagnosticSeverity.Warning || CS$<>8__locals1.severity == DiagnosticSeverity.Info)
							{
								reportDiagnostic = ReportDiagnostic.Suppress;
								CS$<>8__locals1.isSpecified = true;
							}
						}
					}
					else if (CSharpDiagnosticFilter.<GetDiagnosticReport>g__promoteToAnError|2_0(ref CS$<>8__locals1))
					{
						return ReportDiagnostic.Error;
					}
				}
				return reportDiagnostic;
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0006FD96 File Offset: 0x0006DF96
		[CompilerGenerated]
		internal static bool <GetDiagnosticReport>g__promoteToAnError|2_0(ref CSharpDiagnosticFilter.<>c__DisplayClass2_0 A_0)
		{
			return A_0.severity == DiagnosticSeverity.Warning && !A_0.isSpecified;
		}

		// Token: 0x04000626 RID: 1574
		private static readonly ErrorCode[] s_alinkWarnings = new ErrorCode[]
		{
			ErrorCode.WRN_ConflictingMachineAssembly,
			ErrorCode.WRN_RefCultureMismatch,
			ErrorCode.WRN_InvalidVersionFormat
		};
	}
}
