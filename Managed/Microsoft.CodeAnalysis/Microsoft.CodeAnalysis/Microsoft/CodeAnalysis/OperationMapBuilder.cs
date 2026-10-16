using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C4 RID: 452
	[NullableContext(1)]
	[Nullable(0)]
	internal static class OperationMapBuilder
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x00039F6B File Offset: 0x0003816B
		internal static void AddToMap(IOperation root, Dictionary<SyntaxNode, IOperation> dictionary)
		{
			OperationMapBuilder.Walker.Instance.Visit(root, dictionary);
		}

		// Token: 0x020005CC RID: 1484
		[Nullable(new byte[] { 0, 1, 1, 1 })]
		private sealed class Walker : OperationWalker<Dictionary<SyntaxNode, IOperation>>
		{
			// Token: 0x06003663 RID: 13923 RVA: 0x000993A8 File Offset: 0x000975A8
			[return: Nullable(2)]
			public override object DefaultVisit(IOperation operation, Dictionary<SyntaxNode, IOperation> argument)
			{
				OperationMapBuilder.Walker.RecordOperation(operation, argument);
				return base.DefaultVisit(operation, argument);
			}

			// Token: 0x06003664 RID: 13924 RVA: 0x000993BC File Offset: 0x000975BC
			[NullableContext(2)]
			public override object VisitBinaryOperator([DisallowNull] IBinaryOperation operation, [Nullable(1)] Dictionary<SyntaxNode, IOperation> argument)
			{
				for (;;)
				{
					OperationMapBuilder.Walker.RecordOperation(operation, argument);
					this.Visit(operation.RightOperand, argument);
					IBinaryOperation binaryOperation = operation.LeftOperand as IBinaryOperation;
					if (binaryOperation == null)
					{
						break;
					}
					operation = binaryOperation;
				}
				this.Visit(operation.LeftOperand, argument);
				return null;
			}

			// Token: 0x06003665 RID: 13925 RVA: 0x00099401 File Offset: 0x00097601
			[return: Nullable(2)]
			internal override object VisitNoneOperation(IOperation operation, Dictionary<SyntaxNode, IOperation> argument)
			{
				return this.DefaultVisit(operation, argument);
			}

			// Token: 0x06003666 RID: 13926 RVA: 0x0009940B File Offset: 0x0009760B
			private static void RecordOperation(IOperation operation, Dictionary<SyntaxNode, IOperation> argument)
			{
				if (!operation.IsImplicit)
				{
					argument.Add(operation.Syntax, operation);
				}
			}

			// Token: 0x040016A9 RID: 5801
			internal static readonly OperationMapBuilder.Walker Instance = new OperationMapBuilder.Walker();
		}
	}
}
