using System;
using System.Diagnostics;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200010E RID: 270
	internal sealed class PlayerLoopRunner
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		public PlayerLoopRunner(PlayerLoopTiming timing)
		{
			this.unhandledExceptionCallback = delegate(Exception ex)
			{
				global::UnityEngine.Debug.LogException(ex);
			};
			this.timing = timing;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000E36C File Offset: 0x0000C56C
		public void AddAction(IPlayerLoopItem item)
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				if (this.running)
				{
					this.waitQueue.Enqueue(item);
					return;
				}
			}
			obj = this.arrayLock;
			lock (obj)
			{
				if (this.loopItems.Length == this.tail)
				{
					Array.Resize<IPlayerLoopItem>(ref this.loopItems, checked(this.tail * 2));
				}
				IPlayerLoopItem[] array = this.loopItems;
				int num = this.tail;
				this.tail = num + 1;
				array[num] = item;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000E424 File Offset: 0x0000C624
		public int Clear()
		{
			object obj = this.arrayLock;
			int num2;
			lock (obj)
			{
				int num = 0;
				for (int i = 0; i < this.loopItems.Length; i++)
				{
					if (this.loopItems[i] != null)
					{
						num++;
					}
					this.loopItems[i] = null;
				}
				this.tail = 0;
				num2 = num;
			}
			return num2;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000E498 File Offset: 0x0000C698
		public void Run()
		{
			this.RunCore();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		private void Initialization()
		{
			this.RunCore();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		private void LastInitialization()
		{
			this.RunCore();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		private void EarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		private void LastEarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		private void FixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		private void LastFixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		private void PreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		private void LastPreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		private void Update()
		{
			this.RunCore();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		private void LastUpdate()
		{
			this.RunCore();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		private void PreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		private void LastPreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000E500 File Offset: 0x0000C700
		private void PostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000E508 File Offset: 0x0000C708
		private void LastPostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000E510 File Offset: 0x0000C710
		private void TimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000E518 File Offset: 0x0000C718
		private void LastTimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000E520 File Offset: 0x0000C720
		[DebuggerHidden]
		private void RunCore()
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				this.running = true;
			}
			obj = this.arrayLock;
			lock (obj)
			{
				int num = this.tail - 1;
				int i = 0;
				while (i < this.loopItems.Length)
				{
					IPlayerLoopItem playerLoopItem = this.loopItems[i];
					if (playerLoopItem != null)
					{
						try
						{
							if (!playerLoopItem.MoveNext())
							{
								this.loopItems[i] = null;
								goto IL_00F9;
							}
							goto IL_0106;
						}
						catch (Exception ex)
						{
							this.loopItems[i] = null;
							try
							{
								this.unhandledExceptionCallback(ex);
							}
							catch
							{
							}
							goto IL_00F9;
						}
						goto IL_0093;
					}
					goto IL_00F9;
					IL_0106:
					i++;
					continue;
					IL_0093:
					IPlayerLoopItem playerLoopItem2 = this.loopItems[num];
					if (playerLoopItem2 != null)
					{
						try
						{
							if (!playerLoopItem2.MoveNext())
							{
								this.loopItems[num] = null;
								num--;
								goto IL_00F9;
							}
							this.loopItems[i] = playerLoopItem2;
							this.loopItems[num] = null;
							num--;
							goto IL_0106;
						}
						catch (Exception ex2)
						{
							this.loopItems[num] = null;
							num--;
							try
							{
								this.unhandledExceptionCallback(ex2);
							}
							catch
							{
							}
							goto IL_00F9;
						}
					}
					num--;
					IL_00F9:
					if (i >= num)
					{
						this.tail = i;
						break;
					}
					goto IL_0093;
				}
				object obj2 = this.runningAndQueueLock;
				lock (obj2)
				{
					this.running = false;
					while (this.waitQueue.Count != 0)
					{
						if (this.loopItems.Length == this.tail)
						{
							Array.Resize<IPlayerLoopItem>(ref this.loopItems, checked(this.tail * 2));
						}
						IPlayerLoopItem[] array = this.loopItems;
						int num2 = this.tail;
						this.tail = num2 + 1;
						array[num2] = this.waitQueue.Dequeue();
					}
				}
			}
		}

		// Token: 0x04000113 RID: 275
		private const int InitialSize = 16;

		// Token: 0x04000114 RID: 276
		private readonly PlayerLoopTiming timing;

		// Token: 0x04000115 RID: 277
		private readonly object runningAndQueueLock = new object();

		// Token: 0x04000116 RID: 278
		private readonly object arrayLock = new object();

		// Token: 0x04000117 RID: 279
		private readonly Action<Exception> unhandledExceptionCallback;

		// Token: 0x04000118 RID: 280
		private int tail;

		// Token: 0x04000119 RID: 281
		private bool running;

		// Token: 0x0400011A RID: 282
		private IPlayerLoopItem[] loopItems = new IPlayerLoopItem[16];

		// Token: 0x0400011B RID: 283
		private MinimumQueue<IPlayerLoopItem> waitQueue = new MinimumQueue<IPlayerLoopItem>(16);
	}
}
