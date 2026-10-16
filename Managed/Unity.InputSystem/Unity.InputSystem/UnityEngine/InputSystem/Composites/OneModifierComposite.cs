using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000153 RID: 339
	[DisplayStringFormat("{modifier}+{binding}")]
	[DisplayName("Binding With One Modifier")]
	public class OneModifierComposite : InputBindingComposite
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000577F4 File Offset: 0x000559F4
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x000577FC File Offset: 0x000559FC
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00057804 File Offset: 0x00055A04
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00057821 File Offset: 0x00055A21
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifierIsPressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00057848 File Offset: 0x00055A48
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier);
			if (flag && this.m_BindingIsButton && this.modifiersOrder == OneModifierComposite.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(this.binding);
				return context.GetPressTime(this.modifier) <= pressTime;
			}
			return flag;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00057898 File Offset: 0x00055A98
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (this.modifiersOrder == OneModifierComposite.ModifiersOrder.Default)
			{
				if (this.overrideModifiersNeedToBePressedFirst)
				{
					this.modifiersOrder = OneModifierComposite.ModifiersOrder.Unordered;
					return;
				}
				this.modifiersOrder = (InputSystem.settings.shortcutKeysConsumeInput ? OneModifierComposite.ModifiersOrder.Ordered : OneModifierComposite.ModifiersOrder.Unordered);
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000578F1 File Offset: 0x00055AF1
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00057910 File Offset: 0x00055B10
		internal static void DetermineValueTypeAndSize(ref InputBindingCompositeContext context, int part, out Type valueType, out int valueSizeInBytes, out bool isButton)
		{
			valueSizeInBytes = 0;
			isButton = true;
			Type type = null;
			foreach (InputBindingCompositeContext.PartBinding partBinding in context.controls)
			{
				if (partBinding.part == part)
				{
					Type valueType2 = partBinding.control.valueType;
					if (type == null || valueType2.IsAssignableFrom(type))
					{
						type = valueType2;
					}
					else if (!type.IsAssignableFrom(valueType2))
					{
						type = typeof(Object);
					}
					valueSizeInBytes = Math.Max(partBinding.control.valueSizeInBytes, valueSizeInBytes);
					isButton &= partBinding.control.isButton;
				}
			}
			valueType = type;
		}

		// Token: 0x0400075B RID: 1883
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x0400075C RID: 1884
		[InputControl]
		public int binding;

		// Token: 0x0400075D RID: 1885
		[Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x0400075E RID: 1886
		[Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public OneModifierComposite.ModifiersOrder modifiersOrder;

		// Token: 0x0400075F RID: 1887
		private int m_ValueSizeInBytes;

		// Token: 0x04000760 RID: 1888
		private Type m_ValueType;

		// Token: 0x04000761 RID: 1889
		private bool m_BindingIsButton;

		// Token: 0x0200025A RID: 602
		public enum ModifiersOrder
		{
			// Token: 0x04000CCC RID: 3276
			Default,
			// Token: 0x04000CCD RID: 3277
			Ordered,
			// Token: 0x04000CCE RID: 3278
			Unordered
		}
	}
}
