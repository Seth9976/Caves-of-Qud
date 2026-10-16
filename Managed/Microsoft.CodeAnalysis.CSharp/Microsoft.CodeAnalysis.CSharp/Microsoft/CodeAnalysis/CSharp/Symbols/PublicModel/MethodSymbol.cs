using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C2 RID: 1474
	internal sealed class MethodSymbol : Symbol, IMethodSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007B6F RID: 31599 RVA: 0x001C0547 File Offset: 0x001BE747
		public MethodSymbol(MethodSymbol underlying)
		{
			this._underlying = underlying;
		}

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x06007B70 RID: 31600 RVA: 0x001C0556 File Offset: 0x001BE756
		internal override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x06007B71 RID: 31601 RVA: 0x001C055E File Offset: 0x001BE75E
		internal MethodSymbol UnderlyingMethodSymbol
		{
			get
			{
				return this._underlying;
			}
		}

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06007B72 RID: 31602 RVA: 0x001C0568 File Offset: 0x001BE768
		MethodKind IMethodSymbol.MethodKind
		{
			get
			{
				switch (this._underlying.MethodKind)
				{
				case MethodKind.AnonymousFunction:
					return MethodKind.AnonymousFunction;
				case MethodKind.Constructor:
					return MethodKind.Constructor;
				case MethodKind.Conversion:
					return MethodKind.Conversion;
				case MethodKind.DelegateInvoke:
					return MethodKind.DelegateInvoke;
				case MethodKind.Destructor:
					return MethodKind.Destructor;
				case MethodKind.EventAdd:
					return MethodKind.EventAdd;
				case MethodKind.EventRemove:
					return MethodKind.EventRemove;
				case MethodKind.ExplicitInterfaceImplementation:
					return MethodKind.ExplicitInterfaceImplementation;
				case MethodKind.UserDefinedOperator:
					return MethodKind.UserDefinedOperator;
				case MethodKind.Ordinary:
					return MethodKind.Ordinary;
				case MethodKind.PropertyGet:
					return MethodKind.PropertyGet;
				case MethodKind.PropertySet:
					return MethodKind.PropertySet;
				case MethodKind.ReducedExtension:
					return MethodKind.ReducedExtension;
				case MethodKind.StaticConstructor:
					return MethodKind.StaticConstructor;
				case MethodKind.BuiltinOperator:
					return MethodKind.BuiltinOperator;
				case MethodKind.LocalFunction:
					return MethodKind.LocalFunction;
				case MethodKind.FunctionPointerSignature:
					return MethodKind.FunctionPointerSignature;
				}
				throw ExceptionUtilities.UnexpectedValue(this._underlying.MethodKind);
			}
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06007B73 RID: 31603 RVA: 0x001C0615 File Offset: 0x001BE815
		ITypeSymbol IMethodSymbol.ReturnType
		{
			get
			{
				if (this._lazyReturnType == null)
				{
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyReturnType, this._underlying.ReturnTypeWithAnnotations.GetPublicSymbol(), null);
				}
				return this._lazyReturnType;
			}
		}

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06007B74 RID: 31604 RVA: 0x001C0642 File Offset: 0x001BE842
		NullableAnnotation IMethodSymbol.ReturnNullableAnnotation
		{
			get
			{
				return this._underlying.ReturnTypeWithAnnotations.ToPublicAnnotation();
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x06007B75 RID: 31605 RVA: 0x001C0654 File Offset: 0x001BE854
		ImmutableArray<ITypeSymbol> IMethodSymbol.TypeArguments
		{
			get
			{
				if (this._lazyTypeArguments.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ITypeSymbol>(ref this._lazyTypeArguments, this._underlying.TypeArgumentsWithAnnotations.GetPublicSymbols(), default(ImmutableArray<ITypeSymbol>));
				}
				return this._lazyTypeArguments;
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x06007B76 RID: 31606 RVA: 0x001C0699 File Offset: 0x001BE899
		ImmutableArray<NullableAnnotation> IMethodSymbol.TypeArgumentNullableAnnotations
		{
			get
			{
				return this._underlying.TypeArgumentsWithAnnotations.ToPublicAnnotations();
			}
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x06007B77 RID: 31607 RVA: 0x001C06AB File Offset: 0x001BE8AB
		ImmutableArray<ITypeParameterSymbol> IMethodSymbol.TypeParameters
		{
			get
			{
				return this._underlying.TypeParameters.GetPublicSymbols();
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x06007B78 RID: 31608 RVA: 0x001C06BD File Offset: 0x001BE8BD
		ImmutableArray<IParameterSymbol> IMethodSymbol.Parameters
		{
			get
			{
				return this._underlying.Parameters.GetPublicSymbols();
			}
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x06007B79 RID: 31609 RVA: 0x001C06CF File Offset: 0x001BE8CF
		IMethodSymbol IMethodSymbol.ConstructedFrom
		{
			get
			{
				return this._underlying.ConstructedFrom.GetPublicSymbol();
			}
		}

		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x06007B7A RID: 31610 RVA: 0x001C06E1 File Offset: 0x001BE8E1
		bool IMethodSymbol.IsReadOnly
		{
			get
			{
				return this._underlying.IsEffectivelyReadOnly;
			}
		}

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x06007B7B RID: 31611 RVA: 0x001C06EE File Offset: 0x001BE8EE
		bool IMethodSymbol.IsInitOnly
		{
			get
			{
				return this._underlying.IsInitOnly;
			}
		}

		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06007B7C RID: 31612 RVA: 0x001C06FB File Offset: 0x001BE8FB
		IMethodSymbol IMethodSymbol.OriginalDefinition
		{
			get
			{
				return this._underlying.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06007B7D RID: 31613 RVA: 0x001C070D File Offset: 0x001BE90D
		IMethodSymbol IMethodSymbol.OverriddenMethod
		{
			get
			{
				return this._underlying.OverriddenMethod.GetPublicSymbol();
			}
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06007B7E RID: 31614 RVA: 0x001C071F File Offset: 0x001BE91F
		ITypeSymbol IMethodSymbol.ReceiverType
		{
			get
			{
				if (this._lazyReceiverType == null)
				{
					TypeSymbol receiverType = this._underlying.ReceiverType;
					Interlocked.CompareExchange<ITypeSymbol>(ref this._lazyReceiverType, (receiverType != null) ? receiverType.GetITypeSymbol(this._underlying.ReceiverNullableAnnotation) : null, null);
				}
				return this._lazyReceiverType;
			}
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06007B7F RID: 31615 RVA: 0x001C075E File Offset: 0x001BE95E
		NullableAnnotation IMethodSymbol.ReceiverNullableAnnotation
		{
			get
			{
				return this._underlying.ReceiverNullableAnnotation;
			}
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x06007B80 RID: 31616 RVA: 0x001C076B File Offset: 0x001BE96B
		IMethodSymbol IMethodSymbol.ReducedFrom
		{
			get
			{
				return this._underlying.ReducedFrom.GetPublicSymbol();
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x001C077D File Offset: 0x001BE97D
		ITypeSymbol IMethodSymbol.GetTypeInferredDuringReduction(ITypeParameterSymbol reducedFromTypeParameter)
		{
			return this._underlying.GetTypeInferredDuringReduction(reducedFromTypeParameter.EnsureCSharpSymbolOrNull("reducedFromTypeParameter")).GetPublicSymbol();
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x001C079A File Offset: 0x001BE99A
		IMethodSymbol IMethodSymbol.ReduceExtensionMethod(ITypeSymbol receiverType)
		{
			return this._underlying.ReduceExtensionMethod(receiverType.EnsureCSharpSymbolOrNull("receiverType"), null).GetPublicSymbol();
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x06007B83 RID: 31619 RVA: 0x001C07B8 File Offset: 0x001BE9B8
		ImmutableArray<IMethodSymbol> IMethodSymbol.ExplicitInterfaceImplementations
		{
			get
			{
				return this._underlying.ExplicitInterfaceImplementations.GetPublicSymbols();
			}
		}

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06007B84 RID: 31620 RVA: 0x001C07CA File Offset: 0x001BE9CA
		ISymbol IMethodSymbol.AssociatedSymbol
		{
			get
			{
				return this._underlying.AssociatedSymbol.GetPublicSymbol();
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x06007B85 RID: 31621 RVA: 0x001C07DC File Offset: 0x001BE9DC
		bool IMethodSymbol.IsGenericMethod
		{
			get
			{
				return this._underlying.IsGenericMethod;
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x06007B86 RID: 31622 RVA: 0x001C07E9 File Offset: 0x001BE9E9
		bool IMethodSymbol.IsAsync
		{
			get
			{
				return this._underlying.IsAsync;
			}
		}

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x06007B87 RID: 31623 RVA: 0x001C07F6 File Offset: 0x001BE9F6
		bool IMethodSymbol.HidesBaseMethodsByName
		{
			get
			{
				return this._underlying.HidesBaseMethodsByName;
			}
		}

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06007B88 RID: 31624 RVA: 0x001C0804 File Offset: 0x001BEA04
		ImmutableArray<CustomModifier> IMethodSymbol.ReturnTypeCustomModifiers
		{
			get
			{
				return this._underlying.ReturnTypeWithAnnotations.CustomModifiers;
			}
		}

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06007B89 RID: 31625 RVA: 0x001C0824 File Offset: 0x001BEA24
		ImmutableArray<CustomModifier> IMethodSymbol.RefCustomModifiers
		{
			get
			{
				return this._underlying.RefCustomModifiers;
			}
		}

		// Token: 0x06007B8A RID: 31626 RVA: 0x001C0831 File Offset: 0x001BEA31
		ImmutableArray<AttributeData> IMethodSymbol.GetReturnTypeAttributes()
		{
			return this._underlying.GetReturnTypeAttributes().Cast<CSharpAttributeData, AttributeData>();
		}

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06007B8B RID: 31627 RVA: 0x001C0843 File Offset: 0x001BEA43
		SignatureCallingConvention IMethodSymbol.CallingConvention
		{
			get
			{
				return this._underlying.CallingConvention.ToSignatureConvention();
			}
		}

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06007B8C RID: 31628 RVA: 0x001C0855 File Offset: 0x001BEA55
		ImmutableArray<INamedTypeSymbol> IMethodSymbol.UnmanagedCallingConventionTypes
		{
			get
			{
				return this._underlying.UnmanagedCallingConventionTypes.SelectAsArray<NamedTypeSymbol, INamedTypeSymbol>((NamedTypeSymbol t) => t.GetPublicSymbol());
			}
		}

		// Token: 0x06007B8D RID: 31629 RVA: 0x001C0886 File Offset: 0x001BEA86
		IMethodSymbol IMethodSymbol.Construct(params ITypeSymbol[] typeArguments)
		{
			return this._underlying.Construct(Symbol.ConstructTypeArguments(typeArguments)).GetPublicSymbol();
		}

		// Token: 0x06007B8E RID: 31630 RVA: 0x001C089E File Offset: 0x001BEA9E
		IMethodSymbol IMethodSymbol.Construct(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
		{
			return this._underlying.Construct(Symbol.ConstructTypeArguments(typeArguments, typeArgumentNullableAnnotations)).GetPublicSymbol();
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06007B8F RID: 31631 RVA: 0x001C08B7 File Offset: 0x001BEAB7
		IMethodSymbol IMethodSymbol.PartialImplementationPart
		{
			get
			{
				return this._underlying.PartialImplementationPart.GetPublicSymbol();
			}
		}

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x06007B90 RID: 31632 RVA: 0x001C08C9 File Offset: 0x001BEAC9
		IMethodSymbol IMethodSymbol.PartialDefinitionPart
		{
			get
			{
				return this._underlying.PartialDefinitionPart.GetPublicSymbol();
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x06007B91 RID: 31633 RVA: 0x001C08DB File Offset: 0x001BEADB
		bool IMethodSymbol.IsPartialDefinition
		{
			get
			{
				return this._underlying.IsPartialDefinition();
			}
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x06007B92 RID: 31634 RVA: 0x001C08E8 File Offset: 0x001BEAE8
		INamedTypeSymbol IMethodSymbol.AssociatedAnonymousDelegate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x06007B93 RID: 31635 RVA: 0x001C08EB File Offset: 0x001BEAEB
		int IMethodSymbol.Arity
		{
			get
			{
				return this._underlying.Arity;
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x06007B94 RID: 31636 RVA: 0x001C08F8 File Offset: 0x001BEAF8
		bool IMethodSymbol.IsExtensionMethod
		{
			get
			{
				return this._underlying.IsExtensionMethod;
			}
		}

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x06007B95 RID: 31637 RVA: 0x001C0905 File Offset: 0x001BEB05
		MethodImplAttributes IMethodSymbol.MethodImplementationFlags
		{
			get
			{
				return this._underlying.ImplementationAttributes;
			}
		}

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x06007B96 RID: 31638 RVA: 0x001C0912 File Offset: 0x001BEB12
		bool IMethodSymbol.IsVararg
		{
			get
			{
				return this._underlying.IsVararg;
			}
		}

		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x06007B97 RID: 31639 RVA: 0x001C091F File Offset: 0x001BEB1F
		bool IMethodSymbol.IsCheckedBuiltin
		{
			get
			{
				return this._underlying.IsCheckedBuiltin;
			}
		}

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x06007B98 RID: 31640 RVA: 0x001C092C File Offset: 0x001BEB2C
		bool IMethodSymbol.ReturnsVoid
		{
			get
			{
				return this._underlying.ReturnsVoid;
			}
		}

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x06007B99 RID: 31641 RVA: 0x001C0939 File Offset: 0x001BEB39
		bool IMethodSymbol.ReturnsByRef
		{
			get
			{
				return this._underlying.ReturnsByRef;
			}
		}

		// Token: 0x170023E7 RID: 9191
		// (get) Token: 0x06007B9A RID: 31642 RVA: 0x001C0946 File Offset: 0x001BEB46
		bool IMethodSymbol.ReturnsByRefReadonly
		{
			get
			{
				return this._underlying.ReturnsByRefReadonly;
			}
		}

		// Token: 0x170023E8 RID: 9192
		// (get) Token: 0x06007B9B RID: 31643 RVA: 0x001C0953 File Offset: 0x001BEB53
		RefKind IMethodSymbol.RefKind
		{
			get
			{
				return this._underlying.RefKind;
			}
		}

		// Token: 0x170023E9 RID: 9193
		// (get) Token: 0x06007B9C RID: 31644 RVA: 0x001C0960 File Offset: 0x001BEB60
		bool IMethodSymbol.IsConditional
		{
			get
			{
				return this._underlying.IsConditional;
			}
		}

		// Token: 0x06007B9D RID: 31645 RVA: 0x001C096D File Offset: 0x001BEB6D
		DllImportData IMethodSymbol.GetDllImportData()
		{
			return this._underlying.GetDllImportData();
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x001C097A File Offset: 0x001BEB7A
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitMethod(this);
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x001C0983 File Offset: 0x001BEB83
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitMethod(this);
		}

		// Token: 0x04001D42 RID: 7490
		private readonly MethodSymbol _underlying;

		// Token: 0x04001D43 RID: 7491
		private ITypeSymbol _lazyReturnType;

		// Token: 0x04001D44 RID: 7492
		private ImmutableArray<ITypeSymbol> _lazyTypeArguments;

		// Token: 0x04001D45 RID: 7493
		private ITypeSymbol _lazyReceiverType;
	}
}
