using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010C3 RID: 4291
	[Serializable]
	public class LeaksFluid : IPoweredPart
	{
		// Token: 0x0600AAD2 RID: 43730 RVA: 0x003E76E0 File Offset: 0x003E58E0
		public LeaksFluid()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AAD3 RID: 43731 RVA: 0x003E7758 File Offset: 0x003E5958
		public override bool SameAs(IPart p)
		{
			LeaksFluid leaksFluid = p as LeaksFluid;
			return leaksFluid.Rate == this.Rate && leaksFluid.ChanceSmearParent == this.ChanceSmearParent && !(leaksFluid.SmearParentDuration != this.SmearParentDuration) && leaksFluid.ChanceSkipSelf == this.ChanceSkipSelf && leaksFluid.ChanceSkipSameCell == this.ChanceSkipSameCell && !(leaksFluid.Liquid != this.Liquid) && !(leaksFluid.Frequency != this.Frequency) && !(leaksFluid.Drams != this.Drams) && leaksFluid.PreferCollectors == this.PreferCollectors && leaksFluid.PureOnFloor == this.PureOnFloor && leaksFluid.FillSelfOnly == this.FillSelfOnly && leaksFluid.Message == this.Message && !(leaksFluid.MessageVerb != this.MessageVerb) && base.SameAs(p);
		}

		// Token: 0x0600AAD4 RID: 43732 RVA: 0x003E785C File Offset: 0x003E5A5C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowLiquidCollectionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ProducesLiquidEvent.ID;
		}

		// Token: 0x0600AAD5 RID: 43733 RVA: 0x003E7882 File Offset: 0x003E5A82
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return this.IsLiquidCollectionCompatible(E.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600AAD6 RID: 43734 RVA: 0x003E789B File Offset: 0x003E5A9B
		public override bool HandleEvent(ProducesLiquidEvent E)
		{
			return !(E.Liquid == this.Liquid) && base.HandleEvent(E);
		}

		// Token: 0x0600AAD7 RID: 43735 RVA: 0x003E78B9 File Offset: 0x003E5AB9
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.Frequency))
			{
				this.Rate = this.Frequency.RollCached();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAD8 RID: 43736 RVA: 0x003E78E0 File Offset: 0x003E5AE0
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AAD9 RID: 43737 RVA: 0x003E78E3 File Offset: 0x003E5AE3
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Process(Amount);
		}

		// Token: 0x0600AADA RID: 43738 RVA: 0x003E78EC File Offset: 0x003E5AEC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AADB RID: 43739 RVA: 0x003E78F0 File Offset: 0x003E5AF0
		private void Process(int Turns = 1)
		{
			if (this.IsNeeded() && base.IsReady(true, false, true, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				int num = this.Tick + 1;
				this.Tick = num;
				if (num >= this.Rate)
				{
					if (string.IsNullOrEmpty(this.ConsumesLiquid) || base.ConsumeLiquid(this.ConsumesLiquid, this.LiquidConsumptionAmount, this.LiquidMustBePure))
					{
						this.DistributeLiquid();
					}
					this.Tick = 0;
					if (!string.IsNullOrEmpty(this.Frequency))
					{
						this.Rate = this.Frequency.RollCached();
					}
				}
			}
		}

		// Token: 0x0600AADC RID: 43740 RVA: 0x003E799C File Offset: 0x003E5B9C
		public bool DistributeLiquid()
		{
			LeaksFluid.<>c__DisplayClass25_0 CS$<>8__locals1 = new LeaksFluid.<>c__DisplayClass25_0();
			CS$<>8__locals1.<>4__this = this;
			if (!LeaksFluid.produced.ComponentLiquids.ContainsKey(this.Liquid) || LeaksFluid.produced.ComponentLiquids.Count > 1)
			{
				if (LeaksFluid.produced.ComponentLiquids.Count > 0)
				{
					LeaksFluid.produced.ComponentLiquids.Clear();
				}
				LeaksFluid.produced.ComponentLiquids.Add(this.Liquid, 1000);
			}
			LeaksFluid.produced.MaxVolume = this.Drams.RollCached();
			LeaksFluid.produced.Volume = LeaksFluid.produced.MaxVolume;
			if (this.FillSelfOnly || this.ChanceSkipSelf.in100())
			{
				CS$<>8__locals1.V = this.ParentObject.LiquidVolume;
				if (CS$<>8__locals1.V != null && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
				{
					CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
					return true;
				}
			}
			if (this.FillSelfOnly)
			{
				return false;
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return false;
			}
			string text = ((LeaksFluid.produced.Volume == 1) ? "dram" : "drams");
			if (this.Message && this.ParentObject.IsVisible())
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					this.ParentObject.Does(this.MessageVerb, int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" ",
					LeaksFluid.produced.Volume.ToString(),
					" ",
					text,
					" of ",
					LeaksFluid.produced.GetLiquidName(),
					"."
				}), null, true);
			}
			if (this.ChanceSmearParent.in100())
			{
				this.ParentObject.ApplyEffect(new LiquidCovered(LeaksFluid.produced, 1, this.SmearParentDuration.RollCached(), true, null, false), null);
			}
			if (this.ChanceSkipSameCell < 100 && !currentCell.IsSolid(true) && !this.ChanceSkipSameCell.in100())
			{
				CS$<>8__locals1.result = currentCell.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
				{
					CS$<>8__locals1.V = GO.LiquidVolume;
					if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
					{
						CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
						return false;
					}
					return true;
				});
				if (!CS$<>8__locals1.result)
				{
					return true;
				}
				CS$<>8__locals1.result = currentCell.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
				{
					CS$<>8__locals1.V = GO.LiquidVolume;
					if (CS$<>8__locals1.V.MaxVolume == -1)
					{
						CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
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
				CS$<>8__locals1.V.Volume = LeaksFluid.produced.Volume;
				if (this.PureOnFloor || string.IsNullOrEmpty(currentCell.GroundLiquid))
				{
					CS$<>8__locals1.V.InitialLiquid = this.Liquid + "-1000";
				}
				else
				{
					CS$<>8__locals1.V.InitialLiquid = currentCell.GroundLiquid + "-1000";
					CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
				}
				currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return true;
			}
			else
			{
				CS$<>8__locals1.num = 0;
				CS$<>8__locals1.result = currentCell.ForeachLocalAdjacentCell(delegate(Cell AC)
				{
					if (CS$<>8__locals1.<>4__this.PreferCollectors)
					{
						string text2 = "LiquidVolume";
						Predicate<GameObject> predicate;
						if ((predicate = CS$<>8__locals1.<>9__3) == null)
						{
							predicate = (CS$<>8__locals1.<>9__3 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
								{
									CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals1.result = AC.ForeachObjectWithPart(text2, predicate);
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
					CS$<>8__locals1.result = currentCell.ForeachLocalAdjacentCell(delegate(Cell AC)
					{
						int num2 = pos + 1;
						pos = num2;
						if (num2 != select)
						{
							return true;
						}
						LeaksFluid.<>c__DisplayClass25_0 CS$<>8__locals3 = CS$<>8__locals1;
						string text3 = "LiquidVolume";
						Predicate<GameObject> predicate2;
						if ((predicate2 = CS$<>8__locals1.<>9__5) == null)
						{
							predicate2 = (CS$<>8__locals1.<>9__5 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume != -1 && CS$<>8__locals1.V.Collector && CS$<>8__locals1.V.Volume < CS$<>8__locals1.V.MaxVolume)
								{
									CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals3.result = AC.ForeachObjectWithPart(text3, predicate2);
						if (!CS$<>8__locals1.result)
						{
							return false;
						}
						LeaksFluid.<>c__DisplayClass25_0 CS$<>8__locals4 = CS$<>8__locals1;
						string text4 = "LiquidVolume";
						Predicate<GameObject> predicate3;
						if ((predicate3 = CS$<>8__locals1.<>9__6) == null)
						{
							predicate3 = (CS$<>8__locals1.<>9__6 = delegate(GameObject GO)
							{
								CS$<>8__locals1.V = GO.LiquidVolume;
								if (CS$<>8__locals1.V.MaxVolume == -1)
								{
									CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
									return false;
								}
								return true;
							});
						}
						CS$<>8__locals4.result = AC.ForeachObjectWithPart(text4, predicate3);
						if (!CS$<>8__locals1.result)
						{
							return false;
						}
						GameObject gameObject2 = GameObject.Create("Water");
						CS$<>8__locals1.V = gameObject2.LiquidVolume;
						CS$<>8__locals1.V.Volume = LeaksFluid.produced.Volume;
						if (CS$<>8__locals1.<>4__this.PureOnFloor || string.IsNullOrEmpty(AC.GroundLiquid))
						{
							CS$<>8__locals1.V.InitialLiquid = CS$<>8__locals1.<>4__this.Liquid + "-1000";
						}
						else
						{
							CS$<>8__locals1.V.InitialLiquid = AC.GroundLiquid + "-1000";
							CS$<>8__locals1.V.MixWith(LeaksFluid.produced, null, null, null, false);
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

		// Token: 0x0600AADD RID: 43741 RVA: 0x003E7D80 File Offset: 0x003E5F80
		public bool IsNeeded()
		{
			if (!this.FillSelfOnly)
			{
				return true;
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && liquidVolume.Volume >= liquidVolume.MaxVolume)
			{
				if (liquidVolume.ComponentLiquids.Count == 1 && liquidVolume.ComponentLiquids.ContainsKey(this.Liquid))
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

		// Token: 0x0600AADE RID: 43742 RVA: 0x003E7DFC File Offset: 0x003E5FFC
		public bool IsLiquidCollectionCompatible(string LiquidType)
		{
			return this.Liquid == LiquidType || this.WantsLiquidCollection(LiquidType);
		}

		// Token: 0x04004666 RID: 18022
		public int Tick;

		// Token: 0x04004667 RID: 18023
		public int Rate = -1;

		// Token: 0x04004668 RID: 18024
		public int ChanceSmearParent = 100;

		// Token: 0x04004669 RID: 18025
		public string SmearParentDuration = "100-200";

		// Token: 0x0400466A RID: 18026
		public int ChanceSkipSelf;

		// Token: 0x0400466B RID: 18027
		public int ChanceSkipSameCell;

		// Token: 0x0400466C RID: 18028
		public string Liquid = "water";

		// Token: 0x0400466D RID: 18029
		public string Frequency = "300-400";

		// Token: 0x0400466E RID: 18030
		public string Drams = "2-3";

		// Token: 0x0400466F RID: 18031
		public bool PreferCollectors;

		// Token: 0x04004670 RID: 18032
		public bool PureOnFloor;

		// Token: 0x04004671 RID: 18033
		public bool FillSelfOnly;

		// Token: 0x04004672 RID: 18034
		public bool Message = true;

		// Token: 0x04004673 RID: 18035
		public string MessageVerb = "leak";

		// Token: 0x04004674 RID: 18036
		[NonSerialized]
		private static LiquidVolume produced = new LiquidVolume();
	}
}
