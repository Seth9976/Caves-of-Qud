using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BE RID: 446
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ModuleMetadata : Metadata
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x000399D2 File Offset: 0x00037BD2
		private ModuleMetadata(PEReader peReader)
			: base(true, MetadataId.CreateNewId())
		{
			this._module = new PEModule(this, peReader, IntPtr.Zero, 0, false, false);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000399F5 File Offset: 0x00037BF5
		private ModuleMetadata(IntPtr metadata, int size, bool includeEmbeddedInteropTypes, bool ignoreAssemblyRefs)
			: base(true, MetadataId.CreateNewId())
		{
			this._module = new PEModule(this, null, metadata, size, includeEmbeddedInteropTypes, ignoreAssemblyRefs);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00039A15 File Offset: 0x00037C15
		private ModuleMetadata(ModuleMetadata metadata)
			: base(false, metadata.Id)
		{
			this._module = metadata.Module;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00039A30 File Offset: 0x00037C30
		public static ModuleMetadata CreateFromMetadata(IntPtr metadata, int size)
		{
			if (metadata == IntPtr.Zero)
			{
				throw new ArgumentNullException("metadata");
			}
			if (size <= 0)
			{
				throw new ArgumentOutOfRangeException(CodeAnalysisResources.SizeHasToBePositive, "size");
			}
			return new ModuleMetadata(metadata, size, false, false);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00039A67 File Offset: 0x00037C67
		internal static ModuleMetadata CreateFromMetadata(IntPtr metadata, int size, bool includeEmbeddedInteropTypes, bool ignoreAssemblyRefs = false)
		{
			return new ModuleMetadata(metadata, size, includeEmbeddedInteropTypes, ignoreAssemblyRefs);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00039A72 File Offset: 0x00037C72
		public unsafe static ModuleMetadata CreateFromImage(IntPtr peImage, int size)
		{
			if (peImage == IntPtr.Zero)
			{
				throw new ArgumentNullException("peImage");
			}
			if (size <= 0)
			{
				throw new ArgumentOutOfRangeException(CodeAnalysisResources.SizeHasToBePositive, "size");
			}
			return new ModuleMetadata(new PEReader((byte*)(void*)peImage, size));
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00039AB1 File Offset: 0x00037CB1
		public static ModuleMetadata CreateFromImage(IEnumerable<byte> peImage)
		{
			if (peImage == null)
			{
				throw new ArgumentNullException("peImage");
			}
			return ModuleMetadata.CreateFromImage(ImmutableArray.CreateRange<byte>(peImage));
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00039ACC File Offset: 0x00037CCC
		[NullableContext(0)]
		[return: Nullable(1)]
		public static ModuleMetadata CreateFromImage(ImmutableArray<byte> peImage)
		{
			if (peImage.IsDefault)
			{
				throw new ArgumentNullException("peImage");
			}
			return new ModuleMetadata(new PEReader(peImage));
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00039AED File Offset: 0x00037CED
		public static ModuleMetadata CreateFromStream(Stream peStream, bool leaveOpen = false)
		{
			return ModuleMetadata.CreateFromStream(peStream, leaveOpen ? PEStreamOptions.LeaveOpen : PEStreamOptions.Default);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00039AFC File Offset: 0x00037CFC
		public static ModuleMetadata CreateFromStream(Stream peStream, PEStreamOptions options)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanRead || !peStream.CanSeek)
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportReadAndSeek, "peStream");
			}
			if (peStream.Length == 0L && (options & PEStreamOptions.PrefetchEntireImage) != PEStreamOptions.Default && (options & PEStreamOptions.PrefetchMetadata) != PEStreamOptions.Default)
			{
				new PEHeaders(peStream);
			}
			return new ModuleMetadata(new PEReader(peStream, options));
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00039B5C File Offset: 0x00037D5C
		public static ModuleMetadata CreateFromFile(string path)
		{
			return ModuleMetadata.CreateFromStream(StandardFileSystem.Instance.OpenFileWithNormalizedException(path, 3, 1, 1), false);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00039B72 File Offset: 0x00037D72
		internal new ModuleMetadata Copy()
		{
			return new ModuleMetadata(this);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00039B7A File Offset: 0x00037D7A
		protected override Metadata CommonCopy()
		{
			return this.Copy();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00039B82 File Offset: 0x00037D82
		public override void Dispose()
		{
			this._isDisposed = true;
			if (this.IsImageOwner)
			{
				this._module.Dispose();
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00039B9E File Offset: 0x00037D9E
		public bool IsDisposed
		{
			get
			{
				return this._isDisposed || this._module.IsDisposed;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00039BB5 File Offset: 0x00037DB5
		internal PEModule Module
		{
			get
			{
				if (this.IsDisposed)
				{
					throw new ObjectDisposedException("ModuleMetadata");
				}
				return this._module;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00039BD0 File Offset: 0x00037DD0
		public string Name
		{
			get
			{
				return this.Module.Name;
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00039BDD File Offset: 0x00037DDD
		public Guid GetModuleVersionId()
		{
			return this.Module.GetModuleVersionIdOrThrow();
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00039BEA File Offset: 0x00037DEA
		public override MetadataImageKind Kind
		{
			get
			{
				return MetadataImageKind.Module;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00039BED File Offset: 0x00037DED
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> GetModuleNames()
		{
			return this.Module.GetMetadataModuleNamesOrThrow();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00039BFA File Offset: 0x00037DFA
		public MetadataReader GetMetadataReader()
		{
			return this.MetadataReader;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00039C02 File Offset: 0x00037E02
		internal MetadataReader MetadataReader
		{
			get
			{
				return this.Module.MetadataReader;
			}
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00039C0F File Offset: 0x00037E0F
		[NullableContext(2)]
		[return: Nullable(1)]
		public PortableExecutableReference GetReference(DocumentationProvider documentation = null, string filePath = null, string display = null)
		{
			return new MetadataImageReference(this, MetadataReferenceProperties.Module, documentation, filePath, display);
		}

		// Token: 0x04000599 RID: 1433
		private bool _isDisposed;

		// Token: 0x0400059A RID: 1434
		private readonly PEModule _module;
	}
}
