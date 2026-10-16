using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E6 RID: 1254
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class TokenMap
	{
		// Token: 0x06003282 RID: 12930 RVA: 0x0008FB4F File Offset: 0x0008DD4F
		internal TokenMap()
		{
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x0008FB70 File Offset: 0x0008DD70
		public uint GetOrAddTokenFor(IReference item, out bool referenceAdded)
		{
			uint num;
			if (this._itemIdentityToToken.TryGetValue(new IReferenceOrISignature(item), ref num))
			{
				referenceAdded = false;
				return num;
			}
			return this.AddItem(new IReferenceOrISignature(item), out referenceAdded);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0008FBA4 File Offset: 0x0008DDA4
		public uint GetOrAddTokenFor(ISignature item, out bool referenceAdded)
		{
			uint num;
			if (this._itemIdentityToToken.TryGetValue(new IReferenceOrISignature(item), ref num))
			{
				referenceAdded = false;
				return num;
			}
			return this.AddItem(new IReferenceOrISignature(item), out referenceAdded);
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x0008FBD8 File Offset: 0x0008DDD8
		private uint AddItem(IReferenceOrISignature item, out bool referenceAdded)
		{
			ConcurrentDictionary<IReferenceOrISignature, uint> itemIdentityToToken = this._itemIdentityToToken;
			uint count;
			lock (itemIdentityToToken)
			{
				if (!this._itemIdentityToToken.TryGetValue(item, ref count))
				{
					count = (uint)this._count;
					referenceAdded = this._itemIdentityToToken.TryAdd(item, count);
					int num = (int)(count + 1U);
					object[] items = this._items;
					if (items.Length > num)
					{
						items[(int)count] = item.AsObject();
					}
					else
					{
						Array.Resize<object>(ref items, Math.Max(8, num * 2));
						items[(int)count] = item.AsObject();
						Volatile.Write<object[]>(ref this._items, items);
					}
					Volatile.Write(ref this._count, num);
				}
				else
				{
					referenceAdded = false;
				}
			}
			return count;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x0008FC94 File Offset: 0x0008DE94
		public object GetItem(uint token)
		{
			return this._items[(int)token];
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x0008FCA0 File Offset: 0x0008DEA0
		[return: Nullable(new byte[] { 0, 1 })]
		public ReadOnlySpan<object> GetAllItems()
		{
			int num = Volatile.Read(ref this._count);
			return new ReadOnlySpan<object>(Volatile.Read<object[]>(ref this._items), 0, num);
		}

		// Token: 0x0400137C RID: 4988
		private readonly ConcurrentDictionary<IReferenceOrISignature, uint> _itemIdentityToToken = new ConcurrentDictionary<IReferenceOrISignature, uint>();

		// Token: 0x0400137D RID: 4989
		private object[] _items = Array.Empty<object>();

		// Token: 0x0400137E RID: 4990
		private int _count;
	}
}
