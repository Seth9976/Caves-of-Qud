using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000117 RID: 279
	internal static class MethodBodySynthesizer
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x0007DF68 File Offset: 0x0007C168
		internal static ImmutableArray<BoundStatement> ConstructScriptConstructorBody(BoundStatement loweredBody, MethodSymbol constructor, SynthesizedSubmissionFields previousSubmissionFields, CSharpCompilation compilation)
		{
			SyntaxNode syntax = loweredBody.Syntax;
			NamedTypeSymbol specialType = constructor.ContainingAssembly.GetSpecialType(SpecialType.System_Object);
			BoundExpression boundExpression = new BoundThisReference(syntax, constructor.ContainingType)
			{
				WasCompilerGenerated = true
			};
			BoundStatement boundStatement = new BoundExpressionStatement(syntax, new BoundCall(syntax, boundExpression, specialType.InstanceConstructors[0], ImmutableArray<BoundExpression>.Empty, ImmutableArray<string>.Empty, ImmutableArray<RefKind>.Empty, false, false, false, ImmutableArray<int>.Empty, BitVector.Empty, LookupResultKind.Viable, specialType, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			};
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			instance.Add(boundStatement);
			if (constructor.IsSubmissionConstructor)
			{
				MethodBodySynthesizer.MakeSubmissionInitialization(instance, syntax, constructor, previousSubmissionFields, compilation);
			}
			instance.Add(loweredBody);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0007E01C File Offset: 0x0007C21C
		private static void MakeSubmissionInitialization(ArrayBuilder<BoundStatement> statements, SyntaxNode syntax, MethodSymbol submissionConstructor, SynthesizedSubmissionFields synthesizedFields, CSharpCompilation compilation)
		{
			BoundParameter boundParameter = new BoundParameter(syntax, submissionConstructor.Parameters[0])
			{
				WasCompilerGenerated = true
			};
			NamedTypeSymbol specialType = compilation.GetSpecialType(SpecialType.System_Int32);
			NamedTypeSymbol specialType2 = compilation.GetSpecialType(SpecialType.System_Object);
			BoundThisReference boundThisReference = new BoundThisReference(syntax, submissionConstructor.ContainingType)
			{
				WasCompilerGenerated = true
			};
			int submissionSlotIndex = compilation.GetSubmissionSlotIndex();
			statements.Add(new BoundExpressionStatement(syntax, new BoundAssignmentOperator(syntax, new BoundArrayAccess(syntax, boundParameter, ImmutableArray.Create<BoundExpression>(new BoundLiteral(syntax, ConstantValue.Create(submissionSlotIndex), specialType)
			{
				WasCompilerGenerated = true
			}), specialType2, false)
			{
				WasCompilerGenerated = true
			}, boundThisReference, false, boundThisReference.Type, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			});
			FieldSymbol hostObjectField = synthesizedFields.GetHostObjectField();
			if (hostObjectField != null)
			{
				statements.Add(new BoundExpressionStatement(syntax, new BoundAssignmentOperator(syntax, new BoundFieldAccess(syntax, boundThisReference, hostObjectField, null, false)
				{
					WasCompilerGenerated = true
				}, BoundConversion.Synthesized(syntax, new BoundArrayAccess(syntax, boundParameter, ImmutableArray.Create<BoundExpression>(new BoundLiteral(syntax, ConstantValue.Create(0), specialType)
				{
					WasCompilerGenerated = true
				}), specialType2, false), Conversion.ExplicitReference, false, true, null, null, hostObjectField.Type, false), hostObjectField.Type, false, false)
				{
					WasCompilerGenerated = true
				}, false)
				{
					WasCompilerGenerated = true
				});
			}
			foreach (FieldSymbol fieldSymbol in synthesizedFields.FieldSymbols)
			{
				ImplicitNamedTypeSymbol implicitNamedTypeSymbol = (ImplicitNamedTypeSymbol)fieldSymbol.Type;
				int submissionSlotIndex2 = implicitNamedTypeSymbol.DeclaringCompilation.GetSubmissionSlotIndex();
				statements.Add(new BoundExpressionStatement(syntax, new BoundAssignmentOperator(syntax, new BoundFieldAccess(syntax, boundThisReference, fieldSymbol, null, false)
				{
					WasCompilerGenerated = true
				}, BoundConversion.Synthesized(syntax, new BoundArrayAccess(syntax, boundParameter, ImmutableArray.Create<BoundExpression>(new BoundLiteral(syntax, ConstantValue.Create(submissionSlotIndex2), specialType)
				{
					WasCompilerGenerated = true
				}), specialType2, false)
				{
					WasCompilerGenerated = true
				}, Conversion.ExplicitReference, false, true, null, null, implicitNamedTypeSymbol, false), implicitNamedTypeSymbol, false, false)
				{
					WasCompilerGenerated = true
				}, false)
				{
					WasCompilerGenerated = true
				});
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0007E220 File Offset: 0x0007C420
		internal static BoundBlock ConstructAutoPropertyAccessorBody(SourceMemberMethodSymbol accessor)
		{
			SourcePropertySymbolBase sourcePropertySymbolBase = (SourcePropertySymbolBase)accessor.AssociatedSymbol;
			CSharpSyntaxNode csharpSyntaxNode = sourcePropertySymbolBase.CSharpSyntaxNode;
			BoundExpression boundExpression = null;
			if (!accessor.IsStatic)
			{
				ParameterSymbol thisParameter = accessor.ThisParameter;
				boundExpression = new BoundThisReference(csharpSyntaxNode, thisParameter.Type)
				{
					WasCompilerGenerated = true
				};
			}
			SynthesizedBackingFieldSymbol backingField = sourcePropertySymbolBase.BackingField;
			BoundFieldAccess boundFieldAccess = new BoundFieldAccess(csharpSyntaxNode, boundExpression, backingField, null, false)
			{
				WasCompilerGenerated = true
			};
			BoundStatement boundStatement;
			if (accessor.MethodKind == MethodKind.PropertyGet)
			{
				boundStatement = new BoundReturnStatement(accessor.SyntaxNode, RefKind.None, boundFieldAccess, false);
			}
			else
			{
				ParameterSymbol parameterSymbol = accessor.Parameters[0];
				boundStatement = new BoundExpressionStatement(accessor.SyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, boundFieldAccess, new BoundParameter(csharpSyntaxNode, parameterSymbol)
				{
					WasCompilerGenerated = true
				}, sourcePropertySymbolBase.Type, false, false)
				{
					WasCompilerGenerated = true
				}, false);
			}
			return BoundBlock.SynthesizedNoLocals(csharpSyntaxNode, boundStatement);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0007E2EC File Offset: 0x0007C4EC
		internal static BoundBlock ConstructFieldLikeEventAccessorBody(SourceEventSymbol eventSymbol, bool isAddMethod, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			if (!eventSymbol.IsWindowsRuntimeEvent)
			{
				return MethodBodySynthesizer.ConstructFieldLikeEventAccessorBody_Regular(eventSymbol, isAddMethod, compilation, diagnostics);
			}
			return MethodBodySynthesizer.ConstructFieldLikeEventAccessorBody_WinRT(eventSymbol, isAddMethod, compilation, diagnostics);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0007E30C File Offset: 0x0007C50C
		internal static BoundBlock ConstructFieldLikeEventAccessorBody_WinRT(SourceEventSymbol eventSymbol, bool isAddMethod, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			CSharpSyntaxNode csharpSyntaxNode = eventSymbol.CSharpSyntaxNode;
			MethodSymbol methodSymbol = (isAddMethod ? eventSymbol.AddMethod : eventSymbol.RemoveMethod);
			FieldSymbol associatedField = eventSymbol.AssociatedField;
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)associatedField.Type;
			MethodSymbol methodSymbol2 = (MethodSymbol)Binder.GetWellKnownTypeMember(compilation, WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T__GetOrCreateEventRegistrationTokenTable, diagnostics, null, csharpSyntaxNode, false);
			if (methodSymbol2 == null)
			{
				return null;
			}
			methodSymbol2 = methodSymbol2.AsMember(namedTypeSymbol);
			WellKnownMember wellKnownMember = (isAddMethod ? WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T__AddEventHandler : WellKnownMember.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationTokenTable_T__RemoveEventHandler);
			MethodSymbol methodSymbol3 = (MethodSymbol)Binder.GetWellKnownTypeMember(compilation, wellKnownMember, diagnostics, null, csharpSyntaxNode, false);
			if (methodSymbol3 == null)
			{
				return null;
			}
			methodSymbol3 = methodSymbol3.AsMember(namedTypeSymbol);
			BoundFieldAccess boundFieldAccess = new BoundFieldAccess(csharpSyntaxNode, associatedField.IsStatic ? null : new BoundThisReference(csharpSyntaxNode, methodSymbol.ThisParameter.Type), associatedField, null, false)
			{
				WasCompilerGenerated = true
			};
			BoundCall boundCall = BoundCall.Synthesized(csharpSyntaxNode, null, methodSymbol2, boundFieldAccess);
			BoundParameter boundParameter = new BoundParameter(csharpSyntaxNode, methodSymbol.Parameters[0]);
			BoundCall boundCall2 = BoundCall.Synthesized(csharpSyntaxNode, boundCall, methodSymbol3, boundParameter);
			if (isAddMethod)
			{
				BoundStatement boundStatement = BoundReturnStatement.Synthesized(csharpSyntaxNode, RefKind.None, boundCall2, false);
				return BoundBlock.SynthesizedNoLocals(csharpSyntaxNode, boundStatement);
			}
			BoundStatement boundStatement2 = new BoundExpressionStatement(csharpSyntaxNode, boundCall2, false);
			BoundStatement boundStatement3 = new BoundReturnStatement(csharpSyntaxNode, RefKind.None, null, false);
			return BoundBlock.SynthesizedNoLocals(csharpSyntaxNode, new BoundStatement[] { boundStatement2, boundStatement3 });
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0007E43C File Offset: 0x0007C63C
		internal static BoundBlock ConstructFieldLikeEventAccessorBody_Regular(SourceEventSymbol eventSymbol, bool isAddMethod, CSharpCompilation compilation, BindingDiagnosticBag diagnostics)
		{
			CSharpSyntaxNode csharpSyntaxNode = eventSymbol.CSharpSyntaxNode;
			TypeSymbol type = eventSymbol.Type;
			MethodSymbol methodSymbol = (isAddMethod ? eventSymbol.AddMethod : eventSymbol.RemoveMethod);
			ParameterSymbol thisParameter = methodSymbol.ThisParameter;
			TypeSymbol specialType = compilation.GetSpecialType(SpecialType.System_Boolean);
			SpecialMember specialMember = (isAddMethod ? SpecialMember.System_Delegate__Combine : SpecialMember.System_Delegate__Remove);
			MethodSymbol methodSymbol2 = (MethodSymbol)compilation.GetSpecialTypeMember(specialMember);
			BoundStatement boundStatement = new BoundReturnStatement(csharpSyntaxNode, RefKind.None, null, false)
			{
				WasCompilerGenerated = true
			};
			if (methodSymbol2 == null)
			{
				MemberDescriptor descriptor = SpecialMembers.GetDescriptor(specialMember);
				diagnostics.Add(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[] { descriptor.DeclaringTypeMetadataName, descriptor.Name }), csharpSyntaxNode.Location, false));
				return BoundBlock.SynthesizedNoLocals(csharpSyntaxNode, boundStatement);
			}
			Binder.ReportUseSite(methodSymbol2, diagnostics, csharpSyntaxNode);
			BoundThisReference boundThisReference;
			if (!eventSymbol.IsStatic)
			{
				(boundThisReference = new BoundThisReference(csharpSyntaxNode, thisParameter.Type)).WasCompilerGenerated = true;
			}
			else
			{
				boundThisReference = null;
			}
			BoundThisReference boundThisReference2 = boundThisReference;
			BoundFieldAccess boundFieldAccess = new BoundFieldAccess(csharpSyntaxNode, boundThisReference2, eventSymbol.AssociatedField, null, false)
			{
				WasCompilerGenerated = true
			};
			BoundParameter boundParameter = new BoundParameter(csharpSyntaxNode, methodSymbol.Parameters[0])
			{
				WasCompilerGenerated = true
			};
			MethodSymbol methodSymbol3 = (MethodSymbol)compilation.GetWellKnownTypeMember(WellKnownMember.System_Threading_Interlocked__CompareExchange_T);
			BoundExpression boundExpression;
			if (methodSymbol3 == null)
			{
				boundExpression = BoundConversion.SynthesizedNonUserDefined(csharpSyntaxNode, BoundCall.Synthesized(csharpSyntaxNode, null, methodSymbol2, ImmutableArray.Create<BoundExpression>(boundFieldAccess, boundParameter)), Conversion.ExplicitReference, type, null);
				BoundStatement boundStatement2 = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, boundFieldAccess, boundExpression, type, false, false)
				{
					WasCompilerGenerated = true
				}, false)
				{
					WasCompilerGenerated = true
				};
				return BoundBlock.SynthesizedNoLocals(csharpSyntaxNode, ImmutableArray.Create<BoundStatement>(boundStatement2, boundStatement));
			}
			methodSymbol3 = methodSymbol3.Construct(ImmutableArray.Create<TypeSymbol>(type));
			Binder.ReportUseSite(methodSymbol3, diagnostics, csharpSyntaxNode);
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("loop");
			LocalSymbol[] array = new LocalSymbol[3];
			BoundLocal[] array2 = new BoundLocal[3];
			for (int i = 0; i < 3; i++)
			{
				array[i] = new SynthesizedLocal(methodSymbol, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LoweringTemp, null, false, RefKind.None);
				array2[i] = new BoundLocal(csharpSyntaxNode, array[i], null, type, false)
				{
					WasCompilerGenerated = true
				};
			}
			BoundStatement boundStatement3 = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, array2[0], boundFieldAccess, type, false, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			};
			BoundStatement boundStatement4 = new BoundLabelStatement(csharpSyntaxNode, generatedLabelSymbol)
			{
				WasCompilerGenerated = true
			};
			BoundStatement boundStatement5 = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, array2[1], array2[0], type, false, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			};
			boundExpression = BoundConversion.SynthesizedNonUserDefined(csharpSyntaxNode, BoundCall.Synthesized(csharpSyntaxNode, null, methodSymbol2, ImmutableArray.Create<BoundExpression>(array2[1], boundParameter)), Conversion.ExplicitReference, type, null);
			BoundStatement boundStatement6 = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, array2[2], boundExpression, type, false, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			};
			BoundExpression boundExpression2 = BoundCall.Synthesized(csharpSyntaxNode, null, methodSymbol3, ImmutableArray.Create<BoundExpression>(boundFieldAccess, array2[2], array2[1]));
			BoundStatement boundStatement7 = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, array2[0], boundExpression2, type, false, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = true
			};
			BoundExpression boundExpression3 = new BoundBinaryOperator(csharpSyntaxNode, BinaryOperatorKind.ObjectEqual, null, null, LookupResultKind.Viable, array2[0], array2[1], specialType, false)
			{
				WasCompilerGenerated = true
			};
			BoundStatement boundStatement8 = new BoundConditionalGoto(csharpSyntaxNode, boundExpression3, false, generatedLabelSymbol, false)
			{
				WasCompilerGenerated = true
			};
			return new BoundBlock(csharpSyntaxNode, array.AsImmutable<LocalSymbol>(), ImmutableArray.Create<BoundStatement>(new BoundStatement[] { boundStatement3, boundStatement4, boundStatement5, boundStatement6, boundStatement7, boundStatement8, boundStatement }), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0007E7A8 File Offset: 0x0007C9A8
		internal static BoundBlock ConstructDestructorBody(MethodSymbol method, BoundBlock block)
		{
			SyntaxNode syntax = block.Syntax;
			MethodSymbol baseTypeFinalizeMethod = MethodBodySynthesizer.GetBaseTypeFinalizeMethod(method);
			if (baseTypeFinalizeMethod != null)
			{
				BoundStatement boundStatement = new BoundExpressionStatement(syntax, BoundCall.Synthesized(syntax, new BoundBaseReference(syntax, method.ContainingType)
				{
					WasCompilerGenerated = true
				}, baseTypeFinalizeMethod), false)
				{
					WasCompilerGenerated = true
				};
				if (syntax.Kind() == SyntaxKind.Block)
				{
					boundStatement = new BoundSequencePointWithSpan(syntax, boundStatement, ((BlockSyntax)syntax).CloseBraceToken.Span, false);
				}
				return new BoundBlock(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(new BoundTryStatement(syntax, block, ImmutableArray<BoundCatchBlock>.Empty, new BoundBlock(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(boundStatement), false)
				{
					WasCompilerGenerated = true
				}, null)
				{
					WasCompilerGenerated = true
				}), false);
			}
			return block;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0007E85C File Offset: 0x0007CA5C
		private static MethodSymbol GetBaseTypeFinalizeMethod(MethodSymbol method)
		{
			for (NamedTypeSymbol namedTypeSymbol = method.ContainingType.BaseTypeNoUseSiteDiagnostics; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeNoUseSiteDiagnostics)
			{
				foreach (Symbol symbol in namedTypeSymbol.GetMembers("Finalize"))
				{
					if (symbol.Kind == SymbolKind.Method)
					{
						MethodSymbol methodSymbol = (MethodSymbol)symbol;
						Accessibility declaredAccessibility = methodSymbol.DeclaredAccessibility;
						if ((declaredAccessibility == Accessibility.ProtectedOrInternal || declaredAccessibility == Accessibility.Protected) && methodSymbol.ParameterCount == 0 && methodSymbol.Arity == 0 && methodSymbol.ReturnsVoid)
						{
							return methodSymbol;
						}
					}
				}
			}
			return null;
		}
	}
}
