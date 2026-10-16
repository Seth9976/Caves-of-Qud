using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029F RID: 671
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PooledStringBuilder
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x00053B53 File Offset: 0x00051D53
		private PooledStringBuilder(ObjectPool<PooledStringBuilder> pool)
		{
			this._pool = pool;
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00053B6D File Offset: 0x00051D6D
		public int Length
		{
			get
			{
				return this.Builder.Length;
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00053B7C File Offset: 0x00051D7C
		public void Free()
		{
			StringBuilder builder = this.Builder;
			if (builder.Capacity <= 1024)
			{
				builder.Clear();
				this._pool.Free(this);
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00053BB0 File Offset: 0x00051DB0
		[Obsolete("Consider calling ToStringAndFree instead.")]
		public string ToString()
		{
			return this.Builder.ToString();
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00053BBD File Offset: 0x00051DBD
		public string ToStringAndFree()
		{
			string text = this.Builder.ToString();
			this.Free();
			return text;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x00053BD0 File Offset: 0x00051DD0
		public string ToStringAndFree(int startIndex, int length)
		{
			string text = this.Builder.ToString(startIndex, length);
			this.Free();
			return text;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00053BE5 File Offset: 0x00051DE5
		public static ObjectPool<PooledStringBuilder> CreatePool(int size = 32)
		{
			ObjectPool<PooledStringBuilder> pool = null;
			pool = new ObjectPool<PooledStringBuilder>(() => new PooledStringBuilder(pool), size);
			return pool;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00053C10 File Offset: 0x00051E10
		public static PooledStringBuilder GetInstance()
		{
			return PooledStringBuilder.s_poolInstance.Allocate();
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x00053C1C File Offset: 0x00051E1C
		public static implicit operator StringBuilder(PooledStringBuilder obj)
		{
			return obj.Builder;
		}

		// Token: 0x04000D50 RID: 3408
		public readonly StringBuilder Builder = new StringBuilder();

		// Token: 0x04000D51 RID: 3409
		private readonly ObjectPool<PooledStringBuilder> _pool;

		// Token: 0x04000D52 RID: 3410
		private static readonly ObjectPool<PooledStringBuilder> s_poolInstance = PooledStringBuilder.CreatePool(32);
	}
}
