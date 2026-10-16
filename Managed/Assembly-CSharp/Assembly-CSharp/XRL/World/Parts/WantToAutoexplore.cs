using System;
using System.Reflection;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <summary>
	///     Will respond to <see cref="T:XRL.World.AutoexploreObjectEvent" /> with a request to be visited.
	///     <para>
	///     Set the <see cref="F:XRL.World.Parts.WantToAutoexplore.AdjacentAction" /> to control the <see cref="F:XRL.World.AutoexploreObjectEvent.Command" />
	///     which becomes a <see cref="F:XRL.World.InventoryActionEvent.Command" /> dispatched to your part when visited.
	///     </para>
	/// </summary>
	// Token: 0x02000F79 RID: 3961
	[Serializable]
	public class WantToAutoexplore : IPart
	{
		// Token: 0x06009DAB RID: 40363 RVA: 0x00391B04 File Offset: 0x0038FD04
		public override bool WantEvent(int ID, int cascade)
		{
			int? triggeredMinEvent = this.TriggeredMinEvent;
			if ((ID == triggeredMinEvent.GetValueOrDefault()) & (triggeredMinEvent != null))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID;
		}

		// Token: 0x06009DAC RID: 40364 RVA: 0x00391B4C File Offset: 0x0038FD4C
		public override void Register(GameObject GO, IEventRegistrar Registrar)
		{
			Type type = ModManager.ResolveType("XRL.World", this.TriggeredEvent, false, false, true);
			if (type != null)
			{
				PropertyInfo property = type.GetProperty("ID", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					this.TriggeredMinEvent = new int?((int)property.GetValue(null));
				}
			}
			if (this.TriggeredMinEvent == null && !this.TriggeredEvent.IsNullOrEmpty())
			{
				GO.RegisterPartEvent(this, this.TriggeredEvent);
			}
		}

		// Token: 0x06009DAD RID: 40365 RVA: 0x00391BBF File Offset: 0x0038FDBF
		public override bool FireEvent(Event E)
		{
			if (E.ID == this.TriggeredEvent)
			{
				this.ParentObject.RemovePart(this);
			}
			return base.FireEvent(E);
		}

		/// <summary>
		///     If the <see cref="F:XRL.World.AutoexploreObjectEvent.Command" /> of the event isn't already set,
		///     set it = <see cref="F:XRL.World.Parts.WantToAutoexplore.AdjacentAction" /> and set <see cref="F:XRL.World.AutoexploreObjectEvent.AllowRetry" /> to
		///     <see cref="F:XRL.World.Parts.WantToAutoexplore.AllowRetry" />.
		///     <para>
		///     When <see cref="F:XRL.World.AutoexploreObjectEvent.AutogetOnlyMode" /> is enabled, only works for "Autoget" and "CollectLiquid" actions.
		///     In order to prevent constantly doing the requested <see cref="F:XRL.World.Parts.WantToAutoexplore.AdjacentAction" />,
		///     checks <see cref="M:XRL.World.Capabilities.AutoAct.GetAutoexploreActionProperty(XRL.World.GameObject,System.String)" /> is less than or equal to 0.
		///     </para>
		///     This check has no general-exploration equivalent property to check so the part would need to be removed if AdjacentAction is null.
		/// </summary>
		// Token: 0x06009DAE RID: 40366 RVA: 0x00391BE8 File Offset: 0x0038FDE8
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!this.AdjacentAction.IsNullOrEmpty() && (E.Command == null || (this.Override && E.Command != this.AdjacentAction)) && (!E.AutogetOnlyMode || this.AdjacentAction == "Autoget" || this.AdjacentAction == "CollectLiquid") && (this.AllowRetry || AutoAct.GetAutoexploreActionProperty(this.ParentObject, this.AdjacentAction) <= 0))
			{
				E.Command = this.AdjacentAction;
			}
			return base.HandleEvent(E);
		}

		/// <summary>
		///     Controls the <see cref="F:XRL.World.AutoexploreObjectEvent.Command" /> setting.
		/// </summary>
		// Token: 0x0400412D RID: 16685
		public string AdjacentAction;

		/// <summary>
		///     Sets <see cref="F:XRL.World.AutoexploreObjectEvent.AllowRetry" />, which if true means that the command
		///     is allowed to be re-executed on the object; otherwise, autoexplore will only try a given command
		///     once per object.
		/// </summary>
		// Token: 0x0400412E RID: 16686
		public bool AllowRetry;

		/// <summary>
		///     If true, we will configure the <see cref="T:XRL.World.AutoexploreObjectEvent" /> to our values any time
		///     its <see cref="F:XRL.World.AutoexploreObjectEvent.Command" /> is not already the same as our
		///     <see cref="F:XRL.World.Parts.WantToAutoexplore.AdjacentAction" />, not just when it is null.
		/// </summary>
		// Token: 0x0400412F RID: 16687
		public bool Override;

		/// <summary>
		///     A text event name, or minevent class name, that when triggered should count as having
		///     already done the AdjacentAction.   Example if AdjacentAction is "Look" TriggeredEvent should be
		///     "AfterLookedAt".  Then if someone looks before autoexploring, this part will be removed.
		/// </summary>
		// Token: 0x04004130 RID: 16688
		public string TriggeredEvent;

		// Token: 0x04004131 RID: 16689
		[NonSerialized]
		private int? TriggeredMinEvent;
	}
}
