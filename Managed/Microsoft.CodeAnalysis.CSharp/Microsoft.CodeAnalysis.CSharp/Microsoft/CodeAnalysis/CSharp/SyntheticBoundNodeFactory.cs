using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000216 RID: 534
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SyntheticBoundNodeFactory
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000D09F0 File Offset: 0x000CEBF0
		public CSharpCompilation Compilation
		{
			get
			{
				return this.CompilationState.Compilation;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000D09FD File Offset: 0x000CEBFD
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x000D0A05 File Offset: 0x000CEC05
		public SyntaxNode Syntax { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000D0A0E File Offset: 0x000CEC0E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public PEModuleBuilder ModuleBuilderOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.CompilationState.ModuleBuilderOpt;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000D0A1B File Offset: 0x000CEC1B
		public BindingDiagnosticBag Diagnostics { get; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x000D0A23 File Offset: 0x000CEC23
		public TypeCompilationState CompilationState { get; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000D0A2B File Offset: 0x000CEC2B
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000D0A33 File Offset: 0x000CEC33
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NamedTypeSymbol CurrentType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._currentType;
			}
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			set
			{
				this._currentType = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000D0A3C File Offset: 0x000CEC3C
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000D0A44 File Offset: 0x000CEC44
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol CurrentFunction
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._currentFunction;
			}
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			set
			{
				this._currentFunction = value;
				if (value != null && value.MethodKind != MethodKind.AnonymousFunction && value.MethodKind != MethodKind.LocalFunction)
				{
					this._topLevelMethod = value;
					this._currentType = value.ContainingType;
				}
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000D0A75 File Offset: 0x000CEC75
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x000D0A7D File Offset: 0x000CEC7D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol TopLevelMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._topLevelMethod;
			}
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			private set
			{
				this._topLevelMethod = value;
			}
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000D0A88 File Offset: 0x000CEC88
		internal BoundExpression MakeInvocationExpression(BinderFlags flags, SyntaxNode node, BoundExpression receiver, string methodName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args, BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeSymbol> typeArgs = default(ImmutableArray<TypeSymbol>), bool allowUnexpandedForm = true)
		{
			if (this._binder == null || this._binder.Flags != flags)
			{
				this._binder = new SyntheticBoundNodeFactory.SyntheticBinderImpl(this).WithFlags(flags);
			}
			Binder binder = this._binder;
			ImmutableArray<TypeWithAnnotations> immutableArray;
			if (!typeArgs.IsDefault)
			{
				immutableArray = typeArgs.SelectAsArray<TypeSymbol, TypeWithAnnotations>((TypeSymbol t) => TypeWithAnnotations.Create(t, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			}
			else
			{
				immutableArray = default(ImmutableArray<TypeWithAnnotations>);
			}
			ImmutableArray<TypeWithAnnotations> immutableArray2 = immutableArray;
			return binder.MakeInvocationExpression(node, receiver, methodName, args, diagnostics, default(SeparatedSyntaxList<TypeSyntax>), immutableArray2, null, false, allowUnexpandedForm);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000D0B1B File Offset: 0x000CED1B
		public SyntheticBoundNodeFactory(MethodSymbol topLevelMethod, SyntaxNode node, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
			: this(topLevelMethod, topLevelMethod.ContainingType, node, compilationState, diagnostics)
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000D0B2E File Offset: 0x000CED2E
		public SyntheticBoundNodeFactory([global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol topLevelMethodOpt, [global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol currentClassOpt, SyntaxNode node, TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			this.CompilationState = compilationState;
			this.CurrentType = currentClassOpt;
			this.TopLevelMethod = topLevelMethodOpt;
			this.CurrentFunction = topLevelMethodOpt;
			this.Syntax = node;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000D0B62 File Offset: 0x000CED62
		[Conditional("DEBUG")]
		private void CheckCurrentType()
		{
			NamedTypeSymbol currentType = this.CurrentType;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000D0B6B File Offset: 0x000CED6B
		public void AddNestedType(NamedTypeSymbol nestedType)
		{
			this.ModuleBuilderOpt.AddSynthesizedDefinition(this.CurrentType, nestedType.GetCciAdapter());
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000D0B84 File Offset: 0x000CED84
		public void OpenNestedType(NamedTypeSymbol nestedType)
		{
			this.AddNestedType(nestedType);
			this.CurrentFunction = null;
			this.TopLevelMethod = null;
			this.CurrentType = nestedType;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000D0BA2 File Offset: 0x000CEDA2
		public BoundHoistedFieldAccess HoistedField(FieldSymbol field)
		{
			return new BoundHoistedFieldAccess(this.Syntax, field, field.Type);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		public StateMachineFieldSymbol StateMachineField(TypeWithAnnotations type, string name, bool isPublic = false, bool isThis = false)
		{
			StateMachineFieldSymbol stateMachineFieldSymbol = new StateMachineFieldSymbol(this.CurrentType, type, name, isPublic, isThis);
			this.AddField(this.CurrentType, stateMachineFieldSymbol);
			return stateMachineFieldSymbol;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000D0BE4 File Offset: 0x000CEDE4
		public StateMachineFieldSymbol StateMachineField(TypeSymbol type, string name, bool isPublic = false, bool isThis = false)
		{
			StateMachineFieldSymbol stateMachineFieldSymbol = new StateMachineFieldSymbol(this.CurrentType, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), name, isPublic, isThis);
			this.AddField(this.CurrentType, stateMachineFieldSymbol);
			return stateMachineFieldSymbol;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000D0C20 File Offset: 0x000CEE20
		public StateMachineFieldSymbol StateMachineField(TypeSymbol type, string name, SynthesizedLocalKind synthesizedKind, int slotIndex)
		{
			StateMachineFieldSymbol stateMachineFieldSymbol = new StateMachineFieldSymbol(this.CurrentType, type, name, synthesizedKind, slotIndex, false);
			this.AddField(this.CurrentType, stateMachineFieldSymbol);
			return stateMachineFieldSymbol;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000D0C50 File Offset: 0x000CEE50
		public StateMachineFieldSymbol StateMachineField(TypeSymbol type, string name, LocalSlotDebugInfo slotDebugInfo, int slotIndex)
		{
			StateMachineFieldSymbol stateMachineFieldSymbol = new StateMachineFieldSymbol(this.CurrentType, type, name, slotDebugInfo, slotIndex, false);
			this.AddField(this.CurrentType, stateMachineFieldSymbol);
			return stateMachineFieldSymbol;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000D0C7D File Offset: 0x000CEE7D
		public void AddField(NamedTypeSymbol containingType, FieldSymbol field)
		{
			this.ModuleBuilderOpt.AddSynthesizedDefinition(containingType, field.GetCciAdapter());
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000D0C91 File Offset: 0x000CEE91
		public GeneratedLabelSymbol GenerateLabel(string prefix)
		{
			return new GeneratedLabelSymbol(prefix);
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000D0C99 File Offset: 0x000CEE99
		public BoundThisReference This()
		{
			return new BoundThisReference(this.Syntax, this.CurrentFunction.ThisParameter.Type)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000D0CBD File Offset: 0x000CEEBD
		public BoundExpression This(LocalSymbol thisTempOpt)
		{
			if (!(thisTempOpt != null))
			{
				return this.This();
			}
			return this.Local(thisTempOpt);
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000D0CD6 File Offset: 0x000CEED6
		public BoundBaseReference Base(NamedTypeSymbol baseType)
		{
			return new BoundBaseReference(this.Syntax, baseType)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000D0CEB File Offset: 0x000CEEEB
		public BoundBadExpression BadExpression(TypeSymbol type)
		{
			return new BoundBadExpression(this.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray<BoundExpression>.Empty, type, true);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000D0D05 File Offset: 0x000CEF05
		public BoundParameter Parameter(ParameterSymbol p)
		{
			return new BoundParameter(this.Syntax, p, p.Type)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000D0D20 File Offset: 0x000CEF20
		public BoundFieldAccess Field([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol f)
		{
			return new BoundFieldAccess(this.Syntax, receiver, f, null, LookupResultKind.Viable, f.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000D0D40 File Offset: 0x000CEF40
		public BoundFieldAccess InstanceField(FieldSymbol f)
		{
			return this.Field(this.This(), f);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000D0D4F File Offset: 0x000CEF4F
		public BoundExpression Property(WellKnownMember member)
		{
			return this.Property(null, member);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000D0D5C File Offset: 0x000CEF5C
		public BoundExpression Property([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, WellKnownMember member)
		{
			PropertySymbol propertySymbol = (PropertySymbol)this.WellKnownMember(member);
			Binder.ReportUseSite(propertySymbol, this.Diagnostics, this.Syntax);
			return this.Property(receiverOpt, propertySymbol);
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000D0D91 File Offset: 0x000CEF91
		public BoundExpression Property([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol property)
		{
			return this.Call(receiverOpt, property.GetMethod);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000D0DA0 File Offset: 0x000CEFA0
		public BoundExpression Indexer([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol property, BoundExpression arg0)
		{
			return this.Call(receiverOpt, property.GetMethod, arg0);
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x000D0DB0 File Offset: 0x000CEFB0
		public NamedTypeSymbol SpecialType(SpecialType st)
		{
			NamedTypeSymbol specialType = this.Compilation.GetSpecialType(st);
			Binder.ReportUseSite(specialType, this.Diagnostics, this.Syntax);
			return specialType;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000D0DD1 File Offset: 0x000CEFD1
		public ArrayTypeSymbol WellKnownArrayType(WellKnownType elementType)
		{
			return this.Compilation.CreateArrayTypeSymbol(this.WellKnownType(elementType), 1, NullableAnnotation.Oblivious);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000D0DE7 File Offset: 0x000CEFE7
		public NamedTypeSymbol WellKnownType(WellKnownType wt)
		{
			NamedTypeSymbol wellKnownType = this.Compilation.GetWellKnownType(wt);
			Binder.ReportUseSite(wellKnownType, this.Diagnostics, this.Syntax);
			return wellKnownType;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000D0E08 File Offset: 0x000CF008
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public Symbol WellKnownMember(WellKnownMember wm, bool isOptional)
		{
			Symbol wellKnownTypeMember = Binder.GetWellKnownTypeMember(this.Compilation, wm, this.Diagnostics, null, this.Syntax, true);
			if (wellKnownTypeMember == null && !isOptional)
			{
				MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(wm);
				throw new SyntheticBoundNodeFactory.MissingPredefinedMember(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[] { descriptor.DeclaringTypeMetadataName, descriptor.Name }), this.Syntax.Location, false));
			}
			return wellKnownTypeMember;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000D0E78 File Offset: 0x000CF078
		public Symbol WellKnownMember(WellKnownMember wm)
		{
			return this.WellKnownMember(wm, false);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000D0E82 File Offset: 0x000CF082
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public MethodSymbol WellKnownMethod(WellKnownMember wm, bool isOptional)
		{
			return (MethodSymbol)this.WellKnownMember(wm, isOptional);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000D0E91 File Offset: 0x000CF091
		public MethodSymbol WellKnownMethod(WellKnownMember wm)
		{
			return (MethodSymbol)this.WellKnownMember(wm, false);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000D0EA0 File Offset: 0x000CF0A0
		public Symbol SpecialMember(SpecialMember sm)
		{
			Symbol specialTypeMember = this.Compilation.GetSpecialTypeMember(sm);
			if (specialTypeMember == null)
			{
				MemberDescriptor descriptor = SpecialMembers.GetDescriptor(sm);
				throw new SyntheticBoundNodeFactory.MissingPredefinedMember(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_MissingPredefinedMember, new object[] { descriptor.DeclaringTypeMetadataName, descriptor.Name }), this.Syntax.Location, false));
			}
			Binder.ReportUseSite(specialTypeMember, this.Diagnostics, this.Syntax);
			return specialTypeMember;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000D0F12 File Offset: 0x000CF112
		public MethodSymbol SpecialMethod(SpecialMember sm)
		{
			return (MethodSymbol)this.SpecialMember(sm);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000D0F20 File Offset: 0x000CF120
		public PropertySymbol SpecialProperty(SpecialMember sm)
		{
			return (PropertySymbol)this.SpecialMember(sm);
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000D0F2E File Offset: 0x000CF12E
		public BoundExpressionStatement Assignment(BoundExpression left, BoundExpression right, bool isRef = false)
		{
			return this.ExpressionStatement(this.AssignmentExpression(left, right, isRef));
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000D0F3F File Offset: 0x000CF13F
		public BoundExpressionStatement ExpressionStatement(BoundExpression expr)
		{
			return new BoundExpressionStatement(this.Syntax, expr, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000D0F55 File Offset: 0x000CF155
		public BoundAssignmentOperator AssignmentExpression(BoundExpression left, BoundExpression right, bool isRef = false)
		{
			return new BoundAssignmentOperator(this.Syntax, left, right, left.Type, isRef, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000D0F73 File Offset: 0x000CF173
		public BoundBlock Block()
		{
			return this.Block(ImmutableArray<BoundStatement>.Empty);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000D0F80 File Offset: 0x000CF180
		public BoundBlock Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return this.Block(ImmutableArray<LocalSymbol>.Empty, statements);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000D0F8E File Offset: 0x000CF18E
		public BoundBlock Block(params BoundStatement[] statements)
		{
			return this.Block(ImmutableArray.Create<BoundStatement>(statements));
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000D0F9C File Offset: 0x000CF19C
		public BoundBlock Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, params BoundStatement[] statements)
		{
			return this.Block(locals, ImmutableArray.Create<BoundStatement>(statements));
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x000D0FAB File Offset: 0x000CF1AB
		public BoundBlock Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return new BoundBlock(this.Syntax, locals, statements, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000D0FC2 File Offset: 0x000CF1C2
		public BoundBlock Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> localFunctions, params BoundStatement[] statements)
		{
			return this.Block(locals, localFunctions, ImmutableArray.Create<BoundStatement>(statements));
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000D0FD2 File Offset: 0x000CF1D2
		public BoundBlock Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> localFunctions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return new BoundBlock(this.Syntax, locals, localFunctions, statements, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000D0FEA File Offset: 0x000CF1EA
		public BoundExtractedFinallyBlock ExtractedFinallyBlock(BoundBlock finallyBlock)
		{
			return new BoundExtractedFinallyBlock(this.Syntax, finallyBlock, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000D1000 File Offset: 0x000CF200
		public BoundStatementList StatementList()
		{
			return this.StatementList(ImmutableArray<BoundStatement>.Empty);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000D100D File Offset: 0x000CF20D
		public BoundStatementList StatementList([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return new BoundStatementList(this.Syntax, statements, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000D1023 File Offset: 0x000CF223
		public BoundStatementList StatementList(BoundStatement first, BoundStatement second)
		{
			return new BoundStatementList(this.Syntax, ImmutableArray.Create<BoundStatement>(first, second), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000D1040 File Offset: 0x000CF240
		public BoundReturnStatement Return([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression expression = null)
		{
			if (expression != null)
			{
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				Conversion conversion = this.Compilation.Conversions.ClassifyConversionFromType(expression.Type, this.CurrentFunction.ReturnType, ref discarded, false);
				if (conversion.Kind != ConversionKind.Identity)
				{
					expression = BoundConversion.Synthesized(this.Syntax, expression, conversion, false, false, null, null, this.CurrentFunction.ReturnType, false);
				}
			}
			return new BoundReturnStatement(this.Syntax, this.CurrentFunction.RefKind, expression, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000D10C3 File Offset: 0x000CF2C3
		public void CloseMethod(BoundStatement body)
		{
			if (body.Kind != BoundKind.Block)
			{
				body = this.Block(new BoundStatement[] { body });
			}
			this.CompilationState.AddSynthesizedMethod(this.CurrentFunction, body);
			this.CurrentFunction = null;
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000D10FC File Offset: 0x000CF2FC
		public LocalSymbol SynthesizedLocal(TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntax = null, bool isPinned = false, RefKind refKind = RefKind.None, SynthesizedLocalKind kind = SynthesizedLocalKind.LoweringTemp)
		{
			return new SynthesizedLocal(this.CurrentFunction, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), kind, syntax, isPinned, refKind);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000D112C File Offset: 0x000CF32C
		public ParameterSymbol SynthesizedParameter(TypeSymbol type, string name, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol container = null, int ordinal = 0)
		{
			return SynthesizedParameterSymbol.Create(container, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), ordinal, RefKind.None, name, default(ImmutableArray<CustomModifier>), null);
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000D1160 File Offset: 0x000CF360
		public BoundBinaryOperator Binary(BinaryOperatorKind kind, TypeSymbol type, BoundExpression left, BoundExpression right)
		{
			return new BoundBinaryOperator(this.Syntax, kind, null, null, LookupResultKind.Viable, left, right, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000D1189 File Offset: 0x000CF389
		public BoundAsOperator As(BoundExpression operand, TypeSymbol type)
		{
			return new BoundAsOperator(this.Syntax, operand, this.Type(type), Conversion.ExplicitReference, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000D11AC File Offset: 0x000CF3AC
		public BoundIsOperator Is(BoundExpression operand, TypeSymbol type)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			Conversion conversion = this.Compilation.Conversions.ClassifyBuiltInConversion(operand.Type, type, ref discarded);
			return new BoundIsOperator(this.Syntax, operand, this.Type(type), conversion, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000D11FC File Offset: 0x000CF3FC
		public BoundBinaryOperator LogicalAnd(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.LogicalBoolAnd, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000D1212 File Offset: 0x000CF412
		public BoundBinaryOperator LogicalOr(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.LogicalBoolOr, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000D1228 File Offset: 0x000CF428
		public BoundBinaryOperator IntEqual(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.IntEqual, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000D123E File Offset: 0x000CF43E
		public BoundBinaryOperator ObjectEqual(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.ObjectEqual, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000D1254 File Offset: 0x000CF454
		public BoundBinaryOperator ObjectNotEqual(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.ObjectNotEqual, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000D126A File Offset: 0x000CF46A
		public BoundBinaryOperator IntNotEqual(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.IntNotEqual, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000D1280 File Offset: 0x000CF480
		public BoundBinaryOperator IntLessThan(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.IntLessThan, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000D1296 File Offset: 0x000CF496
		public BoundBinaryOperator IntGreaterThanOrEqual(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.IntGreaterThanOrEqual, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean), left, right);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000D12AC File Offset: 0x000CF4AC
		public BoundBinaryOperator IntSubtract(BoundExpression left, BoundExpression right)
		{
			return this.Binary(BinaryOperatorKind.IntSubtraction, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32), left, right);
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000D12C3 File Offset: 0x000CF4C3
		public BoundLiteral Literal(int value)
		{
			return new BoundLiteral(this.Syntax, ConstantValue.Create(value), this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000D12E5 File Offset: 0x000CF4E5
		public BoundLiteral Literal(uint value)
		{
			return new BoundLiteral(this.Syntax, ConstantValue.Create(value), this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_UInt32))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000D1307 File Offset: 0x000CF507
		public BoundLiteral Literal(ConstantValue value, TypeSymbol type)
		{
			return new BoundLiteral(this.Syntax, value, type)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000D1320 File Offset: 0x000CF520
		public BoundObjectCreationExpression New(NamedTypeSymbol type, params BoundExpression[] args)
		{
			MethodSymbol methodSymbol = type.InstanceConstructors.Single<MethodSymbol>((MethodSymbol c) => c.ParameterCount == args.Length);
			return this.New(methodSymbol, args);
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000D135F File Offset: 0x000CF55F
		public BoundObjectCreationExpression New(MethodSymbol ctor, params BoundExpression[] args)
		{
			return this.New(ctor, args.ToImmutableArray<BoundExpression>());
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000D136E File Offset: 0x000CF56E
		public BoundObjectCreationExpression New(MethodSymbol ctor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args)
		{
			return new BoundObjectCreationExpression(this.Syntax, ctor, args)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000D1384 File Offset: 0x000CF584
		public BoundObjectCreationExpression New(WellKnownMember wm, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args)
		{
			MethodSymbol methodSymbol = this.WellKnownMethod(wm);
			return new BoundObjectCreationExpression(this.Syntax, methodSymbol, args)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000D13B0 File Offset: 0x000CF5B0
		public BoundExpression MakeIsNotANumberTest(BoundExpression input)
		{
			TypeSymbol type = input.Type;
			if (type != null)
			{
				SpecialType specialType = type.SpecialType;
				if (specialType == Microsoft.CodeAnalysis.SpecialType.System_Single)
				{
					return this.StaticCall(Microsoft.CodeAnalysis.SpecialMember.System_Single__IsNaN, new BoundExpression[] { input });
				}
				if (specialType == Microsoft.CodeAnalysis.SpecialType.System_Double)
				{
					return this.StaticCall(Microsoft.CodeAnalysis.SpecialMember.System_Double__IsNaN, new BoundExpression[] { input });
				}
			}
			throw ExceptionUtilities.UnexpectedValue(input.Type);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000D140C File Offset: 0x000CF60C
		public BoundExpression InstanceCall(BoundExpression receiver, string name, BoundExpression arg)
		{
			return this.MakeInvocationExpression(BinderFlags.None, this.Syntax, receiver, name, ImmutableArray.Create<BoundExpression>(arg), this.Diagnostics, default(ImmutableArray<TypeSymbol>), true);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000D1440 File Offset: 0x000CF640
		public BoundExpression InstanceCall(BoundExpression receiver, string name)
		{
			return this.MakeInvocationExpression(BinderFlags.None, this.Syntax, receiver, name, ImmutableArray<BoundExpression>.Empty, this.Diagnostics, default(ImmutableArray<TypeSymbol>), true);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000D1474 File Offset: 0x000CF674
		public BoundExpression StaticCall(TypeSymbol receiver, string name, params BoundExpression[] args)
		{
			return this.MakeInvocationExpression(BinderFlags.None, this.Syntax, this.Type(receiver), name, args.ToImmutableArray<BoundExpression>(), this.Diagnostics, default(ImmutableArray<TypeSymbol>), true);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000D14AC File Offset: 0x000CF6AC
		public BoundExpression StaticCall(TypeSymbol receiver, string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args, bool allowUnexpandedForm)
		{
			return this.MakeInvocationExpression(BinderFlags.None, this.Syntax, this.Type(receiver), name, args, this.Diagnostics, default(ImmutableArray<TypeSymbol>), allowUnexpandedForm);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000D14E4 File Offset: 0x000CF6E4
		public BoundExpression StaticCall(BinderFlags flags, TypeSymbol receiver, string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeSymbol> typeArgs, params BoundExpression[] args)
		{
			return this.MakeInvocationExpression(flags, this.Syntax, this.Type(receiver), name, args.ToImmutableArray<BoundExpression>(), this.Diagnostics, typeArgs, true);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000D1516 File Offset: 0x000CF716
		public BoundExpression StaticCall(TypeSymbol receiver, MethodSymbol method, params BoundExpression[] args)
		{
			if (method == null)
			{
				return new BoundBadExpression(this.Syntax, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, args.AsImmutable<BoundExpression>(), receiver);
			}
			return this.Call(null, method, args);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000D153D File Offset: 0x000CF73D
		public BoundExpression StaticCall(MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args)
		{
			return this.Call(null, method, args);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000D1548 File Offset: 0x000CF748
		public BoundExpression StaticCall(WellKnownMember method, params BoundExpression[] args)
		{
			MethodSymbol methodSymbol = this.WellKnownMethod(method);
			Binder.ReportUseSite(methodSymbol, this.Diagnostics, this.Syntax);
			return this.Call(null, methodSymbol, args);
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000D157C File Offset: 0x000CF77C
		public BoundExpression StaticCall(SpecialMember method, params BoundExpression[] args)
		{
			MethodSymbol methodSymbol = this.SpecialMethod(method);
			Binder.ReportUseSite(methodSymbol, this.Diagnostics, this.Syntax);
			return this.Call(null, methodSymbol, args);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000D15AD File Offset: 0x000CF7AD
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method)
		{
			return this.Call(receiver, method, ImmutableArray<BoundExpression>.Empty);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000D15BC File Offset: 0x000CF7BC
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method, BoundExpression arg0)
		{
			return this.Call(receiver, method, ImmutableArray.Create<BoundExpression>(arg0));
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000D15CC File Offset: 0x000CF7CC
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method, BoundExpression arg0, BoundExpression arg1)
		{
			return this.Call(receiver, method, ImmutableArray.Create<BoundExpression>(arg0, arg1));
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000D15DE File Offset: 0x000CF7DE
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method, params BoundExpression[] args)
		{
			return this.Call(receiver, method, ImmutableArray.Create<BoundExpression>(args));
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000D15EE File Offset: 0x000CF7EE
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, WellKnownMember method, BoundExpression arg0)
		{
			return this.Call(receiver, this.WellKnownMethod(method), ImmutableArray.Create<BoundExpression>(arg0));
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000D1604 File Offset: 0x000CF804
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args)
		{
			return new BoundCall(this.Syntax, receiver, method, args, default(ImmutableArray<string>), method.ParameterRefKinds, false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, method.ReturnType, method.OriginalDefinition is ErrorMethodSymbol)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000D1660 File Offset: 0x000CF860
		public BoundCall Call([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> args)
		{
			return new BoundCall(this.Syntax, receiver, method, args, default(ImmutableArray<string>), refKinds, false, false, false, ImmutableArray<int>.Empty, default(BitVector), LookupResultKind.Viable, method.ReturnType, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000D16A8 File Offset: 0x000CF8A8
		public BoundExpression Conditional(BoundExpression condition, BoundExpression consequence, BoundExpression alternative, TypeSymbol type)
		{
			return new BoundConditionalOperator(this.Syntax, false, condition, consequence, alternative, null, type, false, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000D16D2 File Offset: 0x000CF8D2
		public BoundExpression ComplexConditionalReceiver(BoundExpression valueTypeReceiver, BoundExpression referenceTypeReceiver)
		{
			return new BoundComplexConditionalReceiver(this.Syntax, valueTypeReceiver, referenceTypeReceiver, valueTypeReceiver.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000D16EF File Offset: 0x000CF8EF
		public BoundExpression Coalesce(BoundExpression left, BoundExpression right)
		{
			return new BoundNullCoalescingOperator(this.Syntax, left, right, Conversion.Identity, BoundNullCoalescingOperatorResultKind.LeftType, left.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000D1712 File Offset: 0x000CF912
		public BoundStatement If(BoundExpression condition, BoundStatement thenClause, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement elseClauseOpt = null)
		{
			return this.If(condition, ImmutableArray<LocalSymbol>.Empty, thenClause, elseClauseOpt);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000D1722 File Offset: 0x000CF922
		public BoundStatement ConditionalGoto(BoundExpression condition, LabelSymbol label, bool jumpIfTrue)
		{
			return new BoundConditionalGoto(this.Syntax, condition, jumpIfTrue, label, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000D173C File Offset: 0x000CF93C
		public BoundStatement If(BoundExpression condition, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundStatement thenClause, [global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement elseClauseOpt = null)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("afterif");
			if (elseClauseOpt != null)
			{
				GeneratedLabelSymbol generatedLabelSymbol2 = new GeneratedLabelSymbol("alternative");
				instance.Add(this.ConditionalGoto(condition, generatedLabelSymbol2, false));
				instance.Add(thenClause);
				instance.Add(this.Goto(generatedLabelSymbol));
				if (!locals.IsDefaultOrEmpty)
				{
					BoundBlock boundBlock = this.Block(locals, instance.ToImmutable());
					instance.Clear();
					instance.Add(boundBlock);
				}
				instance.Add(this.Label(generatedLabelSymbol2));
				instance.Add(elseClauseOpt);
			}
			else
			{
				instance.Add(this.ConditionalGoto(condition, generatedLabelSymbol, false));
				instance.Add(thenClause);
				if (!locals.IsDefaultOrEmpty)
				{
					BoundBlock boundBlock2 = this.Block(locals, instance.ToImmutable());
					instance.Clear();
					instance.Add(boundBlock2);
				}
			}
			instance.Add(this.Label(generatedLabelSymbol));
			return this.Block(instance.ToImmutableAndFree());
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000D181C File Offset: 0x000CFA1C
		public BoundThrowStatement Throw(BoundExpression e)
		{
			return new BoundThrowStatement(this.Syntax, e, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000D1832 File Offset: 0x000CFA32
		public BoundLocal Local(LocalSymbol local)
		{
			return new BoundLocal(this.Syntax, local, null, local.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000D184F File Offset: 0x000CFA4F
		public BoundExpression MakeSequence(LocalSymbol temp, params BoundExpression[] parts)
		{
			return this.MakeSequence(ImmutableArray.Create<LocalSymbol>(temp), parts);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000D185E File Offset: 0x000CFA5E
		public BoundExpression MakeSequence(params BoundExpression[] parts)
		{
			return this.MakeSequence(ImmutableArray<LocalSymbol>.Empty, parts);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000D186C File Offset: 0x000CFA6C
		public BoundExpression MakeSequence([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, params BoundExpression[] parts)
		{
			ArrayBuilder<BoundExpression> instance = ArrayBuilder<BoundExpression>.GetInstance();
			for (int i = 0; i < parts.Length - 1; i++)
			{
				if (LocalRewriter.ReadIsSideeffecting(parts[i]))
				{
					instance.Add(parts[i]);
				}
			}
			BoundExpression boundExpression = parts[parts.Length - 1];
			if (locals.IsDefaultOrEmpty && instance.Count == 0)
			{
				instance.Free();
				return boundExpression;
			}
			return this.Sequence(locals, instance.ToImmutableAndFree(), boundExpression);
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000D18D4 File Offset: 0x000CFAD4
		public BoundSequence Sequence(BoundExpression[] sideEffects, BoundExpression result, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type = null)
		{
			TypeSymbol typeSymbol = type ?? result.Type;
			return new BoundSequence(this.Syntax, ImmutableArray<LocalSymbol>.Empty, sideEffects.AsImmutableOrNull<BoundExpression>(), result, typeSymbol, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000D190D File Offset: 0x000CFB0D
		public BoundExpression Sequence([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> sideEffects, BoundExpression result)
		{
			if (!locals.IsDefaultOrEmpty || !sideEffects.IsDefaultOrEmpty)
			{
				return new BoundSequence(this.Syntax, locals, sideEffects, result, result.Type, false)
				{
					WasCompilerGenerated = true
				};
			}
			return result;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000D193F File Offset: 0x000CFB3F
		public BoundSpillSequence SpillSequence([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> sideEffects, BoundExpression result)
		{
			return new BoundSpillSequence(this.Syntax, locals, sideEffects, result, result.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000D195D File Offset: 0x000CFB5D
		public SyntheticBoundNodeFactory.SyntheticSwitchSection SwitchSection(int value, params BoundStatement[] statements)
		{
			return new SyntheticBoundNodeFactory.SyntheticSwitchSection(ImmutableArray.Create<int>(value), ImmutableArray.Create<BoundStatement>(statements));
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000D1970 File Offset: 0x000CFB70
		public SyntheticBoundNodeFactory.SyntheticSwitchSection SwitchSection(List<int> values, params BoundStatement[] statements)
		{
			return new SyntheticBoundNodeFactory.SyntheticSwitchSection(ImmutableArray.CreateRange<int>(values), ImmutableArray.Create<BoundStatement>(statements));
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000D1984 File Offset: 0x000CFB84
		public BoundStatement Switch(BoundExpression ex, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntheticBoundNodeFactory.SyntheticSwitchSection> sections)
		{
			if (sections.Length == 0)
			{
				return this.ExpressionStatement(ex);
			}
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("break");
			ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>> instance = ArrayBuilder<ValueTuple<ConstantValue, LabelSymbol>>.GetInstance();
			ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance();
			instance2.Add(null);
			foreach (SyntheticBoundNodeFactory.SyntheticSwitchSection syntheticSwitchSection in sections)
			{
				LabelSymbol labelSymbol = new GeneratedLabelSymbol("case " + syntheticSwitchSection.Values[0].ToString());
				instance2.Add(this.Label(labelSymbol));
				instance2.AddRange(syntheticSwitchSection.Statements);
				foreach (int num in syntheticSwitchSection.Values)
				{
					instance.Add(new ValueTuple<ConstantValue, LabelSymbol>(ConstantValue.Create(num), labelSymbol));
				}
			}
			instance2.Add(this.Label(generatedLabelSymbol));
			instance2[0] = new BoundSwitchDispatch(this.Syntax, ex, instance.ToImmutableAndFree(), generatedLabelSymbol, null, false)
			{
				WasCompilerGenerated = true
			};
			return this.Block(instance2.ToImmutableAndFree());
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000D1AA0 File Offset: 0x000CFCA0
		[Conditional("DEBUG")]
		private static void CheckSwitchSections([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntheticBoundNodeFactory.SyntheticSwitchSection> sections)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (SyntheticBoundNodeFactory.SyntheticSwitchSection syntheticSwitchSection in sections)
			{
				foreach (int num in syntheticSwitchSection.Values)
				{
					hashSet.Add(num);
				}
			}
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000D1AF8 File Offset: 0x000CFCF8
		public BoundGotoStatement Goto(LabelSymbol label)
		{
			return new BoundGotoStatement(this.Syntax, label, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000D1B0E File Offset: 0x000CFD0E
		public BoundLabelStatement Label(LabelSymbol label)
		{
			return new BoundLabelStatement(this.Syntax, label)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000D1B23 File Offset: 0x000CFD23
		public BoundLiteral Literal(bool value)
		{
			return new BoundLiteral(this.Syntax, ConstantValue.Create(value), this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Boolean))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000D1B44 File Offset: 0x000CFD44
		public BoundLiteral Literal([global::System.Runtime.CompilerServices.Nullable(2)] string value)
		{
			ConstantValue constantValue = ConstantValue.Create(value);
			return this.StringLiteral(constantValue);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000D1B5F File Offset: 0x000CFD5F
		public BoundLiteral StringLiteral(ConstantValue stringConst)
		{
			return new BoundLiteral(this.Syntax, stringConst, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_String))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000D1B7C File Offset: 0x000CFD7C
		public BoundLiteral StringLiteral(string stringValue)
		{
			return this.StringLiteral(ConstantValue.Create(stringValue));
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000D1B8A File Offset: 0x000CFD8A
		public BoundArrayLength ArrayLength(BoundExpression array)
		{
			return new BoundArrayLength(this.Syntax, array, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32), false);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000D1BA4 File Offset: 0x000CFDA4
		public BoundArrayAccess ArrayAccessFirstElement(BoundExpression array)
		{
			ImmutableArray<BoundExpression> immutableArray = ArrayBuilder<BoundExpression>.GetInstance(((ArrayTypeSymbol)array.Type).Rank, this.Literal(0)).ToImmutableAndFree();
			return this.ArrayAccess(array, immutableArray);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000D1BDB File Offset: 0x000CFDDB
		public BoundArrayAccess ArrayAccess(BoundExpression array, params BoundExpression[] indices)
		{
			return this.ArrayAccess(array, indices.AsImmutableOrNull<BoundExpression>());
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000D1BEA File Offset: 0x000CFDEA
		public BoundArrayAccess ArrayAccess(BoundExpression array, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> indices)
		{
			return new BoundArrayAccess(this.Syntax, array, indices, ((ArrayTypeSymbol)array.Type).ElementType, false);
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000D1C0C File Offset: 0x000CFE0C
		public BoundStatement BaseInitialization()
		{
			NamedTypeSymbol baseTypeNoUseSiteDiagnostics = this.CurrentFunction.ThisParameter.Type.BaseTypeNoUseSiteDiagnostics;
			MethodSymbol methodSymbol = baseTypeNoUseSiteDiagnostics.InstanceConstructors.Single<MethodSymbol>((MethodSymbol c) => c.ParameterCount == 0);
			return new BoundExpressionStatement(this.Syntax, this.Call(this.Base(baseTypeNoUseSiteDiagnostics), methodSymbol), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000D1C7B File Offset: 0x000CFE7B
		public BoundStatement SequencePoint(SyntaxNode syntax, BoundStatement statement)
		{
			return new BoundSequencePoint(syntax, statement, false);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000D1C85 File Offset: 0x000CFE85
		public BoundStatement SequencePointWithSpan(CSharpSyntaxNode syntax, TextSpan span, BoundStatement statement)
		{
			return new BoundSequencePointWithSpan(syntax, statement, span, false);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000D1C90 File Offset: 0x000CFE90
		public BoundStatement HiddenSequencePoint([global::System.Runtime.CompilerServices.Nullable(2)] BoundStatement statementOpt = null)
		{
			return BoundSequencePoint.CreateHidden(statementOpt, false);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000D1C99 File Offset: 0x000CFE99
		public BoundStatement ThrowNull()
		{
			return this.Throw(this.Null(Binder.GetWellKnownType(this.Compilation, Microsoft.CodeAnalysis.WellKnownType.System_Exception, this.Diagnostics, this.Syntax.Location)));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000D1CC5 File Offset: 0x000CFEC5
		public BoundExpression ThrowExpression(BoundExpression thrown, TypeSymbol type)
		{
			return new BoundThrowExpression(thrown.Syntax, thrown, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000D1CDC File Offset: 0x000CFEDC
		public BoundExpression Null(TypeSymbol type)
		{
			return SyntheticBoundNodeFactory.Null(type, this.Syntax);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000D1CEC File Offset: 0x000CFEEC
		public static BoundExpression Null(TypeSymbol type, SyntaxNode syntax)
		{
			BoundExpression boundExpression = new BoundLiteral(syntax, ConstantValue.Null, type)
			{
				WasCompilerGenerated = true
			};
			if (!type.IsPointerOrFunctionPointer())
			{
				return boundExpression;
			}
			return BoundConversion.SynthesizedNonUserDefined(syntax, boundExpression, Conversion.NullToPointer, type, null);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000D1D25 File Offset: 0x000CFF25
		public BoundTypeExpression Type(TypeSymbol type)
		{
			return new BoundTypeExpression(this.Syntax, null, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000D1D3C File Offset: 0x000CFF3C
		public BoundExpression Typeof(WellKnownType type)
		{
			return this.Typeof(this.WellKnownType(type));
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000D1D4B File Offset: 0x000CFF4B
		public BoundExpression Typeof(TypeSymbol type)
		{
			return new BoundTypeOfOperator(this.Syntax, this.Type(type), this.WellKnownMethod(Microsoft.CodeAnalysis.WellKnownMember.System_Type__GetTypeFromHandle), this.WellKnownType(Microsoft.CodeAnalysis.WellKnownType.System_Type), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000D1D77 File Offset: 0x000CFF77
		public BoundExpression Typeof(TypeWithAnnotations type)
		{
			return this.Typeof(type.Type);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000D1D86 File Offset: 0x000CFF86
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> TypeOfs(ImmutableArray<TypeWithAnnotations> typeArguments)
		{
			return typeArguments.SelectAsArray<TypeWithAnnotations, BoundExpression>(new Func<TypeWithAnnotations, BoundExpression>(this.Typeof));
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000D1D9A File Offset: 0x000CFF9A
		public BoundExpression TypeofDynamicOperationContextType()
		{
			return this.Typeof(this.CompilationState.DynamicOperationContextType);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000D1DAD File Offset: 0x000CFFAD
		public BoundExpression Sizeof(TypeSymbol type)
		{
			return new BoundSizeOfOperator(this.Syntax, this.Type(type), Binder.GetConstantSizeOf(type), this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000D1DD7 File Offset: 0x000CFFD7
		internal BoundExpression ConstructorInfo(MethodSymbol ctor)
		{
			return new BoundMethodInfo(this.Syntax, ctor, this.GetMethodFromHandleMethod(ctor.ContainingType), this.WellKnownType(Microsoft.CodeAnalysis.WellKnownType.System_Reflection_ConstructorInfo))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000D1E00 File Offset: 0x000D0000
		public BoundExpression MethodDefIndex(MethodSymbol method)
		{
			return new BoundMethodDefIndex(this.Syntax, method, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000D1E1D File Offset: 0x000D001D
		public BoundExpression ModuleVersionId()
		{
			return new BoundModuleVersionId(this.Syntax, this.WellKnownType(Microsoft.CodeAnalysis.WellKnownType.System_Guid))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000D1E39 File Offset: 0x000D0039
		public BoundExpression ModuleVersionIdString()
		{
			return new BoundModuleVersionIdString(this.Syntax, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_String))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000D1E55 File Offset: 0x000D0055
		public BoundExpression InstrumentationPayloadRoot(int analysisKind, TypeSymbol payloadType)
		{
			return new BoundInstrumentationPayloadRoot(this.Syntax, analysisKind, payloadType)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000D1E6B File Offset: 0x000D006B
		public BoundExpression MaximumMethodDefIndex()
		{
			return new BoundMaximumMethodDefIndex(this.Syntax, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000D1E87 File Offset: 0x000D0087
		public BoundExpression SourceDocumentIndex(DebugSourceDocument document)
		{
			return new BoundSourceDocumentIndex(this.Syntax, document, this.SpecialType(Microsoft.CodeAnalysis.SpecialType.System_Int32))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000D1EA4 File Offset: 0x000D00A4
		public BoundExpression MethodInfo(MethodSymbol method)
		{
			if (!method.ContainingType.IsValueType || !CodeGenerator.MayUseCallForStructMethod(method))
			{
				method = method.GetConstructedLeastOverriddenMethod(this.CompilationState.Type, true);
			}
			return new BoundMethodInfo(this.Syntax, method, this.GetMethodFromHandleMethod(method.ContainingType), this.WellKnownType(Microsoft.CodeAnalysis.WellKnownType.System_Reflection_MethodInfo))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000D1F01 File Offset: 0x000D0101
		public BoundExpression FieldInfo(FieldSymbol field)
		{
			return new BoundFieldInfo(this.Syntax, field, this.GetFieldFromHandleMethod(field.ContainingType), this.WellKnownType(Microsoft.CodeAnalysis.WellKnownType.System_Reflection_FieldInfo))
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000D1F2A File Offset: 0x000D012A
		private MethodSymbol GetMethodFromHandleMethod(NamedTypeSymbol methodContainer)
		{
			return this.WellKnownMethod((methodContainer.AllTypeArgumentCount() == 0 && !methodContainer.IsAnonymousType) ? Microsoft.CodeAnalysis.WellKnownMember.System_Reflection_MethodBase__GetMethodFromHandle : Microsoft.CodeAnalysis.WellKnownMember.System_Reflection_MethodBase__GetMethodFromHandle2);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000D1F48 File Offset: 0x000D0148
		private MethodSymbol GetFieldFromHandleMethod(NamedTypeSymbol fieldContainer)
		{
			return this.WellKnownMethod((fieldContainer.AllTypeArgumentCount() == 0) ? Microsoft.CodeAnalysis.WellKnownMember.System_Reflection_FieldInfo__GetFieldFromHandle : Microsoft.CodeAnalysis.WellKnownMember.System_Reflection_FieldInfo__GetFieldFromHandle2);
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000D1F60 File Offset: 0x000D0160
		public BoundExpression Convert(TypeSymbol type, BoundExpression arg)
		{
			if (TypeSymbol.Equals(type, arg.Type, TypeCompareKind.ConsiderEverything))
			{
				return arg;
			}
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			Conversion conversion = this.Compilation.Conversions.ClassifyConversionFromExpression(arg, type, ref discarded, false);
			return this.Convert(type, arg, conversion, false);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000D1FA4 File Offset: 0x000D01A4
		public BoundExpression Convert(TypeSymbol type, BoundExpression arg, Conversion conversion, bool isChecked = false)
		{
			if (conversion.Method != null && !TypeSymbol.Equals(conversion.Method.Parameters[0].Type, arg.Type, TypeCompareKind.ConsiderEverything))
			{
				arg = this.Convert(conversion.Method.Parameters[0].Type, arg);
			}
			if (conversion.Kind == ConversionKind.ImplicitReference && arg.IsLiteralNull())
			{
				return this.Null(type);
			}
			if (conversion.Kind == ConversionKind.ExplicitNullable && arg.Type.IsNullableType() && arg.Type.GetNullableUnderlyingType().Equals(type, TypeCompareKind.AllIgnoreOptions))
			{
				return this.Call(arg, this.SpecialMethod(Microsoft.CodeAnalysis.SpecialMember.System_Nullable_T_get_Value).AsMember((NamedTypeSymbol)arg.Type));
			}
			return new BoundConversion(this.Syntax, arg, conversion, isChecked, true, null, null, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000D2088 File Offset: 0x000D0288
		public BoundExpression ArrayOrEmpty(TypeSymbol elementType, BoundExpression[] elements)
		{
			return this.ArrayOrEmpty(elementType, elements.AsImmutable<BoundExpression>());
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000D2098 File Offset: 0x000D0298
		public BoundExpression ArrayOrEmpty(TypeSymbol elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> elements)
		{
			if (elements.Length == 0)
			{
				MethodSymbol methodSymbol = this.WellKnownMethod(Microsoft.CodeAnalysis.WellKnownMember.System_Array__Empty, true);
				if (methodSymbol != null)
				{
					methodSymbol = methodSymbol.Construct(ImmutableArray.Create<TypeSymbol>(elementType));
					return this.Call(null, methodSymbol);
				}
			}
			return this.Array(elementType, elements);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000D20D8 File Offset: 0x000D02D8
		public BoundExpression Array(TypeSymbol elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> elements)
		{
			return new BoundArrayCreation(this.Syntax, ImmutableArray.Create<BoundExpression>(this.Literal(elements.Length)), new BoundArrayInitialization(this.Syntax, elements, false)
			{
				WasCompilerGenerated = true
			}, this.Compilation.CreateArrayTypeSymbol(elementType, 1, NullableAnnotation.Oblivious), false);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000D2125 File Offset: 0x000D0325
		public BoundExpression Array(TypeSymbol elementType, BoundExpression length)
		{
			return new BoundArrayCreation(this.Syntax, ImmutableArray.Create<BoundExpression>(length), null, this.Compilation.CreateArrayTypeSymbol(elementType, 1, NullableAnnotation.Oblivious), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000D214F File Offset: 0x000D034F
		internal BoundExpression Default(TypeSymbol type)
		{
			return SyntheticBoundNodeFactory.Default(type, this.Syntax);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000D215D File Offset: 0x000D035D
		internal static BoundExpression Default(TypeSymbol type, SyntaxNode syntax)
		{
			return new BoundDefaultExpression(syntax, type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000D216E File Offset: 0x000D036E
		internal BoundStatement Try(BoundBlock tryBlock, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundCatchBlock> catchBlocks, [global::System.Runtime.CompilerServices.Nullable(2)] BoundBlock finallyBlock = null, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol finallyLabel = null)
		{
			return new BoundTryStatement(this.Syntax, tryBlock, catchBlocks, finallyBlock, finallyLabel)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000D2187 File Offset: 0x000D0387
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<BoundCatchBlock> CatchBlocks(params BoundCatchBlock[] catchBlocks)
		{
			return catchBlocks.AsImmutableOrNull<BoundCatchBlock>();
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000D2190 File Offset: 0x000D0390
		internal BoundCatchBlock Catch(LocalSymbol local, BoundBlock block)
		{
			BoundLocal boundLocal = this.Local(local);
			return new BoundCatchBlock(this.Syntax, ImmutableArray.Create<LocalSymbol>(local), boundLocal, boundLocal.Type, null, null, block, false, false);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000D21C4 File Offset: 0x000D03C4
		internal BoundCatchBlock Catch(BoundExpression source, BoundBlock block)
		{
			return new BoundCatchBlock(this.Syntax, ImmutableArray<LocalSymbol>.Empty, source, source.Type, null, null, block, false, false);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000D21ED File Offset: 0x000D03ED
		internal BoundTryStatement Fault(BoundBlock tryBlock, BoundBlock faultBlock)
		{
			return new BoundTryStatement(this.Syntax, tryBlock, ImmutableArray<BoundCatchBlock>.Empty, faultBlock, null, true, false);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000D2204 File Offset: 0x000D0404
		internal BoundExpression NullOrDefault(TypeSymbol typeSymbol)
		{
			return SyntheticBoundNodeFactory.NullOrDefault(typeSymbol, this.Syntax);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000D2212 File Offset: 0x000D0412
		internal static BoundExpression NullOrDefault(TypeSymbol typeSymbol, SyntaxNode syntax)
		{
			if (!typeSymbol.IsReferenceType)
			{
				return SyntheticBoundNodeFactory.Default(typeSymbol, syntax);
			}
			return SyntheticBoundNodeFactory.Null(typeSymbol, syntax);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000D222B File Offset: 0x000D042B
		internal BoundExpression Not(BoundExpression expression)
		{
			return new BoundUnaryOperator(expression.Syntax, UnaryOperatorKind.BoolLogicalNegation, expression, null, null, LookupResultKind.Viable, expression.Type, false);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000D224C File Offset: 0x000D044C
		public BoundLocal StoreToTemp(BoundExpression argument, out BoundAssignmentOperator store, RefKind refKind = RefKind.None, SynthesizedLocalKind kind = SynthesizedLocalKind.LoweringTemp, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode syntaxOpt = null)
		{
			MethodSymbol currentFunction = this.CurrentFunction;
			switch (refKind)
			{
			case RefKind.None:
			case RefKind.Ref:
			case (RefKind)4:
				break;
			case RefKind.Out:
				refKind = RefKind.Ref;
				break;
			case RefKind.In:
				if (!Binder.HasHome(argument, Binder.AddressKind.ReadOnly, currentFunction, this.Compilation.IsPeVerifyCompatEnabled, null))
				{
					refKind = RefKind.None;
				}
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(refKind);
			}
			SyntaxNode syntax = argument.Syntax;
			TypeSymbol type = argument.Type;
			BoundLocal boundLocal = new BoundLocal(syntax, new SynthesizedLocal(currentFunction, TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), kind, syntaxOpt ?? (kind.IsLongLived() ? syntax : null), false, refKind), null, type, false);
			store = new BoundAssignmentOperator(syntax, boundLocal, argument, refKind > RefKind.None, type, false);
			return boundLocal;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000D22FF File Offset: 0x000D04FF
		internal BoundStatement NoOp(NoOpStatementFlavor noOpStatementFlavor)
		{
			return new BoundNoOpStatement(this.Syntax, noOpStatementFlavor);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000D2310 File Offset: 0x000D0510
		internal BoundLocal MakeTempForDiscard(BoundDiscardExpression node, ArrayBuilder<LocalSymbol> temps)
		{
			LocalSymbol localSymbol;
			BoundLocal boundLocal = this.MakeTempForDiscard(node, out localSymbol);
			temps.Add(localSymbol);
			return boundLocal;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000D2330 File Offset: 0x000D0530
		internal BoundLocal MakeTempForDiscard(BoundDiscardExpression node, out LocalSymbol temp)
		{
			temp = new SynthesizedLocal(this.CurrentFunction, TypeWithAnnotations.Create(node.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LoweringTemp, null, false, RefKind.None);
			return new BoundLocal(node.Syntax, temp, null, node.Type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000D2380 File Offset: 0x000D0580
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<BoundExpression> MakeTempsForDiscardArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, ArrayBuilder<LocalSymbol> builder)
		{
			if (arguments.Any<BoundExpression>((BoundExpression a) => a.Kind == BoundKind.DiscardExpression))
			{
				arguments = arguments.SelectAsArray(delegate(BoundExpression arg, [TupleElementNames(new string[] { "factory", "builder" })] ValueTuple<SyntheticBoundNodeFactory, ArrayBuilder<LocalSymbol>> t)
				{
					if (arg.Kind != BoundKind.DiscardExpression)
					{
						return arg;
					}
					return t.Item1.MakeTempForDiscard((BoundDiscardExpression)arg, t.Item2);
				}, new ValueTuple<SyntheticBoundNodeFactory, ArrayBuilder<LocalSymbol>>(this, builder));
			}
			return arguments;
		}

		// Token: 0x0400111F RID: 4383
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private NamedTypeSymbol _currentType;

		// Token: 0x04001120 RID: 4384
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private MethodSymbol _currentFunction;

		// Token: 0x04001121 RID: 4385
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private MethodSymbol _topLevelMethod;

		// Token: 0x04001122 RID: 4386
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Binder _binder;

		// Token: 0x02000746 RID: 1862
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public class MissingPredefinedMember : Exception
		{
			// Token: 0x06008670 RID: 34416 RVA: 0x001E85FD File Offset: 0x001E67FD
			public MissingPredefinedMember(Diagnostic error)
				: base(error.ToString())
			{
				this.Diagnostic = error;
			}

			// Token: 0x170026C9 RID: 9929
			// (get) Token: 0x06008671 RID: 34417 RVA: 0x001E8612 File Offset: 0x001E6812
			public Diagnostic Diagnostic { get; }
		}

		// Token: 0x02000747 RID: 1863
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class SyntheticBinderImpl : BuckStopsHereBinder
		{
			// Token: 0x06008672 RID: 34418 RVA: 0x001E861A File Offset: 0x001E681A
			internal SyntheticBinderImpl(SyntheticBoundNodeFactory factory)
				: base(factory.Compilation)
			{
				this._factory = factory;
			}

			// Token: 0x170026CA RID: 9930
			// (get) Token: 0x06008673 RID: 34419 RVA: 0x001E862F File Offset: 0x001E682F
			[global::System.Runtime.CompilerServices.Nullable(2)]
			internal override Symbol ContainingMemberOrLambda
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					return this._factory.CurrentFunction;
				}
			}

			// Token: 0x06008674 RID: 34420 RVA: 0x001E863C File Offset: 0x001E683C
			internal override bool IsAccessibleHelper(Symbol symbol, TypeSymbol accessThroughType, out bool failedThroughTypeCheck, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved)
			{
				return AccessCheck.IsSymbolAccessible(symbol, this._factory.CurrentType, accessThroughType, out failedThroughTypeCheck, ref useSiteInfo, basesBeingResolved);
			}

			// Token: 0x04002321 RID: 8993
			private readonly SyntheticBoundNodeFactory _factory;
		}

		// Token: 0x02000748 RID: 1864
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal class SyntheticSwitchSection
		{
			// Token: 0x06008675 RID: 34421 RVA: 0x001E8655 File Offset: 0x001E6855
			public SyntheticSwitchSection(ImmutableArray<int> Values, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> Statements)
			{
				this.Values = Values;
				this.Statements = Statements;
			}

			// Token: 0x04002322 RID: 8994
			public readonly ImmutableArray<int> Values;

			// Token: 0x04002323 RID: 8995
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableArray<BoundStatement> Statements;
		}
	}
}
