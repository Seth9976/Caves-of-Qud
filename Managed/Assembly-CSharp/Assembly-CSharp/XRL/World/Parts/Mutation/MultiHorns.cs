using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001421 RID: 5153
	[Serializable]
	public class MultiHorns : BaseMutation
	{
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600C8A1 RID: 51361 RVA: 0x00490EA4 File Offset: 0x0048F0A4
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::MultiHorns";
			}
		}

		// Token: 0x0600C8A2 RID: 51362 RVA: 0x00490EBB File Offset: 0x0048F0BB
		public override bool AffectsBodyParts()
		{
			return true;
		}

		// Token: 0x0600C8A3 RID: 51363 RVA: 0x00490EBE File Offset: 0x0048F0BE
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C8A4 RID: 51364 RVA: 0x00490EC1 File Offset: 0x0048F0C1
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteGameObjectList(this.Horns);
			base.Write(Basis, Writer);
		}

		// Token: 0x0600C8A5 RID: 51365 RVA: 0x00490ED7 File Offset: 0x0048F0D7
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Horns = new List<GameObject>();
			Reader.ReadGameObjectList(this.Horns, null);
			base.Read(Basis, Reader);
		}

		// Token: 0x0600C8A6 RID: 51366 RVA: 0x00490EF9 File Offset: 0x0048F0F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID;
		}

		// Token: 0x0600C8A7 RID: 51367 RVA: 0x00490F10 File Offset: 0x0048F110
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Charging <= 0 && E.Distance <= this.GetChargeDistance(base.Level) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, false, true, null))
			{
				E.Add("CommandMassiveCharge", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8A8 RID: 51368 RVA: 0x00490F86 File Offset: 0x0048F186
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandMassiveCharge");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C8A9 RID: 51369 RVA: 0x00490FA8 File Offset: 0x0048F1A8
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Distance", this.GetChargeDistance(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetChargeCooldown(Level), 0);
		}

		// Token: 0x0600C8AA RID: 51370 RVA: 0x00490FF6 File Offset: 0x0048F1F6
		private bool ValidChargeTarget(GameObject obj)
		{
			return obj != null && obj.IsCombat;
		}

		// Token: 0x0600C8AB RID: 51371 RVA: 0x00491004 File Offset: 0x0048F204
		public void PickChargeTarget()
		{
			if (this.Charging <= 0)
			{
				this.chargeCells = base.PickLine(this.GetChargeDistance(base.Level), AllowVis.OnlyVisible, new Predicate<GameObject>(this.ValidChargeTarget), false, false, true, false, null, null, "Wrecking Charge", false, true);
				if (this.chargeCells == null)
				{
					return;
				}
				if (this.chargeCells != null)
				{
					this.chargeCells = new List<Cell>(this.chargeCells);
				}
				if (this.chargeCells.FirstOrDefault<Cell>() != this.ParentObject.CurrentCell)
				{
					this.chargeCells.Insert(0, this.ParentObject.CurrentCell);
				}
				if (this.chargeCells.Count > 1 && this.chargeCells[0].ParentZone == this.chargeCells[1].ParentZone)
				{
					while (this.chargeCells.Count < this.GetChargeDistance(base.Level) + 1)
					{
						int num = 0;
						while (num < this.chargeCells.Count - 1 && this.chargeCells.Count < this.GetChargeDistance(base.Level) + 1 && this.chargeCells[num].ParentZone == this.chargeCells[num + 1].ParentZone)
						{
							string directionFromCell = this.chargeCells[num].GetDirectionFromCell(this.chargeCells[num + 1], false);
							this.chargeCells.Add(this.chargeCells[this.chargeCells.Count - 1].GetCellFromDirection(directionFromCell, false));
							num++;
						}
					}
				}
			}
		}

		// Token: 0x0600C8AC RID: 51372 RVA: 0x004911A2 File Offset: 0x0048F3A2
		public string pathDirectionAtStep(int n, List<Cell> path)
		{
			if (path.Count <= 1)
			{
				return ".";
			}
			n %= path.Count;
			return path[n].GetDirectionFromCell(path[(n + 1) % path.Count], false);
		}

		// Token: 0x0600C8AD RID: 51373 RVA: 0x004911DC File Offset: 0x0048F3DC
		public Cell cellAtStep(int n, List<Cell> path)
		{
			if (path.Count <= 1)
			{
				return path[0];
			}
			if (n < path.Count)
			{
				return path[n];
			}
			Cell cell = path[path.Count - 1];
			for (int i = path.Count; i <= n; i++)
			{
				cell = cell.GetCellFromDirection(this.pathDirectionAtStep(n, path), false);
			}
			return cell;
		}

		// Token: 0x0600C8AE RID: 51374 RVA: 0x0049123C File Offset: 0x0048F43C
		public void PerformCharge(List<Cell> ChargePath, bool DoEffect = true)
		{
			this.Charging = 0;
			Dictionary<GameObject, int> dictionary = new Dictionary<GameObject, int>();
			List<GameObject> list = new List<GameObject>();
			if (!ChargePath.Any<Cell>((Cell c) => c.IsVisible()))
			{
			}
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			base.DidX("charge", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			int num = this.ParentObject.Stat("Strength", 0) + base.Level * 2 - 2;
			int num2 = num;
			int num3 = (num - 5) * (num - 5);
			bool flag = this.ParentObject.HasPart<Robot>();
			for (int i = 0; i < ChargePath.Count; i++)
			{
				int num4 = i + 1 + list.Count;
				while (num4 != -1)
				{
					int num5 = num4;
					num4 = -1;
					Cell cell = this.cellAtStep(num5, ChargePath);
					if (flag)
					{
						GameObject firstObjectWithPropertyOrTag = cell.GetFirstObjectWithPropertyOrTag("RobotStop");
						if (firstObjectWithPropertyOrTag != null)
						{
							base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", firstObjectWithPropertyOrTag, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							goto IL_04B3;
						}
					}
					foreach (GameObject gameObject in from go in cell.GetWalls()
						where this.ParentObject.PhaseAndFlightMatches(go)
						select go)
					{
						foreach (GameObject gameObject2 in list)
						{
							if (!dictionary.ContainsKey(gameObject2))
							{
								dictionary.Add(gameObject2, 0);
							}
							Dictionary<GameObject, int> dictionary2 = dictionary;
							GameObject gameObject3 = gameObject2;
							int num6 = dictionary2[gameObject3];
							dictionary2[gameObject3] = num6 + 1;
						}
						if (IComponent<GameObject>.Visible(gameObject))
						{
							CombatJuice._cameraShake(0.25f);
						}
						if (num2 <= Stats.GetCombatAV(gameObject) + 20)
						{
							base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", gameObject, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							goto IL_04B3;
						}
						gameObject.Destroy(null, false, false, null);
					}
					foreach (GameObject gameObject4 in from go in cell.LoopObjectsWithPart("Combat")
						where this.ParentObject.PhaseAndFlightMatches(go)
						select go)
					{
						if (!list.Contains(gameObject4) && gameObject4 != this.ParentObject)
						{
							if (num <= gameObject4.Stat("Strength", 0) || !gameObject4.CanBeInvoluntarilyMoved())
							{
								base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", gameObject4, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
								goto IL_04B3;
							}
							list.Add(gameObject4);
							num4 = num5 + 1;
						}
						if (gameObject4.IsPlayer())
						{
							CombatJuice._cameraShake(0.25f);
						}
					}
					foreach (GameObject gameObject5 in from go in cell.LoopObjectsWithPart("Physics")
						where this.ParentObject.PhaseAndFlightMatches(go)
						select go)
					{
						if (gameObject5.Physics.Solid && !gameObject5.IsWall() && !gameObject5.IsCombat && !list.Contains(gameObject5) && gameObject5 != this.ParentObject)
						{
							if (num3 <= gameObject5.Weight)
							{
								base.DidXToY("are", "stopped in " + this.ParentObject.its + " tracks by", gameObject5, null, "!", null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
								goto IL_04B3;
							}
							list.Add(gameObject5);
							num4 = num5 + 1;
						}
					}
				}
				list.RemoveAll((GameObject O) => O.IsInvalid() || O.IsNowhere());
				for (int j = list.Count - 1; j >= 0; j--)
				{
					list[j].DirectMoveTo(this.cellAtStep(i + j + 1, ChargePath), 0, false, false, false, null);
				}
				this.ParentObject.DirectMoveTo(this.cellAtStep(i, ChargePath), 0, false, false, false, null);
				scrapBuffer.RenderBase();
				scrapBuffer.Draw();
				Thread.Sleep(10);
			}
			IL_04B3:
			foreach (GameObject gameObject6 in list)
			{
				if (gameObject6 != this.ParentObject)
				{
					int num7 = 0;
					if (dictionary.ContainsKey(gameObject6))
					{
						num7 = dictionary[gameObject6];
					}
					int num8 = this.ExtraHeads + 1 + (this.ExtraHeads + 1) * Math.Min(3, num7);
					string damageIncrement = this.GetDamageIncrement(base.Level);
					int num9 = damageIncrement.RollCached();
					for (int k = 0; k < num8; k++)
					{
						num9 += damageIncrement.RollCached();
					}
					if (num9 > 0)
					{
						string text = "from %t charge!";
						if (num7 == 1)
						{
							text = "from being slammed into a wall by %t charge!";
						}
						else if (num7 == 2)
						{
							text = "from being slammed into {{W|2}} walls by %t charge!";
						}
						else if (num7 >= 3)
						{
							text = "from being slammed into {{W|" + num7.ToString() + "}} walls by %t charge!";
						}
						GameObject gameObject7 = gameObject6;
						int num10 = num9;
						GameObject gameObject3 = this.ParentObject;
						gameObject7.TakeDamage(num10, text, null, null, null, null, gameObject3, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					}
					else
					{
						IComponent<GameObject>.XDidY(gameObject6, "are", "shoved by " + Grammar.MakePossessive(this.ParentObject.the + this.ParentObject.DisplayNameOnly) + " charge!", null, null, null, null, gameObject6, false, false, null, null, false, false, false, false, false, null);
					}
				}
			}
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_charge_mega", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetChargeCooldown(base.Level), null, null, false);
			if (ChargePath != null)
			{
				ChargePath.Clear();
			}
		}

		// Token: 0x0600C8AF RID: 51375 RVA: 0x0049192C File Offset: 0x0048FB2C
		public int GetChargeCooldown(int Level)
		{
			return Math.Max(42 - Level * 3, 5);
		}

		// Token: 0x0600C8B0 RID: 51376 RVA: 0x0049193A File Offset: 0x0048FB3A
		public int GetTurnsToCharge()
		{
			return 1;
		}

		// Token: 0x0600C8B1 RID: 51377 RVA: 0x00491940 File Offset: 0x0048FB40
		public override bool Render(RenderEvent E)
		{
			if (this.chargeCells != null && this.chargeCells.Count > 0 && this.ParentObject.CurrentCell != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					E.ColorString = "&r^R";
				}
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600C8B2 RID: 51378 RVA: 0x00491998 File Offset: 0x0048FB98
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.chargeCells == null)
			{
				return;
			}
			int num = 1000;
			int num2 = num / Math.Max(this.chargeCells.Count, 1);
			int num3 = (int)(IComponent<GameObject>.frameTimerMS % (long)num / (long)Math.Max(num2, 1));
			if (num3 > 0 && num3 < this.chargeCells.Count && this.chargeCells[num3].ParentZone == this.ParentObject.Physics.CurrentCell.ParentZone && this.chargeCells[num3].IsVisible())
			{
				buffer.Goto(this.chargeCells[num3].X, this.chargeCells[num3].Y);
				buffer.Write(this.ParentObject.Render.RenderString, true, false, false, null, -1);
				buffer.Buffer[this.chargeCells[num3].X, this.chargeCells[num3].Y].Tile = this.ParentObject.Render.Tile;
				buffer.Buffer[this.chargeCells[num3].X, this.chargeCells[num3].Y].TileForeground = The.Color.DarkRed;
				buffer.Buffer[this.chargeCells[num3].X, this.chargeCells[num3].Y].Detail = The.Color.Red;
				buffer.Buffer[this.chargeCells[num3].X, this.chargeCells[num3].Y].SetForeground('r');
			}
			base.OnPaint(buffer);
		}

		// Token: 0x0600C8B3 RID: 51379 RVA: 0x00491B70 File Offset: 0x0048FD70
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Charging > 0)
				{
					if (!this.ParentObject.CanChangeMovementMode("Charging", true, false, false, false))
					{
						this.Charging = 0;
					}
					else
					{
						if (this.chargeCells.IsNullOrEmpty<Cell>())
						{
							this.PickChargeTarget();
						}
						if (this.chargeCells.IsNullOrEmpty<Cell>())
						{
							this.Charging = 0;
						}
						else
						{
							this.Charging--;
							this.ParentObject.UseEnergy(1000, "Physical Mutation Massive Charge", null, null, false);
							if (this.Charging > 0)
							{
								if (this.ParentObject.IsPlayer())
								{
									The.Core.RenderDelay(500, true, true, false);
								}
								return false;
							}
							this.PerformCharge(this.chargeCells, true);
						}
					}
				}
			}
			else if (E.ID == "CommandMassiveCharge")
			{
				if (this.ParentObject.CurrentCell == null || this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You can't do that here.");
				}
				if (!this.ParentObject.CanChangeMovementMode("Charging", true, false, false, false))
				{
					return false;
				}
				this.PickChargeTarget();
				if (this.chargeCells == null || this.chargeCells.Count <= 0)
				{
					return false;
				}
				this.Charging = this.GetTurnsToCharge();
				this.ParentObject.UseEnergy(1000, "Physical Mutation Massive Charge", null, null, false);
				base.PlayWorldSound("sfx_ability_longBeam_attack_chargeUp", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				base.DidX("stomp", "with bestial fury", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				if (base.Visible() && AutoAct.IsInterruptable())
				{
					AutoAct.Interrupt(null, null, this.ParentObject, true);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C8B4 RID: 51380 RVA: 0x00491D72 File Offset: 0x0048FF72
		public override string GetDescription()
		{
			return "Several horns jut out of your head.";
		}

		// Token: 0x0600C8B5 RID: 51381 RVA: 0x00491D79 File Offset: 0x0048FF79
		public int GetChargeDistance(int Level)
		{
			return 8 + Level;
		}

		// Token: 0x0600C8B6 RID: 51382 RVA: 0x00491D80 File Offset: 0x0048FF80
		public string GetDamageIncrement(int Level)
		{
			return "2d" + (Level / 2 + 3).ToString();
		}

		// Token: 0x0600C8B7 RID: 51383 RVA: 0x00491DA4 File Offset: 0x0048FFA4
		public override string GetLevelText(int Level)
		{
			string text = "";
			int num = 0;
			if (Level == 1)
			{
				text = "2d3";
				num = 0;
			}
			if (Level == 2)
			{
				text = "2d4";
				num = 0;
			}
			if (Level == 3)
			{
				text = "2d4";
				num = 1;
			}
			if (Level == 4)
			{
				text = "2d5";
				num = 1;
			}
			if (Level == 5)
			{
				text = "2d5";
				num = 1;
			}
			if (Level == 6)
			{
				text = "2d6";
				num = 1;
			}
			if (Level == 7)
			{
				text = "2d6";
				num = 2;
			}
			if (Level == 8)
			{
				text = "2d7";
				num = 2;
			}
			if (Level == 9)
			{
				text = "2d7";
				num = 2;
			}
			if (Level >= 10)
			{
				text = "2d8";
				num = 2;
			}
			string text2 = "20% chance on melee attack to gore your opponent\n";
			text2 = text2 + "Damage increment: " + text + "\n";
			if (Level == base.Level)
			{
				text2 += "Goring attacks may cause bleeding\n";
			}
			else
			{
				text2 += "{{rules|Increased bleeding save difficulty and intensity}}\n";
			}
			text2 = text2 + num.Signed(false) + " AV\n";
			text2 += "Cannot wear helmets\n";
			text2 += "Can launch into a destructive charge after a one round warm-up.\n";
			return text2 + "Charge distance: " + this.GetChargeDistance(Level).ToString();
		}

		// Token: 0x0600C8B8 RID: 51384 RVA: 0x00491EB8 File Offset: 0x004900B8
		public override bool ChangeLevel(int NewLevel)
		{
			Body body = this.ParentObject.Body;
			BodyPart body2 = body.GetBody();
			if (!this.bSetHeads)
			{
				for (int i = 0; i < this.ExtraHeads; i++)
				{
					BodyPart bodyPart = body2;
					string text = "Head";
					int num = 0;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					int? num2 = new int?(body2.Category);
					BodyPart bodyPart2 = bodyPart.AddPartAt(text, num, text2, text3, text4, text5, this.ManagerID, num2, null, null, null, null, null, null, null, null, null, null, null, null, "Head", "Back", true);
					string text6 = "Face";
					int num3 = 0;
					string text7 = null;
					string text8 = null;
					string text9 = null;
					string text10 = null;
					int? num4 = new int?(body2.Category);
					string managerID = this.ManagerID;
					int? num5 = num4;
					num2 = null;
					int? num6 = num2;
					num2 = null;
					bodyPart2.AddPart(text6, num3, text7, text8, text9, text10, managerID, num5, num6, num2, null, null, null, null, null, null, null, null, null, null, true);
				}
			}
			this.bSetHeads = true;
			foreach (GameObject gameObject in this.Horns)
			{
				gameObject.Destroy(null, false, false, null);
			}
			foreach (BodyPart bodyPart3 in body.GetPart("Head"))
			{
				bodyPart3.ForceUnequip(true, false, false);
				GameObject gameObject2 = GameObjectFactory.Factory.CreateObject("Horns Single");
				MeleeWeapon part = gameObject2.GetPart<MeleeWeapon>();
				Armor part2 = gameObject2.GetPart<Armor>();
				part.HitBonus = Math.Max(NewLevel - 4, 0);
				if (string.IsNullOrEmpty(this.HornsName))
				{
					this.HornsName = base.GetDisplayName(false).ToLower();
				}
				gameObject2.Render.DisplayName = this.HornsName;
				part.Skill = "Cudgel";
				part.MaxStrengthBonus = 10;
				if (base.Level == 1)
				{
					part.BaseDamage = "2d3";
					part2.AV = 0;
				}
				if (base.Level == 2)
				{
					part.BaseDamage = "2d4";
					part2.AV = 0;
				}
				if (base.Level == 3)
				{
					part.BaseDamage = "2d5";
					part2.AV = 1;
				}
				if (base.Level == 4)
				{
					part.BaseDamage = "2d5";
					part2.AV = 1;
				}
				if (base.Level == 5)
				{
					part.BaseDamage = "2d5";
					part2.AV = 1;
				}
				if (base.Level == 6)
				{
					part.BaseDamage = "2d6";
					part2.AV = 1;
				}
				if (base.Level == 7)
				{
					part.BaseDamage = "2d6";
					part2.AV = 2;
				}
				if (base.Level == 8)
				{
					part.BaseDamage = "2d7";
					part2.AV = 2;
				}
				if (base.Level == 9)
				{
					part.BaseDamage = "2d7";
					part2.AV = 2;
				}
				if (base.Level >= 10)
				{
					part.BaseDamage = "2d8";
					part2.AV = 2;
				}
				this.ParentObject.ForceEquipObject(gameObject2, bodyPart3, true, new int?(0));
				this.Horns.Add(gameObject2);
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C8B9 RID: 51385 RVA: 0x004922C4 File Offset: 0x004904C4
		public override bool Mutate(GameObject GO, int Level)
		{
			if (string.IsNullOrEmpty(this.HornsName))
			{
				if (GO.HasTagOrProperty("HasHorns"))
				{
					base.SetDisplayName("Triple Horn");
					this.HornsName = "horn";
				}
				else if (GO.HasTagOrProperty("HasLittleHorns"))
				{
					base.SetDisplayName("Triple Horn");
					this.HornsName = "little horn";
				}
				else if (GO.Blueprint.Contains("Goat"))
				{
					base.SetDisplayName("Horns");
					this.HornsName = "horns";
				}
				else if (GO.Blueprint.Contains("Rhino"))
				{
					base.SetDisplayName("Horn");
					this.HornsName = "horn";
				}
				else
				{
					int num = Stat.Random(1, 100);
					if (num <= 35)
					{
						base.SetDisplayName("Horns");
						this.HornsName = "horns";
					}
					else if (num <= 65)
					{
						base.SetDisplayName("Antlers");
						this.HornsName = "antlers";
					}
					else
					{
						base.SetDisplayName("Horn");
						this.HornsName = "horn";
					}
				}
			}
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Wrecking Charge", "CommandMassiveCharge", "Physical Mutations", null, "\u00af", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C8BA RID: 51386 RVA: 0x00492420 File Offset: 0x00490620
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			if (this.Horns.Count > 0)
			{
				foreach (GameObject gameObject in this.Horns)
				{
					base.CleanUpMutationEquipment(GO, gameObject);
				}
				this.Horns.Clear();
			}
			GO.RemoveBodyPartsByManager(this.ManagerID, true);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E92 RID: 20114
		[NonSerialized]
		public List<GameObject> Horns = new List<GameObject>(2);

		// Token: 0x04004E93 RID: 20115
		public int ExtraHeads = 2;

		// Token: 0x04004E94 RID: 20116
		public bool bSetHeads;

		// Token: 0x04004E95 RID: 20117
		public string HornsName;

		// Token: 0x04004E96 RID: 20118
		public int Charging;

		// Token: 0x04004E97 RID: 20119
		[NonSerialized]
		private List<Cell> chargeCells;
	}
}
