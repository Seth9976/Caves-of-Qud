using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000128 RID: 296
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class CommandLineArguments
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0001EA12 File Offset: 0x0001CC12
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0001EA1A File Offset: 0x0001CC1A
		internal bool IsScriptRunner { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0001EA23 File Offset: 0x0001CC23
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0001EA2B File Offset: 0x0001CC2B
		public bool InteractiveMode { get; internal set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001EA34 File Offset: 0x0001CC34
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public string BaseDirectory { get; internal set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0001EA45 File Offset: 0x0001CC45
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0001EA4D File Offset: 0x0001CC4D
		[Nullable(new byte[] { 0, 0, 1, 1 })]
		public ImmutableArray<KeyValuePair<string, string>> PathMap
		{
			[return: Nullable(new byte[] { 0, 0, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 0, 1, 1 })]
			internal set;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001EA56 File Offset: 0x0001CC56
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0001EA5E File Offset: 0x0001CC5E
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ReferencePaths
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001EA67 File Offset: 0x0001CC67
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0001EA6F File Offset: 0x0001CC6F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> SourcePaths
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001EA78 File Offset: 0x0001CC78
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0001EA80 File Offset: 0x0001CC80
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> KeyFileSearchPaths
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001EA89 File Offset: 0x0001CC89
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0001EA91 File Offset: 0x0001CC91
		public bool Utf8Output { get; internal set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001EA9A File Offset: 0x0001CC9A
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0001EAA2 File Offset: 0x0001CCA2
		public string CompilationName { get; internal set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001EAAB File Offset: 0x0001CCAB
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0001EAB3 File Offset: 0x0001CCB3
		[Nullable(1)]
		public EmitOptions EmitOptions
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			internal set;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001EABC File Offset: 0x0001CCBC
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		public string OutputFileName { get; internal set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001EACD File Offset: 0x0001CCCD
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0001EAD5 File Offset: 0x0001CCD5
		public string OutputRefFilePath { get; internal set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001EADE File Offset: 0x0001CCDE
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0001EAE6 File Offset: 0x0001CCE6
		public string PdbPath { get; internal set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001EAEF File Offset: 0x0001CCEF
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001EAF7 File Offset: 0x0001CCF7
		public string SourceLink { get; internal set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001EB00 File Offset: 0x0001CD00
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0001EB08 File Offset: 0x0001CD08
		public string RuleSetPath { get; internal set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001EB11 File Offset: 0x0001CD11
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0001EB19 File Offset: 0x0001CD19
		public bool EmitPdb { get; internal set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001EB22 File Offset: 0x0001CD22
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0001EB2A File Offset: 0x0001CD2A
		[Nullable(1)]
		public string OutputDirectory
		{
			[NullableContext(1)]
			get;
			[NullableContext(1)]
			internal set;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001EB33 File Offset: 0x0001CD33
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0001EB3B File Offset: 0x0001CD3B
		public string DocumentationPath { get; internal set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0001EB44 File Offset: 0x0001CD44
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		public string GeneratedFilesOutputDirectory { get; internal set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001EB55 File Offset: 0x0001CD55
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x0001EB5D File Offset: 0x0001CD5D
		public ErrorLogOptions ErrorLogOptions { get; internal set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001EB66 File Offset: 0x0001CD66
		public string ErrorLogPath
		{
			get
			{
				ErrorLogOptions errorLogOptions = this.ErrorLogOptions;
				if (errorLogOptions == null)
				{
					return null;
				}
				return errorLogOptions.Path;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001EB79 File Offset: 0x0001CD79
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0001EB81 File Offset: 0x0001CD81
		public string AppConfigPath { get; internal set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001EB8A File Offset: 0x0001CD8A
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0001EB92 File Offset: 0x0001CD92
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<Diagnostic> Errors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001EB9B File Offset: 0x0001CD9B
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0001EBA3 File Offset: 0x0001CDA3
		[Nullable(0)]
		public ImmutableArray<CommandLineReference> MetadataReferences
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			internal set;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		[Nullable(0)]
		public ImmutableArray<CommandLineAnalyzerReference> AnalyzerReferences
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			internal set;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001EBBD File Offset: 0x0001CDBD
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0001EBC5 File Offset: 0x0001CDC5
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> AnalyzerConfigPaths
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001EBCE File Offset: 0x0001CDCE
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0001EBD6 File Offset: 0x0001CDD6
		[Nullable(0)]
		public ImmutableArray<CommandLineSourceFile> AdditionalFiles
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			internal set;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001EBDF File Offset: 0x0001CDDF
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0001EBE7 File Offset: 0x0001CDE7
		[Nullable(0)]
		public ImmutableArray<CommandLineSourceFile> EmbeddedFiles
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			internal set;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		public bool ReportAnalyzer { get; internal set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001EC01 File Offset: 0x0001CE01
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0001EC09 File Offset: 0x0001CE09
		public bool SkipAnalyzers { get; internal set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001EC12 File Offset: 0x0001CE12
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001EC1A File Offset: 0x0001CE1A
		public bool DisplayLogo { get; internal set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0001EC23 File Offset: 0x0001CE23
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0001EC2B File Offset: 0x0001CE2B
		public bool DisplayHelp { get; internal set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0001EC34 File Offset: 0x0001CE34
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0001EC3C File Offset: 0x0001CE3C
		public bool DisplayVersion { get; internal set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0001EC45 File Offset: 0x0001CE45
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0001EC4D File Offset: 0x0001CE4D
		public bool DisplayLangVersions { get; internal set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001EC56 File Offset: 0x0001CE56
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0001EC5E File Offset: 0x0001CE5E
		public string Win32ResourceFile { get; internal set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001EC67 File Offset: 0x0001CE67
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0001EC6F File Offset: 0x0001CE6F
		public string Win32Icon { get; internal set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0001EC78 File Offset: 0x0001CE78
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0001EC80 File Offset: 0x0001CE80
		public string Win32Manifest { get; internal set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001EC89 File Offset: 0x0001CE89
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0001EC91 File Offset: 0x0001CE91
		public bool NoWin32Manifest { get; internal set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001EC9A File Offset: 0x0001CE9A
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0001ECA2 File Offset: 0x0001CEA2
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ResourceDescription> ManifestResources
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001ECAB File Offset: 0x0001CEAB
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0001ECB3 File Offset: 0x0001CEB3
		public Encoding Encoding { get; internal set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001ECBC File Offset: 0x0001CEBC
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		public SourceHashAlgorithm ChecksumAlgorithm { get; internal set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001ECCD File Offset: 0x0001CECD
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0001ECD5 File Offset: 0x0001CED5
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ScriptArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
			[param: Nullable(new byte[] { 0, 1 })]
			internal set;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0001ECDE File Offset: 0x0001CEDE
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0001ECE6 File Offset: 0x0001CEE6
		[Nullable(0)]
		public ImmutableArray<CommandLineSourceFile> SourceFiles
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			internal set;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001ECEF File Offset: 0x0001CEEF
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x0001ECF7 File Offset: 0x0001CEF7
		public string TouchedFilesPath { get; internal set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001ED00 File Offset: 0x0001CF00
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0001ED08 File Offset: 0x0001CF08
		public bool PrintFullPaths { get; internal set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001ED11 File Offset: 0x0001CF11
		[Nullable(1)]
		public ParseOptions ParseOptions
		{
			[NullableContext(1)]
			get
			{
				return this.ParseOptionsCore;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0001ED19 File Offset: 0x0001CF19
		[Nullable(1)]
		public CompilationOptions CompilationOptions
		{
			[NullableContext(1)]
			get
			{
				return this.CompilationOptionsCore;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000ABF RID: 2751
		[Nullable(1)]
		protected abstract ParseOptions ParseOptionsCore
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000AC0 RID: 2752
		[Nullable(1)]
		protected abstract CompilationOptions CompilationOptionsCore
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001ED21 File Offset: 0x0001CF21
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001ED29 File Offset: 0x0001CF29
		public CultureInfo PreferredUILang { get; internal set; }

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001ED32 File Offset: 0x0001CF32
		[NullableContext(1)]
		internal StrongNameProvider GetStrongNameProvider(StrongNameFileSystem fileSystem)
		{
			return new DesktopStrongNameProvider(this.KeyFileSearchPaths, fileSystem);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001ED40 File Offset: 0x0001CF40
		internal CommandLineArguments()
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001ED48 File Offset: 0x0001CF48
		[NullableContext(1)]
		public string GetOutputFilePath(string outputFileName)
		{
			if (outputFileName == null)
			{
				throw new ArgumentNullException("outputFileName");
			}
			return Path.Combine(this.OutputDirectory, outputFileName);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001ED64 File Offset: 0x0001CF64
		[NullableContext(1)]
		public string GetPdbFilePath(string outputFileName)
		{
			if (outputFileName == null)
			{
				throw new ArgumentNullException("outputFileName");
			}
			return this.PdbPath ?? Path.Combine(this.OutputDirectory, Path.ChangeExtension(outputFileName, ".pdb"));
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001ED94 File Offset: 0x0001CF94
		public bool EmitPdbFile
		{
			get
			{
				return this.EmitPdb && this.EmitOptions.DebugInformationFormat != DebugInformationFormat.Embedded;
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
		[NullableContext(1)]
		public IEnumerable<MetadataReference> ResolveMetadataReferences(MetadataReferenceResolver metadataResolver)
		{
			if (metadataResolver == null)
			{
				throw new ArgumentNullException("metadataResolver");
			}
			return this.ResolveMetadataReferences(metadataResolver, null, null);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001EDCC File Offset: 0x0001CFCC
		[NullableContext(1)]
		internal IEnumerable<MetadataReference> ResolveMetadataReferences(MetadataReferenceResolver metadataResolver, [Nullable(new byte[] { 2, 1 })] List<DiagnosticInfo> diagnosticsOpt, [Nullable(2)] CommonMessageProvider messageProviderOpt)
		{
			List<MetadataReference> list = new List<MetadataReference>();
			this.ResolveMetadataReferences(metadataResolver, diagnosticsOpt, messageProviderOpt, list);
			return list;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		[NullableContext(1)]
		internal virtual bool ResolveMetadataReferences(MetadataReferenceResolver metadataResolver, [Nullable(new byte[] { 2, 1 })] List<DiagnosticInfo> diagnosticsOpt, [Nullable(2)] CommonMessageProvider messageProviderOpt, List<MetadataReference> resolved)
		{
			bool flag = true;
			foreach (CommandLineReference commandLineReference in this.MetadataReferences)
			{
				ImmutableArray<PortableExecutableReference> immutableArray = CommandLineArguments.ResolveMetadataReference(commandLineReference, metadataResolver, diagnosticsOpt, messageProviderOpt);
				if (!immutableArray.IsDefaultOrEmpty)
				{
					resolved.AddRange(immutableArray);
				}
				else
				{
					flag = false;
					if (diagnosticsOpt == null)
					{
						resolved.Add(new UnresolvedMetadataReference(commandLineReference.Reference, commandLineReference.Properties));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001EE64 File Offset: 0x0001D064
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<PortableExecutableReference> ResolveMetadataReference(CommandLineReference cmdReference, MetadataReferenceResolver metadataResolver, [Nullable(new byte[] { 2, 1 })] List<DiagnosticInfo> diagnosticsOpt, [Nullable(2)] CommonMessageProvider messageProviderOpt)
		{
			ImmutableArray<PortableExecutableReference> immutableArray;
			try
			{
				immutableArray = metadataResolver.ResolveReference(cmdReference.Reference, null, cmdReference.Properties);
			}
			catch (Exception ex) when (diagnosticsOpt != null && (ex is BadImageFormatException || ex is IOException))
			{
				Diagnostic diagnostic = PortableExecutableReference.ExceptionToDiagnostic(ex, messageProviderOpt, Location.None, cmdReference.Reference, cmdReference.Properties.Kind);
				diagnosticsOpt.Add(((DiagnosticWithInfo)diagnostic).Info);
				return ImmutableArray<PortableExecutableReference>.Empty;
			}
			if (immutableArray.IsDefaultOrEmpty && diagnosticsOpt != null)
			{
				diagnosticsOpt.Add(new DiagnosticInfo(messageProviderOpt, messageProviderOpt.ERR_MetadataFileNotFound, new object[] { cmdReference.Reference }));
				return ImmutableArray<PortableExecutableReference>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001EF3C File Offset: 0x0001D13C
		[NullableContext(1)]
		public IEnumerable<AnalyzerReference> ResolveAnalyzerReferences(IAnalyzerAssemblyLoader analyzerLoader)
		{
			foreach (CommandLineAnalyzerReference commandLineAnalyzerReference in this.AnalyzerReferences)
			{
				yield return this.ResolveAnalyzerReference(commandLineAnalyzerReference, analyzerLoader) ?? new UnresolvedAnalyzerReference(commandLineAnalyzerReference.FilePath);
			}
			ImmutableArray<CommandLineAnalyzerReference>.Enumerator enumerator = default(ImmutableArray<CommandLineAnalyzerReference>.Enumerator);
			yield break;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001EF54 File Offset: 0x0001D154
		[NullableContext(1)]
		internal void ResolveAnalyzersFromArguments(string language, List<DiagnosticInfo> diagnostics, CommonMessageProvider messageProvider, IAnalyzerAssemblyLoader analyzerLoader, bool skipAnalyzers, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<ISourceGenerator> generators)
		{
			ImmutableArray<DiagnosticAnalyzer>.Builder builder = ImmutableArray.CreateBuilder<DiagnosticAnalyzer>();
			ImmutableArray<ISourceGenerator>.Builder builder2 = ImmutableArray.CreateBuilder<ISourceGenerator>();
			EventHandler<AnalyzerLoadFailureEventArgs> eventHandler = delegate(object o, [Nullable(1)] AnalyzerLoadFailureEventArgs e)
			{
				AnalyzerFileReference analyzerFileReference3 = o as AnalyzerFileReference;
				DiagnosticInfo diagnosticInfo;
				switch (e.ErrorCode)
				{
				case AnalyzerLoadFailureEventArgs.FailureErrorCode.None:
					return;
				case AnalyzerLoadFailureEventArgs.FailureErrorCode.UnableToLoadAnalyzer:
					diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.WRN_UnableToLoadAnalyzer, new object[] { analyzerFileReference3.FullPath, e.Message });
					break;
				case AnalyzerLoadFailureEventArgs.FailureErrorCode.UnableToCreateAnalyzer:
					diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.WRN_AnalyzerCannotBeCreated, new object[]
					{
						e.TypeName ?? "",
						analyzerFileReference3.FullPath,
						e.Message
					});
					break;
				case AnalyzerLoadFailureEventArgs.FailureErrorCode.NoAnalyzers:
					diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.WRN_NoAnalyzerInAssembly, new object[] { analyzerFileReference3.FullPath });
					break;
				case AnalyzerLoadFailureEventArgs.FailureErrorCode.ReferencesFramework:
					diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.WRN_AnalyzerReferencesFramework, new object[] { analyzerFileReference3.FullPath, e.TypeName });
					break;
				default:
					return;
				}
				diagnosticInfo = messageProvider.FilterDiagnosticInfo(diagnosticInfo, this.CompilationOptions);
				if (diagnosticInfo != null)
				{
					diagnostics.Add(diagnosticInfo);
				}
			};
			ArrayBuilder<AnalyzerFileReference> instance = ArrayBuilder<AnalyzerFileReference>.GetInstance();
			foreach (CommandLineAnalyzerReference commandLineAnalyzerReference in this.AnalyzerReferences)
			{
				AnalyzerFileReference analyzerFileReference = this.ResolveAnalyzerReference(commandLineAnalyzerReference, analyzerLoader);
				if (analyzerFileReference != null)
				{
					instance.Add(analyzerFileReference);
					analyzerLoader.AddDependencyLocation(analyzerFileReference.FullPath);
				}
				else
				{
					diagnostics.Add(new DiagnosticInfo(messageProvider, messageProvider.ERR_MetadataFileNotFound, new object[] { commandLineAnalyzerReference.FilePath }));
				}
			}
			foreach (AnalyzerFileReference analyzerFileReference2 in instance)
			{
				analyzerFileReference2.AnalyzerLoadFailed += eventHandler;
				if (!skipAnalyzers)
				{
					analyzerFileReference2.AddAnalyzers(builder, language);
				}
				analyzerFileReference2.AddGenerators(builder2, language);
				analyzerFileReference2.AnalyzerLoadFailed -= eventHandler;
			}
			instance.Free();
			generators = builder2.ToImmutable();
			analyzers = builder.ToImmutable();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001F080 File Offset: 0x0001D280
		[NullableContext(1)]
		[return: Nullable(2)]
		private AnalyzerFileReference ResolveAnalyzerReference(CommandLineAnalyzerReference reference, IAnalyzerAssemblyLoader analyzerLoader)
		{
			string text = FileUtilities.ResolveRelativePath(reference.FilePath, null, this.BaseDirectory, this.ReferencePaths, new Func<string, bool>(File.Exists));
			if (text != null)
			{
				text = FileUtilities.TryNormalizeAbsolutePath(text);
			}
			if (text != null)
			{
				return new AnalyzerFileReference(text, analyzerLoader);
			}
			return null;
		}
	}
}
