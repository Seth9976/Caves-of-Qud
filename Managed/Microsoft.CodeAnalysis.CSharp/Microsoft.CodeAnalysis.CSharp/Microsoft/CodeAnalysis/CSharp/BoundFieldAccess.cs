using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000090 RID: 144
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFieldAccess : BoundExpression
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000531D2 File Offset: 0x000513D2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000531DA File Offset: 0x000513DA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.FieldSymbol;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000531E2 File Offset: 0x000513E2
		public BoundFieldAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, bool hasErrors = false)
			: this(syntax, receiver, fieldSymbol, constantValueOpt, LookupResultKind.Viable, fieldSymbol.Type, hasErrors)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000531FC File Offset: 0x000513FC
		public BoundFieldAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: this(syntax, receiver, fieldSymbol, constantValueOpt, resultKind, BoundFieldAccess.NeedsByValueFieldAccess(receiver, fieldSymbol), false, type, hasErrors)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00053224 File Offset: 0x00051424
		public BoundFieldAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, bool isDeclaration, TypeSymbol type, bool hasErrors = false)
			: this(syntax, receiver, fieldSymbol, constantValueOpt, resultKind, BoundFieldAccess.NeedsByValueFieldAccess(receiver, fieldSymbol), isDeclaration, type, hasErrors)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0005324B File Offset: 0x0005144B
		public BoundFieldAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, TypeSymbol typeSymbol)
		{
			return this.Update(receiver, fieldSymbol, constantValueOpt, resultKind, this.IsByValue, this.IsDeclaration, typeSymbol);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00053268 File Offset: 0x00051468
		private static bool NeedsByValueFieldAccess([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiver, FieldSymbol fieldSymbol)
		{
			if (fieldSymbol.IsStatic || !fieldSymbol.ContainingType.IsValueType || receiver == null)
			{
				return false;
			}
			BoundKind kind = receiver.Kind;
			if (kind != BoundKind.Local)
			{
				return kind == BoundKind.FieldAccess && ((BoundFieldAccess)receiver).IsByValue;
			}
			LocalSymbol localSymbol = ((BoundLocal)receiver).LocalSymbol;
			return !localSymbol.IsWritableVariable && !localSymbol.IsRef;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000532D0 File Offset: 0x000514D0
		public BoundFieldAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, bool isByValue, bool isDeclaration, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.FieldAccess, syntax, type, hasErrors || receiverOpt.HasErrors())
		{
			this.ReceiverOpt = receiverOpt;
			this.FieldSymbol = fieldSymbol;
			this.ConstantValueOpt = constantValueOpt;
			this._ResultKind = resultKind;
			this.IsByValue = isByValue;
			this.IsDeclaration = isDeclaration;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00053326 File Offset: 0x00051526
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0005332E File Offset: 0x0005152E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00053336 File Offset: 0x00051536
		public FieldSymbol FieldSymbol { get; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0005333E File Offset: 0x0005153E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00053346 File Offset: 0x00051546
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0005334E File Offset: 0x0005154E
		public bool IsByValue { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00053356 File Offset: 0x00051556
		public bool IsDeclaration { get; }

		// Token: 0x06000BBE RID: 3006 RVA: 0x0005335E File Offset: 0x0005155E
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFieldAccess(this);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00053368 File Offset: 0x00051568
		public BoundFieldAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, FieldSymbol fieldSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, LookupResultKind resultKind, bool isByValue, bool isDeclaration, TypeSymbol type)
		{
			if (receiverOpt != this.ReceiverOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(fieldSymbol, this.FieldSymbol) || constantValueOpt != this.ConstantValueOpt || resultKind != this.ResultKind || isByValue != this.IsByValue || isDeclaration != this.IsDeclaration || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFieldAccess boundFieldAccess = new BoundFieldAccess(this.Syntax, receiverOpt, fieldSymbol, constantValueOpt, resultKind, isByValue, isDeclaration, type, base.HasErrors);
				boundFieldAccess.CopyAttributes(this);
				return boundFieldAccess;
			}
			return this;
		}

		// Token: 0x040004AC RID: 1196
		private readonly LookupResultKind _ResultKind;
	}
}
