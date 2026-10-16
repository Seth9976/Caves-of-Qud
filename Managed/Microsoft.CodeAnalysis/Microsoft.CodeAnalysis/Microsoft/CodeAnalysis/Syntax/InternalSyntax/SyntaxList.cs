using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B0 RID: 688
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SyntaxList : GreenNode
	{
		// Token: 0x06001D1A RID: 7450 RVA: 0x000552A5 File Offset: 0x000534A5
		internal SyntaxList()
			: base(1)
		{
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000552AE File Offset: 0x000534AE
		internal SyntaxList([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(1, diagnostics, annotations)
		{
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000552B9 File Offset: 0x000534B9
		internal SyntaxList(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000552C2 File Offset: 0x000534C2
		internal static GreenNode List(GreenNode child)
		{
			return child;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000552C8 File Offset: 0x000534C8
		internal static SyntaxList.WithTwoChildren List(GreenNode child0, GreenNode child1)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(1, child0, child1, out num);
			if (greenNode != null)
			{
				return (SyntaxList.WithTwoChildren)greenNode;
			}
			SyntaxList.WithTwoChildren withTwoChildren = new SyntaxList.WithTwoChildren(child0, child1);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(withTwoChildren, num);
			}
			return withTwoChildren;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00055300 File Offset: 0x00053500
		internal static SyntaxList.WithThreeChildren List(GreenNode child0, GreenNode child1, GreenNode child2)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(1, child0, child1, child2, out num);
			if (greenNode != null)
			{
				return (SyntaxList.WithThreeChildren)greenNode;
			}
			SyntaxList.WithThreeChildren withThreeChildren = new SyntaxList.WithThreeChildren(child0, child1, child2);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(withThreeChildren, num);
			}
			return withThreeChildren;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00055338 File Offset: 0x00053538
		internal static GreenNode List([Nullable(new byte[] { 1, 2 })] GreenNode[] nodes)
		{
			return SyntaxList.List(nodes, nodes.Length);
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00055344 File Offset: 0x00053544
		internal static GreenNode List([Nullable(new byte[] { 1, 2 })] GreenNode[] nodes, int count)
		{
			ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[count];
			for (int i = 0; i < count; i++)
			{
				GreenNode greenNode = nodes[i];
				array[i].Value = greenNode;
			}
			return SyntaxList.List(array);
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0005537B File Offset: 0x0005357B
		internal static SyntaxList List([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
		{
			if (children.Length < 10)
			{
				return new SyntaxList.WithManyChildren(children);
			}
			return new SyntaxList.WithLotsOfChildren(children);
		}

		// Token: 0x06001D23 RID: 7459
		internal abstract void CopyTo([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] array, int offset);

		// Token: 0x06001D24 RID: 7460 RVA: 0x00055394 File Offset: 0x00053594
		[NullableContext(2)]
		internal static GreenNode Concat(GreenNode left, GreenNode right)
		{
			if (left == null)
			{
				return right;
			}
			if (right == null)
			{
				return left;
			}
			SyntaxList syntaxList = left as SyntaxList;
			SyntaxList syntaxList2 = right as SyntaxList;
			if (syntaxList != null)
			{
				if (syntaxList2 != null)
				{
					ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[left.SlotCount + right.SlotCount];
					syntaxList.CopyTo(array, 0);
					syntaxList2.CopyTo(array, left.SlotCount);
					return SyntaxList.List(array);
				}
				ArrayElement<GreenNode>[] array2 = new ArrayElement<GreenNode>[left.SlotCount + 1];
				syntaxList.CopyTo(array2, 0);
				array2[left.SlotCount].Value = right;
				return SyntaxList.List(array2);
			}
			else
			{
				if (syntaxList2 != null)
				{
					ArrayElement<GreenNode>[] array3 = new ArrayElement<GreenNode>[syntaxList2.SlotCount + 1];
					array3[0].Value = left;
					syntaxList2.CopyTo(array3, 1);
					return SyntaxList.List(array3);
				}
				return SyntaxList.List(left, right);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00055455 File Offset: 0x00053655
		public sealed override string Language
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0005545C File Offset: 0x0005365C
		public sealed override string KindText
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00055463 File Offset: 0x00053663
		public sealed override SyntaxNode GetStructure(SyntaxTrivia parentTrivia)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x0005546A File Offset: 0x0005366A
		public sealed override SyntaxToken CreateSeparator<[Nullable(0)] TNode>(SyntaxNode element)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00055471 File Offset: 0x00053671
		public sealed override bool IsTriviaWithEndOfLine()
		{
			return false;
		}

		// Token: 0x02000658 RID: 1624
		[Nullable(0)]
		internal sealed class WithLotsOfChildren : SyntaxList.WithManyChildrenBase
		{
			// Token: 0x060038BF RID: 14527 RVA: 0x0009DA89 File Offset: 0x0009BC89
			static WithLotsOfChildren()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxList.WithLotsOfChildren), (ObjectReader r) => new SyntaxList.WithLotsOfChildren(r));
			}

			// Token: 0x060038C0 RID: 14528 RVA: 0x0009DAAA File Offset: 0x0009BCAA
			internal WithLotsOfChildren([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
				: base(children)
			{
				this._childOffsets = SyntaxList.WithLotsOfChildren.CalculateOffsets(children);
			}

			// Token: 0x060038C1 RID: 14529 RVA: 0x0009DABF File Offset: 0x0009BCBF
			internal WithLotsOfChildren([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, [Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children, int[] childOffsets)
				: base(diagnostics, annotations, children)
			{
				this._childOffsets = childOffsets;
			}

			// Token: 0x060038C2 RID: 14530 RVA: 0x0009DAD2 File Offset: 0x0009BCD2
			internal WithLotsOfChildren(ObjectReader reader)
				: base(reader)
			{
				this._childOffsets = SyntaxList.WithLotsOfChildren.CalculateOffsets(this.children);
			}

			// Token: 0x060038C3 RID: 14531 RVA: 0x0009DAEC File Offset: 0x0009BCEC
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
			}

			// Token: 0x060038C4 RID: 14532 RVA: 0x0009DAF5 File Offset: 0x0009BCF5
			public override int GetSlotOffset(int index)
			{
				return this._childOffsets[index];
			}

			// Token: 0x060038C5 RID: 14533 RVA: 0x0009DAFF File Offset: 0x0009BCFF
			public override int FindSlotIndexContainingOffset(int offset)
			{
				return this._childOffsets.BinarySearchUpperBound(offset) - 1;
			}

			// Token: 0x060038C6 RID: 14534 RVA: 0x0009DB10 File Offset: 0x0009BD10
			private static int[] CalculateOffsets([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
			{
				int num = children.Length;
				int[] array = new int[num];
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					array[i] = num2;
					num2 += children[i].Value.FullWidth;
				}
				return array;
			}

			// Token: 0x060038C7 RID: 14535 RVA: 0x0009DB4F File Offset: 0x0009BD4F
			internal override GreenNode SetDiagnostics([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] errors)
			{
				return new SyntaxList.WithLotsOfChildren(errors, base.GetAnnotations(), this.children, this._childOffsets);
			}

			// Token: 0x060038C8 RID: 14536 RVA: 0x0009DB69 File Offset: 0x0009BD69
			internal override GreenNode SetAnnotations([Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			{
				return new SyntaxList.WithLotsOfChildren(base.GetDiagnostics(), annotations, this.children, this._childOffsets);
			}

			// Token: 0x0400186B RID: 6251
			private readonly int[] _childOffsets;
		}

		// Token: 0x02000659 RID: 1625
		[Nullable(0)]
		internal abstract class WithManyChildrenBase : SyntaxList
		{
			// Token: 0x060038C9 RID: 14537 RVA: 0x0009DB83 File Offset: 0x0009BD83
			internal WithManyChildrenBase([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
			{
				this.children = children;
				this.InitializeChildren();
			}

			// Token: 0x060038CA RID: 14538 RVA: 0x0009DB98 File Offset: 0x0009BD98
			internal WithManyChildrenBase([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, [Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
				: base(diagnostics, annotations)
			{
				this.children = children;
				this.InitializeChildren();
			}

			// Token: 0x060038CB RID: 14539 RVA: 0x0009DBB0 File Offset: 0x0009BDB0
			private void InitializeChildren()
			{
				int num = this.children.Length;
				if (num < 255)
				{
					base.SlotCount = (int)((byte)num);
				}
				else
				{
					base.SlotCount = 255;
				}
				for (int i = 0; i < this.children.Length; i++)
				{
					base.AdjustFlagsAndWidth(this.children[i]);
				}
			}

			// Token: 0x060038CC RID: 14540 RVA: 0x0009DC10 File Offset: 0x0009BE10
			internal WithManyChildrenBase(ObjectReader reader)
				: base(reader)
			{
				int num = reader.ReadInt32();
				this.children = new ArrayElement<GreenNode>[num];
				for (int i = 0; i < num; i++)
				{
					this.children[i].Value = (GreenNode)reader.ReadValue();
				}
				this.InitializeChildren();
			}

			// Token: 0x060038CD RID: 14541 RVA: 0x0009DC68 File Offset: 0x0009BE68
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteInt32(this.children.Length);
				for (int i = 0; i < this.children.Length; i++)
				{
					writer.WriteValue(this.children[i].Value);
				}
			}

			// Token: 0x060038CE RID: 14542 RVA: 0x0009DCB4 File Offset: 0x0009BEB4
			protected override int GetSlotCount()
			{
				return this.children.Length;
			}

			// Token: 0x060038CF RID: 14543 RVA: 0x0009DCBE File Offset: 0x0009BEBE
			internal override GreenNode GetSlot(int index)
			{
				return this.children[index];
			}

			// Token: 0x060038D0 RID: 14544 RVA: 0x0009DCD1 File Offset: 0x0009BED1
			internal override void CopyTo([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] array, int offset)
			{
				Array.Copy(this.children, 0, array, offset, this.children.Length);
			}

			// Token: 0x060038D1 RID: 14545 RVA: 0x0009DCEC File Offset: 0x0009BEEC
			internal override SyntaxNode CreateRed([Nullable(2)] SyntaxNode parent, int position)
			{
				bool flag = base.SlotCount > 1 && this.HasNodeTokenPattern();
				if (parent != null && parent.ShouldCreateWeakList())
				{
					if (!flag)
					{
						return new SyntaxList.WithManyWeakChildren(this, parent, position);
					}
					return new SyntaxList.SeparatedWithManyWeakChildren(this, parent, position);
				}
				else
				{
					if (!flag)
					{
						return new SyntaxList.WithManyChildren(this, parent, position);
					}
					return new SyntaxList.SeparatedWithManyChildren(this, parent, position);
				}
			}

			// Token: 0x060038D2 RID: 14546 RVA: 0x0009DD40 File Offset: 0x0009BF40
			private bool HasNodeTokenPattern()
			{
				for (int i = 0; i < base.SlotCount; i++)
				{
					if (this.GetSlot(i).IsToken == ((i & 1) == 0))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0400186C RID: 6252
			[Nullable(new byte[] { 1, 0, 1 })]
			internal readonly ArrayElement<GreenNode>[] children;
		}

		// Token: 0x0200065A RID: 1626
		[Nullable(0)]
		internal sealed class WithManyChildren : SyntaxList.WithManyChildrenBase
		{
			// Token: 0x060038D3 RID: 14547 RVA: 0x0009DD75 File Offset: 0x0009BF75
			static WithManyChildren()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxList.WithManyChildren), (ObjectReader r) => new SyntaxList.WithManyChildren(r));
			}

			// Token: 0x060038D4 RID: 14548 RVA: 0x0009DD96 File Offset: 0x0009BF96
			internal WithManyChildren([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
				: base(children)
			{
			}

			// Token: 0x060038D5 RID: 14549 RVA: 0x0009DD9F File Offset: 0x0009BF9F
			internal WithManyChildren([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, [Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] children)
				: base(diagnostics, annotations, children)
			{
			}

			// Token: 0x060038D6 RID: 14550 RVA: 0x0009DDAA File Offset: 0x0009BFAA
			internal WithManyChildren(ObjectReader reader)
				: base(reader)
			{
			}

			// Token: 0x060038D7 RID: 14551 RVA: 0x0009DDB3 File Offset: 0x0009BFB3
			internal override GreenNode SetDiagnostics([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] errors)
			{
				return new SyntaxList.WithManyChildren(errors, base.GetAnnotations(), this.children);
			}

			// Token: 0x060038D8 RID: 14552 RVA: 0x0009DDC7 File Offset: 0x0009BFC7
			internal override GreenNode SetAnnotations([Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			{
				return new SyntaxList.WithManyChildren(base.GetDiagnostics(), annotations, this.children);
			}
		}

		// Token: 0x0200065B RID: 1627
		[Nullable(0)]
		internal class WithThreeChildren : SyntaxList
		{
			// Token: 0x060038D9 RID: 14553 RVA: 0x0009DDDB File Offset: 0x0009BFDB
			static WithThreeChildren()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxList.WithThreeChildren), (ObjectReader r) => new SyntaxList.WithThreeChildren(r));
			}

			// Token: 0x060038DA RID: 14554 RVA: 0x0009DDFC File Offset: 0x0009BFFC
			internal WithThreeChildren(GreenNode child0, GreenNode child1, GreenNode child2)
			{
				base.SlotCount = 3;
				base.AdjustFlagsAndWidth(child0);
				this._child0 = child0;
				base.AdjustFlagsAndWidth(child1);
				this._child1 = child1;
				base.AdjustFlagsAndWidth(child2);
				this._child2 = child2;
			}

			// Token: 0x060038DB RID: 14555 RVA: 0x0009DE35 File Offset: 0x0009C035
			internal WithThreeChildren([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, GreenNode child0, GreenNode child1, GreenNode child2)
				: base(diagnostics, annotations)
			{
				base.SlotCount = 3;
				base.AdjustFlagsAndWidth(child0);
				this._child0 = child0;
				base.AdjustFlagsAndWidth(child1);
				this._child1 = child1;
				base.AdjustFlagsAndWidth(child2);
				this._child2 = child2;
			}

			// Token: 0x060038DC RID: 14556 RVA: 0x0009DE74 File Offset: 0x0009C074
			internal WithThreeChildren(ObjectReader reader)
				: base(reader)
			{
				base.SlotCount = 3;
				this._child0 = (GreenNode)reader.ReadValue();
				base.AdjustFlagsAndWidth(this._child0);
				this._child1 = (GreenNode)reader.ReadValue();
				base.AdjustFlagsAndWidth(this._child1);
				this._child2 = (GreenNode)reader.ReadValue();
				base.AdjustFlagsAndWidth(this._child2);
			}

			// Token: 0x060038DD RID: 14557 RVA: 0x0009DEE6 File Offset: 0x0009C0E6
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this._child0);
				writer.WriteValue(this._child1);
				writer.WriteValue(this._child2);
			}

			// Token: 0x060038DE RID: 14558 RVA: 0x0009DF13 File Offset: 0x0009C113
			[NullableContext(2)]
			internal override GreenNode GetSlot(int index)
			{
				switch (index)
				{
				case 0:
					return this._child0;
				case 1:
					return this._child1;
				case 2:
					return this._child2;
				default:
					return null;
				}
			}

			// Token: 0x060038DF RID: 14559 RVA: 0x0009DF3F File Offset: 0x0009C13F
			internal override void CopyTo([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] array, int offset)
			{
				array[offset].Value = this._child0;
				array[offset + 1].Value = this._child1;
				array[offset + 2].Value = this._child2;
			}

			// Token: 0x060038E0 RID: 14560 RVA: 0x0009DF7B File Offset: 0x0009C17B
			internal override SyntaxNode CreateRed([Nullable(2)] SyntaxNode parent, int position)
			{
				return new SyntaxList.WithThreeChildren(this, parent, position);
			}

			// Token: 0x060038E1 RID: 14561 RVA: 0x0009DF85 File Offset: 0x0009C185
			internal override GreenNode SetDiagnostics([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] errors)
			{
				return new SyntaxList.WithThreeChildren(errors, base.GetAnnotations(), this._child0, this._child1, this._child2);
			}

			// Token: 0x060038E2 RID: 14562 RVA: 0x0009DFA5 File Offset: 0x0009C1A5
			internal override GreenNode SetAnnotations([Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			{
				return new SyntaxList.WithThreeChildren(base.GetDiagnostics(), annotations, this._child0, this._child1, this._child2);
			}

			// Token: 0x0400186D RID: 6253
			private readonly GreenNode _child0;

			// Token: 0x0400186E RID: 6254
			private readonly GreenNode _child1;

			// Token: 0x0400186F RID: 6255
			private readonly GreenNode _child2;
		}

		// Token: 0x0200065C RID: 1628
		[Nullable(0)]
		internal class WithTwoChildren : SyntaxList
		{
			// Token: 0x060038E3 RID: 14563 RVA: 0x0009DFC5 File Offset: 0x0009C1C5
			static WithTwoChildren()
			{
				ObjectBinder.RegisterTypeReader(typeof(SyntaxList.WithTwoChildren), (ObjectReader r) => new SyntaxList.WithTwoChildren(r));
			}

			// Token: 0x060038E4 RID: 14564 RVA: 0x0009DFE6 File Offset: 0x0009C1E6
			internal WithTwoChildren(GreenNode child0, GreenNode child1)
			{
				base.SlotCount = 2;
				base.AdjustFlagsAndWidth(child0);
				this._child0 = child0;
				base.AdjustFlagsAndWidth(child1);
				this._child1 = child1;
			}

			// Token: 0x060038E5 RID: 14565 RVA: 0x0009E011 File Offset: 0x0009C211
			internal WithTwoChildren([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, GreenNode child0, GreenNode child1)
				: base(diagnostics, annotations)
			{
				base.SlotCount = 2;
				base.AdjustFlagsAndWidth(child0);
				this._child0 = child0;
				base.AdjustFlagsAndWidth(child1);
				this._child1 = child1;
			}

			// Token: 0x060038E6 RID: 14566 RVA: 0x0009E040 File Offset: 0x0009C240
			internal WithTwoChildren(ObjectReader reader)
				: base(reader)
			{
				base.SlotCount = 2;
				this._child0 = (GreenNode)reader.ReadValue();
				base.AdjustFlagsAndWidth(this._child0);
				this._child1 = (GreenNode)reader.ReadValue();
				base.AdjustFlagsAndWidth(this._child1);
			}

			// Token: 0x060038E7 RID: 14567 RVA: 0x0009E095 File Offset: 0x0009C295
			internal override void WriteTo(ObjectWriter writer)
			{
				base.WriteTo(writer);
				writer.WriteValue(this._child0);
				writer.WriteValue(this._child1);
			}

			// Token: 0x060038E8 RID: 14568 RVA: 0x0009E0B6 File Offset: 0x0009C2B6
			[NullableContext(2)]
			internal override GreenNode GetSlot(int index)
			{
				if (index == 0)
				{
					return this._child0;
				}
				if (index != 1)
				{
					return null;
				}
				return this._child1;
			}

			// Token: 0x060038E9 RID: 14569 RVA: 0x0009E0D0 File Offset: 0x0009C2D0
			internal override void CopyTo([Nullable(new byte[] { 1, 0, 1 })] ArrayElement<GreenNode>[] array, int offset)
			{
				array[offset].Value = this._child0;
				array[offset + 1].Value = this._child1;
			}

			// Token: 0x060038EA RID: 14570 RVA: 0x0009E0F8 File Offset: 0x0009C2F8
			internal override SyntaxNode CreateRed([Nullable(2)] SyntaxNode parent, int position)
			{
				return new SyntaxList.WithTwoChildren(this, parent, position);
			}

			// Token: 0x060038EB RID: 14571 RVA: 0x0009E102 File Offset: 0x0009C302
			internal override GreenNode SetDiagnostics([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] errors)
			{
				return new SyntaxList.WithTwoChildren(errors, base.GetAnnotations(), this._child0, this._child1);
			}

			// Token: 0x060038EC RID: 14572 RVA: 0x0009E11C File Offset: 0x0009C31C
			internal override GreenNode SetAnnotations([Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			{
				return new SyntaxList.WithTwoChildren(base.GetDiagnostics(), annotations, this._child0, this._child1);
			}

			// Token: 0x04001870 RID: 6256
			private readonly GreenNode _child0;

			// Token: 0x04001871 RID: 6257
			private readonly GreenNode _child1;
		}
	}
}
