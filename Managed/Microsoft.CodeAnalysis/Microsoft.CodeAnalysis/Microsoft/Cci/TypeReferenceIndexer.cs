using System;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000C7 RID: 199
	internal sealed class TypeReferenceIndexer : ReferenceIndexerBase
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x00017FED File Offset: 0x000161ED
		internal TypeReferenceIndexer(EmitContext context)
			: base(context)
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00017FF6 File Offset: 0x000161F6
		public override void Visit(CommonPEModuleBuilder module)
		{
			base.Visit(module.GetSourceAssemblyAttributes(this.Context.IsRefAssembly));
			base.Visit(module.GetSourceAssemblySecurityAttributes());
			base.Visit(module.GetSourceModuleAttributes());
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00018027 File Offset: 0x00016227
		protected override void RecordAssemblyReference(IAssemblyReference assemblyReference)
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00018029 File Offset: 0x00016229
		protected override void RecordFileReference(IFileReference fileReference)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001802B File Offset: 0x0001622B
		protected override void RecordModuleReference(IModuleReference moduleReference)
		{
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001802D File Offset: 0x0001622D
		public override void Visit(IPlatformInvokeInformation platformInvokeInformation)
		{
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001802F File Offset: 0x0001622F
		protected override void ProcessMethodBody(IMethodDefinition method)
		{
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00018031 File Offset: 0x00016231
		protected override void RecordTypeReference(ITypeReference typeReference)
		{
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00018033 File Offset: 0x00016233
		protected override void ReserveFieldToken(IFieldReference fieldReference)
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00018035 File Offset: 0x00016235
		protected override void ReserveMethodToken(IMethodReference methodReference)
		{
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00018037 File Offset: 0x00016237
		protected override void RecordTypeMemberReference(ITypeMemberReference typeMemberReference)
		{
		}
	}
}
