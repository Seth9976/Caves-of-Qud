using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200071F RID: 1823
	public sealed class ValueAnimation<T> : IValueAnimationUpdate, IValueAnimation
	{
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000CD038 File Offset: 0x000CB238
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x000CD050 File Offset: 0x000CB250
		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000CD072 File Offset: 0x000CB272
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x000CD07A File Offset: 0x000CB27A
		public Func<float, float> easingCurve { get; set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x000CD083 File Offset: 0x000CB283
		// (set) Token: 0x060034DA RID: 13530 RVA: 0x000CD08B File Offset: 0x000CB28B
		public bool isRunning { get; private set; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x000CD094 File Offset: 0x000CB294
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x000CD09C File Offset: 0x000CB29C
		public Action onAnimationCompleted { get; set; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000CD0A5 File Offset: 0x000CB2A5
		// (set) Token: 0x060034DE RID: 13534 RVA: 0x000CD0AD File Offset: 0x000CB2AD
		public bool autoRecycle { get; set; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000CD0B6 File Offset: 0x000CB2B6
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000CD0BE File Offset: 0x000CB2BE
		private bool recycled { get; set; }

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000CD0C7 File Offset: 0x000CB2C7
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000CD0CF File Offset: 0x000CB2CF
		private VisualElement owner { get; set; }

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000CD0D8 File Offset: 0x000CB2D8
		// (set) Token: 0x060034E4 RID: 13540 RVA: 0x000CD0E0 File Offset: 0x000CB2E0
		public Action<VisualElement, T> valueUpdated { get; set; }

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000CD0E9 File Offset: 0x000CB2E9
		// (set) Token: 0x060034E6 RID: 13542 RVA: 0x000CD0F1 File Offset: 0x000CB2F1
		public Func<VisualElement, T> initialValue { get; set; }

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000CD0FA File Offset: 0x000CB2FA
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x000CD102 File Offset: 0x000CB302
		public Func<T, T, float, T> interpolator { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000CD10C File Offset: 0x000CB30C
		// (set) Token: 0x060034EA RID: 13546 RVA: 0x000CD15A File Offset: 0x000CB35A
		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000CD16B File Offset: 0x000CB36B
		// (set) Token: 0x060034EC RID: 13548 RVA: 0x000CD173 File Offset: 0x000CB373
		public T to { get; set; }

		// Token: 0x060034ED RID: 13549 RVA: 0x000CD17C File Offset: 0x000CB37C
		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000CD194 File Offset: 0x000CB394
		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000CD1D4 File Offset: 0x000CB3D4
		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action onAnimationCompleted = this.onAnimationCompleted;
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000CD238 File Offset: 0x000CB438
		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000CD298 File Offset: 0x000CB498
		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long num = currentTimeMs - this.m_StartTimeMs;
			float num2 = (float)num / (float)this.durationMs;
			bool flag = false;
			bool flag2 = num2 >= 1f;
			if (flag2)
			{
				num2 = 1f;
				flag = true;
			}
			Func<float, float> easingCurve = this.easingCurve;
			num2 = ((easingCurve != null) ? easingCurve(num2) : num2);
			bool flag3 = this.interpolator != null;
			if (flag3)
			{
				T t = this.interpolator(this.from, this.to, num2);
				Action<VisualElement, T> valueUpdated = this.valueUpdated;
				if (valueUpdated != null)
				{
					valueUpdated(this.owner, t);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.Stop();
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000CD348 File Offset: 0x000CB548
		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000CD3D4 File Offset: 0x000CB5D4
		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000CD400 File Offset: 0x000CB600
		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000CD42C File Offset: 0x000CB62C
		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000CD468 File Offset: 0x000CB668
		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000CD48C File Offset: 0x000CB68C
		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> valueAnimation = ValueAnimation<T>.sObjectPool.Get();
			valueAnimation.recycled = false;
			valueAnimation.SetOwner(e);
			valueAnimation.interpolator = interpolator;
			return valueAnimation;
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000CD4C4 File Offset: 0x000CB6C4
		public ValueAnimation<T> Ease(Func<float, float> easing)
		{
			this.easingCurve = easing;
			return this;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000CD4E0 File Offset: 0x000CB6E0
		public ValueAnimation<T> OnCompleted(Action callback)
		{
			this.onAnimationCompleted = callback;
			return this;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CD4FC File Offset: 0x000CB6FC
		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}

		// Token: 0x04001AEA RID: 6890
		private const int k_DefaultDurationMs = 400;

		// Token: 0x04001AEB RID: 6891
		private const int k_DefaultMaxPoolSize = 100;

		// Token: 0x04001AEC RID: 6892
		private long m_StartTimeMs;

		// Token: 0x04001AED RID: 6893
		private int m_DurationMs;

		// Token: 0x04001AF3 RID: 6899
		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(() => new ValueAnimation<T>(), 100);

		// Token: 0x04001AF8 RID: 6904
		private T _from;

		// Token: 0x04001AF9 RID: 6905
		private bool fromValueSet = false;
	}
}
