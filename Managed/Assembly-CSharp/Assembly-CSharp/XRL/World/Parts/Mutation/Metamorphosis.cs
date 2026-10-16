using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144D RID: 5197
	[Serializable]
	public class Metamorphosis : BaseMutation
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600CB08 RID: 51976 RVA: 0x0049CC38 File Offset: 0x0049AE38
		public Brain pBrain
		{
			get
			{
				if (this._pBrain == null)
				{
					this._pBrain = this.ParentObject.Brain;
				}
				return this._pBrain;
			}
		}

		// Token: 0x0600CB0A RID: 51978 RVA: 0x0049CC64 File Offset: 0x0049AE64
		public static void ClearInventory(GameObject go)
		{
			Inventory inventory = go.Inventory;
			if (inventory != null)
			{
				inventory.Clear();
			}
			Body body = go.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.GetParts())
				{
					try
					{
						if (bodyPart.Equipped != null)
						{
							GameObject equipped = bodyPart.Equipped;
							if (!bodyPart.Equipped.IsNatural())
							{
								bodyPart.Equipped.Obliterate(null, false, null);
							}
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("metamorphosis0", ex, "game_exception");
					}
				}
			}
		}

		// Token: 0x0600CB0B RID: 51979 RVA: 0x0049CD18 File Offset: 0x0049AF18
		public static void TransferInventory(GameObject source, GameObject dest, bool bTagLastEquipped = true)
		{
			Inventory inventory = source.Inventory;
			Inventory inventory2 = dest.Inventory;
			while (inventory.Objects.Count > 0)
			{
				GameObject gameObject = inventory.Objects[0];
				inventory.RemoveObject(gameObject, null);
				inventory2.AddObject(gameObject, null, false, false, true, null);
			}
			Body body = source.Body;
			Body body2 = dest.Body;
			List<GameObject> list = new List<GameObject>();
			List<BodyPart> list2 = new List<BodyPart>();
			foreach (BodyPart bodyPart in body.GetParts())
			{
				try
				{
					if (bodyPart.Equipped != null)
					{
						GameObject equipped = bodyPart.Equipped;
						if (!bodyPart.Equipped.IsNatural())
						{
							if (bTagLastEquipped)
							{
								bodyPart.Equipped.SetStringProperty("MetamorphLastEquipped", bodyPart.Name, false);
							}
							source.FireEvent(Event.New("CommandUnequipObject", "BodyPart", bodyPart, "SemiForced", 1));
							if (bodyPart.Equipped == null)
							{
								if (!list.Contains(equipped))
								{
									list.Add(equipped);
									list2.Add(bodyPart);
								}
							}
							else if (!bodyPart.Equipped.IsNatural())
							{
								list.Add(bodyPart.Equipped);
								list2.Add(bodyPart);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("metamorphosis0", ex, "game_exception");
				}
			}
			inventory.Objects.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				try
				{
					if (list[i].HasStringProperty("MetamorphLastEquipped"))
					{
						BodyPart partByName = body2.GetPartByName(list[i].GetStringProperty("MetamorphLastEquipped", null));
						if (partByName != null && partByName.Equipped != null && partByName.Equipped.DisplayName == list[i].DisplayName)
						{
							partByName._Equipped = list[i];
							list[i].Physics._Equipped = list[i];
							goto IL_02DF;
						}
						if (partByName != null && partByName.Equipped == null)
						{
							dest.FireEvent(Event.New("CommandEquipObject", "Object", list[i], "BodyPart", partByName));
							if (partByName.Equipped == list[i])
							{
								goto IL_02DF;
							}
						}
					}
					List<BodyPart> unequippedPart = body2.GetUnequippedPart(list2[i].Type);
					if ((unequippedPart.Count == 0 || !dest.FireEvent(Event.New("CommandEquipObject", "Object", list[i], "BodyPart", unequippedPart[0])) || unequippedPart[0].Equipped != list[i]) && !inventory2.Objects.Contains(list[i]))
					{
						inventory2.AddObject(list[i], null, false, false, true, null);
					}
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("metamorphosis1", ex2, "game_exception");
				}
				IL_02DF:;
			}
			List<GameObject> list3 = new List<GameObject>(inventory2.Objects);
			for (int j = 0; j < list3.Count; j++)
			{
				try
				{
					if (list3[j].HasStringProperty("MetamorphLastEquipped"))
					{
						BodyPart partByName2 = body2.GetPartByName(list3[j].GetStringProperty("MetamorphLastEquipped", null));
						if (partByName2 != null && partByName2.Equipped == null)
						{
							dest.FireEvent(Event.New("CommandEquipObject", "Object", list3[j], "BodyPart", partByName2));
						}
						if (!bTagLastEquipped)
						{
							list3[j].Property.Remove("MetamorphLastEquipped");
						}
					}
				}
				catch (Exception ex3)
				{
					MetricsManager.LogException("metamorphosis2", ex3, "game_exception");
				}
			}
		}

		// Token: 0x0600CB0C RID: 51980 RVA: 0x0049D13C File Offset: 0x0049B33C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandMetamorphosis");
		}

		// Token: 0x0600CB0D RID: 51981 RVA: 0x0049D149 File Offset: 0x0049B349
		public override string GetDescription()
		{
			return "You assume the form of any creature you touch.";
		}

		// Token: 0x0600CB0E RID: 51982 RVA: 0x0049D150 File Offset: 0x0049B350
		public override string GetLevelText(int Level)
		{
			return "May only assume the form of creatures level " + this.GetMaxLevel(Level).ToString() + " or lower.\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600CB0F RID: 51983 RVA: 0x0049D199 File Offset: 0x0049B399
		public int GetCooldown(int Level)
		{
			return 525 - 25 * Level;
		}

		// Token: 0x0600CB10 RID: 51984 RVA: 0x0049D1A5 File Offset: 0x0049B3A5
		public int GetMaxLevel(int Level)
		{
			return (1 + Level) * 5;
		}

		// Token: 0x0600CB11 RID: 51985 RVA: 0x0049D1AC File Offset: 0x0049B3AC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("MaxLevel", this.GetMaxLevel(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600CB12 RID: 51986 RVA: 0x0049D1FC File Offset: 0x0049B3FC
		public static void TransferMental(GameObject Source, GameObject Target)
		{
			if (Source.IsPlayer())
			{
				Target.RemovePart<ConversationScript>();
			}
			Target.Statistics["Intelligence"] = new Statistic(Source.Statistics["Intelligence"]);
			Target.Statistics["Willpower"] = new Statistic(Source.Statistics["Willpower"]);
			Target.Statistics["Ego"] = new Statistic(Source.Statistics["Ego"]);
		}

		// Token: 0x0600CB13 RID: 51987 RVA: 0x0049D288 File Offset: 0x0049B488
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandMetamorphosis")
			{
				Cell cell = base.PickDirection(true, "Metamorphosis", null);
				if (cell != null)
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPart("Combat"))
					{
						if (gameObject != this.ParentObject)
						{
							GameObject gameObject2 = gameObject.DeepCopy(false, false, null);
							gameObject2.RemovePart<GivesRep>();
							gameObject2.Inventory.Objects.Clear();
							if (gameObject2.ApplyEffect(new Metamorphed(this.ParentObject, 1), null))
							{
								GameObject player = The.Player;
								Metamorphosis.ClearInventory(gameObject2);
								Metamorphosis.TransferMental(player, gameObject2);
								Metamorphosis.TransferInventory(player, gameObject2, true);
								base.DidX("assume", "the form of " + gameObject.a + gameObject.ShortDisplayName, null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
								XRLCore.Core.Game.ActionManager.RemoveActiveObject(player);
								XRLCore.Core.Game.ActionManager.AddActiveObject(gameObject2);
								Cell currentCell = player.Physics.CurrentCell;
								currentCell.RemoveObject(player, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
								currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								player.MakeInactive();
								gameObject2.MakeActive(false);
								XRLCore.Core.Game.Player.Body = gameObject2;
							}
							gameObject2.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							base.UseEnergy(1000, "Physical Mutation");
							base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
							return true;
						}
						if (this.ParentObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("That creature is of too high a level to duplicate!", 'K', true);
						}
					}
					return true;
				}
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CB14 RID: 51988 RVA: 0x0049D4B4 File Offset: 0x0049B6B4
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB15 RID: 51989 RVA: 0x0049D4C0 File Offset: 0x0049B6C0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Metamorphosis", "CommandMetamorphosis", "Physical Mutations", null, "\u0001", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB16 RID: 51990 RVA: 0x0049D509 File Offset: 0x0049B709
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004F06 RID: 20230
		public GameObject Target;

		// Token: 0x04004F07 RID: 20231
		public Brain _pBrain;
	}
}
