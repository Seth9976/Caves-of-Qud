using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000326 RID: 806
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerParameterSyntax : BaseParameterSyntax
	{
		// Token: 0x06004557 RID: 17751 RVA: 0x001168BE File Offset: 0x00114ABE
		internal FunctionPointerParameterSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x001168C9 File Offset: 0x00114AC9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x001168E0 File Offset: 0x00114AE0
		public override SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x0011691C File Offset: 0x00114B1C
		public override TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x0011692C File Offset: 0x00114B2C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<TypeSyntax>(ref this.type, 2);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero(ref this.attributeLists);
			}
			return syntaxNode;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00116964 File Offset: 0x00114B64
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 2)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.type;
				}
			}
			else
			{
				syntaxNode = this.attributeLists;
			}
			return syntaxNode;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x0011698F File Offset: 0x00114B8F
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerParameter(this);
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x00116998 File Offset: 0x00114B98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerParameter(this);
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x001169A4 File Offset: 0x00114BA4
		public FunctionPointerParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && type == this.Type)
			{
				return this;
			}
			FunctionPointerParameterSyntax functionPointerParameterSyntax = SyntaxFactory.FunctionPointerParameter(attributeLists, modifiers, type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerParameterSyntax;
			}
			return functionPointerParameterSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x001169F8 File Offset: 0x00114BF8
		internal override BaseParameterSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x00116A01 File Offset: 0x00114C01
		public new FunctionPointerParameterSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Type);
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x00116A16 File Offset: 0x00114C16
		internal override BaseParameterSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x00116A1F File Offset: 0x00114C1F
		public new FunctionPointerParameterSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Type);
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x00116A34 File Offset: 0x00114C34
		internal override BaseParameterSyntax WithTypeCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this.WithType(type);
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x00116A4C File Offset: 0x00114C4C
		public new FunctionPointerParameterSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, type);
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x00116A61 File Offset: 0x00114C61
		internal override BaseParameterSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x00116A6C File Offset: 0x00114C6C
		public new FunctionPointerParameterSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x00116A8E File Offset: 0x00114C8E
		internal override BaseParameterSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06004569 RID: 17769 RVA: 0x00116A98 File Offset: 0x00114C98
		public new FunctionPointerParameterSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x0400150B RID: 5387
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x0400150C RID: 5388
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
