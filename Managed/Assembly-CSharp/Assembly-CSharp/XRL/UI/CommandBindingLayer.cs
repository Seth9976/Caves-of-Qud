using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace XRL.UI
{
	// Token: 0x0200146F RID: 5231
	public class CommandBindingLayer
	{
		// Token: 0x0600CDB6 RID: 52662 RVA: 0x004A8EC4 File Offset: 0x004A70C4
		public CommandBindingLayer(string name)
		{
			this.name = name;
		}

		// Token: 0x0600CDB7 RID: 52663 RVA: 0x004A8EE0 File Offset: 0x004A70E0
		public void Enable()
		{
			if (this.sanityCheck != 0)
			{
				MetricsManager.LogWarning("layer " + this.name + " didn't pass sanity check on Enable");
			}
			foreach (CommandBinding commandBinding in this.actions)
			{
				if (!commandBinding.enabled)
				{
					commandBinding.Enable();
				}
			}
			this.enabled = true;
		}

		// Token: 0x0600CDB8 RID: 52664 RVA: 0x004A8F64 File Offset: 0x004A7164
		public void Disable()
		{
			foreach (CommandBinding commandBinding in this.actions)
			{
				if (commandBinding.enabled)
				{
					commandBinding.Disable();
				}
			}
			this.enabled = false;
		}

		// Token: 0x0600CDB9 RID: 52665 RVA: 0x004A8FC8 File Offset: 0x004A71C8
		public InputAction AddAction(string id, InputActionType type)
		{
			try
			{
				this.sanityCheck++;
				CommandBindingManager.currentActionMap.Disable();
				return CommandBindingManager.currentActionMap.AddAction(id, type, null, null, null, null, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("InputActionLayer::AddAction", ex, "game_exception");
			}
			return null;
		}

		// Token: 0x0600CDBA RID: 52666 RVA: 0x004A9028 File Offset: 0x004A7228
		public void AddBinding(CommandBinding binding)
		{
			this.actions.Add(binding);
			binding.Disable();
			this.sanityCheck--;
			if (this.sanityCheck != 0)
			{
				MetricsManager.LogWarning("layer " + this.name + " didn't pass sanity check on AddBinding");
			}
		}

		// Token: 0x04004F7E RID: 20350
		public bool enabled;

		// Token: 0x04004F7F RID: 20351
		public string name;

		// Token: 0x04004F80 RID: 20352
		public List<CommandBinding> actions = new List<CommandBinding>();

		// Token: 0x04004F81 RID: 20353
		private int sanityCheck;
	}
}
