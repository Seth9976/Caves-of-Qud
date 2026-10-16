using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000151 RID: 337
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier}+{button}")]
	public class ButtonWithOneModifier : InputBindingComposite<float>
	{
		// Token: 0x06001268 RID: 4712 RVA: 0x00057683 File Offset: 0x00055883
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000576A0 File Offset: 0x000558A0
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier);
			if (flag && this.modifiersOrder == ButtonWithOneModifier.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(this.button);
				return context.GetPressTime(this.modifier) <= pressTime;
			}
			return flag;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x000576E7 File Offset: 0x000558E7
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000576F0 File Offset: 0x000558F0
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (this.modifiersOrder == ButtonWithOneModifier.ModifiersOrder.Default)
			{
				if (this.overrideModifiersNeedToBePressedFirst)
				{
					this.modifiersOrder = ButtonWithOneModifier.ModifiersOrder.Unordered;
					return;
				}
				this.modifiersOrder = (InputSystem.settings.shortcutKeysConsumeInput ? ButtonWithOneModifier.ModifiersOrder.Ordered : ButtonWithOneModifier.ModifiersOrder.Unordered);
			}
		}

		// Token: 0x04000752 RID: 1874
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x04000753 RID: 1875
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x04000754 RID: 1876
		[Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000755 RID: 1877
		[Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public ButtonWithOneModifier.ModifiersOrder modifiersOrder;

		// Token: 0x02000258 RID: 600
		public enum ModifiersOrder
		{
			// Token: 0x04000CC4 RID: 3268
			Default,
			// Token: 0x04000CC5 RID: 3269
			Ordered,
			// Token: 0x04000CC6 RID: 3270
			Unordered
		}
	}
}
