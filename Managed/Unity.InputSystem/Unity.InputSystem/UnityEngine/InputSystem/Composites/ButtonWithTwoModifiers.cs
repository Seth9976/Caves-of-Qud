using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000152 RID: 338
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier1}+{modifier2}+{button}")]
	public class ButtonWithTwoModifiers : InputBindingComposite<float>
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x00057728 File Offset: 0x00055928
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00057748 File Offset: 0x00055948
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (flag && this.modifiersOrder == ButtonWithTwoModifiers.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(this.button);
				double pressTime2 = context.GetPressTime(this.modifier1);
				double pressTime3 = context.GetPressTime(this.modifier2);
				return pressTime2 <= pressTime && pressTime3 <= pressTime;
			}
			return flag;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000577B3 File Offset: 0x000559B3
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000577BC File Offset: 0x000559BC
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (this.modifiersOrder == ButtonWithTwoModifiers.ModifiersOrder.Default)
			{
				if (this.overrideModifiersNeedToBePressedFirst)
				{
					this.modifiersOrder = ButtonWithTwoModifiers.ModifiersOrder.Unordered;
					return;
				}
				this.modifiersOrder = (InputSystem.settings.shortcutKeysConsumeInput ? ButtonWithTwoModifiers.ModifiersOrder.Ordered : ButtonWithTwoModifiers.ModifiersOrder.Unordered);
			}
		}

		// Token: 0x04000756 RID: 1878
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x04000757 RID: 1879
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000758 RID: 1880
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x04000759 RID: 1881
		[Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x0400075A RID: 1882
		[Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public ButtonWithTwoModifiers.ModifiersOrder modifiersOrder;

		// Token: 0x02000259 RID: 601
		public enum ModifiersOrder
		{
			// Token: 0x04000CC8 RID: 3272
			Default,
			// Token: 0x04000CC9 RID: 3273
			Ordered,
			// Token: 0x04000CCA RID: 3274
			Unordered
		}
	}
}
