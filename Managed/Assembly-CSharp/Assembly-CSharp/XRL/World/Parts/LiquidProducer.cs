using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200105E RID: 4190
	[Serializable]
	public class LiquidProducer : IPoweredPart
	{
		// Token: 0x0600A6B7 RID: 42679 RVA: 0x003CF97B File Offset: 0x003CDB7B
		public LiquidProducer()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A6B8 RID: 42680 RVA: 0x003CF9B0 File Offset: 0x003CDBB0
		public override bool SameAs(IPart p)
		{
			LiquidProducer liquidProducer = p as LiquidProducer;
			return liquidProducer.Rate == this.Rate && liquidProducer.ChanceSkipSelf == this.ChanceSkipSelf && liquidProducer.ChanceSkipSameCell == this.ChanceSkipSameCell && !(liquidProducer.Liquid != this.Liquid) && !(liquidProducer.VariableRate != this.VariableRate) && liquidProducer.PreferCollectors == this.PreferCollectors && liquidProducer.PureOnFloor == this.PureOnFloor && liquidProducer.FillSelfOnly == this.FillSelfOnly && base.SameAs(p);
		}

		// Token: 0x0600A6B9 RID: 42681 RVA: 0x003CFA55 File Offset: 0x003CDC55
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowLiquidCollectionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ProducesLiquidEvent.ID;
		}

		// Token: 0x0600A6BA RID: 42682 RVA: 0x003CFA7B File Offset: 0x003CDC7B
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600A6BB RID: 42683 RVA: 0x003CFA94 File Offset: 0x003CDC94
		public override bool HandleEvent(ProducesLiquidEvent E)
		{
			return !(E.Liquid == this.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600A6BC RID: 42684 RVA: 0x003CFAB2 File Offset: 0x003CDCB2
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.VariableRate))
			{
				this.Rate = this.VariableRate.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6BD RID: 42685 RVA: 0x003CFAD9 File Offset: 0x003CDCD9
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A6BE RID: 42686 RVA: 0x003CFADC File Offset: 0x003CDCDC
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Process(Amount);
		}

		// Token: 0x0600A6BF RID: 42687 RVA: 0x003CFAE5 File Offset: 0x003CDCE5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A6C0 RID: 42688 RVA: 0x003CFAE8 File Offset: 0x003CDCE8
		private void Process(int Turns = 1)
		{
			if (this.IsNeeded() && base.IsReady(true, false, true, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.Tick += Turns;
				while (this.Tick >= this.Rate)
				{
					this.Tick -= this.Rate;
					if (string.IsNullOrEmpty(this.ConsumesLiquid) || base.ConsumeLiquid(this.ConsumesLiquid, this.LiquidConsumptionAmount, !this.LiquidMustBePure))
					{
						this.DistributeLiquid();
					}
					if (!string.IsNullOrEmpty(this.VariableRate))
					{
						this.Rate = this.VariableRate.RollCached();
					}
				}
			}
		}

		// Token: 0x0600A6C1 RID: 42689 RVA: 0x003CFBAC File Offset: 0x003CDDAC
		public bool IsNeeded()
		{
			if (!this.FillSelfOnly)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null && !currentCell.OnWorldMap())
				{
					return true;
				}
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && liquidVolume.Volume >= liquidVolume.MaxVolume)
			{
				if (liquidVolume.IsPureLiquid(this.Liquid, false))
				{
					return false;
				}
				if (string.IsNullOrEmpty(this.ConsumesLiquid))
				{
					return false;
				}
				if (!liquidVolume.ComponentLiquids.ContainsKey(this.ConsumesLiquid))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600A6C2 RID: 42690 RVA: 0x003CFC2D File Offset: 0x003CDE2D
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.Liquid == LiquidType || this.WantsLiquidCollection(LiquidType);
		}

		// Token: 0x0600A6C3 RID: 42691 RVA: 0x003CFC48 File Offset: 0x003CDE48
		public bool DistributeLiquid()
		{
			LiquidProducer.<>c__DisplayClass22_0 CS$<>8__locals1 = new LiquidProducer.<>c__DisplayClass22_0();
			CS$<>8__locals1.<>4__this = this;
			if (!LiquidProducer.produced.ComponentLiquids.ContainsKey(this.Liquid) || LiquidProducer.produced.ComponentLiquids.Count > 1)
			{
				if (LiquidProducer.produced.ComponentLiquids.Count > 0)
				{
					LiquidProducer.produced.ComponentLiquids.Clear();
				}
				LiquidProducer.produced.ComponentLiquids.Add(this.Liquid, 1000);
			}
			LiquidProducer.produced.MaxVolume = 1;
			LiquidProducer.produced.Volume = 1;
			if (this.FillSelfOnly || !this.ChanceSkipSelf.in100())
			{
				CS$<>8__locals1.V = this.ParentObject.LiquidVolume;
				if (CS$<>8__locals1.V != null && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
				{
					CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
					return true;
				}
			}
			if (this.FillSelfOnly)
			{
				return false;
			}
			CS$<>8__locals1.C = this.ParentObject.GetCurrentCell();
			if (CS$<>8__locals1.C == null || CS$<>8__locals1.C.ParentZone.IsWorldMap())
			{
				return false;
			}
			if (!this.ChanceSkipSameCell.in100() && !CS$<>8__locals1.C.IsSolid(true))
			{
				CS$<>8__locals1.result = CS$<>8__locals1.C.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
				{
					CS$<>8__locals1.V = GO.LiquidVolume;
					if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
					{
						CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
						return false;
					}
					return true;
				});
				if (!CS$<>8__locals1.result)
				{
					return true;
				}
				CS$<>8__locals1.result = CS$<>8__locals1.C.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
				{
					CS$<>8__locals1.V = GO.LiquidVolume;
					if (CS$<>8__locals1.V.MaxVolume == -1)
					{
						CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
						return false;
					}
					return true;
				});
				if (!CS$<>8__locals1.result)
				{
					return true;
				}
				GameObject gameObject = GameObject.Create("Water");
				CS$<>8__locals1.V = gameObject.LiquidVolume;
				CS$<>8__locals1.V.Volume = LiquidProducer.produced.Volume;
				if (this.PureOnFloor || string.IsNullOrEmpty(CS$<>8__locals1.C.GroundLiquid))
				{
					CS$<>8__locals1.V.InitialLiquid = this.Liquid + "-1000";
				}
				else
				{
					CS$<>8__locals1.V.InitialLiquid = CS$<>8__locals1.C.GroundLiquid;
					CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
				}
				CS$<>8__locals1.C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return true;
			}
			else
			{
				CS$<>8__locals1.num = 0;
				CS$<>8__locals1.result = CS$<>8__locals1.C.ForeachLocalAdjacentCell(delegate(Cell AC)
				{
					if (CS$<>8__locals1.<>4__this.PreferCollectors)
					{
						string text = "LiquidVolume";
						Predicate<GameObject> predicate;
						if ((predicate = CS$<>8__locals1.<>9__3) == null)
						{
							predicate = (CS$<>8__locals1.<>9__3 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
								{
									CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals1.result = AC.ForeachObjectWithPart(text, predicate);
						if (!CS$<>8__locals1.result)
						{
							return false;
						}
					}
					if (!AC.IsSolid(true))
					{
						int num = CS$<>8__locals1.num;
						CS$<>8__locals1.num = num + 1;
					}
					return true;
				});
				if (!CS$<>8__locals1.result)
				{
					return true;
				}
				if (CS$<>8__locals1.num > 0)
				{
					int select = Stat.Random(1, CS$<>8__locals1.num);
					int pos = 0;
					CS$<>8__locals1.result = CS$<>8__locals1.C.ForeachLocalAdjacentCell(delegate(Cell AC)
					{
						int num2 = pos + 1;
						pos = num2;
						if (num2 != select)
						{
							return true;
						}
						LiquidProducer.<>c__DisplayClass22_0 CS$<>8__locals3 = CS$<>8__locals1;
						string text2 = "LiquidVolume";
						Predicate<GameObject> predicate2;
						if ((predicate2 = CS$<>8__locals1.<>9__5) == null)
						{
							predicate2 = (CS$<>8__locals1.<>9__5 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
								{
									CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals3.result = AC.ForeachObjectWithPart(text2, predicate2);
						if (!CS$<>8__locals1.result)
						{
							return false;
						}
						LiquidProducer.<>c__DisplayClass22_0 CS$<>8__locals4 = CS$<>8__locals1;
						string text3 = "LiquidVolume";
						Predicate<GameObject> predicate3;
						if ((predicate3 = CS$<>8__locals1.<>9__6) == null)
						{
							predicate3 = (CS$<>8__locals1.<>9__6 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume == -1)
								{
									CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals4.result = AC.ForeachObjectWithPart(text3, predicate3);
						if (!CS$<>8__locals1.result)
						{
							return false;
						}
						GameObject gameObject2 = GameObject.Create("Water");
						CS$<>8__locals1.V = gameObject2.LiquidVolume;
						CS$<>8__locals1.V.Volume = LiquidProducer.produced.Volume;
						if (CS$<>8__locals1.<>4__this.PureOnFloor || string.IsNullOrEmpty(CS$<>8__locals1.C.GroundLiquid))
						{
							CS$<>8__locals1.V.InitialLiquid = CS$<>8__locals1.<>4__this.Liquid + "-1000";
						}
						else
						{
							CS$<>8__locals1.V.InitialLiquid = CS$<>8__locals1.C.GroundLiquid;
							CS$<>8__locals1.V.MixWith(LiquidProducer.produced, null, null, null, false);
						}
						AC.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return false;
					});
					if (!CS$<>8__locals1.result)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x040044E5 RID: 17637
		public int Tick;

		// Token: 0x040044E6 RID: 17638
		public int Rate = 1000;

		// Token: 0x040044E7 RID: 17639
		public int ChanceSkipSelf;

		// Token: 0x040044E8 RID: 17640
		public int ChanceSkipSameCell;

		// Token: 0x040044E9 RID: 17641
		public string Liquid = "water";

		// Token: 0x040044EA RID: 17642
		public string VariableRate;

		// Token: 0x040044EB RID: 17643
		public bool PreferCollectors;

		// Token: 0x040044EC RID: 17644
		public bool PureOnFloor;

		// Token: 0x040044ED RID: 17645
		public bool FillSelfOnly;

		// Token: 0x040044EE RID: 17646
		[NonSerialized]
		private static LiquidVolume produced = new LiquidVolume();
	}
}
