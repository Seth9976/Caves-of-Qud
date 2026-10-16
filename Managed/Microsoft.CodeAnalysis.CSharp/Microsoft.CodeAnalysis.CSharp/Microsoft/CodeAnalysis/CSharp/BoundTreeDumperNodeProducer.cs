using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E3 RID: 483
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2, 1 })]
	internal sealed class BoundTreeDumperNodeProducer : BoundTreeVisitor<object, TreeDumperNode>
	{
		// Token: 0x06002057 RID: 8279 RVA: 0x000A41D5 File Offset: 0x000A23D5
		private BoundTreeDumperNodeProducer()
		{
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000A41DD File Offset: 0x000A23DD
		public static TreeDumperNode MakeTree(BoundNode node)
		{
			return new BoundTreeDumperNodeProducer().Visit(node, null);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000A41EC File Offset: 0x000A23EC
		public override TreeDumperNode VisitFieldEqualsValue(BoundFieldEqualsValue node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fieldEqualsValue", null, new TreeDumperNode[]
			{
				new TreeDumperNode("field", node.Field, null),
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000A4274 File Offset: 0x000A2474
		public override TreeDumperNode VisitPropertyEqualsValue(BoundPropertyEqualsValue node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("propertyEqualsValue", null, new TreeDumperNode[]
			{
				new TreeDumperNode("property", node.Property, null),
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000A42FC File Offset: 0x000A24FC
		public override TreeDumperNode VisitParameterEqualsValue(BoundParameterEqualsValue node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("parameterEqualsValue", null, new TreeDumperNode[]
			{
				new TreeDumperNode("parameter", node.Parameter, null),
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000A4384 File Offset: 0x000A2584
		public override TreeDumperNode VisitGlobalStatementInitializer(BoundGlobalStatementInitializer node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("globalStatementInitializer", null, new TreeDumperNode[]
			{
				new TreeDumperNode("statement", null, new TreeDumperNode[] { this.Visit(node.Statement, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000A43E0 File Offset: 0x000A25E0
		public override TreeDumperNode VisitDeconstructValuePlaceholder(BoundDeconstructValuePlaceholder node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("deconstructValuePlaceholder", null, new TreeDumperNode[]
			{
				new TreeDumperNode("valEscape", node.ValEscape, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000A4460 File Offset: 0x000A2660
		public override TreeDumperNode VisitTupleOperandPlaceholder(BoundTupleOperandPlaceholder node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("tupleOperandPlaceholder", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000A44C4 File Offset: 0x000A26C4
		public override TreeDumperNode VisitAwaitableValuePlaceholder(BoundAwaitableValuePlaceholder node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("awaitableValuePlaceholder", null, new TreeDumperNode[]
			{
				new TreeDumperNode("valEscape", node.ValEscape, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000A4544 File Offset: 0x000A2744
		public override TreeDumperNode VisitDisposableValuePlaceholder(BoundDisposableValuePlaceholder node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("disposableValuePlaceholder", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000A45A8 File Offset: 0x000A27A8
		public override TreeDumperNode VisitObjectOrCollectionValuePlaceholder(BoundObjectOrCollectionValuePlaceholder node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("objectOrCollectionValuePlaceholder", null, new TreeDumperNode[]
			{
				new TreeDumperNode("isNewInstance", node.IsNewInstance, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000A4628 File Offset: 0x000A2828
		public override TreeDumperNode VisitDup(BoundDup node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dup", null, new TreeDumperNode[]
			{
				new TreeDumperNode("refKind", node.RefKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000A46A8 File Offset: 0x000A28A8
		public override TreeDumperNode VisitPassByCopy(BoundPassByCopy node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("passByCopy", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000A4730 File Offset: 0x000A2930
		public override TreeDumperNode VisitBadExpression(BoundBadExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("badExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("symbols", node.Symbols, null),
				new TreeDumperNode("childBoundNodes", null, node.ChildBoundNodes.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000A47EC File Offset: 0x000A29EC
		public override TreeDumperNode VisitBadStatement(BoundBadStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("badStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("childBoundNodes", null, node.ChildBoundNodes.Select<BoundNode, TreeDumperNode>((BoundNode x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000A4848 File Offset: 0x000A2A48
		public override TreeDumperNode VisitExtractedFinallyBlock(BoundExtractedFinallyBlock node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("extractedFinallyBlock", null, new TreeDumperNode[]
			{
				new TreeDumperNode("finallyBlock", null, new TreeDumperNode[] { this.Visit(node.FinallyBlock, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000A48A4 File Offset: 0x000A2AA4
		public override TreeDumperNode VisitTypeExpression(BoundTypeExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			string text = "typeExpression";
			object obj = null;
			TreeDumperNode[] array = new TreeDumperNode[7];
			array[0] = new TreeDumperNode("aliasOpt", node.AliasOpt, null);
			array[1] = new TreeDumperNode("boundContainingTypeOpt", null, new TreeDumperNode[] { this.Visit(node.BoundContainingTypeOpt, null) });
			int num = 2;
			string text2 = "boundDimensionsOpt";
			object obj2 = null;
			IEnumerable<TreeDumperNode> enumerable;
			if (!node.BoundDimensionsOpt.IsDefault)
			{
				enumerable = node.BoundDimensionsOpt.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null));
			}
			else
			{
				IEnumerable<TreeDumperNode> enumerable2 = Array.Empty<TreeDumperNode>();
				enumerable = enumerable2;
			}
			array[num] = new TreeDumperNode(text2, obj2, enumerable);
			array[3] = new TreeDumperNode("typeWithAnnotations", node.TypeWithAnnotations, null);
			array[4] = new TreeDumperNode("type", node.Type, null);
			array[5] = new TreeDumperNode("isSuppressed", node.IsSuppressed, null);
			array[6] = new TreeDumperNode("hasErrors", node.HasErrors, null);
			return new TreeDumperNode(text, obj, array);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000A4998 File Offset: 0x000A2B98
		public override TreeDumperNode VisitTypeOrValueExpression(BoundTypeOrValueExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("typeOrValueExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("data", node.Data, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000A4A18 File Offset: 0x000A2C18
		public override TreeDumperNode VisitNamespaceExpression(BoundNamespaceExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("namespaceExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("namespaceSymbol", node.NamespaceSymbol, null),
				new TreeDumperNode("aliasOpt", node.AliasOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000A4AA4 File Offset: 0x000A2CA4
		public override TreeDumperNode VisitUnaryOperator(BoundUnaryOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unaryOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalUserDefinedOperatorsOpt", node.OriginalUserDefinedOperatorsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000A4BA0 File Offset: 0x000A2DA0
		public override TreeDumperNode VisitIncrementOperator(BoundIncrementOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("incrementOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("operandConversion", node.OperandConversion, null),
				new TreeDumperNode("resultConversion", node.ResultConversion, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalUserDefinedOperatorsOpt", node.OriginalUserDefinedOperatorsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000A4CBC File Offset: 0x000A2EBC
		public override TreeDumperNode VisitAddressOfOperator(BoundAddressOfOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("addressOfOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("isManaged", node.IsManaged, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000A4D60 File Offset: 0x000A2F60
		public override TreeDumperNode VisitUnconvertedAddressOfOperator(BoundUnconvertedAddressOfOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unconvertedAddressOfOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000A4DE8 File Offset: 0x000A2FE8
		public override TreeDumperNode VisitFunctionPointerLoad(BoundFunctionPointerLoad node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("functionPointerLoad", null, new TreeDumperNode[]
			{
				new TreeDumperNode("targetMethod", node.TargetMethod, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x000A4E60 File Offset: 0x000A3060
		public override TreeDumperNode VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("pointerIndirectionOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000A4EE8 File Offset: 0x000A30E8
		public override TreeDumperNode VisitPointerElementAccess(BoundPointerElementAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("pointerElementAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("index", null, new TreeDumperNode[] { this.Visit(node.Index, null) }),
				new TreeDumperNode("@checked", node.Checked, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000A4FB0 File Offset: 0x000A31B0
		public override TreeDumperNode VisitFunctionPointerInvocation(BoundFunctionPointerInvocation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("functionPointerInvocation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("invokedExpression", null, new TreeDumperNode[] { this.Visit(node.InvokedExpression, null) }),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000A5090 File Offset: 0x000A3290
		public override TreeDumperNode VisitRefTypeOperator(BoundRefTypeOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("refTypeOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("getTypeFromHandle", node.GetTypeFromHandle, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000A512C File Offset: 0x000A332C
		public override TreeDumperNode VisitMakeRefOperator(BoundMakeRefOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("makeRefOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000A51B4 File Offset: 0x000A33B4
		public override TreeDumperNode VisitRefValueOperator(BoundRefValueOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("refValueOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("nullableAnnotation", node.NullableAnnotation, null),
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000A5258 File Offset: 0x000A3458
		public override TreeDumperNode VisitFromEndIndexExpression(BoundFromEndIndexExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fromEndIndexExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000A52F4 File Offset: 0x000A34F4
		public override TreeDumperNode VisitRangeExpression(BoundRangeExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("rangeExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("leftOperandOpt", null, new TreeDumperNode[] { this.Visit(node.LeftOperandOpt, null) }),
				new TreeDumperNode("rightOperandOpt", null, new TreeDumperNode[] { this.Visit(node.RightOperandOpt, null) }),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000A53B4 File Offset: 0x000A35B4
		public override TreeDumperNode VisitBinaryOperator(BoundBinaryOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("binaryOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalUserDefinedOperatorsOpt", node.OriginalUserDefinedOperatorsOpt, null),
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000A54D8 File Offset: 0x000A36D8
		public override TreeDumperNode VisitTupleBinaryOperator(BoundTupleBinaryOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("tupleBinaryOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("operators", node.Operators, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000A55B4 File Offset: 0x000A37B4
		public override TreeDumperNode VisitUserDefinedConditionalLogicalOperator(BoundUserDefinedConditionalLogicalOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("userDefinedConditionalLogicalOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("logicalOperator", node.LogicalOperator, null),
				new TreeDumperNode("trueOperator", node.TrueOperator, null),
				new TreeDumperNode("falseOperator", node.FalseOperator, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalUserDefinedOperatorsOpt", node.OriginalUserDefinedOperatorsOpt, null),
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000A56EC File Offset: 0x000A38EC
		public override TreeDumperNode VisitCompoundAssignmentOperator(BoundCompoundAssignmentOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("compoundAssignmentOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("@operator", node.Operator, null),
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("leftConversion", node.LeftConversion, null),
				new TreeDumperNode("finalConversion", node.FinalConversion, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalUserDefinedOperatorsOpt", node.OriginalUserDefinedOperatorsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000A5818 File Offset: 0x000A3A18
		public override TreeDumperNode VisitAssignmentOperator(BoundAssignmentOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("assignmentOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("isRef", node.IsRef, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x000A58E0 File Offset: 0x000A3AE0
		public override TreeDumperNode VisitDeconstructionAssignmentOperator(BoundDeconstructionAssignmentOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("deconstructionAssignmentOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("isUsed", node.IsUsed, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x000A59A8 File Offset: 0x000A3BA8
		public override TreeDumperNode VisitNullCoalescingOperator(BoundNullCoalescingOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("nullCoalescingOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("leftOperand", null, new TreeDumperNode[] { this.Visit(node.LeftOperand, null) }),
				new TreeDumperNode("rightOperand", null, new TreeDumperNode[] { this.Visit(node.RightOperand, null) }),
				new TreeDumperNode("leftConversion", node.LeftConversion, null),
				new TreeDumperNode("operatorResultKind", node.OperatorResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x000A5A88 File Offset: 0x000A3C88
		public override TreeDumperNode VisitNullCoalescingAssignmentOperator(BoundNullCoalescingAssignmentOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("nullCoalescingAssignmentOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("leftOperand", null, new TreeDumperNode[] { this.Visit(node.LeftOperand, null) }),
				new TreeDumperNode("rightOperand", null, new TreeDumperNode[] { this.Visit(node.RightOperand, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000A5B34 File Offset: 0x000A3D34
		public override TreeDumperNode VisitUnconvertedConditionalOperator(BoundUnconvertedConditionalOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unconvertedConditionalOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("consequence", null, new TreeDumperNode[] { this.Visit(node.Consequence, null) }),
				new TreeDumperNode("alternative", null, new TreeDumperNode[] { this.Visit(node.Alternative, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("noCommonTypeError", node.NoCommonTypeError, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000A5C34 File Offset: 0x000A3E34
		public override TreeDumperNode VisitConditionalOperator(BoundConditionalOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("conditionalOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("isRef", node.IsRef, null),
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("consequence", null, new TreeDumperNode[] { this.Visit(node.Consequence, null) }),
				new TreeDumperNode("alternative", null, new TreeDumperNode[] { this.Visit(node.Alternative, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("naturalTypeOpt", node.NaturalTypeOpt, null),
				new TreeDumperNode("wasTargetTyped", node.WasTargetTyped, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x000A5D60 File Offset: 0x000A3F60
		public override TreeDumperNode VisitArrayAccess(BoundArrayAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("arrayAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("indices", null, node.Indices.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000A5E10 File Offset: 0x000A4010
		public override TreeDumperNode VisitArrayLength(BoundArrayLength node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("arrayLength", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000A5E98 File Offset: 0x000A4098
		public override TreeDumperNode VisitAwaitableInfo(BoundAwaitableInfo node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("awaitableInfo", null, new TreeDumperNode[]
			{
				new TreeDumperNode("awaitableInstancePlaceholder", null, new TreeDumperNode[] { this.Visit(node.AwaitableInstancePlaceholder, null) }),
				new TreeDumperNode("isDynamic", node.IsDynamic, null),
				new TreeDumperNode("getAwaiter", null, new TreeDumperNode[] { this.Visit(node.GetAwaiter, null) }),
				new TreeDumperNode("isCompleted", node.IsCompleted, null),
				new TreeDumperNode("getResult", node.GetResult, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000A5F58 File Offset: 0x000A4158
		public override TreeDumperNode VisitAwaitExpression(BoundAwaitExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("awaitExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("awaitableInfo", null, new TreeDumperNode[] { this.Visit(node.AwaitableInfo, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000A6004 File Offset: 0x000A4204
		public override TreeDumperNode VisitTypeOfOperator(BoundTypeOfOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("typeOfOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("sourceType", null, new TreeDumperNode[] { this.Visit(node.SourceType, null) }),
				new TreeDumperNode("getTypeFromHandle", node.GetTypeFromHandle, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000A60A0 File Offset: 0x000A42A0
		public override TreeDumperNode VisitMethodDefIndex(BoundMethodDefIndex node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("methodDefIndex", null, new TreeDumperNode[]
			{
				new TreeDumperNode("method", node.Method, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000A6118 File Offset: 0x000A4318
		public override TreeDumperNode VisitMaximumMethodDefIndex(BoundMaximumMethodDefIndex node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("maximumMethodDefIndex", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000A617C File Offset: 0x000A437C
		public override TreeDumperNode VisitInstrumentationPayloadRoot(BoundInstrumentationPayloadRoot node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("instrumentationPayloadRoot", null, new TreeDumperNode[]
			{
				new TreeDumperNode("analysisKind", node.AnalysisKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000A61FC File Offset: 0x000A43FC
		public override TreeDumperNode VisitModuleVersionId(BoundModuleVersionId node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("moduleVersionId", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000A6260 File Offset: 0x000A4460
		public override TreeDumperNode VisitModuleVersionIdString(BoundModuleVersionIdString node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("moduleVersionIdString", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000A62C4 File Offset: 0x000A44C4
		public override TreeDumperNode VisitSourceDocumentIndex(BoundSourceDocumentIndex node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sourceDocumentIndex", null, new TreeDumperNode[]
			{
				new TreeDumperNode("document", node.Document, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000A633C File Offset: 0x000A453C
		public override TreeDumperNode VisitMethodInfo(BoundMethodInfo node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("methodInfo", null, new TreeDumperNode[]
			{
				new TreeDumperNode("method", node.Method, null),
				new TreeDumperNode("getMethodFromHandle", node.GetMethodFromHandle, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000A63C8 File Offset: 0x000A45C8
		public override TreeDumperNode VisitFieldInfo(BoundFieldInfo node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fieldInfo", null, new TreeDumperNode[]
			{
				new TreeDumperNode("field", node.Field, null),
				new TreeDumperNode("getFieldFromHandle", node.GetFieldFromHandle, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000A6454 File Offset: 0x000A4654
		public override TreeDumperNode VisitDefaultLiteral(BoundDefaultLiteral node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("defaultLiteral", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000A64B8 File Offset: 0x000A46B8
		public override TreeDumperNode VisitDefaultExpression(BoundDefaultExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("defaultExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("targetType", null, new TreeDumperNode[] { this.Visit(node.TargetType, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000A6554 File Offset: 0x000A4754
		public override TreeDumperNode VisitIsOperator(BoundIsOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("isOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("targetType", null, new TreeDumperNode[] { this.Visit(node.TargetType, null) }),
				new TreeDumperNode("conversion", node.Conversion, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000A661C File Offset: 0x000A481C
		public override TreeDumperNode VisitAsOperator(BoundAsOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("asOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("targetType", null, new TreeDumperNode[] { this.Visit(node.TargetType, null) }),
				new TreeDumperNode("conversion", node.Conversion, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000A66E4 File Offset: 0x000A48E4
		public override TreeDumperNode VisitSizeOfOperator(BoundSizeOfOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sizeOfOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("sourceType", null, new TreeDumperNode[] { this.Visit(node.SourceType, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000A6780 File Offset: 0x000A4980
		public override TreeDumperNode VisitConversion(BoundConversion node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("conversion", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("conversion", node.Conversion, null),
				new TreeDumperNode("isBaseConversion", node.IsBaseConversion, null),
				new TreeDumperNode("@checked", node.Checked, null),
				new TreeDumperNode("explicitCastInCode", node.ExplicitCastInCode, null),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("conversionGroupOpt", node.ConversionGroupOpt, null),
				new TreeDumperNode("originalUserDefinedConversionsOpt", node.OriginalUserDefinedConversionsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000A68B0 File Offset: 0x000A4AB0
		public override TreeDumperNode VisitReadOnlySpanFromArray(BoundReadOnlySpanFromArray node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("readOnlySpanFromArray", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operand", null, new TreeDumperNode[] { this.Visit(node.Operand, null) }),
				new TreeDumperNode("conversionMethod", node.ConversionMethod, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000A694C File Offset: 0x000A4B4C
		public override TreeDumperNode VisitArgList(BoundArgList node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("argList", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000A69B0 File Offset: 0x000A4BB0
		public override TreeDumperNode VisitArgListOperator(BoundArgListOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("argListOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000A6A54 File Offset: 0x000A4C54
		public override TreeDumperNode VisitFixedLocalCollectionInitializer(BoundFixedLocalCollectionInitializer node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fixedLocalCollectionInitializer", null, new TreeDumperNode[]
			{
				new TreeDumperNode("elementPointerType", node.ElementPointerType, null),
				new TreeDumperNode("elementPointerTypeConversion", node.ElementPointerTypeConversion, null),
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("getPinnableOpt", node.GetPinnableOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000A6B20 File Offset: 0x000A4D20
		public override TreeDumperNode VisitSequencePoint(BoundSequencePoint node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sequencePoint", null, new TreeDumperNode[]
			{
				new TreeDumperNode("statementOpt", null, new TreeDumperNode[] { this.Visit(node.StatementOpt, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000A6B7C File Offset: 0x000A4D7C
		public override TreeDumperNode VisitSequencePointWithSpan(BoundSequencePointWithSpan node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sequencePointWithSpan", null, new TreeDumperNode[]
			{
				new TreeDumperNode("statementOpt", null, new TreeDumperNode[] { this.Visit(node.StatementOpt, null) }),
				new TreeDumperNode("span", node.Span, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000A6BF0 File Offset: 0x000A4DF0
		public override TreeDumperNode VisitSavePreviousSequencePoint(BoundSavePreviousSequencePoint node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("savePreviousSequencePoint", null, new TreeDumperNode[]
			{
				new TreeDumperNode("identifier", node.Identifier, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x000A6C30 File Offset: 0x000A4E30
		public override TreeDumperNode VisitRestorePreviousSequencePoint(BoundRestorePreviousSequencePoint node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("restorePreviousSequencePoint", null, new TreeDumperNode[]
			{
				new TreeDumperNode("identifier", node.Identifier, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x000A6C70 File Offset: 0x000A4E70
		public override TreeDumperNode VisitStepThroughSequencePoint(BoundStepThroughSequencePoint node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("stepThroughSequencePoint", null, new TreeDumperNode[]
			{
				new TreeDumperNode("span", node.Span, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000A6CC0 File Offset: 0x000A4EC0
		public override TreeDumperNode VisitBlock(BoundBlock node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("block", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("localFunctions", node.LocalFunctions, null),
				new TreeDumperNode("statements", null, node.Statements.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000A6D50 File Offset: 0x000A4F50
		public override TreeDumperNode VisitScope(BoundScope node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("scope", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("statements", null, node.Statements.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000A6DC8 File Offset: 0x000A4FC8
		public override TreeDumperNode VisitStateMachineScope(BoundStateMachineScope node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("stateMachineScope", null, new TreeDumperNode[]
			{
				new TreeDumperNode("fields", node.Fields, null),
				new TreeDumperNode("statement", null, new TreeDumperNode[] { this.Visit(node.Statement, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000A6E3C File Offset: 0x000A503C
		public override TreeDumperNode VisitLocalDeclaration(BoundLocalDeclaration node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			string text = "localDeclaration";
			object obj = null;
			TreeDumperNode[] array = new TreeDumperNode[6];
			array[0] = new TreeDumperNode("localSymbol", node.LocalSymbol, null);
			array[1] = new TreeDumperNode("declaredTypeOpt", null, new TreeDumperNode[] { this.Visit(node.DeclaredTypeOpt, null) });
			array[2] = new TreeDumperNode("initializerOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerOpt, null) });
			int num = 3;
			string text2 = "argumentsOpt";
			object obj2 = null;
			IEnumerable<TreeDumperNode> enumerable;
			if (!node.ArgumentsOpt.IsDefault)
			{
				enumerable = node.ArgumentsOpt.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null));
			}
			else
			{
				IEnumerable<TreeDumperNode> enumerable2 = Array.Empty<TreeDumperNode>();
				enumerable = enumerable2;
			}
			array[num] = new TreeDumperNode(text2, obj2, enumerable);
			array[4] = new TreeDumperNode("inferredType", node.InferredType, null);
			array[5] = new TreeDumperNode("hasErrors", node.HasErrors, null);
			return new TreeDumperNode(text, obj, array);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000A6F28 File Offset: 0x000A5128
		public override TreeDumperNode VisitMultipleLocalDeclarations(BoundMultipleLocalDeclarations node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("multipleLocalDeclarations", null, new TreeDumperNode[]
			{
				new TreeDumperNode("localDeclarations", null, node.LocalDeclarations.Select<BoundLocalDeclaration, TreeDumperNode>((BoundLocalDeclaration x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000A6F84 File Offset: 0x000A5184
		public override TreeDumperNode VisitUsingLocalDeclarations(BoundUsingLocalDeclarations node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("usingLocalDeclarations", null, new TreeDumperNode[]
			{
				new TreeDumperNode("patternDisposeInfoOpt", node.PatternDisposeInfoOpt, null),
				new TreeDumperNode("iDisposableConversion", node.IDisposableConversion, null),
				new TreeDumperNode("awaitOpt", null, new TreeDumperNode[] { this.Visit(node.AwaitOpt, null) }),
				new TreeDumperNode("localDeclarations", null, node.LocalDeclarations.Select<BoundLocalDeclaration, TreeDumperNode>((BoundLocalDeclaration x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000A7034 File Offset: 0x000A5234
		public override TreeDumperNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("localFunctionStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("symbol", node.Symbol, null),
				new TreeDumperNode("blockBody", null, new TreeDumperNode[] { this.Visit(node.BlockBody, null) }),
				new TreeDumperNode("expressionBody", null, new TreeDumperNode[] { this.Visit(node.ExpressionBody, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000A70C8 File Offset: 0x000A52C8
		public override TreeDumperNode VisitNoOpStatement(BoundNoOpStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("noOpStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("flavor", node.Flavor, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000A7118 File Offset: 0x000A5318
		public override TreeDumperNode VisitReturnStatement(BoundReturnStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("returnStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("refKind", node.RefKind, null),
				new TreeDumperNode("expressionOpt", null, new TreeDumperNode[] { this.Visit(node.ExpressionOpt, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000A718C File Offset: 0x000A538C
		public override TreeDumperNode VisitYieldReturnStatement(BoundYieldReturnStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("yieldReturnStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000A71E7 File Offset: 0x000A53E7
		public override TreeDumperNode VisitYieldBreakStatement(BoundYieldBreakStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("yieldBreakStatement", null, Array.Empty<TreeDumperNode>());
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x000A71FC File Offset: 0x000A53FC
		public override TreeDumperNode VisitThrowStatement(BoundThrowStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("throwStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expressionOpt", null, new TreeDumperNode[] { this.Visit(node.ExpressionOpt, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000A7258 File Offset: 0x000A5458
		public override TreeDumperNode VisitExpressionStatement(BoundExpressionStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("expressionStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000A72B3 File Offset: 0x000A54B3
		public override TreeDumperNode VisitBreakStatement(BoundBreakStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("breakStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000A72F3 File Offset: 0x000A54F3
		public override TreeDumperNode VisitContinueStatement(BoundContinueStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("continueStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A7334 File Offset: 0x000A5534
		public override TreeDumperNode VisitSwitchStatement(BoundSwitchStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("switchStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("innerLocals", node.InnerLocals, null),
				new TreeDumperNode("innerLocalFunctions", node.InnerLocalFunctions, null),
				new TreeDumperNode("switchSections", null, node.SwitchSections.Select<BoundSwitchSection, TreeDumperNode>((BoundSwitchSection x) => this.Visit(x, null))),
				new TreeDumperNode("decisionDag", null, new TreeDumperNode[] { this.Visit(node.DecisionDag, null) }),
				new TreeDumperNode("defaultLabel", null, new TreeDumperNode[] { this.Visit(node.DefaultLabel, null) }),
				new TreeDumperNode("breakLabel", node.BreakLabel, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000A7444 File Offset: 0x000A5644
		public override TreeDumperNode VisitSwitchDispatch(BoundSwitchDispatch node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("switchDispatch", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("cases", node.Cases, null),
				new TreeDumperNode("defaultLabel", node.DefaultLabel, null),
				new TreeDumperNode("equalityMethod", node.EqualityMethod, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000A74E0 File Offset: 0x000A56E0
		public override TreeDumperNode VisitIfStatement(BoundIfStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("ifStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("consequence", null, new TreeDumperNode[] { this.Visit(node.Consequence, null) }),
				new TreeDumperNode("alternativeOpt", null, new TreeDumperNode[] { this.Visit(node.AlternativeOpt, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000A7584 File Offset: 0x000A5784
		public override TreeDumperNode VisitDoStatement(BoundDoStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("doStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("breakLabel", node.BreakLabel, null),
				new TreeDumperNode("continueLabel", node.ContinueLabel, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000A7644 File Offset: 0x000A5844
		public override TreeDumperNode VisitWhileStatement(BoundWhileStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("whileStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("breakLabel", node.BreakLabel, null),
				new TreeDumperNode("continueLabel", node.ContinueLabel, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x000A7704 File Offset: 0x000A5904
		public override TreeDumperNode VisitForStatement(BoundForStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("forStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("outerLocals", node.OuterLocals, null),
				new TreeDumperNode("initializer", null, new TreeDumperNode[] { this.Visit(node.Initializer, null) }),
				new TreeDumperNode("innerLocals", node.InnerLocals, null),
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("increment", null, new TreeDumperNode[] { this.Visit(node.Increment, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("breakLabel", node.BreakLabel, null),
				new TreeDumperNode("continueLabel", node.ContinueLabel, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000A7828 File Offset: 0x000A5A28
		public override TreeDumperNode VisitForEachStatement(BoundForEachStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("forEachStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("enumeratorInfoOpt", node.EnumeratorInfoOpt, null),
				new TreeDumperNode("elementConversion", node.ElementConversion, null),
				new TreeDumperNode("iterationVariableType", null, new TreeDumperNode[] { this.Visit(node.IterationVariableType, null) }),
				new TreeDumperNode("iterationVariables", node.IterationVariables, null),
				new TreeDumperNode("iterationErrorExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.IterationErrorExpressionOpt, null) }),
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("deconstructionOpt", null, new TreeDumperNode[] { this.Visit(node.DeconstructionOpt, null) }),
				new TreeDumperNode("awaitOpt", null, new TreeDumperNode[] { this.Visit(node.AwaitOpt, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("@checked", node.Checked, null),
				new TreeDumperNode("breakLabel", node.BreakLabel, null),
				new TreeDumperNode("continueLabel", node.ContinueLabel, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A79C4 File Offset: 0x000A5BC4
		public override TreeDumperNode VisitForEachDeconstructStep(BoundForEachDeconstructStep node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("forEachDeconstructStep", null, new TreeDumperNode[]
			{
				new TreeDumperNode("deconstructionAssignment", null, new TreeDumperNode[] { this.Visit(node.DeconstructionAssignment, null) }),
				new TreeDumperNode("targetPlaceholder", null, new TreeDumperNode[] { this.Visit(node.TargetPlaceholder, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A7A44 File Offset: 0x000A5C44
		public override TreeDumperNode VisitUsingStatement(BoundUsingStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("usingStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("declarationsOpt", null, new TreeDumperNode[] { this.Visit(node.DeclarationsOpt, null) }),
				new TreeDumperNode("expressionOpt", null, new TreeDumperNode[] { this.Visit(node.ExpressionOpt, null) }),
				new TreeDumperNode("iDisposableConversion", node.IDisposableConversion, null),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("awaitOpt", null, new TreeDumperNode[] { this.Visit(node.AwaitOpt, null) }),
				new TreeDumperNode("patternDisposeInfoOpt", node.PatternDisposeInfoOpt, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A7B54 File Offset: 0x000A5D54
		public override TreeDumperNode VisitFixedStatement(BoundFixedStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fixedStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("declarations", null, new TreeDumperNode[] { this.Visit(node.Declarations, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000A7BEC File Offset: 0x000A5DEC
		public override TreeDumperNode VisitLockStatement(BoundLockStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("lockStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("argument", null, new TreeDumperNode[] { this.Visit(node.Argument, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x000A7C6C File Offset: 0x000A5E6C
		public override TreeDumperNode VisitTryStatement(BoundTryStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("tryStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("tryBlock", null, new TreeDumperNode[] { this.Visit(node.TryBlock, null) }),
				new TreeDumperNode("catchBlocks", null, node.CatchBlocks.Select<BoundCatchBlock, TreeDumperNode>((BoundCatchBlock x) => this.Visit(x, null))),
				new TreeDumperNode("finallyBlockOpt", null, new TreeDumperNode[] { this.Visit(node.FinallyBlockOpt, null) }),
				new TreeDumperNode("finallyLabelOpt", node.FinallyLabelOpt, null),
				new TreeDumperNode("preferFaultHandler", node.PreferFaultHandler, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x000A7D40 File Offset: 0x000A5F40
		public override TreeDumperNode VisitCatchBlock(BoundCatchBlock node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("catchBlock", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("exceptionSourceOpt", null, new TreeDumperNode[] { this.Visit(node.ExceptionSourceOpt, null) }),
				new TreeDumperNode("exceptionTypeOpt", node.ExceptionTypeOpt, null),
				new TreeDumperNode("exceptionFilterPrologueOpt", null, new TreeDumperNode[] { this.Visit(node.ExceptionFilterPrologueOpt, null) }),
				new TreeDumperNode("exceptionFilterOpt", null, new TreeDumperNode[] { this.Visit(node.ExceptionFilterOpt, null) }),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("isSynthesizedAsyncCatchAll", node.IsSynthesizedAsyncCatchAll, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000A7E50 File Offset: 0x000A6050
		public override TreeDumperNode VisitLiteral(BoundLiteral node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("literal", null, new TreeDumperNode[]
			{
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000A7EC8 File Offset: 0x000A60C8
		public override TreeDumperNode VisitThisReference(BoundThisReference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("thisReference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000A7F2C File Offset: 0x000A612C
		public override TreeDumperNode VisitPreviousSubmissionReference(BoundPreviousSubmissionReference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("previousSubmissionReference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000A7F90 File Offset: 0x000A6190
		public override TreeDumperNode VisitHostObjectMemberReference(BoundHostObjectMemberReference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("hostObjectMemberReference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000A7FF4 File Offset: 0x000A61F4
		public override TreeDumperNode VisitBaseReference(BoundBaseReference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("baseReference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000A8058 File Offset: 0x000A6258
		public override TreeDumperNode VisitLocal(BoundLocal node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("local", null, new TreeDumperNode[]
			{
				new TreeDumperNode("localSymbol", node.LocalSymbol, null),
				new TreeDumperNode("declarationKind", node.DeclarationKind, null),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("isNullableUnknown", node.IsNullableUnknown, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000A8118 File Offset: 0x000A6318
		public override TreeDumperNode VisitPseudoVariable(BoundPseudoVariable node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("pseudoVariable", null, new TreeDumperNode[]
			{
				new TreeDumperNode("localSymbol", node.LocalSymbol, null),
				new TreeDumperNode("emitExpressions", node.EmitExpressions, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000A81A4 File Offset: 0x000A63A4
		public override TreeDumperNode VisitRangeVariable(BoundRangeVariable node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("rangeVariable", null, new TreeDumperNode[]
			{
				new TreeDumperNode("rangeVariableSymbol", node.RangeVariableSymbol, null),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000A8240 File Offset: 0x000A6440
		public override TreeDumperNode VisitParameter(BoundParameter node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("parameter", null, new TreeDumperNode[]
			{
				new TreeDumperNode("parameterSymbol", node.ParameterSymbol, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000A82B8 File Offset: 0x000A64B8
		public override TreeDumperNode VisitLabelStatement(BoundLabelStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("labelStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000A82F8 File Offset: 0x000A64F8
		public override TreeDumperNode VisitGotoStatement(BoundGotoStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("gotoStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("caseExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.CaseExpressionOpt, null) }),
				new TreeDumperNode("labelExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.LabelExpressionOpt, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000A838C File Offset: 0x000A658C
		public override TreeDumperNode VisitLabeledStatement(BoundLabeledStatement node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("labeledStatement", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A83FC File Offset: 0x000A65FC
		public override TreeDumperNode VisitLabel(BoundLabel node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("label", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000A8474 File Offset: 0x000A6674
		public override TreeDumperNode VisitStatementList(BoundStatementList node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("statementList", null, new TreeDumperNode[]
			{
				new TreeDumperNode("statements", null, node.Statements.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000A84D0 File Offset: 0x000A66D0
		public override TreeDumperNode VisitConditionalGoto(BoundConditionalGoto node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("conditionalGoto", null, new TreeDumperNode[]
			{
				new TreeDumperNode("condition", null, new TreeDumperNode[] { this.Visit(node.Condition, null) }),
				new TreeDumperNode("jumpIfTrue", node.JumpIfTrue, null),
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000A8558 File Offset: 0x000A6758
		public override TreeDumperNode VisitSwitchExpressionArm(BoundSwitchExpressionArm node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("switchExpressionArm", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("pattern", null, new TreeDumperNode[] { this.Visit(node.Pattern, null) }),
				new TreeDumperNode("whenClause", null, new TreeDumperNode[] { this.Visit(node.WhenClause, null) }),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000A8628 File Offset: 0x000A6828
		public override TreeDumperNode VisitUnconvertedSwitchExpression(BoundUnconvertedSwitchExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unconvertedSwitchExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("switchArms", null, node.SwitchArms.Select<BoundSwitchExpressionArm, TreeDumperNode>((BoundSwitchExpressionArm x) => this.Visit(x, null))),
				new TreeDumperNode("decisionDag", null, new TreeDumperNode[] { this.Visit(node.DecisionDag, null) }),
				new TreeDumperNode("defaultLabel", node.DefaultLabel, null),
				new TreeDumperNode("reportedNotExhaustive", node.ReportedNotExhaustive, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000A8728 File Offset: 0x000A6928
		public override TreeDumperNode VisitConvertedSwitchExpression(BoundConvertedSwitchExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("convertedSwitchExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("naturalTypeOpt", node.NaturalTypeOpt, null),
				new TreeDumperNode("wasTargetTyped", node.WasTargetTyped, null),
				new TreeDumperNode("conversion", node.Conversion, null),
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("switchArms", null, node.SwitchArms.Select<BoundSwitchExpressionArm, TreeDumperNode>((BoundSwitchExpressionArm x) => this.Visit(x, null))),
				new TreeDumperNode("decisionDag", null, new TreeDumperNode[] { this.Visit(node.DecisionDag, null) }),
				new TreeDumperNode("defaultLabel", node.DefaultLabel, null),
				new TreeDumperNode("reportedNotExhaustive", node.ReportedNotExhaustive, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000A8870 File Offset: 0x000A6A70
		public override TreeDumperNode VisitDecisionDag(BoundDecisionDag node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("decisionDag", null, new TreeDumperNode[]
			{
				new TreeDumperNode("rootNode", null, new TreeDumperNode[] { this.Visit(node.RootNode, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000A88CC File Offset: 0x000A6ACC
		public override TreeDumperNode VisitEvaluationDecisionDagNode(BoundEvaluationDecisionDagNode node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("evaluationDecisionDagNode", null, new TreeDumperNode[]
			{
				new TreeDumperNode("evaluation", null, new TreeDumperNode[] { this.Visit(node.Evaluation, null) }),
				new TreeDumperNode("next", null, new TreeDumperNode[] { this.Visit(node.Next, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000A894C File Offset: 0x000A6B4C
		public override TreeDumperNode VisitTestDecisionDagNode(BoundTestDecisionDagNode node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("testDecisionDagNode", null, new TreeDumperNode[]
			{
				new TreeDumperNode("test", null, new TreeDumperNode[] { this.Visit(node.Test, null) }),
				new TreeDumperNode("whenTrue", null, new TreeDumperNode[] { this.Visit(node.WhenTrue, null) }),
				new TreeDumperNode("whenFalse", null, new TreeDumperNode[] { this.Visit(node.WhenFalse, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000A89F0 File Offset: 0x000A6BF0
		public override TreeDumperNode VisitWhenDecisionDagNode(BoundWhenDecisionDagNode node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("whenDecisionDagNode", null, new TreeDumperNode[]
			{
				new TreeDumperNode("bindings", node.Bindings, null),
				new TreeDumperNode("whenExpression", null, new TreeDumperNode[] { this.Visit(node.WhenExpression, null) }),
				new TreeDumperNode("whenTrue", null, new TreeDumperNode[] { this.Visit(node.WhenTrue, null) }),
				new TreeDumperNode("whenFalse", null, new TreeDumperNode[] { this.Visit(node.WhenFalse, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000A8AAC File Offset: 0x000A6CAC
		public override TreeDumperNode VisitLeafDecisionDagNode(BoundLeafDecisionDagNode node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("leafDecisionDagNode", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000A8AEC File Offset: 0x000A6CEC
		public override TreeDumperNode VisitDagTemp(BoundDagTemp node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagTemp", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("source", null, new TreeDumperNode[] { this.Visit(node.Source, null) }),
				new TreeDumperNode("index", node.Index, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000A8B74 File Offset: 0x000A6D74
		public override TreeDumperNode VisitDagTypeTest(BoundDagTypeTest node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagTypeTest", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000A8BE4 File Offset: 0x000A6DE4
		public override TreeDumperNode VisitDagNonNullTest(BoundDagNonNullTest node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagNonNullTest", null, new TreeDumperNode[]
			{
				new TreeDumperNode("isExplicitTest", node.IsExplicitTest, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000A8C58 File Offset: 0x000A6E58
		public override TreeDumperNode VisitDagExplicitNullTest(BoundDagExplicitNullTest node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagExplicitNullTest", null, new TreeDumperNode[]
			{
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000A8CB4 File Offset: 0x000A6EB4
		public override TreeDumperNode VisitDagValueTest(BoundDagValueTest node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagValueTest", null, new TreeDumperNode[]
			{
				new TreeDumperNode("value", node.Value, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000A8D24 File Offset: 0x000A6F24
		public override TreeDumperNode VisitDagRelationalTest(BoundDagRelationalTest node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagRelationalTest", null, new TreeDumperNode[]
			{
				new TreeDumperNode("operatorKind", node.OperatorKind, null),
				new TreeDumperNode("value", node.Value, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000A8DAC File Offset: 0x000A6FAC
		public override TreeDumperNode VisitDagDeconstructEvaluation(BoundDagDeconstructEvaluation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagDeconstructEvaluation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("deconstructMethod", node.DeconstructMethod, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000A8E1C File Offset: 0x000A701C
		public override TreeDumperNode VisitDagTypeEvaluation(BoundDagTypeEvaluation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagTypeEvaluation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000A8E8C File Offset: 0x000A708C
		public override TreeDumperNode VisitDagFieldEvaluation(BoundDagFieldEvaluation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagFieldEvaluation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("field", node.Field, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000A8EFC File Offset: 0x000A70FC
		public override TreeDumperNode VisitDagPropertyEvaluation(BoundDagPropertyEvaluation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagPropertyEvaluation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("property", node.Property, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000A8F6C File Offset: 0x000A716C
		public override TreeDumperNode VisitDagIndexEvaluation(BoundDagIndexEvaluation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dagIndexEvaluation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("property", node.Property, null),
				new TreeDumperNode("index", node.Index, null),
				new TreeDumperNode("input", null, new TreeDumperNode[] { this.Visit(node.Input, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000A8FF4 File Offset: 0x000A71F4
		public override TreeDumperNode VisitSwitchSection(BoundSwitchSection node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("switchSection", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("switchLabels", null, node.SwitchLabels.Select<BoundSwitchLabel, TreeDumperNode>((BoundSwitchLabel x) => this.Visit(x, null))),
				new TreeDumperNode("statements", null, node.Statements.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000A9090 File Offset: 0x000A7290
		public override TreeDumperNode VisitSwitchLabel(BoundSwitchLabel node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("switchLabel", null, new TreeDumperNode[]
			{
				new TreeDumperNode("label", node.Label, null),
				new TreeDumperNode("pattern", null, new TreeDumperNode[] { this.Visit(node.Pattern, null) }),
				new TreeDumperNode("whenClause", null, new TreeDumperNode[] { this.Visit(node.WhenClause, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000A9124 File Offset: 0x000A7324
		public override TreeDumperNode VisitSequencePointExpression(BoundSequencePointExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sequencePointExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000A91AC File Offset: 0x000A73AC
		public override TreeDumperNode VisitSequence(BoundSequence node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("sequence", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("sideEffects", null, node.SideEffects.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000A9274 File Offset: 0x000A7474
		public override TreeDumperNode VisitSpillSequence(BoundSpillSequence node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("spillSequence", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("sideEffects", null, node.SideEffects.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000A933C File Offset: 0x000A753C
		public override TreeDumperNode VisitDynamicMemberAccess(BoundDynamicMemberAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicMemberAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("typeArgumentsOpt", node.TypeArgumentsOpt, null),
				new TreeDumperNode("name", node.Name, null),
				new TreeDumperNode("invoked", node.Invoked, null),
				new TreeDumperNode("indexed", node.Indexed, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000A9424 File Offset: 0x000A7624
		public override TreeDumperNode VisitDynamicInvocation(BoundDynamicInvocation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicInvocation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("applicableMethods", node.ApplicableMethods, null),
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000A951C File Offset: 0x000A771C
		public override TreeDumperNode VisitConditionalAccess(BoundConditionalAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("conditionalAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("accessExpression", null, new TreeDumperNode[] { this.Visit(node.AccessExpression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000A95C8 File Offset: 0x000A77C8
		public override TreeDumperNode VisitLoweredConditionalAccess(BoundLoweredConditionalAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("loweredConditionalAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("hasValueMethodOpt", node.HasValueMethodOpt, null),
				new TreeDumperNode("whenNotNull", null, new TreeDumperNode[] { this.Visit(node.WhenNotNull, null) }),
				new TreeDumperNode("whenNullOpt", null, new TreeDumperNode[] { this.Visit(node.WhenNullOpt, null) }),
				new TreeDumperNode("id", node.Id, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000A96C8 File Offset: 0x000A78C8
		public override TreeDumperNode VisitConditionalReceiver(BoundConditionalReceiver node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("conditionalReceiver", null, new TreeDumperNode[]
			{
				new TreeDumperNode("id", node.Id, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000A9748 File Offset: 0x000A7948
		public override TreeDumperNode VisitComplexConditionalReceiver(BoundComplexConditionalReceiver node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("complexConditionalReceiver", null, new TreeDumperNode[]
			{
				new TreeDumperNode("valueTypeReceiver", null, new TreeDumperNode[] { this.Visit(node.ValueTypeReceiver, null) }),
				new TreeDumperNode("referenceTypeReceiver", null, new TreeDumperNode[] { this.Visit(node.ReferenceTypeReceiver, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000A97F4 File Offset: 0x000A79F4
		public override TreeDumperNode VisitMethodGroup(BoundMethodGroup node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("methodGroup", null, new TreeDumperNode[]
			{
				new TreeDumperNode("typeArgumentsOpt", node.TypeArgumentsOpt, null),
				new TreeDumperNode("name", node.Name, null),
				new TreeDumperNode("methods", node.Methods, null),
				new TreeDumperNode("lookupSymbolOpt", node.LookupSymbolOpt, null),
				new TreeDumperNode("lookupError", node.LookupError, null),
				new TreeDumperNode("flags", node.Flags, null),
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000A9920 File Offset: 0x000A7B20
		public override TreeDumperNode VisitPropertyGroup(BoundPropertyGroup node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("propertyGroup", null, new TreeDumperNode[]
			{
				new TreeDumperNode("properties", node.Properties, null),
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000A99DC File Offset: 0x000A7BDC
		public override TreeDumperNode VisitCall(BoundCall node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("call", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("method", node.Method, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("isDelegateCall", node.IsDelegateCall, null),
				new TreeDumperNode("expanded", node.Expanded, null),
				new TreeDumperNode("invokedAsExtensionMethod", node.InvokedAsExtensionMethod, null),
				new TreeDumperNode("argsToParamsOpt", node.ArgsToParamsOpt, null),
				new TreeDumperNode("defaultArguments", node.DefaultArguments, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("originalMethodsOpt", node.OriginalMethodsOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000A9B88 File Offset: 0x000A7D88
		public override TreeDumperNode VisitEventAssignmentOperator(BoundEventAssignmentOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("eventAssignmentOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("@event", node.Event, null),
				new TreeDumperNode("isAddition", node.IsAddition, null),
				new TreeDumperNode("isDynamic", node.IsDynamic, null),
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("argument", null, new TreeDumperNode[] { this.Visit(node.Argument, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000A9C7C File Offset: 0x000A7E7C
		public override TreeDumperNode VisitAttribute(BoundAttribute node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("attribute", null, new TreeDumperNode[]
			{
				new TreeDumperNode("constructor", node.Constructor, null),
				new TreeDumperNode("constructorArguments", null, node.ConstructorArguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("constructorArgumentNamesOpt", node.ConstructorArgumentNamesOpt, null),
				new TreeDumperNode("constructorArgumentsToParamsOpt", node.ConstructorArgumentsToParamsOpt, null),
				new TreeDumperNode("constructorExpanded", node.ConstructorExpanded, null),
				new TreeDumperNode("namedArguments", null, node.NamedArguments.Select<BoundAssignmentOperator, TreeDumperNode>((BoundAssignmentOperator x) => this.Visit(x, null))),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000A9DA4 File Offset: 0x000A7FA4
		public override TreeDumperNode VisitUnconvertedObjectCreationExpression(BoundUnconvertedObjectCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unconvertedObjectCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("initializerOpt", node.InitializerOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000A9E74 File Offset: 0x000A8074
		public override TreeDumperNode VisitObjectCreationExpression(BoundObjectCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("objectCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("constructor", node.Constructor, null),
				new TreeDumperNode("constructorsGroup", node.ConstructorsGroup, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("expanded", node.Expanded, null),
				new TreeDumperNode("argsToParamsOpt", node.ArgsToParamsOpt, null),
				new TreeDumperNode("defaultArguments", node.DefaultArguments, null),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("initializerExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerExpressionOpt, null) }),
				new TreeDumperNode("wasTargetTyped", node.WasTargetTyped, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000AA000 File Offset: 0x000A8200
		public override TreeDumperNode VisitTupleLiteral(BoundTupleLiteral node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("tupleLiteral", null, new TreeDumperNode[]
			{
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("inferredNamesOpt", node.InferredNamesOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000AA0BC File Offset: 0x000A82BC
		public override TreeDumperNode VisitConvertedTupleLiteral(BoundConvertedTupleLiteral node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("convertedTupleLiteral", null, new TreeDumperNode[]
			{
				new TreeDumperNode("sourceTuple", null, new TreeDumperNode[] { this.Visit(node.SourceTuple, null) }),
				new TreeDumperNode("wasTargetTyped", node.WasTargetTyped, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("inferredNamesOpt", node.InferredNamesOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000AA1B4 File Offset: 0x000A83B4
		public override TreeDumperNode VisitDynamicObjectCreationExpression(BoundDynamicObjectCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicObjectCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("name", node.Name, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("initializerExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerExpressionOpt, null) }),
				new TreeDumperNode("applicableMethods", node.ApplicableMethods, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000AA2C4 File Offset: 0x000A84C4
		public override TreeDumperNode VisitNoPiaObjectCreationExpression(BoundNoPiaObjectCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("noPiaObjectCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("guidString", node.GuidString, null),
				new TreeDumperNode("initializerExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerExpressionOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000AA360 File Offset: 0x000A8560
		public override TreeDumperNode VisitObjectInitializerExpression(BoundObjectInitializerExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("objectInitializerExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("placeholder", null, new TreeDumperNode[] { this.Visit(node.Placeholder, null) }),
				new TreeDumperNode("initializers", null, node.Initializers.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000AA410 File Offset: 0x000A8610
		public override TreeDumperNode VisitObjectInitializerMember(BoundObjectInitializerMember node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("objectInitializerMember", null, new TreeDumperNode[]
			{
				new TreeDumperNode("memberSymbol", node.MemberSymbol, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("expanded", node.Expanded, null),
				new TreeDumperNode("argsToParamsOpt", node.ArgsToParamsOpt, null),
				new TreeDumperNode("defaultArguments", node.DefaultArguments, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("receiverType", node.ReceiverType, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000AA55C File Offset: 0x000A875C
		public override TreeDumperNode VisitDynamicObjectInitializerMember(BoundDynamicObjectInitializerMember node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicObjectInitializerMember", null, new TreeDumperNode[]
			{
				new TreeDumperNode("memberName", node.MemberName, null),
				new TreeDumperNode("receiverType", node.ReceiverType, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000AA5E8 File Offset: 0x000A87E8
		public override TreeDumperNode VisitCollectionInitializerExpression(BoundCollectionInitializerExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("collectionInitializerExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("placeholder", null, new TreeDumperNode[] { this.Visit(node.Placeholder, null) }),
				new TreeDumperNode("initializers", null, node.Initializers.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000AA698 File Offset: 0x000A8898
		public override TreeDumperNode VisitCollectionElementInitializer(BoundCollectionElementInitializer node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("collectionElementInitializer", null, new TreeDumperNode[]
			{
				new TreeDumperNode("addMethod", node.AddMethod, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("implicitReceiverOpt", null, new TreeDumperNode[] { this.Visit(node.ImplicitReceiverOpt, null) }),
				new TreeDumperNode("expanded", node.Expanded, null),
				new TreeDumperNode("argsToParamsOpt", node.ArgsToParamsOpt, null),
				new TreeDumperNode("defaultArguments", node.DefaultArguments, null),
				new TreeDumperNode("invokedAsExtensionMethod", node.InvokedAsExtensionMethod, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000AA7DC File Offset: 0x000A89DC
		public override TreeDumperNode VisitDynamicCollectionElementInitializer(BoundDynamicCollectionElementInitializer node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicCollectionElementInitializer", null, new TreeDumperNode[]
			{
				new TreeDumperNode("applicableMethods", node.ApplicableMethods, null),
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000AA8A4 File Offset: 0x000A8AA4
		public override TreeDumperNode VisitImplicitReceiver(BoundImplicitReceiver node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("implicitReceiver", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000AA908 File Offset: 0x000A8B08
		public override TreeDumperNode VisitAnonymousObjectCreationExpression(BoundAnonymousObjectCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("anonymousObjectCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("constructor", node.Constructor, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("declarations", null, node.Declarations.Select<BoundAnonymousPropertyDeclaration, TreeDumperNode>((BoundAnonymousPropertyDeclaration x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000AA9CC File Offset: 0x000A8BCC
		public override TreeDumperNode VisitAnonymousPropertyDeclaration(BoundAnonymousPropertyDeclaration node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("anonymousPropertyDeclaration", null, new TreeDumperNode[]
			{
				new TreeDumperNode("property", node.Property, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000AAA44 File Offset: 0x000A8C44
		public override TreeDumperNode VisitNewT(BoundNewT node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("newT", null, new TreeDumperNode[]
			{
				new TreeDumperNode("initializerExpressionOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerExpressionOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000AAACC File Offset: 0x000A8CCC
		public override TreeDumperNode VisitDelegateCreationExpression(BoundDelegateCreationExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("delegateCreationExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("argument", null, new TreeDumperNode[] { this.Visit(node.Argument, null) }),
				new TreeDumperNode("methodOpt", node.MethodOpt, null),
				new TreeDumperNode("isExtensionMethod", node.IsExtensionMethod, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000AAB84 File Offset: 0x000A8D84
		public override TreeDumperNode VisitArrayCreation(BoundArrayCreation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("arrayCreation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("bounds", null, node.Bounds.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("initializerOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000AAC34 File Offset: 0x000A8E34
		public override TreeDumperNode VisitArrayInitialization(BoundArrayInitialization node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("arrayInitialization", null, new TreeDumperNode[]
			{
				new TreeDumperNode("initializers", null, node.Initializers.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000AACC0 File Offset: 0x000A8EC0
		public override TreeDumperNode VisitStackAllocArrayCreation(BoundStackAllocArrayCreation node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("stackAllocArrayCreation", null, new TreeDumperNode[]
			{
				new TreeDumperNode("elementType", node.ElementType, null),
				new TreeDumperNode("count", null, new TreeDumperNode[] { this.Visit(node.Count, null) }),
				new TreeDumperNode("initializerOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000AAD80 File Offset: 0x000A8F80
		public override TreeDumperNode VisitConvertedStackAllocExpression(BoundConvertedStackAllocExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("convertedStackAllocExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("elementType", node.ElementType, null),
				new TreeDumperNode("count", null, new TreeDumperNode[] { this.Visit(node.Count, null) }),
				new TreeDumperNode("initializerOpt", null, new TreeDumperNode[] { this.Visit(node.InitializerOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000AAE40 File Offset: 0x000A9040
		public override TreeDumperNode VisitFieldAccess(BoundFieldAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("fieldAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("fieldSymbol", node.FieldSymbol, null),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("isByValue", node.IsByValue, null),
				new TreeDumperNode("isDeclaration", node.IsDeclaration, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000AAF3C File Offset: 0x000A913C
		public override TreeDumperNode VisitHoistedFieldAccess(BoundHoistedFieldAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("hoistedFieldAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("fieldSymbol", node.FieldSymbol, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000AAFB4 File Offset: 0x000A91B4
		public override TreeDumperNode VisitPropertyAccess(BoundPropertyAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("propertyAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("propertySymbol", node.PropertySymbol, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000AB06C File Offset: 0x000A926C
		public override TreeDumperNode VisitEventAccess(BoundEventAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("eventAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("eventSymbol", node.EventSymbol, null),
				new TreeDumperNode("isUsableAsField", node.IsUsableAsField, null),
				new TreeDumperNode("resultKind", node.ResultKind, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000AB13C File Offset: 0x000A933C
		public override TreeDumperNode VisitIndexerAccess(BoundIndexerAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("indexerAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("indexer", node.Indexer, null),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("expanded", node.Expanded, null),
				new TreeDumperNode("argsToParamsOpt", node.ArgsToParamsOpt, null),
				new TreeDumperNode("defaultArguments", node.DefaultArguments, null),
				new TreeDumperNode("originalIndexersOpt", node.OriginalIndexersOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000AB298 File Offset: 0x000A9498
		public override TreeDumperNode VisitIndexOrRangePatternIndexerAccess(BoundIndexOrRangePatternIndexerAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("indexOrRangePatternIndexerAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("lengthOrCountProperty", node.LengthOrCountProperty, null),
				new TreeDumperNode("patternSymbol", node.PatternSymbol, null),
				new TreeDumperNode("argument", null, new TreeDumperNode[] { this.Visit(node.Argument, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000AB36C File Offset: 0x000A956C
		public override TreeDumperNode VisitDynamicIndexerAccess(BoundDynamicIndexerAccess node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("dynamicIndexerAccess", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("arguments", null, node.Arguments.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("argumentNamesOpt", node.ArgumentNamesOpt, null),
				new TreeDumperNode("argumentRefKindsOpt", node.ArgumentRefKindsOpt, null),
				new TreeDumperNode("applicableIndexers", node.ApplicableIndexers, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000AB464 File Offset: 0x000A9664
		public override TreeDumperNode VisitLambda(BoundLambda node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("lambda", null, new TreeDumperNode[]
			{
				new TreeDumperNode("unboundLambda", null, new TreeDumperNode[] { this.Visit(node.UnboundLambda, null) }),
				new TreeDumperNode("symbol", node.Symbol, null),
				new TreeDumperNode("body", null, new TreeDumperNode[] { this.Visit(node.Body, null) }),
				new TreeDumperNode("diagnostics", node.Diagnostics, null),
				new TreeDumperNode("binder", node.Binder, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000AB554 File Offset: 0x000A9754
		public override TreeDumperNode VisitUnboundLambda(UnboundLambda node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unboundLambda", null, new TreeDumperNode[]
			{
				new TreeDumperNode("data", node.Data, null),
				new TreeDumperNode("withDependencies", node.WithDependencies, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000AB5E8 File Offset: 0x000A97E8
		public override TreeDumperNode VisitQueryClause(BoundQueryClause node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("queryClause", null, new TreeDumperNode[]
			{
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("definedSymbol", node.DefinedSymbol, null),
				new TreeDumperNode("operation", null, new TreeDumperNode[] { this.Visit(node.Operation, null) }),
				new TreeDumperNode("cast", null, new TreeDumperNode[] { this.Visit(node.Cast, null) }),
				new TreeDumperNode("binder", node.Binder, null),
				new TreeDumperNode("unoptimizedForm", null, new TreeDumperNode[] { this.Visit(node.UnoptimizedForm, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000AB708 File Offset: 0x000A9908
		public override TreeDumperNode VisitTypeOrInstanceInitializers(BoundTypeOrInstanceInitializers node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("typeOrInstanceInitializers", null, new TreeDumperNode[]
			{
				new TreeDumperNode("statements", null, node.Statements.Select<BoundStatement, TreeDumperNode>((BoundStatement x) => this.Visit(x, null))),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000AB764 File Offset: 0x000A9964
		public override TreeDumperNode VisitNameOfOperator(BoundNameOfOperator node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("nameOfOperator", null, new TreeDumperNode[]
			{
				new TreeDumperNode("argument", null, new TreeDumperNode[] { this.Visit(node.Argument, null) }),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000AB800 File Offset: 0x000A9A00
		public override TreeDumperNode VisitUnconvertedInterpolatedString(BoundUnconvertedInterpolatedString node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("unconvertedInterpolatedString", null, new TreeDumperNode[]
			{
				new TreeDumperNode("parts", null, node.Parts.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000AB8A0 File Offset: 0x000A9AA0
		public override TreeDumperNode VisitInterpolatedString(BoundInterpolatedString node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("interpolatedString", null, new TreeDumperNode[]
			{
				new TreeDumperNode("parts", null, node.Parts.Select<BoundExpression, TreeDumperNode>((BoundExpression x) => this.Visit(x, null))),
				new TreeDumperNode("constantValueOpt", node.ConstantValueOpt, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000AB940 File Offset: 0x000A9B40
		public override TreeDumperNode VisitStringInsert(BoundStringInsert node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("stringInsert", null, new TreeDumperNode[]
			{
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("alignment", null, new TreeDumperNode[] { this.Visit(node.Alignment, null) }),
				new TreeDumperNode("format", null, new TreeDumperNode[] { this.Visit(node.Format, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000ABA10 File Offset: 0x000A9C10
		public override TreeDumperNode VisitIsPatternExpression(BoundIsPatternExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("isPatternExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("pattern", null, new TreeDumperNode[] { this.Visit(node.Pattern, null) }),
				new TreeDumperNode("isNegated", node.IsNegated, null),
				new TreeDumperNode("decisionDag", null, new TreeDumperNode[] { this.Visit(node.DecisionDag, null) }),
				new TreeDumperNode("whenTrueLabel", node.WhenTrueLabel, null),
				new TreeDumperNode("whenFalseLabel", node.WhenFalseLabel, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000ABB24 File Offset: 0x000A9D24
		public override TreeDumperNode VisitConstantPattern(BoundConstantPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("constantPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("constantValue", node.ConstantValue, null),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000ABBBC File Offset: 0x000A9DBC
		public override TreeDumperNode VisitDiscardPattern(BoundDiscardPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("discardPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		public override TreeDumperNode VisitDeclarationPattern(BoundDeclarationPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("declarationPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("variable", node.Variable, null),
				new TreeDumperNode("variableAccess", null, new TreeDumperNode[] { this.Visit(node.VariableAccess, null) }),
				new TreeDumperNode("declaredType", null, new TreeDumperNode[] { this.Visit(node.DeclaredType, null) }),
				new TreeDumperNode("isVar", node.IsVar, null),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000ABCF0 File Offset: 0x000A9EF0
		public override TreeDumperNode VisitRecursivePattern(BoundRecursivePattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			string text = "recursivePattern";
			object obj = null;
			TreeDumperNode[] array = new TreeDumperNode[10];
			array[0] = new TreeDumperNode("declaredType", null, new TreeDumperNode[] { this.Visit(node.DeclaredType, null) });
			array[1] = new TreeDumperNode("deconstructMethod", node.DeconstructMethod, null);
			int num = 2;
			string text2 = "deconstruction";
			object obj2 = null;
			IEnumerable<TreeDumperNode> enumerable;
			if (!node.Deconstruction.IsDefault)
			{
				enumerable = node.Deconstruction.Select<BoundSubpattern, TreeDumperNode>((BoundSubpattern x) => this.Visit(x, null));
			}
			else
			{
				IEnumerable<TreeDumperNode> enumerable2 = Array.Empty<TreeDumperNode>();
				enumerable = enumerable2;
			}
			array[num] = new TreeDumperNode(text2, obj2, enumerable);
			int num2 = 3;
			string text3 = "properties";
			object obj3 = null;
			IEnumerable<TreeDumperNode> enumerable3;
			if (!node.Properties.IsDefault)
			{
				enumerable3 = node.Properties.Select<BoundSubpattern, TreeDumperNode>((BoundSubpattern x) => this.Visit(x, null));
			}
			else
			{
				IEnumerable<TreeDumperNode> enumerable2 = Array.Empty<TreeDumperNode>();
				enumerable3 = enumerable2;
			}
			array[num2] = new TreeDumperNode(text3, obj3, enumerable3);
			array[4] = new TreeDumperNode("variable", node.Variable, null);
			array[5] = new TreeDumperNode("variableAccess", null, new TreeDumperNode[] { this.Visit(node.VariableAccess, null) });
			array[6] = new TreeDumperNode("isExplicitNotNullTest", node.IsExplicitNotNullTest, null);
			array[7] = new TreeDumperNode("inputType", node.InputType, null);
			array[8] = new TreeDumperNode("narrowedType", node.NarrowedType, null);
			array[9] = new TreeDumperNode("hasErrors", node.HasErrors, null);
			return new TreeDumperNode(text, obj, array);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000ABE58 File Offset: 0x000AA058
		public override TreeDumperNode VisitITuplePattern(BoundITuplePattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("iTuplePattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("getLengthMethod", node.GetLengthMethod, null),
				new TreeDumperNode("getItemMethod", node.GetItemMethod, null),
				new TreeDumperNode("subpatterns", null, node.Subpatterns.Select<BoundSubpattern, TreeDumperNode>((BoundSubpattern x) => this.Visit(x, null))),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000ABF04 File Offset: 0x000AA104
		public override TreeDumperNode VisitSubpattern(BoundSubpattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("subpattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("symbol", node.Symbol, null),
				new TreeDumperNode("pattern", null, new TreeDumperNode[] { this.Visit(node.Pattern, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000ABF74 File Offset: 0x000AA174
		public override TreeDumperNode VisitTypePattern(BoundTypePattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("typePattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("declaredType", null, new TreeDumperNode[] { this.Visit(node.DeclaredType, null) }),
				new TreeDumperNode("isExplicitNotNullTest", node.IsExplicitNotNullTest, null),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000AC010 File Offset: 0x000AA210
		public override TreeDumperNode VisitBinaryPattern(BoundBinaryPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("binaryPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("disjunction", node.Disjunction, null),
				new TreeDumperNode("left", null, new TreeDumperNode[] { this.Visit(node.Left, null) }),
				new TreeDumperNode("right", null, new TreeDumperNode[] { this.Visit(node.Right, null) }),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000AC0D0 File Offset: 0x000AA2D0
		public override TreeDumperNode VisitNegatedPattern(BoundNegatedPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("negatedPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("negated", null, new TreeDumperNode[] { this.Visit(node.Negated, null) }),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000AC154 File Offset: 0x000AA354
		public override TreeDumperNode VisitRelationalPattern(BoundRelationalPattern node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("relationalPattern", null, new TreeDumperNode[]
			{
				new TreeDumperNode("relation", node.Relation, null),
				new TreeDumperNode("value", null, new TreeDumperNode[] { this.Visit(node.Value, null) }),
				new TreeDumperNode("constantValue", node.ConstantValue, null),
				new TreeDumperNode("inputType", node.InputType, null),
				new TreeDumperNode("narrowedType", node.NarrowedType, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000AC204 File Offset: 0x000AA404
		public override TreeDumperNode VisitDiscardExpression(BoundDiscardExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("discardExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000AC268 File Offset: 0x000AA468
		public override TreeDumperNode VisitThrowExpression(BoundThrowExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("throwExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000AC2F0 File Offset: 0x000AA4F0
		public override TreeDumperNode VisitOutVariablePendingInference(OutVariablePendingInference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("outVariablePendingInference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("variableSymbol", node.VariableSymbol, null),
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000AC38C File Offset: 0x000AA58C
		public override TreeDumperNode VisitDeconstructionVariablePendingInference(DeconstructionVariablePendingInference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("deconstructionVariablePendingInference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("variableSymbol", node.VariableSymbol, null),
				new TreeDumperNode("receiverOpt", null, new TreeDumperNode[] { this.Visit(node.ReceiverOpt, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000AC428 File Offset: 0x000AA628
		public override TreeDumperNode VisitOutDeconstructVarPendingInference(OutDeconstructVarPendingInference node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("outDeconstructVarPendingInference", null, new TreeDumperNode[]
			{
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000AC48C File Offset: 0x000AA68C
		public override TreeDumperNode VisitNonConstructorMethodBody(BoundNonConstructorMethodBody node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("nonConstructorMethodBody", null, new TreeDumperNode[]
			{
				new TreeDumperNode("blockBody", null, new TreeDumperNode[] { this.Visit(node.BlockBody, null) }),
				new TreeDumperNode("expressionBody", null, new TreeDumperNode[] { this.Visit(node.ExpressionBody, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000AC50C File Offset: 0x000AA70C
		public override TreeDumperNode VisitConstructorMethodBody(BoundConstructorMethodBody node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("constructorMethodBody", null, new TreeDumperNode[]
			{
				new TreeDumperNode("locals", node.Locals, null),
				new TreeDumperNode("initializer", null, new TreeDumperNode[] { this.Visit(node.Initializer, null) }),
				new TreeDumperNode("blockBody", null, new TreeDumperNode[] { this.Visit(node.BlockBody, null) }),
				new TreeDumperNode("expressionBody", null, new TreeDumperNode[] { this.Visit(node.ExpressionBody, null) }),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000AC5C8 File Offset: 0x000AA7C8
		public override TreeDumperNode VisitExpressionWithNullability(BoundExpressionWithNullability node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("expressionWithNullability", null, new TreeDumperNode[]
			{
				new TreeDumperNode("expression", null, new TreeDumperNode[] { this.Visit(node.Expression, null) }),
				new TreeDumperNode("nullableAnnotation", node.NullableAnnotation, null),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000AC66C File Offset: 0x000AA86C
		public override TreeDumperNode VisitWithExpression(BoundWithExpression node, [global::System.Runtime.CompilerServices.Nullable(2)] object arg)
		{
			return new TreeDumperNode("withExpression", null, new TreeDumperNode[]
			{
				new TreeDumperNode("receiver", null, new TreeDumperNode[] { this.Visit(node.Receiver, null) }),
				new TreeDumperNode("cloneMethod", node.CloneMethod, null),
				new TreeDumperNode("initializerExpression", null, new TreeDumperNode[] { this.Visit(node.InitializerExpression, null) }),
				new TreeDumperNode("type", node.Type, null),
				new TreeDumperNode("isSuppressed", node.IsSuppressed, null),
				new TreeDumperNode("hasErrors", node.HasErrors, null)
			});
		}
	}
}
