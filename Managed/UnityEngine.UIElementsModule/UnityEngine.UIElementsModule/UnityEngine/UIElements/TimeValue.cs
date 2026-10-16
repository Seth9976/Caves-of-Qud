using System;
using System.Globalization;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200047C RID: 1148
	public struct TimeValue : IEquatable<TimeValue>
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x000845A0 File Offset: 0x000827A0
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x000845A8 File Offset: 0x000827A8
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x000845B1 File Offset: 0x000827B1
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x000845B9 File Offset: 0x000827B9
		public TimeUnit unit
		{
			get
			{
				return this.m_Unit;
			}
			set
			{
				this.m_Unit = value;
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000845C2 File Offset: 0x000827C2
		public TimeValue(float value)
		{
			this = new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000845CE File Offset: 0x000827CE
		public TimeValue(float value, TimeUnit unit)
		{
			this.m_Value = value;
			this.m_Unit = unit;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000845E0 File Offset: 0x000827E0
		public static implicit operator TimeValue(float value)
		{
			return new TimeValue(value, TimeUnit.Second);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000845FC File Offset: 0x000827FC
		public static bool operator ==(TimeValue lhs, TimeValue rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00084630 File Offset: 0x00082830
		public static bool operator !=(TimeValue lhs, TimeValue rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0008464C File Offset: 0x0008284C
		public bool Equals(TimeValue other)
		{
			return other == this;
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0008466C File Offset: 0x0008286C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is TimeValue)
			{
				TimeValue timeValue = (TimeValue)obj;
				flag = this.Equals(timeValue);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00084698 File Offset: 0x00082898
		public override int GetHashCode()
		{
			return (this.m_Value.GetHashCode() * 397) ^ (int)this.m_Unit;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000846C4 File Offset: 0x000828C4
		public override string ToString()
		{
			string text = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string text2 = string.Empty;
			TimeUnit unit = this.unit;
			TimeUnit timeUnit = unit;
			if (timeUnit != TimeUnit.Second)
			{
				if (timeUnit == TimeUnit.Millisecond)
				{
					text2 = "ms";
				}
			}
			else
			{
				text2 = "s";
			}
			return text + text2;
		}

		// Token: 0x04000FE4 RID: 4068
		private float m_Value;

		// Token: 0x04000FE5 RID: 4069
		private TimeUnit m_Unit;

		// Token: 0x0200047D RID: 1149
		internal class PropertyBag : ContainerPropertyBag<TimeValue>
		{
			// Token: 0x06002421 RID: 9249 RVA: 0x00084724 File Offset: 0x00082924
			public PropertyBag()
			{
				base.AddProperty<float>(new TimeValue.PropertyBag.ValueProperty());
				base.AddProperty<TimeUnit>(new TimeValue.PropertyBag.UnitProperty());
			}

			// Token: 0x0200047E RID: 1150
			private class ValueProperty : Property<TimeValue, float>
			{
				// Token: 0x17000958 RID: 2392
				// (get) Token: 0x06002422 RID: 9250 RVA: 0x00084746 File Offset: 0x00082946
				public override string Name { get; } = "value";

				// Token: 0x17000959 RID: 2393
				// (get) Token: 0x06002423 RID: 9251 RVA: 0x0008474E File Offset: 0x0008294E
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002424 RID: 9252 RVA: 0x00084756 File Offset: 0x00082956
				public override float GetValue(ref TimeValue container)
				{
					return container.value;
				}

				// Token: 0x06002425 RID: 9253 RVA: 0x0008475E File Offset: 0x0008295E
				public override void SetValue(ref TimeValue container, float value)
				{
					container.value = value;
				}
			}

			// Token: 0x0200047F RID: 1151
			private class UnitProperty : Property<TimeValue, TimeUnit>
			{
				// Token: 0x1700095A RID: 2394
				// (get) Token: 0x06002427 RID: 9255 RVA: 0x00084783 File Offset: 0x00082983
				public override string Name { get; } = "unit";

				// Token: 0x1700095B RID: 2395
				// (get) Token: 0x06002428 RID: 9256 RVA: 0x0008478B File Offset: 0x0008298B
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06002429 RID: 9257 RVA: 0x00084793 File Offset: 0x00082993
				public override TimeUnit GetValue(ref TimeValue container)
				{
					return container.unit;
				}

				// Token: 0x0600242A RID: 9258 RVA: 0x0008479B File Offset: 0x0008299B
				public override void SetValue(ref TimeValue container, TimeUnit value)
				{
					container.unit = value;
				}
			}
		}
	}
}
