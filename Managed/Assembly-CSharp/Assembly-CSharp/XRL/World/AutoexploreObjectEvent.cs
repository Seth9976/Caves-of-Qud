using System;
using XRL.World.Capabilities;

namespace XRL.World
{
	/// <summary>
	///   Event fired during autoact sequence.  Two major modes <see cref="F:XRL.World.AutoexploreObjectEvent.AutogetOnlyMode" /> 
	///   is checked when walking around manually, is false when auto-explore is engaged.
	/// </summary>
	// Token: 0x02000681 RID: 1665
	[GameEvent(Cascade = 256, Cache = MinEvent.Cache.Singleton)]
	public class AutoexploreObjectEvent : IActOnItemEvent
	{
		// Token: 0x06004769 RID: 18281 RVA: 0x0016963C File Offset: 0x0016783C
		public AutoexploreObjectEvent()
		{
			this.ID = AutoexploreObjectEvent.ID;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0016964F File Offset: 0x0016784F
		public override int GetCascadeLevel()
		{
			return AutoexploreObjectEvent.CascadeLevel;
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00169656 File Offset: 0x00167856
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0016966A File Offset: 0x0016786A
		public override void Reset()
		{
			base.Reset();
			this.Setting = null;
			this.Action = null;
			this.Command = null;
			this.AllowRetry = false;
			this.AutogetOnlyMode = false;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00169698 File Offset: 0x00167898
		public static bool Check(GameObject Actor, GameObject Object, string Setting, OngoingAction Action)
		{
			if (AutoAct.AutoexploreSuppressed(Object))
			{
				return false;
			}
			AutoexploreObjectEvent.Instance.Actor = Actor;
			AutoexploreObjectEvent.Instance.Item = Object;
			AutoexploreObjectEvent.Instance.Setting = Setting;
			AutoexploreObjectEvent.Instance.Action = Action;
			AutoexploreObjectEvent.Instance.Command = null;
			AutoexploreObjectEvent.Instance.AllowRetry = false;
			AutoexploreObjectEvent.Instance.AutogetOnlyMode = false;
			if (Actor.HandleEvent(AutoexploreObjectEvent.Instance))
			{
				Object.HandleEvent(AutoexploreObjectEvent.Instance);
			}
			return AutoexploreObjectEvent.Instance.Command != null && (AutoexploreObjectEvent.Instance.AllowRetry || AutoAct.GetAutoexploreActionProperty(Object, AutoexploreObjectEvent.Instance.Command) <= 0);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x00169748 File Offset: 0x00167948
		public static bool CheckForAdjacent(GameObject Actor, GameObject Object, string Setting, OngoingAction Action, bool AutogetOnly = false)
		{
			string text;
			bool flag;
			return AutoexploreObjectEvent.CheckForAdjacent(out text, out flag, Actor, Object, Setting, Action, AutogetOnly);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00169764 File Offset: 0x00167964
		public static bool CheckForAdjacent(out string Command, out bool AllowRetry, GameObject Actor, GameObject Object, string Setting, OngoingAction Action, bool AutogetOnly = false)
		{
			if (AutoAct.AutoexploreSuppressed(Object))
			{
				Command = null;
				AllowRetry = false;
				return false;
			}
			AutoexploreObjectEvent.Instance.Actor = Actor;
			AutoexploreObjectEvent.Instance.Item = Object;
			AutoexploreObjectEvent.Instance.Setting = Setting;
			AutoexploreObjectEvent.Instance.Action = Action;
			AutoexploreObjectEvent.Instance.Command = null;
			AutoexploreObjectEvent.Instance.AllowRetry = false;
			AutoexploreObjectEvent.Instance.AutogetOnlyMode = AutogetOnly;
			if (Actor.HandleEvent(AutoexploreObjectEvent.Instance))
			{
				Object.HandleEvent(AutoexploreObjectEvent.Instance);
			}
			Command = AutoexploreObjectEvent.Instance.Command;
			AllowRetry = AutoexploreObjectEvent.Instance.AllowRetry;
			return Command != null && (AllowRetry || AutoAct.GetAutoexploreActionProperty(Object, Command) <= 0);
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x0016981C File Offset: 0x00167A1C
		public static string GetAdjacentAction(GameObject Actor, GameObject Object, string Setting, OngoingAction Action)
		{
			if (AutoAct.AutoexploreSuppressed(Object))
			{
				return null;
			}
			AutoexploreObjectEvent.Instance.Actor = Actor;
			AutoexploreObjectEvent.Instance.Item = Object;
			AutoexploreObjectEvent.Instance.Setting = Setting;
			AutoexploreObjectEvent.Instance.Action = Action;
			AutoexploreObjectEvent.Instance.AllowRetry = false;
			AutoexploreObjectEvent.Instance.Command = null;
			if (Actor.HandleEvent(AutoexploreObjectEvent.Instance))
			{
				Object.HandleEvent(AutoexploreObjectEvent.Instance);
			}
			if (AutoexploreObjectEvent.Instance.Command == null || (!AutoexploreObjectEvent.Instance.AllowRetry && AutoAct.GetAutoexploreActionProperty(Object, AutoexploreObjectEvent.Instance.Command) > 0))
			{
				return null;
			}
			return AutoexploreObjectEvent.Instance.Command;
		}

		// Token: 0x0400220E RID: 8718
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AutoexploreObjectEvent), null, null, null);

		// Token: 0x0400220F RID: 8719
		public new static readonly int CascadeLevel = 256;

		// Token: 0x04002210 RID: 8720
		public static readonly AutoexploreObjectEvent Instance = new AutoexploreObjectEvent();

		/// <summary>The autoact setting we are requesting feedback for.</summary>
		// Token: 0x04002211 RID: 8721
		public string Setting;

		/// <summary>The ongoing action attached to the autoact setting, if any</summary>
		// Token: 0x04002212 RID: 8722
		public OngoingAction Action;

		/// <summary><see cref="F:XRL.World.InventoryActionEvent.Command" /> we are requesting from the explorer.</summary>
		// Token: 0x04002213 RID: 8723
		public string Command;

		/// <summary>Should we be able to re-initiate this action multiple times from autoexplore?</summary>
		// Token: 0x04002214 RID: 8724
		public bool AllowRetry;

		/// <summary>If true, we should only respond with the "Autoget" class of actions.</summary>
		// Token: 0x04002215 RID: 8725
		public bool AutogetOnlyMode;
	}
}
