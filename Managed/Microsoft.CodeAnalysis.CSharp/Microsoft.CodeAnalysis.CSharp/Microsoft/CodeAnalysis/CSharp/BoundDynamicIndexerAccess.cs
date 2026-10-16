using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000093 RID: 147
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicIndexerAccess : BoundExpression
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0005372C File Offset: 0x0005192C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal string TryGetIndexedPropertyName()
		{
			foreach (PropertySymbol propertySymbol in this.ApplicableIndexers)
			{
				if (!propertySymbol.IsIndexer && propertySymbol.IsIndexedProperty)
				{
					return propertySymbol.Name;
				}
			}
			return null;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00053774 File Offset: 0x00051974
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.Arguments.Insert(0, this.Receiver));
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0005379C File Offset: 0x0005199C
		public BoundDynamicIndexerAccess(SyntaxNode syntax, BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> applicableIndexers, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DynamicIndexerAccess, syntax, type, hasErrors || receiver.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.Receiver = receiver;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.ApplicableIndexers = applicableIndexers;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000537F2 File Offset: 0x000519F2
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x000537FA File Offset: 0x000519FA
		public BoundExpression Receiver { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00053802 File Offset: 0x00051A02
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0005380A File Offset: 0x00051A0A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00053812 File Offset: 0x00051A12
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0005381A File Offset: 0x00051A1A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<PropertySymbol> ApplicableIndexers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00053822 File Offset: 0x00051A22
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicIndexerAccess(this);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0005382C File Offset: 0x00051A2C
		public BoundDynamicIndexerAccess Update(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> applicableIndexers, TypeSymbol type)
		{
			if (receiver != this.Receiver || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || applicableIndexers != this.ApplicableIndexers || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicIndexerAccess boundDynamicIndexerAccess = new BoundDynamicIndexerAccess(this.Syntax, receiver, arguments, argumentNamesOpt, argumentRefKindsOpt, applicableIndexers, type, base.HasErrors);
				boundDynamicIndexerAccess.CopyAttributes(this);
				return boundDynamicIndexerAccess;
			}
			return this;
		}
	}
}
