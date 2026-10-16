using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057E RID: 1406
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedRecordPropertySymbol : SourcePropertySymbolBase
	{
		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06007591 RID: 30097 RVA: 0x001B6430 File Offset: 0x001B4630
		public SourceParameterSymbol BackingParameter { get; }

		// Token: 0x06007592 RID: 30098 RVA: 0x001B6438 File Offset: 0x001B4638
		public SynthesizedRecordPropertySymbol(SourceMemberContainerTypeSymbol containingType, CSharpSyntaxNode syntax, ParameterSymbol backingParameter, bool isOverride, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax, true, true, false, null, null, DeclarationModifiers.Public | (isOverride ? DeclarationModifiers.Override : DeclarationModifiers.None), true, true, false, SynthesizedRecordPropertySymbol.ShouldUseInit(containingType), RefKind.None, backingParameter.Name, default(SyntaxList<AttributeListSyntax>), backingParameter.Locations[0], diagnostics)
		{
			this.BackingParameter = (SourceParameterSymbol)backingParameter;
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06007593 RID: 30099 RVA: 0x001B6497 File Offset: 0x001B4697
		public override IAttributeTargetSymbol AttributesOwner
		{
			get
			{
				return (this.BackingParameter as IAttributeTargetSymbol) ?? this;
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06007594 RID: 30100 RVA: 0x001B64A9 File Offset: 0x001B46A9
		protected override Location TypeLocation
		{
			get
			{
				return ((ParameterSyntax)base.CSharpSyntaxNode).Type.Location;
			}
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06007595 RID: 30101 RVA: 0x001B64C0 File Offset: 0x001B46C0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeDeclarationSyntaxList
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.BackingParameter.AttributeDeclarationList;
			}
		}

		// Token: 0x06007596 RID: 30102 RVA: 0x001B64CD File Offset: 0x001B46CD
		protected override SourcePropertyAccessorSymbol CreateGetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			return this.CreateAccessorSymbol(true, base.CSharpSyntaxNode, diagnostics);
		}

		// Token: 0x06007597 RID: 30103 RVA: 0x001B64DD File Offset: 0x001B46DD
		protected override SourcePropertyAccessorSymbol CreateSetAccessorSymbol(bool isAutoPropertyAccessor, BindingDiagnosticBag diagnostics)
		{
			return this.CreateAccessorSymbol(false, base.CSharpSyntaxNode, diagnostics);
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x001B64ED File Offset: 0x001B46ED
		private static bool ShouldUseInit(TypeSymbol container)
		{
			return !container.IsStructType() || container.IsReadOnly;
		}

		// Token: 0x06007599 RID: 30105 RVA: 0x001B6500 File Offset: 0x001B4700
		private SourcePropertyAccessorSymbol CreateAccessorSymbol(bool isGet, CSharpSyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			bool flag = !isGet && SynthesizedRecordPropertySymbol.ShouldUseInit(this.ContainingType);
			return SourcePropertyAccessorSymbol.CreateAccessorSymbol(isGet, flag, this.ContainingType, this, this._modifiers, ((ParameterSyntax)syntax).Identifier.GetLocation(), syntax, diagnostics);
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x001B6548 File Offset: 0x001B4748
		[return: TupleElementNames(new string[] { "Type", "Parameters" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindType(BindingDiagnosticBag diagnostics)
		{
			return new ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>>(this.BackingParameter.TypeWithAnnotations, ImmutableArray<ParameterSymbol>.Empty);
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x0600759B RID: 30107 RVA: 0x001B655F File Offset: 0x001B475F
		protected override bool HasPointerTypeSyntactically
		{
			get
			{
				return this.TypeWithAnnotations.DefaultType.IsPointerOrFunctionPointer();
			}
		}

		// Token: 0x0600759C RID: 30108 RVA: 0x001B6574 File Offset: 0x001B4774
		public static bool HaveCorrespondingSynthesizedRecordPropertySymbol(SourceParameterSymbol parameter)
		{
			if (parameter.ContainingSymbol is SynthesizedRecordConstructor)
			{
				return parameter.ContainingType.GetMembersUnordered().Any(delegate(Symbol s, SourceParameterSymbol parameter)
				{
					SynthesizedRecordPropertySymbol synthesizedRecordPropertySymbol = s as SynthesizedRecordPropertySymbol;
					return ((synthesizedRecordPropertySymbol != null) ? synthesizedRecordPropertySymbol.BackingParameter : null) == parameter;
				}, parameter);
			}
			return false;
		}
	}
}
