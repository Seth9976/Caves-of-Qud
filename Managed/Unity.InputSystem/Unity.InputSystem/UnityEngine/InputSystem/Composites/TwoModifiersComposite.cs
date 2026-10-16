using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000154 RID: 340
	[DisplayStringFormat("{modifier1}+{modifier2}+{binding}")]
	[DisplayName("Binding With Two Modifiers")]
	public class TwoModifiersComposite : InputBindingComposite
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000579D4 File Offset: 0x00055BD4
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x000579DC File Offset: 0x00055BDC
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000579E4 File Offset: 0x00055BE4
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00057A01 File Offset: 0x00055C01
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifiersArePressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00057A28 File Offset: 0x00055C28
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (flag && this.m_BindingIsButton && this.modifiersOrder == TwoModifiersComposite.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(this.binding);
				double pressTime2 = context.GetPressTime(this.modifier1);
				double pressTime3 = context.GetPressTime(this.modifier2);
				return pressTime2 <= pressTime && pressTime3 <= pressTime;
			}
			return flag;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00057A9C File Offset: 0x00055C9C
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (this.modifiersOrder == TwoModifiersComposite.ModifiersOrder.Default)
			{
				if (this.overrideModifiersNeedToBePressedFirst)
				{
					this.modifiersOrder = TwoModifiersComposite.ModifiersOrder.Unordered;
					return;
				}
				this.modifiersOrder = (InputSystem.settings.shortcutKeysConsumeInput ? TwoModifiersComposite.ModifiersOrder.Ordered : TwoModifiersComposite.ModifiersOrder.Unordered);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00057AF5 File Offset: 0x00055CF5
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x04000762 RID: 1890
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x04000763 RID: 1891
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000764 RID: 1892
		[InputControl]
		public int binding;

		// Token: 0x04000765 RID: 1893
		[Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000766 RID: 1894
		[Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public TwoModifiersComposite.ModifiersOrder modifiersOrder;

		// Token: 0x04000767 RID: 1895
		private int m_ValueSizeInBytes;

		// Token: 0x04000768 RID: 1896
		private Type m_ValueType;

		// Token: 0x04000769 RID: 1897
		private bool m_BindingIsButton;

		// Token: 0x0200025B RID: 603
		public enum ModifiersOrder
		{
			// Token: 0x04000CD0 RID: 3280
			Default,
			// Token: 0x04000CD1 RID: 3281
			Ordered,
			// Token: 0x04000CD2 RID: 3282
			Unordered
		}
	}
}
