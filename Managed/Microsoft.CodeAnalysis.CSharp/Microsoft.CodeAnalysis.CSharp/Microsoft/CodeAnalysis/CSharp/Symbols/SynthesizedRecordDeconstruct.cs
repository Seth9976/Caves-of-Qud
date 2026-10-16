using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000573 RID: 1395
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordDeconstruct : SynthesizedRecordOrdinaryMethod
	{
		// Token: 0x0600753C RID: 30012 RVA: 0x001B4CF5 File Offset: 0x001B2EF5
		public SynthesizedRecordDeconstruct(SourceMemberContainerTypeSymbol containingType, SynthesizedRecordConstructor ctor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> positionalMembers, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, "Deconstruct", true, memberOffset, diagnostics)
		{
			this._ctor = ctor;
			this._positionalMembers = positionalMembers;
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x001B4D16 File Offset: 0x001B2F16
		protected override DeclarationModifiers MakeDeclarationModifiers(DeclarationModifiers allowedModifiers, BindingDiagnosticBag diagnostics)
		{
			return DeclarationModifiers.Public;
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x001B4D1C File Offset: 0x001B2F1C
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters", "IsVararg", "DeclaredConstraintsForOverrideOrImplementation" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location returnTypeLocation = this.ReturnTypeLocation;
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>, bool, ImmutableArray<TypeParameterConstraintClause>>(TypeWithAnnotations.Create(Binder.GetSpecialType(declaringCompilation, SpecialType.System_Void, returnTypeLocation, diagnostics), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), this._ctor.Parameters.SelectAsArray((ParameterSymbol param, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations) => new SourceSimpleParameterSymbol(this, param.TypeWithAnnotations, param.Ordinal, RefKind.Out, param.Name, locations), this.Locations), false, ImmutableArray<TypeParameterConstraintClause>.Empty);
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x001B4D7A File Offset: 0x001B2F7A
		protected override int GetParameterCountFromSyntax()
		{
			return this._ctor.ParameterCount;
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x001B4D88 File Offset: 0x001B2F88
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, this.ContainingType.GetNonNullSyntaxNode(), compilationState, diagnostics);
			if (this.ParameterCount != this._positionalMembers.Length)
			{
				syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
				return;
			}
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance(this._positionalMembers.Length + 1);
			for (int i = 0; i < this._positionalMembers.Length; i++)
			{
				ParameterSymbol parameterSymbol = this.Parameters[i];
				Symbol symbol = this._positionalMembers[i];
				PropertySymbol propertySymbol = symbol as PropertySymbol;
				TypeSymbol typeSymbol;
				if (propertySymbol == null)
				{
					FieldSymbol fieldSymbol = symbol as FieldSymbol;
					if (fieldSymbol == null)
					{
						throw ExceptionUtilities.Unreachable;
					}
					typeSymbol = fieldSymbol.Type;
				}
				else
				{
					typeSymbol = propertySymbol.Type;
				}
				TypeSymbol typeSymbol2 = typeSymbol;
				if (!parameterSymbol.Type.Equals(typeSymbol2, TypeCompareKind.AllIgnoreOptions))
				{
					instance.Free();
					syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.ThrowNull());
					return;
				}
				PropertySymbol propertySymbol2 = symbol as PropertySymbol;
				if (propertySymbol2 == null)
				{
					FieldSymbol fieldSymbol2 = symbol as FieldSymbol;
					if (fieldSymbol2 != null)
					{
						instance.Add(syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Parameter(parameterSymbol), syntheticBoundNodeFactory.Field(syntheticBoundNodeFactory.This(), fieldSymbol2), false));
					}
				}
				else
				{
					instance.Add(syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.Parameter(parameterSymbol), syntheticBoundNodeFactory.Property(syntheticBoundNodeFactory.This(), propertySymbol2), false));
				}
			}
			instance.Add(syntheticBoundNodeFactory.Return(null));
			syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(instance.ToImmutableAndFree()));
		}

		// Token: 0x04001C9C RID: 7324
		private readonly SynthesizedRecordConstructor _ctor;

		// Token: 0x04001C9D RID: 7325
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<Symbol> _positionalMembers;
	}
}
