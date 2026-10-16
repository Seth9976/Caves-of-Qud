using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000273 RID: 627
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ParameterSyntax : BaseParameterSyntax
	{
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x0010337A File Offset: 0x0010157A
		internal bool IsArgList
		{
			get
			{
				return this.Type == null && this.Identifier.ContextualKind() == SyntaxKind.ArgListKeyword;
			}
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x00103398 File Offset: 0x00101598
		internal ParameterSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x001033A3 File Offset: 0x001015A3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x001033B8 File Offset: 0x001015B8
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

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x001033F4 File Offset: 0x001015F4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x00103403 File Offset: 0x00101603
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((ParameterSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003B98 RID: 15256 RVA: 0x00103429 File Offset: 0x00101629
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EqualsValueClauseSyntax Default
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<EqualsValueClauseSyntax>(ref this.@default, 4);
			}
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00103438 File Offset: 0x00101638
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<TypeSyntax>(ref this.type, 2);
			case 4:
				return base.GetRed<EqualsValueClauseSyntax>(ref this.@default, 4);
			}
			return null;
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x00103494 File Offset: 0x00101694
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.type;
			case 4:
				return this.@default;
			}
			return null;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x001034DB File Offset: 0x001016DB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParameter(this);
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x001034E4 File Offset: 0x001016E4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x001034F0 File Offset: 0x001016F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public ParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax @default)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && type == this.Type && !(identifier != this.Identifier) && @default == this.Default)
			{
				return this;
			}
			ParameterSyntax parameterSyntax = SyntaxFactory.Parameter(attributeLists, modifiers, type, identifier, @default);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return parameterSyntax;
			}
			return parameterSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00103561 File Offset: 0x00101761
		internal override BaseParameterSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x0010356A File Offset: 0x0010176A
		public new ParameterSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.Type, this.Identifier, this.Default);
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x0010358B File Offset: 0x0010178B
		internal override BaseParameterSyntax WithModifiersCore(SyntaxTokenList modifiers)
		{
			return this.WithModifiers(modifiers);
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00103594 File Offset: 0x00101794
		public new ParameterSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.Type, this.Identifier, this.Default);
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x001035B5 File Offset: 0x001017B5
		internal override BaseParameterSyntax WithTypeCore([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.WithType(type);
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x001035BE File Offset: 0x001017BE
		public new ParameterSyntax WithType([global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return this.Update(this.AttributeLists, this.Modifiers, type, this.Identifier, this.Default);
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x001035DF File Offset: 0x001017DF
		public ParameterSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, identifier, this.Default);
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00103600 File Offset: 0x00101800
		public ParameterSyntax WithDefault([global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax @default)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.Type, this.Identifier, @default);
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x00103621 File Offset: 0x00101821
		internal override BaseParameterSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x0010362C File Offset: 0x0010182C
		public new ParameterSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0010364E File Offset: 0x0010184E
		internal override BaseParameterSyntax AddModifiersCore(params SyntaxToken[] items)
		{
			return this.AddModifiers(items);
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00103658 File Offset: 0x00101858
		public new ParameterSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x04001403 RID: 5123
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x04001404 RID: 5124
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;

		// Token: 0x04001405 RID: 5125
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private EqualsValueClauseSyntax @default;
	}
}
