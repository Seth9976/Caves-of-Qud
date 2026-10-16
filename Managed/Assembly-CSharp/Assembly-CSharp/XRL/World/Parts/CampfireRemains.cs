using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001005 RID: 4101
	[Serializable]
	public class CampfireRemains : IPart
	{
		// Token: 0x0600A36D RID: 41837 RVA: 0x003BBAC0 File Offset: 0x003B9CC0
		public override bool SameAs(IPart p)
		{
			return !((p as CampfireRemains).Blueprint != this.Blueprint);
		}

		// Token: 0x0600A36E RID: 41838 RVA: 0x003BBADD File Offset: 0x003B9CDD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A36F RID: 41839 RVA: 0x003BBB14 File Offset: 0x003B9D14
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null) && Campfire.FindExtinguishingPool(this.ParentObject) == null)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !currentCell.IsSolidFor(E.Actor, false))
				{
					string propertyOrTag = this.ParentObject.GetPropertyOrTag("PointOfInterestKey", null);
					bool flag = true;
					string text = null;
					if (!string.IsNullOrEmpty(propertyOrTag))
					{
						PointOfInterest pointOfInterest = E.Find(propertyOrTag);
						if (pointOfInterest != null)
						{
							if (this.ParentObject.DistanceTo(E.Actor) < pointOfInterest.GetDistanceTo(E.Actor))
							{
								E.Remove(pointOfInterest);
								text = "nearest";
							}
							else
							{
								flag = false;
								pointOfInterest.Explanation = "nearest";
							}
						}
					}
					if (flag)
					{
						E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), text, propertyOrTag, null, null, null, 1, null, 0);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A370 RID: 41840 RVA: 0x003BBC0C File Offset: 0x003B9E0C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Light", "light", "LightCampfire", null, 'i', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A371 RID: 41841 RVA: 0x003BBC44 File Offset: 0x003B9E44
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "LightCampfire")
			{
				this.AttemptLight(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A372 RID: 41842 RVA: 0x003BBC6B File Offset: 0x003B9E6B
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600A373 RID: 41843 RVA: 0x003BBC6E File Offset: 0x003B9E6E
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			this.AttemptLight(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A374 RID: 41844 RVA: 0x003BBC83 File Offset: 0x003B9E83
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A375 RID: 41845 RVA: 0x003BBC86 File Offset: 0x003B9E86
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Burn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A376 RID: 41846 RVA: 0x003BBC9C File Offset: 0x003B9E9C
		public void Light()
		{
			bool flag = this.ParentObject.HasTagOrProperty("PlayerCampfire");
			GameObject gameObject = this.ParentObject.ReplaceWith(GameObject.Create(this.Blueprint));
			if (gameObject != null && flag)
			{
				gameObject.SetIntProperty("PlayerCampfire", 1, false);
				gameObject.SetStringProperty("PointOfInterestKey", "PlayerCampfire", false);
			}
		}

		// Token: 0x0600A377 RID: 41847 RVA: 0x003BBCF8 File Offset: 0x003B9EF8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Burn")
			{
				this.Light();
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A378 RID: 41848 RVA: 0x003BBD1C File Offset: 0x003B9F1C
		public void AttemptLight(GameObject who)
		{
			GameObject gameObject = Campfire.FindExtinguishingPool(this.ParentObject);
			if (gameObject != null)
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You cannot light ",
						this.ParentObject.the,
						this.ParentObject.ShortDisplayName,
						" while ",
						this.ParentObject.itis,
						" in ",
						gameObject.the,
						gameObject.ShortDisplayName,
						"."
					}), true, true, true);
				}
				return;
			}
			GameObject gameObject2 = GameObject.Create(this.Blueprint);
			IComponent<GameObject>.XDidY(who, "light", gameObject2.the + gameObject2.ShortDisplayName, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.Light();
		}

		// Token: 0x040043B6 RID: 17334
		public string Blueprint = "Campfire";
	}
}
