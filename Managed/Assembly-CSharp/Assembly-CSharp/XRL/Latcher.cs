using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace XRL
{
	// Token: 0x020004E7 RID: 1255
	public sealed class Latcher : IDisposable
	{
		// Token: 0x06003636 RID: 13878 RVA: 0x00104A70 File Offset: 0x00102C70
		public Latcher.WriteLock TakeWriteLock()
		{
			this.Write.Wait();
			try
			{
				this.Read.Wait();
			}
			catch (Exception ex)
			{
				this.Write.Release();
				ExceptionDispatchInfo.Throw(ex);
			}
			return new Latcher.WriteLock(this);
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x00104AC0 File Offset: 0x00102CC0
		public Latcher.ReadLock TakeReadLock()
		{
			this.Write.Wait();
			if (Interlocked.Increment(ref this.Readers) == 1)
			{
				try
				{
					this.Read.Wait();
				}
				catch (Exception ex)
				{
					Interlocked.Decrement(ref this.Readers);
					this.Write.Release();
					ExceptionDispatchInfo.Throw(ex);
				}
			}
			this.Write.Release();
			return new Latcher.ReadLock(this);
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x00104B34 File Offset: 0x00102D34
		public async Task<Latcher.WriteLock> TakeWriteLockAsync(CancellationToken Token = default(CancellationToken))
		{
			await this.Write.WaitAsync(Token).ConfigureAwait(false);
			try
			{
				await this.Read.WaitAsync(Token).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				this.Write.Release();
				ExceptionDispatchInfo.Throw(ex);
			}
			return new Latcher.WriteLock(this);
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x00104B80 File Offset: 0x00102D80
		public async Task<Latcher.ReadLock> TakeReadLockAsync(CancellationToken Token = default(CancellationToken))
		{
			await this.Write.WaitAsync(Token).ConfigureAwait(false);
			if (Interlocked.Increment(ref this.Readers) == 1)
			{
				try
				{
					await this.Read.WaitAsync(Token).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					Interlocked.Decrement(ref this.Readers);
					this.Write.Release();
					ExceptionDispatchInfo.Throw(ex);
				}
			}
			this.Write.Release();
			return new Latcher.ReadLock(this);
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x00104BCB File Offset: 0x00102DCB
		private void ReleaseWrite()
		{
			this.Read.Release();
			this.Write.Release();
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x00104BE5 File Offset: 0x00102DE5
		private void ReleaseRead()
		{
			if (Interlocked.Decrement(ref this.Readers) == 0)
			{
				this.Read.Release();
			}
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x00104C00 File Offset: 0x00102E00
		public void Dispose()
		{
			this.Write.Dispose();
			this.Read.Dispose();
		}

		// Token: 0x04001B2F RID: 6959
		private readonly SemaphoreSlim Read = new SemaphoreSlim(1, 1);

		// Token: 0x04001B30 RID: 6960
		private readonly SemaphoreSlim Write = new SemaphoreSlim(1, 1);

		// Token: 0x04001B31 RID: 6961
		private int Readers;

		// Token: 0x02001928 RID: 6440
		public struct Lock : IDisposable
		{
			// Token: 0x0600E27D RID: 57981 RVA: 0x00524C66 File Offset: 0x00522E66
			public Lock(Latcher Parent, bool Write)
			{
				this.Parent = Parent;
				this.Write = Write;
			}

			// Token: 0x0600E27E RID: 57982 RVA: 0x00524C76 File Offset: 0x00522E76
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Release()
			{
				if (this.Parent == null)
				{
					return;
				}
				if (this.Write)
				{
					this.Parent.ReleaseWrite();
				}
				else
				{
					this.Parent.ReleaseRead();
				}
				this.Parent = null;
			}

			// Token: 0x0600E27F RID: 57983 RVA: 0x00524CA8 File Offset: 0x00522EA8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IDisposable.Dispose()
			{
				this.Release();
			}

			// Token: 0x04006512 RID: 25874
			private Latcher Parent;

			// Token: 0x04006513 RID: 25875
			private readonly bool Write;
		}

		// Token: 0x02001929 RID: 6441
		public struct WriteLock : IDisposable
		{
			// Token: 0x0600E280 RID: 57984 RVA: 0x00524CB0 File Offset: 0x00522EB0
			public WriteLock(Latcher Parent)
			{
				this.Parent = Parent;
			}

			// Token: 0x0600E281 RID: 57985 RVA: 0x00524CB9 File Offset: 0x00522EB9
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Latcher.Lock Normalize()
			{
				return new Latcher.Lock(this.Parent, true);
			}

			// Token: 0x0600E282 RID: 57986 RVA: 0x00524CC7 File Offset: 0x00522EC7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Release()
			{
				if (this.Parent == null)
				{
					return;
				}
				this.Parent.ReleaseWrite();
				this.Parent = null;
			}

			// Token: 0x0600E283 RID: 57987 RVA: 0x00524CE4 File Offset: 0x00522EE4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IDisposable.Dispose()
			{
				this.Release();
			}

			// Token: 0x04006514 RID: 25876
			private Latcher Parent;
		}

		// Token: 0x0200192A RID: 6442
		public struct ReadLock : IDisposable
		{
			// Token: 0x0600E284 RID: 57988 RVA: 0x00524CEC File Offset: 0x00522EEC
			public ReadLock(Latcher Parent)
			{
				this.Parent = Parent;
			}

			// Token: 0x0600E285 RID: 57989 RVA: 0x00524CF5 File Offset: 0x00522EF5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public Latcher.Lock Normalize()
			{
				return new Latcher.Lock(this.Parent, false);
			}

			// Token: 0x0600E286 RID: 57990 RVA: 0x00524D03 File Offset: 0x00522F03
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Release()
			{
				if (this.Parent == null)
				{
					return;
				}
				this.Parent.ReleaseRead();
				this.Parent = null;
			}

			// Token: 0x0600E287 RID: 57991 RVA: 0x00524D20 File Offset: 0x00522F20
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IDisposable.Dispose()
			{
				this.Release();
			}

			// Token: 0x04006515 RID: 25877
			private Latcher Parent;
		}
	}
}
