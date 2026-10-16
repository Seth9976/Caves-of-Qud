using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000587 RID: 1415
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedEmbeddedNativeIntegerAttributeSymbol : SynthesizedEmbeddedAttributeSymbolBase
	{
		// Token: 0x06007646 RID: 30278 RVA: 0x001B7214 File Offset: 0x001B5414
		public SynthesizedEmbeddedNativeIntegerAttributeSymbol(string name, NamespaceSymbol containingNamespace, ModuleSymbol containingModule, NamedTypeSymbol systemAttributeType, TypeSymbol boolType)
			: base(name, containingNamespace, containingModule, systemAttributeType)
		{
			this._boolType = boolType;
			TypeWithAnnotations boolArrayType = TypeWithAnnotations.Create(ArrayTypeSymbol.CreateSZArray(boolType.ContainingAssembly, TypeWithAnnotations.Create(boolType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			this._fields = ImmutableArray.Create<FieldSymbol>(new SynthesizedFieldSymbol(this, boolArrayType.Type, "TransformFlags", true, true, false));
			this._constructors = ImmutableArray.Create<MethodSymbol>(new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray<ParameterSymbol>.Empty, delegate(SyntheticBoundNodeFactory f, ArrayBuilder<BoundStatement> s, ImmutableArray<ParameterSymbol> p)
			{
				this.GenerateParameterlessConstructorBody(f, s);
			}), new SynthesizedEmbeddedAttributeConstructorWithBodySymbol(this, (MethodSymbol m) => ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(m, boolArrayType, 0, RefKind.None, "", default(ImmutableArray<CustomModifier>), null)), delegate(SyntheticBoundNodeFactory f, ArrayBuilder<BoundStatement> s, ImmutableArray<ParameterSymbol> p)
			{
				this.GenerateBoolArrayConstructorBody(f, s, p);
			}));
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x001B72EA File Offset: 0x001B54EA
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			return this._fields;
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x06007648 RID: 30280 RVA: 0x001B72F7 File Offset: 0x001B54F7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MethodSymbol> Constructors
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._constructors;
			}
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x06007649 RID: 30281 RVA: 0x001B72FF File Offset: 0x001B54FF
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x0600764A RID: 30282 RVA: 0x001B7302 File Offset: 0x001B5502
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x001B7305 File Offset: 0x001B5505
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x001B7308 File Offset: 0x001B5508
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return new AttributeUsageInfo(27524, false, false);
		}

		// Token: 0x0600764D RID: 30285 RVA: 0x001B7318 File Offset: 0x001B5518
		private void GenerateParameterlessConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Array(this._boolType, ImmutableArray.Create<BoundExpression>(factory.Literal(true))), false)));
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x001B7367 File Offset: 0x001B5567
		private void GenerateBoolArrayConstructorBody(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ParameterSymbol> parameters)
		{
			statements.Add(factory.ExpressionStatement(factory.AssignmentExpression(factory.Field(factory.This(), this._fields.Single<FieldSymbol>()), factory.Parameter(parameters.Single<ParameterSymbol>()), false)));
		}

		// Token: 0x04001CB7 RID: 7351
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<FieldSymbol> _fields;

		// Token: 0x04001CB8 RID: 7352
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<MethodSymbol> _constructors;

		// Token: 0x04001CB9 RID: 7353
		private readonly TypeSymbol _boolType;

		// Token: 0x04001CBA RID: 7354
		private const string FieldName = "TransformFlags";
	}
}
