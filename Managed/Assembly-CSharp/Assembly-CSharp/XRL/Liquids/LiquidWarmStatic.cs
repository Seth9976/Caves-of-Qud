using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.Liquids
{
	// Token: 0x02000563 RID: 1379
	[IsLiquid]
	[HasModSensitiveStaticCache]
	[HasWishCommand]
	[Serializable]
	public class LiquidWarmStatic : BaseLiquid
	{
		// Token: 0x06003BCD RID: 15309 RVA: 0x00123FD0 File Offset: 0x001221D0
		public LiquidWarmStatic()
			: base("warmstatic")
		{
			this.VaporTemperature = 2000;
			this.VaporObject = "GlitterGas";
			this.Combustibility = 30;
			this.PureElectricalConductivity = 100;
			this.MixedElectricalConductivity = 100;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 3;
			this.InterruptAutowalk = true;
			this.ConversionProduct = "acid::1;;algae::1;;blood::1;;brainbrine::1;;cider::1;;cloning::1;;convalessence::1;;gel::1;;goo::1;;honey::1;;ink::1;;lava::1;;neutronflux::1;;oil::1;;ooze::1;;proteangunk::1;;putrid::1;;salt::5;;sap::1;;slime::1;;sludge::1;;sunslag::1;;asphalt::1;;water::1;;wax::1;;wine::1;;";
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x0012404D File Offset: 0x0012224D
		public override List<string> GetColors()
		{
			return LiquidWarmStatic.Colors;
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x00124054 File Offset: 0x00122254
		public override string GetColor()
		{
			return "Y";
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x0012405B File Offset: 0x0012225B
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{Y|warm static}}";
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x00124062 File Offset: 0x00122262
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{Y|entropic}}";
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x00124069 File Offset: 0x00122269
		public override string GetWaterRitualName()
		{
			return "warm static";
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00124070 File Offset: 0x00122270
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{Y|entropic}}";
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x00124077 File Offset: 0x00122277
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{Y|entropic}}";
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x0012407E File Offset: 0x0012227E
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{Y|warm static}}";
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x00124088 File Offset: 0x00122288
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.IsPlayer())
			{
				GameManager.Instance.StaticEffecting = 2;
				SoundManager.PlayUISound("sfx_warmStaticSizzle", 1f, false, false, SoundRequest.SoundEffectType.None);
			}
			if (Liquid.IsPure())
			{
				if (Target.IsMutant() && 50.in100())
				{
					LiquidWarmStatic.GlitchMutations(Target);
					int num = Stat.Random(4, 12);
					Target.GetStat("MP").BaseValue += num;
					Target.ShowSuccess("You gained {{C|" + num.ToString() + "}} mutation points!", true);
				}
				else
				{
					LiquidWarmStatic.GlitchSkills(Target);
					int num2 = Stat.Random(400, 1200);
					Target.GetStat("SP").BaseValue += num2;
					Target.ShowSuccess("You gained {{C|" + num2.ToString() + "}} skill points!", true);
				}
			}
			else
			{
				LiquidWarmStatic.ApplyRandomEffectTo(Target, 0, true, true);
			}
			return true;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x00124178 File Offset: 0x00122378
		public override bool Douse(LiquidVolume Liquid, GameObject Actor, ref int PourAmount, ref bool RequestInterfaceExit)
		{
			return RequestInterfaceExit = true;
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0012418C File Offset: 0x0012238C
		private static bool TryGetValidEntry(BaseSkill Part, out SkillEntry Skill, out PowerEntry Power)
		{
			if (SkillFactory.Factory.SkillByClass.TryGetValue(Part.Name, out Skill))
			{
				Power = null;
				return Skill.Cost > 0;
			}
			return SkillFactory.Factory.PowersByClass.TryGetValue(Part.Name, out Power) && Power.ParentSkill != null && Power.Cost > 0;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x001241F0 File Offset: 0x001223F0
		private static bool GlitchSkills(GameObject Object)
		{
			Skills part = Object.GetPart<Skills>();
			if (part == null)
			{
				return false;
			}
			List<BaseSkill> partsDescendedFrom = Object.GetPartsDescendedFrom<BaseSkill>();
			if (partsDescendedFrom.IsNullOrEmpty<BaseSkill>())
			{
				return false;
			}
			bool flag = Object.IsPlayerControlled();
			StringBuilder stringBuilder = new StringBuilder("{{W|").Append(Object.Poss("mind starts to fluctuate in and out of coherence.", true)).Append("}}");
			Object.EmitMessage(stringBuilder.ToString(), null, null, flag);
			stringBuilder.Clear();
			List<int> list = partsDescendedFrom.Select<BaseSkill, int>((BaseSkill x) => 2).ToList<int>();
			int i = 0;
			int count = partsDescendedFrom.Count;
			while (i < count)
			{
				BaseSkill baseSkill = partsDescendedFrom[i];
				SkillEntry skillEntry;
				PowerEntry powerEntry;
				if (!LiquidWarmStatic.TryGetValidEntry(baseSkill, out skillEntry, out powerEntry))
				{
					list[i] = 0;
				}
				else
				{
					int j = 100;
					while (j > 0)
					{
						string name = baseSkill.Name;
						if (skillEntry != null)
						{
							SkillEntry randomElement = SkillFactory.GetSkills().GetRandomElement<SkillEntry>(null);
							if (randomElement.Cost > 0 && !randomElement.ExcludeFromPool)
							{
								name = randomElement.Class;
								goto IL_0152;
							}
						}
						else
						{
							if (powerEntry == null)
							{
								goto IL_0152;
							}
							PowerEntry randomElement2 = SkillFactory.GetPowers().GetRandomElement<PowerEntry>(null);
							if (randomElement2.Cost > 0 && !randomElement2.ExcludeFromPool)
							{
								name = randomElement2.Class;
								goto IL_0152;
							}
						}
						IL_0208:
						j--;
						continue;
						IL_0152:
						int num = partsDescendedFrom.FindIndex((BaseSkill x) => x.Name == name);
						if (num == -1)
						{
							num = partsDescendedFrom.Count;
							partsDescendedFrom.Add(ModManager.CreateInstance<BaseSkill>("XRL.World.Parts.Skill." + name));
							list.Add(1);
						}
						else
						{
							if (list[num] != 2)
							{
								goto IL_0208;
							}
							list[num] = 0;
						}
						if (flag && i != num)
						{
							stringBuilder.Compound(Object.Poss("knowledge of {{rules|", true), '\n').Append(partsDescendedFrom[i].DisplayName).Append("}} distorts into knowledge of {{rules|")
								.Append(partsDescendedFrom[num].DisplayName)
								.Append("}}.");
							break;
						}
						break;
					}
				}
				i++;
			}
			if (flag)
			{
				Object.EmitMessage(stringBuilder.ToString(), null, null, true);
				if (Object.IsPlayer())
				{
					JournalAPI.AddAccomplishment("You drank the raw entropy of the universe and your mind fluctuated in and out of coherence.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
				}
			}
			int k = 0;
			int count2 = partsDescendedFrom.Count;
			while (k < count2)
			{
				if (list[k] == 1)
				{
					part.AddSkill(partsDescendedFrom[k], null, null);
				}
				else if (list[k] == 2)
				{
					part.RemoveSkill(partsDescendedFrom[k]);
					Object.RemovePart(partsDescendedFrom[k]);
				}
				k++;
			}
			return true;
		}

		// Token: 0x06003BDA RID: 15322 RVA: 0x001244B8 File Offset: 0x001226B8
		private static bool GlitchMutations(GameObject Object)
		{
			Mutations part = Object.GetPart<Mutations>();
			if (part == null)
			{
				return false;
			}
			List<BaseMutation> list = new List<BaseMutation>(part.MutationList);
			list.RemoveAll((BaseMutation x) => x.BaseLevel <= 0);
			if (list.IsNullOrEmpty<BaseMutation>())
			{
				return false;
			}
			List<MutationEntry> list2 = new List<MutationEntry>();
			List<MutationEntry> list3 = new List<MutationEntry>(list.Count);
			for (int m = 0; m < list.Count; m++)
			{
				MutationEntry mutationEntry;
				if (!MutationFactory.TryGetMutationEntry(list[m], out mutationEntry))
				{
					list.RemoveAt(m--);
				}
				else if (mutationEntry.Category == MutationFactory.Morphotypes)
				{
					list.RemoveAt(m--);
					list2.Add(mutationEntry);
				}
				else
				{
					list3.Add(mutationEntry);
				}
			}
			bool flag = Object.IsPlayerControlled();
			StringBuilder stringBuilder = new StringBuilder("{{W|").Append(Object.Poss("genome fluctuates and genes start turning on and off at random.", true)).Append("}}");
			Object.EmitMessage(stringBuilder.ToString(), null, null, flag);
			stringBuilder.Clear();
			List<MutationEntry> list4 = new List<MutationEntry>();
			List<BaseMutation> list5 = new List<BaseMutation>(list);
			List<int> list6 = list.Select<BaseMutation, int>((BaseMutation x) => x.BaseLevel).ToList<int>();
			List<int> rapids = list.Select<BaseMutation, int>((BaseMutation x) => x.GetRapidLevelAmount()).ToList<int>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				BaseMutation baseMutation = list[i];
				MutationEntry entry = list3[i];
				bool defect = entry.IsDefect();
				bool single = entry.Cost <= 1;
				list4.Clear();
				list4.AddRange(from x in MutationFactory.GetCategories().Where<MutationCategory>(delegate(MutationCategory x)
					{
						if (rapids[i] <= 0)
						{
							return x != MutationFactory.Morphotypes;
						}
						return x == entry.Category;
					}).SelectMany<MutationCategory, MutationEntry>((MutationCategory x) => x.Entries)
					where !x.ExcludeFromPool && defect == x.IsDefect() && single == x.Cost <= 1
					select x);
				for (int j = 100; j > 0; j--)
				{
					MutationEntry mutation = list4.GetRandomElement<MutationEntry>(null);
					if (!list2.Contains(mutation) && !list2.Exists((MutationEntry x) => !x.OkWith(mutation, true, true)))
					{
						int num = list.FindIndex((BaseMutation x) => x.Name == mutation.Class);
						BaseMutation baseMutation2 = ((num < 0) ? mutation.CreateInstance() : list[num]);
						if (baseMutation2.CanLevel() == baseMutation.CanLevel())
						{
							list5[i] = baseMutation2;
							list6[i] = list[i].BaseLevel;
							rapids[i] = list[i].GetRapidLevelAmount();
							list2.Add(mutation);
							if (flag && list[i] != list5[i])
							{
								stringBuilder.Compound(Object.Poss("mutation {{rules|", true), '\n').Append(list[i].GetDisplayName(true)).Append("}} transmutes into the mutation {{rules|")
									.Append(list5[i].GetDisplayName(true))
									.Append("}}.");
								break;
							}
							break;
						}
					}
				}
				int i2 = i;
				i = i2 + 1;
			}
			if (flag)
			{
				Object.EmitMessage(stringBuilder.ToString(), null, null, true);
				if (Object.IsPlayer())
				{
					JournalAPI.AddAccomplishment("You drank the raw entropy of the universe and your genome fluctuated in and out of coherence.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
				}
			}
			int k = 0;
			int count2 = list.Count;
			while (k < count2)
			{
				if (!list5.Contains(list[k]))
				{
					list[k].SetRapidLevelAmount(0, false);
					part.RemoveMutation(list[k], false);
				}
				k++;
			}
			int l = 0;
			int count3 = list.Count;
			while (l < count3)
			{
				if (list5[l].ParentObject != null)
				{
					list5[l].BaseLevel = list6[l];
				}
				else
				{
					part.AddMutation(list5[l], list6[l], false);
				}
				list5[l].SetRapidLevelAmount(rapids[l], false);
				l++;
			}
			Object.SyncMutationLevelAndGlimmer();
			return true;
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x001249E0 File Offset: 0x00122BE0
		public static bool GlitchObject(GameObject Object)
		{
			if (!GameObject.Validate(Object))
			{
				return false;
			}
			if (Object.IsOpenLiquidVolume())
			{
				return LiquidWarmStatic.GlitchLiquidComponents(Object, "RandomLiquid", 0, false);
			}
			bool flag = Object.IsPlayer();
			if (Transmutation.TransmuteObject(Object, null, null, null, null, "=subject.T= =verb:glitch= into =object.an=.", "Transmute", "sfx_statusEffect_spacetimeWeirdness", false, false))
			{
				if (flag)
				{
					Achievement.GLITCH_SELF.Unlock();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00124A44 File Offset: 0x00122C44
		public static bool GlitchLiquidComponents(GameObject Object, string LiquidTable = "RandomLiquid", int Consume = 0, bool Silent = false)
		{
			LiquidVolume liquidVolume = Object.LiquidVolume;
			if (liquidVolume == null)
			{
				return false;
			}
			GameObject gameObject = Object.Equipped ?? Object.InInventory;
			if (!Silent)
			{
				if (liquidVolume.IsOpenVolume())
				{
					liquidVolume.EmitMessage(Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " starts to glitch.", 'W', false, gameObject != null, false, null, null);
				}
				else
				{
					liquidVolume.EmitMessage("The liquid mixture inside " + Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " starts to glitch.", 'W', false, gameObject != null, false, null, null);
				}
			}
			if (liquidVolume.ComponentLiquids.Count > 1)
			{
				liquidVolume.ComponentLiquids.Remove("warmstatic");
				liquidVolume.NormalizeProportions();
			}
			liquidVolume.Volume -= Consume;
			List<string> list = new List<string>(liquidVolume.ComponentLiquids.Keys);
			int num = 0;
			int num2 = 0;
			int count = list.Count;
			while (num < count && num2 < 100)
			{
				PopulationResult populationResult = PopulationManager.GenerateOne(LiquidTable, null, null, null, null, null);
				string text = ((populationResult != null) ? populationResult.Blueprint : null);
				if (text != null && !list.Contains(text))
				{
					int num3 = liquidVolume.ComponentLiquids[list[num]];
					liquidVolume.ComponentLiquids.Remove(list[num]);
					liquidVolume.ComponentLiquids[text] = num3;
					list[num++] = text;
				}
				num2++;
			}
			liquidVolume.Update();
			return true;
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00124BDC File Offset: 0x00122DDC
		public override bool PourIntoCell(LiquidVolume Liquid, GameObject Pourer, Cell TargetCell, ref int PourAmount, bool CanPourOn, ref bool RequestInterfaceExit)
		{
			if (CanPourOn && Liquid.IsPure() && Pourer != null && Pourer.IsPlayer())
			{
				GameManager.Instance.StaticEffecting = 3;
				SoundManager.PlayUISound("sfx_warmStaticSizzle", 1f, false, false, SoundRequest.SoundEffectType.None);
				if (TargetCell.HasRealObject() && !TargetCell.IsEmptyAtRenderLayer(1))
				{
					int num = 0;
					using (List<GameObject>.Enumerator enumerator = TargetCell.GetObjectsViaEventList().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (LiquidWarmStatic.GlitchObject(enumerator.Current) && ++num >= PourAmount * 5)
							{
								break;
							}
						}
					}
					int num2 = (int)Math.Ceiling((double)((float)num / 5f));
					PourAmount -= num2;
					Liquid.Volume -= num2;
					if (Pourer.IsPlayer() && num > 0)
					{
						JournalAPI.AddAccomplishment("You poured the raw entropy of the universe onto things and caused them to glitch into other things.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
						Achievement.GLITCH_OBJECT.Unlock();
					}
				}
				else
				{
					PourAmount--;
					Liquid.Volume--;
					Popup.Show("{{W|The space around you starts to glitch.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					LiquidWarmStatic.GlitchZone(TargetCell.ParentZone);
					if (Pourer.IsPlayer())
					{
						JournalAPI.AddAccomplishment("You poured the raw entropy of the universe on the ground and caused the space around you to glitch.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
						Achievement.GLITCH_OBJECT.Unlock();
					}
				}
				RequestInterfaceExit = true;
				if (PourAmount <= 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00124D60 File Offset: 0x00122F60
		private static bool IsValidObject(GameObject Object)
		{
			return Object != null && Object.IsReal && Object.Render != null && Object.Render.Visible;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x00124D84 File Offset: 0x00122F84
		public static bool MoveToRandomIn(Zone Z, GameObject Object)
		{
			for (int i = 0; i < 10; i++)
			{
				Cell cell = Z.GetCell(Stat.Rnd.Next(Z.Width), Stat.Rnd.Next(Z.Height));
				if (cell != null && cell.IsPassable(Object, true) && Object.SystemMoveTo(cell, new int?(0), true, true, false, false))
				{
					return true;
				}
			}
			List<Cell> passableCells = Z.GetPassableCells(Object);
			return !passableCells.IsNullOrEmpty<Cell>() && Object.SystemMoveTo(passableCells.GetRandomElement<Cell>(null), new int?(0), true, true, false, false);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00124E14 File Offset: 0x00123014
		public static void GlitchZone(Zone Z)
		{
			GameManager.Instance.StaticEffecting = 4;
			SoundManager.PlayUISound("sfx_warmStaticSizzle", 1f, false, false, SoundRequest.SoundEffectType.None);
			List<GameObject> objects = Z.GetObjects(new Predicate<GameObject>(LiquidWarmStatic.IsValidObject));
			Stat.PushState("WarmStaticGlitchZone" + Z.ZoneID, false);
			try
			{
				foreach (GameObject gameObject in objects)
				{
					if (20.in100())
					{
						LiquidWarmStatic.MoveToRandomIn(Z, gameObject);
					}
					if (2.in100())
					{
						LiquidWarmStatic.GlitchObject(gameObject);
					}
				}
			}
			finally
			{
				Stat.PopState();
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x00124ED4 File Offset: 0x001230D4
		public static List<Type> RandomEffects
		{
			get
			{
				if (LiquidWarmStatic._RandomEffects == null)
				{
					LiquidWarmStatic._RandomEffects = ModManager.GetTypesAssignableFrom(typeof(ITierInitialized), false);
					LiquidWarmStatic._RandomEffects.RemoveAll((Type x) => !x.IsSubclassOf(typeof(Effect)) || x.IsAbstract);
					for (int i = LiquidWarmStatic._RandomEffects.Count - 1; i >= 0; i--)
					{
						RequiresMod customAttribute = LiquidWarmStatic._RandomEffects[i].GetCustomAttribute<RequiresMod>();
						if (customAttribute != null && (customAttribute.ID.IsNullOrEmpty() || ModManager.GetMod(customAttribute.ID) == null) && (customAttribute.WorkshopID <= 0UL || ModManager.GetMod(customAttribute.WorkshopID) == null))
						{
							LiquidWarmStatic._RandomEffects.RemoveAt(i);
						}
					}
				}
				return LiquidWarmStatic._RandomEffects;
			}
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x00124F98 File Offset: 0x00123198
		public static Effect GetFirstApplicableTo(GameObject Object)
		{
			int num = 0;
			Effect effect;
			do
			{
				effect = Activator.CreateInstance(LiquidWarmStatic.RandomEffects.GetRandomElement<Type>(null)) as Effect;
			}
			while (num++ < 100 && (effect == null || !effect.CanBeAppliedTo(Object)));
			return effect;
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x00124FD4 File Offset: 0x001231D4
		public static Effect ApplyRandomEffectTo(GameObject Object, int Tier = 0, bool EmitMessage = true, bool FromDialog = false)
		{
			if (EmitMessage)
			{
				Object.Physics.DidX("feel", "a bit glitchy", null, null, "W", null, null, false, false, null, null, false, false, false, FromDialog, false, null);
				if (Object.IsPlayer())
				{
					GameManager.Instance.StaticEffecting = 1;
					SoundManager.PlayUISound("sfx_warmStaticSizzle", 1f, false, false, SoundRequest.SoundEffectType.None);
				}
			}
			for (int i = 0; i < 10; i++)
			{
				Effect firstApplicableTo = LiquidWarmStatic.GetFirstApplicableTo(Object);
				if (firstApplicableTo == null)
				{
					return null;
				}
				((ITierInitialized)firstApplicableTo).Initialize((Tier > 0) ? Tier : Object.CurrentZone.NewTier);
				if (Object.ApplyEffect(firstApplicableTo, null))
				{
					return firstApplicableTo;
				}
			}
			return null;
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x00125074 File Offset: 0x00123274
		public override void SmearOn(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			if (Target.CurrentCell == null || !Target.IsCreature)
			{
				return;
			}
			if (Liquid.IsPure())
			{
				LiquidWarmStatic.GlitchObject(Target);
				return;
			}
			if (Target.GetStringProperty("WarmStaticSmearedIn", null) == Target.CurrentZone.ZoneID)
			{
				return;
			}
			LiquidWarmStatic.ApplyRandomEffectTo(Target, 0, true, false);
			Target.SetStringProperty("WarmStaticSmearedIn", Target.CurrentZone.ZoneID, false);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x001250E4 File Offset: 0x001232E4
		public override void MixedWith(LiquidVolume Liquid, LiquidVolume NewLiquid, int Amount, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy, ref bool ExitInterface)
		{
			if (Liquid.ParentObject != null && Liquid.IsOpenVolume())
			{
				return;
			}
			if (NewLiquid.ParentObject != null && NewLiquid.IsOpenVolume())
			{
				return;
			}
			if (Liquid.IsPure())
			{
				GameObject gameObject = Liquid.ParentObject ?? NewLiquid.ParentObject;
				if (gameObject == null)
				{
					return;
				}
				gameObject.ApplyEffect(new ContainedStaticGlitching(), null);
			}
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0012513C File Offset: 0x0012333C
		public override void BeforeRender(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Cell currentCell = Liquid.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					int num = Liquid.Amount("warmstatic");
					int num2;
					if (num >= 1000)
					{
						num2 = 3;
					}
					else if (num >= 500)
					{
						num2 = 2;
					}
					else
					{
						num2 = 1;
					}
					Zone parentZone = currentCell.ParentZone;
					if (parentZone == null)
					{
						return;
					}
					parentZone.AddLight(currentCell.X, currentCell.Y, num2, LightLevel.Light, false);
				}
			}
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x001251B1 File Offset: 0x001233B1
		public override void BeforeRenderSecondary(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Liquid.AddLight(1, LightLevel.Light, false);
			}
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x001251D0 File Offset: 0x001233D0
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&Y";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x0012520B File Offset: 0x0012340B
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^Y" + eRender.ColorString;
			}
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x0012522C File Offset: 0x0012342C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&y^Y";
			Liquid.ParentObject.Render.TileColor = "&y";
			Liquid.ParentObject.Render.DetailColor = "Y";
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x00125278 File Offset: 0x00123478
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&y";
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x0012529C File Offset: 0x0012349C
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^Y", "&y", "Y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			this.Animate(Liquid, render, (Liquid.Secondary == null) ? LiquidWarmStatic.MaskAnimationPureTiles : LiquidWarmStatic.MaskAnimationImpureTiles);
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.TileVariantColors("&Y^Y", "&Y", "Y");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^Y";
				render.TileColor = "&Y";
				render.DetailColor = "Y";
				if (num < 15 || num >= 30)
				{
				}
			}
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x00125378 File Offset: 0x00123578
		public void Animate(LiquidVolume Liquid, Render RenderPart, Dictionary<int, List<string>> FrameSets)
		{
			RenderPart.RenderString = "±";
			int lastPaintMask = Liquid.LastPaintMask;
			if (lastPaintMask < 0)
			{
				return;
			}
			List<string> list;
			if (!FrameSets.TryGetValue(lastPaintMask, out list))
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null).Append(this.GetPaintAtlas(Liquid)).Append(this.GetPaint(Liquid))
					.Append('-')
					.AppendMask(lastPaintMask, 8)
					.Append(this.GetPaintExtension(Liquid));
				list = (FrameSets[lastPaintMask] = new List<string>(4));
				int num = stringBuilder.IndexOf("deep_") + 5;
				for (int i = 0; i < 4; i++)
				{
					stringBuilder[num] = (char)(49 + i);
					list.Add(stringBuilder.ToString());
				}
			}
			int num2 = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 20;
			if (num2 < 5)
			{
				RenderPart.Tile = list[0];
				RenderPart.ColorString = "&Y^k";
				return;
			}
			if (num2 < 10)
			{
				RenderPart.Tile = list[1];
				RenderPart.ColorString = "&k^Y";
				return;
			}
			if (num2 < 15)
			{
				RenderPart.Tile = list[2];
				RenderPart.ColorString = "&Y^k";
				return;
			}
			RenderPart.Tile = list[3];
			RenderPart.ColorString = "&k^Y";
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x001254AC File Offset: 0x001236AC
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&Y";
			}
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x001254CC File Offset: 0x001236CC
		public override string GetPaint(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "deep_1";
			}
			return base.GetPaint(Liquid);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x001254E3 File Offset: 0x001236E3
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (!Liquid.IsWadingDepth())
			{
				return base.GetPaintAtlas(Liquid);
			}
			if (Liquid.Secondary != null)
			{
				return "Liquids/Static/Impure/";
			}
			return "Liquids/Static/Pure/";
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00125508 File Offset: 0x00123708
		public override int GetPaintGroup(LiquidVolume Liquid)
		{
			if (!Liquid.IsWadingDepth() || Liquid.Secondary != null)
			{
				return base.GetPaintGroup(Liquid);
			}
			if (this.PurePaintGroup == -1)
			{
				return this.PurePaintGroup = this.AllocatePaintGroup(Liquid);
			}
			return this.PurePaintGroup;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0012554D File Offset: 0x0012374D
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("chance", 5);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x0012555B File Offset: 0x0012375B
		public override float GetValuePerDram()
		{
			return 50f;
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x00125562 File Offset: 0x00123762
		public override float GetPureLiquidValueMultipler()
		{
			return 20f;
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00125569 File Offset: 0x00123769
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			return 95;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0012556D File Offset: 0x0012376D
		[WishCommand("warmplayer", null)]
		public static void WishTransmute()
		{
			LiquidWarmStatic.GlitchObject(The.Player);
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x0012557A File Offset: 0x0012377A
		[WishCommand("warmskills", null)]
		public static void WishWarmSkills()
		{
			LiquidWarmStatic.GlitchSkills(The.Player);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00125587 File Offset: 0x00123787
		[WishCommand("warmmutations", null)]
		public static void WishWarmMutations()
		{
			LiquidWarmStatic.GlitchMutations(The.Player);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00125594 File Offset: 0x00123794
		[WishCommand("warmeffect", null)]
		public static void WishWarmEffect()
		{
			Effect effect = LiquidWarmStatic.ApplyRandomEffectTo(The.Player, 0, true, false);
			The.Player.EmitMessage(((effect != null) ? effect.DisplayNameStripped : null) + " applied to " + The.Player.DisplayNameOnlyDirectAndStripped + ".", null, "K", false);
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x001255E8 File Offset: 0x001237E8
		[WishCommand("warmeffect", null)]
		public static void WishWarmEffectSpec(string Name)
		{
			Type type = LiquidWarmStatic.RandomEffects.FirstOrDefault<Type>((Type x) => x.Name.EqualsNoCase(Name));
			if (type == null)
			{
				return;
			}
			Effect effect = Activator.CreateInstance(type) as Effect;
			if (effect == null)
			{
				return;
			}
			((ITierInitialized)effect).Initialize(The.Player.CurrentZone.NewTier);
			GameObject gameObject = The.Player;
			for (int i = 0; i < 10; i++)
			{
				if (gameObject.ApplyEffect(effect, null))
				{
					The.Player.EmitMessage(effect.DisplayNameStripped + " applied to " + gameObject.DisplayNameOnlyDirectAndStripped + ".", null, "K", false);
					return;
				}
				gameObject = The.Player.Inventory.Objects.GetRandomElement<GameObject>(null);
			}
			The.Player.EmitMessage("No valid targets for " + effect.DisplayNameStripped + ".", null, "K", false);
		}

		// Token: 0x04001D82 RID: 7554
		public new const string ID = "warmstatic";

		// Token: 0x04001D83 RID: 7555
		public const int NUM_FRAMES = 4;

		// Token: 0x04001D84 RID: 7556
		public const string MSG_GLITCH_OBJECT = "=subject.T= =verb:glitch= into =object.an=.";

		// Token: 0x04001D85 RID: 7557
		[NonSerialized]
		public static List<string> Colors = new List<string>(3) { "Y", "y", "K" };

		// Token: 0x04001D86 RID: 7558
		[NonSerialized]
		public static Dictionary<int, List<string>> MaskAnimationPureTiles = new Dictionary<int, List<string>>();

		// Token: 0x04001D87 RID: 7559
		[NonSerialized]
		public static Dictionary<int, List<string>> MaskAnimationImpureTiles = new Dictionary<int, List<string>>();

		// Token: 0x04001D88 RID: 7560
		public int PurePaintGroup = -1;

		// Token: 0x04001D89 RID: 7561
		[ModSensitiveStaticCache(false)]
		public static List<Type> _RandomEffects;
	}
}
