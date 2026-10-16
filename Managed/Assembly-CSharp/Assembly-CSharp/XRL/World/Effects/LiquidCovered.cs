using System;
using System.Collections.Generic;
using System.Text;
using XRL.Liquids;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D5A RID: 3418
	[Serializable]
	public class LiquidCovered : Effect
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600871B RID: 34587 RVA: 0x003083C6 File Offset: 0x003065C6
		public override string RemoveSound
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600871C RID: 34588 RVA: 0x003083C9 File Offset: 0x003065C9
		public LiquidCovered()
		{
			this.DisplayName = "covered in liquid";
			this.Duration = 1;
		}

		// Token: 0x0600871D RID: 34589 RVA: 0x003083E3 File Offset: 0x003065E3
		public LiquidCovered(LiquidVolume From, int Drams, int Duration = 1, bool Poured = false, GameObject PouredBy = null, bool FromCell = false)
			: this()
		{
			this.Liquid = From.Split(Drams);
			this.Duration = Duration;
			this.Poured = Poured;
			this.PouredBy = PouredBy;
			this.FromCell = FromCell;
		}

		// Token: 0x0600871E RID: 34590 RVA: 0x00308417 File Offset: 0x00306617
		public LiquidCovered(string LiquidSpec, int Drams, int Duration = 1, bool Poured = false, GameObject PouredBy = null, bool FromCell = false)
			: this()
		{
			this.Liquid = new LiquidVolume(LiquidSpec, Drams);
			this.Duration = Duration;
			this.Poured = Poured;
			this.PouredBy = PouredBy;
			this.FromCell = FromCell;
		}

		// Token: 0x0600871F RID: 34591 RVA: 0x0030844C File Offset: 0x0030664C
		public override int GetEffectType()
		{
			int num = 67108896;
			if (this.Liquid != null && this.Liquid.ConsiderLiquidDangerousToContact())
			{
				num |= 33554432;
			}
			return num;
		}

		// Token: 0x06008720 RID: 34592 RVA: 0x00308480 File Offset: 0x00306680
		public override bool SameAs(Effect e)
		{
			LiquidCovered liquidCovered = e as LiquidCovered;
			return liquidCovered.Poured == this.Poured && liquidCovered.FromCell == this.FromCell && this.Liquid == null == (liquidCovered.Liquid == null) && (this.Liquid == null || liquidCovered.Liquid.SameAs(this.Liquid)) && liquidCovered.PouredBy == this.PouredBy && base.SameAs(e);
		}

		// Token: 0x06008721 RID: 34593 RVA: 0x003084FE File Offset: 0x003066FE
		public override bool CanApplyToStack()
		{
			return true;
		}

		// Token: 0x06008722 RID: 34594 RVA: 0x00308504 File Offset: 0x00306704
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			if (this.Liquid == null)
			{
				this.Liquid = new LiquidVolume();
			}
			IPart.Save(this.Liquid, Writer);
			Writer.WriteGameObject(this.PouredBy);
			Writer.Write(this.Poured);
			Writer.Write(this.FromCell);
		}

		// Token: 0x06008723 RID: 34595 RVA: 0x00308554 File Offset: 0x00306754
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Liquid = IPart.Load(null, Reader) as LiquidVolume;
			this.PouredBy = Reader.ReadGameObject(null);
			this.Poured = Reader.ReadBoolean();
			this.FromCell = Reader.ReadBoolean();
		}

		// Token: 0x06008724 RID: 34596 RVA: 0x0030858D File Offset: 0x0030678D
		public override string GetDescription()
		{
			if (this.Liquid == null)
			{
				return "covered in liquid";
			}
			return this.Liquid.SmearedName.Color(this.Liquid.SmearedColor);
		}

		// Token: 0x06008725 RID: 34597 RVA: 0x003085B8 File Offset: 0x003067B8
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06008726 RID: 34598 RVA: 0x003085BC File Offset: 0x003067BC
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Covered in ");
			if (this.Liquid != null)
			{
				stringBuilder.Append(this.Liquid.Volume.Things("dram", null)).Append(" of ").Append(this.Liquid.GetLiquidName());
			}
			else
			{
				stringBuilder.Append("liquid");
			}
			stringBuilder.Append('.');
			return stringBuilder.ToString();
		}

		// Token: 0x06008727 RID: 34599 RVA: 0x00308638 File Offset: 0x00306838
		public override bool Apply(GameObject Object)
		{
			if (this.Liquid == null)
			{
				return false;
			}
			bool flag = false;
			bool flag2;
			try
			{
				if (Object.HasRegisteredEvent("ApplyLiquidCovered") && !Object.FireEvent(Event.New("ApplyLiquidCovered", "Liquid", this.Liquid)))
				{
					flag2 = false;
				}
				else
				{
					this.Liquid.SmearOn(Object, this.PouredBy, this.FromCell);
					if (this.Liquid.Volume <= 0)
					{
						flag2 = false;
					}
					else
					{
						LiquidCovered effect = Object.GetEffect<LiquidCovered>();
						if (effect != null && effect.Liquid != null)
						{
							flag = true;
							effect.Liquid.MixWith(this.Liquid, null, null, null, false);
							effect.CheckCapacity();
							if (!this.FromCell && effect.FromCell)
							{
								effect.FromCell = false;
							}
							flag2 = false;
						}
						else
						{
							this.Liquid.Update();
							flag = true;
							if (Object.HasRegisteredEvent("AppliedLiquidCovered"))
							{
								Object.FireEvent(Event.New("AppliedLiquidCovered", "Liquid", this.Liquid));
							}
							if (this.Liquid.ParentObject != null)
							{
								base.LogError(string.Concat(new string[]
								{
									"liquid ",
									this.Liquid.GetLiquidDebugDesignation(),
									" for covering on ",
									Object.DebugName,
									" has parent ",
									this.Liquid.ParentObject.DebugName
								}));
							}
							flag2 = true;
						}
					}
				}
			}
			finally
			{
				if (!flag && this.Liquid.Volume > 0)
				{
					this.Liquid.FlowIntoCell(-1, Object.GetCurrentCell(), null, false, false, true);
				}
			}
			return flag2;
		}

		// Token: 0x06008728 RID: 34600 RVA: 0x003087E8 File Offset: 0x003069E8
		public void CheckCapacity()
		{
			if (this.Liquid != null)
			{
				int adsorbableDrams = this.Liquid.GetAdsorbableDrams(base.Object);
				if (this.Liquid.Volume > adsorbableDrams)
				{
					int num = this.Liquid.Volume - adsorbableDrams;
					LiquidVolume liquid = this.Liquid;
					Cell currentCell = base.Object.GetCurrentCell();
					if (!liquid.FlowIntoCell(num, currentCell, null, false, false, true))
					{
						this.Liquid.UseDrams(num);
					}
				}
			}
		}

		// Token: 0x06008729 RID: 34601 RVA: 0x00308858 File Offset: 0x00306A58
		public void ProcessDynamics()
		{
			if (this.Liquid == null || this.Liquid.Volume <= 0)
			{
				base.Object.RemoveEffect(this, true);
				return;
			}
			if (base.Object == null)
			{
				return;
			}
			Cell currentCell = base.Object.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			if (this.FromCell)
			{
				GameObject openLiquidVolume = currentCell.GetOpenLiquidVolume();
				if (openLiquidVolume != null && this.Liquid.LiquidSameAs(openLiquidVolume.LiquidVolume))
				{
					return;
				}
				this.FromCell = false;
			}
			bool flag = base.Object.HasTag("Creature");
			int liquidFluidity = this.Liquid.GetLiquidFluidity();
			int num = ((currentCell != null && currentCell.HasWadingDepthLiquid()) ? 0 : this.Liquid.GetLiquidEvaporativity());
			int num2 = ((!flag || !base.Object.IsOrganic) ? this.Liquid.GetLiquidStaining() : 0);
			int num3 = (base.Object.HasEffect<LiquidStained>() ? this.Liquid.GetLiquidCleansing() : 0);
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			if (currentCell != null)
			{
				if (currentCell.OnWorldMap())
				{
					XRLGame game = The.Game;
					if (string.IsNullOrEmpty((game != null) ? game.GetStringGameState("LastLocationOnSurface", "") : null))
					{
						goto IL_011E;
					}
				}
				for (int i = 0; i < this.Liquid.Volume; i++)
				{
					if (num3.in100())
					{
						num7++;
					}
					if (liquidFluidity.in100())
					{
						num4++;
					}
					else if (num2.in100())
					{
						num6++;
					}
					else if (num.in100())
					{
						num5++;
					}
				}
				goto IL_0281;
			}
			IL_011E:
			int num8 = liquidFluidity + num + num2 + num3;
			if (num8 > 0)
			{
				num4 = this.Liquid.Volume * liquidFluidity / num8;
				num5 = this.Liquid.Volume * num / num8;
				num6 = this.Liquid.Volume * num2 / num8;
				num7 = this.Liquid.Volume * num3 / num8;
				int num9 = num5 + num4 + num6 + num7;
				int num10 = this.Liquid.Volume - num9;
				if (num10 > 0)
				{
					BallBag<int> ballBag = new BallBag<int>(0);
					if (liquidFluidity > 0)
					{
						ballBag.Add(1, liquidFluidity);
					}
					if (num > 0)
					{
						ballBag.Add(2, num);
					}
					if (num2 > 0)
					{
						ballBag.Add(3, num2);
					}
					if (num3 > 0)
					{
						ballBag.Add(4, num3);
					}
					for (int j = 0; j < num10; j++)
					{
						int num11 = ballBag.PeekOne();
						if (num11 == 1)
						{
							num4++;
						}
						else if (num11 == 2)
						{
							num5++;
						}
						else if (num11 == 3)
						{
							num6++;
						}
						else if (num11 == 4)
						{
							num7++;
						}
					}
				}
			}
			IL_0281:
			if (num5 > 0)
			{
				this.Liquid.UseDramsByEvaporativity(num5);
			}
			if (num4 > 0)
			{
				LiquidVolume liquid = this.Liquid;
				Cell cell = currentCell;
				if (!liquid.FlowIntoCell(num4, cell, null, false, false, true))
				{
					this.Liquid.UseDrams(num4);
				}
			}
			if (num6 > 0)
			{
				this.Liquid.Stain(base.Object, num6);
			}
			if (num7 > 0)
			{
				LiquidStained effect = base.Object.GetEffect<LiquidStained>();
				if (effect != null)
				{
					effect.Cleanse(num7);
				}
			}
			if (this.Liquid.Volume <= 0)
			{
				base.Object.RemoveEffect(this, true);
				return;
			}
			if ((num4 > 0 || num5 > 0 || num6 > 0) && GlobalConfig.GetBoolSetting("LiquidCoveringsHaveWeight", false))
			{
				base.FlushWeightCaches();
			}
		}

		// Token: 0x0600872A RID: 34602 RVA: 0x00308B9C File Offset: 0x00306D9C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeTradedEvent>.ID || ID == PooledEvent<CheckAnythingToCleanEvent>.ID || ID == PooledEvent<CleanItemsEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || (ID == GetExtrinsicWeightEvent.ID && GlobalConfig.GetBoolSetting("LiquidCoveringsHaveWeight", false));
		}

		// Token: 0x0600872B RID: 34603 RVA: 0x00308BFA File Offset: 0x00306DFA
		public override bool HandleEvent(CanBeTradedEvent E)
		{
			return (this.Liquid == null || !this.Liquid.ConsiderLiquidDangerousToContact()) && base.HandleEvent(E);
		}

		// Token: 0x0600872C RID: 34604 RVA: 0x00308C1A File Offset: 0x00306E1A
		public override bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return (this.Liquid == null || (this.Liquid.ComponentLiquids.Count <= 2 && !this.Liquid.ConsiderLiquidDangerousToContact() && this.Liquid.IsLiquidUsableForCleaning())) && base.HandleEvent(E);
		}

		// Token: 0x0600872D RID: 34605 RVA: 0x00308C5A File Offset: 0x00306E5A
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 3 && this.Liquid != null && E.Object == base.Object)
			{
				E.MinValue(this.Liquid.GetLiquidElectricalConductivity());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600872E RID: 34606 RVA: 0x00308C94 File Offset: 0x00306E94
		public override bool HandleEvent(CleanItemsEvent E)
		{
			E.RegisterObject(base.Object);
			if (this.Liquid == null)
			{
				E.RegisterType("liquids");
			}
			else
			{
				BaseLiquid primaryLiquid = this.Liquid.GetPrimaryLiquid();
				if (primaryLiquid != null)
				{
					if (!primaryLiquid.EnableCleaning)
					{
						E.RegisterType(primaryLiquid.GetName(this.Liquid));
					}
					BaseLiquid secondaryLiquid = this.Liquid.GetSecondaryLiquid();
					if (secondaryLiquid != null && !secondaryLiquid.EnableCleaning)
					{
						E.RegisterType(secondaryLiquid.GetName(this.Liquid));
					}
				}
				List<string> tertiaries = this.Liquid.GetTertiaries();
				if (tertiaries != null)
				{
					foreach (string text in tertiaries)
					{
						BaseLiquid liquid = LiquidVolume.GetLiquid(text);
						if (!liquid.EnableCleaning)
						{
							E.RegisterType(liquid.GetName(this.Liquid));
						}
					}
				}
				this.Liquid.FlowIntoCell(-1, base.Object.GetCurrentCell(), E.Actor, false, false, true);
			}
			base.Object.RemoveEffect(this, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600872F RID: 34607 RVA: 0x00308DC0 File Offset: 0x00306FC0
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			if (this.Liquid != null && this.Duration > 0 && GlobalConfig.GetBoolSetting("LiquidCoveringsHaveWeight", false))
			{
				E.Weight += this.Liquid.GetLiquidWeight();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008730 RID: 34608 RVA: 0x00308E00 File Offset: 0x00307000
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Liquid != null && this.Liquid.Volume > 0 && this.Duration > 0)
			{
				this.Liquid.SmearOnTick(base.Object, this.PouredBy, this.FromCell);
				if (this.Duration <= 1)
				{
					this.ProcessDynamics();
				}
				else if (this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			else
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008731 RID: 34609 RVA: 0x00308E8C File Offset: 0x0030708C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.Liquid != null && this.Duration > 0)
			{
				this.Liquid.ProcessSmear(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008732 RID: 34610 RVA: 0x00308EB4 File Offset: 0x003070B4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && base.Object != null && base.Object.Render != null && this.Liquid != null && (!base.Object.IsPlayer() || !Options.AlwaysHPColor))
			{
				if (!this.LiquidNativeCached)
				{
					this.LiquidNative = base.Object.GetTagOrStringProperty("LiquidNative", null);
					this.LiquidNativeCached = true;
				}
				this.Liquid.RenderSmearWithNativeSupplied(E, base.Object, this.LiquidNative);
			}
			return true;
		}

		// Token: 0x06008733 RID: 34611 RVA: 0x00308F3C File Offset: 0x0030713C
		public override void WasUnstackedFrom(GameObject obj)
		{
			base.WasUnstackedFrom(obj);
			if (this.Liquid != null)
			{
				LiquidCovered effect = obj.GetEffect<LiquidCovered>();
				if (effect != null && effect.Liquid != null && effect.Liquid.Volume == this.Liquid.Volume)
				{
					int count = base.Object.Count;
					int count2 = obj.Count;
					if (count + count2 > 0)
					{
						int num = this.Liquid.Volume * count / (count + count2);
						int num2 = this.Liquid.Volume * count2 / (count + count2);
						if (num + num2 < this.Liquid.Volume)
						{
							if (count > count2)
							{
								num++;
								if (num + num2 < this.Liquid.Volume)
								{
									num2++;
								}
							}
							else
							{
								num2++;
								if (num + num2 < this.Liquid.Volume)
								{
									num++;
								}
							}
						}
						if (num > 0)
						{
							this.Liquid.Volume = num;
						}
						else
						{
							this.Liquid.Empty(false, false);
							this.Duration = 0;
						}
						if (num2 > 0)
						{
							this.Liquid.Volume = num2;
							return;
						}
						effect.Liquid.Empty(false, false);
						effect.Duration = 0;
					}
				}
			}
		}

		// Token: 0x04003A12 RID: 14866
		public bool Poured;

		// Token: 0x04003A13 RID: 14867
		public LiquidVolume Liquid;

		// Token: 0x04003A14 RID: 14868
		public GameObject PouredBy;

		// Token: 0x04003A15 RID: 14869
		public bool FromCell;

		// Token: 0x04003A16 RID: 14870
		private bool LiquidNativeCached;

		// Token: 0x04003A17 RID: 14871
		private string LiquidNative;
	}
}
