using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058A RID: 1418
	internal sealed class SynthesizedEmbeddedNullableContextAttributeSymbol : SynthesizedEmbeddedAttributeSymbolBase
	{
		// Token: 0x0600765E RID: 30302 RVA: 0x001B7580 File Offset: 0x001B5780
		public SynthesizedEmbeddedNullableContextAttributeSymbol(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol systemAttributeType, TypeSymbol systemByteType)
			: base(name, containingNamespace, containingModule, systemAttributeType)
		{
			this._fields = ImmutableArray.Create<FieldSymbol>(new SynthesizedFieldSymbol(this, systemByteType, "Flag", true, true, false));
			this._constructors = ImmutableArray.Create<MethodSymbol>(new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(m, TypeWithAnnotations.Create(systemByteType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "", default(ImmutableArray<CustomModifier>), null)), new Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>>(this.GenerateConstructorBody)));
		}

		// Token: 0x0600765F RID: 30303 RVA: 0x001B75EE File Offset: 0x001B57EE
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return this._fields;
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06007660 RID: 30304 RVA: 0x001B75FB File Offset: 0x001B57FB
		public override ImmutableArray<MethodSymbol> Constructors
		{
			get
			{
				return this._constructors;
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06007661 RID: 30305 RVA: 0x001B7603 File Offset: 0x001B5803
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06007662 RID: 30306 RVA: 0x001B7606 File Offset: 0x001B5806
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x001B7609 File Offset: 0x001B5809
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x001B760C File Offset: 0x001B580C
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return new AttributeUsageInfo(5196, false, false);
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x001B761A File Offset: 0x001B581A
		private void GenerateConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, ImmutableArray<ParameterSymbol> parameters)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Parameter(parameters.Single<ParameterSymbol>()), false)));
		}

		// Token: 0x04001CC1 RID: 7361
		private readonly ImmutableArray<FieldSymbol> _fields;

		// Token: 0x04001CC2 RID: 7362
		private readonly ImmutableArray<MethodSymbol> _constructors;
	}
}
