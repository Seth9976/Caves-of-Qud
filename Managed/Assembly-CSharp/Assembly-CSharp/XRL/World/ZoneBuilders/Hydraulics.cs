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
	// Token: 0x02000AA0 RID: 2720
	public class Hydraulics : ZoneBuilderSandbox
	{
		// Token: 0x06006F0E RID: 28430 RVA: 0x00266DD0 File Offset: 0x00264FD0
		private int HydraulicProducerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("HydraulicProducerRate", -1);
			if (intProperty >= 0)
			{
				return intProperty;
			}
			return obj.QueryCharge(false, 0L, true, true);
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x00266DFC File Offset: 0x00264FFC
		private int HydraulicConsumerRate(GameObject obj)
		{
			int intProperty = obj.GetIntProperty("HydraulicConsumerRate", -1);
			if (intProperty >= 0)
			{
				return intProperty;
			}
			int num = 0;
			foreach (IPart part in obj.PartsList)
			{
				if (part is IPoweredPart)
				{
					IPoweredPart poweredPart = part as IPoweredPart;
					num += poweredPart.GetActiveChargeUse(null, null);
				}
			}
			return num;
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x00266E8C File Offset: 0x0026508C
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

		// Token: 0x06006F11 RID: 28433 RVA: 0x00266EEC File Offset: 0x002650EC
		private bool IsProducer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoHydraulicPower"))
			{
				return false;
			}
			if (obj.HasTagOrProperty("HydraulicPowerConsumer"))
			{
				return false;
			}
			HydraulicPowerTransmission part = obj.GetPart<HydraulicPowerTransmission>();
			if (part != null && part.IsProducer)
			{
				return true;
			}
			if (obj.HasTagOrProperty("HydraulicPowerProducer"))
			{
				return true;
			}
			obj.HasTagOrProperty("HydraulicPowerConsumer");
			return false;
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x00266F48 File Offset: 0x00265148
		private bool IsConsumer(GameObject obj)
		{
			if (obj.HasTagOrProperty("NoHydraulicPower"))
			{
				return false;
			}
			HydraulicPowerTransmission part = obj.GetPart<HydraulicPowerTransmission>();
			if (part != null && part.IsConsumer)
			{
				return true;
			}
			if (obj.HasTagOrProperty("HydraulicPowerConsumer"))
			{
				return true;
			}
			obj.HasTagOrProperty("HydraulicPowerProducer");
			return false;
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x00266F94 File Offset: 0x00265194
		private bool ValidInstall(GameObject obj)
		{
			return !obj.Takeable && obj.ConsiderSolid() && !obj.IsCombat && (string.IsNullOrEmpty(this.ConduitModPart) || !obj.HasPart(this.ConduitModPart)) && obj.Brain == null && !obj.HasPart<XRL.World.Parts.Temporary>() && !obj.HasPart<ExistenceSupport>();
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x00266FFC File Offset: 0x002651FC
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

		// Token: 0x06006F15 RID: 28437 RVA: 0x002671B4 File Offset: 0x002653B4
		private GameObject SetUpMissingConsumer(Zone Z)
		{
			GameObject gameObject;
			if (Hydraulics.missingConsumerIndex >= Hydraulics.missingConsumers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("HydraulicPowerConsumer", 1, false);
			}
			else
			{
				gameObject = Hydraulics.missingConsumers[Hydraulics.missingConsumerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingConsumerChargeRate))
			{
				gameObject.RemoveIntProperty("HydraulicConsumerRate");
			}
			else
			{
				gameObject.SetIntProperty("HydraulicConsumerRate", this.MissingConsumerChargeRate.RollCached(), false);
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
			Hydraulics.missingConsumers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x0026733C File Offset: 0x0026553C
		private GameObject SetUpMissingProducer(Zone Z)
		{
			GameObject gameObject;
			if (Hydraulics.missingProducerIndex >= Hydraulics.missingProducers.Count)
			{
				gameObject = GameObject.CreateUnmodified("CosmeticObject");
				gameObject.Physics.Solid = true;
				gameObject.SetIntProperty("HydraulicPowerProducer", 1, false);
			}
			else
			{
				gameObject = Hydraulics.missingProducers[Hydraulics.missingProducerIndex++];
			}
			if (string.IsNullOrEmpty(this.MissingProducerChargeRate))
			{
				gameObject.RemoveIntProperty("HydraulicProducerRate");
			}
			else
			{
				gameObject.SetIntProperty("HydraulicProducerRate", this.MissingProducerChargeRate.RollCached(), false);
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
			Hydraulics.missingProducers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x002674B8 File Offset: 0x002656B8
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

		// Token: 0x06006F18 RID: 28440 RVA: 0x00267534 File Offset: 0x00265734
		public bool BuildZone(Zone Z)
		{
			Hydraulics.consumers.Clear();
			Hydraulics.producers.Clear();
			Hydraulics.conduits.Clear();
			Hydraulics.all.Clear();
			Hydraulics.missingConsumerIndex = 0;
			Hydraulics.missingProducerIndex = 0;
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
							if (gameObject6.HasPart<HydraulicPowerTransmission>())
							{
								gameObject5 = gameObject6;
							}
						}
						if (gameObject3 != null || gameObject4 != null)
						{
							if (gameObject3 != null)
							{
								Hydraulics.producers.Add(gameObject3);
							}
							if (gameObject4 != null)
							{
								Hydraulics.consumers.Add(gameObject4);
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
							HydraulicPowerTransmission part = gameObject.GetPart<HydraulicPowerTransmission>();
							if (part != null)
							{
								num3 -= 20 + part.ChargeRate / 1000;
								Hydraulics.conduits.Add(gameObject);
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
							HydraulicPowerTransmission part2 = gameObject2.GetPart<HydraulicPowerTransmission>();
							if (part2 != null)
							{
								num3 -= 18 + part2.ChargeRate / 1000;
								Hydraulics.conduits.Add(gameObject2);
							}
							else if (this.AvoidWalls)
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
							HydraulicPowerTransmission part3 = gameObject5.GetPart<HydraulicPowerTransmission>();
							if (part3 != null)
							{
								Hydraulics.conduits.Add(gameObject5);
								num3 -= 20 + part3.ChargeRate / 1000;
							}
							else
							{
								num3 -= 22;
							}
						}
						pathfinder.CurrentNavigationMap[num2, num] += num3;
					}
				}
				if (Hydraulics.producers.Count > 0 && Hydraulics.consumers.Count > 0)
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
					Hydraulics.consumers.Sort(new CentralitySorter(Z));
					if (!string.IsNullOrEmpty(this.DamageChance))
					{
						this.FinalDamageChance = this.DamageChance.RollCached();
					}
					if (this.FinalDamageChance > 0 && !string.IsNullOrEmpty(this.DamageIsBreakageChance))
					{
						this.FinalDamageIsBreakageChance = this.DamageIsBreakageChance.RollCached();
					}
					bool[,] array = new bool[Z.Width, Z.Height];
					foreach (GameObject gameObject7 in Hydraulics.consumers)
					{
						Location2D location2D = Location2D.Get(gameObject7.CurrentCell.X, gameObject7.CurrentCell.Y);
						GameObject gameObject8 = this.FindBestConnection(gameObject7, Hydraulics.producers, Z);
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
											Debug.LogError("hydraulic grid trying to apply at consumer location");
										}
										else if (pathfinderNode.X == location2D2.X && pathfinderNode.Y == location2D2.Y)
										{
											Debug.LogError("hydraulic grid trying to apply at producer location");
										}
										else
										{
											Cell cell3 = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
											if (!cell3.HasObjectWithPart("HydraulicPowerTransmission"))
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
													Hydraulics.conduits.Add(gameObject9);
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
														Hydraulics.conduits.Add(gameObject10);
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
													HydraulicPowerTransmission hydraulicPowerTransmission = ((gameObject9 != null) ? gameObject9.GetPart<HydraulicPowerTransmission>() : null) ?? ((gameObject10 != null) ? gameObject10.GetPart<HydraulicPowerTransmission>() : null);
													num14 -= ((hydraulicPowerTransmission == null) ? 12 : (10 + hydraulicPowerTransmission.ChargeRate / 500));
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
				if (!string.IsNullOrEmpty(this.LiquidTable))
				{
					PopulationResult populationResult = PopulationManager.RollOneFrom(this.LiquidTable, null, null);
					this.Liquid = ((populationResult != null) ? populationResult.Blueprint : null);
				}
				if (!string.IsNullOrEmpty(this.Liquid))
				{
					string text = this.Liquid;
					if (text.Contains(",") || text.Contains("-"))
					{
						text = text.Split(',', StringSplitOptions.None)[0].Split('-', StringSplitOptions.None)[0];
					}
					Hydraulics.all.AddRange(Hydraulics.producers);
					Hydraulics.all.AddRange(Hydraulics.consumers);
					Hydraulics.all.AddRange(Hydraulics.conduits);
					foreach (GameObject gameObject11 in Hydraulics.all)
					{
						HydraulicPowerTransmission part4 = gameObject11.GetPart<HydraulicPowerTransmission>();
						if (part4 != null && !string.IsNullOrEmpty(part4.DependsOnLiquid))
						{
							part4.DependsOnLiquid = text;
							LiquidVolume liquidVolume = gameObject11.LiquidVolume;
							if (liquidVolume != null)
							{
								liquidVolume.InitialLiquid = this.Liquid;
							}
						}
					}
				}
				Hydraulics.producers.Clear();
				Hydraulics.consumers.Clear();
				Hydraulics.conduits.Clear();
				Hydraulics.all.Clear();
				this.CleanUp(Hydraulics.missingConsumers);
				this.CleanUp(Hydraulics.missingProducers);
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x06006F19 RID: 28441 RVA: 0x002680C0 File Offset: 0x002662C0
		private GameObject FindBestConnection(GameObject obj, List<GameObject> connects, Zone Z)
		{
			double num = 999999.0;
			GameObject gameObject = null;
			int num2 = Z.Width / 2;
			int num3 = Z.Height / 2;
			int num4 = this.HydraulicConsumerRate(obj);
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

		// Token: 0x06006F1A RID: 28442 RVA: 0x00268150 File Offset: 0x00266350
		private int Suitability(GameObject obj, GameObject connect, int CenterX, int CenterY, int draw)
		{
			double num = (double)XRL.Rules.Geometry.Distance(CenterX, CenterY, connect);
			double num2 = obj.RealDistanceTo(connect);
			double num3 = num / 3.0 + num2;
			if (draw > 0)
			{
				int num4 = this.HydraulicProducerRate(connect);
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

		// Token: 0x040032D0 RID: 13008
		public string DamageChance;

		// Token: 0x040032D1 RID: 13009
		public string DamageIsBreakageChance = "50";

		// Token: 0x040032D2 RID: 13010
		public string ConduitBlueprint = "GlassHydraulicPipe";

		// Token: 0x040032D3 RID: 13011
		public string ConduitModPart = "ModPiping";

		// Token: 0x040032D4 RID: 13012
		public string MissingConsumers;

		// Token: 0x040032D5 RID: 13013
		public string MissingProducers;

		// Token: 0x040032D6 RID: 13014
		public string MissingConsumerChargeRate = "50|100|200|500|1000|2000|1d20x500";

		// Token: 0x040032D7 RID: 13015
		public string MissingProducerChargeRate = "2000|4000|8000|1d20x1000";

		// Token: 0x040032D8 RID: 13016
		public string MissingConsumerLocations;

		// Token: 0x040032D9 RID: 13017
		public string MissingProducerLocations;

		// Token: 0x040032DA RID: 13018
		public string Liquid;

		// Token: 0x040032DB RID: 13019
		public string LiquidTable = "HydraulicFluid";

		// Token: 0x040032DC RID: 13020
		public int FinalDamageChance;

		// Token: 0x040032DD RID: 13021
		public int FinalDamageIsBreakageChance;

		// Token: 0x040032DE RID: 13022
		public int FinalMissingConsumers;

		// Token: 0x040032DF RID: 13023
		public int FinalMissingProducers;

		// Token: 0x040032E0 RID: 13024
		public int Noise;

		// Token: 0x040032E1 RID: 13025
		public bool PreferWalls = true;

		// Token: 0x040032E2 RID: 13026
		public bool AvoidWalls;

		// Token: 0x040032E3 RID: 13027
		public bool ShowPathfinding;

		// Token: 0x040032E4 RID: 13028
		public bool ShowPathWeights;

		// Token: 0x040032E5 RID: 13029
		public List<Location2D> FinalMissingConsumerLocations;

		// Token: 0x040032E6 RID: 13030
		public List<Location2D> FinalMissingProducerLocations;

		// Token: 0x040032E7 RID: 13031
		[NonSerialized]
		private static List<GameObject> consumers = new List<GameObject>(16);

		// Token: 0x040032E8 RID: 13032
		[NonSerialized]
		private static List<GameObject> producers = new List<GameObject>(8);

		// Token: 0x040032E9 RID: 13033
		[NonSerialized]
		private static List<GameObject> conduits = new List<GameObject>(32);

		// Token: 0x040032EA RID: 13034
		[NonSerialized]
		private static List<GameObject> all = new List<GameObject>(32);

		// Token: 0x040032EB RID: 13035
		[NonSerialized]
		private static List<GameObject> missingConsumers = new List<GameObject>(16);

		// Token: 0x040032EC RID: 13036
		[NonSerialized]
		private static List<GameObject> missingProducers = new List<GameObject>(8);

		// Token: 0x040032ED RID: 13037
		[NonSerialized]
		private static int missingConsumerIndex = 0;

		// Token: 0x040032EE RID: 13038
		[NonSerialized]
		private static int missingProducerIndex = 0;
	}
}
