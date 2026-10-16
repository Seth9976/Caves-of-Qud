using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000354 RID: 852
	public struct EasingFunction : IEquatable<EasingFunction>
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00069B1C File Offset: 0x00067D1C
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x00069B24 File Offset: 0x00067D24
		public EasingMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00069B2D File Offset: 0x00067D2D
		public EasingFunction(EasingMode mode)
		{
			this.m_Mode = mode;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00069B38 File Offset: 0x00067D38
		public static implicit operator EasingFunction(EasingMode easingMode)
		{
			return new EasingFunction(easingMode);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00069B50 File Offset: 0x00067D50
		public static bool operator ==(EasingFunction lhs, EasingFunction rhs)
		{
			return lhs.m_Mode == rhs.m_Mode;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00069B70 File Offset: 0x00067D70
		public static bool operator !=(EasingFunction lhs, EasingFunction rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00069B8C File Offset: 0x00067D8C
		public bool Equals(EasingFunction other)
		{
			return other == this;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00069BAC File Offset: 0x00067DAC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is EasingFunction)
			{
				EasingFunction easingFunction = (EasingFunction)obj;
				flag = this.Equals(easingFunction);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00069BD8 File Offset: 0x00067DD8
		public override string ToString()
		{
			return this.m_Mode.ToString();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00069BFC File Offset: 0x00067DFC
		public override int GetHashCode()
		{
			return (int)this.m_Mode;
		}

		// Token: 0x04000D89 RID: 3465
		private EasingMode m_Mode;

		// Token: 0x02000355 RID: 853
		internal class PropertyBag : ContainerPropertyBag<EasingFunction>
		{
			// Token: 0x06001943 RID: 6467 RVA: 0x00069C14 File Offset: 0x00067E14
			public PropertyBag()
			{
				base.AddProperty<EasingMode>(new EasingFunction.PropertyBag.ModeProperty());
			}

			// Token: 0x02000356 RID: 854
			private class ModeProperty : Property<EasingFunction, EasingMode>
			{
				// Token: 0x170004FF RID: 1279
				// (get) Token: 0x06001944 RID: 6468 RVA: 0x00069C2A File Offset: 0x00067E2A
				public override string Name { get; } = "mode";

				// Token: 0x17000500 RID: 1280
				// (get) Token: 0x06001945 RID: 6469 RVA: 0x00069C32 File Offset: 0x00067E32
				public override bool IsReadOnly { get; } = false;

				// Token: 0x06001946 RID: 6470 RVA: 0x00069C3A File Offset: 0x00067E3A
				public override EasingMode GetValue(ref EasingFunction container)
				{
					return container.mode;
				}

				// Token: 0x06001947 RID: 6471 RVA: 0x00069C42 File Offset: 0x00067E42
				public override void SetValue(ref EasingFunction container, EasingMode value)
				{
					container.mode = value;
				}
			}
		}
	}
}
