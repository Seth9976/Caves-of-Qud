using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004B9 RID: 1209
	internal class SynthesizedStateMachineProperty : PropertySymbol, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x0600676D RID: 26477 RVA: 0x00185F78 File Offset: 0x00184178
		internal SynthesizedStateMachineProperty(MethodSymbol interfacePropertyGetter, StateMachineTypeSymbol stateMachineType)
		{
			this._name = ExplicitInterfaceHelpers.GetMemberName(interfacePropertyGetter.AssociatedSymbol.Name, interfacePropertyGetter.ContainingType, null);
			string memberName = ExplicitInterfaceHelpers.GetMemberName(interfacePropertyGetter.Name, interfacePropertyGetter.ContainingType, null);
			this._getter = new SynthesizedStateMachineDebuggerHiddenMethod(memberName, interfacePropertyGetter, stateMachineType, this, false);
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x0600676E RID: 26478 RVA: 0x00185FCB File Offset: 0x001841CB
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x00185FD3 File Offset: 0x001841D3
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x00185FD6 File Offset: 0x001841D6
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._getter.ReturnTypeWithAnnotations;
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x00185FE3 File Offset: 0x001841E3
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._getter.RefCustomModifiers;
			}
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06006772 RID: 26482 RVA: 0x00185FF0 File Offset: 0x001841F0
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._getter.Parameters;
			}
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x00186000 File Offset: 0x00184200
		public override bool IsIndexer
		{
			get
			{
				return !this._getter.Parameters.IsEmpty;
			}
		}

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06006774 RID: 26484 RVA: 0x00186023 File Offset: 0x00184223
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06006775 RID: 26485 RVA: 0x00186026 File Offset: 0x00184226
		public override MethodSymbol GetMethod
		{
			get
			{
				return this._getter;
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06006776 RID: 26486 RVA: 0x0018602E File Offset: 0x0018422E
		public override MethodSymbol SetMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x00186031 File Offset: 0x00184231
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this._getter.CallingConvention;
			}
		}

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06006778 RID: 26488 RVA: 0x0018603E File Offset: 0x0018423E
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06006779 RID: 26489 RVA: 0x00186044 File Offset: 0x00184244
		private PropertySymbol ImplementedProperty
		{
			get
			{
				return (PropertySymbol)this._getter.ExplicitInterfaceImplementations[0].AssociatedSymbol;
			}
		}

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x0600677A RID: 26490 RVA: 0x0018606F File Offset: 0x0018426F
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray.Create<PropertySymbol>(this.ImplementedProperty);
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x0600677B RID: 26491 RVA: 0x0018607C File Offset: 0x0018427C
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._getter.ContainingSymbol;
			}
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x00186089 File Offset: 0x00184289
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x0600677D RID: 26493 RVA: 0x00186090 File Offset: 0x00184290
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x0600677E RID: 26494 RVA: 0x00186097 File Offset: 0x00184297
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this._getter.DeclaredAccessibility;
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x0600677F RID: 26495 RVA: 0x001860A4 File Offset: 0x001842A4
		public override bool IsStatic
		{
			get
			{
				return this._getter.IsStatic;
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x001860B1 File Offset: 0x001842B1
		public override bool IsVirtual
		{
			get
			{
				return this._getter.IsVirtual;
			}
		}

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06006781 RID: 26497 RVA: 0x001860BE File Offset: 0x001842BE
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06006782 RID: 26498 RVA: 0x001860C1 File Offset: 0x001842C1
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06006783 RID: 26499 RVA: 0x001860C4 File Offset: 0x001842C4
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06006784 RID: 26500 RVA: 0x001860C7 File Offset: 0x001842C7
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06006785 RID: 26501 RVA: 0x001860CA File Offset: 0x001842CA
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06006786 RID: 26502 RVA: 0x001860CD File Offset: 0x001842CD
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return this._getter.HasMethodBodyDependency;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06006787 RID: 26503 RVA: 0x001860DA File Offset: 0x001842DA
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return ((ISynthesizedMethodBodyImplementationSymbol)this.ContainingSymbol).Method;
			}
		}

		// Token: 0x040019B5 RID: 6581
		private readonly SynthesizedStateMachineMethod _getter;

		// Token: 0x040019B6 RID: 6582
		private readonly string _name;
	}
}
