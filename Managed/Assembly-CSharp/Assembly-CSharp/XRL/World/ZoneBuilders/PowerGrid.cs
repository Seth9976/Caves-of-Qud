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
	// Token: 0x02000AC9 RID: 2761
	public class PowerGrid : ZoneBuilderSandbox
	{
		// Token: 0x06006F9B RID: 28571 RVA: 0x002725FC File Offset: 0x002707FC
		private int GridProducerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("GridProducerRate", -1);
			if (intProperty >= 0)
			{
				return intProperty;
			}
			return obj.QueryCharge(false, 0L, true, true);
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x00272628 File Offset: 0x00270828
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

		// Token: 0x06006F9D RID: 28573 RVA: 0x00272688 File Offset: 0x00270888
		private bool IsProducer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoGridPower"))
			{
				return false;
			}
			if (obj.HasTagOrProperty("GridPowerConsumer"))
			{
				return false;
			}
			ElectricalPowerTransmission part = obj.GetPart<ElectricalPowerTransmission>();
			return (part != null && part.IsProducer) || obj.HasTagOrProperty("GridPowerProducer");
		}

		// Token: 0x06006F9E RID: 28574 RVA: 0x002726D8 File Offset: 0x002708D8
		private bool IsConsumer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoGridPower"))
			{
				return false;
			}
			ElectricalPowerTransmission part = obj.GetPart<ElectricalPowerTransmission>();
			if (part != null && part.IsConsumer)
			{
				return true;
			}
			if (obj.HasTagOrProperty("GridPowerConsumer"))
			{
				return true;
			}
			obj.HasTagOrProperty("GridPowerProducer");
			return false;
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x00272724 File Offset: 0x00270924
		private int GridConsumerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("GridConsumerRate", -1);
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

		// Token: 0x06006FA0 RID: 28576 RVA: 0x002727AC File Offset: 0x002709AC
		private bool ValidInstall(GameObject obj)
		{
			return !obj.Takeable && obj.ConsiderSolid() && !obj.IsCombat && (string.IsNullOrEmpty(this.ConduitModPart) || !obj.HasPart(this.ConduitModPart)) && obj.Brain == null && !obj.HasPart<XRL.World.Parts.Temporary>() && !obj.HasPart<ExistenceSupport>();
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x00272813 File Offset: 0x00270A13
		private bool ValidConduitInstall(GameObject obj)
		{
			return obj != null && !string.IsNullOrEmpty(this.ConduitModPart) && this.ValidInstall(obj) && ItemModding.ModificationApplicable(this.ConduitModPart, obj, null, null);
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x00272844 File Offset: 0x00270A44
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

		// Token: 0x06006FA3 RID: 28579 RVA: 0x002729FC File Offset: 0x00270BFC
		private GameObject SetUpMissingConsumer(Zone Z)
		{
			GameObject gameObject;
			if (PowerGrid.missingConsumerIndex >= PowerGrid.missingConsumers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("GridPowerConsumer", 1, false);
			}
			else
			{
				gameObject = PowerGrid.missingConsumers[PowerGrid.missingConsumerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingConsumerChargeRate))
			{
				gameObject.RemoveIntProperty("GridConsumerRate");
			}
			else
			{
				gameObject.SetIntProperty("GridConsumerRate", this.MissingConsumerChargeRate.RollCached(), false);
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
			PowerGrid.missingConsumers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x00272B84 File Offset: 0x00270D84
		private GameObject SetUpMissingProducer(Zone Z)
		{
			GameObject gameObject;
			if (PowerGrid.missingProducerIndex >= PowerGrid.missingProducers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("GridPowerProducer", 1, false);
			}
			else
			{
				gameObject = PowerGrid.missingProducers[PowerGrid.missingProducerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingProducerChargeRate))
			{
				gameObject.RemoveIntProperty("GridProducerRate");
			}
			else
			{
				gameObject.SetIntProperty("GridProducerRate", this.MissingProducerChargeRate.RollCached(), false);
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
			PowerGrid.missingProducers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x00272D00 File Offset: 0x00270F00
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

		// Token: 0x06006FA6 RID: 28582 RVA: 0x00272D7C File Offset: 0x00270F7C
		public bool BuildZone(Zone Z)
		{
			PowerGrid.consumers.Clear();
			PowerGrid.producers.Clear();
			PowerGrid.missingConsumerIndex = 0;
			PowerGrid.missingProducerIndex = 0;
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
							if (gameObject6.HasPart<ElectricalPowerTransmission>())
							{
								gameObject5 = gameObject6;
							}
						}
						if (gameObject3 != null || gameObject4 != null)
						{
							if (gameObject3 != null)
							{
								PowerGrid.producers.Add(gameObject3);
							}
							if (gameObject4 != null)
							{
								PowerGrid.consumers.Add(gameObject4);
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
							ElectricalPowerTransmission part = gameObject.GetPart<ElectricalPowerTransmission>();
							if (part != null)
							{
								num3 -= 20 + part.ChargeRate / 250;
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
							ElectricalPowerTransmission part2 = gameObject2.GetPart<ElectricalPowerTransmission>();
							if (part2 != null)
							{
								num3 -= 18 + part2.ChargeRate / 250;
							}
							if (this.AvoidWalls)
							{
								num3 += 28 - Math.Max(gameObject2.GetTechTier(), 1) * 2;
							}
							else if (this.PreferWalls)
							{
								num3 -= 18 + Math.Max(gameObject2.GetTechTier(), 1) * 2;
							}
						}
						else if (gameObject5 != null)
						{
							ElectricalPowerTransmission part3 = gameObject5.GetPart<ElectricalPowerTransmission>();
							if (part3 != null)
							{
								num3 -= 20 + part3.ChargeRate / 250;
							}
							else
							{
								num3 -= 22;
							}
						}
						pathfinder.CurrentNavigationMap[num2, num] += num3;
					}
				}
				if (PowerGrid.producers.Count > 0 && PowerGrid.consumers.Count > 0)
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
						for (int num10 = 0; num10 < Z.Width; num10++)
						{
							for (int num11 = 0; num11 < Z.Height; num11++)
							{
								screenBuffer.Goto(num10, num11);
								screenBuffer.Write(this.RepresentWeight(pathfinder.CurrentNavigationMap[num10, num11]), true, false, false, null, -1);
							}
						}
						XRLCore._Console.DrawBuffer(screenBuffer, null, false);
						Keyboard.getch();
					}
					PowerGrid.consumers.Sort(new CentralitySorter(Z));
					if (!string.IsNullOrEmpty(this.DamageChance))
					{
						this.FinalDamageChance = this.DamageChance.RollCached();
					}
					if (this.FinalDamageChance > 0 && !string.IsNullOrEmpty(this.DamageIsBreakageChance))
					{
						this.FinalDamageIsBreakageChance = this.DamageIsBreakageChance.RollCached();
					}
					bool[,] array = new bool[Z.Width, Z.Height];
					foreach (GameObject gameObject7 in PowerGrid.consumers)
					{
						Location2D location2D = Location2D.Get(gameObject7.CurrentCell.X, gameObject7.CurrentCell.Y);
						GameObject gameObject8 = this.FindBestConnection(gameObject7, PowerGrid.producers, Z);
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
											if (!cell3.HasObjectWithPart("ElectricalPowerTransmission"))
											{
												bool flag2 = false;
												GameObject gameObject9 = null;
												GameObject gameObject10 = null;
												if (!string.IsNullOrEmpty(this.ConduitModPart))
												{
													gameObject9 = cell3.GetFirstObjectWithPropertyOrTag("Wall", new Predicate<GameObject>(this.ValidConduitInstall)) ?? cell3.GetFirstObjectWithPart("Physics", new Predicate<GameObject>(this.ValidConduitInstall));
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
												}
												if (gameObject9 == null)
												{
													if (!string.IsNullOrEmpty(this.ConduitBlueprint))
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
													ElectricalPowerTransmission electricalPowerTransmission = ((gameObject9 != null) ? gameObject9.GetPart<ElectricalPowerTransmission>() : null) ?? ((gameObject10 != null) ? gameObject10.GetPart<ElectricalPowerTransmission>() : null);
													num14 -= ((electricalPowerTransmission == null) ? 12 : (10 + electricalPowerTransmission.ChargeRate / 500));
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
				PowerGrid.producers.Clear();
				PowerGrid.consumers.Clear();
				this.CleanUp(PowerGrid.missingConsumers);
				this.CleanUp(PowerGrid.missingProducers);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x00273778 File Offset: 0x00271978
		private GameObject FindBestConnection(GameObject obj, List<GameObject> connects, Zone Z)
		{
			double num = 999999.0;
			GameObject gameObject = null;
			int num2 = Z.Width / 2;
			int num3 = Z.Height / 2;
			int num4 = this.GridConsumerRate(obj);
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

		// Token: 0x06006FA8 RID: 28584 RVA: 0x00273808 File Offset: 0x00271A08
		private int Suitability(GameObject obj, GameObject connect, int CenterX, int CenterY, int draw)
		{
			double num = (double)XRL.Rules.Geometry.Distance(CenterX, CenterY, connect);
			double num2 = obj.RealDistanceTo(connect);
			double num3 = num / 3.0 + num2;
			if (draw > 0)
			{
				int num4 = this.GridProducerRate(connect);
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

		// Token: 0x0400333A RID: 13114
		public string DamageChance;

		// Token: 0x0400333B RID: 13115
		public string DamageIsBreakageChance = "50";

		// Token: 0x0400333C RID: 13116
		public string ConduitBlueprint = "PowerLine";

		// Token: 0x0400333D RID: 13117
		public string ConduitModPart = "ModWired";

		// Token: 0x0400333E RID: 13118
		public string MissingConsumers;

		// Token: 0x0400333F RID: 13119
		public string MissingProducers;

		// Token: 0x04003340 RID: 13120
		public string MissingConsumerChargeRate = "1|2|5|50|100|200|500|1000|1d8x500";

		// Token: 0x04003341 RID: 13121
		public string MissingProducerChargeRate = "500|1000|1500|2500|3000|1d20x500";

		// Token: 0x04003342 RID: 13122
		public string MissingConsumerLocations;

		// Token: 0x04003343 RID: 13123
		public string MissingProducerLocations;

		// Token: 0x04003344 RID: 13124
		public int FinalDamageChance;

		// Token: 0x04003345 RID: 13125
		public int FinalDamageIsBreakageChance;

		// Token: 0x04003346 RID: 13126
		public int FinalMissingConsumers;

		// Token: 0x04003347 RID: 13127
		public int FinalMissingProducers;

		// Token: 0x04003348 RID: 13128
		public int Noise;

		// Token: 0x04003349 RID: 13129
		public bool PreferWalls = true;

		// Token: 0x0400334A RID: 13130
		public bool AvoidWalls;

		// Token: 0x0400334B RID: 13131
		public bool ShowPathfinding;

		// Token: 0x0400334C RID: 13132
		public bool ShowPathWeights;

		// Token: 0x0400334D RID: 13133
		public List<Location2D> FinalMissingConsumerLocations;

		// Token: 0x0400334E RID: 13134
		public List<Location2D> FinalMissingProducerLocations;

		// Token: 0x0400334F RID: 13135
		[NonSerialized]
		private static List<GameObject> consumers = new List<GameObject>(16);

		// Token: 0x04003350 RID: 13136
		[NonSerialized]
		private static List<GameObject> producers = new List<GameObject>(8);

		// Token: 0x04003351 RID: 13137
		[NonSerialized]
		private static List<GameObject> missingConsumers = new List<GameObject>(16);

		// Token: 0x04003352 RID: 13138
		[NonSerialized]
		private static List<GameObject> missingProducers = new List<GameObject>(8);

		// Token: 0x04003353 RID: 13139
		[NonSerialized]
		private static int missingConsumerIndex = 0;

		// Token: 0x04003354 RID: 13140
		[NonSerialized]
		private static int missingProducerIndex = 0;
	}
}
