using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000075 RID: 117
	internal sealed class TypeofBinder : Binder
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0005066D File Offset: 0x0004E86D
		internal TypeofBinder(ExpressionSyntax typeExpression, Binder next)
			: base(next, next.Flags | BinderFlags.UnsafeRegion)
		{
			TypeofBinder.OpenTypeVisitor.Visit(typeExpression, out this._allowedMap, out this._isTypeExpressionOpen);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00050694 File Offset: 0x0004E894
		internal bool IsTypeExpressionOpen
		{
			get
			{
				return this._isTypeExpressionOpen;
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0005069C File Offset: 0x0004E89C
		protected override bool IsUnboundTypeAllowed(GenericNameSyntax syntax)
		{
			bool flag;
			return this._allowedMap != null && this._allowedMap.TryGetValue(syntax, ref flag) && flag;
		}

		// Token: 0x04000473 RID: 1139
		private readonly Dictionary<GenericNameSyntax, bool> _allowedMap;

		// Token: 0x04000474 RID: 1140
		private readonly bool _isTypeExpressionOpen;

		// Token: 0x02000678 RID: 1656
		private class OpenTypeVisitor : CSharpSyntaxVisitor
		{
			// Token: 0x060082F0 RID: 33520 RVA: 0x001DB168 File Offset: 0x001D9368
			public static void Visit(ExpressionSyntax typeSyntax, out Dictionary<GenericNameSyntax, bool> allowedMap, out bool isUnboundGenericType)
			{
				TypeofBinder.OpenTypeVisitor openTypeVisitor = new TypeofBinder.OpenTypeVisitor();
				openTypeVisitor.Visit(typeSyntax);
				allowedMap = openTypeVisitor._allowedMap;
				isUnboundGenericType = openTypeVisitor._seenGeneric && !openTypeVisitor._seenConstructed;
			}

			// Token: 0x060082F1 RID: 33521 RVA: 0x001DB1A0 File Offset: 0x001D93A0
			public override void VisitGenericName(GenericNameSyntax node)
			{
				this._seenGeneric = true;
				SeparatedSyntaxList<TypeSyntax> arguments = node.TypeArgumentList.Arguments;
				if (node.IsUnboundGenericName)
				{
					if (this._allowedMap == null)
					{
						this._allowedMap = new Dictionary<GenericNameSyntax, bool>();
					}
					this._allowedMap[node] = !this._seenConstructed;
					return;
				}
				this._seenConstructed = true;
				foreach (TypeSyntax typeSyntax in arguments)
				{
					this.Visit(typeSyntax);
				}
			}

			// Token: 0x060082F2 RID: 33522 RVA: 0x001DB21C File Offset: 0x001D941C
			public override void VisitQualifiedName(QualifiedNameSyntax node)
			{
				bool seenConstructed = this._seenConstructed;
				this.Visit(node.Right);
				bool seenConstructed2 = this._seenConstructed;
				this.Visit(node.Left);
				if (!seenConstructed && !seenConstructed2 && this._seenConstructed)
				{
					this.Visit(node.Right);
				}
			}

			// Token: 0x060082F3 RID: 33523 RVA: 0x001DB267 File Offset: 0x001D9467
			public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
			{
				this.Visit(node.Name);
			}

			// Token: 0x060082F4 RID: 33524 RVA: 0x001DB275 File Offset: 0x001D9475
			public override void VisitArrayType(ArrayTypeSyntax node)
			{
				this._seenConstructed = true;
				this.Visit(node.ElementType);
			}

			// Token: 0x060082F5 RID: 33525 RVA: 0x001DB28A File Offset: 0x001D948A
			public override void VisitPointerType(PointerTypeSyntax node)
			{
				this._seenConstructed = true;
				this.Visit(node.ElementType);
			}

			// Token: 0x060082F6 RID: 33526 RVA: 0x001DB29F File Offset: 0x001D949F
			public override void VisitNullableType(NullableTypeSyntax node)
			{
				this._seenConstructed = true;
				this.Visit(node.ElementType);
			}

			// Token: 0x0400206A RID: 8298
			private Dictionary<GenericNameSyntax, bool> _allowedMap;

			// Token: 0x0400206B RID: 8299
			private bool _seenConstructed;

			// Token: 0x0400206C RID: 8300
			private bool _seenGeneric;
		}
	}
}
