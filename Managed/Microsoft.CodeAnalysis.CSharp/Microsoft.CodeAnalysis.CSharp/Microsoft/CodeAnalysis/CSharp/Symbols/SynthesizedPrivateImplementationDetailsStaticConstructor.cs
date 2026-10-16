using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B7 RID: 1207
	internal sealed class SynthesizedPrivateImplementationDetailsStaticConstructor : SynthesizedGlobalMethodSymbol
	{
		// Token: 0x060066FB RID: 26363 RVA: 0x00185661 File Offset: 0x00183861
		internal SynthesizedPrivateImplementationDetailsStaticConstructor(SourceModuleSymbol containingModule, PrivateImplementationDetails privateImplementationType, NamedTypeSymbol voidType)
			: base(containingModule, privateImplementationType, voidType, ".cctor")
		{
			base.SetParameters(ImmutableArray<ParameterSymbol>.Empty);
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x060066FC RID: 26364 RVA: 0x0018567C File Offset: 0x0018387C
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.StaticConstructor;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x060066FD RID: 26365 RVA: 0x00185680 File Offset: 0x00183880
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x00185684 File Offset: 0x00183884
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			CSharpSyntaxNode nonNullSyntaxNode = this.GetNonNullSyntaxNode();
			SyntheticBoundNodeFactory syntheticBoundNodeFactory = new SyntheticBoundNodeFactory(this, nonNullSyntaxNode, compilationState, diagnostics);
			syntheticBoundNodeFactory.CurrentFunction = this;
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance();
			try
			{
				foreach (KeyValuePair<int, InstrumentationPayloadRootField> keyValuePair in base.ContainingPrivateImplementationDetailsType.GetInstrumentationPayloadRoots())
				{
					int key = keyValuePair.Key;
					ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)keyValuePair.Value.Type.GetInternalSymbol();
					BoundStatement boundStatement = syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.InstrumentationPayloadRoot(key, arrayTypeSymbol), syntheticBoundNodeFactory.Array(arrayTypeSymbol.ElementType, syntheticBoundNodeFactory.Binary(BinaryOperatorKind.Addition, syntheticBoundNodeFactory.SpecialType(SpecialType.System_Int32), syntheticBoundNodeFactory.MaximumMethodDefIndex(), syntheticBoundNodeFactory.Literal(1))), false);
					instance.Add(boundStatement);
				}
				instance.Add(syntheticBoundNodeFactory.Assignment(syntheticBoundNodeFactory.ModuleVersionId(), syntheticBoundNodeFactory.New(syntheticBoundNodeFactory.WellKnownMethod(WellKnownMember.System_Guid__ctor), new BoundExpression[] { syntheticBoundNodeFactory.ModuleVersionIdString() }), false));
			}
			catch (SyntheticBoundNodeFactory.MissingPredefinedMember missingPredefinedMember)
			{
				diagnostics.Add(missingPredefinedMember.Diagnostic);
			}
			BoundStatement boundStatement2 = syntheticBoundNodeFactory.Return(null);
			instance.Add(boundStatement2);
			syntheticBoundNodeFactory.CloseMethod(syntheticBoundNodeFactory.Block(instance.ToImmutableAndFree()));
		}
	}
}
