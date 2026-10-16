using System;

namespace System.Buffers
{
	// Token: 0x02000AD1 RID: 2769
	public abstract class ArrayPool<T>
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0014A5CC File Offset: 0x001487CC
		public static ArrayPool<T> Shared { get; } = new TlsOverPerCoreLockedStacksArrayPool<T>();

		// Token: 0x060062D1 RID: 25297 RVA: 0x0014A5D3 File Offset: 0x001487D3
		public static ArrayPool<T> Create()
		{
			return new ConfigurableArrayPool<T>();
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0014A5DA File Offset: 0x001487DA
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new ConfigurableArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x060062D3 RID: 25299
		public abstract T[] Rent(int minimumLength);

		// Token: 0x060062D4 RID: 25300
		public abstract void Return(T[] array, bool clearArray = false);
	}
}
