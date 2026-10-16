using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044D RID: 1101
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerParameterSyntax : BaseParameterSyntax
	{
		// Token: 0x06005D58 RID: 23896 RVA: 0x0016BF00 File Offset: 0x0016A100
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal FunctionPointerParameterSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0016BF54 File Offset: 0x0016A154
		internal FunctionPointerParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x0016BFAC File Offset: 0x0016A1AC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal FunctionPointerParameterSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x0016BFF9 File Offset: 0x0016A1F9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06005D5C RID: 23900 RVA: 0x0016C006 File Offset: 0x0016A206
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x0016C013 File Offset: 0x0016A213
		public override TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0016C01C File Offset: 0x0016A21C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.type;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0016C05B File Offset: 0x0016A25B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerParameterSyntax(this, parent, position);
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0016C065 File Offset: 0x0016A265
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerParameter(this);
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0016C06E File Offset: 0x0016A26E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerParameter(this);
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0016C078 File Offset: 0x0016A278
		public FunctionPointerParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || type != this.Type)
			{
				FunctionPointerParameterSyntax functionPointerParameterSyntax = SyntaxFactory.FunctionPointerParameter(attributeLists, modifiers, type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					functionPointerParameterSyntax = functionPointerParameterSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					functionPointerParameterSyntax = functionPointerParameterSyntax.WithAnnotationsGreen(annotations);
				}
				return functionPointerParameterSyntax;
			}
			return this;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0016C0E2 File Offset: 0x0016A2E2
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerParameterSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0016C108 File Offset: 0x0016A308
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerParameterSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0016C130 File Offset: 0x0016A330
		internal FunctionPointerParameterSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0016C19F File Offset: 0x0016A39F
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0016C1CC File Offset: 0x0016A3CC
		static FunctionPointerParameterSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerParameterSyntax), (ObjectReader r) => new FunctionPointerParameterSyntax(r));
		}

		// Token: 0x040018A0 RID: 6304
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040018A1 RID: 6305
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040018A2 RID: 6306
		internal readonly TypeSyntax type;
	}
}
