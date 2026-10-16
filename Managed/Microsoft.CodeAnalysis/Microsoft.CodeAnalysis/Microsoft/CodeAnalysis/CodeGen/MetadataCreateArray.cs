using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D1 RID: 1233
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MetadataCreateArray : IMetadataExpression
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x0008DE3C File Offset: 0x0008C03C
		public IArrayTypeReference ArrayType { get; }

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003196 RID: 12694 RVA: 0x0008DE44 File Offset: 0x0008C044
		public ITypeReference ElementType { get; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x0008DE4C File Offset: 0x0008C04C
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IMetadataExpression> Elements
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0008DE54 File Offset: 0x0008C054
		public MetadataCreateArray(IArrayTypeReference arrayType, ITypeReference elementType, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IMetadataExpression> initializers)
		{
			this.ArrayType = arrayType;
			this.ElementType = elementType;
			this.Elements = initializers;
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x0008DE71 File Offset: 0x0008C071
		ITypeReference IMetadataExpression.Type
		{
			get
			{
				return this.ArrayType;
			}
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x0008DE79 File Offset: 0x0008C079
		void IMetadataExpression.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
