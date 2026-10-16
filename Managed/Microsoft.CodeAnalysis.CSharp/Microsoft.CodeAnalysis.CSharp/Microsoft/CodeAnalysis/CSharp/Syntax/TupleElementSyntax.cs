using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200029A RID: 666
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TupleElementSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E46 RID: 15942 RVA: 0x0010A0C6 File Offset: 0x001082C6
		internal TupleElementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003E47 RID: 15943 RVA: 0x0010A0D1 File Offset: 0x001082D1
		public TypeSyntax Type
		{
			get
			{
				return base.GetRedAtZero<TypeSyntax>(ref this.type);
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x0010A0E0 File Offset: 0x001082E0
		public SyntaxToken Identifier
		{
			get
			{
				SyntaxToken identifier = ((TupleElementSyntax)base.Green).identifier;
				if (identifier == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0010A120 File Offset: 0x00108320
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return base.GetRedAtZero<TypeSyntax>(ref this.type);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0010A133 File Offset: 0x00108333
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x0010A140 File Offset: 0x00108340
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleElement(this);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x0010A149 File Offset: 0x00108349
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleElement(this);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0010A154 File Offset: 0x00108354
		public TupleElementSyntax Update(TypeSyntax type, SyntaxToken identifier)
		{
			if (type == this.Type && !(identifier != this.Identifier))
			{
				return this;
			}
			TupleElementSyntax tupleElementSyntax = SyntaxFactory.TupleElement(type, identifier);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return tupleElementSyntax;
			}
			return tupleElementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0010A199 File Offset: 0x00108399
		public TupleElementSyntax WithType(TypeSyntax type)
		{
			return this.Update(type, this.Identifier);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x0010A1A8 File Offset: 0x001083A8
		public TupleElementSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.Type, identifier);
		}

		// Token: 0x04001457 RID: 5207
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
