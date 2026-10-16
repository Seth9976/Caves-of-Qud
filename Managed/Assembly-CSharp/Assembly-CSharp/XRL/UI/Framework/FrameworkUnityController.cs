using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001513 RID: 5395
	public class FrameworkUnityController : MonoBehaviour
	{
		// Token: 0x0600D234 RID: 53812 RVA: 0x004E09A1 File Offset: 0x004DEBA1
		public void Awake()
		{
			FrameworkUnityController.instance = this;
			this.controller = NavigationController.instance;
		}

		// Token: 0x0600D235 RID: 53813 RVA: 0x004E09B4 File Offset: 0x004DEBB4
		public void Update()
		{
			NavigationContext navigationContext = this.controller.activeContext;
			FrameworkUnityController.usedCommands.Clear();
			while (navigationContext != null)
			{
				NavigationContext navigationContext2 = navigationContext;
				if (navigationContext.commandHandlers != null)
				{
					foreach (KeyValuePair<string, Action> keyValuePair in navigationContext.commandHandlers)
					{
						if (!FrameworkUnityController.usedCommands.Contains(keyValuePair.Key))
						{
							FrameworkUnityController.usedCommands.Add(keyValuePair.Key);
							if (ControlManager.isCommandDown(keyValuePair.Key, true, false, false))
							{
								Event @event = this.controller.FireInputCommandEvent(keyValuePair.Key, null);
								if (@event.cancelled || @event.handled || navigationContext2 != this.controller.activeContext)
								{
									return;
								}
								navigationContext2 = navigationContext;
							}
						}
					}
				}
				navigationContext = navigationContext.parentContext;
			}
			if (this.controller.activeContext == null)
			{
				return;
			}
			if (ControlManager.isCommandDown("Navigate Left", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationXAxis, null, -1);
			}
			if (ControlManager.isCommandDown("Navigate Right", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationXAxis, null, 1);
			}
			if (ControlManager.isCommandDown("Navigate Up", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationYAxis, null, -1);
			}
			if (ControlManager.isCommandDown("Navigate Down", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationYAxis, null, 1);
			}
			if (ControlManager.isCommandDown("U Negative", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationUAxis, null, -1);
			}
			if (ControlManager.isCommandDown("U Positive", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationUAxis, null, 1);
			}
			if (ControlManager.isCommandDown("V Negative", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationVAxis, null, -1);
			}
			if (ControlManager.isCommandDown("V Positive", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationVAxis, null, 1);
			}
			if (ControlManager.isCommandDown("Page Up", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationPageYAxis, null, -1);
			}
			if (ControlManager.isCommandDown("Page Down", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationPageYAxis, null, 1);
			}
			if (ControlManager.isCommandDown("Page Left", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationPageXAxis, null, -1);
			}
			if (ControlManager.isCommandDown("Page Right", true, false, false))
			{
				this.controller.FireInputAxisEvent(InputAxisTypes.NavigationPageXAxis, null, 1);
			}
			if (ControlManager.isCommandDown("Accept", true, false, false))
			{
				this.controller.FireInputButtonEvent(InputButtonTypes.AcceptButton, null);
			}
			if (ControlManager.isCommandDown("Cancel", true, false, false))
			{
				this.controller.FireInputButtonEvent(InputButtonTypes.CancelButton, null);
				return;
			}
		}

		// Token: 0x0400534F RID: 21327
		public bool DebugContextActivity;

		// Token: 0x04005350 RID: 21328
		public static FrameworkUnityController instance;

		// Token: 0x04005351 RID: 21329
		public NavigationController controller;

		// Token: 0x04005352 RID: 21330
		private static HashSet<string> usedCommands = new HashSet<string>();
	}
}
