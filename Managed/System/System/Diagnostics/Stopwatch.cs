using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000283 RID: 643
	public class Stopwatch
	{
		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001462 RID: 5218
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTimestamp();

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001463 RID: 5219 RVA: 0x0005311E File Offset: 0x0005131E
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0005312B File Offset: 0x0005132B
		public TimeSpan Elapsed
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return TimeSpan.FromTicks(this.ElapsedTicks / (Stopwatch.Frequency / 10000000L));
				}
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00053158 File Offset: 0x00051358
		public long ElapsedMilliseconds
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return this.ElapsedTicks / (Stopwatch.Frequency / 1000L);
				}
				return checked((long)this.Elapsed.TotalMilliseconds);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0005318F File Offset: 0x0005138F
		public long ElapsedTicks
		{
			get
			{
				if (!this.is_running)
				{
					return this.elapsed;
				}
				return Stopwatch.GetTimestamp() - this.started + this.elapsed;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000531B3 File Offset: 0x000513B3
		public bool IsRunning
		{
			get
			{
				return this.is_running;
			}
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001469 RID: 5225 RVA: 0x000531BB File Offset: 0x000513BB
		public void Reset()
		{
			this.elapsed = 0L;
			this.is_running = false;
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146A RID: 5226 RVA: 0x000531CC File Offset: 0x000513CC
		public void Start()
		{
			if (this.is_running)
			{
				return;
			}
			this.started = Stopwatch.GetTimestamp();
			this.is_running = true;
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146B RID: 5227 RVA: 0x000531E9 File Offset: 0x000513E9
		public void Stop()
		{
			if (!this.is_running)
			{
				return;
			}
			this.elapsed += Stopwatch.GetTimestamp() - this.started;
			if (this.elapsed < 0L)
			{
				this.elapsed = 0L;
			}
			this.is_running = false;
		}

		/// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
		// Token: 0x0600146C RID: 5228 RVA: 0x00053226 File Offset: 0x00051426
		public void Restart()
		{
			this.started = Stopwatch.GetTimestamp();
			this.elapsed = 0L;
			this.is_running = true;
		}

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B84 RID: 2948
		public static readonly long Frequency = 10000000L;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B85 RID: 2949
		public static readonly bool IsHighResolution = true;

		// Token: 0x04000B86 RID: 2950
		private long elapsed;

		// Token: 0x04000B87 RID: 2951
		private long started;

		// Token: 0x04000B88 RID: 2952
		private bool is_running;
	}
}
