using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000588 RID: 1416
	internal sealed class SynthesizedEmbeddedNullableAttributeSymbol : SynthesizedEmbeddedAttributeSymbolBase
	{
		// Token: 0x06007651 RID: 30289 RVA: 0x001B73B4 File Offset: 0x001B55B4
		public SynthesizedEmbeddedNullableAttributeSymbol(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol systemAttributeType, TypeSymbol systemByteType)
			: base(name, containingNamespace, containingModule, systemAttributeType)
		{
			this._byteTypeSymbol = systemByteType;
			TypeWithAnnotations annotatedByteType = TypeWithAnnotations.Create(systemByteType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			TypeWithAnnotations byteArrayType = TypeWithAnnotations.Create(ArrayTypeSymbol.CreateSZArray(systemByteType.ContainingAssembly, annotatedByteType), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this._fields = ImmutableArray.Create<FieldSymbol>(new SynthesizedFieldSymbol(this, byteArrayType.Type, "NullableFlags", true, true, false));
			this._constructors = ImmutableArray.Create<MethodSymbol>(new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(m, annotatedByteType, 0, RefKind.None, "", default(ImmutableArray<CustomModifier>), null)), new Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>>(this.GenerateSingleByteConstructorBody)), new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(m, byteArrayType, 0, RefKind.None, "", default(ImmutableArray<CustomModifier>), null)), new Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>>(this.GenerateByteArrayConstructorBody)));
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x001B7483 File Offset: 0x001B5683
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return this._fields;
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06007653 RID: 30291 RVA: 0x001B7490 File Offset: 0x001B5690
		public override ImmutableArray<MethodSymbol> Constructors
		{
			get
			{
				return this._constructors;
			}
		}

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06007654 RID: 30292 RVA: 0x001B7498 File Offset: 0x001B5698
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06007655 RID: 30293 RVA: 0x001B749B File Offset: 0x001B569B
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x001B749E File Offset: 0x001B569E
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x001B74A1 File Offset: 0x001B56A1
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return new AttributeUsageInfo(27524, false, false);
		}

		// Token: 0x06007658 RID: 30296 RVA: 0x001B74AF File Offset: 0x001B56AF
		private void GenerateByteArrayConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, ImmutableArray<ParameterSymbol> parameters)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Parameter(parameters.Single<ParameterSymbol>()), false)));
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x001B74E8 File Offset: 0x001B56E8
		private void GenerateSingleByteConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, ImmutableArray<ParameterSymbol> parameters)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Array(this._byteTypeSymbol, ImmutableArray.Create<BoundExpression>(factory.Parameter(parameters.Single<ParameterSymbol>()))), false)));
		}

		// Token: 0x04001CBB RID: 7355
		private readonly ImmutableArray<FieldSymbol> _fields;

		// Token: 0x04001CBC RID: 7356
		private readonly ImmutableArray<MethodSymbol> _constructors;

		// Token: 0x04001CBD RID: 7357
		private readonly TypeSymbol _byteTypeSymbol;

		// Token: 0x04001CBE RID: 7358
		private const string NullableFlagsFieldName = "NullableFlags";
	}
}
