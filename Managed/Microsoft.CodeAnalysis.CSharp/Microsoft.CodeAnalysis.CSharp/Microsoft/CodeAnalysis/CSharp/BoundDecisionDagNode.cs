using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000088 RID: 136
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundDecisionDagNode : BoundNode
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x00052538 File Offset: 0x00050738
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = this as BoundEvaluationDecisionDagNode;
			if (boundEvaluationDecisionDagNode != null)
			{
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode2 = other as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode2 != null)
				{
					return boundEvaluationDecisionDagNode.Evaluation.Equals(boundEvaluationDecisionDagNode2.Evaluation) && boundEvaluationDecisionDagNode.Next == boundEvaluationDecisionDagNode2.Next;
				}
			}
			else
			{
				BoundTestDecisionDagNode boundTestDecisionDagNode = this as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode != null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode2 = other as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode2 != null)
					{
						return boundTestDecisionDagNode.Test.Equals(boundTestDecisionDagNode2.Test) && boundTestDecisionDagNode.WhenTrue == boundTestDecisionDagNode2.WhenTrue && boundTestDecisionDagNode.WhenFalse == boundTestDecisionDagNode2.WhenFalse;
					}
				}
				else
				{
					BoundWhenDecisionDagNode boundWhenDecisionDagNode = this as BoundWhenDecisionDagNode;
					if (boundWhenDecisionDagNode != null)
					{
						BoundWhenDecisionDagNode boundWhenDecisionDagNode2 = other as BoundWhenDecisionDagNode;
						if (boundWhenDecisionDagNode2 != null)
						{
							return boundWhenDecisionDagNode.WhenExpression == boundWhenDecisionDagNode2.WhenExpression && boundWhenDecisionDagNode.WhenTrue == boundWhenDecisionDagNode2.WhenTrue && boundWhenDecisionDagNode.WhenFalse == boundWhenDecisionDagNode2.WhenFalse;
						}
					}
					else
					{
						BoundLeafDecisionDagNode boundLeafDecisionDagNode = this as BoundLeafDecisionDagNode;
						if (boundLeafDecisionDagNode != null)
						{
							BoundLeafDecisionDagNode boundLeafDecisionDagNode2 = other as BoundLeafDecisionDagNode;
							if (boundLeafDecisionDagNode2 != null)
							{
								return boundLeafDecisionDagNode.Label == boundLeafDecisionDagNode2.Label;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00052664 File Offset: 0x00050864
		public override int GetHashCode()
		{
			BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = this as BoundEvaluationDecisionDagNode;
			if (boundEvaluationDecisionDagNode != null)
			{
				return Hash.Combine(boundEvaluationDecisionDagNode.Evaluation.GetHashCode(), RuntimeHelpers.GetHashCode(boundEvaluationDecisionDagNode.Next));
			}
			BoundTestDecisionDagNode boundTestDecisionDagNode = this as BoundTestDecisionDagNode;
			if (boundTestDecisionDagNode != null)
			{
				return Hash.Combine(boundTestDecisionDagNode.Test.GetHashCode(), Hash.Combine(RuntimeHelpers.GetHashCode(boundTestDecisionDagNode.WhenFalse), RuntimeHelpers.GetHashCode(boundTestDecisionDagNode.WhenTrue)));
			}
			BoundWhenDecisionDagNode boundWhenDecisionDagNode = this as BoundWhenDecisionDagNode;
			if (boundWhenDecisionDagNode != null)
			{
				return Hash.Combine(RuntimeHelpers.GetHashCode(boundWhenDecisionDagNode.WhenExpression), Hash.Combine(RuntimeHelpers.GetHashCode(boundWhenDecisionDagNode.WhenFalse), RuntimeHelpers.GetHashCode(boundWhenDecisionDagNode.WhenTrue)));
			}
			BoundLeafDecisionDagNode boundLeafDecisionDagNode = this as BoundLeafDecisionDagNode;
			if (boundLeafDecisionDagNode == null)
			{
				throw ExceptionUtilities.UnexpectedValue(this);
			}
			return RuntimeHelpers.GetHashCode(boundLeafDecisionDagNode.Label);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0005272B File Offset: 0x0005092B
		protected BoundDecisionDagNode(BoundKind kind, SyntaxNode syntax, bool hasErrors)
			: base(kind, syntax, hasErrors)
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00052736 File Offset: 0x00050936
		protected BoundDecisionDagNode(BoundKind kind, SyntaxNode syntax)
			: base(kind, syntax)
		{
		}
	}
}
