using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E4 RID: 228
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x0000872E File Offset: 0x0000692E
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.CollectionAccessType = collectionAccessType;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000873F File Offset: 0x0000693F
		public CollectionAccessType CollectionAccessType { get; }
	}
}
