using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers
{
	/// <summary>Generates recurring events in an application.</summary>
	// Token: 0x02000196 RID: 406
	[DefaultEvent("Elapsed")]
	[DefaultProperty("Interval")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Timer : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
		// Token: 0x06000A92 RID: 2706 RVA: 0x0002D744 File Offset: 0x0002B944
		public Timer()
		{
			this.interval = 100.0;
			this.enabled = false;
			this.autoReset = true;
			this.initializing = false;
			this.delayedEnable = false;
			this.callback = new TimerCallback(this.MyTimerCallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
		/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x06000A93 RID: 2707 RVA: 0x0002D794 File Offset: 0x0002B994
		public Timer(double interval)
			: this()
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
			}
			this.interval = (double)Timer.CalculateRoundedInterval(interval, true);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the specified interval elapses or only after the first time it elapses.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; false if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is true.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
		[DefaultValue(true)]
		[TimersDescription("Indicates whether the timer will be restarted when it is enabled.")]
		[Category("Behavior")]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				if (base.DesignMode)
				{
					this.autoReset = value;
					return;
				}
				if (this.autoReset != value)
				{
					this.autoReset = value;
					if (this.timer != null)
					{
						this.UpdateTimer();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>true if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled. </exception>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002D820 File Offset: 0x0002BA20
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0002D828 File Offset: 0x0002BA28
		[TimersDescription("Indicates whether the timer is enabled to fire events at a defined interval.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (base.DesignMode)
				{
					this.delayedEnable = value;
					this.enabled = value;
					return;
				}
				if (this.initializing)
				{
					this.delayedEnable = value;
					return;
				}
				if (this.enabled != value)
				{
					if (!value)
					{
						if (this.timer != null)
						{
							this.cookie = null;
							this.timer.Dispose();
							this.timer = null;
						}
						this.enabled = value;
						return;
					}
					this.enabled = value;
					if (this.timer == null)
					{
						if (this.disposed)
						{
							throw new ObjectDisposedException(base.GetType().Name);
						}
						int num = Timer.CalculateRoundedInterval(this.interval, false);
						this.cookie = new object();
						this.timer = new Timer(this.callback, this.cookie, num, this.autoReset ? num : (-1));
						return;
					}
					else
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002D900 File Offset: 0x0002BB00
		private static int CalculateRoundedInterval(double interval, bool argumentCheck = false)
		{
			double num = Math.Ceiling(interval);
			if (num <= 2147483647.0 && num > 0.0)
			{
				return (int)num;
			}
			if (argumentCheck)
			{
				throw new ArgumentException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
			}
			throw new ArgumentOutOfRangeException(SR.GetString("Invalid value '{1}' for parameter '{0}'.", new object[] { "interval", interval }));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002D980 File Offset: 0x0002BB80
		private void UpdateTimer()
		{
			int num = Timer.CalculateRoundedInterval(this.interval, false);
			this.timer.Change(num, this.autoReset ? num : (-1));
		}

		/// <summary>Gets or sets the interval at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.-or-The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.) </exception>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002D9B3 File Offset: 0x0002BBB3
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002D9BC File Offset: 0x0002BBBC
		[SettingsBindable(true)]
		[DefaultValue(100.0)]
		[TimersDescription("The number of milliseconds between timer events.")]
		[Category("Behavior")]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(SR.GetString("'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.", new object[] { value, 0 }));
				}
				this.interval = value;
				if (this.timer != null)
				{
					this.UpdateTimer();
				}
			}
		}

		/// <summary>Occurs when the interval elapses.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A9C RID: 2716 RVA: 0x0002DA12 File Offset: 0x0002BC12
		// (remove) Token: 0x06000A9D RID: 2717 RVA: 0x0002DA2B File Offset: 0x0002BC2B
		[TimersDescription("Occurs when the Interval has elapsed.")]
		[Category("Behavior")]
		public event ElapsedEventHandler Elapsed
		{
			add
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(this.onIntervalElapsed, value);
			}
			remove
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(this.onIntervalElapsed, value);
			}
		}

		/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002DA44 File Offset: 0x0002BC44
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (base.DesignMode)
				{
					this.enabled = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is null.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0002DA64 File Offset: 0x0002BC64
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0002DABE File Offset: 0x0002BCBE
		[TimersDescription("The object used to marshal the event handler calls issued when an interval has elapsed.")]
		[DefaultValue(null)]
		[Browsable(false)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002DAC7 File Offset: 0x0002BCC7
		public void BeginInit()
		{
			this.Close();
			this.initializing = true;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002DAD6 File Offset: 0x0002BCD6
		public void Close()
		{
			this.initializing = false;
			this.delayedEnable = false;
			this.enabled = false;
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002DB07 File Offset: 0x0002BD07
		protected override void Dispose(bool disposing)
		{
			this.Close();
			this.disposed = true;
			base.Dispose(disposing);
		}

		/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002DB1D File Offset: 0x0002BD1D
		public void EndInit()
		{
			this.initializing = false;
			this.Enabled = this.delayedEnable;
		}

		/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to true.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002DB32 File Offset: 0x0002BD32
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to false.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002DB3B File Offset: 0x0002BD3B
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002DB44 File Offset: 0x0002BD44
		private void MyTimerCallback(object state)
		{
			if (state != this.cookie)
			{
				return;
			}
			if (!this.autoReset)
			{
				this.enabled = false;
			}
			ElapsedEventArgs e = new ElapsedEventArgs(DateTime.Now);
			try
			{
				ElapsedEventHandler elapsedEventHandler = this.onIntervalElapsed;
				if (elapsedEventHandler != null)
				{
					if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
					{
						this.SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[] { this, e });
					}
					else
					{
						elapsedEventHandler(this, e);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000737 RID: 1847
		private double interval;

		// Token: 0x04000738 RID: 1848
		private bool enabled;

		// Token: 0x04000739 RID: 1849
		private bool initializing;

		// Token: 0x0400073A RID: 1850
		private bool delayedEnable;

		// Token: 0x0400073B RID: 1851
		private ElapsedEventHandler onIntervalElapsed;

		// Token: 0x0400073C RID: 1852
		private bool autoReset;

		// Token: 0x0400073D RID: 1853
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x0400073E RID: 1854
		private bool disposed;

		// Token: 0x0400073F RID: 1855
		private Timer timer;

		// Token: 0x04000740 RID: 1856
		private TimerCallback callback;

		// Token: 0x04000741 RID: 1857
		private object cookie;
	}
}
