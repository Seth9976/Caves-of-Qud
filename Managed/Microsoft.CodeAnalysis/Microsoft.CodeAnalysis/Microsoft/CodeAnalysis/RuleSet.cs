using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D2 RID: 466
	[NullableContext(1)]
	[Nullable(0)]
	public class RuleSet
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x0003CD63 File Offset: 0x0003AF63
		public string FilePath
		{
			get
			{
				return this._filePath;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0003CD6B File Offset: 0x0003AF6B
		public ReportDiagnostic GeneralDiagnosticOption
		{
			get
			{
				return this._generalDiagnosticOption;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0003CD73 File Offset: 0x0003AF73
		public ImmutableDictionary<string, ReportDiagnostic> SpecificDiagnosticOptions
		{
			get
			{
				return this._specificDiagnosticOptions;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0003CD7B File Offset: 0x0003AF7B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<RuleSetInclude> Includes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._includes;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0003CD83 File Offset: 0x0003AF83
		public RuleSet(string filePath, ReportDiagnostic generalOption, ImmutableDictionary<string, ReportDiagnostic> specificOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<RuleSetInclude> includes)
		{
			this._filePath = filePath;
			this._generalDiagnosticOption = generalOption;
			this._specificDiagnosticOptions = ((specificOptions == null) ? ImmutableDictionary<string, ReportDiagnostic>.Empty : specificOptions);
			this._includes = includes.NullToEmpty<RuleSetInclude>();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0003CDB8 File Offset: 0x0003AFB8
		[NullableContext(2)]
		public RuleSet WithEffectiveAction(ReportDiagnostic action)
		{
			if (!this._includes.IsEmpty)
			{
				throw new ArgumentException("Effective action cannot be applied to rulesets with Includes");
			}
			switch (action)
			{
			case ReportDiagnostic.Default:
				return this;
			case ReportDiagnostic.Error:
			case ReportDiagnostic.Warn:
			case ReportDiagnostic.Info:
			case ReportDiagnostic.Hidden:
			{
				ReportDiagnostic reportDiagnostic = ((this._generalDiagnosticOption == ReportDiagnostic.Default) ? ReportDiagnostic.Default : action);
				ImmutableDictionary<string, ReportDiagnostic>.Builder builder = this._specificDiagnosticOptions.ToBuilder();
				foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair in this._specificDiagnosticOptions)
				{
					if (keyValuePair.Value != ReportDiagnostic.Suppress && keyValuePair.Value != ReportDiagnostic.Default)
					{
						builder[keyValuePair.Key] = action;
					}
				}
				return new RuleSet(this.FilePath, reportDiagnostic, builder.ToImmutable(), this._includes);
			}
			case ReportDiagnostic.Suppress:
				return null;
			default:
				return null;
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003CE9C File Offset: 0x0003B09C
		private RuleSet GetEffectiveRuleSet(HashSet<string> includedRulesetPaths)
		{
			ReportDiagnostic reportDiagnostic = this._generalDiagnosticOption;
			Dictionary<string, ReportDiagnostic> dictionary = new Dictionary<string, ReportDiagnostic>();
			if (this._includes.IsEmpty)
			{
				return this;
			}
			foreach (RuleSetInclude ruleSetInclude in this._includes)
			{
				if (ruleSetInclude.Action != ReportDiagnostic.Suppress)
				{
					RuleSet ruleSet = ruleSetInclude.LoadRuleSet(this);
					if (ruleSet != null && !includedRulesetPaths.Contains(ruleSet.FilePath.ToLowerInvariant()))
					{
						includedRulesetPaths.Add(ruleSet.FilePath.ToLowerInvariant());
						RuleSet ruleSet2 = ruleSet.GetEffectiveRuleSet(includedRulesetPaths);
						ruleSet2 = ruleSet2.WithEffectiveAction(ruleSetInclude.Action);
						if (RuleSet.IsStricterThan(ruleSet2.GeneralDiagnosticOption, reportDiagnostic))
						{
							reportDiagnostic = ruleSet2.GeneralDiagnosticOption;
						}
						foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair in ruleSet2.SpecificDiagnosticOptions)
						{
							ReportDiagnostic reportDiagnostic2;
							if (dictionary.TryGetValue(keyValuePair.Key, ref reportDiagnostic2))
							{
								if (RuleSet.IsStricterThan(keyValuePair.Value, reportDiagnostic2))
								{
									dictionary[keyValuePair.Key] = keyValuePair.Value;
								}
							}
							else
							{
								dictionary.Add(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
				}
			}
			foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair2 in this._specificDiagnosticOptions)
			{
				if (dictionary.ContainsKey(keyValuePair2.Key))
				{
					dictionary[keyValuePair2.Key] = keyValuePair2.Value;
				}
				else
				{
					dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			return new RuleSet(this._filePath, reportDiagnostic, dictionary.ToImmutableDictionary<string, ReportDiagnostic>(), ImmutableArray<RuleSetInclude>.Empty);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0003D084 File Offset: 0x0003B284
		[return: Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> GetEffectiveIncludes()
		{
			ImmutableArray<string>.Builder builder = ImmutableArray.CreateBuilder<string>();
			this.GetEffectiveIncludesCore(builder);
			return builder.ToImmutable();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003D0A4 File Offset: 0x0003B2A4
		private void GetEffectiveIncludesCore(ImmutableArray<string>.Builder arrayBuilder)
		{
			arrayBuilder.Add(this.FilePath);
			foreach (RuleSetInclude ruleSetInclude in this._includes)
			{
				RuleSet ruleSet = ruleSetInclude.LoadRuleSet(this);
				if (ruleSet != null && !arrayBuilder.Contains(ruleSet.FilePath, StringComparer.OrdinalIgnoreCase))
				{
					ruleSet.GetEffectiveIncludesCore(arrayBuilder);
				}
			}
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0003D104 File Offset: 0x0003B304
		private static bool IsStricterThan(ReportDiagnostic action1, ReportDiagnostic action2)
		{
			switch (action2)
			{
			case ReportDiagnostic.Default:
				return action1 == ReportDiagnostic.Warn || action1 == ReportDiagnostic.Error || action1 == ReportDiagnostic.Info || action1 == ReportDiagnostic.Hidden;
			case ReportDiagnostic.Error:
				return false;
			case ReportDiagnostic.Warn:
				return action1 == ReportDiagnostic.Error;
			case ReportDiagnostic.Info:
				return action1 == ReportDiagnostic.Warn || action1 == ReportDiagnostic.Error;
			case ReportDiagnostic.Hidden:
				return action1 == ReportDiagnostic.Warn || action1 == ReportDiagnostic.Error || action1 == ReportDiagnostic.Info;
			case ReportDiagnostic.Suppress:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003D168 File Offset: 0x0003B368
		public static RuleSet LoadEffectiveRuleSetFromFile(string filePath)
		{
			return RuleSetProcessor.LoadFromFile(filePath).GetEffectiveRuleSet(new HashSet<string>());
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0003D17C File Offset: 0x0003B37C
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<string> GetEffectiveIncludesFromFile(string filePath)
		{
			RuleSet ruleSet = RuleSetProcessor.LoadFromFile(filePath);
			if (ruleSet != null)
			{
				return ruleSet.GetEffectiveIncludes();
			}
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003D19F File Offset: 0x0003B39F
		public static ReportDiagnostic GetDiagnosticOptionsFromRulesetFile([Nullable(2)] string rulesetFileFullPath, out Dictionary<string, ReportDiagnostic> specificDiagnosticOptions)
		{
			return RuleSet.GetDiagnosticOptionsFromRulesetFile(rulesetFileFullPath, out specificDiagnosticOptions, null, null);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0003D1AA File Offset: 0x0003B3AA
		[NullableContext(2)]
		internal static ReportDiagnostic GetDiagnosticOptionsFromRulesetFile(string rulesetFileFullPath, [Nullable(1)] out Dictionary<string, ReportDiagnostic> diagnosticOptions, [Nullable(new byte[] { 2, 1 })] IList<Diagnostic> diagnosticsOpt, CommonMessageProvider messageProviderOpt)
		{
			diagnosticOptions = new Dictionary<string, ReportDiagnostic>();
			if (rulesetFileFullPath == null)
			{
				return ReportDiagnostic.Default;
			}
			return RuleSet.GetDiagnosticOptionsFromRulesetFile(diagnosticOptions, rulesetFileFullPath, diagnosticsOpt, messageProviderOpt);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
		private static ReportDiagnostic GetDiagnosticOptionsFromRulesetFile(Dictionary<string, ReportDiagnostic> diagnosticOptions, string resolvedPath, [Nullable(new byte[] { 2, 1 })] IList<Diagnostic> diagnosticsOpt, [Nullable(2)] CommonMessageProvider messageProviderOpt)
		{
			ReportDiagnostic reportDiagnostic = ReportDiagnostic.Default;
			try
			{
				RuleSet ruleSet = RuleSet.LoadEffectiveRuleSetFromFile(resolvedPath);
				reportDiagnostic = ruleSet.GeneralDiagnosticOption;
				foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair in ruleSet.SpecificDiagnosticOptions)
				{
					diagnosticOptions.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			catch (InvalidRuleSetException ex)
			{
				if (diagnosticsOpt != null && messageProviderOpt != null)
				{
					diagnosticsOpt.Add(Diagnostic.Create(messageProviderOpt, messageProviderOpt.ERR_CantReadRulesetFile, new object[] { resolvedPath, ex.Message }));
				}
			}
			catch (IOException ex2)
			{
				if (ex2 is FileNotFoundException || ex2.GetType().Name == "DirectoryNotFoundException")
				{
					if (diagnosticsOpt != null && messageProviderOpt != null)
					{
						diagnosticsOpt.Add(Diagnostic.Create(messageProviderOpt, messageProviderOpt.ERR_CantReadRulesetFile, new object[]
						{
							resolvedPath,
							new CodeAnalysisResourcesLocalizableErrorArgument("FileNotFound")
						}));
					}
				}
				else if (diagnosticsOpt != null && messageProviderOpt != null)
				{
					diagnosticsOpt.Add(Diagnostic.Create(messageProviderOpt, messageProviderOpt.ERR_CantReadRulesetFile, new object[] { resolvedPath, ex2.Message }));
				}
			}
			return reportDiagnostic;
		}

		// Token: 0x040005D5 RID: 1493
		private readonly string _filePath;

		// Token: 0x040005D6 RID: 1494
		private readonly ReportDiagnostic _generalDiagnosticOption;

		// Token: 0x040005D7 RID: 1495
		private readonly ImmutableDictionary<string, ReportDiagnostic> _specificDiagnosticOptions;

		// Token: 0x040005D8 RID: 1496
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<RuleSetInclude> _includes;
	}
}
