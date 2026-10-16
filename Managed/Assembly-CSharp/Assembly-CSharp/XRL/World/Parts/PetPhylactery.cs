using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x0200107B RID: 4219
	[Serializable]
	public class PetPhylactery : IPoweredPart
	{
		// Token: 0x0600A7DD RID: 42973 RVA: 0x003D56FF File Offset: 0x003D38FF
		public PetPhylactery()
		{
			this.ChargeUse = 1;
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
			this.WorksOnEquipper = true;
			this.MustBeUnderstood = true;
		}

		// Token: 0x0600A7DE RID: 42974 RVA: 0x003D5735 File Offset: 0x003D3935
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A7DF RID: 42975 RVA: 0x003D5738 File Offset: 0x003D3938
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveItemListEvent.ID || ID == AfterObjectCreatedEvent.ID || ID == PooledEvent<CheckExistenceSupportEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == OnDestroyObjectEvent.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600A7E0 RID: 42976 RVA: 0x003D57A4 File Offset: 0x003D39A4
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			if (this.wraith == null && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Add("ActivateTemplarPhylactery", 100, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E1 RID: 42977 RVA: 0x003D5800 File Offset: 0x003D3A00
		public override bool HandleEvent(CheckExistenceSupportEvent E)
		{
			return (E.Object != this.wraith || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A7E2 RID: 42978 RVA: 0x003D584C File Offset: 0x003D3A4C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.Despawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E3 RID: 42979 RVA: 0x003D585B File Offset: 0x003D3A5B
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.Despawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E4 RID: 42980 RVA: 0x003D586C File Offset: 0x003D3A6C
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E5 RID: 42981 RVA: 0x003D58B0 File Offset: 0x003D3AB0
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E6 RID: 42982 RVA: 0x003D58F4 File Offset: 0x003D3AF4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (GameObject.Validate(ref this.wraith) && base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Despawn();
			}
			GameObject objectContext = this.ParentObject.GetObjectContext();
			if (objectContext == null)
			{
				this.Despawn();
			}
			else if (!objectContext.IDMatch(this.spawnerID))
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E7 RID: 42983 RVA: 0x003D5970 File Offset: 0x003D3B70
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.GetObjectContext() != null)
			{
				if (GameObject.Validate(ref this.wraith))
				{
					E.AddAction("Deactivate", "deactivate", "DeactivateTemplarPhylactery", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Activate", "activate", "ActivateTemplarPhylactery", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E8 RID: 42984 RVA: 0x003D59E6 File Offset: 0x003D3BE6
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateTemplarPhylactery")
			{
				this.Spawn();
			}
			else if (E.Command == "DeactivateTemplarPhylactery")
			{
				this.Despawn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7E9 RID: 42985 RVA: 0x003D5A24 File Offset: 0x003D3C24
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (E.Context != "Sample" && E.ReplacementObject == null && this.wraithID == null)
			{
				GameObject gameObject = GameObject.Create(this.wraithBlueprint);
				gameObject.RequirePart<HologramMaterial>(false);
				gameObject.RequirePart<HologramInvulnerability>(false);
				gameObject.RequirePart<Unreplicable>(false);
				gameObject.ModIntProperty("IgnoresWalls", 1, false);
				foreach (GameObject gameObject2 in gameObject.GetInventoryAndEquipment())
				{
					if (gameObject2.HasPropertyOrTag("MeleeWeapon"))
					{
						gameObject2.AddPart<ModPsionic>(new ModPsionic(), true, false);
					}
					else
					{
						gameObject2.Obliterate(null, false, null);
					}
				}
				this.ParentObject.Render.DisplayName = "phylactery of " + gameObject.ShortDisplayNameWithoutTitles;
				if (The.ZoneManager != null)
				{
					this.wraithID = The.ZoneManager.CacheObject(gameObject, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7EA RID: 42986 RVA: 0x003D5B38 File Offset: 0x003D3D38
		public void Despawn()
		{
			if (GameObject.Validate(ref this.wraith))
			{
				this.wraith.Splatter("&M-");
				this.wraith.Splatter("&M.");
				this.wraith.Splatter("&M/");
				IComponent<GameObject>.XDidY(this.wraith, "disappear", null, null, null, null, null, null, false, false, null, null, true, false, false, false, false, null);
				this.wraith.Obliterate(null, false, null);
				this.wraith = null;
			}
		}

		// Token: 0x0600A7EB RID: 42987 RVA: 0x003D5BB8 File Offset: 0x003D3DB8
		public void Spawn()
		{
			this.Despawn();
			GameObject objectContext = this.ParentObject.GetObjectContext();
			if (objectContext == null)
			{
				return;
			}
			Cell cell = objectContext.GetCurrentCell();
			if (cell == null)
			{
				return;
			}
			cell = cell.GetEmptyAdjacentCells(1, 5).GetRandomElement<Cell>(null);
			if (cell == null)
			{
				return;
			}
			bool flag = false;
			if (!objectContext.IsPlayerControlled() && objectContext.HasProperty("PsychicHunter") && objectContext.HasPart<Extradimensional>())
			{
				flag = true;
			}
			this.spawnerID = objectContext.ID;
			this.wraith = The.ZoneManager.peekCachedObject(this.wraithID).DeepCopy(false, true, null);
			Temporary.AddHierarchically(this.wraith, 0, null, this.ParentObject, false);
			this.wraith.MakeActive(false);
			objectContext.PlayWorldOrUISound("Sounds/Interact/sfx_interact_phylactery_on", null);
			cell.AddObject(this.wraith, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.wraith.TeleportSwirl(null, "&B", true, null, 'ù', false);
			IComponent<GameObject>.XDidYToZ(objectContext, "activate", this.wraith, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			IComponent<GameObject>.XDidY(this.wraith, "appear", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.wraith.PartyLeader = objectContext;
			if (flag)
			{
				this.wraith.TakeAllegiance<AllySummon>(objectContext);
			}
		}

		// Token: 0x04004543 RID: 17731
		public string wraithBlueprint = "PetWraith";

		// Token: 0x04004544 RID: 17732
		public string wraithID;

		// Token: 0x04004545 RID: 17733
		public GameObject wraith;

		// Token: 0x04004546 RID: 17734
		public string spawnerID;
	}
}
