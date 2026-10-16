using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016A RID: 362
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundObjectOrCollectionValuePlaceholder : BoundValuePlaceholderBase
	{
		// Token: 0x06001DC4 RID: 7620 RVA: 0x0009FC96 File Offset: 0x0009DE96
		public BoundObjectOrCollectionValuePlaceholder(SyntaxNode syntax, bool isNewInstance, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ObjectOrCollectionValuePlaceholder, syntax, type, hasErrors)
		{
			this.IsNewInstance = isNewInstance;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0009FCAA File Offset: 0x0009DEAA
		public BoundObjectOrCollectionValuePlaceholder(SyntaxNode syntax, bool isNewInstance, TypeSymbol type)
			: base(BoundKind.ObjectOrCollectionValuePlaceholder, syntax, type)
		{
			this.IsNewInstance = isNewInstance;
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0009FCBC File Offset: 0x0009DEBC
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0009FCC4 File Offset: 0x0009DEC4
		public bool IsNewInstance { get; }

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0009FCCC File Offset: 0x0009DECC
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitObjectOrCollectionValuePlaceholder(this);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0009FCD5 File Offset: 0x0009DED5
		public BoundObjectOrCollectionValuePlaceholder Update(bool isNewInstance, TypeSymbol type)
		{
			if (isNewInstance != this.IsNewInstance || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundObjectOrCollectionValuePlaceholder boundObjectOrCollectionValuePlaceholder = new BoundObjectOrCollectionValuePlaceholder(this.Syntax, isNewInstance, type, base.HasErrors);
				boundObjectOrCollectionValuePlaceholder.CopyAttributes(this);
				return boundObjectOrCollectionValuePlaceholder;
			}
			return this;
		}
	}
}
