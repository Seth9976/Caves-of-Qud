using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200026D RID: 621
	[NullableContext(1)]
	[Nullable(0)]
	public static class AnnotationExtensions
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x000462CE File Offset: 0x000444CE
		public static TNode WithAdditionalAnnotations<[Nullable(0)] TNode>(this TNode node, params SyntaxAnnotation[] annotations) where TNode : SyntaxNode
		{
			return (TNode)((object)node.WithAdditionalAnnotationsInternal(annotations));
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000462E1 File Offset: 0x000444E1
		public static TNode WithAdditionalAnnotations<[Nullable(0)] TNode>(this TNode node, IEnumerable<SyntaxAnnotation> annotations) where TNode : SyntaxNode
		{
			return (TNode)((object)node.WithAdditionalAnnotationsInternal(annotations));
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000462F4 File Offset: 0x000444F4
		public static TNode WithoutAnnotations<[Nullable(0)] TNode>(this TNode node, params SyntaxAnnotation[] annotations) where TNode : SyntaxNode
		{
			return (TNode)((object)node.GetNodeWithoutAnnotations(annotations));
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00046307 File Offset: 0x00044507
		public static TNode WithoutAnnotations<[Nullable(0)] TNode>(this TNode node, IEnumerable<SyntaxAnnotation> annotations) where TNode : SyntaxNode
		{
			return (TNode)((object)node.GetNodeWithoutAnnotations(annotations));
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004631A File Offset: 0x0004451A
		public static TNode WithoutAnnotations<[Nullable(0)] TNode>(this TNode node, string annotationKind) where TNode : SyntaxNode
		{
			if (node.HasAnnotations(annotationKind))
			{
				return node.WithoutAnnotations(Enumerable.ToArray<SyntaxAnnotation>(node.GetAnnotations(annotationKind)));
			}
			return node;
		}
	}
}
