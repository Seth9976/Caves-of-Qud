using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009D RID: 157
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConversion : BoundExpression
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0005432B File Offset: 0x0005252B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00054334 File Offset: 0x00052534
		public ConversionKind ConversionKind
		{
			get
			{
				return this.Conversion.Kind;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00054350 File Offset: 0x00052550
		public bool IsExtensionMethod
		{
			get
			{
				return this.Conversion.IsExtensionMethod;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0005436C File Offset: 0x0005256C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol SymbolOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Conversion.Method;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00054387 File Offset: 0x00052587
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.SymbolOpt;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0005438F File Offset: 0x0005258F
		public override bool SuppressVirtualCalls
		{
			get
			{
				return this.IsBaseConversion;
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00054398 File Offset: 0x00052598
		public BoundConversion UpdateOperand(BoundExpression operand)
		{
			return this.Update(operand, this.Conversion, this.IsBaseConversion, this.Checked, this.ExplicitCastInCode, this.ConstantValue, this.ConversionGroupOpt, this.OriginalUserDefinedConversionsOpt, this.Type);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000543DC File Offset: 0x000525DC
		internal bool ConversionHasSideEffects()
		{
			ConversionKind conversionKind = this.ConversionKind;
			return conversionKind - ConversionKind.Identity > 2 && conversionKind - ConversionKind.ImplicitReference > 1 && (conversionKind != ConversionKind.ExplicitNumeric || this.Checked);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00054410 File Offset: 0x00052610
		public static BoundConversion SynthesizedNonUserDefined(SyntaxNode syntax, BoundExpression operand, Conversion conversion, TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt = null)
		{
			return new BoundConversion(syntax, operand, conversion, false, false, false, constantValueOpt, null, default(ImmutableArray<MethodSymbol>), type, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00054440 File Offset: 0x00052640
		public static BoundConversion Synthesized(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
		{
			return new BoundConversion(syntax, operand, conversion, @checked, explicitCastInCode, conversionGroupOpt, constantValueOpt, type, hasErrors || !conversion.IsValid)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00054478 File Offset: 0x00052678
		public BoundConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operand, conversion, false, @checked, explicitCastInCode, constantValueOpt, conversionGroupOpt, conversion.OriginalUserDefinedConversions, type, hasErrors || !conversion.IsValid)
		{
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000544B4 File Offset: 0x000526B4
		public BoundConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool isBaseConversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operand, conversion, isBaseConversion, @checked, explicitCastInCode, constantValueOpt, conversionGroupOpt, default(ImmutableArray<MethodSymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000544E4 File Offset: 0x000526E4
		public BoundConversion Update(BoundExpression operand, Conversion conversion, bool isBaseConversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, TypeSymbol type)
		{
			return this.Update(operand, conversion, isBaseConversion, @checked, explicitCastInCode, constantValueOpt, conversionGroupOpt, this.OriginalUserDefinedConversionsOpt, type);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0005450C File Offset: 0x0005270C
		public BoundConversion(SyntaxNode syntax, BoundExpression operand, Conversion conversion, bool isBaseConversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedConversionsOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.Conversion, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
			this.Conversion = conversion;
			this.IsBaseConversion = isBaseConversion;
			this.Checked = @checked;
			this.ExplicitCastInCode = explicitCastInCode;
			this.ConstantValueOpt = constantValueOpt;
			this.ConversionGroupOpt = conversionGroupOpt;
			this.OriginalUserDefinedConversionsOpt = originalUserDefinedConversionsOpt;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0005456F File Offset: 0x0005276F
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00054577 File Offset: 0x00052777
		public BoundExpression Operand { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0005457F File Offset: 0x0005277F
		public Conversion Conversion { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00054587 File Offset: 0x00052787
		public bool IsBaseConversion { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0005458F File Offset: 0x0005278F
		public bool Checked { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00054597 File Offset: 0x00052797
		public bool ExplicitCastInCode { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0005459F File Offset: 0x0005279F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x000545A7 File Offset: 0x000527A7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConversionGroup ConversionGroupOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000545AF File Offset: 0x000527AF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedConversionsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000545B7 File Offset: 0x000527B7
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConversion(this);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000545C0 File Offset: 0x000527C0
		public BoundConversion Update(BoundExpression operand, Conversion conversion, bool isBaseConversion, bool @checked, bool explicitCastInCode, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] ConversionGroup conversionGroupOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedConversionsOpt, TypeSymbol type)
		{
			if (operand != this.Operand || conversion != this.Conversion || isBaseConversion != this.IsBaseConversion || @checked != this.Checked || explicitCastInCode != this.ExplicitCastInCode || constantValueOpt != this.ConstantValueOpt || conversionGroupOpt != this.ConversionGroupOpt || originalUserDefinedConversionsOpt != this.OriginalUserDefinedConversionsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConversion boundConversion = new BoundConversion(this.Syntax, operand, conversion, isBaseConversion, @checked, explicitCastInCode, constantValueOpt, conversionGroupOpt, originalUserDefinedConversionsOpt, type, base.HasErrors);
				boundConversion.CopyAttributes(this);
				return boundConversion;
			}
			return this;
		}
	}
}
