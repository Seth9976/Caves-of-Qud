using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001523 RID: 5411
	public class NavigationController
	{
		// Token: 0x0600D29F RID: 53919 RVA: 0x004E3366 File Offset: 0x004E1566
		public static bool BeginDragWithContext(NavigationController.DragContext context, Action endDrag)
		{
			NavigationController.endDragDelegate = endDrag;
			NavigationController.dragPriorContext = NavigationController.instance.activeContext;
			NavigationController.dragContext = context;
			context.Activate(false);
			return true;
		}

		// Token: 0x0600D2A0 RID: 53920 RVA: 0x004E338C File Offset: 0x004E158C
		public static bool EndDragWithContext(NavigationContext context)
		{
			if (NavigationController.instance.activeContext == context)
			{
				NavigationContext navigationContext = NavigationController.dragPriorContext;
				if (navigationContext != null)
				{
					navigationContext.Activate(false);
				}
			}
			NavigationController.dragContext = null;
			NavigationController.endDragDelegate = null;
			NavigationController.dragPriorContext = null;
			return true;
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600D2A1 RID: 53921 RVA: 0x004E33C0 File Offset: 0x004E15C0
		public static Event currentEvent
		{
			get
			{
				return NavigationController.instance._currentEvent;
			}
		}

		// Token: 0x0600D2A2 RID: 53922 RVA: 0x004E33CC File Offset: 0x004E15CC
		public async Task SuspendContextWhile(Func<Task> taskCreator)
		{
			NavigationContext oldContext = this.activeContext;
			NavigationContext activeContext = this.activeContext;
			NavigationContext globalContext = ((activeContext != null) ? activeContext.parents.LastOrDefault<NavigationContext>() : null);
			NavigationContext navigationContext = globalContext;
			bool? globalContextDisabled = ((navigationContext != null) ? new bool?(navigationContext.disabled) : null);
			if (globalContext != null)
			{
				globalContext.disabled = true;
			}
			this.activeContext = this.suspensionContext;
			try
			{
				await taskCreator();
			}
			finally
			{
				this.activeContext = oldContext;
				if (globalContext != null)
				{
					globalContext.disabled = globalContextDisabled.GetValueOrDefault();
				}
			}
		}

		// Token: 0x0600D2A3 RID: 53923 RVA: 0x004E3418 File Offset: 0x004E1618
		public async Task<T> SuspendContextWhile<T>(Func<Task<T>> taskCreator)
		{
			NavigationContext oldContext = this.activeContext;
			NavigationContext activeContext = this.activeContext;
			NavigationContext globalContext = ((activeContext != null) ? activeContext.parents.LastOrDefault<NavigationContext>() : null);
			NavigationContext navigationContext = globalContext;
			bool? globalContextDisabled = ((navigationContext != null) ? new bool?(navigationContext.disabled) : null);
			if (globalContext != null)
			{
				globalContext.disabled = true;
			}
			this.activeContext = this.suspensionContext;
			T t;
			try
			{
				t = await taskCreator();
			}
			finally
			{
				this.activeContext = oldContext;
				if (globalContext != null)
				{
					globalContext.disabled = globalContextDisabled.GetValueOrDefault();
				}
			}
			return t;
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600D2A4 RID: 53924 RVA: 0x004E3463 File Offset: 0x004E1663
		// (set) Token: 0x0600D2A5 RID: 53925 RVA: 0x004E346C File Offset: 0x004E166C
		public NavigationContext activeContext
		{
			get
			{
				return this._activeContext;
			}
			set
			{
				if (this._activeContext != value)
				{
					FrameworkUnityController frameworkUnityController = FrameworkUnityController.instance;
					if (frameworkUnityController != null && frameworkUnityController.DebugContextActivity)
					{
						global::UnityEngine.Debug.LogError(string.Format("set activecontext {0} at {1}", value, new StackTrace().ToString()));
					}
					NavigationContext activeContext = this._activeContext;
					NavigationController.SuspensionNavigationContext suspensionNavigationContext = this.suspensionContext;
					if (this._fromContext == null)
					{
						this._fromContext = this._activeContext;
					}
					Dictionary<string, object> dictionary = new Dictionary<string, object>
					{
						{ "from", this._fromContext },
						{ "to", value },
						{
							"triggeringEvent",
							this.triggeringEvent ?? this._currentEvent
						}
					};
					this.FireEvent(Event.Type.Exit, dictionary);
					this._activeContext = value;
					this.FireEvent(Event.Type.Enter, dictionary);
					this._fromContext = null;
				}
			}
		}

		// Token: 0x0600D2A6 RID: 53926 RVA: 0x004E3534 File Offset: 0x004E1734
		public string EditorStatus()
		{
			if (this._activeContext == null)
			{
				return "<no active context>";
			}
			return this._activeContext.ToString();
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600D2A7 RID: 53927 RVA: 0x004E3550 File Offset: 0x004E1750
		public Event triggeringEvent
		{
			get
			{
				object obj = null;
				Event currentEvent = this._currentEvent;
				bool? flag;
				if (currentEvent == null)
				{
					flag = null;
				}
				else
				{
					Dictionary<string, object> data = currentEvent.data;
					flag = ((data != null) ? new bool?(data.TryGetValue("triggeringEvent", out obj)) : null);
				}
				bool? flag2 = flag;
				if (flag2.GetValueOrDefault())
				{
					return obj as Event;
				}
				return null;
			}
		}

		// Token: 0x0600D2A8 RID: 53928 RVA: 0x004E35AB File Offset: 0x004E17AB
		public Event FireEvent(Event.Type type, Dictionary<string, object> data = null)
		{
			return this.FireEvent(new Event
			{
				type = type,
				data = data
			});
		}

		// Token: 0x0600D2A9 RID: 53929 RVA: 0x004E35C8 File Offset: 0x004E17C8
		public Event FireEvent(Event e)
		{
			NavigationContext navigationContext = this._activeContext;
			Event currentEvent = this._currentEvent;
			this._currentEvent = e;
			try
			{
				while (navigationContext != null && e != null && e != null && !e.cancelled && e != null && !e.handled)
				{
					if (e.type == Event.Type.Enter)
					{
						navigationContext.OnEnter();
					}
					else if (e.type == Event.Type.Exit)
					{
						navigationContext.OnExit();
					}
					else
					{
						navigationContext.OnInput(e);
					}
					navigationContext = navigationContext.parentContext;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Exception during UI.Framework::FireEvent", ex, "game_exception");
			}
			finally
			{
				this._currentEvent = currentEvent;
			}
			return e;
		}

		// Token: 0x0600D2AA RID: 53930 RVA: 0x004E3678 File Offset: 0x004E1878
		public Event FireInputCommandEvent(string commandId, Dictionary<string, object> additionalData = null)
		{
			Dictionary<string, object> dictionary = additionalData ?? new Dictionary<string, object>();
			dictionary.Set("commandId", commandId);
			Event @event = this.FireEvent(Event.Type.Input, dictionary);
			if (@event.handled)
			{
				ControlManager.ConsumeCurrentInput();
			}
			return @event;
		}

		// Token: 0x0600D2AB RID: 53931 RVA: 0x004E36B4 File Offset: 0x004E18B4
		public Event FireInputButtonEvent(InputButtonTypes buttonType, Dictionary<string, object> additionalData = null)
		{
			Dictionary<string, object> dictionary = additionalData ?? new Dictionary<string, object>();
			dictionary.Set("button", buttonType);
			Event @event = this.FireEvent(Event.Type.Input, dictionary);
			if (@event.handled)
			{
				ControlManager.ConsumeCurrentInput();
			}
			return @event;
		}

		// Token: 0x0600D2AC RID: 53932 RVA: 0x004E36F4 File Offset: 0x004E18F4
		public Event FireInputAxisEvent(InputAxisTypes axisType, Dictionary<string, object> additionalData = null, int value = 0)
		{
			Dictionary<string, object> dictionary = additionalData ?? new Dictionary<string, object>();
			dictionary.Set("axis", axisType);
			Event @event = new Event
			{
				type = Event.Type.Input,
				axisValue = new int?(value),
				data = dictionary
			};
			return this.FireEvent(@event);
		}

		// Token: 0x0600D2AD RID: 53933 RVA: 0x004E3744 File Offset: 0x004E1944
		public static IEnumerable<MenuOption> GetMenuOptions()
		{
			HashSet<string> commandsSent = new HashSet<string>();
			for (NavigationContext context = NavigationController.instance.activeContext; context != null; context = context.parentContext)
			{
				if (context.menuOptionDescriptions != null)
				{
					foreach (MenuOption desc in context.menuOptionDescriptions)
					{
						if (!commandsSent.Contains(desc.InputCommand))
						{
							yield return desc;
						}
						commandsSent.Add(desc.InputCommand);
						desc = null;
					}
					List<MenuOption>.Enumerator enumerator = default(List<MenuOption>.Enumerator);
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600D2AE RID: 53934 RVA: 0x004E374D File Offset: 0x004E194D
		public static IEnumerable<MenuOption> GetMenuOptions(IEnumerable<MenuOption> defaultList)
		{
			Dictionary<string, MenuOption> menuOptions = NavigationController.GetMenuOptions().ToDictionary<MenuOption, string>((MenuOption option) => option.InputCommand);
			foreach (MenuOption option2 in defaultList)
			{
				if (menuOptions.ContainsKey(option2.InputCommand))
				{
					yield return menuOptions[option2.InputCommand];
					menuOptions.Remove(option2.InputCommand);
				}
				else
				{
					yield return option2;
				}
				option2 = null;
			}
			IEnumerator<MenuOption> enumerator = null;
			foreach (MenuOption menuOption in menuOptions.Values)
			{
				yield return menuOption;
			}
			Dictionary<string, MenuOption>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, MenuOption>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400538A RID: 21386
		public static NavigationController.DragContext dragContext = null;

		// Token: 0x0400538B RID: 21387
		public static NavigationContext dragPriorContext = null;

		// Token: 0x0400538C RID: 21388
		public static Action endDragDelegate = null;

		// Token: 0x0400538D RID: 21389
		public static NavigationController instance = new NavigationController();

		// Token: 0x0400538E RID: 21390
		public NavigationController.SuspensionNavigationContext suspensionContext = new NavigationController.SuspensionNavigationContext();

		// Token: 0x0400538F RID: 21391
		private Event _currentEvent;

		// Token: 0x04005390 RID: 21392
		private NavigationContext _activeContext;

		// Token: 0x04005391 RID: 21393
		private NavigationContext _fromContext;

		// Token: 0x02001E2C RID: 7724
		public class DragContext : NavigationContext
		{
			// Token: 0x0600F65F RID: 63071 RVA: 0x00559B1D File Offset: 0x00557D1D
			public DragContext(Action A)
			{
				this.EndDragDelegate = A;
				if (this.commandHandlers == null)
				{
					this.commandHandlers = new Dictionary<string, Action>();
				}
				this.commandHandlers.Set("Cancel", delegate
				{
					if (this.EndDragDelegate != null)
					{
						this.EndDragDelegate();
					}
					NavigationController.currentEvent.Cancel();
				});
			}

			// Token: 0x040075BD RID: 30141
			public Action EndDragDelegate;
		}

		// Token: 0x02001E2D RID: 7725
		public class SuspensionNavigationContext : NavigationContext
		{
			// Token: 0x0600F661 RID: 63073 RVA: 0x00559B7A File Offset: 0x00557D7A
			public SuspensionNavigationContext()
				: base("suspension context")
			{
			}
		}
	}
}
