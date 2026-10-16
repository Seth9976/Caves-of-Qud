using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B8 RID: 440
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Metadata : IDisposable
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x000393D8 File Offset: 0x000375D8
		public MetadataId Id { get; }

		// Token: 0x0600127A RID: 4730 RVA: 0x000393E0 File Offset: 0x000375E0
		internal Metadata(bool isImageOwner, MetadataId id)
		{
			this.IsImageOwner = isImageOwner;
			this.Id = id;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600127B RID: 4731
		public abstract MetadataImageKind Kind { get; }

		// Token: 0x0600127C RID: 4732
		public abstract void Dispose();

		// Token: 0x0600127D RID: 4733
		protected abstract Metadata CommonCopy();

		// Token: 0x0600127E RID: 4734 RVA: 0x000393F6 File Offset: 0x000375F6
		public Metadata Copy()
		{
			return this.CommonCopy();
		}

		// Token: 0x0400058D RID: 1421
		internal readonly bool IsImageOwner;
	}
}
