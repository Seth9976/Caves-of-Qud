using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000155 RID: 341
	[DisplayStringFormat("{up}/{left}/{down}/{right}")]
	[DisplayName("Up/Down/Left/Right Composite")]
	public class Vector2Composite : InputBindingComposite<Vector2>
	{
		// Token: 0x06001283 RID: 4739 RVA: 0x00057B2C File Offset: 0x00055D2C
		public override Vector2 ReadValue(ref InputBindingCompositeContext context)
		{
			Vector2Composite.Mode mode = this.mode;
			if (mode == Vector2Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(this.up);
				float num2 = context.ReadValue<float>(this.down);
				float num3 = context.ReadValue<float>(this.left);
				float num4 = context.ReadValue<float>(this.right);
				return DpadControl.MakeDpadVector(num, num2, num3, num4);
			}
			bool flag = context.ReadValueAsButton(this.up);
			bool flag2 = context.ReadValueAsButton(this.down);
			bool flag3 = context.ReadValueAsButton(this.left);
			bool flag4 = context.ReadValueAsButton(this.right);
			if (!this.normalize)
			{
				mode = Vector2Composite.Mode.Digital;
			}
			return DpadControl.MakeDpadVector(flag, flag2, flag3, flag4, mode == Vector2Composite.Mode.DigitalNormalized);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00057BD0 File Offset: 0x00055DD0
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context).magnitude;
		}

		// Token: 0x0400076A RID: 1898
		[InputControl(layout = "Axis")]
		public int up;

		// Token: 0x0400076B RID: 1899
		[InputControl(layout = "Axis")]
		public int down;

		// Token: 0x0400076C RID: 1900
		[InputControl(layout = "Axis")]
		public int left;

		// Token: 0x0400076D RID: 1901
		[InputControl(layout = "Axis")]
		public int right;

		// Token: 0x0400076E RID: 1902
		[Obsolete("Use Mode.DigitalNormalized with 'mode' instead")]
		public bool normalize = true;

		// Token: 0x0400076F RID: 1903
		public Vector2Composite.Mode mode;

		// Token: 0x0200025C RID: 604
		public enum Mode
		{
			// Token: 0x04000CD4 RID: 3284
			Analog = 2,
			// Token: 0x04000CD5 RID: 3285
			DigitalNormalized = 0,
			// Token: 0x04000CD6 RID: 3286
			Digital
		}
	}
}
