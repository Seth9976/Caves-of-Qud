using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA8 RID: 2728
	public class MechanicalPower : ZoneBuilderSandbox
	{
		// Token: 0x06006F32 RID: 28466 RVA: 0x0026A35C File Offset: 0x0026855C
		private int MechanicalProducerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("MechanicalProducerRate", -1);
			if (intProperty >= 0)
			{
				return intProperty;
			}
			return obj.QueryCharge(false, 0L, true, true);
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0026A388 File Offset: 0x00268588
		private int MechanicalConsumerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("MechanicalConsumerRate", -1);
			if (intProperty >= 0)
			{
				return intProperty;
			}
			int num = 0;
			foreach (IPart part in obj.PartsList)
			{
				IPoweredPart poweredPart = part as IPoweredPart;
				if (poweredPart != null)
				{
					num += poweredPart.GetActiveChargeUse(null, null);
				}
			}
			return num;
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x0026A410 File Offset: 0x00268610
		private List<Location2D> ResolveLocations(string spec)
		{
			if (string.IsNullOrEmpty(spec))
			{
				return null;
			}
			string[] array = spec.Split(';', StringSplitOptions.None);
			List<Location2D> list = new List<Location2D>(array.Length);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(',', StringSplitOptions.None);
				list.Add(Location2D.Get(Convert.ToInt32(array3[0]), Convert.ToInt32(array3[1])));
			}
			return list;
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x0026A470 File Offset: 0x00268670
		private bool IsProducer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoMechanicalPower"))
			{
				return false;
			}
			MechanicalPowerTransmission part = obj.GetPart<MechanicalPowerTransmission>();
			if (part != null && part.IsProducer)
			{
				return true;
			}
			if (obj.HasTagOrProperty("MechanicalPowerProducer"))
			{
				return true;
			}
			obj.HasTagOrProperty("MechanicalPowerConsumer");
			return false;
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x0026A4BC File Offset: 0x002686BC
		private bool IsConsumer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoMechanicalPower"))
			{
				return false;
			}
			if (obj.HasTagOrProperty("MechanicalPowerProducer"))
			{
				return false;
			}
			MechanicalPowerTransmission part = obj.GetPart<MechanicalPowerTransmission>();
			return (part != null && part.IsConsumer) || obj.HasTagOrProperty("MechanicalPowerConsumer");
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x0026A50C File Offset: 0x0026870C
		private bool ValidInstall(GameObject obj)
		{
			return !obj.Takeable && obj.ConsiderSolid() && !obj.IsCombat && (string.IsNullOrEmpty(this.ConduitModPart) || !obj.HasPart(this.ConduitModPart)) && obj.Brain == null && !obj.HasPart<XRL.World.Parts.Temporary>() && !obj.HasPart<ExistenceSupport>();
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x0026A574 File Offset: 0x00268774
		private string RepresentWeight(int weight)
		{
			string text = weight.ToString();
			if (text.Length == 1)
			{
				return text;
			}
			char c = text[text.Length - 1];
			if (weight < 20)
			{
				return "&Y" + c.ToString() + "&y";
			}
			if (weight < 30)
			{
				return "&r" + c.ToString() + "&y";
			}
			if (weight < 40)
			{
				return "&R" + c.ToString() + "&y";
			}
			if (weight < 50)
			{
				return "&o" + c.ToString() + "&y";
			}
			if (weight < 60)
			{
				return "&O" + c.ToString() + "&y";
			}
			if (weight < 70)
			{
				return "&w" + c.ToString() + "&y";
			}
			if (weight < 80)
			{
				return "&W" + c.ToString() + "&y";
			}
			if (weight < 90)
			{
				return "&g" + c.ToString() + "&y";
			}
			if (weight < 100)
			{
				return "&G" + c.ToString() + "&y";
			}
			if (weight < 110)
			{
				return "&b" + c.ToString() + "&y";
			}
			if (weight < 120)
			{
				return "&B" + c.ToString() + "&y";
			}
			if (weight < 130)
			{
				return "&m" + c.ToString() + "&y";
			}
			if (weight < 140)
			{
				return "&M" + c.ToString() + "&y";
			}
			return "&K" + c.ToString() + "&y";
		}

		// Token: 0x06006F39 RID: 28473 RVA: 0x0026A72C File Offset: 0x0026892C
		private GameObject SetUpMissingConsumer(Zone Z)
		{
			GameObject gameObject;
			if (MechanicalPower.missingConsumerIndex >= MechanicalPower.missingConsumers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("MechanicalPowerConsumer", 1, false);
			}
			else
			{
				gameObject = MechanicalPower.missingConsumers[MechanicalPower.missingConsumerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingConsumerChargeRate))
			{
				gameObject.RemoveIntProperty("MechanicalConsumerRate");
			}
			else
			{
				gameObject.SetIntProperty("MechanicalConsumerRate", this.MissingConsumerChargeRate.RollCached(), false);
			}
			if (this.FinalMissingConsumerLocations != null && this.FinalMissingConsumerLocations.Count > 0)
			{
				Z.GetCell(this.FinalMissingConsumerLocations[0]).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.FinalMissingConsumerLocations.RemoveAt(0);
			}
			else
			{
				int num = 0;
				bool flag = false;
				while (!flag)
				{
					if (num++ >= 10)
					{
						break;
					}
					Cell randomCell = Z.GetRandomCell(0);
					if (randomCell.IsEmpty() && randomCell.HasAdjacentLocalWallCell())
					{
						randomCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						flag = true;
					}
				}
				while (!flag && num++ < 20)
				{
					Cell randomCell2 = Z.GetRandomCell(0);
					if (randomCell2.IsEmpty())
					{
						randomCell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						flag = true;
					}
				}
				if (!flag)
				{
					Z.GetRandomCell(0).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			MechanicalPower.missingConsumers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x0026A8B4 File Offset: 0x00268AB4
		private GameObject SetUpMissingProducer(Zone Z)
		{
			GameObject gameObject;
			if (MechanicalPower.missingProducerIndex >= MechanicalPower.missingProducers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("MechanicalPowerProducer", 1, false);
			}
			else
			{
				gameObject = MechanicalPower.missingProducers[MechanicalPower.missingProducerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingProducerChargeRate))
			{
				gameObject.RemoveIntProperty("MechanicalProducerRate");
			}
			else
			{
				gameObject.SetIntProperty("MechanicalProducerRate", this.MissingProducerChargeRate.RollCached(), false);
			}
			if (this.FinalMissingProducerLocations != null && this.FinalMissingProducerLocations.Count > 0)
			{
				Z.GetCell(this.FinalMissingProducerLocations[0]).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.FinalMissingProducerLocations.RemoveAt(0);
			}
			else
			{
				int num = 0;
				bool flag = false;
				Cell randomCell = Z.GetRandomCell(0);
				if (randomCell.IsEmpty() && randomCell.HasAdjacentLocalWallCell())
				{
					randomCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					flag = true;
				}
				while (!flag && num++ < 20)
				{
					Cell randomCell2 = Z.GetRandomCell(0);
					if (randomCell2.IsEmpty())
					{
						randomCell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						flag = true;
					}
				}
				if (!flag)
				{
					Z.GetRandomCell(0).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			MechanicalPower.missingProducers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x0026AA30 File Offset: 0x00268C30
		private void CleanUp(List<GameObject> list)
		{
			if (list != null)
			{
				foreach (GameObject gameObject in list)
				{
					Cell currentCell = gameObject.CurrentCell;
					if (currentCell != null)
					{
						currentCell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
					gameObject.Obliterate(null, false, null);
				}
				list.Clear();
			}
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x0026AAAC File Offset: 0x00268CAC
		public bool BuildZone(Zone Z)
		{
			MechanicalPower.consumers.Clear();
			MechanicalPower.producers.Clear();
			MechanicalPower.missingConsumerIndex = 0;
			MechanicalPower.missingProducerIndex = 0;
			if (!string.IsNullOrEmpty(this.MissingConsumers))
			{
				this.FinalMissingConsumers = this.MissingConsumers.RollCached();
			}
			if (!string.IsNullOrEmpty(this.MissingProducers))
			{
				this.FinalMissingProducers = this.MissingProducers.RollCached();
			}
			if (this.FinalMissingConsumers > 0)
			{
				this.FinalMissingConsumerLocations = this.ResolveLocations(this.MissingConsumerLocations);
			}
			if (this.FinalMissingProducers > 0)
			{
				this.FinalMissingProducerLocations = this.ResolveLocations(this.MissingProducerLocations);
			}
			for (int i = 0; i < this.FinalMissingConsumers; i++)
			{
				this.SetUpMissingConsumer(Z);
			}
			for (int j = 0; j < this.FinalMissingProducers; j++)
			{
				this.SetUpMissingProducer(Z);
			}
			bool flag5;
			using (Pathfinder pathfinder = Z.getPathfinder(null))
			{
				for (int k = 0; k < Z.Height; k++)
				{
					for (int l = 0; l < Z.Width; l++)
					{
						pathfinder.CurrentNavigationMap[l, k] = 40;
					}
				}
				if (this.Noise > 0)
				{
					for (int m = 0; m < Z.Height; m++)
					{
						for (int n = 0; n < Z.Width; n++)
						{
							pathfinder.CurrentNavigationMap[n, m] += Stat.Random(0, this.Noise * 2) - this.Noise;
						}
					}
				}
				for (int num = 0; num < Z.Height; num++)
				{
					for (int num2 = 0; num2 < Z.Width; num2++)
					{
						Cell cell = Z.GetCell(num2, num);
						int num3 = 0;
						GameObject gameObject = null;
						GameObject gameObject2 = null;
						GameObject gameObject3 = null;
						GameObject gameObject4 = null;
						GameObject gameObject5 = null;
						foreach (GameObject gameObject6 in cell.Objects)
						{
							if (gameObject == null && gameObject6.IsWall() && !gameObject6.HasPart<ExistenceSupport>())
							{
								gameObject = gameObject6;
							}
							if (gameObject2 == null && gameObject6.HasPart<Door>() && !gameObject6.HasPart<ExistenceSupport>())
							{
								gameObject2 = gameObject6;
							}
							if (this.IsProducer(gameObject6))
							{
								gameObject3 = gameObject6;
							}
							if (this.IsConsumer(gameObject6))
							{
								gameObject4 = gameObject6;
							}
							if (gameObject6.HasPart<MechanicalPowerTransmission>())
							{
								gameObject5 = gameObject6;
							}
						}
						if (gameObject3 != null || gameObject4 != null)
						{
							if (gameObject3 != null)
							{
								MechanicalPower.producers.Add(gameObject3);
							}
							if (gameObject4 != null)
							{
								MechanicalPower.consumers.Add(gameObject4);
							}
							bool flag = false;
							if (gameObject == null && this.PreferWalls && !this.AvoidWalls)
							{
								foreach (Cell cell2 in cell.GetLocalCardinalAdjacentCells())
								{
									if (cell2.HasWall())
									{
										for (int num4 = 0; num4 < Z.Width; num4++)
										{
											pathfinder.CurrentNavigationMap[num4, cell2.Y]--;
										}
										for (int num5 = 0; num5 < Z.Height; num5++)
										{
											pathfinder.CurrentNavigationMap[cell2.X, num5]--;
										}
										flag = true;
									}
								}
							}
							if (flag)
							{
								num3 += 40;
							}
							else
							{
								for (int num6 = 0; num6 < Z.Width; num6++)
								{
									pathfinder.CurrentNavigationMap[num6, num]--;
								}
								for (int num7 = 0; num7 < Z.Height; num7++)
								{
									pathfinder.CurrentNavigationMap[num2, num7]--;
								}
								num3 += 8;
							}
						}
						else if (gameObject != null)
						{
							MechanicalPowerTransmission part = gameObject.GetPart<MechanicalPowerTransmission>();
							if (part != null)
							{
								num3 -= 20 + part.ChargeRate / 100;
							}
							else if (this.AvoidWalls)
							{
								num3 += 30 - Math.Max(gameObject.GetTechTier(), 1) * 2;
							}
							else if (this.PreferWalls)
							{
								num3 -= 20 + Math.Max(gameObject.GetTechTier(), 1) * 2;
							}
						}
						else if (gameObject2 != null)
						{
							MechanicalPowerTransmission part2 = gameObject2.GetPart<MechanicalPowerTransmission>();
							if (part2 != null)
							{
								num3 -= 18 + part2.ChargeRate / 100;
							}
							else if (this.AvoidWalls)
							{
								num3 -= 28 - Math.Max(gameObject2.GetTechTier(), 1) * 2;
							}
							else if (this.PreferWalls)
							{
								num3 -= 18 + Math.Max(gameObject2.GetTechTier(), 1) * 2;
							}
						}
						else if (gameObject5 != null)
						{
							MechanicalPowerTransmission part3 = gameObject5.GetPart<MechanicalPowerTransmission>();
							if (part3 != null)
							{
								num3 -= 20 + part3.ChargeRate / 100;
							}
							else
							{
								num3 -= 22;
							}
						}
						pathfinder.CurrentNavigationMap[num2, num] += num3;
					}
				}
				if (MechanicalPower.producers.Count > 0 && MechanicalPower.consumers.Count > 0)
				{
					for (int num8 = 0; num8 < Z.Height; num8++)
					{
						for (int num9 = 0; num9 < Z.Width; num9++)
						{
							if (pathfinder.CurrentNavigationMap[num9, num8] < 1)
							{
								pathfinder.CurrentNavigationMap[num9, num8] = 1;
							}
						}
					}
					if (this.ShowPathWeights)
					{
						ScreenBuffer screenBuffer = Popup._ScreenBuffer;
						for (int num10 = 0; num10 < Z.Height; num10++)
						{
							for (int num11 = 0; num11 < Z.Width; num11++)
							{
								screenBuffer.Goto(num11, num10);
								screenBuffer.Write(this.RepresentWeight(pathfinder.CurrentNavigationMap[num11, num10]), true, false, false, null, -1);
							}
						}
						XRLCore._Console.DrawBuffer(screenBuffer, null, false);
						Keyboard.getch();
					}
					MechanicalPower.consumers.Sort(new CentralitySorter(Z));
					if (!string.IsNullOrEmpty(this.DamageChance))
					{
						this.FinalDamageChance = this.DamageChance.RollCached();
					}
					if (this.FinalDamageChance > 0 && !string.IsNullOrEmpty(this.DamageIsBreakageChance))
					{
						this.FinalDamageIsBreakageChance = this.DamageIsBreakageChance.RollCached();
					}
					bool[,] array = new bool[Z.Width, Z.Height];
					foreach (GameObject gameObject7 in MechanicalPower.consumers)
					{
						Location2D location2D = Location2D.Get(gameObject7.CurrentCell.X, gameObject7.CurrentCell.Y);
						GameObject gameObject8 = this.FindBestConnection(gameObject7, MechanicalPower.producers, Z);
						if (gameObject8 != null)
						{
							Location2D location2D2 = Location2D.Get(gameObject8.CurrentCell.X, gameObject8.CurrentCell.Y);
							if (pathfinder.FindPath(location2D, location2D2, this.ShowPathfinding, true, 9999, false))
							{
								int num12 = 1;
								int num13 = pathfinder.Steps.Count - 1;
								while (num12 < num13)
								{
									PathfinderNode pathfinderNode = pathfinder.Steps[num12];
									if (!array[pathfinderNode.X, pathfinderNode.Y])
									{
										if (pathfinderNode.X == location2D.X && pathfinderNode.Y == location2D.Y)
										{
											Debug.LogError("power grid trying to apply at consumer location");
										}
										else if (pathfinderNode.X == location2D2.X && pathfinderNode.Y == location2D2.Y)
										{
											Debug.LogError("power grid trying to apply at producer location");
										}
										else
										{
											Cell cell3 = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
											if (!cell3.HasObjectWithPart("MechanicalPowerTransmission"))
											{
												bool flag2 = false;
												GameObject gameObject9 = null;
												GameObject gameObject10 = null;
												if (!string.IsNullOrEmpty(this.ConduitModPart))
												{
													gameObject9 = cell3.GetFirstObjectWithPropertyOrTag("Wall", new Predicate<GameObject>(this.ValidInstall)) ?? cell3.GetFirstObjectWithPart("Physics", new Predicate<GameObject>(this.ValidInstall));
												}
												if (gameObject9 != null)
												{
													if (!this.FinalDamageChance.in100())
													{
														ItemModding.ApplyModification(gameObject9, this.ConduitModPart, true, null, false);
														if (cell3.ParentZone.Built)
														{
															ZoneManager.PaintWalls(cell3.ParentZone, cell3.X - 1, cell3.Y - 1, cell3.X + 1, cell3.Y + 1);
														}
													}
													else
													{
														array[pathfinderNode.X, pathfinderNode.Y] = true;
													}
													flag2 = true;
												}
												else if (!string.IsNullOrEmpty(this.ConduitBlueprint))
												{
													bool flag3 = this.FinalDamageChance.in100();
													bool flag4 = flag3 && this.FinalDamageIsBreakageChance.in100();
													if (!flag3 || flag4)
													{
														gameObject10 = GameObject.CreateUnmodified(this.ConduitBlueprint);
														if (flag4)
														{
															gameObject10.ApplyEffect(new Broken(), null);
														}
														cell3.AddObject(gameObject10, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
													}
													else
													{
														array[pathfinderNode.X, pathfinderNode.Y] = true;
													}
													flag2 = true;
												}
												if (flag2)
												{
													int num14 = pathfinderNode.weight;
													MechanicalPowerTransmission mechanicalPowerTransmission = ((gameObject9 != null) ? gameObject9.GetPart<MechanicalPowerTransmission>() : null) ?? ((gameObject10 != null) ? gameObject10.GetPart<MechanicalPowerTransmission>() : null);
													num14 -= ((mechanicalPowerTransmission == null) ? 12 : (10 + mechanicalPowerTransmission.ChargeRate / 500));
													if (num14 < 1)
													{
														num14 = 1;
													}
													pathfinder.CurrentNavigationMap[pathfinderNode.X, pathfinderNode.Y] = num14;
													pathfinderNode.weight = num14;
												}
											}
										}
									}
									num12++;
								}
							}
						}
					}
				}
				MechanicalPower.producers.Clear();
				MechanicalPower.consumers.Clear();
				this.CleanUp(MechanicalPower.missingConsumers);
				this.CleanUp(MechanicalPower.missingProducers);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x0026B49C File Offset: 0x0026969C
		private GameObject FindBestConnection(GameObject obj, List<GameObject> connects, Zone Z)
		{
			double num = 999999.0;
			GameObject gameObject = null;
			int num2 = Z.Width / 2;
			int num3 = Z.Height / 2;
			int num4 = this.MechanicalConsumerRate(obj);
			foreach (GameObject gameObject2 in connects)
			{
				int num5 = this.Suitability(obj, gameObject2, num2, num3, num4);
				if ((double)num5 < num)
				{
					num = (double)num5;
					gameObject = gameObject2;
				}
			}
			return gameObject;
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x0026B52C File Offset: 0x0026972C
		private int Suitability(GameObject obj, GameObject connect, int CenterX, int CenterY, int draw)
		{
			double num = (double)XRL.Rules.Geometry.Distance(CenterX, CenterY, connect);
			double num2 = obj.RealDistanceTo(connect);
			double num3 = num / 3.0 + num2;
			if (draw > 0)
			{
				int num4 = this.MechanicalProducerRate(connect);
				if (draw > num4)
				{
					if (num4 > 0)
					{
						num3 = num3 * (double)draw / (double)num4;
					}
					else
					{
						num3 *= 100.0;
					}
				}
			}
			return Convert.ToInt32(num3);
		}

		// Token: 0x040032FE RID: 13054
		public string DamageChance;

		// Token: 0x040032FF RID: 13055
		public string DamageIsBreakageChance = "50";

		// Token: 0x04003300 RID: 13056
		public string ConduitBlueprint = "WoodenMechanicalTransmission";

		// Token: 0x04003301 RID: 13057
		public string ConduitModPart = "ModGearbox";

		// Token: 0x04003302 RID: 13058
		public string MissingConsumers;

		// Token: 0x04003303 RID: 13059
		public string MissingProducers;

		// Token: 0x04003304 RID: 13060
		public string MissingConsumerChargeRate = "1|1|1|2|5|10|20|30|1d20x10";

		// Token: 0x04003305 RID: 13061
		public string MissingProducerChargeRate = "50|100|150|200|1d20x20";

		// Token: 0x04003306 RID: 13062
		public string MissingConsumerLocations;

		// Token: 0x04003307 RID: 13063
		public string MissingProducerLocations;

		// Token: 0x04003308 RID: 13064
		public int FinalDamageChance;

		// Token: 0x04003309 RID: 13065
		public int FinalDamageIsBreakageChance;

		// Token: 0x0400330A RID: 13066
		public int FinalMissingConsumers;

		// Token: 0x0400330B RID: 13067
		public int FinalMissingProducers;

		// Token: 0x0400330C RID: 13068
		public int Noise;

		// Token: 0x0400330D RID: 13069
		public bool PreferWalls = true;

		// Token: 0x0400330E RID: 13070
		public bool AvoidWalls;

		// Token: 0x0400330F RID: 13071
		public bool ShowPathfinding;

		// Token: 0x04003310 RID: 13072
		public bool ShowPathWeights;

		// Token: 0x04003311 RID: 13073
		public List<Location2D> FinalMissingConsumerLocations;

		// Token: 0x04003312 RID: 13074
		public List<Location2D> FinalMissingProducerLocations;

		// Token: 0x04003313 RID: 13075
		[NonSerialized]
		private static List<GameObject> consumers = new List<GameObject>(16);

		// Token: 0x04003314 RID: 13076
		[NonSerialized]
		private static List<GameObject> producers = new List<GameObject>(8);

		// Token: 0x04003315 RID: 13077
		[NonSerialized]
		private static List<GameObject> missingConsumers = new List<GameObject>(16);

		// Token: 0x04003316 RID: 13078
		[NonSerialized]
		private static List<GameObject> missingProducers = new List<GameObject>(8);

		// Token: 0x04003317 RID: 13079
		[NonSerialized]
		private static int missingConsumerIndex = 0;

		// Token: 0x04003318 RID: 13080
		[NonSerialized]
		private static int missingProducerIndex = 0;
	}
}
