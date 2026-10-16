using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FF9 RID: 4089
	[Serializable]
	public class ArkCore : IPart
	{
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600A2EB RID: 41707 RVA: 0x003B8849 File Offset: 0x003B6A49
		public bool Opened
		{
			get
			{
				return The.Game.HasDelimitedGameState("EndExtra", ',', "OpenArk");
			}
		}

		// Token: 0x0600A2EC RID: 41708 RVA: 0x003B8864 File Offset: 0x003B6A64
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == TookDamageEvent.ID || ID == PooledEvent<DefendMeleeHitEvent>.ID || ID == PooledEvent<DefenderMissileHitEvent>.ID || ID == BeforeDestroyObjectEvent.ID || ID == LeavingCellEvent.ID;
		}

		// Token: 0x0600A2ED RID: 41709 RVA: 0x003B88C5 File Offset: 0x003B6AC5
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600A2EE RID: 41710 RVA: 0x003B88DD File Offset: 0x003B6ADD
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.TryOpen(E.Actor);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2EF RID: 41711 RVA: 0x003B8902 File Offset: 0x003B6B02
		public override bool HandleEvent(TookDamageEvent E)
		{
			this.ChimeAt(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F0 RID: 41712 RVA: 0x003B8917 File Offset: 0x003B6B17
		public override bool HandleEvent(DefendMeleeHitEvent E)
		{
			this.ChimeAt(E.Attacker);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F1 RID: 41713 RVA: 0x003B892C File Offset: 0x003B6B2C
		public override bool HandleEvent(DefenderMissileHitEvent E)
		{
			this.ChimeAt(E.Attacker);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F2 RID: 41714 RVA: 0x003B8941 File Offset: 0x003B6B41
		public override bool HandleEvent(LeavingCellEvent E)
		{
			this.ChimeAt(The.Player);
			this.StartEnd(true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F3 RID: 41715 RVA: 0x003B895C File Offset: 0x003B6B5C
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.ChimeAt(The.Player);
			this.StartEnd(true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F4 RID: 41716 RVA: 0x003B8978 File Offset: 0x003B6B78
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.Opened)
			{
				E.AddAction("Open", "open", "Open", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F5 RID: 41717 RVA: 0x003B89B7 File Offset: 0x003B6BB7
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Open" && this.TryOpen(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2F6 RID: 41718 RVA: 0x003B89E8 File Offset: 0x003B6BE8
		public bool TryOpen(GameObject Actor)
		{
			if (this.Opened)
			{
				return false;
			}
			if (this.ParentObject.CurrentZone.HasObjectWithTagOrProperty("CherubimLock"))
			{
				return Actor.ShowFailure("The protective force of the cherubim prevents you from opening the ark.");
			}
			string text = "OPEN ARK";
			if (Popup.AskString("Opening the ark will expose its nondeterministic core to the chamber's ambient normality and irrevocably damage Resheph. Continue?\n\nType " + text + " to confirm.", "", "Sounds/UI/ui_notification", null, text, text.Length, 0, false, true, new bool?(false)).ToUpper() == text)
			{
				this.StartEnd(false);
			}
			return true;
		}

		// Token: 0x0600A2F7 RID: 41719 RVA: 0x003B8A6D File Offset: 0x003B6C6D
		public void ChimeAt(GameObject Actor)
		{
			if (GameObject.Validate(Actor))
			{
				AIHelpBroadcastEvent.Send(this.ParentObject, Actor, null, "Cherubim", 80, 100f, HelpCause.Trespass);
			}
		}

		// Token: 0x0600A2F8 RID: 41720 RVA: 0x003B8A94 File Offset: 0x003B6C94
		public void StartEnd(bool Broken = false)
		{
			if (this.Opened)
			{
				return;
			}
			if (Broken)
			{
				Popup.Show("The ark was broken.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			The.Game.RequireSystem<ArkOpenedSystem>();
			if (NephalProperties.IsFoiled("Ehalcodon"))
			{
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			The.Game.TryAddDelimitedGameState("EndExtra", ',', "OpenArk");
			CombatJuice.cameraShake(0.5f, false);
			try
			{
				Zone parentZone = base.currentCell.ParentZone;
				SoundManager.PlayUISound("Sounds/Abilities/sfx_ability_electromagnetic_pulse", 1f, false, false, SoundRequest.SoundEffectType.None);
				foreach (GameObject gameObject in parentZone.FindObjects((GameObject o) => o.GetBlueprint(true).DescendsFrom("BaseTriumHologram")))
				{
					gameObject.Obliterate(null, false, null);
				}
				foreach (GameObject gameObject2 in parentZone.FindObjects((GameObject o) => o.GetBlueprint(true).DescendsFrom("BaseHologramProjector")))
				{
					gameObject2.ApplyEffect(new Broken(), null);
					gameObject2.SetIntProperty("NoRepair", 1, false);
				}
				The.Core.RenderDelay(500, true, true, false);
				SoundManager.PlayUISound("Sounds/Misc/sfx_spark", 1f, false, false, SoundRequest.SoundEffectType.None);
				CombatJuice.cameraShake(0.4f, false);
				foreach (GameObject gameObject3 in parentZone.FindObjectsWithPart("HologramMaterial"))
				{
					gameObject3.Render.ColorString = ((Stat.RandomCosmetic(1, 100) < 50) ? "&R" : "&r");
					gameObject3.GetPart<HologramMaterial>().ColorStrings = "&r,&R,&w,&r";
					gameObject3.GetPart<HologramMaterial>().DetailColors = "r,w,c,R";
					gameObject3.GetPart<HologramMaterial>().reset();
					if (Stat.RandomCosmetic(1, 100) < 50)
					{
						gameObject3.Sparksplatter();
						gameObject3.Obliterate(null, false, null);
					}
				}
				The.Core.RenderDelay(500, true, true, false);
				CombatJuice.cameraShake(0.3f, false);
				SoundManager.PlayUISound("Sounds/Grenade/sfx_grenade_highExplosive_explode", 1f, false, false, SoundRequest.SoundEffectType.None);
				foreach (GameObject gameObject4 in parentZone.FindObjectsWithPart("AnimatedWallGeneric"))
				{
					gameObject4.Render.ColorString = ((Stat.RandomCosmetic(1, 100) < 50) ? "&y^R" : "&y^r");
					gameObject4.Render.TileColor = ((Stat.RandomCosmetic(1, 100) < 50) ? "&y^R" : "&y^r");
					if (Stat.Random(1, 100) < 5)
					{
						gameObject4.Render.TileColor = "&y+^" + Crayons.GetRandomColorAll(null);
					}
					AnimatedWallGeneric part = gameObject4.GetPart<AnimatedWallGeneric>();
					part.FrameMS = Stat.Random(10, 50);
					part.RushingChance = Stat.Random(10, 20000);
					if (Stat.Random(1, 100) < 5)
					{
						gameObject4.CurrentCell.AddObject("Firestarter", null, null, null, null);
						gameObject4.Physics.Temperature += 5000;
					}
				}
				The.Core.RenderDelay(500, true, true, false);
				SoundManager.PlayUISound("Sounds/Abilities/sfx_ability_gas_breathe", 1f, false, false, SoundRequest.SoundEffectType.None);
				CombatJuice.cameraShake(0.4f, false);
				foreach (GameObject gameObject5 in parentZone.FindObjectsWithPart("ArkCore"))
				{
					Cell currentCell = gameObject5.CurrentCell;
					if (currentCell != null)
					{
						currentCell.AddObject("CryoGas1000", null, null, null, null);
					}
				}
				The.Core.RenderDelay(500, true, true, false);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ArkCore Open", ex, "game_exception");
			}
			if (!EndGame.CheckMarooned())
			{
				Popup.Show("The North Sheva superstrate evanesces, and the latches that prevent descent unlock.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}
	}
}
