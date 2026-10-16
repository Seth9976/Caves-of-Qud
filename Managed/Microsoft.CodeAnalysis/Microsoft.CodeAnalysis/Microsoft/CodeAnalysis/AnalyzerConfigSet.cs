using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000126 RID: 294
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AnalyzerConfigSet
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public static AnalyzerConfigSet Create<[Nullable(0)] TList>(TList analyzerConfigs) where TList : IReadOnlyCollection<AnalyzerConfig>
		{
			ImmutableArray<Diagnostic> immutableArray;
			return AnalyzerConfigSet.Create<TList>(analyzerConfigs, out immutableArray);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E114 File Offset: 0x0001C314
		public static AnalyzerConfigSet Create<[Nullable(0)] TList>(TList analyzerConfigs, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<Diagnostic> diagnostics) where TList : IReadOnlyCollection<AnalyzerConfig>
		{
			ArrayBuilder<AnalyzerConfig> instance = ArrayBuilder<AnalyzerConfig>.GetInstance(analyzerConfigs.Count);
			instance.AddRange(analyzerConfigs);
			instance.Sort(AnalyzerConfig.DirectoryLengthComparer);
			AnalyzerConfigSet.GlobalAnalyzerConfig globalAnalyzerConfig = AnalyzerConfigSet.MergeGlobalConfigs(instance, out diagnostics);
			return new AnalyzerConfigSet(instance.ToImmutableAndFree(), globalAnalyzerConfig);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E160 File Offset: 0x0001C360
		private AnalyzerConfigSet([Nullable(new byte[] { 0, 1 })] ImmutableArray<AnalyzerConfig> analyzerConfigs, AnalyzerConfigSet.GlobalAnalyzerConfig globalConfig)
		{
			this._analyzerConfigs = analyzerConfigs;
			this._globalConfig = globalConfig;
			ArrayBuilder<ImmutableArray<AnalyzerConfig.SectionNameMatcher?>> instance = ArrayBuilder<ImmutableArray<AnalyzerConfig.SectionNameMatcher?>>.GetInstance(this._analyzerConfigs.Length);
			foreach (AnalyzerConfig analyzerConfig in this._analyzerConfigs)
			{
				ArrayBuilder<AnalyzerConfig.SectionNameMatcher?> instance2 = ArrayBuilder<AnalyzerConfig.SectionNameMatcher?>.GetInstance(analyzerConfig.NamedSections.Length);
				foreach (AnalyzerConfig.Section section in analyzerConfig.NamedSections)
				{
					AnalyzerConfig.SectionNameMatcher? sectionNameMatcher = AnalyzerConfig.TryCreateSectionNameMatcher(section.Name);
					instance2.Add(sectionNameMatcher);
				}
				instance.Add(instance2.ToImmutableAndFree());
			}
			this._analyzerMatchers = instance.ToImmutableAndFree();
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001E2B1 File Offset: 0x0001C4B1
		public AnalyzerConfigOptionsResult GlobalConfigOptions
		{
			get
			{
				if (this._lazyConfigOptions == null)
				{
					Interlocked.CompareExchange<StrongBox<AnalyzerConfigOptionsResult>>(ref this._lazyConfigOptions, new StrongBox<AnalyzerConfigOptionsResult>(this.ParseGlobalConfigOptions()), null);
				}
				return this._lazyConfigOptions.Value;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		public AnalyzerConfigOptionsResult GetOptionsForSourcePath(string sourcePath)
		{
			if (sourcePath == null)
			{
				throw new ArgumentNullException("sourcePath");
			}
			List<AnalyzerConfig.Section> list = this._sectionKeyPool.Allocate();
			string text = PathUtilities.NormalizeWithForwardSlash(sourcePath);
			foreach (AnalyzerConfig.Section section in this._globalConfig.NamedSections)
			{
				if (text.Equals(section.Name, AnalyzerConfig.Section.NameComparer))
				{
					list.Add(section);
				}
			}
			int count = list.Count;
			for (int i = 0; i < this._analyzerConfigs.Length; i++)
			{
				AnalyzerConfig analyzerConfig = this._analyzerConfigs[i];
				if (text.StartsWith(analyzerConfig.NormalizedDirectory, 4))
				{
					if (analyzerConfig.IsRoot)
					{
						list.RemoveRange(count, list.Count - count);
					}
					int num = analyzerConfig.NormalizedDirectory.Length;
					if (analyzerConfig.NormalizedDirectory.get_Chars(num - 1) == '/')
					{
						num--;
					}
					string text2 = text.Substring(num);
					ImmutableArray<AnalyzerConfig.SectionNameMatcher?> immutableArray = this._analyzerMatchers[i];
					for (int j = 0; j < immutableArray.Length; j++)
					{
						AnalyzerConfig.SectionNameMatcher? sectionNameMatcher;
						if (immutableArray[j] != null && sectionNameMatcher.GetValueOrDefault().IsMatch(text2))
						{
							AnalyzerConfig.Section section2 = analyzerConfig.NamedSections[j];
							list.Add(section2);
						}
					}
				}
			}
			AnalyzerConfigOptionsResult analyzerConfigOptionsResult;
			if (!this._optionsCache.TryGetValue(list, out analyzerConfigOptionsResult))
			{
				ImmutableDictionary<string, ReportDiagnostic>.Builder builder = this._treeOptionsPool.Allocate();
				ImmutableDictionary<string, string>.Builder builder2 = this._analyzerOptionsPool.Allocate();
				ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance();
				int num2 = 0;
				builder2.AddRange(this.GlobalConfigOptions.AnalyzerOptions);
				foreach (AnalyzerConfig.Section section3 in this._globalConfig.NamedSections)
				{
					if (list.Count > 0 && section3 == list[num2])
					{
						AnalyzerConfigSet.ParseSectionOptions(list[num2], builder, builder2, instance, "<Global Config>", this._diagnosticIdCache);
						num2++;
						if (num2 == list.Count)
						{
							break;
						}
					}
				}
				int num3 = 0;
				while (num3 < this._analyzerConfigs.Length && num2 < list.Count)
				{
					AnalyzerConfig analyzerConfig2 = this._analyzerConfigs[num3];
					ImmutableArray<AnalyzerConfig.SectionNameMatcher?> immutableArray2 = this._analyzerMatchers[num3];
					for (int k = 0; k < immutableArray2.Length; k++)
					{
						if (list[num2] == analyzerConfig2.NamedSections[k])
						{
							AnalyzerConfigSet.ParseSectionOptions(list[num2], builder, builder2, instance, analyzerConfig2.PathToFile, this._diagnosticIdCache);
							num2++;
							if (num2 == list.Count)
							{
								break;
							}
						}
					}
					num3++;
				}
				analyzerConfigOptionsResult = new AnalyzerConfigOptionsResult((builder.Count > 0) ? builder.ToImmutable() : SyntaxTree.EmptyDiagnosticOptions, (builder2.Count > 0) ? builder2.ToImmutable() : AnalyzerConfigOptions.EmptyDictionary, instance.ToImmutableAndFree());
				if (!this._optionsCache.TryAdd(list, analyzerConfigOptionsResult))
				{
					AnalyzerConfigSet.<GetOptionsForSourcePath>g__freeKey|18_0(list, this._sectionKeyPool);
				}
				builder.Clear();
				builder2.Clear();
				this._treeOptionsPool.Free(builder);
				this._analyzerOptionsPool.Free(builder2);
			}
			else
			{
				AnalyzerConfigSet.<GetOptionsForSourcePath>g__freeKey|18_0(list, this._sectionKeyPool);
			}
			return analyzerConfigOptionsResult;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E660 File Offset: 0x0001C860
		internal static bool TryParseSeverity(string value, out ReportDiagnostic severity)
		{
			StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
			if (ordinalIgnoreCase.Equals(value, "default"))
			{
				severity = ReportDiagnostic.Default;
				return true;
			}
			if (ordinalIgnoreCase.Equals(value, "error"))
			{
				severity = ReportDiagnostic.Error;
				return true;
			}
			if (ordinalIgnoreCase.Equals(value, "warning"))
			{
				severity = ReportDiagnostic.Warn;
				return true;
			}
			if (ordinalIgnoreCase.Equals(value, "suggestion"))
			{
				severity = ReportDiagnostic.Info;
				return true;
			}
			if (ordinalIgnoreCase.Equals(value, "silent") || ordinalIgnoreCase.Equals(value, "refactoring"))
			{
				severity = ReportDiagnostic.Hidden;
				return true;
			}
			if (ordinalIgnoreCase.Equals(value, "none"))
			{
				severity = ReportDiagnostic.Suppress;
				return true;
			}
			severity = ReportDiagnostic.Default;
			return false;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		private AnalyzerConfigOptionsResult ParseGlobalConfigOptions()
		{
			ImmutableDictionary<string, ReportDiagnostic>.Builder builder = this._treeOptionsPool.Allocate();
			ImmutableDictionary<string, string>.Builder builder2 = this._analyzerOptionsPool.Allocate();
			ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance();
			AnalyzerConfigSet.ParseSectionOptions(this._globalConfig.GlobalSection, builder, builder2, instance, "<Global Config>", this._diagnosticIdCache);
			AnalyzerConfigOptionsResult analyzerConfigOptionsResult = new AnalyzerConfigOptionsResult(builder.ToImmutable(), builder2.ToImmutable(), instance.ToImmutableAndFree());
			builder.Clear();
			builder2.Clear();
			this._treeOptionsPool.Free(builder);
			this._analyzerOptionsPool.Free(builder2);
			return analyzerConfigOptionsResult;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E77C File Offset: 0x0001C97C
		private static void ParseSectionOptions(AnalyzerConfig.Section section, ImmutableDictionary<string, ReportDiagnostic>.Builder treeBuilder, ImmutableDictionary<string, string>.Builder analyzerBuilder, ArrayBuilder<Diagnostic> diagnosticBuilder, string analyzerConfigPath, [Nullable(new byte[] { 1, 0, 1 })] ConcurrentDictionary<ReadOnlyMemory<char>, string> diagIdCache)
		{
			foreach (KeyValuePair<string, string> keyValuePair in section.Properties)
			{
				string text;
				string text2;
				keyValuePair.Deconstruct<string, string>(out text, out text2);
				string text3 = text;
				string text4 = text2;
				int num = -1;
				if (text3.StartsWith("dotnet_diagnostic.", 4) && text3.EndsWith(".severity", 4))
				{
					num = text3.Length - ("dotnet_diagnostic.".Length + ".severity".Length);
				}
				if (num >= 0)
				{
					ReadOnlyMemory<char> readOnlyMemory = MemoryExtensions.AsMemory(text3).Slice("dotnet_diagnostic.".Length, num);
					string text5;
					if (!diagIdCache.TryGetValue(readOnlyMemory, ref text5))
					{
						text5 = readOnlyMemory.ToString();
						text5 = diagIdCache.GetOrAdd(MemoryExtensions.AsMemory(text5), text5);
					}
					ReportDiagnostic reportDiagnostic;
					if (AnalyzerConfigSet.TryParseSeverity(text4, out reportDiagnostic))
					{
						treeBuilder[text5] = reportDiagnostic;
					}
					else
					{
						diagnosticBuilder.Add(Diagnostic.Create(AnalyzerConfigSet.InvalidAnalyzerConfigSeverityDescriptor, Location.None, new object[] { text5, text4, analyzerConfigPath }));
					}
				}
				else
				{
					analyzerBuilder[text3] = text4;
				}
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		internal static AnalyzerConfigSet.GlobalAnalyzerConfig MergeGlobalConfigs(ArrayBuilder<AnalyzerConfig> analyzerConfigs, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<Diagnostic> diagnostics)
		{
			AnalyzerConfigSet.GlobalAnalyzerConfigBuilder globalAnalyzerConfigBuilder = default(AnalyzerConfigSet.GlobalAnalyzerConfigBuilder);
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			for (int i = 0; i < analyzerConfigs.Count; i++)
			{
				if (analyzerConfigs[i].IsGlobal)
				{
					globalAnalyzerConfigBuilder.MergeIntoGlobalConfig(analyzerConfigs[i], instance);
					analyzerConfigs.RemoveAt(i);
					i--;
				}
			}
			AnalyzerConfigSet.GlobalAnalyzerConfig globalAnalyzerConfig = globalAnalyzerConfigBuilder.Build(instance);
			diagnostics = instance.ToReadOnlyAndFree();
			return globalAnalyzerConfig;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E9AA File Offset: 0x0001CBAA
		[CompilerGenerated]
		internal static void <GetOptionsForSourcePath>g__freeKey|18_0(List<AnalyzerConfig.Section> sectionKey, ObjectPool<List<AnalyzerConfig.Section>> pool)
		{
			sectionKey.Clear();
			pool.Free(sectionKey);
		}

		// Token: 0x040002DF RID: 735
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<AnalyzerConfig> _analyzerConfigs;

		// Token: 0x040002E0 RID: 736
		private readonly AnalyzerConfigSet.GlobalAnalyzerConfig _globalConfig;

		// Token: 0x040002E1 RID: 737
		[Nullable(0)]
		private readonly ImmutableArray<ImmutableArray<AnalyzerConfig.SectionNameMatcher?>> _analyzerMatchers;

		// Token: 0x040002E2 RID: 738
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly ConcurrentDictionary<ReadOnlyMemory<char>, string> _diagnosticIdCache = new ConcurrentDictionary<ReadOnlyMemory<char>, string>(CharMemoryEqualityComparer.Instance);

		// Token: 0x040002E3 RID: 739
		private readonly ConcurrentCache<List<AnalyzerConfig.Section>, AnalyzerConfigOptionsResult> _optionsCache = new ConcurrentCache<List<AnalyzerConfig.Section>, AnalyzerConfigOptionsResult>(50, AnalyzerConfigSet.SequenceEqualComparer.Instance);

		// Token: 0x040002E4 RID: 740
		private readonly ObjectPool<ImmutableDictionary<string, ReportDiagnostic>.Builder> _treeOptionsPool = new ObjectPool<ImmutableDictionary<string, ReportDiagnostic>.Builder>(() => ImmutableDictionary.CreateBuilder<string, ReportDiagnostic>(AnalyzerConfig.Section.PropertiesKeyComparer));

		// Token: 0x040002E5 RID: 741
		private readonly ObjectPool<ImmutableDictionary<string, string>.Builder> _analyzerOptionsPool = new ObjectPool<ImmutableDictionary<string, string>.Builder>(() => ImmutableDictionary.CreateBuilder<string, string>(AnalyzerConfig.Section.PropertiesKeyComparer));

		// Token: 0x040002E6 RID: 742
		private readonly ObjectPool<List<AnalyzerConfig.Section>> _sectionKeyPool = new ObjectPool<List<AnalyzerConfig.Section>>(() => new List<AnalyzerConfig.Section>());

		// Token: 0x040002E7 RID: 743
		[Nullable(2)]
		private StrongBox<AnalyzerConfigOptionsResult> _lazyConfigOptions;

		// Token: 0x040002E8 RID: 744
		private static readonly DiagnosticDescriptor InvalidAnalyzerConfigSeverityDescriptor = new DiagnosticDescriptor("InvalidSeverityInAnalyzerConfig", CodeAnalysisResources.WRN_InvalidSeverityInAnalyzerConfig_Title, CodeAnalysisResources.WRN_InvalidSeverityInAnalyzerConfig, "AnalyzerConfig", DiagnosticSeverity.Warning, true, null, null, Array.Empty<string>());

		// Token: 0x040002E9 RID: 745
		private static readonly DiagnosticDescriptor MultipleGlobalAnalyzerKeysDescriptor = new DiagnosticDescriptor("MultipleGlobalAnalyzerKeys", CodeAnalysisResources.WRN_MultipleGlobalAnalyzerKeys_Title, CodeAnalysisResources.WRN_MultipleGlobalAnalyzerKeys, "AnalyzerConfig", DiagnosticSeverity.Warning, true, null, null, Array.Empty<string>());

		// Token: 0x040002EA RID: 746
		private static readonly DiagnosticDescriptor InvalidGlobalAnalyzerSectionDescriptor = new DiagnosticDescriptor("InvalidGlobalSectionName", CodeAnalysisResources.WRN_InvalidGlobalSectionName_Title, CodeAnalysisResources.WRN_InvalidGlobalSectionName, "AnalyzerConfig", DiagnosticSeverity.Warning, true, null, null, Array.Empty<string>());

		// Token: 0x0200055C RID: 1372
		[Nullable(0)]
		private sealed class SequenceEqualComparer : IEqualityComparer<List<AnalyzerConfig.Section>>
		{
			// Token: 0x17000EDC RID: 3804
			// (get) Token: 0x06003413 RID: 13331 RVA: 0x0009313B File Offset: 0x0009133B
			public static AnalyzerConfigSet.SequenceEqualComparer Instance { get; } = new AnalyzerConfigSet.SequenceEqualComparer();

			// Token: 0x06003414 RID: 13332 RVA: 0x00093144 File Offset: 0x00091344
			public bool Equals([Nullable(new byte[] { 2, 1 })] List<AnalyzerConfig.Section> x, [Nullable(new byte[] { 2, 1 })] List<AnalyzerConfig.Section> y)
			{
				if (x == null || y == null)
				{
					return x == null && y == null;
				}
				if (x.Count != y.Count)
				{
					return false;
				}
				for (int i = 0; i < x.Count; i++)
				{
					if (x[i] != y[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003415 RID: 13333 RVA: 0x00093195 File Offset: 0x00091395
			public int GetHashCode(List<AnalyzerConfig.Section> obj)
			{
				return Hash.CombineValues<AnalyzerConfig.Section>(obj, int.MaxValue);
			}
		}

		// Token: 0x0200055D RID: 1373
		[Nullable(0)]
		internal struct GlobalAnalyzerConfigBuilder
		{
			// Token: 0x06003418 RID: 13336 RVA: 0x000931B8 File Offset: 0x000913B8
			internal void MergeIntoGlobalConfig(AnalyzerConfig config, DiagnosticBag diagnostics)
			{
				if (this._values == null)
				{
					this._values = ImmutableDictionary.CreateBuilder<string, ImmutableDictionary<string, ValueTuple<string, string, int>>.Builder>(AnalyzerConfig.Section.NameEqualityComparer);
					this._duplicates = ImmutableDictionary.CreateBuilder<string, ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder>(AnalyzerConfig.Section.NameEqualityComparer);
				}
				this.MergeSection(config.PathToFile, config.GlobalSection, config.GlobalLevel, true);
				foreach (AnalyzerConfig.Section section in config.NamedSections)
				{
					if (AnalyzerConfig.IsAbsoluteEditorConfigPath(section.Name))
					{
						this.MergeSection(config.PathToFile, section, config.GlobalLevel, false);
					}
					else
					{
						diagnostics.Add(Diagnostic.Create(AnalyzerConfigSet.InvalidGlobalAnalyzerSectionDescriptor, Location.None, new object[] { section.Name, config.PathToFile }));
					}
				}
			}

			// Token: 0x06003419 RID: 13337 RVA: 0x00093278 File Offset: 0x00091478
			internal AnalyzerConfigSet.GlobalAnalyzerConfig Build(DiagnosticBag diagnostics)
			{
				if (this._values == null || this._duplicates == null)
				{
					return new AnalyzerConfigSet.GlobalAnalyzerConfig(new AnalyzerConfig.Section("Global Section", ImmutableDictionary<string, string>.Empty), ImmutableArray<AnalyzerConfig.Section>.Empty);
				}
				foreach (KeyValuePair<string, ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder> keyValuePair in this._duplicates)
				{
					string text;
					ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder builder;
					keyValuePair.Deconstruct<string, ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder>(out text, out builder);
					string text2 = text;
					ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder builder2 = builder;
					string text3 = (string.IsNullOrWhiteSpace(text2) ? "Global Section" : text2);
					foreach (KeyValuePair<string, ValueTuple<int, ArrayBuilder<string>>> keyValuePair2 in builder2)
					{
						ValueTuple<int, ArrayBuilder<string>> valueTuple;
						keyValuePair2.Deconstruct<string, ValueTuple<int, ArrayBuilder<string>>>(out text, out valueTuple);
						ref ValueTuple<int, ArrayBuilder<string>> ptr = valueTuple;
						string text4 = text;
						ArrayBuilder<string> item = ptr.Item2;
						diagnostics.Add(Diagnostic.Create(AnalyzerConfigSet.MultipleGlobalAnalyzerKeysDescriptor, Location.None, new object[]
						{
							text4,
							text3,
							string.Join(", ", item)
						}));
					}
				}
				this._duplicates = null;
				AnalyzerConfig.Section section = this.GetSection(string.Empty);
				this._values.Remove(string.Empty);
				ArrayBuilder<AnalyzerConfig.Section> arrayBuilder = new ArrayBuilder<AnalyzerConfig.Section>(this._values.Count);
				foreach (string text5 in this._values.Keys.Order<string>())
				{
					arrayBuilder.Add(this.GetSection(text5));
				}
				AnalyzerConfigSet.GlobalAnalyzerConfig globalAnalyzerConfig = new AnalyzerConfigSet.GlobalAnalyzerConfig(section, arrayBuilder.ToImmutableAndFree());
				this._values = null;
				return globalAnalyzerConfig;
			}

			// Token: 0x0600341A RID: 13338 RVA: 0x00093430 File Offset: 0x00091630
			private AnalyzerConfig.Section GetSection(string sectionName)
			{
				ImmutableDictionary<string, string> immutableDictionary = this._values[sectionName].ToImmutableDictionary<KeyValuePair<string, ValueTuple<string, string, int>>, string, string>(([TupleElementNames(new string[] { "value", "configPath", "globalLevel" })] KeyValuePair<string, ValueTuple<string, string, int>> d) => d.Key, ([TupleElementNames(new string[] { "value", "configPath", "globalLevel" })] KeyValuePair<string, ValueTuple<string, string, int>> d) => d.Value.Item1, AnalyzerConfig.Section.PropertiesKeyComparer);
				return new AnalyzerConfig.Section(sectionName, immutableDictionary);
			}

			// Token: 0x0600341B RID: 13339 RVA: 0x0009349C File Offset: 0x0009169C
			private void MergeSection(string configPath, AnalyzerConfig.Section section, int globalLevel, bool isGlobalSection)
			{
				ImmutableDictionary<string, ValueTuple<string, string, int>>.Builder builder;
				if (!this._values.TryGetValue(section.Name, out builder))
				{
					builder = ImmutableDictionary.CreateBuilder<string, ValueTuple<string, string, int>>(AnalyzerConfig.Section.PropertiesKeyComparer);
					this._values.Add(section.Name, builder);
				}
				ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder builder2;
				this._duplicates.TryGetValue(section.Name, out builder2);
				foreach (KeyValuePair<string, string> keyValuePair in section.Properties)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct<string, string>(out text, out text2);
					string text3 = text;
					string text4 = text2;
					if (!isGlobalSection || (!AnalyzerConfig.Section.PropertiesKeyComparer.Equals(text3, "is_global") && !AnalyzerConfig.Section.PropertiesKeyComparer.Equals(text3, "global_level")))
					{
						ValueTuple<string, string, int> valueTuple;
						bool flag = builder.TryGetValue(text3, out valueTuple);
						ValueTuple<int, ArrayBuilder<string>> valueTuple2 = default(ValueTuple<int, ArrayBuilder<string>>);
						bool flag2 = !flag && builder2 != null && builder2.TryGetValue(text3, out valueTuple2);
						if (!flag && !flag2)
						{
							builder.Add(text3, new ValueTuple<string, string, int>(text4, configPath, globalLevel));
						}
						else
						{
							int num = (flag ? valueTuple.Item3 : valueTuple2.Item1);
							if (num < globalLevel)
							{
								builder[text3] = new ValueTuple<string, string, int>(text4, configPath, globalLevel);
								if (flag2)
								{
									builder2.Remove(text3);
								}
							}
							else if (num == globalLevel)
							{
								if (builder2 == null)
								{
									builder2 = ImmutableDictionary.CreateBuilder<string, ValueTuple<int, ArrayBuilder<string>>>(AnalyzerConfig.Section.PropertiesKeyComparer);
									this._duplicates.Add(section.Name, builder2);
								}
								ArrayBuilder<string> arrayBuilder = valueTuple2.Item2 ?? ArrayBuilder<string>.GetInstance();
								arrayBuilder.Add(configPath);
								builder2[text3] = new ValueTuple<int, ArrayBuilder<string>>(globalLevel, arrayBuilder);
								if (flag)
								{
									ValueTuple<string, string, int> valueTuple3 = valueTuple;
									builder.Remove(text3);
									arrayBuilder.Insert(0, valueTuple3.Item2);
								}
							}
						}
					}
				}
			}

			// Token: 0x04001541 RID: 5441
			[TupleElementNames(new string[] { "value", "configPath", "globalLevel" })]
			[Nullable(new byte[] { 2, 1, 1, 1, 0, 1, 1 })]
			private ImmutableDictionary<string, ImmutableDictionary<string, ValueTuple<string, string, int>>.Builder>.Builder _values;

			// Token: 0x04001542 RID: 5442
			[TupleElementNames(new string[] { "globalLevel", "configPaths" })]
			[Nullable(new byte[] { 2, 1, 1, 1, 0, 1, 1 })]
			private ImmutableDictionary<string, ImmutableDictionary<string, ValueTuple<int, ArrayBuilder<string>>>.Builder>.Builder _duplicates;

			// Token: 0x04001543 RID: 5443
			internal const string GlobalConfigPath = "<Global Config>";

			// Token: 0x04001544 RID: 5444
			internal const string GlobalSectionName = "Global Section";
		}

		// Token: 0x0200055E RID: 1374
		[Nullable(0)]
		internal sealed class GlobalAnalyzerConfig
		{
			// Token: 0x17000EDD RID: 3805
			// (get) Token: 0x0600341C RID: 13340 RVA: 0x0009366C File Offset: 0x0009186C
			internal AnalyzerConfig.Section GlobalSection { get; }

			// Token: 0x17000EDE RID: 3806
			// (get) Token: 0x0600341D RID: 13341 RVA: 0x00093674 File Offset: 0x00091874
			[Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<AnalyzerConfig.Section> NamedSections
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x0600341E RID: 13342 RVA: 0x0009367C File Offset: 0x0009187C
			public GlobalAnalyzerConfig(AnalyzerConfig.Section globalSection, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AnalyzerConfig.Section> namedSections)
			{
				this.GlobalSection = globalSection;
				this.NamedSections = namedSections;
			}
		}
	}
}
