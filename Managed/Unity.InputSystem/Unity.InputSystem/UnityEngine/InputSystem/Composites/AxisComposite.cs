using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000150 RID: 336
	[DisplayStringFormat("{negative}/{positive}")]
	[DisplayName("Positive/Negative Binding")]
	public class AxisComposite : InputBindingComposite<float>
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00057542 File Offset: 0x00055742
		public float midPoint
		{
			get
			{
				return (this.maxValue + this.minValue) / 2f;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00057558 File Offset: 0x00055758
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			float num = Mathf.Abs(context.ReadValue<float>(this.negative));
			float num2 = Mathf.Abs(context.ReadValue<float>(this.positive));
			bool flag = num > Mathf.Epsilon;
			bool flag2 = num2 > Mathf.Epsilon;
			if (flag == flag2)
			{
				switch (this.whichSideWins)
				{
				case AxisComposite.WhichSideWins.Neither:
					return this.midPoint;
				case AxisComposite.WhichSideWins.Positive:
					flag = false;
					break;
				}
			}
			float midPoint = this.midPoint;
			if (flag)
			{
				return midPoint - (midPoint - this.minValue) * num;
			}
			return midPoint + (this.maxValue - midPoint) * num2;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000575F4 File Offset: 0x000557F4
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			float num = this.ReadValue(ref context);
			if (num < this.midPoint)
			{
				num = Mathf.Abs(num - this.midPoint);
				return NormalizeProcessor.Normalize(num, 0f, Mathf.Abs(this.minValue), 0f);
			}
			num = Mathf.Abs(num - this.midPoint);
			return NormalizeProcessor.Normalize(num, 0f, Mathf.Abs(this.maxValue), 0f);
		}

		// Token: 0x0400074D RID: 1869
		[InputControl(layout = "Axis")]
		public int negative;

		// Token: 0x0400074E RID: 1870
		[InputControl(layout = "Axis")]
		public int positive;

		// Token: 0x0400074F RID: 1871
		[Tooltip("Value to return when the negative side is fully actuated.")]
		public float minValue = -1f;

		// Token: 0x04000750 RID: 1872
		[Tooltip("Value to return when the positive side is fully actuated.")]
		public float maxValue = 1f;

		// Token: 0x04000751 RID: 1873
		[Tooltip("If both the positive and negative side are actuated, decides what value to return. 'Neither' (default) means that the resulting value is the midpoint between min and max. 'Positive' means that max will be returned. 'Negative' means that min will be returned.")]
		public AxisComposite.WhichSideWins whichSideWins;

		// Token: 0x02000257 RID: 599
		public enum WhichSideWins
		{
			// Token: 0x04000CC0 RID: 3264
			Neither,
			// Token: 0x04000CC1 RID: 3265
			Positive,
			// Token: 0x04000CC2 RID: 3266
			Negative
		}
	}
}
