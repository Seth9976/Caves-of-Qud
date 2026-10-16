using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000FBE RID: 4030
	[Serializable]
	public class Gas : IPart
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600A02E RID: 41006 RVA: 0x003A4AA3 File Offset: 0x003A2CA3
		// (set) Token: 0x0600A02D RID: 41005 RVA: 0x003A4A9A File Offset: 0x003A2C9A
		public GameObject Creator
		{
			get
			{
				GameObject.Validate(ref this._Creator);
				return this._Creator;
			}
			set
			{
				this._Creator = value;
			}
		}

		// Token: 0x0600A02F RID: 41007 RVA: 0x003A4AB7 File Offset: 0x003A2CB7
		public Gas()
		{
			this.FrameOffset = Stat.Random(0, 60);
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600A030 RID: 41008 RVA: 0x003A4AE7 File Offset: 0x003A2CE7
		// (set) Token: 0x0600A031 RID: 41009 RVA: 0x003A4AF0 File Offset: 0x003A2CF0
		public int Density
		{
			get
			{
				return this._Density;
			}
			set
			{
				if (this._Density != value && this.ParentObject != null && this.ParentObject.HasRegisteredEvent("DensityChange"))
				{
					this.ParentObject.FireEvent(Event.New("DensityChange", "OldValue", this._Density, "NewValue", value));
				}
				this._Density = value;
			}
		}

		// Token: 0x0600A032 RID: 41010 RVA: 0x003A4B50 File Offset: 0x003A2D50
		public override void Initialize()
		{
			if (this.ParentObject.Render != null)
			{
				this.ParentObject.Render.RenderString = "°";
				if (!this.ColorString.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.ColorString;
				}
			}
		}

		// Token: 0x0600A033 RID: 41011 RVA: 0x003A4BA4 File Offset: 0x003A2DA4
		public override bool SameAs(IPart Part)
		{
			Gas gas = Part as Gas;
			return gas._Density == this._Density && gas.Level == this.Level && gas.Seeping == this.Seeping && gas.Stable == this.Stable && !(gas.GasType != this.GasType) && !(gas.ColorString != this.ColorString) && gas._Creator == this._Creator;
		}

		// Token: 0x0600A034 RID: 41012 RVA: 0x003A4C33 File Offset: 0x003A2E33
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A035 RID: 41013 RVA: 0x003A4C6C File Offset: 0x003A2E6C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Density", this.Density);
			E.AddEntry(this, "Level", this.Level);
			E.AddEntry(this, "Seeping", this.Seeping);
			E.AddEntry(this, "Stable", this.Stable);
			E.AddEntry(this, "GasType", this.GasType);
			E.AddEntry(this, "ColorString", this.ColorString);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A036 RID: 41014 RVA: 0x003A4CEC File Offset: 0x003A2EEC
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Creator = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A037 RID: 41015 RVA: 0x003A4CFC File Offset: 0x003A2EFC
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(3);
			return false;
		}

		// Token: 0x0600A038 RID: 41016 RVA: 0x003A4D06 File Offset: 0x003A2F06
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600A039 RID: 41017 RVA: 0x003A4D11 File Offset: 0x003A2F11
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			GameObject.Validate(ref this._Creator);
			return !this.CheckMergeGas(E.Object) && base.HandleEvent(E);
		}

		// Token: 0x0600A03A RID: 41018 RVA: 0x003A4D36 File Offset: 0x003A2F36
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A03B RID: 41019 RVA: 0x003A4D39 File Offset: 0x003A2F39
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ProcessGasBehavior() && Amount > 1)
			{
				this.Disperse(Amount);
			}
		}

		// Token: 0x0600A03C RID: 41020 RVA: 0x003A4D50 File Offset: 0x003A2F50
		public override bool Render(RenderEvent E)
		{
			if (this.ColorString != null)
			{
				E.ColorString = this.ColorString;
			}
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
			if (num < 15)
			{
				E.RenderString = "◄";
				E.Tile = "Tiles2/gas_0.png";
			}
			else if (num < 30)
			{
				E.RenderString = "▒";
				E.Tile = "Tiles2/gas_1.png";
			}
			else if (num < 45)
			{
				E.RenderString = "▓";
				E.Tile = "Tiles2/gas_2.png";
			}
			else
			{
				E.RenderString = "█";
				E.Tile = "Tiles2/gas_3.png";
			}
			if (this.Density < 50 && this.GasType != "Cryo")
			{
				E.BackgroundString = "^k";
			}
			return true;
		}

		// Token: 0x0600A03D RID: 41021 RVA: 0x003A4E18 File Offset: 0x003A3018
		public bool ProcessGasBehavior()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			int num = 0;
			string text = null;
			Zone parentZone = currentCell.ParentZone;
			if (parentZone != null && GlobalConfig.GetBoolSetting("WindAffectsGasDispersal", false))
			{
				num = parentZone.CurrentWindSpeed;
				text = parentZone.CurrentWindDirection;
			}
			GameObject.Validate(ref this._Creator);
			if (this.Density > 10)
			{
				if (!this.Stable)
				{
					this.Density -= this.GetDispersalRate(1);
				}
				if ((25 + num).in100())
				{
					int i = 0;
					int num2 = Stat.Random(1 + num / 30, 4 + num / 20);
					while (i < num2)
					{
						string text2 = ((num.in100() && 90.in100()) ? text : null) ?? Directions.GetRandomDirection();
						Cell localCellFromDirection = currentCell.GetLocalCellFromDirection(text2, true);
						if (localCellFromDirection != null)
						{
							bool flag = false;
							List<GameObject> list = null;
							if (this.Seeping || !localCellFromDirection.IsSolidFor(this.ParentObject, false))
							{
								if (this.Stable)
								{
									flag = localCellFromDirection.IsEmpty();
									if (localCellFromDirection.GetObjectCountWithPart("Gas") > 0)
									{
										if (list == null)
										{
											list = Event.NewGameObjectList();
											localCellFromDirection.GetObjectsWithPart("Gas", list);
										}
										int j = 0;
										int count = list.Count;
										while (j < count)
										{
											GameObject gameObject = list[j];
											if (gameObject.PhaseMatches(this.ParentObject))
											{
												this.ParentObject.FireEvent(Event.New("GasPressureOut", "Object", gameObject));
												gameObject.FireEvent(Event.New("GasPressureIn", "Object", this.ParentObject));
												flag = false;
											}
											j++;
										}
									}
								}
								else
								{
									flag = true;
								}
							}
							if (flag)
							{
								int num3 = Stat.Random(1, Math.Min(this.Density, 30));
								if (list == null)
								{
									list = Event.NewGameObjectList();
									localCellFromDirection.GetObjectsWithPart("Gas", list);
								}
								bool flag2 = false;
								int k = 0;
								int count2 = list.Count;
								while (k < count2)
								{
									if (this.CheckMergeToGas(list[k], num3))
									{
										flag2 = true;
										break;
									}
									k++;
								}
								if (!flag2)
								{
									GameObject gameObject2 = GameObject.Create(this.ParentObject.Blueprint);
									Gas gas;
									if (gameObject2.TryGetPart<Gas>(out gas))
									{
										gas.Creator = this.Creator;
										gas.Density = num3;
										gas.ColorString = this.ColorString;
										gas.Level = this.Level;
										gas.Seeping = this.Seeping;
									}
									this.Density -= num3;
									gameObject2.FireEvent(Event.New("GasSpawned", "Parent", this.ParentObject));
									Temporary.CarryOver(this.ParentObject, gameObject2, false);
									Phase.carryOver(this.ParentObject, gameObject2);
									localCellFromDirection.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
								if (this.Density <= 0)
								{
									break;
								}
							}
						}
						i++;
					}
				}
			}
			if (this.Density <= 0 || (this.Density <= 10 && (50 + num).in100()))
			{
				this.Dissipate();
				return false;
			}
			return true;
		}

		// Token: 0x0600A03E RID: 41022 RVA: 0x003A5130 File Offset: 0x003A3330
		public void Dissipate()
		{
			if (this.ParentObject.IsPlayer() || this.ParentObject.HasTag("Creature"))
			{
				this.ParentObject.Die(null, "dissipation", "You dissipated.", this.ParentObject.It + " @@dissipated.", true, null, null, false, false, null, null, null);
				return;
			}
			this.ParentObject.Obliterate(null, true, null);
		}

		// Token: 0x0600A03F RID: 41023 RVA: 0x003A51A0 File Offset: 0x003A33A0
		public void Disperse(int Factor = 1)
		{
			this.Density -= this.GetDispersalRate(Factor);
			if (this.Density < 0 || (this.Density <= 10 && 50.in100()))
			{
				this.Dissipate();
			}
		}

		// Token: 0x0600A040 RID: 41024 RVA: 0x003A51D8 File Offset: 0x003A33D8
		public int GetDispersalRate(int Factor = 1)
		{
			int num = Stat.Random(Factor, Factor * 3);
			if (this.Creator != null && this.Creator.DistanceTo(this.ParentObject) <= 1 && this.Creator.HasRegisteredEvent("CreatorModifyGasDispersal"))
			{
				Event @event = new Event("CreatorModifyGasDispersal", "Rate", num);
				this.Creator.FireEvent(@event);
				num = @event.GetIntParameter("Rate", 0);
			}
			return num;
		}

		// Token: 0x0600A041 RID: 41025 RVA: 0x003A5249 File Offset: 0x003A3449
		private bool IsGasMergeable(Gas gas)
		{
			return gas != null && gas.GasType == this.GasType && gas.ColorString == this.ColorString;
		}

		// Token: 0x0600A042 RID: 41026 RVA: 0x003A5274 File Offset: 0x003A3474
		private void MergeGas(Gas gas)
		{
			this.Density += gas.Density;
			if (gas.Level > this.Level)
			{
				this.Level = gas.Level;
			}
			if (gas.Seeping && !this.Seeping)
			{
				this.Seeping = true;
			}
			if (gas.Creator != null && this.Creator == null)
			{
				this.Creator = gas.Creator;
			}
		}

		// Token: 0x0600A043 RID: 41027 RVA: 0x003A52E4 File Offset: 0x003A34E4
		private void MergeToGas(Gas gas, int Contribution)
		{
			if (Contribution > this.Density)
			{
				Contribution = this.Density;
			}
			gas.Density += Contribution;
			if (this.Level > gas.Level)
			{
				gas.Level = this.Level;
			}
			if (this.Seeping && !gas.Seeping)
			{
				gas.Seeping = true;
			}
			if (this.Creator != null && gas.Creator == null)
			{
				gas.Creator = this.Creator;
			}
			this.Density -= Contribution;
		}

		// Token: 0x0600A044 RID: 41028 RVA: 0x003A536C File Offset: 0x003A356C
		private bool CheckMergeGas(GameObject obj)
		{
			Gas gas;
			if (obj != this.ParentObject && obj.TryGetPart<Gas>(out gas) && this.IsGasMergeable(gas) && obj.PhaseMatches(this.ParentObject))
			{
				this.MergeGas(gas);
				gas.Dissipate();
				return true;
			}
			return false;
		}

		// Token: 0x0600A045 RID: 41029 RVA: 0x003A53B4 File Offset: 0x003A35B4
		private bool CheckMergeToGas(GameObject obj, int Contribution)
		{
			Gas gas;
			if (obj != this.ParentObject && obj.TryGetPart<Gas>(out gas) && this.IsGasMergeable(gas) && obj.PhaseMatches(this.ParentObject))
			{
				this.MergeToGas(gas, Contribution);
				return true;
			}
			return false;
		}

		// Token: 0x04004297 RID: 17047
		public int _Density = 100;

		// Token: 0x04004298 RID: 17048
		public int Level = 1;

		// Token: 0x04004299 RID: 17049
		public bool Seeping;

		// Token: 0x0400429A RID: 17050
		public bool Stable;

		// Token: 0x0400429B RID: 17051
		public string GasType = "BaseGas";

		// Token: 0x0400429C RID: 17052
		public string ColorString;

		// Token: 0x0400429D RID: 17053
		[NonSerialized]
		private int FrameOffset;

		// Token: 0x0400429E RID: 17054
		public GameObject _Creator;
	}
}
