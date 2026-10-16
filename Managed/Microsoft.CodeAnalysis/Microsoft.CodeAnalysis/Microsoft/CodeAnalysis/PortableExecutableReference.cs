using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BF RID: 447
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PortableExecutableReference : MetadataReference
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x00039C1F File Offset: 0x00037E1F
		[NullableContext(2)]
		protected PortableExecutableReference(MetadataReferenceProperties properties, string fullPath = null, DocumentationProvider initialDocumentation = null)
			: base(properties)
		{
			this._filePath = fullPath;
			this._lazyDocumentation = initialDocumentation;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00039C36 File Offset: 0x00037E36
		[Nullable(2)]
		public override string Display
		{
			[NullableContext(2)]
			get
			{
				return this.FilePath;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00039C3E File Offset: 0x00037E3E
		[Nullable(2)]
		public string FilePath
		{
			[NullableContext(2)]
			get
			{
				return this._filePath;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00039C46 File Offset: 0x00037E46
		internal DocumentationProvider DocumentationProvider
		{
			get
			{
				if (this._lazyDocumentation == null)
				{
					Interlocked.CompareExchange<DocumentationProvider>(ref this._lazyDocumentation, this.CreateDocumentationProvider(), null);
				}
				return this._lazyDocumentation;
			}
		}

		// Token: 0x060012CC RID: 4812
		protected abstract DocumentationProvider CreateDocumentationProvider();

		// Token: 0x060012CD RID: 4813 RVA: 0x00039C69 File Offset: 0x00037E69
		public new PortableExecutableReference WithAliases(IEnumerable<string> aliases)
		{
			return this.WithAliases(ImmutableArray.CreateRange<string>(aliases));
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00039C78 File Offset: 0x00037E78
		public new PortableExecutableReference WithAliases([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases)
		{
			return this.WithProperties(base.Properties.WithAliases(aliases));
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00039C9C File Offset: 0x00037E9C
		public new PortableExecutableReference WithEmbedInteropTypes(bool value)
		{
			return this.WithProperties(base.Properties.WithEmbedInteropTypes(value));
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00039CBE File Offset: 0x00037EBE
		public new PortableExecutableReference WithProperties(MetadataReferenceProperties properties)
		{
			if (properties == base.Properties)
			{
				return this;
			}
			return this.WithPropertiesImpl(properties);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00039CD7 File Offset: 0x00037ED7
		internal sealed override MetadataReference WithPropertiesImplReturningMetadataReference(MetadataReferenceProperties properties)
		{
			return this.WithPropertiesImpl(properties);
		}

		// Token: 0x060012D2 RID: 4818
		protected abstract PortableExecutableReference WithPropertiesImpl(MetadataReferenceProperties properties);

		// Token: 0x060012D3 RID: 4819
		protected abstract Metadata GetMetadataImpl();

		// Token: 0x060012D4 RID: 4820 RVA: 0x00039CE0 File Offset: 0x00037EE0
		internal Metadata GetMetadataNoCopy()
		{
			return this.GetMetadataImpl();
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00039CE8 File Offset: 0x00037EE8
		public Metadata GetMetadata()
		{
			return this.GetMetadataNoCopy().Copy();
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00039CF5 File Offset: 0x00037EF5
		public MetadataId GetMetadataId()
		{
			return this.GetMetadataNoCopy().Id;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00039D04 File Offset: 0x00037F04
		internal static Diagnostic ExceptionToDiagnostic(Exception e, CommonMessageProvider messageProvider, Location location, string display, MetadataImageKind kind)
		{
			if (e is BadImageFormatException)
			{
				int num = ((kind == MetadataImageKind.Assembly) ? messageProvider.ERR_InvalidAssemblyMetadata : messageProvider.ERR_InvalidModuleMetadata);
				return messageProvider.CreateDiagnostic(num, location, new object[] { display, e.Message });
			}
			FileNotFoundException ex = e as FileNotFoundException;
			if (ex != null)
			{
				return messageProvider.CreateDiagnostic(messageProvider.ERR_MetadataFileNotFound, location, new object[] { ex.FileName ?? string.Empty });
			}
			int num2 = ((kind == MetadataImageKind.Assembly) ? messageProvider.ERR_ErrorOpeningAssemblyFile : messageProvider.ERR_ErrorOpeningModuleFile);
			return messageProvider.CreateDiagnostic(num2, location, new object[] { display, e.Message });
		}

		// Token: 0x0400059B RID: 1435
		[Nullable(2)]
		private readonly string _filePath;

		// Token: 0x0400059C RID: 1436
		[Nullable(2)]
		private DocumentationProvider _lazyDocumentation;
	}
}
