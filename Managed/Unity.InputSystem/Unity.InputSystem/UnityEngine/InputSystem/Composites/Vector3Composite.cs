using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000156 RID: 342
	[DisplayStringFormat("{up}+{down}/{left}+{right}/{forward}+{backward}")]
	[DisplayName("Up/Down/Left/Right/Forward/Backward Composite")]
	public class Vector3Composite : InputBindingComposite<Vector3>
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x00057BFC File Offset: 0x00055DFC
		public override Vector3 ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.mode == Vector3Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(this.up);
				float num2 = context.ReadValue<float>(this.down);
				float num3 = context.ReadValue<float>(this.left);
				float num4 = context.ReadValue<float>(this.right);
				float num5 = context.ReadValue<float>(this.forward);
				float num6 = context.ReadValue<float>(this.backward);
				return new Vector3(num4 - num3, num - num2, num5 - num6);
			}
			float num7 = (context.ReadValueAsButton(this.up) ? 1f : 0f);
			float num8 = (context.ReadValueAsButton(this.down) ? (-1f) : 0f);
			float num9 = (context.ReadValueAsButton(this.left) ? (-1f) : 0f);
			float num10 = (context.ReadValueAsButton(this.right) ? 1f : 0f);
			float num11 = (context.ReadValueAsButton(this.forward) ? 1f : 0f);
			float num12 = (context.ReadValueAsButton(this.backward) ? (-1f) : 0f);
			Vector3 normalized = new Vector3(num9 + num10, num7 + num8, num11 + num12);
			if (this.mode == Vector3Composite.Mode.DigitalNormalized)
			{
				normalized = normalized.normalized;
			}
			return normalized;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00057D40 File Offset: 0x00055F40
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context).magnitude;
		}

		// Token: 0x04000770 RID: 1904
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x04000771 RID: 1905
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x04000772 RID: 1906
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x04000773 RID: 1907
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x04000774 RID: 1908
		[InputControl(layout = "Axis")]
		public int forward;

		// Token: 0x04000775 RID: 1909
		[InputControl(layout = "Axis")]
		public int backward;

		// Token: 0x04000776 RID: 1910
		public Vector3Composite.Mode mode;

		// Token: 0x0200025D RID: 605
		public enum Mode
		{
			// Token: 0x04000CD8 RID: 3288
			Analog,
			// Token: 0x04000CD9 RID: 3289
			DigitalNormalized,
			// Token: 0x04000CDA RID: 3290
			Digital
		}
	}
}
