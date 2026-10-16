using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000245 RID: 581
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AttributeSyntax : CSharpSyntaxNode
	{
		// Token: 0x060037C6 RID: 14278 RVA: 0x000F9DFE File Offset: 0x000F7FFE
		internal string GetErrorDisplayName()
		{
			return this.Name.ErrorDisplayName();
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000F9E0C File Offset: 0x000F800C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal AttributeArgumentSyntax GetNamedArgumentSyntax(string namedArgName)
		{
			if (this.argumentList != null)
			{
				foreach (AttributeArgumentSyntax attributeArgumentSyntax in this.argumentList.Arguments)
				{
					if (attributeArgumentSyntax.NameEquals != null && attributeArgumentSyntax.NameEquals.Name.Identifier.ValueText == namedArgName)
					{
						return attributeArgumentSyntax;
					}
				}
			}
			return null;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000F9E73 File Offset: 0x000F8073
		internal AttributeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000F9E7E File Offset: 0x000F807E
		public NameSyntax Name
		{
			get
			{
				return base.GetRedAtZero<NameSyntax>(ref this.name);
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000F9E8C File Offset: 0x000F808C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AttributeArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<AttributeArgumentListSyntax>(ref this.argumentList, 1);
			}
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000F9E9C File Offset: 0x000F809C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<AttributeArgumentListSyntax>(ref this.argumentList, 1);
				}
			}
			else
			{
				syntaxNode = base.GetRedAtZero<NameSyntax>(ref this.name);
			}
			return syntaxNode;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000F9ED4 File Offset: 0x000F80D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 0)
			{
				if (index != 1)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.argumentList;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000F9EFF File Offset: 0x000F80FF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttribute(this);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000F9F08 File Offset: 0x000F8108
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttribute(this);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000F9F14 File Offset: 0x000F8114
		public AttributeSyntax Update(NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList)
		{
			if (name == this.Name && argumentList == this.ArgumentList)
			{
				return this;
			}
			AttributeSyntax attributeSyntax = SyntaxFactory.Attribute(name, argumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return attributeSyntax;
			}
			return attributeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000F9F54 File Offset: 0x000F8154
		public AttributeSyntax WithName(NameSyntax name)
		{
			return this.Update(name, this.ArgumentList);
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000F9F63 File Offset: 0x000F8163
		public AttributeSyntax WithArgumentList([global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList)
		{
			return this.Update(this.Name, argumentList);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000F9F74 File Offset: 0x000F8174
		public AttributeSyntax AddArgumentListArguments(params AttributeArgumentSyntax[] items)
		{
			AttributeArgumentListSyntax attributeArgumentListSyntax;
			if ((attributeArgumentListSyntax = this.ArgumentList) == null)
			{
				attributeArgumentListSyntax = SyntaxFactory.AttributeArgumentList(default(SeparatedSyntaxList<AttributeArgumentSyntax>));
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax2 = attributeArgumentListSyntax;
			return this.WithArgumentList(attributeArgumentListSyntax2.WithArguments(attributeArgumentListSyntax2.Arguments.AddRange(items)));
		}

		// Token: 0x04001397 RID: 5015
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NameSyntax name;

		// Token: 0x04001398 RID: 5016
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private AttributeArgumentListSyntax argumentList;
	}
}
