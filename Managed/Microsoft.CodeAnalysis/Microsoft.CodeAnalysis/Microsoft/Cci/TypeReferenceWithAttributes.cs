using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Microsoft.Cci
{
	// Token: 0x020000DF RID: 223
	[NullableContext(1)]
	[Nullable(0)]
	internal struct TypeReferenceWithAttributes
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00018056 File Offset: 0x00016256
		public readonly ITypeReference TypeRef { get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001805E File Offset: 0x0001625E
		[Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<ICustomAttribute> Attributes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00018066 File Offset: 0x00016266
		public TypeReferenceWithAttributes(ITypeReference typeRef, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ICustomAttribute> attributes = default(ImmutableArray<ICustomAttribute>))
		{
			this.TypeRef = typeRef;
			this.Attributes = attributes.NullToEmpty<ICustomAttribute>();
		}
	}
}
