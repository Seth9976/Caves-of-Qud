using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000047 RID: 71
	internal class SecurePooledObject<[Nullable(2)] T>
	{
		// Token: 0x06000377 RID: 887 RVA: 0x000092EF File Offset: 0x000074EF
		[NullableContext(1)]
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00009309 File Offset: 0x00007509
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00009311 File Offset: 0x00007511
		internal int Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000931A File Offset: 0x0000751A
		[return: Nullable(1)]
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00009336 File Offset: 0x00007536
		internal bool TryUse<TCaller>(ref TCaller caller, [Nullable(1)] [MaybeNullWhen(false)] out T value) where TCaller : struct, ISecurePooledObjectUser
		{
			if (this.IsOwned<TCaller>(ref caller))
			{
				value = this._value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00009357 File Offset: 0x00007557
		[MethodImpl(256)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x04000042 RID: 66
		private readonly T _value;

		// Token: 0x04000043 RID: 67
		private int _owner;
	}
}
