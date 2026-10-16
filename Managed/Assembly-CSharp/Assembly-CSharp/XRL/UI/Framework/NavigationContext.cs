using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	/// <summary>
	/// Navigation Context describes a smaller section of a larger screen (or perhaps a whole screen for some simple dialogs.)
	/// </summary>
	// Token: 0x02001525 RID: 5413
	public class NavigationContext
	{
		// Token: 0x0600D2B5 RID: 53941 RVA: 0x004E37E2 File Offset: 0x004E19E2
		public NavigationContext()
		{
		}

		// Token: 0x0600D2B6 RID: 53942 RVA: 0x004E37EA File Offset: 0x004E19EA
		public NavigationContext(string debugName)
		{
			this.displayName = debugName;
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600D2B7 RID: 53943 RVA: 0x004E37F9 File Offset: 0x004E19F9
		public string parentNames
		{
			get
			{
				if (this.parentContext != null)
				{
					return " < " + this.parentContext.displayName + this.parentContext.parentNames;
				}
				return "";
			}
		}

		// Token: 0x0600D2B8 RID: 53944 RVA: 0x004E3829 File Offset: 0x004E1A29
		public override string ToString()
		{
			return base.GetType().Name + ": " + this.displayName + this.parentNames;
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600D2B9 RID: 53945 RVA: 0x004E384C File Offset: 0x004E1A4C
		// (set) Token: 0x0600D2BA RID: 53946 RVA: 0x004E3854 File Offset: 0x004E1A54
		public virtual List<MenuOption> menuOptionDescriptions { get; set; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600D2BB RID: 53947 RVA: 0x004E385D File Offset: 0x004E1A5D
		public NavigationController navigationController
		{
			get
			{
				return NavigationController.instance;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600D2BC RID: 53948 RVA: 0x004E3864 File Offset: 0x004E1A64
		public Event currentEvent
		{
			get
			{
				return NavigationController.currentEvent;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600D2BD RID: 53949 RVA: 0x004E386B File Offset: 0x004E1A6B
		// (set) Token: 0x0600D2BE RID: 53950 RVA: 0x004E3873 File Offset: 0x004E1A73
		public virtual bool disabled { get; set; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600D2BF RID: 53951 RVA: 0x004E387C File Offset: 0x004E1A7C
		public virtual bool hasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600D2C0 RID: 53952 RVA: 0x004E387F File Offset: 0x004E1A7F
		public virtual IEnumerable<NavigationContext> parents
		{
			get
			{
				for (NavigationContext p = this.parentContext; p != null; p = p.parentContext)
				{
					yield return p;
				}
				yield break;
			}
		}

		// Token: 0x0600D2C1 RID: 53953 RVA: 0x004E388F File Offset: 0x004E1A8F
		public virtual void Setup()
		{
		}

		// Token: 0x0600D2C2 RID: 53954 RVA: 0x004E3894 File Offset: 0x004E1A94
		public virtual bool isDisabled()
		{
			for (NavigationContext navigationContext = this; navigationContext != null; navigationContext = navigationContext.parentContext)
			{
				if (navigationContext.disabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D2C3 RID: 53955 RVA: 0x004E38BA File Offset: 0x004E1ABA
		public virtual bool ActivateAndEnable()
		{
			return this.Activate(true);
		}

		// Token: 0x0600D2C4 RID: 53956 RVA: 0x004E38C4 File Offset: 0x004E1AC4
		public virtual bool Activate(bool evenIfDisabled = false)
		{
			if (this.isDisabled() && evenIfDisabled)
			{
				for (NavigationContext navigationContext = this; navigationContext != null; navigationContext = navigationContext.parentContext)
				{
					if (navigationContext.disabled)
					{
						navigationContext.disabled = false;
						if (FrameworkUnityController.instance.DebugContextActivity)
						{
							MetricsManager.LogError("DEBUG:Forced a UI Attempt to activate disabled context");
						}
					}
				}
			}
			FrameworkUnityController instance = FrameworkUnityController.instance;
			if (instance == null || !instance.DebugContextActivity || this.isDisabled())
			{
			}
			if (!this.isDisabled() || evenIfDisabled)
			{
				if (this.navigationController.activeContext != this)
				{
					SoundManager.PlayUISound("Sounds/UI/ui_cursor_scroll", 1f, false, true, SoundRequest.SoundEffectType.None);
				}
				this.navigationController.activeContext = this;
			}
			return this.IsActive(true);
		}

		// Token: 0x0600D2C5 RID: 53957 RVA: 0x004E396C File Offset: 0x004E1B6C
		public virtual bool IsActive(bool checkParents = true)
		{
			NavigationContext activeContext = this.navigationController.activeContext;
			while (checkParents && activeContext != null && activeContext != this)
			{
				activeContext = activeContext.parentContext;
			}
			return activeContext == this;
		}

		// Token: 0x0600D2C6 RID: 53958 RVA: 0x004E399C File Offset: 0x004E1B9C
		public bool IsInside(NavigationContext test)
		{
			for (NavigationContext navigationContext = this; navigationContext != null; navigationContext = navigationContext.parentContext)
			{
				if (navigationContext == test)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D2C7 RID: 53959 RVA: 0x004E39BE File Offset: 0x004E1BBE
		public virtual void OnEnter()
		{
			if (this.enterHandler != null)
			{
				this.enterHandler();
			}
		}

		// Token: 0x0600D2C8 RID: 53960 RVA: 0x004E39D3 File Offset: 0x004E1BD3
		public virtual void OnExit()
		{
			if (this.exitHandler != null)
			{
				this.exitHandler();
			}
		}

		// Token: 0x0600D2C9 RID: 53961 RVA: 0x004E39E8 File Offset: 0x004E1BE8
		public virtual void OnInput(Event e)
		{
			object obj;
			if (this.currentEvent.data.TryGetValue("commandId", out obj))
			{
				string text = obj as string;
				Action action;
				if (this.commandHandlers != null && this.commandHandlers.TryGetValue(text, out action))
				{
					action();
					return;
				}
			}
			else if (this.currentEvent.data.TryGetValue("button", out obj))
			{
				InputButtonTypes inputButtonTypes = (InputButtonTypes)obj;
				Action action2;
				if (this.buttonHandlers != null && this.buttonHandlers.TryGetValue(inputButtonTypes, out action2))
				{
					action2();
					return;
				}
			}
			else if (this.currentEvent.data.TryGetValue("axis", out obj))
			{
				InputAxisTypes inputAxisTypes = (InputAxisTypes)obj;
				Action action3;
				if (this.axisHandlers != null && this.axisHandlers.TryGetValue(inputAxisTypes, out action3))
				{
					action3();
				}
			}
		}

		// Token: 0x04005397 RID: 21399
		public string displayName;

		// Token: 0x04005398 RID: 21400
		public Action enterHandler;

		// Token: 0x04005399 RID: 21401
		public Action exitHandler;

		// Token: 0x0400539A RID: 21402
		public Dictionary<InputButtonTypes, Action> buttonHandlers;

		// Token: 0x0400539B RID: 21403
		public Dictionary<InputAxisTypes, Action> axisHandlers;

		// Token: 0x0400539C RID: 21404
		public Dictionary<string, Action> commandHandlers;

		// Token: 0x0400539E RID: 21406
		public NavigationContext parentContext;
	}
}
