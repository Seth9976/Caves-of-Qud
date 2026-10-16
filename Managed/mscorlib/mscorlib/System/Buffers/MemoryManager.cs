using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AD9 RID: 2777
	public abstract class MemoryManager<T> : IMemoryOwner<T>, IDisposable, IPinnable
	{
		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060062ED RID: 25325 RVA: 0x0014AC34 File Offset: 0x00148E34
		public virtual Memory<T> Memory
		{
			get
			{
				return new Memory<T>(this, this.GetSpan().Length);
			}
		}

		// Token: 0x060062EE RID: 25326
		public abstract Span<T> GetSpan();

		// Token: 0x060062EF RID: 25327
		public abstract MemoryHandle Pin(int elementIndex = 0);

		// Token: 0x060062F0 RID: 25328
		public abstract void Unpin();

		// Token: 0x060062F1 RID: 25329 RVA: 0x0014AC55 File Offset: 0x00148E55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int length)
		{
			return new Memory<T>(this, length);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x0014AC5E File Offset: 0x00148E5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected Memory<T> CreateMemory(int start, int length)
		{
			return new Memory<T>(this, start, length);
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x0014AC68 File Offset: 0x00148E68
		protected internal virtual bool TryGetArray(out ArraySegment<T> segment)
		{
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x0014AC72 File Offset: 0x00148E72
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060062F5 RID: 25333
		protected abstract void Dispose(bool disposing);
	}
}
