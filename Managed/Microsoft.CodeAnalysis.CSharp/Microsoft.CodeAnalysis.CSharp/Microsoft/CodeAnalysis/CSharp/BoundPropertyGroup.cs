using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E6 RID: 230
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPropertyGroup : BoundMethodOrPropertyGroup
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0005F09B File Offset: 0x0005D29B
		public override object Display
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0005F0A2 File Offset: 0x0005D2A2
		public BoundPropertyGroup(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> properties, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, LookupResultKind resultKind, bool hasErrors = false)
			: base(BoundKind.PropertyGroup, syntax, receiverOpt, resultKind, hasErrors || receiverOpt.HasErrors())
		{
			this.Properties = properties;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0005F0C7 File Offset: 0x0005D2C7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<PropertySymbol> Properties
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0005F0CF File Offset: 0x0005D2CF
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPropertyGroup(this);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0005F0D8 File Offset: 0x0005D2D8
		public BoundPropertyGroup Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> properties, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, LookupResultKind resultKind)
		{
			if (properties != this.Properties || receiverOpt != base.ReceiverOpt || resultKind != this.ResultKind)
			{
				BoundPropertyGroup boundPropertyGroup = new BoundPropertyGroup(this.Syntax, properties, receiverOpt, resultKind, base.HasErrors);
				boundPropertyGroup.CopyAttributes(this);
				return boundPropertyGroup;
			}
			return this;
		}
	}
}
