using System;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000498 RID: 1176
	internal class SpecializedMethodReference : MethodReference, ISpecializedMethodReference, IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x060061A8 RID: 25000 RVA: 0x001781D5 File Offset: 0x001763D5
		public SpecializedMethodReference(MethodSymbol underlyingMethod)
			: base(underlyingMethod)
		{
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x001781DE File Offset: 0x001763DE
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x001781E7 File Offset: 0x001763E7
		IMethodReference ISpecializedMethodReference.UnspecializedVersion
		{
			get
			{
				return this.UnderlyingMethod.OriginalDefinition.GetCciAdapter();
			}
		}

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x060061AB RID: 25003 RVA: 0x001781F9 File Offset: 0x001763F9
		public override ISpecializedMethodReference AsSpecializedMethodReference
		{
			get
			{
				return this;
			}
		}
	}
}
