using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C1 RID: 449
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnresolvedMetadataReference : MetadataReference
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00039DB6 File Offset: 0x00037FB6
		public string Reference { get; }

		// Token: 0x060012DA RID: 4826 RVA: 0x00039DBE File Offset: 0x00037FBE
		internal UnresolvedMetadataReference(string reference, MetadataReferenceProperties properties)
			: base(properties)
		{
			this.Reference = reference;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00039DCE File Offset: 0x00037FCE
		public override string Display
		{
			get
			{
				return CodeAnalysisResources.Unresolved + this.Reference;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00039DE0 File Offset: 0x00037FE0
		internal override bool IsUnresolved
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00039DE3 File Offset: 0x00037FE3
		internal override MetadataReference WithPropertiesImplReturningMetadataReference(MetadataReferenceProperties properties)
		{
			return new UnresolvedMetadataReference(this.Reference, properties);
		}
	}
}
