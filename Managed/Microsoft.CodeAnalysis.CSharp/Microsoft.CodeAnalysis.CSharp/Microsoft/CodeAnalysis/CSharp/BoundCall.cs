using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008C RID: 140
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundCall : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x000529D6 File Offset: 0x00050BD6
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Method;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000529E0 File Offset: 0x00050BE0
		public BoundCall(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool isDelegateCall, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: this(syntax, receiverOpt, method, arguments, argumentNamesOpt, argumentRefKindsOpt, isDelegateCall, expanded, invokedAsExtensionMethod, argsToParamsOpt, defaultArguments, resultKind, default(ImmutableArray<MethodSymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00052A18 File Offset: 0x00050C18
		public BoundCall Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool isDelegateCall, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, TypeSymbol type)
		{
			return this.Update(receiverOpt, method, arguments, argumentNamesOpt, argumentRefKindsOpt, isDelegateCall, expanded, invokedAsExtensionMethod, argsToParamsOpt, defaultArguments, resultKind, this.OriginalMethodsOpt, type);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00052A48 File Offset: 0x00050C48
		public static BoundCall ErrorCall(SyntaxNode node, BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> namedArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, bool isDelegateCall, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalMethods, LookupResultKind resultKind, Binder binder)
		{
			if (!originalMethods.IsEmpty)
			{
				resultKind = resultKind.WorseResultKind(LookupResultKind.OverloadResolutionFailure);
			}
			return new BoundCall(node, binder.BindToTypeForErrorRecovery(receiverOpt, null), method, arguments.SelectAsArray((BoundExpression e, Binder binder) => binder.BindToTypeForErrorRecovery(e, null), binder), namedArguments, refKinds, isDelegateCall, false, invokedAsExtensionMethod, default(ImmutableArray<int>), default(BitVector), resultKind, originalMethods, method.ReturnType, true);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00052AC8 File Offset: 0x00050CC8
		public BoundCall Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments)
		{
			return this.Update(this.ReceiverOpt, this.Method, arguments, this.ArgumentNamesOpt, this.ArgumentRefKindsOpt, this.IsDelegateCall, this.Expanded, this.InvokedAsExtensionMethod, this.ArgsToParamsOpt, this.DefaultArguments, this.ResultKind, this.OriginalMethodsOpt, this.Type);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00052B24 File Offset: 0x00050D24
		public BoundCall Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments)
		{
			return this.Update(receiverOpt, method, arguments, this.ArgumentNamesOpt, this.ArgumentRefKindsOpt, this.IsDelegateCall, this.Expanded, this.InvokedAsExtensionMethod, this.ArgsToParamsOpt, this.DefaultArguments, this.ResultKind, this.OriginalMethodsOpt, this.Type);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00052B76 File Offset: 0x00050D76
		public static BoundCall Synthesized(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method)
		{
			return BoundCall.Synthesized(syntax, receiverOpt, method, ImmutableArray<BoundExpression>.Empty);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00052B85 File Offset: 0x00050D85
		public static BoundCall Synthesized(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, BoundExpression arg0)
		{
			return BoundCall.Synthesized(syntax, receiverOpt, method, ImmutableArray.Create<BoundExpression>(arg0));
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00052B95 File Offset: 0x00050D95
		public static BoundCall Synthesized(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, BoundExpression arg0, BoundExpression arg1)
		{
			return BoundCall.Synthesized(syntax, receiverOpt, method, ImmutableArray.Create<BoundExpression>(arg0, arg1));
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00052BA8 File Offset: 0x00050DA8
		public static BoundCall Synthesized(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments)
		{
			return new BoundCall(syntax, receiverOpt, method, arguments, default(ImmutableArray<string>), method.ParameterRefKinds, false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, default(ImmutableArray<MethodSymbol>), method.ReturnType, method.OriginalDefinition is ErrorMethodSymbol)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00052C08 File Offset: 0x00050E08
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return CSharpOperationFactory.CreateInvalidChildrenFromArgumentsExpression(this.ReceiverOpt, this.Arguments, null);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00052C1C File Offset: 0x00050E1C
		public BoundCall(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool isDelegateCall, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalMethodsOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.Call, syntax, type, hasErrors || receiverOpt.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.ReceiverOpt = receiverOpt;
			this.Method = method;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.IsDelegateCall = isDelegateCall;
			this.Expanded = expanded;
			this.InvokedAsExtensionMethod = invokedAsExtensionMethod;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.DefaultArguments = defaultArguments;
			this._ResultKind = resultKind;
			this.OriginalMethodsOpt = originalMethodsOpt;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00052CAB File Offset: 0x00050EAB
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00052CB3 File Offset: 0x00050EB3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00052CBB File Offset: 0x00050EBB
		public MethodSymbol Method { get; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00052CC3 File Offset: 0x00050EC3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00052CCB File Offset: 0x00050ECB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00052CD3 File Offset: 0x00050ED3
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x00052CDB File Offset: 0x00050EDB
		public bool IsDelegateCall { get; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00052CE3 File Offset: 0x00050EE3
		public bool Expanded { get; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00052CEB File Offset: 0x00050EEB
		public bool InvokedAsExtensionMethod { get; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00052CF3 File Offset: 0x00050EF3
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<int> ArgsToParamsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00052CFB File Offset: 0x00050EFB
		public BitVector DefaultArguments { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00052D03 File Offset: 0x00050F03
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00052D0B File Offset: 0x00050F0B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalMethodsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00052D13 File Offset: 0x00050F13
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitCall(this);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00052D1C File Offset: 0x00050F1C
		public BoundCall Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, MethodSymbol method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool isDelegateCall, bool expanded, bool invokedAsExtensionMethod, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalMethodsOpt, TypeSymbol type)
		{
			if (receiverOpt != this.ReceiverOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(method, this.Method) || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || isDelegateCall != this.IsDelegateCall || expanded != this.Expanded || invokedAsExtensionMethod != this.InvokedAsExtensionMethod || argsToParamsOpt != this.ArgsToParamsOpt || defaultArguments != this.DefaultArguments || resultKind != this.ResultKind || originalMethodsOpt != this.OriginalMethodsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundCall boundCall = new BoundCall(this.Syntax, receiverOpt, method, arguments, argumentNamesOpt, argumentRefKindsOpt, isDelegateCall, expanded, invokedAsExtensionMethod, argsToParamsOpt, defaultArguments, resultKind, originalMethodsOpt, type, base.HasErrors);
				boundCall.CopyAttributes(this);
				return boundCall;
			}
			return this;
		}

		// Token: 0x0400049D RID: 1181
		private readonly LookupResultKind _ResultKind;
	}
}
