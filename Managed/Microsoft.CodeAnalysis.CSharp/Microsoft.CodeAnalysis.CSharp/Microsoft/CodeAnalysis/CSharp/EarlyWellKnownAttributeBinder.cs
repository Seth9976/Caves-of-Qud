using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001A RID: 26
	internal sealed class EarlyWellKnownAttributeBinder : Binder
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x000350F0 File Offset: 0x000332F0
		internal EarlyWellKnownAttributeBinder(Binder enclosing)
			: base(enclosing, enclosing.Flags | BinderFlags.EarlyAttributeBinding)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00035108 File Offset: 0x00033308
		internal CSharpAttributeData GetAttribute(AttributeSyntax node, NamedTypeSymbol boundAttributeType, out bool generatedDiagnostics)
		{
			BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(DiagnosticBag.GetInstance());
			CSharpAttributeData attribute = base.GetAttribute(node, boundAttributeType, bindingDiagnosticBag);
			generatedDiagnostics = !bindingDiagnosticBag.DiagnosticBag.IsEmptyWithoutResolution;
			bindingDiagnosticBag.Free();
			return attribute;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0003513F File Offset: 0x0003333F
		[Obsolete("EarlyWellKnownAttributeBinder has a better overload - GetAttribute(AttributeSyntax, NamedTypeSymbol, out bool)", true)]
		internal new CSharpAttributeData GetAttribute(AttributeSyntax node, NamedTypeSymbol boundAttributeType, BindingDiagnosticBag diagnostics)
		{
			diagnostics.Add(ErrorCode.ERR_InternalError, node.Location);
			return base.GetAttribute(node, boundAttributeType, diagnostics);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0003515C File Offset: 0x0003335C
		internal static bool CanBeValidAttributeArgument(ExpressionSyntax node, Binder typeBinder)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.CastExpression)
			{
				if (syntaxKind <= SyntaxKind.PredefinedType)
				{
					if (syntaxKind - SyntaxKind.IdentifierName > 2 && syntaxKind - SyntaxKind.AliasQualifiedName > 1)
					{
						return false;
					}
				}
				else if (syntaxKind - SyntaxKind.ParenthesizedExpression > 2 && syntaxKind != SyntaxKind.CastExpression)
				{
					return false;
				}
			}
			else if (syntaxKind <= SyntaxKind.LogicalNotExpression)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.ObjectCreationExpression:
				case SyntaxKind.ImplicitObjectCreationExpression:
				{
					BaseObjectCreationExpressionSyntax baseObjectCreationExpressionSyntax = (BaseObjectCreationExpressionSyntax)node;
					if (baseObjectCreationExpressionSyntax.Initializer == null)
					{
						ArgumentListSyntax argumentList = baseObjectCreationExpressionSyntax.ArgumentList;
						return ((argumentList != null) ? argumentList.Arguments.Count : 0) == 0;
					}
					return false;
				}
				case SyntaxKind.AnonymousObjectCreationExpression:
				case SyntaxKind.ArrayCreationExpression:
				case SyntaxKind.ImplicitArrayCreationExpression:
				case SyntaxKind.StackAllocArrayCreationExpression:
				case SyntaxKind.OmittedArraySizeExpression:
				case SyntaxKind.InterpolatedStringExpression:
				case SyntaxKind.ImplicitElementAccess:
				case SyntaxKind.IsPatternExpression:
				case SyntaxKind.RangeExpression:
				case (SyntaxKind)8660:
				case (SyntaxKind)8661:
				case (SyntaxKind)8662:
				case (SyntaxKind)8663:
				case (SyntaxKind)8664:
				case (SyntaxKind)8665:
				case (SyntaxKind)8666:
				case (SyntaxKind)8667:
				case SyntaxKind.IsExpression:
				case SyntaxKind.AsExpression:
				case SyntaxKind.CoalesceExpression:
					return false;
				case SyntaxKind.AddExpression:
				case SyntaxKind.SubtractExpression:
				case SyntaxKind.MultiplyExpression:
				case SyntaxKind.DivideExpression:
				case SyntaxKind.ModuloExpression:
				case SyntaxKind.LeftShiftExpression:
				case SyntaxKind.RightShiftExpression:
				case SyntaxKind.LogicalOrExpression:
				case SyntaxKind.LogicalAndExpression:
				case SyntaxKind.BitwiseOrExpression:
				case SyntaxKind.BitwiseAndExpression:
				case SyntaxKind.ExclusiveOrExpression:
				case SyntaxKind.EqualsExpression:
				case SyntaxKind.NotEqualsExpression:
				case SyntaxKind.LessThanExpression:
				case SyntaxKind.LessThanOrEqualExpression:
				case SyntaxKind.GreaterThanExpression:
				case SyntaxKind.GreaterThanOrEqualExpression:
				case SyntaxKind.SimpleMemberAccessExpression:
					break;
				default:
					if (syntaxKind - SyntaxKind.UnaryPlusExpression > 3)
					{
						return false;
					}
					break;
				}
			}
			else if (syntaxKind - SyntaxKind.NumericLiteralExpression > 5 && syntaxKind - SyntaxKind.TypeOfExpression > 4)
			{
				return false;
			}
			return true;
		}
	}
}
