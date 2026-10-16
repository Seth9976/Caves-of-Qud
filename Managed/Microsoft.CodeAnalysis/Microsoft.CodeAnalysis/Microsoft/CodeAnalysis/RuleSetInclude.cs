using System;
using System.IO;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D3 RID: 467
	[NullableContext(1)]
	[Nullable(0)]
	public class RuleSetInclude
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0003D308 File Offset: 0x0003B508
		public string IncludePath
		{
			get
			{
				return this._includePath;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0003D310 File Offset: 0x0003B510
		public ReportDiagnostic Action
		{
			get
			{
				return this._action;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0003D318 File Offset: 0x0003B518
		public RuleSetInclude(string includePath, ReportDiagnostic action)
		{
			this._includePath = includePath;
			this._action = action;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003D330 File Offset: 0x0003B530
		[return: Nullable(2)]
		public RuleSet LoadRuleSet(RuleSet parent)
		{
			RuleSet ruleSet = null;
			string text = this._includePath;
			try
			{
				text = this.GetIncludePath(parent);
				if (text == null)
				{
					return null;
				}
				ruleSet = RuleSetProcessor.LoadFromFile(text);
			}
			catch (FileNotFoundException)
			{
			}
			catch (Exception ex)
			{
				throw new InvalidRuleSetException(string.Format(CodeAnalysisResources.InvalidRuleSetInclude, text, ex.Message));
			}
			return ruleSet;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003D398 File Offset: 0x0003B598
		[return: Nullable(2)]
		private string GetIncludePath(RuleSet parent)
		{
			string text = RuleSetInclude.<GetIncludePath>g__resolveIncludePath|8_0(this._includePath, (parent != null) ? parent.FilePath : null);
			if (text == null)
			{
				return null;
			}
			return Path.GetFullPath(text);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
		[NullableContext(2)]
		[CompilerGenerated]
		internal static string <GetIncludePath>g__resolveIncludePath|8_0([Nullable(1)] string includePath, string parentRulesetPath)
		{
			string text = RuleSetInclude.<GetIncludePath>g__resolveIncludePathCore|8_1(includePath, parentRulesetPath);
			if (text == null && PathUtilities.IsUnixLikePlatform)
			{
				includePath = includePath.Replace('\\', Path.DirectorySeparatorChar);
				text = RuleSetInclude.<GetIncludePath>g__resolveIncludePathCore|8_1(includePath, parentRulesetPath);
			}
			return text;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0003D400 File Offset: 0x0003B600
		[NullableContext(2)]
		[CompilerGenerated]
		internal static string <GetIncludePath>g__resolveIncludePathCore|8_1([Nullable(1)] string includePath, string parentRulesetPath)
		{
			includePath = Environment.ExpandEnvironmentVariables(includePath);
			if (Path.IsPathRooted(includePath))
			{
				if (File.Exists(includePath))
				{
					return includePath;
				}
			}
			else if (!string.IsNullOrEmpty(parentRulesetPath))
			{
				includePath = PathUtilities.CombinePathsUnchecked(Path.GetDirectoryName(parentRulesetPath) ?? "", includePath);
				if (File.Exists(includePath))
				{
					return includePath;
				}
			}
			return null;
		}

		// Token: 0x040005D9 RID: 1497
		private readonly string _includePath;

		// Token: 0x040005DA RID: 1498
		private readonly ReportDiagnostic _action;
	}
}
