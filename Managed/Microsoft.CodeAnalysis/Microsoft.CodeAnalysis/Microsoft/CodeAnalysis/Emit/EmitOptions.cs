using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200048F RID: 1167
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class EmitOptions : IEquatable<EmitOptions>
	{
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x00082428 File Offset: 0x00080628
		// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x00082430 File Offset: 0x00080630
		public bool EmitMetadataOnly { get; private set; }

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x00082439 File Offset: 0x00080639
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x00082441 File Offset: 0x00080641
		public bool TolerateErrors { get; private set; }

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x0008244A File Offset: 0x0008064A
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x00082452 File Offset: 0x00080652
		public bool IncludePrivateMembers { get; private set; }

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x0008245B File Offset: 0x0008065B
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x00082463 File Offset: 0x00080663
		[Nullable(0)]
		public ImmutableArray<InstrumentationKind> InstrumentationKinds
		{
			[NullableContext(0)]
			get;
			[NullableContext(0)]
			private set;
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x0008246C File Offset: 0x0008066C
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x00082474 File Offset: 0x00080674
		public SubsystemVersion SubsystemVersion { get; private set; }

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x0008247D File Offset: 0x0008067D
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x00082485 File Offset: 0x00080685
		public int FileAlignment { get; private set; }

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x0008248E File Offset: 0x0008068E
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x00082496 File Offset: 0x00080696
		public bool HighEntropyVirtualAddressSpace { get; private set; }

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x0008249F File Offset: 0x0008069F
		// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x000824A7 File Offset: 0x000806A7
		public ulong BaseAddress { get; private set; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x000824B0 File Offset: 0x000806B0
		// (set) Token: 0x06002DE8 RID: 11752 RVA: 0x000824B8 File Offset: 0x000806B8
		public DebugInformationFormat DebugInformationFormat { get; private set; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000824C1 File Offset: 0x000806C1
		// (set) Token: 0x06002DEA RID: 11754 RVA: 0x000824C9 File Offset: 0x000806C9
		public string OutputNameOverride { get; private set; }

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000824D2 File Offset: 0x000806D2
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000824DA File Offset: 0x000806DA
		public string PdbFilePath { get; private set; }

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000824E3 File Offset: 0x000806E3
		// (set) Token: 0x06002DEE RID: 11758 RVA: 0x000824EB File Offset: 0x000806EB
		public HashAlgorithmName PdbChecksumAlgorithm { get; private set; }

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000824F4 File Offset: 0x000806F4
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000824FC File Offset: 0x000806FC
		public string RuntimeMetadataVersion { get; private set; }

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x00082505 File Offset: 0x00080705
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x0008250D File Offset: 0x0008070D
		public Encoding DefaultSourceFileEncoding { get; private set; }

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x00082516 File Offset: 0x00080716
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x0008251E File Offset: 0x0008071E
		public Encoding FallbackSourceFileEncoding { get; private set; }

		// Token: 0x06002DF5 RID: 11765 RVA: 0x00082528 File Offset: 0x00080728
		[NullableContext(1)]
		public EmitOptions(bool metadataOnly, DebugInformationFormat debugInformationFormat, string pdbFilePath, string outputNameOverride, int fileAlignment, ulong baseAddress, bool highEntropyVirtualAddressSpace, SubsystemVersion subsystemVersion, string runtimeMetadataVersion, bool tolerateErrors, bool includePrivateMembers)
			: this(metadataOnly, debugInformationFormat, pdbFilePath, outputNameOverride, fileAlignment, baseAddress, highEntropyVirtualAddressSpace, subsystemVersion, runtimeMetadataVersion, tolerateErrors, includePrivateMembers, ImmutableArray<InstrumentationKind>.Empty)
		{
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x00082554 File Offset: 0x00080754
		[NullableContext(1)]
		public EmitOptions(bool metadataOnly, DebugInformationFormat debugInformationFormat, string pdbFilePath, string outputNameOverride, int fileAlignment, ulong baseAddress, bool highEntropyVirtualAddressSpace, SubsystemVersion subsystemVersion, string runtimeMetadataVersion, bool tolerateErrors, bool includePrivateMembers, [Nullable(0)] ImmutableArray<InstrumentationKind> instrumentationKinds)
			: this(metadataOnly, debugInformationFormat, pdbFilePath, outputNameOverride, fileAlignment, baseAddress, highEntropyVirtualAddressSpace, subsystemVersion, runtimeMetadataVersion, tolerateErrors, includePrivateMembers, instrumentationKinds, default(HashAlgorithmName?))
		{
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00082588 File Offset: 0x00080788
		public EmitOptions(bool metadataOnly, DebugInformationFormat debugInformationFormat, string pdbFilePath, string outputNameOverride, int fileAlignment, ulong baseAddress, bool highEntropyVirtualAddressSpace, SubsystemVersion subsystemVersion, string runtimeMetadataVersion, bool tolerateErrors, bool includePrivateMembers, [Nullable(0)] ImmutableArray<InstrumentationKind> instrumentationKinds, HashAlgorithmName? pdbChecksumAlgorithm)
			: this(metadataOnly, debugInformationFormat, pdbFilePath, outputNameOverride, fileAlignment, baseAddress, highEntropyVirtualAddressSpace, subsystemVersion, runtimeMetadataVersion, tolerateErrors, includePrivateMembers, instrumentationKinds, pdbChecksumAlgorithm, null, null)
		{
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x000825B4 File Offset: 0x000807B4
		public EmitOptions(bool metadataOnly = false, DebugInformationFormat debugInformationFormat = (DebugInformationFormat)0, string pdbFilePath = null, string outputNameOverride = null, int fileAlignment = 0, ulong baseAddress = 0UL, bool highEntropyVirtualAddressSpace = false, SubsystemVersion subsystemVersion = default(SubsystemVersion), string runtimeMetadataVersion = null, bool tolerateErrors = false, bool includePrivateMembers = true, [Nullable(0)] ImmutableArray<InstrumentationKind> instrumentationKinds = default(ImmutableArray<InstrumentationKind>), HashAlgorithmName? pdbChecksumAlgorithm = null, Encoding defaultSourceFileEncoding = null, Encoding fallbackSourceFileEncoding = null)
		{
			this.EmitMetadataOnly = metadataOnly;
			this.DebugInformationFormat = ((debugInformationFormat == (DebugInformationFormat)0) ? DebugInformationFormat.Pdb : debugInformationFormat);
			this.PdbFilePath = pdbFilePath;
			this.OutputNameOverride = outputNameOverride;
			this.FileAlignment = fileAlignment;
			this.BaseAddress = baseAddress;
			this.HighEntropyVirtualAddressSpace = highEntropyVirtualAddressSpace;
			this.SubsystemVersion = subsystemVersion;
			this.RuntimeMetadataVersion = runtimeMetadataVersion;
			this.TolerateErrors = tolerateErrors;
			this.IncludePrivateMembers = includePrivateMembers;
			this.InstrumentationKinds = instrumentationKinds.NullToEmpty<InstrumentationKind>();
			this.PdbChecksumAlgorithm = pdbChecksumAlgorithm ?? HashAlgorithmName.SHA256;
			this.DefaultSourceFileEncoding = defaultSourceFileEncoding;
			this.FallbackSourceFileEncoding = fallbackSourceFileEncoding;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x00082660 File Offset: 0x00080860
		[NullableContext(1)]
		private EmitOptions(EmitOptions other)
			: this(other.EmitMetadataOnly, other.DebugInformationFormat, other.PdbFilePath, other.OutputNameOverride, other.FileAlignment, other.BaseAddress, other.HighEntropyVirtualAddressSpace, other.SubsystemVersion, other.RuntimeMetadataVersion, other.TolerateErrors, other.IncludePrivateMembers, other.InstrumentationKinds, new HashAlgorithmName?(other.PdbChecksumAlgorithm), other.DefaultSourceFileEncoding, other.FallbackSourceFileEncoding)
		{
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000826D2 File Offset: 0x000808D2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EmitOptions);
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000826E0 File Offset: 0x000808E0
		public bool Equals(EmitOptions other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.EmitMetadataOnly == other.EmitMetadataOnly && this.BaseAddress == other.BaseAddress && this.FileAlignment == other.FileAlignment && this.HighEntropyVirtualAddressSpace == other.HighEntropyVirtualAddressSpace && this.SubsystemVersion.Equals(other.SubsystemVersion) && this.DebugInformationFormat == other.DebugInformationFormat && this.PdbFilePath == other.PdbFilePath && this.PdbChecksumAlgorithm == other.PdbChecksumAlgorithm && this.OutputNameOverride == other.OutputNameOverride && this.RuntimeMetadataVersion == other.RuntimeMetadataVersion && this.TolerateErrors == other.TolerateErrors && this.IncludePrivateMembers == other.IncludePrivateMembers)
			{
				if (this.InstrumentationKinds.NullToEmpty<InstrumentationKind>().SequenceEqual<InstrumentationKind, InstrumentationKind>(other.InstrumentationKinds.NullToEmpty<InstrumentationKind>(), (InstrumentationKind a, InstrumentationKind b) => a == b) && this.DefaultSourceFileEncoding == other.DefaultSourceFileEncoding)
				{
					return this.FallbackSourceFileEncoding == other.FallbackSourceFileEncoding;
				}
			}
			return false;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0008282C File Offset: 0x00080A2C
		public override int GetHashCode()
		{
			return Hash.Combine(this.EmitMetadataOnly, Hash.Combine(this.BaseAddress.GetHashCode(), Hash.Combine(this.FileAlignment, Hash.Combine(this.HighEntropyVirtualAddressSpace, Hash.Combine(this.SubsystemVersion.GetHashCode(), Hash.Combine((int)this.DebugInformationFormat, Hash.Combine<string>(this.PdbFilePath, Hash.Combine(this.PdbChecksumAlgorithm.GetHashCode(), Hash.Combine<string>(this.OutputNameOverride, Hash.Combine<string>(this.RuntimeMetadataVersion, Hash.Combine(this.TolerateErrors, Hash.Combine(this.IncludePrivateMembers, Hash.Combine(Hash.CombineValues<InstrumentationKind>(this.InstrumentationKinds, int.MaxValue), Hash.Combine<Encoding>(this.DefaultSourceFileEncoding, Hash.Combine<Encoding>(this.FallbackSourceFileEncoding, 0)))))))))))))));
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0008290D File Offset: 0x00080B0D
		public static bool operator ==(EmitOptions left, EmitOptions right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x00082916 File Offset: 0x00080B16
		public static bool operator !=(EmitOptions left, EmitOptions right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x00082924 File Offset: 0x00080B24
		[NullableContext(1)]
		internal void ValidateOptions(DiagnosticBag diagnostics, CommonMessageProvider messageProvider, bool isDeterministic)
		{
			if (!this.DebugInformationFormat.IsValid())
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidDebugInformationFormat, Location.None, new object[] { (int)this.DebugInformationFormat }));
			}
			foreach (InstrumentationKind instrumentationKind in this.InstrumentationKinds)
			{
				if (!instrumentationKind.IsValid())
				{
					diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidInstrumentationKind, Location.None, new object[] { (int)instrumentationKind }));
				}
			}
			if (this.OutputNameOverride != null)
			{
				MetadataHelpers.CheckAssemblyOrModuleName(this.OutputNameOverride, messageProvider, messageProvider.ERR_InvalidOutputName, diagnostics);
			}
			if (this.FileAlignment != 0 && !EmitOptions.IsValidFileAlignment(this.FileAlignment))
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidFileAlignment, Location.None, new object[] { this.FileAlignment }));
			}
			if (!this.SubsystemVersion.Equals(SubsystemVersion.None) && !this.SubsystemVersion.IsValid)
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidSubsystemVersion, Location.None, new object[] { this.SubsystemVersion.ToString() }));
			}
			if (this.PdbChecksumAlgorithm.Name != null)
			{
				try
				{
					IncrementalHash.CreateHash(this.PdbChecksumAlgorithm).Dispose();
					return;
				}
				catch
				{
					diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidHashAlgorithmName, Location.None, new object[] { this.PdbChecksumAlgorithm.ToString() }));
					return;
				}
			}
			if (isDeterministic)
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_InvalidHashAlgorithmName, Location.None, new object[] { "" }));
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x00082B00 File Offset: 0x00080D00
		internal bool EmitTestCoverageData
		{
			get
			{
				return this.InstrumentationKinds.Contains(InstrumentationKind.TestCoverage);
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x00082B1C File Offset: 0x00080D1C
		internal static bool IsValidFileAlignment(int value)
		{
			if (value <= 1024)
			{
				if (value != 512 && value != 1024)
				{
					return false;
				}
			}
			else if (value != 2048 && value != 4096 && value != 8192)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x00082B53 File Offset: 0x00080D53
		[NullableContext(1)]
		public EmitOptions WithEmitMetadataOnly(bool value)
		{
			if (this.EmitMetadataOnly == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				EmitMetadataOnly = value
			};
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00082B6D File Offset: 0x00080D6D
		[NullableContext(1)]
		public EmitOptions WithPdbFilePath(string path)
		{
			if (this.PdbFilePath == path)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				PdbFilePath = path
			};
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x00082B8C File Offset: 0x00080D8C
		[NullableContext(1)]
		public EmitOptions WithPdbChecksumAlgorithm(HashAlgorithmName name)
		{
			if (this.PdbChecksumAlgorithm == name)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				PdbChecksumAlgorithm = name
			};
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x00082BAB File Offset: 0x00080DAB
		[NullableContext(1)]
		public EmitOptions WithOutputNameOverride(string outputName)
		{
			if (this.OutputNameOverride == outputName)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				OutputNameOverride = outputName
			};
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00082BCA File Offset: 0x00080DCA
		[NullableContext(1)]
		public EmitOptions WithDebugInformationFormat(DebugInformationFormat format)
		{
			if (this.DebugInformationFormat == format)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				DebugInformationFormat = format
			};
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00082BE4 File Offset: 0x00080DE4
		[NullableContext(1)]
		public EmitOptions WithFileAlignment(int value)
		{
			if (this.FileAlignment == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				FileAlignment = value
			};
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x00082BFE File Offset: 0x00080DFE
		[NullableContext(1)]
		public EmitOptions WithBaseAddress(ulong value)
		{
			if (this.BaseAddress == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				BaseAddress = value
			};
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00082C18 File Offset: 0x00080E18
		[NullableContext(1)]
		public EmitOptions WithHighEntropyVirtualAddressSpace(bool value)
		{
			if (this.HighEntropyVirtualAddressSpace == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				HighEntropyVirtualAddressSpace = value
			};
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00082C32 File Offset: 0x00080E32
		[NullableContext(1)]
		public EmitOptions WithSubsystemVersion(SubsystemVersion subsystemVersion)
		{
			if (subsystemVersion.Equals(this.SubsystemVersion))
			{
				return this;
			}
			return new EmitOptions(this)
			{
				SubsystemVersion = subsystemVersion
			};
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00082C52 File Offset: 0x00080E52
		[NullableContext(1)]
		public EmitOptions WithRuntimeMetadataVersion(string version)
		{
			if (this.RuntimeMetadataVersion == version)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				RuntimeMetadataVersion = version
			};
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00082C71 File Offset: 0x00080E71
		[NullableContext(1)]
		public EmitOptions WithTolerateErrors(bool value)
		{
			if (this.TolerateErrors == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				TolerateErrors = value
			};
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00082C8B File Offset: 0x00080E8B
		[NullableContext(1)]
		public EmitOptions WithIncludePrivateMembers(bool value)
		{
			if (this.IncludePrivateMembers == value)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				IncludePrivateMembers = value
			};
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00082CA5 File Offset: 0x00080EA5
		[NullableContext(0)]
		[return: Nullable(1)]
		public EmitOptions WithInstrumentationKinds(ImmutableArray<InstrumentationKind> instrumentationKinds)
		{
			if (this.InstrumentationKinds == instrumentationKinds)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				InstrumentationKinds = instrumentationKinds
			};
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x00082CC4 File Offset: 0x00080EC4
		[NullableContext(1)]
		public EmitOptions WithDefaultSourceFileEncoding([Nullable(2)] Encoding defaultSourceFileEncoding)
		{
			if (this.DefaultSourceFileEncoding == defaultSourceFileEncoding)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				DefaultSourceFileEncoding = defaultSourceFileEncoding
			};
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00082CDE File Offset: 0x00080EDE
		[NullableContext(1)]
		public EmitOptions WithFallbackSourceFileEncoding([Nullable(2)] Encoding fallbackSourceFileEncoding)
		{
			if (this.FallbackSourceFileEncoding == fallbackSourceFileEncoding)
			{
				return this;
			}
			return new EmitOptions(this)
			{
				FallbackSourceFileEncoding = fallbackSourceFileEncoding
			};
		}

		// Token: 0x0400124A RID: 4682
		[Nullable(1)]
		internal static readonly EmitOptions Default = (PlatformInformation.IsWindows ? new EmitOptions(false, (DebugInformationFormat)0, null, null, 0, 0UL, false, default(SubsystemVersion), null, false, true, default(ImmutableArray<InstrumentationKind>), default(HashAlgorithmName?), null, null) : new EmitOptions(false, (DebugInformationFormat)0, null, null, 0, 0UL, false, default(SubsystemVersion), null, false, true, default(ImmutableArray<InstrumentationKind>), default(HashAlgorithmName?), null, null).WithDebugInformationFormat(DebugInformationFormat.PortablePdb));
	}
}
