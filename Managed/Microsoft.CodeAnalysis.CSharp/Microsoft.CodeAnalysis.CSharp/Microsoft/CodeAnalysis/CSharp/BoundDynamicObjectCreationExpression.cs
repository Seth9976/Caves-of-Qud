using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E0 RID: 224
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicObjectCreationExpression : BoundExpression
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0005E97C File Offset: 0x0005CB7C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.Arguments.AddRange(BoundObjectCreationExpression.GetChildInitializers(this.InitializerExpressionOpt)));
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0005E9A8 File Offset: 0x0005CBA8
		public BoundDynamicObjectCreationExpression(SyntaxNode syntax, string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DynamicObjectCreationExpression, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>() || initializerExpressionOpt.HasErrors())
		{
			this.Name = name;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.InitializerExpressionOpt = initializerExpressionOpt;
			this.ApplicableMethods = applicableMethods;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0005EA07 File Offset: 0x0005CC07
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0005EA0F File Offset: 0x0005CC0F
		public string Name { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0005EA17 File Offset: 0x0005CC17
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0005EA1F File Offset: 0x0005CC1F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0005EA27 File Offset: 0x0005CC27
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0005EA2F File Offset: 0x0005CC2F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundObjectInitializerExpressionBase InitializerExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0005EA37 File Offset: 0x0005CC37
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> ApplicableMethods
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0005EA3F File Offset: 0x0005CC3F
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicObjectCreationExpression(this);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0005EA48 File Offset: 0x0005CC48
		public BoundDynamicObjectCreationExpression Update(string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, TypeSymbol type)
		{
			if (name != this.Name || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || initializerExpressionOpt != this.InitializerExpressionOpt || applicableMethods != this.ApplicableMethods || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicObjectCreationExpression boundDynamicObjectCreationExpression = new BoundDynamicObjectCreationExpression(this.Syntax, name, arguments, argumentNamesOpt, argumentRefKindsOpt, initializerExpressionOpt, applicableMethods, type, base.HasErrors);
				boundDynamicObjectCreationExpression.CopyAttributes(this);
				return boundDynamicObjectCreationExpression;
			}
			return this;
		}
	}
}
