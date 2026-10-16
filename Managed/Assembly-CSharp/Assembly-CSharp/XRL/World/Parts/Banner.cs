using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FFF RID: 4095
	[Serializable]
	public class Banner : IActivePart
	{
		// Token: 0x0600A32A RID: 41770 RVA: 0x003BA414 File Offset: 0x003B8614
		public Banner()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.MustBeUnderstood = false;
			this.WorksOnWearer = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600A32B RID: 41771 RVA: 0x003BA450 File Offset: 0x003B8650
		private Type effectType
		{
			get
			{
				if (this._effectType == null)
				{
					if (!this.Effect.Contains("."))
					{
						this._effectType = ModManager.ResolveType("XRL.World.Effects." + this.Effect, false, false, true);
					}
					else
					{
						this._effectType = ModManager.ResolveType(this.Effect, false, false, true);
					}
				}
				return this._effectType;
			}
		}

		// Token: 0x0600A32C RID: 41772 RVA: 0x003BA4B7 File Offset: 0x003B86B7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A32D RID: 41773 RVA: 0x003BA4BC File Offset: 0x003B86BC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == DroppedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID || ID == TakenEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A32E RID: 41774 RVA: 0x003BA520 File Offset: 0x003B8720
		public override bool HandleEvent(EndTurnEvent E)
		{
			if ((this.ParentObject.CurrentCell != null && this.Raised) || this.ParentObject.Equipped != null)
			{
				int checkInterval = this.CheckInterval;
				this.CheckInterval = checkInterval - 1;
				if (checkInterval > 0)
				{
					return base.HandleEvent(E);
				}
				this.CheckInterval = 5;
				if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 5, null, false, 0L, null))
				{
					Cell anyBasisCell = this.GetAnyBasisCell();
					if (anyBasisCell != null)
					{
						GameObject[] array = The.ActionManager.ActionQueue.GetArray();
						int i = 0;
						int num = array.Length;
						while (i < num)
						{
							if (array[i].IsValid() && array[i].IsMemberOfFaction(this.Faction) && array[i].HasLOSTo(anyBasisCell, true, false, false, true, null))
							{
								this.ApplyEffectTo(array[i]);
							}
							i++;
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A32F RID: 41775 RVA: 0x003BA60C File Offset: 0x003B880C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if ((this.ParentObject.CurrentCell != null && this.Raised) || this.ParentObject.Equipped != null)
			{
				E.AddTag("{{y|[{{g|raised}}]}}", -20);
			}
			else
			{
				E.AddTag("{{y|[{{K|furled}}]}}", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A330 RID: 41776 RVA: 0x003BA660 File Offset: 0x003B8860
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Description == null)
			{
				Effect effect = Activator.CreateInstance(this.effectType) as Effect;
				this.Description = string.Concat(new string[]
				{
					"Bestows the {{|",
					effect.DisplayName,
					"}} effect to ",
					XRL.World.Faction.GetFormattedName(this.Faction),
					" who can see this item."
				});
			}
			E.Postfix.AppendRules(this.Description);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A331 RID: 41777 RVA: 0x003BA6DF File Offset: 0x003B88DF
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Raised = true;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A332 RID: 41778 RVA: 0x003BA6EF File Offset: 0x003B88EF
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.Raised = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A333 RID: 41779 RVA: 0x003BA6FF File Offset: 0x003B88FF
		public override bool HandleEvent(DroppedEvent E)
		{
			this.Raised = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A334 RID: 41780 RVA: 0x003BA70F File Offset: 0x003B890F
		public override bool HandleEvent(TakenEvent E)
		{
			this.Raised = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A335 RID: 41781 RVA: 0x003BA720 File Offset: 0x003B8920
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.Raised && this.ParentObject.CurrentCell != null)
			{
				E.AddAction("Raise", "raise", "RaiseBanner", null, 'r', false, 10, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A336 RID: 41782 RVA: 0x003BA770 File Offset: 0x003B8970
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RaiseBanner")
			{
				E.Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_banner_raise", null);
				IComponent<GameObject>.XDidYToZ(E.Actor, "raise", this.ParentObject, null, null, null, null, E.Actor, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
				this.Raised = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A337 RID: 41783 RVA: 0x003BA7E8 File Offset: 0x003B89E8
		public void ApplyEffectTo(GameObject target)
		{
			Type effectType = this.effectType;
			if (target.HasEffect(effectType))
			{
				target.GetEffect(effectType).Duration = 7;
				return;
			}
			Effect effect = Activator.CreateInstance(effectType) as Effect;
			effect.Duration = 7;
			target.ApplyEffect(effect, null);
		}

		// Token: 0x04004396 RID: 17302
		public bool Raised = true;

		// Token: 0x04004397 RID: 17303
		public string Effect;

		// Token: 0x04004398 RID: 17304
		public string Faction;

		// Token: 0x04004399 RID: 17305
		public string Description;

		// Token: 0x0400439A RID: 17306
		[NonSerialized]
		private Type _effectType;

		// Token: 0x0400439B RID: 17307
		[NonSerialized]
		private int CheckInterval;
	}
}
