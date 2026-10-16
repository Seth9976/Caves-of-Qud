using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B5 RID: 437
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AssemblyMetadata : Metadata
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x00038D50 File Offset: 0x00036F50
		private AssemblyMetadata(AssemblyMetadata other, bool shareCachedSymbols)
			: base(false, other.Id)
		{
			if (shareCachedSymbols)
			{
				this.CachedSymbols = other.CachedSymbols;
			}
			this._lazyData = other._lazyData;
			this._moduleFactoryOpt = other._moduleFactoryOpt;
			this._initialModules = other._initialModules;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00038DA8 File Offset: 0x00036FA8
		internal AssemblyMetadata([Nullable(new byte[] { 0, 1 })] ImmutableArray<ModuleMetadata> modules)
			: base(true, MetadataId.CreateNewId())
		{
			this._initialModules = modules;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00038DC8 File Offset: 0x00036FC8
		internal AssemblyMetadata(ModuleMetadata manifestModule, Func<string, ModuleMetadata> moduleFactory)
			: base(true, MetadataId.CreateNewId())
		{
			this._initialModules = ImmutableArray.Create<ModuleMetadata>(manifestModule);
			this._moduleFactoryOpt = moduleFactory;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00038DF4 File Offset: 0x00036FF4
		[NullableContext(0)]
		[return: Nullable(1)]
		public static AssemblyMetadata CreateFromImage(ImmutableArray<byte> peImage)
		{
			return AssemblyMetadata.Create(ModuleMetadata.CreateFromImage(peImage));
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00038E01 File Offset: 0x00037001
		public static AssemblyMetadata CreateFromImage(IEnumerable<byte> peImage)
		{
			return AssemblyMetadata.Create(ModuleMetadata.CreateFromImage(peImage));
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00038E0E File Offset: 0x0003700E
		public static AssemblyMetadata CreateFromStream(Stream peStream, bool leaveOpen = false)
		{
			return AssemblyMetadata.Create(ModuleMetadata.CreateFromStream(peStream, leaveOpen));
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00038E1C File Offset: 0x0003701C
		public static AssemblyMetadata CreateFromStream(Stream peStream, PEStreamOptions options)
		{
			return AssemblyMetadata.Create(ModuleMetadata.CreateFromStream(peStream, options));
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00038E2A File Offset: 0x0003702A
		public static AssemblyMetadata CreateFromFile(string path)
		{
			return AssemblyMetadata.CreateFromFile(ModuleMetadata.CreateFromFile(path), path);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00038E38 File Offset: 0x00037038
		internal static AssemblyMetadata CreateFromFile(ModuleMetadata manifestModule, string path)
		{
			return new AssemblyMetadata(manifestModule, (string moduleName) => ModuleMetadata.CreateFromFile(Path.Combine(Path.GetDirectoryName(path) ?? "", moduleName)));
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00038E64 File Offset: 0x00037064
		public static AssemblyMetadata Create(ModuleMetadata module)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			return new AssemblyMetadata(ImmutableArray.Create<ModuleMetadata>(module));
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00038E80 File Offset: 0x00037080
		public static AssemblyMetadata Create([Nullable(new byte[] { 0, 1 })] ImmutableArray<ModuleMetadata> modules)
		{
			if (modules.IsDefaultOrEmpty)
			{
				throw new ArgumentException(CodeAnalysisResources.AssemblyMustHaveAtLeastOneModule, "modules");
			}
			for (int i = 0; i < modules.Length; i++)
			{
				if (modules[i] == null)
				{
					throw new ArgumentNullException("modules[" + i.ToString() + "]");
				}
				if (!modules[i].IsImageOwner)
				{
					throw new ArgumentException(CodeAnalysisResources.ModuleCopyCannotBeUsedToCreateAssemblyMetadata, "modules[" + i.ToString() + "]");
				}
			}
			return new AssemblyMetadata(modules);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00038F14 File Offset: 0x00037114
		public static AssemblyMetadata Create(IEnumerable<ModuleMetadata> modules)
		{
			return AssemblyMetadata.Create(modules.AsImmutableOrNull<ModuleMetadata>());
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00038F21 File Offset: 0x00037121
		public static AssemblyMetadata Create(params ModuleMetadata[] modules)
		{
			return AssemblyMetadata.Create(ImmutableArray.CreateRange<ModuleMetadata>(modules));
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00038F2E File Offset: 0x0003712E
		internal new AssemblyMetadata Copy()
		{
			return new AssemblyMetadata(this, true);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00038F37 File Offset: 0x00037137
		internal AssemblyMetadata CopyWithoutSharingCachedSymbols()
		{
			return new AssemblyMetadata(this, false);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00038F40 File Offset: 0x00037140
		protected override Metadata CommonCopy()
		{
			return this.Copy();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00038F48 File Offset: 0x00037148
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ModuleMetadata> GetModules()
		{
			if (this._lazyPublishedModules.IsDefault)
			{
				ImmutableArray<ModuleMetadata> immutableArray = this.GetOrCreateData().Modules;
				if (!this.IsImageOwner)
				{
					immutableArray = immutableArray.SelectAsArray<ModuleMetadata, ModuleMetadata>((ModuleMetadata module) => module.Copy());
				}
				ImmutableInterlocked.InterlockedInitialize<ModuleMetadata>(ref this._lazyPublishedModules, immutableArray);
			}
			if (this._lazyData == AssemblyMetadata.Data.Disposed)
			{
				throw new ObjectDisposedException("AssemblyMetadata");
			}
			return this._lazyPublishedModules;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00038FC7 File Offset: 0x000371C7
		[NullableContext(2)]
		internal PEAssembly GetAssembly()
		{
			return this.GetOrCreateData().Assembly;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00038FD4 File Offset: 0x000371D4
		private AssemblyMetadata.Data GetOrCreateData()
		{
			if (this._lazyData == null)
			{
				ImmutableArray<ModuleMetadata> immutableArray = this._initialModules;
				ImmutableArray<ModuleMetadata>.Builder builder = null;
				bool flag = false;
				try
				{
					if (this._moduleFactoryOpt != null)
					{
						ImmutableArray<string> moduleNames = this._initialModules[0].GetModuleNames();
						if (moduleNames.Length > 0)
						{
							builder = ImmutableArray.CreateBuilder<ModuleMetadata>(1 + moduleNames.Length);
							builder.Add(this._initialModules[0]);
							foreach (string text in moduleNames)
							{
								builder.Add(this._moduleFactoryOpt.Invoke(text));
							}
							immutableArray = builder.ToImmutable();
						}
					}
					PEAssembly peassembly = new PEAssembly(this, immutableArray.SelectAsArray<ModuleMetadata, PEModule>((ModuleMetadata m) => m.Module));
					AssemblyMetadata.Data data = new AssemblyMetadata.Data(immutableArray, peassembly);
					flag = Interlocked.CompareExchange<AssemblyMetadata.Data>(ref this._lazyData, data, null) == null;
				}
				finally
				{
					if (builder != null && !flag)
					{
						for (int i = this._initialModules.Length; i < builder.Count; i++)
						{
							builder[i].Dispose();
						}
					}
				}
			}
			if (this._lazyData.IsDisposed)
			{
				throw new ObjectDisposedException("AssemblyMetadata");
			}
			return this._lazyData;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0003912C File Offset: 0x0003732C
		public override void Dispose()
		{
			AssemblyMetadata.Data data = Interlocked.Exchange<AssemblyMetadata.Data>(ref this._lazyData, AssemblyMetadata.Data.Disposed);
			if (data == AssemblyMetadata.Data.Disposed || !this.IsImageOwner)
			{
				return;
			}
			foreach (ModuleMetadata moduleMetadata in this._initialModules)
			{
				moduleMetadata.Dispose();
			}
			if (data == null)
			{
				return;
			}
			for (int i = this._initialModules.Length; i < data.Modules.Length; i++)
			{
				data.Modules[i].Dispose();
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000391C0 File Offset: 0x000373C0
		internal bool IsValidAssembly()
		{
			ImmutableArray<ModuleMetadata> modules = this.GetModules();
			if (!modules[0].Module.IsManifestModule)
			{
				return false;
			}
			for (int i = 1; i < modules.Length; i++)
			{
				PEModule module = modules[i].Module;
				if (!module.IsLinkedModule && module.MetadataReader.MetadataKind != MetadataKind.WindowsMetadata)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00039223 File Offset: 0x00037423
		public override MetadataImageKind Kind
		{
			get
			{
				return MetadataImageKind.Assembly;
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00039226 File Offset: 0x00037426
		[NullableContext(2)]
		[return: Nullable(1)]
		public PortableExecutableReference GetReference(DocumentationProvider documentation = null, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases = default(ImmutableArray<string>), bool embedInteropTypes = false, string filePath = null, string display = null)
		{
			return new MetadataImageReference(this, new MetadataReferenceProperties(MetadataImageKind.Assembly, aliases, embedInteropTypes), documentation, filePath, display);
		}

		// Token: 0x04000588 RID: 1416
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Func<string, ModuleMetadata> _moduleFactoryOpt;

		// Token: 0x04000589 RID: 1417
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ModuleMetadata> _initialModules;

		// Token: 0x0400058A RID: 1418
		[Nullable(2)]
		private AssemblyMetadata.Data _lazyData;

		// Token: 0x0400058B RID: 1419
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ModuleMetadata> _lazyPublishedModules;

		// Token: 0x0400058C RID: 1420
		internal readonly WeakList<IAssemblySymbolInternal> CachedSymbols = new WeakList<IAssemblySymbolInternal>();

		// Token: 0x020005C6 RID: 1478
		[Nullable(0)]
		private sealed class Data
		{
			// Token: 0x0600364B RID: 13899 RVA: 0x0009919D File Offset: 0x0009739D
			private Data()
			{
			}

			// Token: 0x0600364C RID: 13900 RVA: 0x000991A5 File Offset: 0x000973A5
			public Data([Nullable(new byte[] { 0, 1 })] ImmutableArray<ModuleMetadata> modules, PEAssembly assembly)
			{
				this.Modules = modules;
				this.Assembly = assembly;
			}

			// Token: 0x17000F52 RID: 3922
			// (get) Token: 0x0600364D RID: 13901 RVA: 0x000991BB File Offset: 0x000973BB
			public bool IsDisposed
			{
				get
				{
					return this.Assembly == null;
				}
			}

			// Token: 0x0400169D RID: 5789
			public static readonly AssemblyMetadata.Data Disposed = new AssemblyMetadata.Data();

			// Token: 0x0400169E RID: 5790
			[Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableArray<ModuleMetadata> Modules;

			// Token: 0x0400169F RID: 5791
			[Nullable(2)]
			public readonly PEAssembly Assembly;
		}
	}
}
