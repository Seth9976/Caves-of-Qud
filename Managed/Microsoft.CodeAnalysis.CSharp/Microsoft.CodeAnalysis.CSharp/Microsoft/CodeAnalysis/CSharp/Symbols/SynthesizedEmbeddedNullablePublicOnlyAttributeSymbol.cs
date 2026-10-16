using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058B RID: 1419
	internal sealed class SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol : SynthesizedEmbeddedAttributeSymbolBase
	{
		// Token: 0x06007666 RID: 30310 RVA: 0x001B7654 File Offset: 0x001B5854
		public SynthesizedEmbeddedNullablePublicOnlyAttributeSymbol(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol systemAttributeType, TypeSymbol systemBooleanType)
			: base(name, containingNamespace, containingModule, systemAttributeType)
		{
			this._fields = ImmutableArray.Create<FieldSymbol>(new SynthesizedFieldSymbol(this, systemBooleanType, "IncludesInternals", true, true, false));
			this._constructors = ImmutableArray.Create<MethodSymbol>(new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(m, TypeWithAnnotations.Create(systemBooleanType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "", default(ImmutableArray<CustomModifier>), null)), new Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>>(this.GenerateConstructorBody)));
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x001B76C2 File Offset: 0x001B58C2
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return this._fields;
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06007668 RID: 30312 RVA: 0x001B76CF File Offset: 0x001B58CF
		public override ImmutableArray<MethodSymbol> Constructors
		{
			get
			{
				return this._constructors;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06007669 RID: 30313 RVA: 0x001B76D7 File Offset: 0x001B58D7
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x0600766A RID: 30314 RVA: 0x001B76DA File Offset: 0x001B58DA
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x001B76DD File Offset: 0x001B58DD
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x001B76E0 File Offset: 0x001B58E0
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return new AttributeUsageInfo(2, false, false);
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x001B76EA File Offset: 0x001B58EA
		private void GenerateConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, ImmutableArray<ParameterSymbol> parameters)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Parameter(parameters.Single<ParameterSymbol>()), false)));
		}

		// Token: 0x04001CC3 RID: 7363
		private readonly ImmutableArray<FieldSymbol> _fields;

		// Token: 0x04001CC4 RID: 7364
		private readonly ImmutableArray<MethodSymbol> _constructors;
	}
}
