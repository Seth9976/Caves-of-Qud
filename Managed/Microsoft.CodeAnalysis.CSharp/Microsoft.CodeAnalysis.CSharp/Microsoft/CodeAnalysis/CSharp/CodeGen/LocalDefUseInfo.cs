using System;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F6 RID: 1526
	internal class LocalDefUseInfo
	{
		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x060080EC RID: 33004 RVA: 0x001D5384 File Offset: 0x001D3584
		// (set) Token: 0x060080ED RID: 33005 RVA: 0x001D538C File Offset: 0x001D358C
		public int StackAtDeclaration { get; private set; }

		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x060080EE RID: 33006 RVA: 0x001D5398 File Offset: 0x001D3598
		public ArrayBuilder<LocalDefUseSpan> LocalDefs
		{
			get
			{
				ArrayBuilder<LocalDefUseSpan> arrayBuilder = this._localDefs;
				if (arrayBuilder == null)
				{
					arrayBuilder = (this._localDefs = ArrayBuilder<LocalDefUseSpan>.GetInstance());
				}
				return arrayBuilder;
			}
		}

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x060080EF RID: 33007 RVA: 0x001D53BD File Offset: 0x001D35BD
		// (set) Token: 0x060080F0 RID: 33008 RVA: 0x001D53C5 File Offset: 0x001D35C5
		public bool CannotSchedule { get; private set; }

		// Token: 0x060080F1 RID: 33009 RVA: 0x001D53CE File Offset: 0x001D35CE
		public void ShouldNotSchedule()
		{
			this.CannotSchedule = true;
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x001D53D7 File Offset: 0x001D35D7
		private LocalDefUseInfo(ObjectPool<LocalDefUseInfo> pool)
		{
			this._pool = pool;
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x001D53E6 File Offset: 0x001D35E6
		public void Free()
		{
			if (this._localDefs != null)
			{
				this._localDefs.Free();
				this._localDefs = null;
			}
			ObjectPool<LocalDefUseInfo> pool = this._pool;
			if (pool == null)
			{
				return;
			}
			pool.Free(this);
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x001D5413 File Offset: 0x001D3613
		public static ObjectPool<LocalDefUseInfo> CreatePool()
		{
			ObjectPool<LocalDefUseInfo> pool = null;
			pool = new ObjectPool<LocalDefUseInfo>(() => new LocalDefUseInfo(pool), 128);
			return pool;
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x001D5442 File Offset: 0x001D3642
		public static LocalDefUseInfo GetInstance(int stackAtDeclaration)
		{
			LocalDefUseInfo localDefUseInfo = LocalDefUseInfo.s_poolInstance.Allocate();
			localDefUseInfo.StackAtDeclaration = stackAtDeclaration;
			localDefUseInfo.CannotSchedule = false;
			return localDefUseInfo;
		}

		// Token: 0x04001E47 RID: 7751
		private readonly ObjectPool<LocalDefUseInfo> _pool;

		// Token: 0x04001E48 RID: 7752
		private static readonly ObjectPool<LocalDefUseInfo> s_poolInstance = LocalDefUseInfo.CreatePool();

		// Token: 0x04001E49 RID: 7753
		private ArrayBuilder<LocalDefUseSpan> _localDefs;
	}
}
