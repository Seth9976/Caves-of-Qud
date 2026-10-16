using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000270 RID: 624
	[NullableContext(1)]
	[Nullable(0)]
	internal static class GreenNodeExtensions
	{
		// Token: 0x060017FC RID: 6140 RVA: 0x00047508 File Offset: 0x00045708
		public static TNode WithAnnotationsGreen<[Nullable(0)] TNode>(this TNode node, IEnumerable<SyntaxAnnotation> annotations) where TNode : GreenNode
		{
			ArrayBuilder<SyntaxAnnotation> instance = ArrayBuilder<SyntaxAnnotation>.GetInstance();
			foreach (SyntaxAnnotation syntaxAnnotation in annotations)
			{
				if (!instance.Contains(syntaxAnnotation))
				{
					instance.Add(syntaxAnnotation);
				}
			}
			if (instance.Count != 0)
			{
				return (TNode)((object)node.SetAnnotations(instance.ToArrayAndFree()));
			}
			instance.Free();
			SyntaxAnnotation[] annotations2 = node.GetAnnotations();
			if (annotations2 == null || annotations2.Length == 0)
			{
				return node;
			}
			return (TNode)((object)node.SetAnnotations(null));
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000475AC File Offset: 0x000457AC
		public static TNode WithAdditionalAnnotationsGreen<[Nullable(0)] TNode>(this TNode node, [Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxAnnotation> annotations) where TNode : GreenNode
		{
			SyntaxAnnotation[] annotations2 = node.GetAnnotations();
			if (annotations == null)
			{
				return node;
			}
			ArrayBuilder<SyntaxAnnotation> instance = ArrayBuilder<SyntaxAnnotation>.GetInstance();
			instance.AddRange(annotations2);
			foreach (SyntaxAnnotation syntaxAnnotation in annotations)
			{
				if (!instance.Contains(syntaxAnnotation))
				{
					instance.Add(syntaxAnnotation);
				}
			}
			if (instance.Count == annotations2.Length)
			{
				instance.Free();
				return node;
			}
			return (TNode)((object)node.SetAnnotations(instance.ToArrayAndFree()));
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00047644 File Offset: 0x00045844
		public static TNode WithoutAnnotationsGreen<[Nullable(0)] TNode>(this TNode node, [Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxAnnotation> annotations) where TNode : GreenNode
		{
			SyntaxAnnotation[] annotations2 = node.GetAnnotations();
			if (annotations == null || annotations2.Length == 0)
			{
				return node;
			}
			ArrayBuilder<SyntaxAnnotation> instance = ArrayBuilder<SyntaxAnnotation>.GetInstance();
			instance.AddRange(annotations);
			TNode tnode;
			try
			{
				if (instance.Count == 0)
				{
					tnode = node;
				}
				else
				{
					ArrayBuilder<SyntaxAnnotation> instance2 = ArrayBuilder<SyntaxAnnotation>.GetInstance();
					foreach (SyntaxAnnotation syntaxAnnotation in annotations2)
					{
						if (!instance.Contains(syntaxAnnotation))
						{
							instance2.Add(syntaxAnnotation);
						}
					}
					tnode = (TNode)((object)node.SetAnnotations(instance2.ToArrayAndFree()));
				}
			}
			finally
			{
				instance.Free();
			}
			return tnode;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000476E8 File Offset: 0x000458E8
		public static TNode WithDiagnosticsGreen<[Nullable(0)] TNode>(this TNode node, [Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics) where TNode : GreenNode
		{
			return (TNode)((object)node.SetDiagnostics(diagnostics));
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000476FC File Offset: 0x000458FC
		public static TNode WithoutDiagnosticsGreen<[Nullable(0)] TNode>(this TNode node) where TNode : GreenNode
		{
			DiagnosticInfo[] diagnostics = node.GetDiagnostics();
			if (diagnostics == null || diagnostics.Length == 0)
			{
				return node;
			}
			return (TNode)((object)node.SetDiagnostics(null));
		}
	}
}
