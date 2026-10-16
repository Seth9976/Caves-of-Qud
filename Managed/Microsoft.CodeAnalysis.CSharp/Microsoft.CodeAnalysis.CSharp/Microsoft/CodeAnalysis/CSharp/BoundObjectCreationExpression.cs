using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009E RID: 158
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundObjectCreationExpression : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00054662 File Offset: 0x00052862
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0005466A File Offset: 0x0005286A
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Constructor;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00054674 File Offset: 0x00052874
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		internal BoundObjectCreationExpression UpdateArgumentsAndInitializer([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> newArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> newRefKinds, BoundObjectInitializerExpressionBase newInitializerExpression, TypeSymbol changeTypeOpt = null)
		{
			return this.Update(this.Constructor, newArguments, default(ImmutableArray<string>), newRefKinds, false, default(ImmutableArray<int>), default(BitVector), this.ConstantValueOpt, newInitializerExpression, changeTypeOpt ?? this.Type);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000546C0 File Offset: 0x000528C0
		public BoundObjectCreationExpression(SyntaxNode syntax, MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, constructor, ImmutableArray<MethodSymbol>.Empty, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, constantValueOpt, initializerExpressionOpt, false, type, hasErrors)
		{
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000546F0 File Offset: 0x000528F0
		public BoundObjectCreationExpression Update(MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type)
		{
			return this.Update(constructor, ImmutableArray<MethodSymbol>.Empty, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, constantValueOpt, initializerExpressionOpt, this.WasTargetTyped, type);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00054720 File Offset: 0x00052920
		public BoundObjectCreationExpression Update(MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> constructorsGroup, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, TypeSymbol type)
		{
			return this.Update(constructor, constructorsGroup, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, constantValueOpt, initializerExpressionOpt, this.WasTargetTyped, type);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0005474C File Offset: 0x0005294C
		public BoundObjectCreationExpression(SyntaxNode syntax, MethodSymbol constructor, params BoundExpression[] arguments)
			: this(syntax, constructor, ImmutableArray.Create<BoundExpression>(arguments), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), null, null, constructor.ContainingType, false)
		{
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00054798 File Offset: 0x00052998
		public BoundObjectCreationExpression(SyntaxNode syntax, MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments)
			: this(syntax, constructor, arguments, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), null, null, constructor.ContainingType, false)
		{
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000547DC File Offset: 0x000529DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<BoundExpression> GetChildInitializers(BoundExpression objectOrCollectionInitializer)
		{
			BoundObjectInitializerExpression boundObjectInitializerExpression = objectOrCollectionInitializer as BoundObjectInitializerExpression;
			if (boundObjectInitializerExpression != null)
			{
				return boundObjectInitializerExpression.Initializers;
			}
			BoundCollectionInitializerExpression boundCollectionInitializerExpression = objectOrCollectionInitializer as BoundCollectionInitializerExpression;
			if (boundCollectionInitializerExpression != null)
			{
				return boundCollectionInitializerExpression.Initializers;
			}
			return ImmutableArray<BoundExpression>.Empty;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00054810 File Offset: 0x00052A10
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return CSharpOperationFactory.CreateInvalidChildrenFromArgumentsExpression(null, this.Arguments, this.InitializerExpressionOpt);
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00054824 File Offset: 0x00052A24
		public BoundObjectCreationExpression(SyntaxNode syntax, MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> constructorsGroup, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, bool wasTargetTyped, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ObjectCreationExpression, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>() || initializerExpressionOpt.HasErrors())
		{
			this.Constructor = constructor;
			this.ConstructorsGroup = constructorsGroup;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.Expanded = expanded;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.DefaultArguments = defaultArguments;
			this.ConstantValueOpt = constantValueOpt;
			this.InitializerExpressionOpt = initializerExpressionOpt;
			this.WasTargetTyped = wasTargetTyped;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x000548AC File Offset: 0x00052AAC
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x000548B4 File Offset: 0x00052AB4
		public MethodSymbol Constructor { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000548BC File Offset: 0x00052ABC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> ConstructorsGroup
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000548C4 File Offset: 0x00052AC4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000548CC File Offset: 0x00052ACC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000548D4 File Offset: 0x00052AD4
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000548DC File Offset: 0x00052ADC
		public bool Expanded { get; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x000548E4 File Offset: 0x00052AE4
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<int> ArgsToParamsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000548EC File Offset: 0x00052AEC
		public BitVector DefaultArguments { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000548F4 File Offset: 0x00052AF4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000548FC File Offset: 0x00052AFC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundObjectInitializerExpressionBase InitializerExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00054904 File Offset: 0x00052B04
		public bool WasTargetTyped { get; }

		// Token: 0x06000C76 RID: 3190 RVA: 0x0005490C File Offset: 0x00052B0C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitObjectCreationExpression(this);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00054918 File Offset: 0x00052B18
		public BoundObjectCreationExpression Update(MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> constructorsGroup, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundObjectInitializerExpressionBase initializerExpressionOpt, bool wasTargetTyped, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(constructor, this.Constructor) || constructorsGroup != this.ConstructorsGroup || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || expanded != this.Expanded || argsToParamsOpt != this.ArgsToParamsOpt || defaultArguments != this.DefaultArguments || constantValueOpt != this.ConstantValueOpt || initializerExpressionOpt != this.InitializerExpressionOpt || wasTargetTyped != this.WasTargetTyped || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundObjectCreationExpression boundObjectCreationExpression = new BoundObjectCreationExpression(this.Syntax, constructor, constructorsGroup, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, constantValueOpt, initializerExpressionOpt, wasTargetTyped, type, base.HasErrors);
				boundObjectCreationExpression.CopyAttributes(this);
				return boundObjectCreationExpression;
			}
			return this;
		}
	}
}
