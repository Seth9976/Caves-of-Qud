using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Parts.Mutation;
using XRL.World.Quests;

namespace XRL.World.Parts
{
	// Token: 0x02000EE0 RID: 3808
	[HasWishCommand]
	[Serializable]
	public class NephalProperties : IPart
	{
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06009856 RID: 38998 RVA: 0x003753F8 File Offset: 0x003735F8
		public static Dictionary<string, Type> Chords
		{
			get
			{
				if (NephalProperties._Chords == null)
				{
					NephalProperties._Chords = new Dictionary<string, Type>();
					foreach (Type type in ModManager.GetTypesAssignableFrom(typeof(INephalChord), false))
					{
						if (!type.IsAbstract)
						{
							INephalChord nephalChord = (INephalChord)Activator.CreateInstance(type, true);
							NephalProperties.Chords[nephalChord.Source] = type;
						}
					}
				}
				return NephalProperties._Chords;
			}
		}

		// Token: 0x06009857 RID: 38999 RVA: 0x0037548C File Offset: 0x0037368C
		public static bool AllPacified()
		{
			string[] nephilim = NephalProperties.Nephilim;
			for (int i = 0; i < nephilim.Length; i++)
			{
				if (!NephalProperties.IsPacified(nephilim[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009858 RID: 39000 RVA: 0x003754BC File Offset: 0x003736BC
		public static bool AnyPacified()
		{
			string[] nephilim = NephalProperties.Nephilim;
			for (int i = 0; i < nephilim.Length; i++)
			{
				if (NephalProperties.IsPacified(nephilim[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009859 RID: 39001 RVA: 0x003754EC File Offset: 0x003736EC
		public static bool AllDead()
		{
			string[] nephilim = NephalProperties.Nephilim;
			for (int i = 0; i < nephilim.Length; i++)
			{
				if (!NephalProperties.IsDead(nephilim[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600985A RID: 39002 RVA: 0x0037551C File Offset: 0x0037371C
		public static bool AnyDead()
		{
			string[] nephilim = NephalProperties.Nephilim;
			for (int i = 0; i < nephilim.Length; i++)
			{
				if (NephalProperties.IsDead(nephilim[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600985B RID: 39003 RVA: 0x0037554C File Offset: 0x0037374C
		public static bool AllFoiled()
		{
			string[] nephilim = NephalProperties.Nephilim;
			for (int i = 0; i < nephilim.Length; i++)
			{
				if (!NephalProperties.IsFoiled(nephilim[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x0037557A File Offset: 0x0037377A
		public static bool IsPacified(string Nephal)
		{
			return The.Game.HasDelimitedGameState(Nephal, ',', "Pacified") && !The.Game.HasDelimitedGameState(Nephal, ',', "Dead");
		}

		// Token: 0x0600985D RID: 39005 RVA: 0x003755A7 File Offset: 0x003737A7
		public static bool IsDead(string Nephal)
		{
			return The.Game.HasDelimitedGameState(Nephal, ',', "Dead");
		}

		// Token: 0x0600985E RID: 39006 RVA: 0x003755BB File Offset: 0x003737BB
		public static bool IsFoiled(string Nephal)
		{
			return The.Game.HasAnyDelimitedGameState(Nephal, ',', "Dead", "Pacified");
		}

		// Token: 0x0600985F RID: 39007 RVA: 0x003755D4 File Offset: 0x003737D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == TookDamageEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == AfterObjectCreatedEvent.ID || ID == ZoneActivatedEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009860 RID: 39008 RVA: 0x0037560C File Offset: 0x0037380C
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (!NephalProperties.Preloaded)
			{
				this.PreloadClips();
			}
			if (this.Phase < 2 && E.Object.GetHPPercent() <= this.PhaseHealth)
			{
				this.Phase = 2;
				this.ExecuteAction(this.PhaseAction);
				this.ParentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_enraged_big", 0.5f, 0f, false, 0f, 0.9f, 1f, int.MaxValue);
				this.ParentObject.PlayWorldSound("Sounds/Creatures/VO/sfx_creature_insect_generic_vo_idle", 0.5f, 0f, false, 0f, 0.75f, 1f, int.MaxValue);
				CombatJuice.cameraShake(2f, false);
				if (!this.PhaseMessage.IsNullOrEmpty())
				{
					char c = IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null);
					base.EmitMessage(GameText.VariableReplace(this.PhaseMessage, E.Object, E.Actor, false), c, false, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009861 RID: 39009 RVA: 0x0037570C File Offset: 0x0037390C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.Phase < 3)
			{
				this.Phase = 3;
				string baseDisplayNameStripped = this.ParentObject.BaseDisplayNameStripped;
				Popup.Show("A sphere of light in the chord of " + baseDisplayNameStripped + " radiates away.\n\nYou feel it absorbed elsewhere.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.CheckChords();
			}
			this.CheckAchievement();
			return base.HandleEvent(E);
		}

		// Token: 0x06009862 RID: 39010 RVA: 0x00375768 File Offset: 0x00373968
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.AbsorbChords(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009863 RID: 39011 RVA: 0x00375778 File Offset: 0x00373978
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.AbsorbChords(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009864 RID: 39012 RVA: 0x00375788 File Offset: 0x00373988
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.Phase = 3;
				DropOnDeath dropOnDeath;
				if (this.ParentObject.TryGetPart<DropOnDeath>(out dropOnDeath))
				{
					E.WantToRemove(dropOnDeath);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009865 RID: 39013 RVA: 0x003757C8 File Offset: 0x003739C8
		public void CheckAchievement()
		{
			int num = 0;
			int num2 = 0;
			foreach (string text in NephalProperties.Nephilim)
			{
				if (NephalProperties.IsDead(text))
				{
					num++;
				}
				else if (NephalProperties.IsPacified(text))
				{
					num2++;
				}
			}
			if (num >= 1)
			{
				Achievement.ALEPH.Unlock();
			}
			if (num >= 2)
			{
				Achievement.BET.Unlock();
			}
			if (num >= 3)
			{
				Achievement.GIMEL.Unlock();
			}
			if (num >= 4)
			{
				Achievement.DALET.Unlock();
			}
			if (num >= 5)
			{
				Achievement.HE.Unlock();
			}
			if (num >= 6)
			{
				Achievement.VAV.Unlock();
			}
			if (num >= 7)
			{
				Achievement.ZAYIN.Unlock();
			}
			if (num2 >= 7)
			{
				Achievement.PACIFY_ALL.Unlock();
			}
		}

		// Token: 0x06009866 RID: 39014 RVA: 0x00375880 File Offset: 0x00373A80
		public bool TryPacify()
		{
			if (!The.Game.TryAddDelimitedGameState(The.Speaker.Blueprint, ',', "Pacified"))
			{
				return false;
			}
			this.ParentObject.RequirePart<Calming>(false);
			this.ParentObject.StopFighting(false);
			AllegianceSet allegiance = this.ParentObject.Brain.Allegiance;
			allegiance.Hostile = false;
			allegiance.Calm = true;
			Spawner spawner;
			if (this.ParentObject.TryGetPart<Spawner>(out spawner))
			{
				spawner.CombatOnly = true;
				if (spawner.PassAttitudes)
				{
					foreach (GameObject gameObject in this.ParentObject.CurrentZone.IterateObjects())
					{
						SpawnVessel spawnVessel;
						if (gameObject.TryGetPart<SpawnVessel>(out spawnVessel) && spawnVessel.SpawnedBy == this.ParentObject)
						{
							gameObject.TakeDemeanor(this.ParentObject);
						}
						if (gameObject.Blueprint == spawner.SpawnCheckBlueprint)
						{
							gameObject.TakeDemeanor(this.ParentObject);
						}
					}
				}
			}
			IrisdualBeam irisdualBeam;
			if (The.Speaker.TryGetPart<IrisdualBeam>(out irisdualBeam))
			{
				irisdualBeam.Stop();
			}
			foreach (GameObject gameObject2 in this.ParentObject.CurrentZone.IterateObjects())
			{
				gameObject2.StopFighting(false);
			}
			The.Core.RenderBase(true, false);
			this.CheckAchievement();
			Popup.Show(The.Speaker.Does("slouch", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " in pacification and radiates a chord of light.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return true;
		}

		// Token: 0x06009867 RID: 39015 RVA: 0x00375A1C File Offset: 0x00373C1C
		public void CheckChords()
		{
			foreach (GameObject gameObject in this.ParentObject.CurrentZone.IterateObjects())
			{
				NephalProperties nephalProperties;
				if (gameObject.IsCombatObject() && gameObject != this.ParentObject && gameObject.TryGetPart<NephalProperties>(out nephalProperties))
				{
					nephalProperties.AbsorbChords(this.ParentObject);
				}
			}
		}

		// Token: 0x06009868 RID: 39016 RVA: 0x00375A7C File Offset: 0x00373C7C
		public void AbsorbChords(GameObject From = null)
		{
			if (From != null && From.Blueprint != this.ParentObject.Blueprint)
			{
				base.DidXToY("absorb", "a chord of", From, "light", null, null, null, this.ParentObject, null, false, false, false, false, true, null, null, null, false, false, false, false, false, null);
			}
			foreach (string text in NephalProperties.Nephilim)
			{
				if (NephalProperties.IsDead(text) && !(text == this.ParentObject.Blueprint) && !this.HasChord(text))
				{
					this.AddChord(text);
				}
			}
		}

		// Token: 0x06009869 RID: 39017 RVA: 0x00375B18 File Offset: 0x00373D18
		public bool HasChord(string Nephal)
		{
			foreach (IPart part in this.ParentObject.PartsList)
			{
				INephalChord nephalChord = part as INephalChord;
				if (nephalChord != null && nephalChord.Source == Nephal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600986A RID: 39018 RVA: 0x00375B88 File Offset: 0x00373D88
		public bool AddChord(string Nephal)
		{
			Type type;
			if (!NephalProperties.Chords.TryGetValue(Nephal, out type))
			{
				return false;
			}
			INephalChord nephalChord = (INephalChord)Activator.CreateInstance(type, true);
			this.ParentObject.AddPart<INephalChord>(nephalChord, true, false);
			return true;
		}

		// Token: 0x0600986B RID: 39019 RVA: 0x00375BC3 File Offset: 0x00373DC3
		public void PreloadClips()
		{
			SoundManager.PreloadClipSet("Sounds/StatusEffects/sfx_statusEffect_enraged_big");
			SoundManager.PreloadClipSet("Sounds/Creatures/VO/sfx_creature_insect_generic_vo_idle");
			NephalProperties.Preloaded = true;
		}

		// Token: 0x0600986C RID: 39020 RVA: 0x00375BE0 File Offset: 0x00373DE0
		public void ExecuteAction(string Action)
		{
			if (Action == "Split")
			{
				Statistic stat = this.ParentObject.GetStat("Hitpoints");
				int num = stat.Value / 2;
				stat.Penalty -= num;
				stat.BaseValue -= num;
				GameObject gameObject = this.ParentObject.DeepCopy(false, false, null);
				this.ParentObject.CurrentCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				WasReplicatedEvent.Send(this.ParentObject, this.ParentObject, gameObject, "NephalProperties", false);
				ReplicaCreatedEvent.Send(gameObject, this.ParentObject, this.ParentObject, "NephalProperties", false);
				return;
			}
			if (Action == "Beam")
			{
				Mutations part = this.ParentObject.GetPart<Mutations>();
				IrisdualBeam part2 = this.ParentObject.GetPart<IrisdualBeam>();
				part.LevelMutation(part2, 10);
				part2.TakeMyActivatedAbilityOffCooldown(part2.ActivatedAbilityID, null);
			}
		}

		// Token: 0x0600986D RID: 39021 RVA: 0x00375CD4 File Offset: 0x00373ED4
		[WishCommand("nephilim:chords", null)]
		public static void WishChords()
		{
			foreach (KeyValuePair<string, Type> keyValuePair in NephalProperties.Chords)
			{
				INephalChord nephalChord = (INephalChord)Activator.CreateInstance(keyValuePair.Value, true);
				The.Player.AddPart<INephalChord>(nephalChord, true, false);
			}
		}

		// Token: 0x0600986E RID: 39022 RVA: 0x00375D40 File Offset: 0x00373F40
		[WishCommand("nephilim:pacify", null)]
		public static void WishPacify()
		{
			ReclamationSystem system = The.Game.GetSystem<ReclamationSystem>();
			string text = "";
			if (system != null && system.Stage == 1)
			{
				text = system.Quest.StepsByID["Nephal"].Value;
			}
			foreach (string text2 in NephalProperties.Nephilim)
			{
				if (!(text2 == text))
				{
					The.Game.TryAddDelimitedGameState(text2, ',', "Pacified");
				}
			}
		}

		// Token: 0x0600986F RID: 39023 RVA: 0x00375DBC File Offset: 0x00373FBC
		[WishCommand("nephilim:dead", null)]
		public static void WishDead()
		{
			ReclamationSystem system = The.Game.GetSystem<ReclamationSystem>();
			string text = "";
			if (system != null && system.Stage == 1)
			{
				text = system.Quest.StepsByID["Nephal"].Value;
			}
			foreach (string text2 in NephalProperties.Nephilim)
			{
				if (!(text2 == text))
				{
					The.Game.TryAddDelimitedGameState(text2, ',', "Dead");
				}
			}
		}

		// Token: 0x04003F7A RID: 16250
		[NonSerialized]
		public static readonly string[] Nephilim = new string[] { "Agolgot", "Bethsaida", "Rermadon", "Qas", "Qon", "Shugruith", "Ehalcodon" };

		// Token: 0x04003F7B RID: 16251
		[NonSerialized]
		private static Dictionary<string, Type> _Chords;

		// Token: 0x04003F7C RID: 16252
		public int Phase = 1;

		// Token: 0x04003F7D RID: 16253
		public int PhaseHealth = 50;

		// Token: 0x04003F7E RID: 16254
		public string PhaseAction = "";

		// Token: 0x04003F7F RID: 16255
		public string PhaseMessage = "";

		// Token: 0x04003F80 RID: 16256
		[NonSerialized]
		private static bool Preloaded;
	}
}
