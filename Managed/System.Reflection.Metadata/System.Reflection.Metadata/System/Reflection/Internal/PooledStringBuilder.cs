using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x02000186 RID: 390
	[NullableContext(1)]
	[Nullable(0)]
	internal class PooledStringBuilder
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x00024425 File Offset: 0x00022625
		private PooledStringBuilder(ObjectPool<PooledStringBuilder> pool)
		{
			this._pool = pool;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0002443F File Offset: 0x0002263F
		public int Length
		{
			get
			{
				return this.Builder.Length;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002444C File Offset: 0x0002264C
		public void Free()
		{
			StringBuilder builder = this.Builder;
			if (builder.Capacity <= 1024)
			{
				builder.Clear();
				this._pool.Free(this);
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00024480 File Offset: 0x00022680
		public string ToStringAndFree()
		{
			string text = this.Builder.ToString();
			this.Free();
			return text;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000244A0 File Offset: 0x000226A0
		public static ObjectPool<PooledStringBuilder> CreatePool()
		{
			ObjectPool<PooledStringBuilder> pool = null;
			pool = new ObjectPool<PooledStringBuilder>(() => new PooledStringBuilder(pool), 32);
			return pool;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000244DC File Offset: 0x000226DC
		public static PooledStringBuilder GetInstance()
		{
			return PooledStringBuilder.s_poolInstance.Allocate();
		}

		// Token: 0x04000969 RID: 2409
		public readonly StringBuilder Builder = new StringBuilder();

		// Token: 0x0400096A RID: 2410
		private readonly ObjectPool<PooledStringBuilder> _pool;

		// Token: 0x0400096B RID: 2411
		private static readonly ObjectPool<PooledStringBuilder> s_poolInstance = PooledStringBuilder.CreatePool();
	}
}
