using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F2 RID: 498
	internal class ExpressionLambdaRewriter
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000B2290 File Offset: 0x000B0490
		private NamedTypeSymbol ExpressionType
		{
			get
			{
				if (this._ExpressionType == null)
				{
					this._ExpressionType = this._bound.WellKnownType(WellKnownType.System_Linq_Expressions_Expression);
				}
				return this._ExpressionType;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000B22B6 File Offset: 0x000B04B6
		private NamedTypeSymbol ParameterExpressionType
		{
			get
			{
				if (this._ParameterExpressionType == null)
				{
					this._ParameterExpressionType = this._bound.WellKnownType(WellKnownType.System_Linq_Expressions_ParameterExpression);
				}
				return this._ParameterExpressionType;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000B22DC File Offset: 0x000B04DC
		private NamedTypeSymbol ElementInitType
		{
			get
			{
				if (this._ElementInitType == null)
				{
					this._ElementInitType = this._bound.WellKnownType(WellKnownType.System_Linq_Expressions_ElementInit);
				}
				return this._ElementInitType;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000B2302 File Offset: 0x000B0502
		public NamedTypeSymbol MemberBindingType
		{
			get
			{
				if (this._MemberBindingType == null)
				{
					this._MemberBindingType = this._bound.WellKnownType(WellKnownType.System_Linq_Expressions_MemberBinding);
				}
				return this._MemberBindingType;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000B2328 File Offset: 0x000B0528
		private NamedTypeSymbol MemberInfoType
		{
			get
			{
				if (this._MemberInfoType == null)
				{
					this._MemberInfoType = this._bound.WellKnownType(WellKnownType.System_Reflection_MemberInfo);
				}
				return this._MemberInfoType;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000B234B File Offset: 0x000B054B
		private BindingDiagnosticBag Diagnostics
		{
			get
			{
				return this._bound.Diagnostics;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000B2358 File Offset: 0x000B0558
		private ExpressionLambdaRewriter(TypeCompilationState compilationState, TypeMap typeMap, SyntaxNode node, int recursionDepth, BindingDiagnosticBag diagnostics)
		{
			this._bound = new SyntheticBoundNodeFactory(null, compilationState.Type, node, compilationState, diagnostics);
			this._ignoreAccessibility = compilationState.ModuleBuilderOpt.IgnoreAccessibility;
			this._int32Type = this._bound.SpecialType(SpecialType.System_Int32);
			this._objectType = this._bound.SpecialType(SpecialType.System_Object);
			this._nullableType = this._bound.SpecialType(SpecialType.System_Nullable_T);
			this._IEnumerableType = this._bound.SpecialType(SpecialType.System_Collections_Generic_IEnumerable_T);
			this._typeMap = typeMap;
			this._recursionDepth = recursionDepth;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000B23F8 File Offset: 0x000B05F8
		internal static BoundNode RewriteLambda(BoundLambda node, TypeCompilationState compilationState, TypeMap typeMap, int recursionDepth, BindingDiagnosticBag diagnostics)
		{
			BoundNode boundNode;
			try
			{
				ExpressionLambdaRewriter expressionLambdaRewriter = new ExpressionLambdaRewriter(compilationState, typeMap, node.Syntax, recursionDepth, diagnostics);
				BoundExpression boundExpression = expressionLambdaRewriter.VisitLambdaInternal(node);
				if (!node.Type.Equals(boundExpression.Type, TypeCompareKind.IgnoreNullableModifiersForReferenceTypes))
				{
					diagnostics.Add(ErrorCode.ERR_MissingPredefinedMember, node.Syntax.Location, new object[] { expressionLambdaRewriter.ExpressionType, "Lambda" });
				}
				boundNode = boundExpression;
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
				boundNode = node;
			}
			return boundNode;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000B2488 File Offset: 0x000B0688
		private BoundExpression TranslateLambdaBody(BoundBlock block)
		{
			foreach (BoundStatement boundStatement in block.Statements)
			{
				while (boundStatement != null)
				{
					BoundKind kind = boundStatement.Kind;
					if (kind <= BoundKind.SequencePointWithSpan)
					{
						if (kind == BoundKind.SequencePoint)
						{
							boundStatement = ((BoundSequencePoint)boundStatement).StatementOpt;
							continue;
						}
						if (kind == BoundKind.SequencePointWithSpan)
						{
							boundStatement = ((BoundSequencePointWithSpan)boundStatement).StatementOpt;
							continue;
						}
					}
					else if (kind != BoundKind.ReturnStatement)
					{
						if (kind == BoundKind.ExpressionStatement)
						{
							return this.Visit(((BoundExpressionStatement)boundStatement).Expression);
						}
					}
					else
					{
						BoundExpression boundExpression = this.Visit(((BoundReturnStatement)boundStatement).ExpressionOpt);
						if (boundExpression != null)
						{
							return boundExpression;
						}
						boundStatement = null;
						continue;
					}
					throw ExceptionUtilities.UnexpectedValue(boundStatement.Kind);
				}
			}
			return null;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000B2550 File Offset: 0x000B0750
		private BoundExpression Visit(BoundExpression node)
		{
			if (node == null)
			{
				return null;
			}
			SyntaxNode syntax = this._bound.Syntax;
			this._bound.Syntax = node.Syntax;
			BoundExpression boundExpression = this.VisitInternal(node);
			this._bound.Syntax = syntax;
			return this._bound.Convert(this.ExpressionType, boundExpression);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x000B25A8 File Offset: 0x000B07A8
		private BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.UserDefinedConditionalLogicalOperator)
			{
				if (kind <= BoundKind.PointerIndirectionOperator)
				{
					if (kind == BoundKind.PassByCopy)
					{
						return this.Visit(((BoundPassByCopy)node).Expression);
					}
					if (kind == BoundKind.UnaryOperator)
					{
						return this.VisitUnaryOperator((BoundUnaryOperator)node);
					}
					if (kind == BoundKind.PointerIndirectionOperator)
					{
						return ExpressionLambdaRewriter.VisitPointerIndirectionOperator((BoundPointerIndirectionOperator)node);
					}
				}
				else
				{
					if (kind == BoundKind.PointerElementAccess)
					{
						return ExpressionLambdaRewriter.VisitPointerElementAccess((BoundPointerElementAccess)node);
					}
					if (kind == BoundKind.BinaryOperator)
					{
						BoundBinaryOperator boundBinaryOperator = (BoundBinaryOperator)node;
						return this.VisitBinaryOperator(boundBinaryOperator.OperatorKind, boundBinaryOperator.MethodOpt, boundBinaryOperator.Type, boundBinaryOperator.Left, boundBinaryOperator.Right);
					}
					if (kind == BoundKind.UserDefinedConditionalLogicalOperator)
					{
						BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = (BoundUserDefinedConditionalLogicalOperator)node;
						return this.VisitBinaryOperator(boundUserDefinedConditionalLogicalOperator.OperatorKind, boundUserDefinedConditionalLogicalOperator.LogicalOperator, boundUserDefinedConditionalLogicalOperator.Type, boundUserDefinedConditionalLogicalOperator.Left, boundUserDefinedConditionalLogicalOperator.Right);
					}
				}
			}
			else
			{
				if (kind <= BoundKind.Parameter)
				{
					switch (kind)
					{
					case BoundKind.NullCoalescingOperator:
						return this.VisitNullCoalescingOperator((BoundNullCoalescingOperator)node);
					case BoundKind.NullCoalescingAssignmentOperator:
					case BoundKind.UnconvertedConditionalOperator:
					case BoundKind.AwaitableInfo:
					case BoundKind.AwaitExpression:
						goto IL_02D5;
					case BoundKind.ConditionalOperator:
						return this.VisitConditionalOperator((BoundConditionalOperator)node);
					case BoundKind.ArrayAccess:
						return this.VisitArrayAccess((BoundArrayAccess)node);
					case BoundKind.ArrayLength:
						return this.VisitArrayLength((BoundArrayLength)node);
					case BoundKind.TypeOfOperator:
						break;
					default:
						switch (kind)
						{
						case BoundKind.MethodInfo:
						case BoundKind.DefaultExpression:
							break;
						case BoundKind.FieldInfo:
						case BoundKind.DefaultLiteral:
							goto IL_02D5;
						case BoundKind.IsOperator:
							return this.VisitIsOperator((BoundIsOperator)node);
						case BoundKind.AsOperator:
							return this.VisitAsOperator((BoundAsOperator)node);
						case BoundKind.SizeOfOperator:
							return ExpressionLambdaRewriter.VisitSizeOfOperator((BoundSizeOfOperator)node);
						case BoundKind.Conversion:
							return this.VisitConversion((BoundConversion)node);
						default:
							switch (kind)
							{
							case BoundKind.Literal:
							case BoundKind.ThisReference:
							case BoundKind.PreviousSubmissionReference:
							case BoundKind.HostObjectMemberReference:
							case BoundKind.Local:
								break;
							case BoundKind.BaseReference:
								return this.VisitBaseReference((BoundBaseReference)node);
							case BoundKind.PseudoVariable:
							case BoundKind.RangeVariable:
								goto IL_02D5;
							case BoundKind.Parameter:
								return this.VisitParameter((BoundParameter)node);
							default:
								goto IL_02D5;
							}
							break;
						}
						break;
					}
					return this.Constant(node);
				}
				if (kind <= BoundKind.ObjectCreationExpression)
				{
					if (kind == BoundKind.Call)
					{
						return this.VisitCall((BoundCall)node);
					}
					if (kind == BoundKind.ObjectCreationExpression)
					{
						return this.VisitObjectCreationExpression((BoundObjectCreationExpression)node);
					}
				}
				else
				{
					switch (kind)
					{
					case BoundKind.NewT:
						return this.VisitNewT((BoundNewT)node);
					case BoundKind.DelegateCreationExpression:
						return this.VisitDelegateCreationExpression((BoundDelegateCreationExpression)node);
					case BoundKind.ArrayCreation:
						return this.VisitArrayCreation((BoundArrayCreation)node);
					case BoundKind.ArrayInitialization:
					case BoundKind.StackAllocArrayCreation:
					case BoundKind.ConvertedStackAllocExpression:
					case BoundKind.HoistedFieldAccess:
						break;
					case BoundKind.FieldAccess:
					{
						BoundFieldAccess boundFieldAccess = (BoundFieldAccess)node;
						if (boundFieldAccess.FieldSymbol.IsCapturedFrame)
						{
							return this.Constant(boundFieldAccess);
						}
						return this.VisitFieldAccess(boundFieldAccess);
					}
					case BoundKind.PropertyAccess:
						return this.VisitPropertyAccess((BoundPropertyAccess)node);
					default:
						if (kind == BoundKind.Lambda)
						{
							return this.VisitLambda((BoundLambda)node);
						}
						break;
					}
				}
			}
			IL_02D5:
			throw ExceptionUtilities.UnexpectedValue(node.Kind);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000B289C File Offset: 0x000B0A9C
		private BoundExpression VisitInternal(BoundExpression node)
		{
			this._recursionDepth++;
			BoundExpression boundExpression;
			if (this._recursionDepth > 1)
			{
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				boundExpression = this.VisitExpressionWithoutStackGuard(node);
			}
			else
			{
				boundExpression = this.VisitExpressionWithStackGuard(node);
			}
			this._recursionDepth--;
			return boundExpression;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000B28EC File Offset: 0x000B0AEC
		private BoundExpression VisitExpressionWithStackGuard(BoundExpression node)
		{
			BoundExpression boundExpression;
			try
			{
				boundExpression = this.VisitExpressionWithoutStackGuard(node);
			}
			catch (InsufficientExecutionStackException ex)
			{
				throw new BoundTreeVisitor.CancelledByStackGuardException(ex, node);
			}
			return boundExpression;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000B291C File Offset: 0x000B0B1C
		private BoundExpression VisitArrayAccess(BoundArrayAccess node)
		{
			BoundExpression boundExpression = this.Visit(node.Expression);
			if (node.Indices.Length == 1)
			{
				BoundExpression boundExpression2 = node.Indices[0];
				BoundExpression boundExpression3 = this.Visit(boundExpression2);
				if (!TypeSymbol.Equals(boundExpression3.Type, this._int32Type, TypeCompareKind.ConsiderEverything))
				{
					boundExpression3 = this.ConvertIndex(boundExpression3, boundExpression2.Type, this._int32Type);
				}
				return this.ExprFactory("ArrayIndex", new BoundExpression[] { boundExpression, boundExpression3 });
			}
			return this.ExprFactory("ArrayIndex", new BoundExpression[]
			{
				boundExpression,
				this.Indices(node.Indices)
			});
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000B29C8 File Offset: 0x000B0BC8
		private BoundExpression Indices(ImmutableArray<BoundExpression> expressions)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			foreach (BoundExpression boundExpression in expressions)
			{
				BoundExpression boundExpression2 = this.Visit(boundExpression);
				if (!TypeSymbol.Equals(boundExpression2.Type, this._int32Type, TypeCompareKind.ConsiderEverything))
				{
					boundExpression2 = this.ConvertIndex(boundExpression2, boundExpression.Type, this._int32Type);
				}
				instance.Add(boundExpression2);
			}
			return this._bound.ArrayOrEmpty(this.ExpressionType, instance.ToImmutableAndFree());
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000B2A44 File Offset: 0x000B0C44
		private BoundExpression Expressions(ImmutableArray<BoundExpression> expressions)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			foreach (BoundExpression boundExpression in expressions)
			{
				instance.Add(this.Visit(boundExpression));
			}
			return this._bound.ArrayOrEmpty(this.ExpressionType, instance.ToImmutableAndFree());
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000B2A98 File Offset: 0x000B0C98
		private BoundExpression VisitArrayCreation(BoundArrayCreation node)
		{
			ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)node.Type;
			BoundExpression boundExpression = this._bound.Typeof(arrayTypeSymbol.ElementType);
			if (node.InitializerOpt == null)
			{
				return this.ExprFactory("NewArrayBounds", new BoundExpression[]
				{
					boundExpression,
					this.Expressions(node.Bounds)
				});
			}
			if (arrayTypeSymbol.IsSZArray)
			{
				return this.ExprFactory("NewArrayInit", new BoundExpression[]
				{
					boundExpression,
					this.Expressions(node.InitializerOpt.Initializers)
				});
			}
			return new BoundBadExpression(node.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), this.ExpressionType);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000B2B3F File Offset: 0x000B0D3F
		private BoundExpression VisitArrayLength(BoundArrayLength node)
		{
			return this.ExprFactory("ArrayLength", new BoundExpression[] { this.Visit(node.Expression) });
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000B2B64 File Offset: 0x000B0D64
		private BoundExpression VisitAsOperator(BoundAsOperator node)
		{
			if (node.Operand.IsLiteralNull() && node.Operand.Type == null)
			{
				BoundExpression boundExpression = this._bound.Null(this._bound.SpecialType(SpecialType.System_Object));
				node = node.Update(boundExpression, node.TargetType, node.Conversion, node.Type);
			}
			return this.ExprFactory("TypeAs", new BoundExpression[]
			{
				this.Visit(node.Operand),
				this._bound.Typeof(node.Type)
			});
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000B2BF2 File Offset: 0x000B0DF2
		private BoundExpression VisitBaseReference(BoundBaseReference node)
		{
			return new BoundBadExpression(node.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), this.ExpressionType);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000B2C14 File Offset: 0x000B0E14
		private static string GetBinaryOperatorName(BinaryOperatorKind opKind, out bool isChecked, out bool isLifted, out bool requiresLifted)
		{
			isChecked = opKind.IsChecked();
			isLifted = opKind.IsLifted();
			requiresLifted = opKind.IsComparison();
			BinaryOperatorKind binaryOperatorKind = opKind.Operator();
			if (binaryOperatorKind <= BinaryOperatorKind.Equal)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.Division)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.Addition)
					{
						if (binaryOperatorKind != BinaryOperatorKind.Multiplication)
						{
							if (binaryOperatorKind == BinaryOperatorKind.Addition)
							{
								if (!isChecked)
								{
									return "Add";
								}
								return "AddChecked";
							}
						}
						else
						{
							if (!isChecked)
							{
								return "Multiply";
							}
							return "MultiplyChecked";
						}
					}
					else if (binaryOperatorKind != BinaryOperatorKind.Subtraction)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Division)
						{
							return "Divide";
						}
					}
					else
					{
						if (!isChecked)
						{
							return "Subtract";
						}
						return "SubtractChecked";
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.LeftShift)
				{
					if (binaryOperatorKind == BinaryOperatorKind.Remainder)
					{
						return "Modulo";
					}
					if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
					{
						return "LeftShift";
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.RightShift)
					{
						return "RightShift";
					}
					if (binaryOperatorKind == BinaryOperatorKind.Equal)
					{
						return "Equal";
					}
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.GreaterThanOrEqual)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
				{
					if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
					{
						return "NotEqual";
					}
					if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
					{
						return "GreaterThan";
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.LessThan)
					{
						return "LessThan";
					}
					if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return "GreaterThanOrEqual";
					}
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.And)
			{
				if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
				{
					return "LessThanOrEqual";
				}
				if (binaryOperatorKind == BinaryOperatorKind.And)
				{
					if (!opKind.IsLogical())
					{
						return "And";
					}
					return "AndAlso";
				}
			}
			else
			{
				if (binaryOperatorKind == BinaryOperatorKind.Xor)
				{
					return "ExclusiveOr";
				}
				if (binaryOperatorKind == BinaryOperatorKind.Or)
				{
					if (!opKind.IsLogical())
					{
						return "Or";
					}
					return "OrElse";
				}
			}
			throw ExceptionUtilities.UnexpectedValue(opKind.Operator());
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000B2DF4 File Offset: 0x000B0FF4
		private BoundExpression VisitBinaryOperator(BinaryOperatorKind opKind, MethodSymbol methodOpt, TypeSymbol type, BoundExpression left, BoundExpression right)
		{
			bool flag;
			bool flag2;
			bool flag3;
			string binaryOperatorName = ExpressionLambdaRewriter.GetBinaryOperatorName(opKind, out flag, out flag2, out flag3);
			if (left.Type == null && left.IsLiteralNull())
			{
				left = this._bound.Default(right.Type);
			}
			if (right.Type == null && right.IsLiteralNull())
			{
				right = this._bound.Default(left.Type);
			}
			BinaryOperatorKind binaryOperatorKind = opKind.OperandTypes();
			if (binaryOperatorKind - BinaryOperatorKind.Enum <= 2)
			{
				BoundExpression boundExpression = ((opKind.OperandTypes() == BinaryOperatorKind.UnderlyingAndEnum) ? right : left);
				TypeSymbol typeSymbol = this.PromotedType(boundExpression.Type.StrippedType().GetEnumUnderlyingType());
				if (opKind.IsLifted())
				{
					typeSymbol = this._nullableType.Construct(new TypeSymbol[] { typeSymbol });
				}
				BoundExpression boundExpression2 = this.VisitAndPromoteEnumOperand(left, typeSymbol, flag);
				BoundExpression boundExpression3 = this.VisitAndPromoteEnumOperand(right, typeSymbol, flag);
				BoundExpression boundExpression4 = this.MakeBinary(methodOpt, type, flag2, flag3, binaryOperatorName, boundExpression2, boundExpression3);
				return this.Demote(boundExpression4, type, flag);
			}
			BoundExpression boundExpression5 = this.Visit(left);
			BoundExpression boundExpression6 = this.Visit(right);
			return this.MakeBinary(methodOpt, type, flag2, flag3, binaryOperatorName, boundExpression5, boundExpression6);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000B2F14 File Offset: 0x000B1114
		private static BoundExpression DemoteEnumOperand(BoundExpression operand)
		{
			if (operand.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)operand;
				if (!boundConversion.ConversionKind.IsUserDefinedConversion() && boundConversion.ConversionKind.IsImplicitConversion() && boundConversion.ConversionKind != ConversionKind.NullLiteral && boundConversion.Type.StrippedType().IsEnumType())
				{
					operand = boundConversion.Operand;
				}
			}
			return operand;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000B2F74 File Offset: 0x000B1174
		private BoundExpression VisitAndPromoteEnumOperand(BoundExpression operand, TypeSymbol promotedType, bool isChecked)
		{
			BoundLiteral boundLiteral = operand as BoundLiteral;
			if (boundLiteral != null)
			{
				return this.Constant(boundLiteral.Update(boundLiteral.ConstantValue, promotedType));
			}
			BoundExpression boundExpression = ExpressionLambdaRewriter.DemoteEnumOperand(operand);
			BoundExpression boundExpression2 = this.Visit(boundExpression);
			return this.Convert(boundExpression2, operand.Type, promotedType, isChecked, false);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000B2FC0 File Offset: 0x000B11C0
		private BoundExpression MakeBinary(MethodSymbol methodOpt, TypeSymbol type, bool isLifted, bool requiresLifted, string opName, BoundExpression loweredLeft, BoundExpression loweredRight)
		{
			if (methodOpt == null)
			{
				return this.ExprFactory(opName, new BoundExpression[] { loweredLeft, loweredRight });
			}
			if (!requiresLifted)
			{
				return this.ExprFactory(opName, new BoundExpression[]
				{
					loweredLeft,
					loweredRight,
					this._bound.MethodInfo(methodOpt)
				});
			}
			return this.ExprFactory(opName, new BoundExpression[]
			{
				loweredLeft,
				loweredRight,
				this._bound.Literal(isLifted && !TypeSymbol.Equals(methodOpt.ReturnType, type, TypeCompareKind.ConsiderEverything)),
				this._bound.MethodInfo(methodOpt)
			});
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000B3060 File Offset: 0x000B1260
		private TypeSymbol PromotedType(TypeSymbol underlying)
		{
			if (underlying.SpecialType == SpecialType.System_Boolean)
			{
				return underlying;
			}
			SpecialType enumPromotedType = Binder.GetEnumPromotedType(underlying.SpecialType);
			if (enumPromotedType == underlying.SpecialType)
			{
				return underlying;
			}
			return this._bound.SpecialType(enumPromotedType);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000B309C File Offset: 0x000B129C
		private BoundExpression Demote(BoundExpression node, TypeSymbol type, bool isChecked)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol != null)
			{
				if (namedTypeSymbol.StrippedType().TypeKind == TypeKind.Enum)
				{
					return this.Convert(node, type, isChecked);
				}
				if (!TypeSymbol.Equals(namedTypeSymbol.IsNullableType() ? this._nullableType.Construct(new TypeSymbol[] { this.PromotedType(namedTypeSymbol.GetNullableUnderlyingType()) }) : this.PromotedType(namedTypeSymbol), type, TypeCompareKind.ConsiderEverything))
				{
					return this.Convert(node, type, isChecked);
				}
			}
			return node;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000B3110 File Offset: 0x000B1310
		private BoundExpression ConvertIndex(BoundExpression expr, TypeSymbol oldType, TypeSymbol newType)
		{
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(this.Diagnostics, this._bound.Compilation.Assembly);
			ConversionKind kind = this._bound.Compilation.Conversions.ClassifyConversionFromType(oldType, newType, ref compoundUseSiteInfo, false).Kind;
			this.Diagnostics.AddDependencies(compoundUseSiteInfo);
			if (kind == ConversionKind.Identity)
			{
				return expr;
			}
			if (kind != ConversionKind.ExplicitNumeric)
			{
				return this.Convert(expr, this._int32Type, false);
			}
			return this.Convert(expr, newType, true);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000B3190 File Offset: 0x000B1390
		private BoundExpression VisitCall(BoundCall node)
		{
			if (node.IsDelegateCall)
			{
				return this.ExprFactory("Invoke", new BoundExpression[]
				{
					this.Visit(node.ReceiverOpt),
					this.Expressions(node.Arguments)
				});
			}
			MethodSymbol method = node.Method;
			return this.ExprFactory("Call", new BoundExpression[]
			{
				method.RequiresInstanceReceiver ? this.Visit(node.ReceiverOpt) : this._bound.Null(this.ExpressionType),
				this._bound.MethodInfo(method),
				this.Expressions(node.Arguments)
			});
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000B3238 File Offset: 0x000B1438
		private BoundExpression VisitConditionalOperator(BoundConditionalOperator node)
		{
			BoundExpression boundExpression = this.Visit(node.Condition);
			BoundExpression boundExpression2 = this.VisitExactType(node.Consequence);
			BoundExpression boundExpression3 = this.VisitExactType(node.Alternative);
			return this.ExprFactory("Condition", new BoundExpression[] { boundExpression, boundExpression2, boundExpression3 });
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000B328C File Offset: 0x000B148C
		private BoundExpression VisitExactType(BoundExpression e)
		{
			BoundConversion boundConversion = e as BoundConversion;
			if (boundConversion != null && !boundConversion.ExplicitCastInCode)
			{
				BoundConversion boundConversion2 = boundConversion;
				BoundExpression operand = boundConversion.Operand;
				Conversion conversion = boundConversion.Conversion;
				bool isBaseConversion = boundConversion.IsBaseConversion;
				bool @checked = boundConversion.Checked;
				bool flag = true;
				ConversionGroup conversionGroupOpt = boundConversion.ConversionGroupOpt;
				e = boundConversion2.Update(operand, conversion, isBaseConversion, @checked, flag, boundConversion.ConstantValueOpt, conversionGroupOpt, boundConversion.Type);
			}
			return this.Visit(e);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000B32E8 File Offset: 0x000B14E8
		private BoundExpression VisitConversion(BoundConversion node)
		{
			ConversionKind conversionKind = node.ConversionKind;
			if (conversionKind <= ConversionKind.ImplicitUserDefined)
			{
				if (conversionKind != ConversionKind.Identity)
				{
					switch (conversionKind)
					{
					case ConversionKind.ImplicitNullable:
					{
						if (node.Operand.Type.IsNullableType())
						{
							return this.Convert(this.Visit(node.Operand), node.Operand.Type, node.Type, node.Checked, node.ExplicitCastInCode);
						}
						TypeSymbol type = ((NamedTypeSymbol)node.Type).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type;
						BoundExpression boundExpression = this.Convert(this.Visit(node.Operand), node.Operand.Type, type, node.Checked, false);
						return this.Convert(boundExpression, type, node.Type, node.Checked, false);
					}
					case ConversionKind.NullLiteral:
						return this.Convert(this.Constant(this._bound.Null(this._objectType)), this._objectType, node.Type, false, node.ExplicitCastInCode);
					case ConversionKind.ImplicitReference:
						break;
					default:
						if (conversionKind != ConversionKind.ImplicitUserDefined)
						{
							goto IL_02C8;
						}
						goto IL_0084;
					}
				}
				BoundExpression boundExpression2 = this.Visit(node.Operand);
				if (!node.ExplicitCastInCode)
				{
					return boundExpression2;
				}
				return this.Convert(boundExpression2, node.Type, false);
			}
			else
			{
				if (conversionKind == ConversionKind.MethodGroup)
				{
					BoundMethodGroup boundMethodGroup = (BoundMethodGroup)node.Operand;
					return this.DelegateCreation(boundMethodGroup.ReceiverOpt, node.SymbolOpt, node.Type, !node.SymbolOpt.RequiresInstanceReceiver && !node.IsExtensionMethod);
				}
				if (conversionKind != ConversionKind.ExplicitUserDefined && conversionKind != ConversionKind.IntPtr)
				{
					goto IL_02C8;
				}
			}
			IL_0084:
			MethodSymbol symbolOpt = node.SymbolOpt;
			TypeSymbol type2 = node.Operand.Type;
			TypeSymbol typeSymbol = type2.StrippedType();
			TypeSymbol type3 = symbolOpt.Parameters[0].Type;
			bool flag = !TypeSymbol.Equals(type2, type3, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(typeSymbol, type3, TypeCompareKind.ConsiderEverything);
			bool flag2 = !TypeSymbol.Equals(typeSymbol, (node.ConversionKind == ConversionKind.ExplicitUserDefined) ? type3 : type3.StrippedType(), TypeCompareKind.ConsiderEverything);
			TypeSymbol typeSymbol2 = ((flag && symbolOpt.ReturnType.IsNonNullableValueType() && node.Type.IsNullableType()) ? this._nullableType.Construct(new TypeSymbol[] { symbolOpt.ReturnType }) : symbolOpt.ReturnType);
			BoundExpression boundExpression3 = (flag2 ? this.Convert(this.Visit(node.Operand), node.Operand.Type, symbolOpt.Parameters[0].Type, node.Checked, false) : this.Visit(node.Operand));
			BoundExpression boundExpression4 = this.ExprFactory("Convert", new BoundExpression[]
			{
				boundExpression3,
				this._bound.Typeof(typeSymbol2),
				this._bound.MethodInfo(symbolOpt)
			});
			return this.Convert(boundExpression4, typeSymbol2, node.Type, node.Checked, false);
			IL_02C8:
			return this.Convert(this.Visit(node.Operand), node.Operand.Type, node.Type, node.Checked, node.ExplicitCastInCode);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000B35EC File Offset: 0x000B17EC
		private BoundExpression Convert(BoundExpression operand, TypeSymbol oldType, TypeSymbol newType, bool isChecked, bool isExplicit)
		{
			if (!TypeSymbol.Equals(oldType, newType, TypeCompareKind.ConsiderEverything) || isExplicit)
			{
				return this.Convert(operand, newType, isChecked);
			}
			return operand;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000B3608 File Offset: 0x000B1808
		private BoundExpression Convert(BoundExpression expr, TypeSymbol type, bool isChecked)
		{
			return this.ExprFactory(isChecked ? "ConvertChecked" : "Convert", new BoundExpression[]
			{
				expr,
				this._bound.Typeof(type)
			});
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000B3638 File Offset: 0x000B1838
		private BoundExpression DelegateCreation(BoundExpression receiver, MethodSymbol method, TypeSymbol delegateType, bool requiresInstanceReceiver)
		{
			BoundExpression boundExpression = this._bound.Null(this._objectType);
			receiver = (requiresInstanceReceiver ? boundExpression : (receiver.Type.IsReferenceType ? receiver : this._bound.Convert(this._objectType, receiver)));
			MethodSymbol methodSymbol = this._bound.WellKnownMethod(WellKnownMember.System_Reflection_MethodInfo__CreateDelegate, true);
			BoundExpression boundExpression2;
			if (methodSymbol != null)
			{
				boundExpression2 = this._bound.Call(this._bound.MethodInfo(method), methodSymbol, this._bound.Typeof(delegateType), receiver);
			}
			else
			{
				boundExpression2 = this._bound.StaticCall(this._bound.SpecialType(SpecialType.System_Delegate), "CreateDelegate", new BoundExpression[]
				{
					this._bound.Typeof(delegateType),
					receiver,
					this._bound.MethodInfo(method)
				});
			}
			return this.Convert(this.Visit(boundExpression2), delegateType, false);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000B3710 File Offset: 0x000B1910
		private BoundExpression VisitDelegateCreationExpression(BoundDelegateCreationExpression node)
		{
			if (node.Argument.Kind == BoundKind.MethodGroup)
			{
				throw ExceptionUtilities.UnexpectedValue(BoundKind.MethodGroup);
			}
			if (node.MethodOpt != null)
			{
				bool flag = !node.MethodOpt.RequiresInstanceReceiver && !node.IsExtensionMethod;
				return this.DelegateCreation(node.Argument, node.MethodOpt, node.Type, flag);
			}
			NamedTypeSymbol namedTypeSymbol = node.Argument.Type as NamedTypeSymbol;
			if (namedTypeSymbol != null && namedTypeSymbol.TypeKind == TypeKind.Delegate)
			{
				return this.DelegateCreation(node.Argument, namedTypeSymbol.DelegateInvokeMethod, node.Type, false);
			}
			throw ExceptionUtilities.UnexpectedValue(node.Argument);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000B37C0 File Offset: 0x000B19C0
		private BoundExpression VisitFieldAccess(BoundFieldAccess node)
		{
			BoundExpression boundExpression = (node.FieldSymbol.IsStatic ? this._bound.Null(this.ExpressionType) : this.Visit(node.ReceiverOpt));
			return this.ExprFactory("Field", new BoundExpression[]
			{
				boundExpression,
				this._bound.FieldInfo(node.FieldSymbol)
			});
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000B3824 File Offset: 0x000B1A24
		private BoundExpression VisitIsOperator(BoundIsOperator node)
		{
			BoundExpression boundExpression = node.Operand;
			if (boundExpression.Type == null && boundExpression.ConstantValue != null && boundExpression.ConstantValue.IsNull)
			{
				boundExpression = this._bound.Null(this._objectType);
			}
			return this.ExprFactory("TypeIs", new BoundExpression[]
			{
				this.Visit(boundExpression),
				this._bound.Typeof(node.TargetType.Type)
			});
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000B38A4 File Offset: 0x000B1AA4
		private BoundExpression VisitLambda(BoundLambda node)
		{
			BoundExpression boundExpression = this.VisitLambdaInternal(node);
			if (!node.Type.IsExpressionTree())
			{
				return boundExpression;
			}
			return this.ExprFactory("Quote", new BoundExpression[] { boundExpression });
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000B38E0 File Offset: 0x000B1AE0
		private BoundExpression VisitLambdaInternal(BoundLambda node)
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			ArrayBuilder<BoundExpression> instance3 = ArrayBuilder<BoundExpression>.GetInstance();
			foreach (ParameterSymbol parameterSymbol in node.Symbol.Parameters)
			{
				LocalSymbol localSymbol = this._bound.SynthesizedLocal(this.ParameterExpressionType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
				instance.Add(localSymbol);
				BoundLocal boundLocal = this._bound.Local(localSymbol);
				instance3.Add(boundLocal);
				BoundExpression boundExpression = this.ExprFactory("Parameter", new BoundExpression[]
				{
					this._bound.Typeof(this._typeMap.SubstituteType(parameterSymbol.Type).Type),
					this._bound.Literal(parameterSymbol.Name)
				});
				instance2.Add(this._bound.AssignmentExpression(boundLocal, boundExpression, false));
				this._parameterMap[parameterSymbol] = boundLocal;
			}
			NamedTypeSymbol delegateType = node.Type.GetDelegateType();
			BoundExpression boundExpression2 = this._bound.Sequence(instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), this.ExprFactory("Lambda", ImmutableArray.Create<TypeSymbol>(delegateType), new BoundExpression[]
			{
				this.TranslateLambdaBody(node.Body),
				this._bound.ArrayOrEmpty(this.ParameterExpressionType, instance3.ToImmutableAndFree())
			}));
			foreach (ParameterSymbol parameterSymbol2 in node.Symbol.Parameters)
			{
				this._parameterMap.Remove(parameterSymbol2);
			}
			return boundExpression2;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000B3A7B File Offset: 0x000B1C7B
		private BoundExpression VisitNewT(BoundNewT node)
		{
			return this.VisitObjectCreationContinued(this.ExprFactory("New", new BoundExpression[] { this._bound.Typeof(node.Type) }), node.InitializerExpressionOpt);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000B3AB0 File Offset: 0x000B1CB0
		private BoundExpression VisitNullCoalescingOperator(BoundNullCoalescingOperator node)
		{
			BoundExpression boundExpression = this.Visit(node.LeftOperand);
			BoundExpression boundExpression2 = this.Visit(node.RightOperand);
			if (node.LeftConversion.IsUserDefined)
			{
				TypeSymbol typeSymbol = node.LeftOperand.Type.StrippedType();
				return this.ExprFactory("Coalesce", new BoundExpression[]
				{
					boundExpression,
					boundExpression2,
					this.MakeConversionLambda(node.LeftConversion, typeSymbol, node.Type)
				});
			}
			return this.ExprFactory("Coalesce", new BoundExpression[] { boundExpression, boundExpression2 });
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000B3B44 File Offset: 0x000B1D44
		private BoundExpression MakeConversionLambda(Conversion conversion, TypeSymbol fromType, TypeSymbol toType)
		{
			string text = "p";
			ParameterSymbol parameterSymbol = this._bound.SynthesizedParameter(fromType, text, null, 0);
			LocalSymbol localSymbol = this._bound.SynthesizedLocal(this.ParameterExpressionType, null, false, RefKind.None, SynthesizedLocalKind.LoweringTemp);
			BoundLocal boundLocal = this._bound.Local(localSymbol);
			BoundExpression boundExpression = this.ExprFactory("Parameter", new BoundExpression[]
			{
				this._bound.Typeof(fromType),
				this._bound.Literal(text)
			});
			this._parameterMap[parameterSymbol] = boundLocal;
			BoundExpression boundExpression2 = this.Visit(this._bound.Convert(toType, this._bound.Parameter(parameterSymbol), conversion, false));
			this._parameterMap.Remove(parameterSymbol);
			return this._bound.Sequence(ImmutableArray.Create<LocalSymbol>(localSymbol), ImmutableArray.Create<BoundExpression>(this._bound.AssignmentExpression(boundLocal, boundExpression, false)), this.ExprFactory("Lambda", new BoundExpression[]
			{
				boundExpression2,
				this._bound.ArrayOrEmpty(this.ParameterExpressionType, ImmutableArray.Create<BoundExpression>(boundLocal))
			}));
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000B3C50 File Offset: 0x000B1E50
		private BoundExpression InitializerMemberSetter(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind == SymbolKind.Event)
			{
				return this._bound.Convert(this.MemberInfoType, this._bound.FieldInfo(((EventSymbol)symbol).AssociatedField));
			}
			if (kind == SymbolKind.Field)
			{
				return this._bound.Convert(this.MemberInfoType, this._bound.FieldInfo((FieldSymbol)symbol));
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			}
			return this._bound.MethodInfo(((PropertySymbol)symbol).GetOwnOrInheritedSetMethod());
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000B3CE8 File Offset: 0x000B1EE8
		private BoundExpression InitializerMemberGetter(Symbol symbol)
		{
			SymbolKind kind = symbol.Kind;
			if (kind == SymbolKind.Event)
			{
				return this._bound.Convert(this.MemberInfoType, this._bound.FieldInfo(((EventSymbol)symbol).AssociatedField));
			}
			if (kind == SymbolKind.Field)
			{
				return this._bound.Convert(this.MemberInfoType, this._bound.FieldInfo((FieldSymbol)symbol));
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(symbol.Kind);
			}
			return this._bound.MethodInfo(((PropertySymbol)symbol).GetOwnOrInheritedGetMethod());
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000B3D80 File Offset: 0x000B1F80
		private BoundExpression VisitInitializer(BoundExpression node, out ExpressionLambdaRewriter.InitializerKind kind)
		{
			BoundKind kind2 = node.Kind;
			if (kind2 == BoundKind.ObjectInitializerExpression)
			{
				BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase = (BoundObjectInitializerExpression)node;
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
				foreach (BoundExpression boundExpression in boundObjectInitializerExpressionBase.Initializers)
				{
					BoundAssignmentOperator boundAssignmentOperator = (BoundAssignmentOperator)boundExpression;
					Symbol memberSymbol = ((BoundObjectInitializerMember)boundAssignmentOperator.Left).MemberSymbol;
					ExpressionLambdaRewriter.InitializerKind initializerKind;
					BoundExpression boundExpression2 = this.VisitInitializer(boundAssignmentOperator.Right, out initializerKind);
					switch (initializerKind)
					{
					case ExpressionLambdaRewriter.InitializerKind.Expression:
					{
						BoundExpression boundExpression3 = this.InitializerMemberSetter(memberSymbol);
						instance.Add(this.ExprFactory("Bind", new BoundExpression[] { boundExpression3, boundExpression2 }));
						break;
					}
					case ExpressionLambdaRewriter.InitializerKind.MemberInitializer:
					{
						BoundExpression boundExpression4 = this.InitializerMemberGetter(memberSymbol);
						instance.Add(this.ExprFactory("MemberBind", new BoundExpression[] { boundExpression4, boundExpression2 }));
						break;
					}
					case ExpressionLambdaRewriter.InitializerKind.CollectionInitializer:
					{
						BoundExpression boundExpression5 = this.InitializerMemberGetter(memberSymbol);
						instance.Add(this.ExprFactory("ListBind", new BoundExpression[] { boundExpression5, boundExpression2 }));
						break;
					}
					default:
						throw ExceptionUtilities.UnexpectedValue(initializerKind);
					}
				}
				kind = ExpressionLambdaRewriter.InitializerKind.MemberInitializer;
				return this._bound.ArrayOrEmpty(this.MemberBindingType, instance.ToImmutableAndFree());
			}
			if (kind2 != BoundKind.CollectionInitializerExpression)
			{
				kind = ExpressionLambdaRewriter.InitializerKind.Expression;
				return this.Visit(node);
			}
			BoundObjectInitializerExpressionBase boundObjectInitializerExpressionBase2 = (BoundCollectionInitializerExpression)node;
			kind = ExpressionLambdaRewriter.InitializerKind.CollectionInitializer;
			ArrayBuilder<BoundExpression> instance2 = ArrayBuilder<BoundExpression>.GetInstance();
			foreach (BoundExpression boundExpression6 in boundObjectInitializerExpressionBase2.Initializers)
			{
				BoundCollectionElementInitializer boundCollectionElementInitializer = (BoundCollectionElementInitializer)boundExpression6;
				BoundExpression boundExpression7 = this.ExprFactory("ElementInit", new BoundExpression[]
				{
					this._bound.MethodInfo(boundCollectionElementInitializer.AddMethod),
					this.Expressions(boundCollectionElementInitializer.Arguments)
				});
				instance2.Add(boundExpression7);
			}
			return this._bound.ArrayOrEmpty(this.ElementInitType, instance2.ToImmutableAndFree());
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000B3F6B File Offset: 0x000B216B
		private BoundExpression VisitObjectCreationExpression(BoundObjectCreationExpression node)
		{
			return this.VisitObjectCreationContinued(this.VisitObjectCreationExpressionInternal(node), node.InitializerExpressionOpt);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000B3F80 File Offset: 0x000B2180
		private BoundExpression VisitObjectCreationContinued(BoundExpression creation, BoundExpression initializerExpressionOpt)
		{
			if (initializerExpressionOpt == null)
			{
				return creation;
			}
			ExpressionLambdaRewriter.InitializerKind initializerKind;
			BoundExpression boundExpression = this.VisitInitializer(initializerExpressionOpt, out initializerKind);
			if (initializerKind == ExpressionLambdaRewriter.InitializerKind.MemberInitializer)
			{
				return this.ExprFactory("MemberInit", new BoundExpression[] { creation, boundExpression });
			}
			if (initializerKind == ExpressionLambdaRewriter.InitializerKind.CollectionInitializer)
			{
				return this.ExprFactory("ListInit", new BoundExpression[] { creation, boundExpression });
			}
			throw ExceptionUtilities.UnexpectedValue(initializerKind);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000B3FE8 File Offset: 0x000B21E8
		private BoundExpression VisitObjectCreationExpressionInternal(BoundObjectCreationExpression node)
		{
			if (node.ConstantValue != null)
			{
				return this.Constant(node);
			}
			if (node.Constructor == null || (node.Arguments.Length == 0 && !node.Type.IsStructType()) || node.Constructor.IsDefaultValueTypeConstructor())
			{
				return this.ExprFactory("New", new BoundExpression[] { this._bound.Typeof(node.Type) });
			}
			BoundExpression boundExpression = this._bound.ConstructorInfo(node.Constructor);
			BoundExpression boundExpression2 = this._bound.Convert(this._IEnumerableType.Construct(new TypeSymbol[] { this.ExpressionType }), this.Expressions(node.Arguments));
			if (node.Type.IsAnonymousType && node.Arguments.Length != 0)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)node.Type;
				ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
				for (int i = 0; i < node.Arguments.Length; i++)
				{
					instance.Add(this._bound.MethodInfo(AnonymousTypeManager.GetAnonymousTypeProperty(namedTypeSymbol, i).GetMethod));
				}
				return this.ExprFactory("New", new BoundExpression[]
				{
					boundExpression,
					boundExpression2,
					this._bound.ArrayOrEmpty(this.MemberInfoType, instance.ToImmutableAndFree())
				});
			}
			return this.ExprFactory("New", new BoundExpression[] { boundExpression, boundExpression2 });
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000B4167 File Offset: 0x000B2367
		private BoundExpression VisitParameter(BoundParameter node)
		{
			return this._parameterMap[node.ParameterSymbol];
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000B417A File Offset: 0x000B237A
		private static BoundExpression VisitPointerIndirectionOperator(BoundPointerIndirectionOperator node)
		{
			return new BoundBadExpression(node.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), node.Type);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000B4199 File Offset: 0x000B2399
		private static BoundExpression VisitPointerElementAccess(BoundPointerElementAccess node)
		{
			return new BoundBadExpression(node.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), node.Type);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000B41B8 File Offset: 0x000B23B8
		private BoundExpression VisitPropertyAccess(BoundPropertyAccess node)
		{
			BoundExpression boundExpression = (node.PropertySymbol.IsStatic ? this._bound.Null(this.ExpressionType) : this.Visit(node.ReceiverOpt));
			MethodSymbol ownOrInheritedGetMethod = node.PropertySymbol.GetOwnOrInheritedGetMethod();
			BoundExpression receiverOpt = node.ReceiverOpt;
			if (receiverOpt != null && receiverOpt.Type.IsTypeParameter() && !node.ReceiverOpt.Type.IsReferenceType)
			{
				boundExpression = this.Convert(boundExpression, ownOrInheritedGetMethod.ReceiverType, false);
			}
			return this.ExprFactory("Property", new BoundExpression[]
			{
				boundExpression,
				this._bound.MethodInfo(ownOrInheritedGetMethod)
			});
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000B425C File Offset: 0x000B245C
		private static BoundExpression VisitSizeOfOperator(BoundSizeOfOperator node)
		{
			return new BoundBadExpression(node.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(node), node.Type);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000B427C File Offset: 0x000B247C
		private BoundExpression VisitUnaryOperator(BoundUnaryOperator node)
		{
			BoundExpression operand = node.Operand;
			BoundExpression boundExpression = this.Visit(operand);
			UnaryOperatorKind operatorKind = node.OperatorKind;
			UnaryOperatorKind unaryOperatorKind = operatorKind & UnaryOperatorKind.OpMask;
			bool flag = (operatorKind & UnaryOperatorKind.Checked) > UnaryOperatorKind.Error;
			string text;
			if (unaryOperatorKind <= UnaryOperatorKind.UnaryMinus)
			{
				if (unaryOperatorKind != UnaryOperatorKind.UnaryPlus)
				{
					if (unaryOperatorKind == UnaryOperatorKind.UnaryMinus)
					{
						text = (flag ? "NegateChecked" : "Negate");
						goto IL_0092;
					}
				}
				else
				{
					if (node.MethodOpt == null)
					{
						return boundExpression;
					}
					text = "UnaryPlus";
					goto IL_0092;
				}
			}
			else if (unaryOperatorKind == UnaryOperatorKind.LogicalNegation || unaryOperatorKind == UnaryOperatorKind.BitwiseComplement)
			{
				text = "Not";
				goto IL_0092;
			}
			throw ExceptionUtilities.UnexpectedValue(unaryOperatorKind);
			IL_0092:
			if (node.OperatorKind.OperandTypes() == UnaryOperatorKind.Enum && (operatorKind & UnaryOperatorKind.Lifted) != UnaryOperatorKind.Error)
			{
				TypeSymbol typeSymbol = this.PromotedType(operand.Type.StrippedType().GetEnumUnderlyingType());
				typeSymbol = this._nullableType.Construct(new TypeSymbol[] { typeSymbol });
				boundExpression = this.Convert(boundExpression, operand.Type, typeSymbol, flag, false);
				BoundExpression boundExpression2 = this.ExprFactory(text, new BoundExpression[] { boundExpression });
				return this.Demote(boundExpression2, node.Type, flag);
			}
			if (node.MethodOpt != null)
			{
				return this.ExprFactory(text, new BoundExpression[]
				{
					boundExpression,
					this._bound.MethodInfo(node.MethodOpt)
				});
			}
			return this.ExprFactory(text, new BoundExpression[] { boundExpression });
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000B43DC File Offset: 0x000B25DC
		private BoundExpression ExprFactory(string name, params BoundExpression[] arguments)
		{
			return this._bound.StaticCall(this.ExpressionType, name, arguments);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000B43F1 File Offset: 0x000B25F1
		private BoundExpression ExprFactory(string name, ImmutableArray<TypeSymbol> typeArgs, params BoundExpression[] arguments)
		{
			return this._bound.StaticCall(this._ignoreAccessibility ? BinderFlags.IgnoreAccessibility : BinderFlags.None, this.ExpressionType, name, typeArgs, arguments);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000B4417 File Offset: 0x000B2617
		private BoundExpression Constant(BoundExpression node)
		{
			return this.ExprFactory("Constant", new BoundExpression[]
			{
				this._bound.Convert(this._objectType, node),
				this._bound.Typeof(node.Type)
			});
		}

		// Token: 0x04001082 RID: 4226
		private readonly SyntheticBoundNodeFactory _bound;

		// Token: 0x04001083 RID: 4227
		private readonly TypeMap _typeMap;

		// Token: 0x04001084 RID: 4228
		private readonly Dictionary<ParameterSymbol, BoundExpression> _parameterMap = new Dictionary<ParameterSymbol, BoundExpression>();

		// Token: 0x04001085 RID: 4229
		private readonly bool _ignoreAccessibility;

		// Token: 0x04001086 RID: 4230
		private int _recursionDepth;

		// Token: 0x04001087 RID: 4231
		private NamedTypeSymbol _ExpressionType;

		// Token: 0x04001088 RID: 4232
		private NamedTypeSymbol _ParameterExpressionType;

		// Token: 0x04001089 RID: 4233
		private NamedTypeSymbol _ElementInitType;

		// Token: 0x0400108A RID: 4234
		private NamedTypeSymbol _MemberBindingType;

		// Token: 0x0400108B RID: 4235
		private readonly NamedTypeSymbol _int32Type;

		// Token: 0x0400108C RID: 4236
		private readonly NamedTypeSymbol _objectType;

		// Token: 0x0400108D RID: 4237
		private readonly NamedTypeSymbol _nullableType;

		// Token: 0x0400108E RID: 4238
		private NamedTypeSymbol _MemberInfoType;

		// Token: 0x0400108F RID: 4239
		private readonly NamedTypeSymbol _IEnumerableType;

		// Token: 0x02000724 RID: 1828
		private enum InitializerKind
		{
			// Token: 0x040022A4 RID: 8868
			Expression,
			// Token: 0x040022A5 RID: 8869
			MemberInitializer,
			// Token: 0x040022A6 RID: 8870
			CollectionInitializer
		}
	}
}
