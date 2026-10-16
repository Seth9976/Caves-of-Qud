using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000450 RID: 1104
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct TransitionData : IStyleDataGroup<TransitionData>, IEquatable<TransitionData>
	{
		// Token: 0x06002245 RID: 8773 RVA: 0x0007C130 File Offset: 0x0007A330
		public TransitionData Copy()
		{
			return new TransitionData
			{
				transitionDelay = new List<TimeValue>(this.transitionDelay),
				transitionDuration = new List<TimeValue>(this.transitionDuration),
				transitionProperty = new List<StylePropertyName>(this.transitionProperty),
				transitionTimingFunction = new List<EasingFunction>(this.transitionTimingFunction)
			};
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0007C194 File Offset: 0x0007A394
		public void CopyFrom(ref TransitionData other)
		{
			bool flag = this.transitionDelay != other.transitionDelay;
			if (flag)
			{
				this.transitionDelay.Clear();
				this.transitionDelay.AddRange(other.transitionDelay);
			}
			bool flag2 = this.transitionDuration != other.transitionDuration;
			if (flag2)
			{
				this.transitionDuration.Clear();
				this.transitionDuration.AddRange(other.transitionDuration);
			}
			bool flag3 = this.transitionProperty != other.transitionProperty;
			if (flag3)
			{
				this.transitionProperty.Clear();
				this.transitionProperty.AddRange(other.transitionProperty);
			}
			bool flag4 = this.transitionTimingFunction != other.transitionTimingFunction;
			if (flag4)
			{
				this.transitionTimingFunction.Clear();
				this.transitionTimingFunction.AddRange(other.transitionTimingFunction);
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0007C278 File Offset: 0x0007A478
		public static bool operator ==(TransitionData lhs, TransitionData rhs)
		{
			return lhs.transitionDelay == rhs.transitionDelay && lhs.transitionDuration == rhs.transitionDuration && lhs.transitionProperty == rhs.transitionProperty && lhs.transitionTimingFunction == rhs.transitionTimingFunction;
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007C2C8 File Offset: 0x0007A4C8
		public static bool operator !=(TransitionData lhs, TransitionData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x0007C2E4 File Offset: 0x0007A4E4
		public bool Equals(TransitionData other)
		{
			return other == this;
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0007C304 File Offset: 0x0007A504
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransitionData && this.Equals((TransitionData)obj);
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0007C33C File Offset: 0x0007A53C
		public override int GetHashCode()
		{
			int num = this.transitionDelay.GetHashCode();
			num = (num * 397) ^ this.transitionDuration.GetHashCode();
			num = (num * 397) ^ this.transitionProperty.GetHashCode();
			return (num * 397) ^ this.transitionTimingFunction.GetHashCode();
		}

		// Token: 0x04000F7B RID: 3963
		public List<TimeValue> transitionDelay;

		// Token: 0x04000F7C RID: 3964
		public List<TimeValue> transitionDuration;

		// Token: 0x04000F7D RID: 3965
		public List<StylePropertyName> transitionProperty;

		// Token: 0x04000F7E RID: 3966
		public List<EasingFunction> transitionTimingFunction;
	}
}
