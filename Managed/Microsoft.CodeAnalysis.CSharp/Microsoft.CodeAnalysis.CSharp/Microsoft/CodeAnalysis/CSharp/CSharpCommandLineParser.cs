using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FE RID: 254
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class CSharpCommandLineParser : CommandLineParser
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00065232 File Offset: 0x00063432
		public static CSharpCommandLineParser Default { get; } = new CSharpCommandLineParser(false);

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00065239 File Offset: 0x00063439
		public static CSharpCommandLineParser Script { get; } = new CSharpCommandLineParser(true);

		// Token: 0x06001330 RID: 4912 RVA: 0x00065240 File Offset: 0x00063440
		internal CSharpCommandLineParser(bool isScriptCommandLineParser = false)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, isScriptCommandLineParser)
		{
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0006524E File Offset: 0x0006344E
		protected override string RegularFileExtension
		{
			get
			{
				return ".cs";
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00065255 File Offset: 0x00063455
		protected override string ScriptFileExtension
		{
			get
			{
				return ".csx";
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006525C File Offset: 0x0006345C
		internal sealed override CommandLineArguments CommonParse(IEnumerable<string> args, string baseDirectory, [global::System.Runtime.CompilerServices.Nullable(2)] string sdkDirectory, [global::System.Runtime.CompilerServices.Nullable(2)] string additionalReferenceDirectories)
		{
			return this.Parse(args, baseDirectory, sdkDirectory, additionalReferenceDirectories);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006526C File Offset: 0x0006346C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public new CSharpCommandLineArguments Parse([global::System.Runtime.CompilerServices.Nullable(1)] IEnumerable<string> args, string baseDirectory, string sdkDirectory, string additionalReferenceDirectories = null)
		{
			List<Diagnostic> list = new List<Diagnostic>();
			List<string> list2 = new List<string>();
			List<string> list3 = (this.IsScriptCommandLineParser ? new List<string>() : null);
			List<string> list4 = (this.IsScriptCommandLineParser ? new List<string>() : null);
			base.FlattenArgs(args, list, list2, list3, baseDirectory, list4);
			string text = null;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			NullableContextOptions nullableContextOptions = NullableContextOptions.Disable;
			bool flag7 = false;
			bool flag8 = true;
			bool flag9 = false;
			bool flag10 = false;
			DebugInformationFormat debugInformationFormat = (PathUtilities.IsUnixLikePlatform ? DebugInformationFormat.PortablePdb : DebugInformationFormat.Pdb);
			bool flag11 = false;
			string text2 = null;
			bool flag12 = this.IsScriptCommandLineParser;
			string text3 = baseDirectory;
			ImmutableArray<KeyValuePair<string, string>> immutableArray = ImmutableArray<KeyValuePair<string, string>>.Empty;
			string text4 = null;
			string text5 = null;
			bool flag13 = false;
			string text6 = null;
			string text7 = null;
			ErrorLogOptions errorLogOptions = null;
			bool flag14 = false;
			bool flag15 = false;
			OutputKind outputKind = OutputKind.ConsoleApplication;
			SubsystemVersion subsystemVersion = SubsystemVersion.None;
			LanguageVersion languageVersion = LanguageVersion.Default;
			string text8 = null;
			string text9 = null;
			string text10 = null;
			string text11 = null;
			bool flag16 = false;
			Platform platform = Platform.AnyCpu;
			ulong num = 0UL;
			int num2 = 0;
			bool? flag17 = default(bool?);
			string text12 = null;
			string text13 = null;
			List<ResourceDescription> list5 = new List<ResourceDescription>();
			List<CommandLineSourceFile> list6 = new List<CommandLineSourceFile>();
			List<CommandLineSourceFile> list7 = new List<CommandLineSourceFile>();
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			List<CommandLineSourceFile> list8 = new List<CommandLineSourceFile>();
			bool flag18 = false;
			bool flag19 = false;
			bool flag20 = false;
			Encoding encoding = null;
			SourceHashAlgorithm sourceHashAlgorithm = SourceHashAlgorithm.Sha256;
			ArrayBuilder<string> instance2 = ArrayBuilder<string>.GetInstance();
			List<CommandLineReference> list9 = new List<CommandLineReference>();
			List<CommandLineAnalyzerReference> list10 = new List<CommandLineAnalyzerReference>();
			List<string> list11 = new List<string>();
			List<string> list12 = new List<string>();
			List<string> list13 = new List<string>();
			List<string> list14 = new List<string>();
			ReportDiagnostic reportDiagnostic = ReportDiagnostic.Default;
			Dictionary<string, ReportDiagnostic> dictionary = new Dictionary<string, ReportDiagnostic>();
			Dictionary<string, ReportDiagnostic> dictionary2 = new Dictionary<string, ReportDiagnostic>();
			Dictionary<string, ReportDiagnostic> dictionary3 = new Dictionary<string, ReportDiagnostic>();
			int num3 = 4;
			bool flag21 = false;
			bool flag22 = false;
			string text14 = null;
			string text15 = null;
			List<string> list15 = new List<string>();
			string text16 = null;
			bool flag23 = false;
			bool flag24 = false;
			bool flag25 = false;
			ArrayBuilder<InstrumentationKind> instance3 = ArrayBuilder<InstrumentationKind>.GetInstance();
			CultureInfo cultureInfo = null;
			string text17 = null;
			bool flag26 = false;
			bool flag27 = false;
			bool flag28 = false;
			string text18 = null;
			string text19 = null;
			if (!this.IsScriptCommandLineParser)
			{
				using (List<string>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text20;
						string text21;
						if (CommandLineParser.TryParseOption(enumerator.Current, out text20, out text21) && text20 == "ruleset")
						{
							string text22 = CommandLineParser.RemoveQuotesAndSlashes(text21);
							if (RoslynString.IsNullOrEmpty(text22))
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text20 });
							}
							else
							{
								text19 = base.ParseGenericPathToFile(text22, list, baseDirectory, true);
								reportDiagnostic = base.GetDiagnosticOptionsFromRulesetFile(text19, out dictionary, list);
							}
						}
					}
				}
			}
			string text34;
			foreach (string text23 in list2)
			{
				string text24;
				string text25;
				if (flag26 || !CommandLineParser.TryParseOption(text23, out text24, out text25))
				{
					foreach (string text26 in base.ParseFileArgument(text23, baseDirectory, list))
					{
						list6.Add(base.ToCommandLineSourceFile(text26, false));
					}
					if (list6.Count > 0)
					{
						flag18 = true;
					}
				}
				else
				{
					uint num4 = global::<PrivateImplementationDetails>.ComputeStringHash(text24);
					if (num4 <= 1181855383U)
					{
						if (num4 <= 946971642U)
						{
							if (num4 != 843890604U)
							{
								if (num4 != 946971642U)
								{
									goto IL_0528;
								}
								if (!(text24 == "help"))
								{
									goto IL_0528;
								}
							}
							else
							{
								if (!(text24 == "lib"))
								{
									goto IL_0528;
								}
								goto IL_0511;
							}
						}
						else if (num4 != 973910158U)
						{
							if (num4 != 1163961547U)
							{
								if (num4 != 1181855383U)
								{
									goto IL_0528;
								}
								if (!(text24 == "version"))
								{
									goto IL_0528;
								}
								flag3 = true;
								continue;
							}
							else
							{
								if (!(text24 == "libpath"))
								{
									goto IL_0528;
								}
								goto IL_0511;
							}
						}
						else if (!(text24 == "?"))
						{
							goto IL_0528;
						}
						flag2 = true;
						continue;
					}
					if (num4 <= 1602454474U)
					{
						if (num4 != 1518465946U)
						{
							if (num4 != 1602454474U)
							{
								goto IL_0528;
							}
							if (!(text24 == "features"))
							{
								goto IL_0528;
							}
							if (text25 == null)
							{
								list15.Clear();
								continue;
							}
							list15.Add(text25);
							continue;
						}
						else if (!(text24 == "reference"))
						{
							goto IL_0528;
						}
					}
					else if (num4 != 3089736743U)
					{
						if (num4 != 4012068264U)
						{
							if (num4 != 4144776981U)
							{
								goto IL_0528;
							}
							if (!(text24 == "r"))
							{
								goto IL_0528;
							}
						}
						else
						{
							if (!(text24 == "libpaths"))
							{
								goto IL_0528;
							}
							goto IL_0511;
						}
					}
					else
					{
						if (!(text24 == "langversion"))
						{
							goto IL_0528;
						}
						text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
						if (RoslynString.IsNullOrEmpty(text25))
						{
							CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
							{
								MessageID.IDS_Text.Localize(),
								"/langversion:"
							});
							continue;
						}
						if (text25.StartsWith("0", 4))
						{
							CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_LanguageVersionCannotHaveLeadingZeroes, new object[] { text25 });
							continue;
						}
						if (text25 == "?")
						{
							flag4 = true;
							continue;
						}
						if (!LanguageVersionFacts.TryParse(text25, out languageVersion))
						{
							CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadCompatMode, new object[] { text25 });
							continue;
						}
						continue;
					}
					list9.AddRange(CSharpCommandLineParser.ParseAssemblyReferences(text23, text25, list, false));
					continue;
					IL_0511:
					CSharpCommandLineParser.ParseAndResolveReferencePaths(text24, text25, baseDirectory, list11, MessageID.IDS_LIB_OPTION, list);
					continue;
					IL_0528:
					if (this.IsScriptCommandLineParser)
					{
						num4 = global::<PrivateImplementationDetails>.ComputeStringHash(text24);
						if (num4 <= 690232819U)
						{
							if (num4 <= 266776146U)
							{
								if (num4 != 57291420U)
								{
									if (num4 != 266776146U)
									{
										goto IL_2945;
									}
									if (!(text24 == "loadpath"))
									{
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "usings"))
									{
										goto IL_2945;
									}
									goto IL_0759;
								}
							}
							else if (num4 != 288002260U)
							{
								if (num4 != 671913016U)
								{
									if (num4 != 690232819U)
									{
										goto IL_2945;
									}
									if (!(text24 == "loadpaths"))
									{
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "-"))
									{
										goto IL_2945;
									}
									if (text25 != null)
									{
										goto IL_2945;
									}
									if (!(text23 == "-"))
									{
										flag26 = true;
										continue;
									}
									if (Console.IsInputRedirected)
									{
										list6.Add(new CommandLineSourceFile("-", true, true));
										flag18 = true;
										continue;
									}
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_StdInOptionProvidedButConsoleInputIsNotRedirected);
									continue;
								}
							}
							else
							{
								if (!(text24 == "import"))
								{
									goto IL_2945;
								}
								goto IL_0759;
							}
							CSharpCommandLineParser.ParseAndResolveReferencePaths(text24, text25, baseDirectory, list12, MessageID.IDS_REFERENCEPATH_OPTION, list);
							continue;
						}
						if (num4 <= 2251876157U)
						{
							if (num4 != 1775113223U)
							{
								if (num4 != 2151210443U)
								{
									if (num4 != 2251876157U)
									{
										goto IL_2945;
									}
									if (!(text24 == "i+"))
									{
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "i-"))
									{
										goto IL_2945;
									}
									if (text25 == null)
									{
										flag27 = false;
										continue;
									}
									goto IL_2945;
								}
							}
							else
							{
								if (!(text24 == "using"))
								{
									goto IL_2945;
								}
								goto IL_0759;
							}
						}
						else if (num4 != 2902570725U)
						{
							if (num4 != 3960223172U)
							{
								if (num4 != 4027333648U)
								{
									goto IL_2945;
								}
								if (!(text24 == "u"))
								{
									goto IL_2945;
								}
								goto IL_0759;
							}
							else if (!(text24 == "i"))
							{
								goto IL_2945;
							}
						}
						else
						{
							if (!(text24 == "imports"))
							{
								goto IL_2945;
							}
							goto IL_0759;
						}
						if (text25 == null)
						{
							flag27 = true;
							continue;
						}
						goto IL_2945;
						IL_0759:
						list14.AddRange(CSharpCommandLineParser.ParseUsings(text23, text25, list));
						continue;
					}
					num4 = global::<PrivateImplementationDetails>.ComputeStringHash(text24);
					if (num4 > 2348019775U)
					{
						if (num4 <= 3281798804U)
						{
							if (num4 <= 2793360781U)
							{
								if (num4 <= 2570337442U)
								{
									if (num4 <= 2474038896U)
									{
										if (num4 != 2456772291U)
										{
											if (num4 != 2458330400U)
											{
												if (num4 != 2474038896U)
												{
													goto IL_2945;
												}
												if (!(text24 == "warnaserror-"))
												{
													goto IL_2945;
												}
												if (text25 == null)
												{
													reportDiagnostic = ReportDiagnostic.Default;
													dictionary3.Clear();
													continue;
												}
												if (string.IsNullOrEmpty(text25))
												{
													CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
													continue;
												}
												using (IEnumerator<string> enumerator2 = CSharpCommandLineParser.ParseWarnings(text25).GetEnumerator())
												{
													while (enumerator2.MoveNext())
													{
														string text27 = enumerator2.Current;
														ReportDiagnostic reportDiagnostic2;
														if (dictionary.TryGetValue(text27, ref reportDiagnostic2))
														{
															dictionary3[text27] = reportDiagnostic2;
														}
														else
														{
															dictionary3[text27] = ReportDiagnostic.Default;
														}
													}
													continue;
												}
												goto IL_226B;
											}
											else
											{
												if (!(text24 == "subsystemversion"))
												{
													goto IL_2945;
												}
												if (RoslynString.IsNullOrEmpty(text25))
												{
													CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
													{
														MessageID.IDS_Text.Localize(),
														"subsystemversion"
													});
													continue;
												}
												SubsystemVersion none = SubsystemVersion.None;
												if (SubsystemVersion.TryParse(text25, out none))
												{
													subsystemVersion = none;
													continue;
												}
												CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_InvalidSubsystemVersion, new object[] { text25 });
												continue;
											}
										}
										else
										{
											if (!(text24 == "nullable-"))
											{
												goto IL_2945;
											}
											if (text25 == null)
											{
												nullableContextOptions = NullableContextOptions.Disable;
												continue;
											}
											goto IL_2945;
										}
									}
									else if (num4 != 2557438005U)
									{
										if (num4 != 2559445937U)
										{
											if (num4 != 2570337442U)
											{
												goto IL_2945;
											}
											if (!(text24 == "win32res"))
											{
												goto IL_2945;
											}
											text10 = CSharpCommandLineParser.GetWin32Setting(text23, text25, list);
											continue;
										}
										else
										{
											if (!(text24 == "publicsign"))
											{
												goto IL_2945;
											}
											goto IL_238E;
										}
									}
									else
									{
										if (!(text24 == "nullable+"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											nullableContextOptions = NullableContextOptions.Enable;
											continue;
										}
										goto IL_2945;
									}
								}
								else if (num4 <= 2590505776U)
								{
									if (num4 != 2573005743U)
									{
										if (num4 != 2574704610U)
										{
											if (num4 != 2590505776U)
											{
												goto IL_2945;
											}
											if (!(text24 == "instrument"))
											{
												goto IL_2945;
											}
											text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
											if (RoslynString.IsNullOrEmpty(text25))
											{
												CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text24 });
												continue;
											}
											using (IEnumerator<InstrumentationKind> enumerator3 = CSharpCommandLineParser.ParseInstrumentationKinds(text25, list).GetEnumerator())
											{
												while (enumerator3.MoveNext())
												{
													InstrumentationKind instrumentationKind = enumerator3.Current;
													if (!instance3.Contains(instrumentationKind))
													{
														instance3.Add(instrumentationKind);
													}
												}
												continue;
											}
											goto IL_1ABB;
										}
										else
										{
											if (!(text24 == "warnaserror+"))
											{
												goto IL_2945;
											}
											goto IL_214C;
										}
									}
									else
									{
										if (!(text24 == "codepage"))
										{
											goto IL_2945;
										}
										text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										if (text25 == null)
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text24 });
											continue;
										}
										Encoding encoding2 = CommandLineParser.TryParseEncodingName(text25);
										if (encoding2 == null)
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.FTL_BadCodepage, new object[] { text25 });
											continue;
										}
										encoding = encoding2;
										continue;
									}
								}
								else if (num4 <= 2695268429U)
								{
									if (num4 != 2644754498U)
									{
										if (num4 != 2695268429U)
										{
											goto IL_2945;
										}
										if (!(text24 == "linkres"))
										{
											goto IL_2945;
										}
										goto IL_1F99;
									}
									else
									{
										if (!(text24 == "recurse"))
										{
											goto IL_2945;
										}
										text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										if (text25 == null)
										{
											goto IL_2945;
										}
										if (RoslynString.IsNullOrEmpty(text25))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
											continue;
										}
										int count = list6.Count;
										list6.AddRange(base.ParseRecurseArgument(text25, baseDirectory, list));
										if (list6.Count > count)
										{
											flag18 = true;
											continue;
										}
										continue;
									}
								}
								else if (num4 != 2714222702U)
								{
									if (num4 != 2793360781U)
									{
										goto IL_2945;
									}
									if (!(text24 == "deterministic"))
									{
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "moduleassemblyname"))
									{
										goto IL_2945;
									}
									text25 = ((text25 != null) ? text25.Unquote() : null);
									if (RoslynString.IsNullOrEmpty(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text23 });
										continue;
									}
									if (!MetadataHelpers.IsValidAssemblyOrModuleName(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_InvalidAssemblyName, new object[] { "<text>", text23 });
										continue;
									}
									text14 = text25;
									continue;
								}
							}
							else if (num4 <= 3068394867U)
							{
								if (num4 <= 2912811502U)
								{
									if (num4 != 2819722039U)
									{
										if (num4 != 2870621791U)
										{
											if (num4 != 2912811502U)
											{
												goto IL_2945;
											}
											if (!(text24 == "unsafe-"))
											{
												goto IL_2945;
											}
											if (text25 == null)
											{
												flag7 = false;
												continue;
											}
											goto IL_2945;
										}
										else
										{
											if (!(text24 == "out"))
											{
												goto IL_2945;
											}
											if (RoslynString.IsNullOrWhiteSpace(text25))
											{
												CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
												continue;
											}
											base.ParseOutputFile(text25, list, baseDirectory, out text4, out text3);
											continue;
										}
									}
									else if (!(text24 == "unsafe"))
									{
										goto IL_2945;
									}
								}
								else if (num4 <= 2946366740U)
								{
									if (num4 != 2912915442U)
									{
										if (num4 != 2946366740U)
										{
											goto IL_2945;
										}
										if (!(text24 == "unsafe+"))
										{
											goto IL_2945;
										}
									}
									else
									{
										if (!(text24 == "nostdlib"))
										{
											goto IL_2945;
										}
										goto IL_26E8;
									}
								}
								else if (num4 != 3056178685U)
								{
									if (num4 != 3068394867U)
									{
										goto IL_2945;
									}
									if (!(text24 == "analyzer"))
									{
										goto IL_2945;
									}
									goto IL_1814;
								}
								else
								{
									if (!(text24 == "warnaserror"))
									{
										goto IL_2945;
									}
									goto IL_214C;
								}
								if (text25 == null)
								{
									flag7 = true;
									continue;
								}
								goto IL_2945;
							}
							else if (num4 <= 3127325408U)
							{
								if (num4 != 3068697161U)
								{
									if (num4 != 3116353128U)
									{
										if (num4 != 3127325408U)
										{
											goto IL_2945;
										}
										if (!(text24 == "deterministic-"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag9 = false;
											continue;
										}
										goto IL_2945;
									}
									else
									{
										if (!(text24 == "sourcelink"))
										{
											goto IL_2945;
										}
										text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										if (RoslynString.IsNullOrEmpty(text25))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
											continue;
										}
										text18 = base.ParseGenericPathToFile(text25, list, baseDirectory, true);
										continue;
									}
								}
								else
								{
									if (!(text24 == "skipanalyzers"))
									{
										goto IL_2945;
									}
									goto IL_26CA;
								}
							}
							else if (num4 <= 3248243566U)
							{
								if (num4 != 3227991122U)
								{
									if (num4 != 3248243566U)
									{
										goto IL_2945;
									}
									if (!(text24 == "publicsign+"))
									{
										goto IL_2945;
									}
									goto IL_238E;
								}
								else if (!(text24 == "deterministic+"))
								{
									goto IL_2945;
								}
							}
							else if (num4 != 3257533744U)
							{
								if (num4 != 3281798804U)
								{
									goto IL_2945;
								}
								if (!(text24 == "publicsign-"))
								{
									goto IL_2945;
								}
								if (text25 == null)
								{
									flag28 = false;
									continue;
								}
								goto IL_2945;
							}
							else
							{
								if (!(text24 == "pathmap"))
								{
									goto IL_2945;
								}
								string text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (text28 != null)
								{
									immutableArray = immutableArray.Concat<KeyValuePair<string, string>>(base.ParsePathMap(text28, list));
									continue;
								}
								goto IL_2945;
							}
							if (text25 == null)
							{
								flag9 = true;
								continue;
							}
							goto IL_2945;
							IL_214C:
							if (text25 == null)
							{
								reportDiagnostic = ReportDiagnostic.Error;
								dictionary3.Clear();
								using (Dictionary<string, ReportDiagnostic>.KeyCollection.Enumerator enumerator4 = dictionary.Keys.GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										string text29 = enumerator4.Current;
										if (dictionary[text29] == ReportDiagnostic.Warn)
										{
											dictionary3[text29] = ReportDiagnostic.Error;
										}
									}
									continue;
								}
							}
							if (string.IsNullOrEmpty(text25))
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
								continue;
							}
							CSharpCommandLineParser.AddWarnings(dictionary3, ReportDiagnostic.Error, CSharpCommandLineParser.ParseWarnings(text25));
							continue;
							IL_238E:
							if (text25 == null)
							{
								flag28 = true;
								continue;
							}
							goto IL_2945;
						}
						else if (num4 <= 3826002220U)
						{
							if (num4 <= 3612909048U)
							{
								if (num4 <= 3562318266U)
								{
									if (num4 != 3283884827U)
									{
										if (num4 != 3286799396U)
										{
											if (num4 != 3562318266U)
											{
												goto IL_2945;
											}
											if (!(text24 == "nowarn"))
											{
												goto IL_2945;
											}
											if (text25 == null)
											{
												CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
												continue;
											}
											if (string.IsNullOrEmpty(text25))
											{
												CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
												continue;
											}
											CSharpCommandLineParser.AddWarnings(dictionary2, ReportDiagnostic.Suppress, CSharpCommandLineParser.ParseWarnings(text25));
											continue;
										}
										else
										{
											if (!(text24 == "refonly"))
											{
												goto IL_2945;
											}
											if (text25 == null)
											{
												flag13 = true;
												continue;
											}
											goto IL_2945;
										}
									}
									else
									{
										if (!(text24 == "bugreport"))
										{
											goto IL_2945;
										}
										CSharpCommandLineParser.UnimplementedSwitch(list, text24);
										continue;
									}
								}
								else if (num4 != 3589461161U)
								{
									if (num4 != 3598478835U)
									{
										if (num4 != 3612909048U)
										{
											goto IL_2945;
										}
										if (!(text24 == "fullpaths"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag22 = true;
											continue;
										}
										goto IL_2945;
									}
									else
									{
										if (!(text24 == "utf8output"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag15 = true;
											continue;
										}
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "delaysign"))
									{
										goto IL_2945;
									}
									goto IL_2366;
								}
							}
							else if (num4 <= 3689034735U)
							{
								if (num4 != 3638623282U)
								{
									if (num4 != 3655479497U)
									{
										if (num4 != 3689034735U)
										{
											goto IL_2945;
										}
										if (!(text24 == "debug-"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag10 = false;
											flag11 = false;
											continue;
										}
										goto IL_2945;
									}
									else
									{
										if (!(text24 == "debug+"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag10 = true;
											flag11 = true;
											continue;
										}
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "parallel"))
									{
										goto IL_2945;
									}
									goto IL_212E;
								}
							}
							else if (num4 <= 3775669363U)
							{
								if (num4 != 3697880417U)
								{
									if (num4 != 3775669363U)
									{
										goto IL_2945;
									}
									if (!(text24 == "d"))
									{
										goto IL_2945;
									}
									goto IL_182A;
								}
								else
								{
									if (!(text24 == "nosdkpath"))
									{
										goto IL_2945;
									}
									sdkDirectory = null;
									continue;
								}
							}
							else if (num4 != 3816167482U)
							{
								if (num4 != 3826002220U)
								{
									goto IL_2945;
								}
								if (!(text24 == "a"))
								{
									goto IL_2945;
								}
							}
							else
							{
								if (!(text24 == "appconfig"))
								{
									goto IL_2945;
								}
								string text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (RoslynString.IsNullOrEmpty(text28))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
									{
										":<text>",
										CommandLineParser.RemoveQuotesAndSlashes(text23)
									});
									continue;
								}
								text = base.ParseGenericPathToFile(text28, list, baseDirectory, true);
								continue;
							}
						}
						else if (num4 <= 4036644977U)
						{
							string text28;
							if (num4 <= 3926667934U)
							{
								if (num4 != 3893112696U)
								{
									if (num4 != 3909890315U)
									{
										if (num4 != 3926667934U)
										{
											goto IL_2945;
										}
										if (!(text24 == "o"))
										{
											goto IL_2945;
										}
										goto IL_20F2;
									}
									else
									{
										if (!(text24 == "l"))
										{
											goto IL_2945;
										}
										goto IL_1F14;
									}
								}
								else if (!(text24 == "m"))
								{
									goto IL_2945;
								}
							}
							else if (num4 <= 3935363592U)
							{
								if (num4 != 3932734293U)
								{
									if (num4 != 3935363592U)
									{
										goto IL_2945;
									}
									if (!(text24 == "main"))
									{
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "doc"))
									{
										goto IL_2945;
									}
									flag14 = true;
									if (RoslynString.IsNullOrEmpty(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
										{
											MessageID.IDS_Text.Localize(),
											text23
										});
										continue;
									}
									text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (RoslynString.IsNullOrEmpty(text28))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
										{
											MessageID.IDS_Text.Localize(),
											"/doc:"
										});
										continue;
									}
									text7 = base.ParseGenericPathToFile(text28, list, baseDirectory, true);
									continue;
								}
							}
							else if (num4 != 4007191885U)
							{
								if (num4 != 4036644977U)
								{
									goto IL_2945;
								}
								if (!(text24 == "analyzerconfig"))
								{
									goto IL_2945;
								}
								goto IL_2883;
							}
							else
							{
								if (!(text24 == "nologo"))
								{
									goto IL_2945;
								}
								flag = false;
								continue;
							}
							text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
							if (string.IsNullOrEmpty(text28))
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text24 });
								continue;
							}
							text8 = text28;
							continue;
						}
						else if (num4 <= 4060888886U)
						{
							if (num4 != 4042037662U)
							{
								if (num4 != 4044111267U)
								{
									if (num4 != 4060888886U)
									{
										goto IL_2945;
									}
									if (!(text24 == "w"))
									{
										goto IL_2945;
									}
									goto IL_226B;
								}
								else
								{
									if (!(text24 == "t"))
									{
										goto IL_2945;
									}
									goto IL_1C11;
								}
							}
							else
							{
								if (!(text24 == "modulename"))
								{
									goto IL_2945;
								}
								string text30 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (string.IsNullOrEmpty(text30))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
									{
										MessageID.IDS_Text.Localize(),
										"modulename"
									});
									continue;
								}
								text15 = text30;
								continue;
							}
						}
						else if (num4 <= 4181670897U)
						{
							if (num4 != 4111221743U)
							{
								if (num4 != 4181670897U)
								{
									goto IL_2945;
								}
								if (!(text24 == "checked-"))
								{
									goto IL_2945;
								}
								if (text25 == null)
								{
									flag6 = false;
									continue;
								}
								goto IL_2945;
							}
							else
							{
								if (!(text24 == "p"))
								{
									goto IL_2945;
								}
								goto IL_212E;
							}
						}
						else if (num4 != 4192678414U)
						{
							if (num4 != 4215226135U)
							{
								goto IL_2945;
							}
							if (!(text24 == "checked+"))
							{
								goto IL_2945;
							}
							goto IL_1936;
						}
						else
						{
							if (!(text24 == "optimize"))
							{
								goto IL_2945;
							}
							goto IL_20F2;
						}
						IL_1814:
						list10.AddRange(CSharpCommandLineParser.ParseAnalyzers(text23, text25, list));
						continue;
					}
					if (num4 <= 1312070580U)
					{
						if (num4 <= 671913016U)
						{
							if (num4 <= 514869512U)
							{
								if (num4 <= 391860326U)
								{
									if (num4 != 81969206U)
									{
										if (num4 != 232457833U)
										{
											if (num4 != 391860326U)
											{
												goto IL_2945;
											}
											if (!(text24 == "highentropyva-"))
											{
												goto IL_2945;
											}
											if (text25 == null)
											{
												flag21 = false;
												continue;
											}
											goto IL_2945;
										}
										else
										{
											if (!(text24 == "link"))
											{
												goto IL_2945;
											}
											goto IL_1F14;
										}
									}
									else
									{
										if (!(text24 == "refout"))
										{
											goto IL_2945;
										}
										text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										if (RoslynString.IsNullOrEmpty(text25))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
											continue;
										}
										text5 = base.ParseGenericPathToFile(text25, list, baseDirectory, true);
										continue;
									}
								}
								else if (num4 != 425415564U)
								{
									if (num4 != 447319523U)
									{
										if (num4 != 514869512U)
										{
											goto IL_2945;
										}
										if (!(text24 == "baseaddress"))
										{
											goto IL_2945;
										}
										text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										ulong num5;
										if (!string.IsNullOrEmpty(text25) && CommandLineParser.TryParseUInt64(text25, out num5))
										{
											num = num5;
											continue;
										}
										if (RoslynString.IsNullOrEmpty(text25))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
											continue;
										}
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadBaseNumber, new object[] { text25 });
										continue;
									}
									else
									{
										if (!(text24 == "keycontainer"))
										{
											goto IL_2945;
										}
										if (string.IsNullOrEmpty(text25))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
											{
												MessageID.IDS_Text.Localize(),
												"keycontainer"
											});
											continue;
										}
										text13 = text25;
										continue;
									}
								}
								else
								{
									if (!(text24 == "highentropyva+"))
									{
										goto IL_2945;
									}
									goto IL_2427;
								}
							}
							else if (num4 <= 625233524U)
							{
								if (num4 != 554782406U)
								{
									if (num4 != 607375305U)
									{
										if (num4 != 625233524U)
										{
											goto IL_2945;
										}
										if (!(text24 == "noconfig"))
										{
											goto IL_2945;
										}
										continue;
									}
									else
									{
										if (!(text24 == "o-"))
										{
											goto IL_2945;
										}
										goto IL_2101;
									}
								}
								else
								{
									if (!(text24 == "checked"))
									{
										goto IL_2945;
									}
									goto IL_1936;
								}
							}
							else if (num4 <= 638064026U)
							{
								if (num4 != 632330593U)
								{
									if (num4 != 638064026U)
									{
										goto IL_2945;
									}
									if (!(text24 == "keyfile"))
									{
										goto IL_2945;
									}
									text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (string.IsNullOrEmpty(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { "keyfile" });
										continue;
									}
									text12 = text25;
									continue;
								}
								else
								{
									if (!(text24 == "win32manifest"))
									{
										goto IL_2945;
									}
									text9 = CSharpCommandLineParser.GetWin32Setting(text23, text25, list);
									flag16 = false;
									continue;
								}
							}
							else if (num4 != 640930543U)
							{
								if (num4 != 671913016U)
								{
									goto IL_2945;
								}
								if (!(text24 == "-"))
								{
									goto IL_2945;
								}
							}
							else
							{
								if (!(text24 == "o+"))
								{
									goto IL_2945;
								}
								goto IL_20F2;
							}
						}
						else
						{
							if (num4 <= 738948484U)
							{
								if (num4 <= 706876140U)
								{
									if (num4 != 672900686U)
									{
										if (num4 != 702513141U)
										{
											if (num4 != 706876140U)
											{
												goto IL_2945;
											}
											if (!(text24 == "nowin32manifest"))
											{
												goto IL_2945;
											}
											flag16 = true;
											text9 = null;
											continue;
										}
										else if (!(text24 == "resource"))
										{
											goto IL_2945;
										}
									}
									else
									{
										if (!(text24 == "embed"))
										{
											goto IL_2945;
										}
										if (RoslynString.IsNullOrEmpty(text25))
										{
											flag19 = true;
											continue;
										}
										using (IEnumerator<string> enumerator2 = base.ParseSeparatedFileArgument(text25, baseDirectory, list).GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												string text31 = enumerator2.Current;
												list8.Add(base.ToCommandLineSourceFile(text31, false));
											}
											continue;
										}
										goto IL_2919;
									}
								}
								else if (num4 != 709505714U)
								{
									if (num4 != 724529819U)
									{
										if (num4 != 738948484U)
										{
											goto IL_2945;
										}
										if (!(text24 == "touchedfiles"))
										{
											goto IL_2945;
										}
										string text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
										if (string.IsNullOrEmpty(text28))
										{
											CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
											{
												MessageID.IDS_Text.Localize(),
												"touchedfiles"
											});
											continue;
										}
										text17 = text28;
										continue;
									}
									else
									{
										if (!(text24 == "nostdlib+"))
										{
											goto IL_2945;
										}
										goto IL_26E8;
									}
								}
								else
								{
									if (!(text24 == "platform"))
									{
										goto IL_2945;
									}
									text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (RoslynString.IsNullOrEmpty(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<string>", text23 });
										continue;
									}
									platform = CSharpCommandLineParser.ParsePlatform(text25, list);
									continue;
								}
							}
							else if (num4 <= 845187144U)
							{
								if (num4 != 804546073U)
								{
									if (num4 != 825195533U)
									{
										if (num4 != 845187144U)
										{
											goto IL_2945;
										}
										if (!(text24 == "target"))
										{
											goto IL_2945;
										}
										goto IL_1C11;
									}
									else
									{
										if (!(text24 == "nostdlib-"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag12 = false;
											continue;
										}
										goto IL_2945;
									}
								}
								else if (!(text24 == "res"))
								{
									goto IL_2945;
								}
							}
							else if (num4 <= 1026574197U)
							{
								if (num4 != 847356170U)
								{
									if (num4 != 1026574197U)
									{
										goto IL_2945;
									}
									if (!(text24 == "reportanalyzer"))
									{
										goto IL_2945;
									}
									flag24 = true;
									continue;
								}
								else
								{
									if (!(text24 == "additionalfile"))
									{
										goto IL_2945;
									}
									if (RoslynString.IsNullOrEmpty(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<file list>", text24 });
										continue;
									}
									using (IEnumerator<string> enumerator2 = base.ParseSeparatedFileArgument(text25, baseDirectory, list).GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											string text32 = enumerator2.Current;
											list7.Add(base.ToCommandLineSourceFile(text32, false));
										}
										continue;
									}
									goto IL_2883;
								}
							}
							else if (num4 != 1248630781U)
							{
								if (num4 != 1312070580U)
								{
									goto IL_2945;
								}
								if (!(text24 == "preferreduilang"))
								{
									goto IL_2945;
								}
								text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (RoslynString.IsNullOrEmpty(text25))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text23 });
									continue;
								}
								try
								{
									cultureInfo = new CultureInfo(text25);
									if ((cultureInfo.CultureTypes & 8) != null)
									{
										cultureInfo = null;
									}
								}
								catch (CultureNotFoundException)
								{
								}
								if (cultureInfo == null)
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.WRN_BadUILang, new object[] { text25 });
									continue;
								}
								continue;
							}
							else
							{
								if (!(text24 == "errorendlocation"))
								{
									goto IL_2945;
								}
								flag23 = true;
								continue;
							}
							if (text25 == null)
							{
								goto IL_2945;
							}
							ResourceDescription resourceDescription = CSharpCommandLineParser.ParseResourceDescription(text23, text25, baseDirectory, list, true);
							if (resourceDescription != null)
							{
								list5.Add(resourceDescription);
								flag20 = true;
								continue;
							}
							continue;
						}
						IL_2919:
						if (Console.IsInputRedirected)
						{
							list6.Add(new CommandLineSourceFile("-", false, true));
							flag18 = true;
							continue;
						}
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_StdInOptionProvidedButConsoleInputIsNotRedirected);
						continue;
					}
					else if (num4 <= 1805613231U)
					{
						if (num4 <= 1412107116U)
						{
							if (num4 <= 1369110961U)
							{
								if (num4 != 1329891494U)
								{
									if (num4 != 1363446732U)
									{
										if (num4 != 1369110961U)
										{
											goto IL_2945;
										}
										if (!(text24 == "ruleset"))
										{
											goto IL_2945;
										}
										continue;
									}
									else
									{
										if (!(text24 == "delaysign-"))
										{
											goto IL_2945;
										}
										if (text25 == null)
										{
											flag17 = new bool?(false);
											continue;
										}
										goto IL_2945;
									}
								}
								else
								{
									if (!(text24 == "delaysign+"))
									{
										goto IL_2945;
									}
									goto IL_2366;
								}
							}
							else if (num4 != 1370291513U)
							{
								if (num4 != 1378551878U)
								{
									if (num4 != 1412107116U)
									{
										goto IL_2945;
									}
									if (!(text24 == "skipanalyzers-"))
									{
										goto IL_2945;
									}
									if (text25 == null)
									{
										flag25 = false;
										continue;
									}
									goto IL_2945;
								}
								else
								{
									if (!(text24 == "skipanalyzers+"))
									{
										goto IL_2945;
									}
									goto IL_26CA;
								}
							}
							else
							{
								if (!(text24 == "linkresource"))
								{
									goto IL_2945;
								}
								goto IL_1F99;
							}
						}
						else if (num4 <= 1550724156U)
						{
							if (num4 != 1475868319U)
							{
								if (num4 != 1483009432U)
								{
									if (num4 != 1550724156U)
									{
										goto IL_2945;
									}
									if (!(text24 == "nullable"))
									{
										goto IL_2945;
									}
									text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (text25 == null)
									{
										nullableContextOptions = NullableContextOptions.Enable;
										continue;
									}
									if (text25.IsEmpty())
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
										{
											MessageID.IDS_Text.Localize(),
											text24
										});
										continue;
									}
									string text33 = text25.ToLower();
									if (text33 == "disable")
									{
										nullableContextOptions = NullableContextOptions.Disable;
										continue;
									}
									if (text33 == "enable")
									{
										nullableContextOptions = NullableContextOptions.Enable;
										continue;
									}
									if (text33 == "warnings")
									{
										nullableContextOptions = NullableContextOptions.Warnings;
										continue;
									}
									if (!(text33 == "annotations"))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadNullableContextOption, new object[] { text25 });
										continue;
									}
									nullableContextOptions = NullableContextOptions.Annotations;
									continue;
								}
								else
								{
									if (!(text24 == "debug"))
									{
										goto IL_2945;
									}
									flag10 = true;
									text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (text25 == null)
									{
										continue;
									}
									if (text25.IsEmpty())
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
										{
											MessageID.IDS_Text.Localize(),
											text24
										});
										continue;
									}
									text34 = text25.ToLower();
									if (text34 == "full" || text34 == "pdbonly")
									{
										debugInformationFormat = (PathUtilities.IsUnixLikePlatform ? DebugInformationFormat.PortablePdb : DebugInformationFormat.Pdb);
										continue;
									}
									if (text34 == "portable")
									{
										debugInformationFormat = DebugInformationFormat.PortablePdb;
										continue;
									}
									if (!(text34 == "embedded"))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadDebugType, new object[] { text25 });
										continue;
									}
									debugInformationFormat = DebugInformationFormat.Embedded;
									continue;
								}
							}
							else
							{
								if (!(text24 == "errorreport"))
								{
									goto IL_2945;
								}
								continue;
							}
						}
						else if (num4 <= 1750515365U)
						{
							if (num4 != 1699594953U)
							{
								if (num4 != 1750515365U)
								{
									goto IL_2945;
								}
								if (!(text24 == "sqmsessionguid"))
								{
									goto IL_2945;
								}
								goto IL_1ABB;
							}
							else
							{
								if (!(text24 == "pdb"))
								{
									goto IL_2945;
								}
								text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (RoslynString.IsNullOrEmpty(text25))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
									continue;
								}
								text2 = base.ParsePdbPath(text25, list, baseDirectory);
								continue;
							}
						}
						else if (num4 != 1788839250U)
						{
							if (num4 != 1805613231U)
							{
								goto IL_2945;
							}
							if (!(text24 == "checksumalgorithm"))
							{
								goto IL_2945;
							}
							if (RoslynString.IsNullOrEmpty(text25))
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text24 });
								continue;
							}
							SourceHashAlgorithm sourceHashAlgorithm2 = CommandLineParser.TryParseHashAlgorithmName(text25);
							if (sourceHashAlgorithm2 == SourceHashAlgorithm.None)
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.FTL_BadChecksumAlgorithm, new object[] { text25 });
								continue;
							}
							sourceHashAlgorithm = sourceHashAlgorithm2;
							continue;
						}
						else
						{
							if (!(text24 == "define"))
							{
								goto IL_2945;
							}
							goto IL_182A;
						}
					}
					else if (num4 <= 2177680879U)
					{
						if (num4 <= 1961742991U)
						{
							if (num4 != 1810949389U)
							{
								if (num4 != 1829198682U)
								{
									if (num4 != 1961742991U)
									{
										goto IL_2945;
									}
									if (!(text24 == "generatedfilesout"))
									{
										goto IL_2945;
									}
									text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (string.IsNullOrWhiteSpace(text25))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
										{
											MessageID.IDS_Text.Localize(),
											text23
										});
										continue;
									}
									text6 = base.ParseGenericPathToFile(text25, list, baseDirectory, true);
									continue;
								}
								else
								{
									if (!(text24 == "runtimemetadataversion"))
									{
										goto IL_2945;
									}
									string text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
									if (string.IsNullOrEmpty(text28))
									{
										CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text24 });
										continue;
									}
									text16 = text28;
									continue;
								}
							}
							else
							{
								if (!(text24 == "errorlog"))
								{
									goto IL_2945;
								}
								string text28 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (RoslynString.IsNullOrEmpty(text28))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
									{
										"<file>[,version={1|1.0|2|2.1}]",
										CommandLineParser.RemoveQuotesAndSlashes(text23)
									});
									continue;
								}
								bool flag29;
								errorLogOptions = base.ParseErrorLogOptions(text28, list, baseDirectory, out flag29);
								if (errorLogOptions == null && !flag29)
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadSwitchValue, new object[] { text28, "/errorlog:", "<file>[,version={1|1.0|2|2.1}]" });
									continue;
								}
								continue;
							}
						}
						else if (num4 <= 2128741192U)
						{
							if (num4 != 2104310437U)
							{
								if (num4 != 2128741192U)
								{
									goto IL_2945;
								}
								if (!(text24 == "filealign"))
								{
									goto IL_2945;
								}
								text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
								if (RoslynString.IsNullOrEmpty(text25))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
									continue;
								}
								ushort num6;
								if (!CommandLineParser.TryParseUInt16(text25, out num6))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_InvalidFileAlignment, new object[] { text25 });
									continue;
								}
								if (!CompilationOptions.IsValidFileAlignment((int)num6))
								{
									CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_InvalidFileAlignment, new object[] { text25 });
									continue;
								}
								num2 = (int)num6;
								continue;
							}
							else
							{
								if (!(text24 == "win32icon"))
								{
									goto IL_2945;
								}
								text11 = CSharpCommandLineParser.GetWin32Setting(text23, text25, list);
								continue;
							}
						}
						else if (num4 != 2138038108U)
						{
							if (num4 != 2177680879U)
							{
								goto IL_2945;
							}
							if (!(text24 == "highentropyva"))
							{
								goto IL_2945;
							}
							goto IL_2427;
						}
						else
						{
							if (!(text24 == "addmodule"))
							{
								goto IL_2945;
							}
							if (text25 == null)
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[]
								{
									MessageID.IDS_Text.Localize(),
									"/addmodule:"
								});
								continue;
							}
							if (string.IsNullOrEmpty(text25))
							{
								CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoFileSpec, new object[] { text23 });
								continue;
							}
							list9.AddRange(Enumerable.Select<string, CommandLineReference>(CommandLineParser.ParseSeparatedPaths(text25), (string path) => new CommandLineReference(path, MetadataReferenceProperties.Module)));
							flag20 = true;
							continue;
						}
					}
					else
					{
						if (num4 <= 2231003889U)
						{
							if (num4 != 2200755035U)
							{
								if (num4 != 2219747942U)
								{
									if (num4 != 2231003889U)
									{
										goto IL_2945;
									}
									if (!(text24 == "warn"))
									{
										goto IL_2945;
									}
									goto IL_226B;
								}
								else if (!(text24 == "p-"))
								{
									goto IL_2945;
								}
							}
							else
							{
								if (!(text24 == "parallel+"))
								{
									goto IL_2945;
								}
								goto IL_212E;
							}
						}
						else if (num4 <= 2301420749U)
						{
							if (num4 != 2253303180U)
							{
								if (num4 != 2301420749U)
								{
									goto IL_2945;
								}
								if (!(text24 == "parallel-"))
								{
									goto IL_2945;
								}
							}
							else
							{
								if (!(text24 == "p+"))
								{
									goto IL_2945;
								}
								goto IL_212E;
							}
						}
						else if (num4 != 2314464537U)
						{
							if (num4 != 2348019775U)
							{
								goto IL_2945;
							}
							if (!(text24 == "optimize+"))
							{
								goto IL_2945;
							}
							goto IL_20F2;
						}
						else
						{
							if (!(text24 == "optimize-"))
							{
								goto IL_2945;
							}
							goto IL_2101;
						}
						if (text25 == null)
						{
							flag8 = false;
							continue;
						}
						goto IL_2945;
					}
					IL_2101:
					if (text25 == null)
					{
						flag5 = false;
						continue;
					}
					goto IL_2945;
					IL_2427:
					if (text25 == null)
					{
						flag21 = true;
						continue;
					}
					goto IL_2945;
					IL_182A:
					if (RoslynString.IsNullOrEmpty(text25))
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<text>", text23 });
						continue;
					}
					IEnumerable<Diagnostic> enumerable;
					instance2.AddRange(CSharpCommandLineParser.ParseConditionalCompilationSymbols(CommandLineParser.RemoveQuotesAndSlashes(text25), out enumerable));
					list.AddRange(enumerable);
					continue;
					IL_1936:
					if (text25 == null)
					{
						flag6 = true;
						continue;
					}
					goto IL_2945;
					IL_1ABB:
					if (text25 == null)
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_MissingGuidForOption, new object[] { "<text>", text24 });
						continue;
					}
					Guid guid;
					if (!Guid.TryParse(text25, ref guid))
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_InvalidFormatForGuidForOption, new object[] { text25, text24 });
						continue;
					}
					continue;
					IL_1C11:
					if (text25 == null)
					{
						goto IL_2945;
					}
					if (RoslynString.IsNullOrEmpty(text25))
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.FTL_InvalidTarget);
						continue;
					}
					outputKind = CSharpCommandLineParser.ParseTarget(text25, list);
					continue;
					IL_1F14:
					list9.AddRange(CSharpCommandLineParser.ParseAssemblyReferences(text23, text25, list, true));
					continue;
					IL_1F99:
					if (text25 == null)
					{
						goto IL_2945;
					}
					ResourceDescription resourceDescription2 = CSharpCommandLineParser.ParseResourceDescription(text23, text25, baseDirectory, list, false);
					if (resourceDescription2 != null)
					{
						list5.Add(resourceDescription2);
						flag20 = true;
						continue;
					}
					continue;
					IL_20F2:
					if (text25 == null)
					{
						flag5 = true;
						continue;
					}
					goto IL_2945;
					IL_212E:
					if (text25 == null)
					{
						flag8 = true;
						continue;
					}
					goto IL_2945;
					IL_226B:
					text25 = CommandLineParser.RemoveQuotesAndSlashes(text25);
					if (text25 == null)
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
						continue;
					}
					int num7;
					if (string.IsNullOrEmpty(text25) || !int.TryParse(text25, 7, CultureInfo.InvariantCulture, ref num7))
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsNumber, new object[] { text24 });
						continue;
					}
					if (num7 < 0)
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadWarningLevel, new object[] { text24 });
						continue;
					}
					num3 = num7;
					continue;
					IL_2366:
					if (text25 == null)
					{
						flag17 = new bool?(true);
						continue;
					}
					goto IL_2945;
					IL_26CA:
					if (text25 == null)
					{
						flag25 = true;
						continue;
					}
					goto IL_2945;
					IL_26E8:
					if (text25 == null)
					{
						flag12 = true;
						continue;
					}
					goto IL_2945;
					IL_2883:
					if (RoslynString.IsNullOrEmpty(text25))
					{
						CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SwitchNeedsString, new object[] { "<file list>", text24 });
						continue;
					}
					instance.AddRange(base.ParseSeparatedFileArgument(text25, baseDirectory, list));
					continue;
					IL_2945:
					CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadSwitch, new object[] { text23 });
				}
			}
			foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair in dictionary3)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<string, ReportDiagnostic> keyValuePair2 in dictionary2)
			{
				dictionary[keyValuePair2.Key] = keyValuePair2.Value;
			}
			if (flag13 && text5 != null)
			{
				CSharpCommandLineParser.AddDiagnostic(list, dictionary, ErrorCode.ERR_NoRefOutWhenRefOnly, Array.Empty<object>());
			}
			if (outputKind == OutputKind.NetModule && (flag13 || text5 != null))
			{
				CSharpCommandLineParser.AddDiagnostic(list, dictionary, ErrorCode.ERR_NoNetModuleOutputWhenRefOutOrRefOnly, Array.Empty<object>());
			}
			if (!this.IsScriptCommandLineParser && !flag18 && (outputKind.IsNetModule() || !flag20))
			{
				CSharpCommandLineParser.AddDiagnostic(list, dictionary, ErrorCode.WRN_NoSources, Array.Empty<object>());
			}
			if (!flag12 && sdkDirectory != null)
			{
				list9.Insert(0, new CommandLineReference(Path.Combine(sdkDirectory, "mscorlib.dll"), MetadataReferenceProperties.Assembly));
			}
			if (!platform.Requires64Bit() && num > (ulong)(-32769))
			{
				CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_BadBaseNumber, new object[] { string.Format("0x{0:X}", num) });
				num = 0UL;
			}
			if (!string.IsNullOrEmpty(additionalReferenceDirectories))
			{
				CSharpCommandLineParser.ParseAndResolveReferencePaths(null, additionalReferenceDirectories, baseDirectory, list11, MessageID.IDS_LIB_ENV, list);
			}
			ImmutableArray<string> immutableArray2 = this.BuildSearchPaths(sdkDirectory, list11, list4);
			CSharpCommandLineParser.ValidateWin32Settings(text10, text11, text9, outputKind, list);
			if (!RoslynString.IsNullOrEmpty(baseDirectory))
			{
				list13.Add(baseDirectory);
			}
			if (RoslynString.IsNullOrEmpty(text3))
			{
				CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_NoOutputDirectory);
			}
			else if (baseDirectory != text3)
			{
				list13.Add(text3);
			}
			if (flag28 && !RoslynString.IsNullOrEmpty(text12))
			{
				text12 = base.ParseGenericPathToFile(text12, list, baseDirectory, true);
			}
			if (text18 != null && !flag10)
			{
				CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_SourceLinkRequiresPdb);
			}
			if (flag19)
			{
				list8.AddRange(list6);
			}
			if (list8.Count > 0 && !flag10)
			{
				CSharpCommandLineParser.AddDiagnostic(list, ErrorCode.ERR_CannotEmbedWithoutPdb);
			}
			ImmutableDictionary<string, string> immutableDictionary = CommandLineParser.ParseFeatures(list15);
			string text35;
			this.GetCompilationAndModuleNames(list, outputKind, list6, flag18, text14, ref text4, ref text15, out text35);
			LanguageVersion languageVersion2 = languageVersion;
			ImmutableArray<string> immutableArray3 = instance2.ToImmutableAndFree();
			CSharpParseOptions csharpParseOptions = new CSharpParseOptions(languageVersion2, flag14 ? DocumentationMode.Diagnose : DocumentationMode.None, this.IsScriptCommandLineParser ? SourceCodeKind.Script : SourceCodeKind.Regular, immutableArray3, immutableDictionary);
			bool flag30 = errorLogOptions != null;
			OutputKind outputKind2 = outputKind;
			text34 = text15;
			string text36 = text8;
			IEnumerable<string> enumerable2 = list14;
			OptimizationLevel optimizationLevel = (flag5 ? OptimizationLevel.Release : OptimizationLevel.Debug);
			bool flag31 = flag6;
			NullableContextOptions nullableContextOptions2 = nullableContextOptions;
			bool flag32 = flag7;
			bool flag33 = flag9;
			bool flag34 = flag8;
			string text37 = text13;
			string text38 = text12;
			bool? flag35 = flag17;
			Platform platform2 = platform;
			ReportDiagnostic reportDiagnostic3 = reportDiagnostic;
			int num8 = num3;
			IEnumerable<KeyValuePair<string, ReportDiagnostic>> enumerable3 = dictionary;
			bool flag36 = flag30;
			string text39 = text34;
			string text40 = text36;
			string text41 = "Script";
			IEnumerable<string> enumerable4 = enumerable2;
			OptimizationLevel optimizationLevel2 = optimizationLevel;
			bool flag37 = flag31;
			bool flag38 = flag32;
			string text42 = text37;
			string text43 = text38;
			bool flag39 = flag28;
			CSharpCompilationOptions csharpCompilationOptions = new CSharpCompilationOptions(outputKind2, flag36, text39, text40, text41, enumerable4, optimizationLevel2, flag37, flag38, text42, text43, default(ImmutableArray<byte>), flag35, platform2, reportDiagnostic3, num8, enumerable3, flag34, flag33, null, null, null, null, null, flag39, MetadataImportOptions.Public, nullableContextOptions2);
			if (flag11)
			{
				csharpCompilationOptions = csharpCompilationOptions.WithDebugPlusMode(flag11);
			}
			bool flag40 = flag13;
			flag39 = !flag13 && text5 == null;
			DebugInformationFormat debugInformationFormat2 = debugInformationFormat;
			string text44 = null;
			string text45 = null;
			ulong num9 = num;
			flag34 = flag21;
			EmitOptions emitOptions = new EmitOptions(flag40, debugInformationFormat2, text44, text45, num2, num9, flag34, subsystemVersion, text16, false, flag39, instance3.ToImmutableAndFree(), new HashAlgorithmName?(HashAlgorithmName.SHA256), encoding, null);
			list.AddRange(csharpCompilationOptions.Errors);
			list.AddRange(csharpParseOptions.Errors);
			if (nullableContextOptions != NullableContextOptions.Disable && csharpParseOptions.LanguageVersion < MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion())
			{
				list.Add(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_NullableOptionNotAvailable, new object[]
				{
					"nullable",
					nullableContextOptions,
					csharpParseOptions.LanguageVersion.ToDisplayString(),
					new CSharpRequiredLanguageVersion(MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion())
				}), Location.None, false));
			}
			immutableArray = CommandLineParser.SortPathMap(immutableArray);
			return new CSharpCommandLineArguments
			{
				IsScriptRunner = this.IsScriptCommandLineParser,
				InteractiveMode = (flag27 || (this.IsScriptCommandLineParser && list6.Count == 0)),
				BaseDirectory = baseDirectory,
				PathMap = immutableArray,
				Errors = list.AsImmutable<Diagnostic>(),
				Utf8Output = flag15,
				CompilationName = text35,
				OutputFileName = text4,
				OutputRefFilePath = text5,
				PdbPath = text2,
				EmitPdb = (flag10 && !flag13),
				SourceLink = text18,
				RuleSetPath = text19,
				OutputDirectory = text3,
				DocumentationPath = text7,
				GeneratedFilesOutputDirectory = text6,
				ErrorLogOptions = errorLogOptions,
				AppConfigPath = text,
				SourceFiles = list6.AsImmutable<CommandLineSourceFile>(),
				Encoding = encoding,
				ChecksumAlgorithm = sourceHashAlgorithm,
				MetadataReferences = list9.AsImmutable<CommandLineReference>(),
				AnalyzerReferences = list10.AsImmutable<CommandLineAnalyzerReference>(),
				AnalyzerConfigPaths = instance.ToImmutableAndFree(),
				AdditionalFiles = list7.AsImmutable<CommandLineSourceFile>(),
				ReferencePaths = immutableArray2,
				SourcePaths = list12.AsImmutable<string>(),
				KeyFileSearchPaths = list13.AsImmutable<string>(),
				Win32ResourceFile = text10,
				Win32Icon = text11,
				Win32Manifest = text9,
				NoWin32Manifest = flag16,
				DisplayLogo = flag,
				DisplayHelp = flag2,
				DisplayVersion = flag3,
				DisplayLangVersions = flag4,
				ManifestResources = list5.AsImmutable<ResourceDescription>(),
				CompilationOptions = csharpCompilationOptions,
				ParseOptions = csharpParseOptions,
				EmitOptions = emitOptions,
				ScriptArguments = list3.AsImmutableOrEmpty<string>(),
				TouchedFilesPath = text17,
				PrintFullPaths = flag22,
				ShouldIncludeErrorEndLocation = flag23,
				PreferredUILang = cultureInfo,
				ReportAnalyzer = flag24,
				SkipAnalyzers = flag25,
				EmbeddedFiles = list8.AsImmutable<CommandLineSourceFile>()
			};
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000682AC File Offset: 0x000664AC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static void ParseAndResolveReferencePaths(string switchName, string switchValue, string baseDirectory, [global::System.Runtime.CompilerServices.Nullable(1)] List<string> builder, MessageID origin, [global::System.Runtime.CompilerServices.Nullable(1)] List<Diagnostic> diagnostics)
		{
			if (string.IsNullOrEmpty(switchValue))
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsString, new object[]
				{
					MessageID.IDS_PathList.Localize(),
					switchName
				});
				return;
			}
			foreach (string text in CommandLineParser.ParseSeparatedPaths(switchValue))
			{
				string text2 = FileUtilities.ResolveRelativePath(text, baseDirectory);
				if (text2 == null)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.WRN_InvalidSearchPathDir, new object[]
					{
						text,
						origin.Localize(),
						MessageID.IDS_DirectoryHasInvalidPath.Localize()
					});
				}
				else if (!Directory.Exists(text2))
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.WRN_InvalidSearchPathDir, new object[]
					{
						text,
						origin.Localize(),
						MessageID.IDS_DirectoryDoesNotExist.Localize()
					});
				}
				else
				{
					builder.Add(text2);
				}
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000683B8 File Offset: 0x000665B8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static string GetWin32Setting(string arg, [global::System.Runtime.CompilerServices.Nullable(2)] string value, List<Diagnostic> diagnostics)
		{
			if (value == null)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, new object[] { arg });
			}
			else
			{
				string text = CommandLineParser.RemoveQuotesAndSlashes(value);
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text;
				}
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, new object[] { arg });
			}
			return null;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00068408 File Offset: 0x00066608
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private void GetCompilationAndModuleNames([global::System.Runtime.CompilerServices.Nullable(1)] List<Diagnostic> diagnostics, OutputKind outputKind, [global::System.Runtime.CompilerServices.Nullable(1)] List<CommandLineSourceFile> sourceFiles, bool sourceFilesSpecified, string moduleAssemblyName, ref string outputFileName, ref string moduleName, out string compilationName)
		{
			string text;
			if (outputFileName == null)
			{
				if (!this.IsScriptCommandLineParser && !sourceFilesSpecified)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_OutputNeedsName);
					text = null;
				}
				else if (outputKind.IsApplication())
				{
					text = null;
				}
				else
				{
					text = PathUtilities.RemoveExtension(PathUtilities.GetFileName(Enumerable.FirstOrDefault<CommandLineSourceFile>(sourceFiles).Path, true));
					outputFileName = text + outputKind.GetDefaultExtension();
					if (text.Length == 0 && !outputKind.IsNetModule())
					{
						CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidInputFileName, new object[] { outputFileName });
						outputFileName = (text = null);
					}
				}
			}
			else
			{
				text = PathUtilities.RemoveExtension(outputFileName);
				if (text.Length == 0)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidInputFileName, new object[] { outputFileName });
					outputFileName = (text = null);
				}
			}
			if (outputKind.IsNetModule())
			{
				compilationName = moduleAssemblyName;
			}
			else
			{
				if (moduleAssemblyName != null)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_AssemblyNameOnNonModule);
				}
				compilationName = text;
			}
			if (moduleName == null)
			{
				moduleName = outputFileName;
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00068500 File Offset: 0x00066700
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> BuildSearchPaths([global::System.Runtime.CompilerServices.Nullable(2)] string sdkDirectoryOpt, List<string> libPaths, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] List<string> responsePathsOpt)
		{
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			if (sdkDirectoryOpt != null)
			{
				instance.Add(sdkDirectoryOpt);
			}
			instance.AddRange(libPaths);
			if (responsePathsOpt != null)
			{
				instance.AddRange(responsePathsOpt);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00068534 File Offset: 0x00066734
		public static IEnumerable<string> ParseConditionalCompilationSymbols(string value, out IEnumerable<Diagnostic> diagnostics)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			value = value.TrimEnd(null);
			if (!value.IsEmpty() && (value.Last() == ';' || value.Last() == ','))
			{
				value = value.Substring(0, value.Length - 1);
			}
			string[] array = value.Split(new char[] { ';', ',' });
			ArrayBuilder<string> arrayBuilder = new ArrayBuilder<string>(array.Length);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i].Trim();
				if (SyntaxFacts.IsValidIdentifier(text))
				{
					arrayBuilder.Add(text);
				}
				else
				{
					instance.Add(Diagnostic.Create(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, 2029, new object[] { text }));
				}
			}
			diagnostics = instance.ToReadOnlyAndFree();
			return Enumerable.AsEnumerable<string>(arrayBuilder);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000685FC File Offset: 0x000667FC
		private static Platform ParsePlatform(string value, IList<Diagnostic> diagnostics)
		{
			string text = value.ToLowerInvariant();
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2212716469U)
			{
				if (num != 946808757U)
				{
					if (num != 1554085904U)
					{
						if (num == 2212716469U)
						{
							if (text == "x64")
							{
								return Platform.X64;
							}
						}
					}
					else if (text == "itanium")
					{
						return Platform.Itanium;
					}
				}
				else if (text == "arm")
				{
					return Platform.Arm;
				}
			}
			else if (num <= 2786156940U)
			{
				if (num != 2449868801U)
				{
					if (num == 2786156940U)
					{
						if (text == "anycpu32bitpreferred")
						{
							return Platform.AnyCpu32BitPreferred;
						}
					}
				}
				else if (text == "x86")
				{
					return Platform.X86;
				}
			}
			else if (num != 3010551159U)
			{
				if (num == 4240884737U)
				{
					if (text == "anycpu")
					{
						return Platform.AnyCpu;
					}
				}
			}
			else if (text == "arm64")
			{
				return Platform.Arm64;
			}
			CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_BadPlatformType, new object[] { value });
			return Platform.AnyCpu;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000686FC File Offset: 0x000668FC
		private static OutputKind ParseTarget(string value, IList<Diagnostic> diagnostics)
		{
			string text = value.ToLowerInvariant();
			if (text == "exe")
			{
				return OutputKind.ConsoleApplication;
			}
			if (text == "winexe")
			{
				return OutputKind.WindowsApplication;
			}
			if (text == "library")
			{
				return OutputKind.DynamicallyLinkedLibrary;
			}
			if (text == "module")
			{
				return OutputKind.NetModule;
			}
			if (text == "appcontainerexe")
			{
				return OutputKind.WindowsRuntimeApplication;
			}
			if (!(text == "winmdobj"))
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidTarget);
				return OutputKind.ConsoleApplication;
			}
			return OutputKind.WindowsRuntimeMetadata;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00068778 File Offset: 0x00066978
		private static IEnumerable<string> ParseUsings(string arg, [global::System.Runtime.CompilerServices.Nullable(2)] string value, IList<Diagnostic> diagnostics)
		{
			if (RoslynString.IsNullOrEmpty(value))
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsString, new object[]
				{
					MessageID.IDS_Namespace1.Localize(),
					arg
				});
				yield break;
			}
			foreach (string text in value.Split(new char[] { ';' }, 1))
			{
				yield return text;
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00068796 File Offset: 0x00066996
		private static IEnumerable<CommandLineAnalyzerReference> ParseAnalyzers(string arg, [global::System.Runtime.CompilerServices.Nullable(2)] string value, List<Diagnostic> diagnostics)
		{
			if (value == null)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsString, new object[]
				{
					MessageID.IDS_Text.Localize(),
					arg
				});
				yield break;
			}
			if (value.Length == 0)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, new object[] { arg });
				yield break;
			}
			List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(CommandLineParser.ParseSeparatedPaths(value), (string path) => !string.IsNullOrWhiteSpace(path)));
			foreach (string text in list)
			{
				yield return new CommandLineAnalyzerReference(text);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000687B4 File Offset: 0x000669B4
		private static IEnumerable<CommandLineReference> ParseAssemblyReferences(string arg, [global::System.Runtime.CompilerServices.Nullable(2)] string value, IList<Diagnostic> diagnostics, bool embedInteropTypes)
		{
			if (value == null)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_SwitchNeedsString, new object[]
				{
					MessageID.IDS_Text.Localize(),
					arg
				});
				yield break;
			}
			if (value.Length == 0)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, new object[] { arg });
				yield break;
			}
			int num = value.IndexOfAny(CSharpCommandLineParser.s_quoteOrEquals);
			string alias;
			if (num >= 0 && value.get_Chars(num) == '=')
			{
				alias = value.Substring(0, num);
				value = value.Substring(num + 1);
				if (!SyntaxFacts.IsValidIdentifier(alias))
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_BadExternIdentifier, new object[] { alias });
					yield break;
				}
			}
			else
			{
				alias = null;
			}
			List<string> list = Enumerable.ToList<string>(Enumerable.Where<string>(CommandLineParser.ParseSeparatedPaths(value), (string path) => !string.IsNullOrWhiteSpace(path)));
			if (alias != null)
			{
				if (list.Count > 1)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_OneAliasPerReference, new object[] { value });
					yield break;
				}
				if (list.Count == 0)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_AliasMissingFile, new object[] { alias });
					yield break;
				}
			}
			foreach (string text in list)
			{
				ImmutableArray<string> immutableArray = ((alias != null) ? ImmutableArray.Create<string>(alias) : ImmutableArray<string>.Empty);
				MetadataReferenceProperties metadataReferenceProperties = new MetadataReferenceProperties(MetadataImageKind.Assembly, immutableArray, embedInteropTypes);
				yield return new CommandLineReference(text, metadataReferenceProperties);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000687D9 File Offset: 0x000669D9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static void ValidateWin32Settings(string win32ResourceFile, string win32IconResourceFile, string win32ManifestFile, OutputKind outputKind, [global::System.Runtime.CompilerServices.Nullable(1)] IList<Diagnostic> diagnostics)
		{
			if (win32ResourceFile != null)
			{
				if (win32IconResourceFile != null)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_CantHaveWin32ResAndIcon);
				}
				if (win32ManifestFile != null)
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_CantHaveWin32ResAndManifest);
				}
			}
			if (outputKind.IsNetModule() && win32ManifestFile != null)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.WRN_CantHaveManifestForModule);
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00068813 File Offset: 0x00066A13
		private static IEnumerable<InstrumentationKind> ParseInstrumentationKinds(string value, IList<Diagnostic> diagnostics)
		{
			string[] array = value.Split(new char[] { ',' }, 1);
			foreach (string text in array)
			{
				if (text.ToLower() == "testcoverage")
				{
					yield return InstrumentationKind.TestCoverage;
				}
				else
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_InvalidInstrumentationKind, new object[] { text });
				}
			}
			string[] array2 = null;
			yield break;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0006882C File Offset: 0x00066A2C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static ResourceDescription ParseResourceDescription(string arg, string resourceDescriptor, [global::System.Runtime.CompilerServices.Nullable(2)] string baseDirectory, IList<Diagnostic> diagnostics, bool embedded)
		{
			string fullPath;
			string text;
			string text2;
			string text3;
			string text4;
			CommandLineParser.ParseResourceDescription(resourceDescriptor, baseDirectory, false, out text, out fullPath, out text2, out text3, out text4);
			bool flag;
			if (text4 == null)
			{
				flag = true;
			}
			else if (string.Equals(text4, "public", 5))
			{
				flag = true;
			}
			else
			{
				if (!string.Equals(text4, "private", 5))
				{
					CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_BadResourceVis, new object[] { text4 });
					return null;
				}
				flag = false;
			}
			if (RoslynString.IsNullOrWhiteSpace(text))
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.ERR_NoFileSpec, new object[] { arg });
				return null;
			}
			if (!PathUtilities.IsValidFilePath(fullPath))
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.FTL_InvalidInputFileName, new object[] { text });
				return null;
			}
			Func<Stream> func = () => new FileStream(fullPath, 3, 1, 3);
			return new ResourceDescription(text3, text2, func, flag, embedded, false);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000688FD File Offset: 0x00066AFD
		private static IEnumerable<string> ParseWarnings(string value)
		{
			value = value.Unquote();
			string[] array = value.Split(new char[] { ',', ';', ' ' }, 1);
			foreach (string text in array)
			{
				ushort num;
				if (string.Equals(text, "nullable", 5))
				{
					foreach (string text2 in ErrorFacts.NullableWarnings)
					{
						yield return text2;
					}
					ImmutableHashSet<string>.Enumerator enumerator = default(ImmutableHashSet<string>.Enumerator);
					yield return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance.GetIdForErrorCode(8632);
					yield return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance.GetIdForErrorCode(8669);
				}
				else if (ushort.TryParse(text, 7, CultureInfo.InvariantCulture, ref num) && ErrorFacts.IsWarning((ErrorCode)num))
				{
					yield return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance.GetIdForErrorCode((int)num);
				}
				else
				{
					yield return text;
				}
			}
			string[] array2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00068910 File Offset: 0x00066B10
		private static void AddWarnings(Dictionary<string, ReportDiagnostic> d, ReportDiagnostic kind, IEnumerable<string> items)
		{
			foreach (string text in items)
			{
				ReportDiagnostic reportDiagnostic;
				if (d.TryGetValue(text, ref reportDiagnostic))
				{
					if (reportDiagnostic != ReportDiagnostic.Suppress)
					{
						d[text] = kind;
					}
				}
				else
				{
					d.Add(text, kind);
				}
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00068974 File Offset: 0x00066B74
		private static void UnimplementedSwitch(IList<Diagnostic> diagnostics, string switchName)
		{
			CSharpCommandLineParser.AddDiagnostic(diagnostics, ErrorCode.WRN_UnimplementedCommandLineSwitch, new object[] { "/" + switchName });
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00068995 File Offset: 0x00066B95
		internal override void GenerateErrorForNoFilesFoundInRecurse(string path, IList<Diagnostic> diagnostics)
		{
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00068997 File Offset: 0x00066B97
		private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode)
		{
			diagnostics.Add(Diagnostic.Create(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, (int)errorCode));
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000689AA File Offset: 0x00066BAA
		private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode, params object[] arguments)
		{
			diagnostics.Add(Diagnostic.Create(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, (int)errorCode, arguments));
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000689C0 File Offset: 0x00066BC0
		private static void AddDiagnostic(IList<Diagnostic> diagnostics, Dictionary<string, ReportDiagnostic> warningOptions, ErrorCode errorCode, params object[] arguments)
		{
			ReportDiagnostic reportDiagnostic;
			warningOptions.TryGetValue(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance.GetIdForErrorCode((int)errorCode), ref reportDiagnostic);
			if (reportDiagnostic != ReportDiagnostic.Suppress)
			{
				CSharpCommandLineParser.AddDiagnostic(diagnostics, errorCode, arguments);
			}
		}

		// Token: 0x040005F0 RID: 1520
		private static readonly char[] s_quoteOrEquals = new char[] { '"', '=' };
	}
}
