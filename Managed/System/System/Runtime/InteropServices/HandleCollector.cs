using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Tracks outstanding handles and forces a garbage collection when the specified threshold is reached.</summary>
	// Token: 0x02000188 RID: 392
	public sealed class HandleCollector
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name and a threshold at which to begin handle collection. </summary>
		/// <param name="name">A name for the collector. This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.</exception>
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002D4D4 File Offset: 0x0002B6D4
		public HandleCollector(string name, int initialThreshold)
			: this(name, initialThreshold, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleCollector" /> class using a name, a threshold at which to begin handle collection, and a threshold at which handle collection must occur. </summary>
		/// <param name="name">A name for the collector.  This parameter allows you to name collectors that track handle types separately.</param>
		/// <param name="initialThreshold">A value that specifies the point at which collections should begin.</param>
		/// <param name="maximumThreshold">A value that specifies the point at which collections must occur. This should be set to the maximum number of available handles.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="initialThreshold" /> parameter is less than 0.-or-The <paramref name="maximumThreshold" /> parameter is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="maximumThreshold" /> parameter is less than the <paramref name="initialThreshold" /> parameter.</exception>
		// Token: 0x06000A70 RID: 2672 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		public HandleCollector(string name, int initialThreshold, int maximumThreshold)
		{
			if (initialThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("initialThreshold", SR.GetString("Non-negative number required."));
			}
			if (maximumThreshold < 0)
			{
				throw new ArgumentOutOfRangeException("maximumThreshold", SR.GetString("Non-negative number required."));
			}
			if (initialThreshold > maximumThreshold)
			{
				throw new ArgumentException(SR.GetString("maximumThreshold cannot be less than initialThreshold."));
			}
			if (name != null)
			{
				this.name = name;
			}
			else
			{
				this.name = string.Empty;
			}
			this.initialThreshold = initialThreshold;
			this.maximumThreshold = maximumThreshold;
			this.threshold = initialThreshold;
			this.handleCount = 0;
		}

		/// <summary>Gets the number of handles collected.</summary>
		/// <returns>The number of handles collected.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0002D57C File Offset: 0x0002B77C
		public int Count
		{
			get
			{
				return this.handleCount;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections should begin.</summary>
		/// <returns>A value that specifies the point at which collections should begin.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002D584 File Offset: 0x0002B784
		public int InitialThreshold
		{
			get
			{
				return this.initialThreshold;
			}
		}

		/// <summary>Gets a value that specifies the point at which collections must occur.</summary>
		/// <returns>A value that specifies the point at which collections must occur.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002D58C File Offset: 0x0002B78C
		public int MaximumThreshold
		{
			get
			{
				return this.maximumThreshold;
			}
		}

		/// <summary>Gets the name of a <see cref="T:System.Runtime.InteropServices.HandleCollector" /> object.</summary>
		/// <returns>This <see cref="P:System.Runtime.InteropServices.HandleCollector.Name" /> property allows you to name collectors that track handle types separately.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002D594 File Offset: 0x0002B794
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Increments the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06000A75 RID: 2677 RVA: 0x0002D59C File Offset: 0x0002B79C
		public void Add()
		{
			int num = -1;
			Interlocked.Increment(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("Handle collector count overflows or underflows."));
			}
			if (this.handleCount > this.threshold)
			{
				lock (this)
				{
					this.threshold = this.handleCount + this.handleCount / 10;
					num = this.gc_gen;
					if (this.gc_gen < 2)
					{
						this.gc_gen++;
					}
				}
			}
			if (num >= 0 && (num == 0 || this.gc_counts[num] == GC.CollectionCount(num)))
			{
				GC.Collect(num);
				Thread.Sleep(10 * num);
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		/// <summary>Decrements the current handle count.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Runtime.InteropServices.HandleCollector.Count" /> property is less than 0.</exception>
		// Token: 0x06000A76 RID: 2678 RVA: 0x0002D67C File Offset: 0x0002B87C
		public void Remove()
		{
			Interlocked.Decrement(ref this.handleCount);
			if (this.handleCount < 0)
			{
				throw new InvalidOperationException(SR.GetString("Handle collector count overflows or underflows."));
			}
			int num = this.handleCount + this.handleCount / 10;
			if (num < this.threshold - this.threshold / 10)
			{
				lock (this)
				{
					if (num > this.initialThreshold)
					{
						this.threshold = num;
					}
					else
					{
						this.threshold = this.initialThreshold;
					}
					this.gc_gen = 0;
				}
			}
			for (int i = 1; i < 3; i++)
			{
				this.gc_counts[i] = GC.CollectionCount(i);
			}
		}

		// Token: 0x0400070A RID: 1802
		private const int deltaPercent = 10;

		// Token: 0x0400070B RID: 1803
		private string name;

		// Token: 0x0400070C RID: 1804
		private int initialThreshold;

		// Token: 0x0400070D RID: 1805
		private int maximumThreshold;

		// Token: 0x0400070E RID: 1806
		private int threshold;

		// Token: 0x0400070F RID: 1807
		private int handleCount;

		// Token: 0x04000710 RID: 1808
		private int[] gc_counts = new int[3];

		// Token: 0x04000711 RID: 1809
		private int gc_gen;
	}
}
