using System;
using System.Collections.Generic;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E3 RID: 227
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class VirtualHeap : CriticalDisposableObject
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x00017B3E File Offset: 0x00015D3E
		private VirtualHeap()
		{
			this._blobs = new Dictionary<uint, VirtualHeap.PinnedBlob>();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00017B54 File Offset: 0x00015D54
		protected override void Release()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Dictionary<uint, VirtualHeap.PinnedBlob> dictionary = Interlocked.Exchange<Dictionary<uint, VirtualHeap.PinnedBlob>>(ref this._blobs, null);
				if (dictionary != null)
				{
					foreach (KeyValuePair<uint, VirtualHeap.PinnedBlob> keyValuePair in dictionary)
					{
						keyValuePair.Value.Handle.Free();
					}
				}
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00017BD4 File Offset: 0x00015DD4
		private Dictionary<uint, VirtualHeap.PinnedBlob> GetBlobs()
		{
			Dictionary<uint, VirtualHeap.PinnedBlob> blobs = this._blobs;
			if (blobs == null)
			{
				throw new ObjectDisposedException("VirtualHeap");
			}
			return blobs;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public bool TryGetMemoryBlock(uint rawHandle, out MemoryBlock block)
		{
			VirtualHeap.PinnedBlob pinnedBlob;
			if (!this.GetBlobs().TryGetValue(rawHandle, ref pinnedBlob))
			{
				block = default(MemoryBlock);
				return false;
			}
			block = pinnedBlob.GetMemoryBlock();
			return true;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00017C2C File Offset: 0x00015E2C
		internal MemoryBlock AddBlob(uint rawHandle, byte[] value)
		{
			Dictionary<uint, VirtualHeap.PinnedBlob> blobs = this.GetBlobs();
			RuntimeHelpers.PrepareConstrainedRegions();
			MemoryBlock memoryBlock;
			try
			{
			}
			finally
			{
				VirtualHeap.PinnedBlob pinnedBlob = new VirtualHeap.PinnedBlob(GCHandle.Alloc(value, 3), value.Length);
				blobs.Add(rawHandle, pinnedBlob);
				memoryBlock = pinnedBlob.GetMemoryBlock();
			}
			return memoryBlock;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00017C7C File Offset: 0x00015E7C
		internal static VirtualHeap GetOrCreateVirtualHeap([Nullable(2)] ref VirtualHeap lazyHeap)
		{
			if (lazyHeap == null)
			{
				Interlocked.CompareExchange<VirtualHeap>(ref lazyHeap, new VirtualHeap(), null);
			}
			return lazyHeap;
		}

		// Token: 0x0400053E RID: 1342
		private Dictionary<uint, VirtualHeap.PinnedBlob> _blobs;

		// Token: 0x020001E8 RID: 488
		private struct PinnedBlob
		{
			// Token: 0x06000D14 RID: 3348 RVA: 0x00026549 File Offset: 0x00024749
			public PinnedBlob(GCHandle handle, int length)
			{
				this.Handle = handle;
				this.Length = length;
			}

			// Token: 0x06000D15 RID: 3349 RVA: 0x00026559 File Offset: 0x00024759
			public unsafe MemoryBlock GetMemoryBlock()
			{
				return new MemoryBlock((byte*)(void*)this.Handle.AddrOfPinnedObject(), this.Length);
			}

			// Token: 0x04000B28 RID: 2856
			public GCHandle Handle;

			// Token: 0x04000B29 RID: 2857
			public readonly int Length;
		}
	}
}
