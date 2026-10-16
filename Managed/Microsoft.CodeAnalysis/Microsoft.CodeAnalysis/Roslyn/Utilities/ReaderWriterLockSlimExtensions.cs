using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Roslyn.Utilities
{
	// Token: 0x02000058 RID: 88
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ReaderWriterLockSlimExtensions
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00006CE4 File Offset: 0x00004EE4
		internal static ReaderWriterLockSlimExtensions.ReadLockExiter DisposableRead(this ReaderWriterLockSlim @lock)
		{
			return new ReaderWriterLockSlimExtensions.ReadLockExiter(@lock);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006CEC File Offset: 0x00004EEC
		internal static ReaderWriterLockSlimExtensions.UpgradeableReadLockExiter DisposableUpgradeableRead(this ReaderWriterLockSlim @lock)
		{
			return new ReaderWriterLockSlimExtensions.UpgradeableReadLockExiter(@lock);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006CF4 File Offset: 0x00004EF4
		internal static ReaderWriterLockSlimExtensions.WriteLockExiter DisposableWrite(this ReaderWriterLockSlim @lock)
		{
			return new ReaderWriterLockSlimExtensions.WriteLockExiter(@lock);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006CFC File Offset: 0x00004EFC
		internal static void AssertCanRead(this ReaderWriterLockSlim @lock)
		{
			if (!@lock.IsReadLockHeld && !@lock.IsUpgradeableReadLockHeld && !@lock.IsWriteLockHeld)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006D1C File Offset: 0x00004F1C
		internal static void AssertCanWrite(this ReaderWriterLockSlim @lock)
		{
			if (!@lock.IsWriteLockHeld)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x02000504 RID: 1284
		[NullableContext(0)]
		[NonCopyable]
		internal readonly struct ReadLockExiter : IDisposable
		{
			// Token: 0x06003302 RID: 13058 RVA: 0x00090AF2 File Offset: 0x0008ECF2
			[NullableContext(1)]
			internal ReadLockExiter(ReaderWriterLockSlim @lock)
			{
				this._lock = @lock;
				@lock.EnterReadLock();
			}

			// Token: 0x06003303 RID: 13059 RVA: 0x00090B01 File Offset: 0x0008ED01
			public void Dispose()
			{
				this._lock.ExitReadLock();
			}

			// Token: 0x04001410 RID: 5136
			[Nullable(1)]
			private readonly ReaderWriterLockSlim _lock;
		}

		// Token: 0x02000505 RID: 1285
		[NullableContext(0)]
		[NonCopyable]
		internal readonly struct UpgradeableReadLockExiter : IDisposable
		{
			// Token: 0x06003304 RID: 13060 RVA: 0x00090B0E File Offset: 0x0008ED0E
			[NullableContext(1)]
			internal UpgradeableReadLockExiter(ReaderWriterLockSlim @lock)
			{
				this._lock = @lock;
				@lock.EnterUpgradeableReadLock();
			}

			// Token: 0x06003305 RID: 13061 RVA: 0x00090B1D File Offset: 0x0008ED1D
			public void Dispose()
			{
				if (this._lock.IsWriteLockHeld)
				{
					this._lock.ExitWriteLock();
				}
				this._lock.ExitUpgradeableReadLock();
			}

			// Token: 0x06003306 RID: 13062 RVA: 0x00090B42 File Offset: 0x0008ED42
			public void EnterWrite()
			{
				this._lock.EnterWriteLock();
			}

			// Token: 0x04001411 RID: 5137
			[Nullable(1)]
			private readonly ReaderWriterLockSlim _lock;
		}

		// Token: 0x02000506 RID: 1286
		[NullableContext(0)]
		[NonCopyable]
		internal readonly struct WriteLockExiter : IDisposable
		{
			// Token: 0x06003307 RID: 13063 RVA: 0x00090B4F File Offset: 0x0008ED4F
			[NullableContext(1)]
			internal WriteLockExiter(ReaderWriterLockSlim @lock)
			{
				this._lock = @lock;
				@lock.EnterWriteLock();
			}

			// Token: 0x06003308 RID: 13064 RVA: 0x00090B5E File Offset: 0x0008ED5E
			public void Dispose()
			{
				this._lock.ExitWriteLock();
			}

			// Token: 0x04001412 RID: 5138
			[Nullable(1)]
			private readonly ReaderWriterLockSlim _lock;
		}
	}
}
