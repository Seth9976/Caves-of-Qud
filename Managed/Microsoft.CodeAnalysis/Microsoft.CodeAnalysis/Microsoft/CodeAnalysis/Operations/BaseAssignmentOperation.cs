using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200040A RID: 1034
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class BaseAssignmentOperation : Operation, IAssignmentOperation, IOperation
	{
		// Token: 0x060027B8 RID: 10168 RVA: 0x00075F51 File Offset: 0x00074151
		protected BaseAssignmentOperation(IOperation target, IOperation value, [Nullable(2)] SemanticModel semanticModel, SyntaxNode syntax, bool isImplicit)
			: base(semanticModel, syntax, isImplicit)
		{
			this.Target = Operation.SetParentOperation<IOperation>(target, this);
			this.Value = Operation.SetParentOperation<IOperation>(value, this);
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00075F78 File Offset: 0x00074178
		public IOperation Target { get; }

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x00075F80 File Offset: 0x00074180
		public IOperation Value { get; }
	}
}
