using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B4 RID: 180
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMethodGroup : BoundMethodOrPropertyGroup
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00056554 File Offset: 0x00054754
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundMethodGroup(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, BoundExpression receiverOpt, string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> methods, LookupResult lookupResult, BoundMethodGroupFlags flags, bool hasErrors = false)
			: this(syntax, typeArgumentsOpt, name, methods, lookupResult.SingleSymbolOrDefault, lookupResult.Error, new BoundMethodGroupFlags?(flags), receiverOpt, lookupResult.Kind, hasErrors)
		{
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0005658C File Offset: 0x0005478C
		public MemberAccessExpressionSyntax MemberAccessExpressionSyntax
		{
			get
			{
				return this.Syntax as MemberAccessExpressionSyntax;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0005659C File Offset: 0x0005479C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxNode NameSyntax
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax = this.MemberAccessExpressionSyntax;
				if (memberAccessExpressionSyntax != null)
				{
					return memberAccessExpressionSyntax.Name;
				}
				return this.Syntax;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000565C0 File Offset: 0x000547C0
		public BoundExpression InstanceOpt
		{
			get
			{
				if (base.ReceiverOpt == null || base.ReceiverOpt.Kind == BoundKind.TypeExpression)
				{
					return null;
				}
				return base.ReceiverOpt;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x000565E4 File Offset: 0x000547E4
		public bool SearchExtensionMethods
		{
			get
			{
				BoundMethodGroupFlags? boundMethodGroupFlags = this.Flags & BoundMethodGroupFlags.SearchExtensionMethods;
				BoundMethodGroupFlags boundMethodGroupFlags2 = BoundMethodGroupFlags.None;
				return !((boundMethodGroupFlags.GetValueOrDefault() == boundMethodGroupFlags2) & (boundMethodGroupFlags != null));
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00056632 File Offset: 0x00054832
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return MessageID.IDS_MethodGroup.Localize();
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00056644 File Offset: 0x00054844
		public BoundMethodGroup([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, [global::System.Runtime.CompilerServices.Nullable(1)] string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> methods, Symbol lookupSymbolOpt, DiagnosticInfo lookupError, BoundMethodGroupFlags? flags, BoundExpression receiverOpt, LookupResultKind resultKind, bool hasErrors = false)
			: base(BoundKind.MethodGroup, syntax, receiverOpt, resultKind, hasErrors || receiverOpt.HasErrors())
		{
			this.TypeArgumentsOpt = typeArgumentsOpt;
			this.Name = name;
			this.Methods = methods;
			this.LookupSymbolOpt = lookupSymbolOpt;
			this.LookupError = lookupError;
			this.Flags = flags;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0005669D File Offset: 0x0005489D
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<TypeWithAnnotations> TypeArgumentsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000566A5 File Offset: 0x000548A5
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public string Name
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x000566AD File Offset: 0x000548AD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> Methods
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x000566B5 File Offset: 0x000548B5
		public Symbol LookupSymbolOpt { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x000566BD File Offset: 0x000548BD
		public DiagnosticInfo LookupError { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x000566C5 File Offset: 0x000548C5
		public BoundMethodGroupFlags? Flags { get; }

		// Token: 0x06000D56 RID: 3414 RVA: 0x000566CD File Offset: 0x000548CD
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMethodGroup(this);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000566D8 File Offset: 0x000548D8
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundMethodGroup Update([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, [global::System.Runtime.CompilerServices.Nullable(1)] string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> methods, Symbol lookupSymbolOpt, DiagnosticInfo lookupError, BoundMethodGroupFlags? flags, BoundExpression receiverOpt, LookupResultKind resultKind)
		{
			if (!(typeArgumentsOpt != this.TypeArgumentsOpt) && !(name != this.Name) && !(methods != this.Methods) && SymbolEqualityComparer.ConsiderEverything.Equals(lookupSymbolOpt, this.LookupSymbolOpt) && lookupError == this.LookupError)
			{
				BoundMethodGroupFlags? boundMethodGroupFlags = flags;
				BoundMethodGroupFlags? flags2 = this.Flags;
				if (((boundMethodGroupFlags.GetValueOrDefault() == flags2.GetValueOrDefault()) & (boundMethodGroupFlags != null == (flags2 != null))) && receiverOpt == base.ReceiverOpt && resultKind == this.ResultKind)
				{
					return this;
				}
			}
			BoundMethodGroup boundMethodGroup = new BoundMethodGroup(this.Syntax, typeArgumentsOpt, name, methods, lookupSymbolOpt, lookupError, flags, receiverOpt, resultKind, base.HasErrors);
			boundMethodGroup.CopyAttributes(this);
			return boundMethodGroup;
		}
	}
}
