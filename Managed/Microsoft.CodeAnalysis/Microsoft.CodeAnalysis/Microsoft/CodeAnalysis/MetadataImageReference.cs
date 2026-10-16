using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BA RID: 442
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class MetadataImageReference : PortableExecutableReference
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x000393FE File Offset: 0x000375FE
		[NullableContext(2)]
		internal MetadataImageReference([Nullable(1)] Metadata metadata, MetadataReferenceProperties properties, DocumentationProvider documentation, string filePath, string display)
			: base(properties, filePath, documentation ?? DocumentationProvider.Default)
		{
			this._display = display;
			this._metadata = metadata;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00039422 File Offset: 0x00037622
		protected override Metadata GetMetadataImpl()
		{
			return this._metadata;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0003942A File Offset: 0x0003762A
		protected override DocumentationProvider CreateDocumentationProvider()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00039431 File Offset: 0x00037631
		protected override PortableExecutableReference WithPropertiesImpl(MetadataReferenceProperties properties)
		{
			return new MetadataImageReference(this._metadata, properties, base.DocumentationProvider, base.FilePath, this._display);
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00039454 File Offset: 0x00037654
		public override string Display
		{
			get
			{
				string text;
				if ((text = this._display) == null && (text = base.FilePath) == null)
				{
					if (base.Properties.Kind != MetadataImageKind.Assembly)
					{
						return CodeAnalysisResources.InMemoryModule;
					}
					text = CodeAnalysisResources.InMemoryAssembly;
				}
				return text;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00039490 File Offset: 0x00037690
		private string GetDebuggerDisplay()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((base.Properties.Kind == MetadataImageKind.Module) ? "Module" : "Assembly");
			if (!base.Properties.Aliases.IsEmpty)
			{
				stringBuilder.Append(" Aliases={");
				stringBuilder.Append(string.Join(", ", base.Properties.Aliases));
				stringBuilder.Append("}");
			}
			if (base.Properties.EmbedInteropTypes)
			{
				stringBuilder.Append(" Embed");
			}
			if (base.FilePath != null)
			{
				stringBuilder.Append(" Path='");
				stringBuilder.Append(base.FilePath);
				stringBuilder.Append("'");
			}
			if (this._display != null)
			{
				stringBuilder.Append(" Display='");
				stringBuilder.Append(this._display);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000592 RID: 1426
		[Nullable(2)]
		private readonly string _display;

		// Token: 0x04000593 RID: 1427
		private readonly Metadata _metadata;
	}
}
