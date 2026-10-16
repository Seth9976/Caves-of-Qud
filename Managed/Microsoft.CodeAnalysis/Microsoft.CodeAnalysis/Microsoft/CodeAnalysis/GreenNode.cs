using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200026F RID: 623
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal abstract class GreenNode : IObjectWritable
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x000466EB File Offset: 0x000448EB
		private string GetDebuggerDisplay()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.KindText,
				" ",
				this.ToString()
			});
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00046728 File Offset: 0x00044928
		protected GreenNode(ushort kind)
		{
			this._kind = kind;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00046737 File Offset: 0x00044937
		protected GreenNode(ushort kind, int fullWidth)
		{
			this._kind = kind;
			this._fullWidth = fullWidth;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0004674D File Offset: 0x0004494D
		protected GreenNode(ushort kind, [Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, int fullWidth)
		{
			this._kind = kind;
			this._fullWidth = fullWidth;
			if (diagnostics != null && diagnostics.Length != 0)
			{
				this.flags |= GreenNode.NodeFlags.ContainsDiagnostics;
				GreenNode.s_diagnosticsTable.Add(this, diagnostics);
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00046784 File Offset: 0x00044984
		protected GreenNode(ushort kind, [Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			this._kind = kind;
			if (diagnostics != null && diagnostics.Length != 0)
			{
				this.flags |= GreenNode.NodeFlags.ContainsDiagnostics;
				GreenNode.s_diagnosticsTable.Add(this, diagnostics);
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000467B4 File Offset: 0x000449B4
		protected GreenNode(ushort kind, [Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: this(kind, diagnostics)
		{
			if (annotations != null && annotations.Length != 0)
			{
				for (int i = 0; i < annotations.Length; i++)
				{
					if (annotations[i] == null)
					{
						throw new ArgumentException("", "annotations");
					}
				}
				this.flags |= GreenNode.NodeFlags.ContainsAnnotations;
				GreenNode.s_annotationsTable.Add(this, annotations);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00046818 File Offset: 0x00044A18
		protected GreenNode(ushort kind, [Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations, int fullWidth)
			: this(kind, diagnostics, fullWidth)
		{
			if (annotations != null && annotations.Length != 0)
			{
				for (int i = 0; i < annotations.Length; i++)
				{
					if (annotations[i] == null)
					{
						throw new ArgumentException("", "annotations");
					}
				}
				this.flags |= GreenNode.NodeFlags.ContainsAnnotations;
				GreenNode.s_annotationsTable.Add(this, annotations);
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004687C File Offset: 0x00044A7C
		protected void AdjustFlagsAndWidth(GreenNode node)
		{
			this.flags |= node.flags & GreenNode.NodeFlags.InheritMask;
			this._fullWidth += node._fullWidth;
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060017A2 RID: 6050
		public abstract string Language { get; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000468A7 File Offset: 0x00044AA7
		public int RawKind
		{
			get
			{
				return (int)this._kind;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000468AF File Offset: 0x00044AAF
		public bool IsList
		{
			get
			{
				return this.RawKind == 1;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060017A5 RID: 6053
		public abstract string KindText { get; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000468BA File Offset: 0x00044ABA
		public virtual bool IsStructuredTrivia
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000468BD File Offset: 0x00044ABD
		public virtual bool IsDirective
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000468C0 File Offset: 0x00044AC0
		public virtual bool IsToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000468C3 File Offset: 0x00044AC3
		public virtual bool IsTrivia
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x000468C6 File Offset: 0x00044AC6
		public virtual bool IsSkippedTokensTrivia
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000468C9 File Offset: 0x00044AC9
		public virtual bool IsDocumentationCommentTrivia
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000468CC File Offset: 0x00044ACC
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x000468F0 File Offset: 0x00044AF0
		public int SlotCount
		{
			get
			{
				int num = (int)this._slotCount;
				if (num == 255)
				{
					num = this.GetSlotCount();
				}
				return num;
			}
			protected set
			{
				this._slotCount = (byte)value;
			}
		}

		// Token: 0x060017AE RID: 6062
		[NullableContext(2)]
		internal abstract GreenNode GetSlot(int index);

		// Token: 0x060017AF RID: 6063 RVA: 0x000468FA File Offset: 0x00044AFA
		internal GreenNode GetRequiredSlot(int index)
		{
			return this.GetSlot(index);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00046903 File Offset: 0x00044B03
		protected virtual int GetSlotCount()
		{
			return (int)this._slotCount;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0004690C File Offset: 0x00044B0C
		public virtual int GetSlotOffset(int index)
		{
			int num = 0;
			for (int i = 0; i < index; i++)
			{
				GreenNode slot = this.GetSlot(i);
				if (slot != null)
				{
					num += slot.FullWidth;
				}
			}
			return num;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0004693C File Offset: 0x00044B3C
		internal ChildSyntaxList ChildNodesAndTokens()
		{
			return new ChildSyntaxList(this);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00046944 File Offset: 0x00044B44
		internal IEnumerable<GreenNode> EnumerateNodes()
		{
			yield return this;
			Stack<ChildSyntaxList.Enumerator> stack = new Stack<ChildSyntaxList.Enumerator>(24);
			stack.Push(this.ChildNodesAndTokens().GetEnumerator());
			while (stack.Count > 0)
			{
				ChildSyntaxList.Enumerator enumerator = stack.Pop();
				if (enumerator.MoveNext())
				{
					GreenNode current = enumerator.Current;
					stack.Push(enumerator);
					yield return current;
					if (!current.IsToken)
					{
						stack.Push(current.ChildNodesAndTokens().GetEnumerator());
					}
					else
					{
						current = null;
					}
				}
			}
			yield break;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00046954 File Offset: 0x00044B54
		public virtual int FindSlotIndexContainingOffset(int offset)
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				GreenNode slot = this.GetSlot(num2);
				if (slot != null)
				{
					num += slot.FullWidth;
					if (offset < num)
					{
						break;
					}
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00046984 File Offset: 0x00044B84
		internal GreenNode.NodeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0004698C File Offset: 0x00044B8C
		internal void SetFlags(GreenNode.NodeFlags flags)
		{
			this.flags |= flags;
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004699C File Offset: 0x00044B9C
		internal void ClearFlags(GreenNode.NodeFlags flags)
		{
			this.flags &= ~flags;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000469AE File Offset: 0x00044BAE
		internal bool IsMissing
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.IsNotMissing) == GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000469BC File Offset: 0x00044BBC
		internal bool ParsedInAsync
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.FactoryContextIsInAsync) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000469CA File Offset: 0x00044BCA
		internal bool ParsedInQuery
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.FactoryContextIsInQuery) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000469DB File Offset: 0x00044BDB
		internal bool ParsedInIterator
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.FactoryContextIsInQuery) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000469EC File Offset: 0x00044BEC
		public bool ContainsSkippedText
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.ContainsSkippedText) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000469F9 File Offset: 0x00044BF9
		public bool ContainsStructuredTrivia
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.ContainsStructuredTrivia) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x00046A06 File Offset: 0x00044C06
		public bool ContainsDirectives
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.ContainsDirectives) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00046A13 File Offset: 0x00044C13
		public bool ContainsDiagnostics
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.ContainsDiagnostics) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00046A20 File Offset: 0x00044C20
		public bool ContainsAnnotations
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.ContainsAnnotations) > GreenNode.NodeFlags.None;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00046A2E File Offset: 0x00044C2E
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x00046A36 File Offset: 0x00044C36
		public int FullWidth
		{
			get
			{
				return this._fullWidth;
			}
			protected set
			{
				this._fullWidth = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00046A3F File Offset: 0x00044C3F
		public virtual int Width
		{
			get
			{
				return this._fullWidth - this.GetLeadingTriviaWidth() - this.GetTrailingTriviaWidth();
			}
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00046A55 File Offset: 0x00044C55
		public virtual int GetLeadingTriviaWidth()
		{
			if (this.FullWidth == 0)
			{
				return 0;
			}
			return this.GetFirstTerminal().GetLeadingTriviaWidth();
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00046A6C File Offset: 0x00044C6C
		public virtual int GetTrailingTriviaWidth()
		{
			if (this.FullWidth == 0)
			{
				return 0;
			}
			return this.GetLastTerminal().GetTrailingTriviaWidth();
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00046A83 File Offset: 0x00044C83
		public bool HasLeadingTrivia
		{
			get
			{
				return this.GetLeadingTriviaWidth() != 0;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00046A8E File Offset: 0x00044C8E
		public bool HasTrailingTrivia
		{
			get
			{
				return this.GetTrailingTriviaWidth() != 0;
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00046A9C File Offset: 0x00044C9C
		internal GreenNode(ObjectReader reader)
		{
			ushort num = reader.ReadUInt16();
			this._kind = (ushort)((int)num & -32769);
			if ((num & 32768) != 0)
			{
				DiagnosticInfo[] array = (DiagnosticInfo[])reader.ReadValue();
				if (array != null && array.Length != 0)
				{
					this.flags |= GreenNode.NodeFlags.ContainsDiagnostics;
					GreenNode.s_diagnosticsTable.Add(this, array);
				}
				SyntaxAnnotation[] array2 = (SyntaxAnnotation[])reader.ReadValue();
				if (array2 != null && array2.Length != 0)
				{
					this.flags |= GreenNode.NodeFlags.ContainsAnnotations;
					GreenNode.s_annotationsTable.Add(this, array2);
				}
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x00046B28 File Offset: 0x00044D28
		bool IObjectWritable.ShouldReuseInSerialization
		{
			get
			{
				return this.ShouldReuseInSerialization;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00046B30 File Offset: 0x00044D30
		internal virtual bool ShouldReuseInSerialization
		{
			get
			{
				return this.IsCacheable;
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00046B38 File Offset: 0x00044D38
		void IObjectWritable.WriteTo(ObjectWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00046B44 File Offset: 0x00044D44
		internal virtual void WriteTo(ObjectWriter writer)
		{
			ushort num = this._kind;
			bool flag = this.GetDiagnostics().Length != 0;
			bool flag2 = this.GetAnnotations().Length != 0;
			if (flag || flag2)
			{
				num |= 32768;
				writer.WriteUInt16(num);
				writer.WriteValue(flag ? this.GetDiagnostics() : null);
				writer.WriteValue(flag2 ? this.GetAnnotations() : null);
				return;
			}
			writer.WriteUInt16(num);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00046BB0 File Offset: 0x00044DB0
		public bool HasAnnotations(string annotationKind)
		{
			SyntaxAnnotation[] annotations = this.GetAnnotations();
			if (annotations == GreenNode.s_noAnnotations)
			{
				return false;
			}
			SyntaxAnnotation[] array = annotations;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Kind == annotationKind)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00046BF4 File Offset: 0x00044DF4
		public bool HasAnnotations(IEnumerable<string> annotationKinds)
		{
			SyntaxAnnotation[] annotations = this.GetAnnotations();
			if (annotations == GreenNode.s_noAnnotations)
			{
				return false;
			}
			foreach (SyntaxAnnotation syntaxAnnotation in annotations)
			{
				if (annotationKinds.Contains(syntaxAnnotation.Kind))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00046C38 File Offset: 0x00044E38
		[NullableContext(2)]
		public bool HasAnnotation([NotNullWhen(true)] SyntaxAnnotation annotation)
		{
			SyntaxAnnotation[] annotations = this.GetAnnotations();
			if (annotations == GreenNode.s_noAnnotations)
			{
				return false;
			}
			SyntaxAnnotation[] array = annotations;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == annotation)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00046C74 File Offset: 0x00044E74
		public IEnumerable<SyntaxAnnotation> GetAnnotations(string annotationKind)
		{
			if (string.IsNullOrWhiteSpace(annotationKind))
			{
				throw new ArgumentNullException("annotationKind");
			}
			SyntaxAnnotation[] annotations = this.GetAnnotations();
			if (annotations == GreenNode.s_noAnnotations)
			{
				return GreenNode.s_noAnnotationsEnumerable;
			}
			return GreenNode.GetAnnotationsSlow(annotations, annotationKind);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00046CB0 File Offset: 0x00044EB0
		private static IEnumerable<SyntaxAnnotation> GetAnnotationsSlow(SyntaxAnnotation[] annotations, string annotationKind)
		{
			foreach (SyntaxAnnotation syntaxAnnotation in annotations)
			{
				if (syntaxAnnotation.Kind == annotationKind)
				{
					yield return syntaxAnnotation;
				}
			}
			SyntaxAnnotation[] array = null;
			yield break;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00046CC8 File Offset: 0x00044EC8
		public IEnumerable<SyntaxAnnotation> GetAnnotations(IEnumerable<string> annotationKinds)
		{
			if (annotationKinds == null)
			{
				throw new ArgumentNullException("annotationKinds");
			}
			SyntaxAnnotation[] annotations = this.GetAnnotations();
			if (annotations == GreenNode.s_noAnnotations)
			{
				return GreenNode.s_noAnnotationsEnumerable;
			}
			return GreenNode.GetAnnotationsSlow(annotations, annotationKinds);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00046CFF File Offset: 0x00044EFF
		private static IEnumerable<SyntaxAnnotation> GetAnnotationsSlow(SyntaxAnnotation[] annotations, IEnumerable<string> annotationKinds)
		{
			foreach (SyntaxAnnotation syntaxAnnotation in annotations)
			{
				if (annotationKinds.Contains(syntaxAnnotation.Kind))
				{
					yield return syntaxAnnotation;
				}
			}
			SyntaxAnnotation[] array = null;
			yield break;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00046D18 File Offset: 0x00044F18
		public SyntaxAnnotation[] GetAnnotations()
		{
			SyntaxAnnotation[] array;
			if (this.ContainsAnnotations && GreenNode.s_annotationsTable.TryGetValue(this, ref array))
			{
				return array;
			}
			return GreenNode.s_noAnnotations;
		}

		// Token: 0x060017D5 RID: 6101
		internal abstract GreenNode SetAnnotations([Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations);

		// Token: 0x060017D6 RID: 6102 RVA: 0x00046D44 File Offset: 0x00044F44
		internal DiagnosticInfo[] GetDiagnostics()
		{
			DiagnosticInfo[] array;
			if (this.ContainsDiagnostics && GreenNode.s_diagnosticsTable.TryGetValue(this, ref array))
			{
				return array;
			}
			return GreenNode.s_noDiagnostics;
		}

		// Token: 0x060017D7 RID: 6103
		internal abstract GreenNode SetDiagnostics([Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics);

		// Token: 0x060017D8 RID: 6104 RVA: 0x00046D70 File Offset: 0x00044F70
		public virtual string ToFullString()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringWriter stringWriter = new StringWriter(instance.Builder, CultureInfo.InvariantCulture);
			this.WriteTo(stringWriter, true, true);
			return instance.ToStringAndFree();
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00046DA4 File Offset: 0x00044FA4
		public override string ToString()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringWriter stringWriter = new StringWriter(instance.Builder, CultureInfo.InvariantCulture);
			this.WriteTo(stringWriter, false, false);
			return instance.ToStringAndFree();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00046DD5 File Offset: 0x00044FD5
		public void WriteTo(TextWriter writer)
		{
			this.WriteTo(writer, true, true);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00046DE0 File Offset: 0x00044FE0
		protected internal void WriteTo(TextWriter writer, bool leading, bool trailing)
		{
			ArrayBuilder<ValueTuple<GreenNode, bool, bool>> instance = ArrayBuilder<ValueTuple<GreenNode, bool, bool>>.GetInstance();
			instance.Push(new ValueTuple<GreenNode, bool, bool>(this, leading, trailing));
			GreenNode.<WriteTo>g__processStack|104_0(writer, instance);
			instance.Free();
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00046E10 File Offset: 0x00045010
		private static int GetFirstNonNullChildIndex(GreenNode node)
		{
			int slotCount = node.SlotCount;
			int num = 0;
			while (num < slotCount && node.GetSlot(num) == null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00046E3C File Offset: 0x0004503C
		private static int GetLastNonNullChildIndex(GreenNode node)
		{
			int num = node.SlotCount - 1;
			while (num >= 0 && node.GetSlot(num) == null)
			{
				num--;
			}
			return num;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00046E66 File Offset: 0x00045066
		protected virtual void WriteTriviaTo(TextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00046E6D File Offset: 0x0004506D
		protected virtual void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00046E74 File Offset: 0x00045074
		public virtual int RawContextualKind
		{
			get
			{
				return this.RawKind;
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00046E7C File Offset: 0x0004507C
		[NullableContext(2)]
		public virtual object GetValue()
		{
			return null;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00046E7F File Offset: 0x0004507F
		public virtual string GetValueText()
		{
			return string.Empty;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00046E86 File Offset: 0x00045086
		[NullableContext(2)]
		public virtual GreenNode GetLeadingTriviaCore()
		{
			return null;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00046E89 File Offset: 0x00045089
		[NullableContext(2)]
		public virtual GreenNode GetTrailingTriviaCore()
		{
			return null;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00046E8C File Offset: 0x0004508C
		public virtual GreenNode WithLeadingTrivia([Nullable(2)] GreenNode trivia)
		{
			return this;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00046E8F File Offset: 0x0004508F
		public virtual GreenNode WithTrailingTrivia([Nullable(2)] GreenNode trivia)
		{
			return this;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00046E94 File Offset: 0x00045094
		[NullableContext(2)]
		internal GreenNode GetFirstTerminal()
		{
			GreenNode greenNode = this;
			int? num;
			int num2;
			do
			{
				GreenNode greenNode2 = null;
				int i = 0;
				int slotCount = greenNode.SlotCount;
				while (i < slotCount)
				{
					GreenNode slot = greenNode.GetSlot(i);
					if (slot != null)
					{
						greenNode2 = slot;
						break;
					}
					i++;
				}
				greenNode = greenNode2;
				byte? b = ((greenNode != null) ? new byte?(greenNode._slotCount) : default(byte?));
				num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?));
				num2 = 0;
			}
			while ((num.GetValueOrDefault() > num2) & (num != null));
			return greenNode;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00046F24 File Offset: 0x00045124
		[NullableContext(2)]
		internal GreenNode GetLastTerminal()
		{
			GreenNode greenNode = this;
			int? num;
			int num2;
			do
			{
				GreenNode greenNode2 = null;
				for (int i = greenNode.SlotCount - 1; i >= 0; i--)
				{
					GreenNode slot = greenNode.GetSlot(i);
					if (slot != null)
					{
						greenNode2 = slot;
						break;
					}
				}
				greenNode = greenNode2;
				byte? b = ((greenNode != null) ? new byte?(greenNode._slotCount) : default(byte?));
				num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?));
				num2 = 0;
			}
			while ((num.GetValueOrDefault() > num2) & (num != null));
			return greenNode;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00046FB4 File Offset: 0x000451B4
		[NullableContext(2)]
		internal GreenNode GetLastNonmissingTerminal()
		{
			GreenNode greenNode = this;
			int? num;
			int num2;
			do
			{
				GreenNode greenNode2 = null;
				for (int i = greenNode.SlotCount - 1; i >= 0; i--)
				{
					GreenNode slot = greenNode.GetSlot(i);
					if (slot != null && !slot.IsMissing)
					{
						greenNode2 = slot;
						break;
					}
				}
				greenNode = greenNode2;
				byte? b = ((greenNode != null) ? new byte?(greenNode._slotCount) : default(byte?));
				num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?));
				num2 = 0;
			}
			while ((num.GetValueOrDefault() > num2) & (num != null));
			return greenNode;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0004704C File Offset: 0x0004524C
		[NullableContext(2)]
		public virtual bool IsEquivalentTo([NotNullWhen(true)] GreenNode other)
		{
			return this == other || (other != null && GreenNode.EquivalentToInternal(this, other));
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00047060 File Offset: 0x00045260
		private static bool EquivalentToInternal(GreenNode node1, GreenNode node2)
		{
			if (node1.RawKind != node2.RawKind)
			{
				if (node1.IsList && node1.SlotCount == 1)
				{
					node1 = node1.GetRequiredSlot(0);
				}
				if (node2.IsList && node2.SlotCount == 1)
				{
					node2 = node2.GetRequiredSlot(0);
				}
				if (node1.RawKind != node2.RawKind)
				{
					return false;
				}
			}
			if (node1._fullWidth != node2._fullWidth)
			{
				return false;
			}
			int slotCount = node1.SlotCount;
			if (slotCount != node2.SlotCount)
			{
				return false;
			}
			for (int i = 0; i < slotCount; i++)
			{
				GreenNode slot = node1.GetSlot(i);
				GreenNode slot2 = node2.GetSlot(i);
				if (slot != null && slot2 != null && !slot.IsEquivalentTo(slot2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017EC RID: 6124
		public abstract SyntaxNode GetStructure(SyntaxTrivia parentTrivia);

		// Token: 0x060017ED RID: 6125
		public abstract SyntaxToken CreateSeparator<[Nullable(0)] TNode>(SyntaxNode element) where TNode : SyntaxNode;

		// Token: 0x060017EE RID: 6126
		public abstract bool IsTriviaWithEndOfLine();

		// Token: 0x060017EF RID: 6127 RVA: 0x00047110 File Offset: 0x00045310
		[NullableContext(2)]
		public static GreenNode CreateList<TFrom>([Nullable(new byte[] { 2, 1 })] IEnumerable<TFrom> enumerable, [Nullable(1)] Func<TFrom, GreenNode> select)
		{
			GreenNode greenNode;
			if (enumerable != null)
			{
				List<TFrom> list = enumerable as List<TFrom>;
				if (list == null)
				{
					IReadOnlyList<TFrom> readOnlyList = enumerable as IReadOnlyList<TFrom>;
					if (readOnlyList == null)
					{
						greenNode = GreenNode.CreateList<TFrom>(Enumerable.ToList<TFrom>(enumerable), select);
					}
					else
					{
						greenNode = GreenNode.CreateList<TFrom>(readOnlyList, select);
					}
				}
				else
				{
					greenNode = GreenNode.CreateList<TFrom>(list, select);
				}
			}
			else
			{
				greenNode = null;
			}
			return greenNode;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0004715C File Offset: 0x0004535C
		[return: Nullable(2)]
		public static GreenNode CreateList<[Nullable(2)] TFrom>(List<TFrom> list, Func<TFrom, GreenNode> select)
		{
			switch (list.Count)
			{
			case 0:
				return null;
			case 1:
				return select.Invoke(list[0]);
			case 2:
				return SyntaxList.List(select.Invoke(list[0]), select.Invoke(list[1]));
			case 3:
				return SyntaxList.List(select.Invoke(list[0]), select.Invoke(list[1]), select.Invoke(list[2]));
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Value = select.Invoke(list[i]);
				}
				return SyntaxList.List(array);
			}
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00047220 File Offset: 0x00045420
		[return: Nullable(2)]
		public static GreenNode CreateList<[Nullable(2)] TFrom>(IReadOnlyList<TFrom> list, Func<TFrom, GreenNode> select)
		{
			switch (list.Count)
			{
			case 0:
				return null;
			case 1:
				return select.Invoke(list[0]);
			case 2:
				return SyntaxList.List(select.Invoke(list[0]), select.Invoke(list[1]));
			case 3:
				return SyntaxList.List(select.Invoke(list[0]), select.Invoke(list[1]), select.Invoke(list[2]));
			default:
			{
				ArrayElement<GreenNode>[] array = new ArrayElement<GreenNode>[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Value = select.Invoke(list[i]);
				}
				return SyntaxList.List(array);
			}
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000472E2 File Offset: 0x000454E2
		public SyntaxNode CreateRed()
		{
			return this.CreateRed(null, 0);
		}

		// Token: 0x060017F3 RID: 6131
		internal abstract SyntaxNode CreateRed([Nullable(2)] SyntaxNode parent, int position);

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000472EC File Offset: 0x000454EC
		internal bool IsCacheable
		{
			get
			{
				return (this.flags & GreenNode.NodeFlags.InheritMask) == GreenNode.NodeFlags.IsNotMissing && this.SlotCount <= 3;
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0004730C File Offset: 0x0004550C
		internal int GetCacheHash()
		{
			int num = (int)this.flags ^ this.RawKind;
			int slotCount = this.SlotCount;
			for (int i = 0; i < slotCount; i++)
			{
				GreenNode slot = this.GetSlot(i);
				if (slot != null)
				{
					num = Hash.Combine(RuntimeHelpers.GetHashCode(slot), num);
				}
			}
			return num & int.MaxValue;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00047359 File Offset: 0x00045559
		[NullableContext(2)]
		internal bool IsCacheEquivalent(int kind, GreenNode.NodeFlags flags, GreenNode child1)
		{
			return this.RawKind == kind && this.flags == flags && this.GetSlot(0) == child1;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00047379 File Offset: 0x00045579
		[NullableContext(2)]
		internal bool IsCacheEquivalent(int kind, GreenNode.NodeFlags flags, GreenNode child1, GreenNode child2)
		{
			return this.RawKind == kind && this.flags == flags && this.GetSlot(0) == child1 && this.GetSlot(1) == child2;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000473A4 File Offset: 0x000455A4
		[NullableContext(2)]
		internal bool IsCacheEquivalent(int kind, GreenNode.NodeFlags flags, GreenNode child1, GreenNode child2, GreenNode child3)
		{
			return this.RawKind == kind && this.flags == flags && this.GetSlot(0) == child1 && this.GetSlot(1) == child2 && this.GetSlot(2) == child3;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000473DC File Offset: 0x000455DC
		internal GreenNode AddError(DiagnosticInfo err)
		{
			DiagnosticInfo[] array;
			if (this.GetDiagnostics() == null)
			{
				array = new DiagnosticInfo[] { err };
			}
			else
			{
				array = this.GetDiagnostics();
				int num = array.Length;
				Array.Resize<DiagnosticInfo>(ref array, num + 1);
				array[num] = err;
			}
			return this.SetDiagnostics(array);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00047454 File Offset: 0x00045654
		[CompilerGenerated]
		internal static void <WriteTo>g__processStack|104_0(TextWriter writer, [TupleElementNames(new string[] { "node", "leading", "trailing" })] [Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ValueTuple<GreenNode, bool, bool>> stack)
		{
			while (stack.Count > 0)
			{
				ValueTuple<GreenNode, bool, bool> valueTuple = stack.Pop<ValueTuple<GreenNode, bool, bool>>();
				GreenNode item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				bool item3 = valueTuple.Item3;
				if (item.IsToken)
				{
					item.WriteTokenTo(writer, item2, item3);
				}
				else if (item.IsTrivia)
				{
					item.WriteTriviaTo(writer);
				}
				else
				{
					int firstNonNullChildIndex = GreenNode.GetFirstNonNullChildIndex(item);
					int lastNonNullChildIndex = GreenNode.GetLastNonNullChildIndex(item);
					for (int i = lastNonNullChildIndex; i >= firstNonNullChildIndex; i--)
					{
						GreenNode slot = item.GetSlot(i);
						if (slot != null)
						{
							bool flag = i == firstNonNullChildIndex;
							bool flag2 = i == lastNonNullChildIndex;
							stack.Push(new ValueTuple<GreenNode, bool, bool>(slot, item2 | !flag, item3 | !flag2));
						}
					}
				}
			}
		}

		// Token: 0x040009EA RID: 2538
		internal const int ListKind = 1;

		// Token: 0x040009EB RID: 2539
		private readonly ushort _kind;

		// Token: 0x040009EC RID: 2540
		protected GreenNode.NodeFlags flags;

		// Token: 0x040009ED RID: 2541
		private byte _slotCount;

		// Token: 0x040009EE RID: 2542
		private int _fullWidth;

		// Token: 0x040009EF RID: 2543
		private static readonly ConditionalWeakTable<GreenNode, DiagnosticInfo[]> s_diagnosticsTable = new ConditionalWeakTable<GreenNode, DiagnosticInfo[]>();

		// Token: 0x040009F0 RID: 2544
		private static readonly ConditionalWeakTable<GreenNode, SyntaxAnnotation[]> s_annotationsTable = new ConditionalWeakTable<GreenNode, SyntaxAnnotation[]>();

		// Token: 0x040009F1 RID: 2545
		private static readonly DiagnosticInfo[] s_noDiagnostics = Array.Empty<DiagnosticInfo>();

		// Token: 0x040009F2 RID: 2546
		private static readonly SyntaxAnnotation[] s_noAnnotations = Array.Empty<SyntaxAnnotation>();

		// Token: 0x040009F3 RID: 2547
		private static readonly IEnumerable<SyntaxAnnotation> s_noAnnotationsEnumerable = SpecializedCollections.EmptyEnumerable<SyntaxAnnotation>();

		// Token: 0x040009F4 RID: 2548
		private const ushort ExtendedSerializationInfoMask = 32768;

		// Token: 0x040009F5 RID: 2549
		internal const int MaxCachedChildNum = 3;

		// Token: 0x020005F4 RID: 1524
		[NullableContext(0)]
		[Flags]
		internal enum NodeFlags : byte
		{
			// Token: 0x0400173B RID: 5947
			None = 0,
			// Token: 0x0400173C RID: 5948
			ContainsDiagnostics = 1,
			// Token: 0x0400173D RID: 5949
			ContainsStructuredTrivia = 2,
			// Token: 0x0400173E RID: 5950
			ContainsDirectives = 4,
			// Token: 0x0400173F RID: 5951
			ContainsSkippedText = 8,
			// Token: 0x04001740 RID: 5952
			ContainsAnnotations = 16,
			// Token: 0x04001741 RID: 5953
			IsNotMissing = 32,
			// Token: 0x04001742 RID: 5954
			FactoryContextIsInAsync = 64,
			// Token: 0x04001743 RID: 5955
			FactoryContextIsInQuery = 128,
			// Token: 0x04001744 RID: 5956
			FactoryContextIsInIterator = 128,
			// Token: 0x04001745 RID: 5957
			InheritMask = 63
		}
	}
}
