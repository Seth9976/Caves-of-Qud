using System;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D2 RID: 1234
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MetadataNamedArgument : IMetadataNamedArgument, IMetadataExpression
	{
		// Token: 0x0600319B RID: 12699 RVA: 0x0008DE82 File Offset: 0x0008C082
		public MetadataNamedArgument(ISymbolInternal entity, ITypeReference type, IMetadataExpression value)
		{
			this._entity = entity;
			this._type = type;
			this._value = value;
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x0008DE9F File Offset: 0x0008C09F
		string IMetadataNamedArgument.ArgumentName
		{
			get
			{
				return this._entity.Name;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x0008DEAC File Offset: 0x0008C0AC
		IMetadataExpression IMetadataNamedArgument.ArgumentValue
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x0008DEB4 File Offset: 0x0008C0B4
		bool IMetadataNamedArgument.IsField
		{
			get
			{
				return this._entity.Kind == SymbolKind.Field;
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0008DEC4 File Offset: 0x0008C0C4
		void IMetadataExpression.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x0008DECD File Offset: 0x0008C0CD
		ITypeReference IMetadataExpression.Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04001323 RID: 4899
		private readonly ISymbolInternal _entity;

		// Token: 0x04001324 RID: 4900
		private readonly ITypeReference _type;

		// Token: 0x04001325 RID: 4901
		private readonly IMetadataExpression _value;
	}
}
