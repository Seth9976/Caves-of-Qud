using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.Messages;
using XRL.Rules;
using XRL.Wish;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200054E RID: 1358
	[HasWishCommand]
	[Serializable]
	public class BaseLiquid
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x0011E94D File Offset: 0x0011CB4D
		public string Name
		{
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x0011E955 File Offset: 0x0011CB55
		public int Viscosity
		{
			get
			{
				return 100 - this.Fluidity;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x0011E960 File Offset: 0x0011CB60
		public string ColoredCirculatoryLossTerm
		{
			get
			{
				if (this._ColoredCirculatoryLossTerm == null)
				{
					this._ColoredCirculatoryLossTerm = string.Concat(new string[]
					{
						"{{",
						this.GetColor(),
						"|",
						this.CirculatoryLossTerm,
						"}}"
					});
				}
				return this._ColoredCirculatoryLossTerm;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x0011E9B8 File Offset: 0x0011CBB8
		public string ColoredCirculatoryLossNoun
		{
			get
			{
				if (this._ColoredCirculatoryLossNoun == null)
				{
					this._ColoredCirculatoryLossNoun = string.Concat(new string[]
					{
						"{{",
						this.GetColor(),
						"|",
						this.CirculatoryLossNoun,
						"}}"
					});
				}
				return this._ColoredCirculatoryLossNoun;
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x0011EA10 File Offset: 0x0011CC10
		public BaseLiquid(string ID)
		{
			this.ID = ID;
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0011EB08 File Offset: 0x0011CD08
		public virtual float GetValuePerDram()
		{
			return 0f;
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0011EB0F File Offset: 0x0011CD0F
		public float GetExtrinsicValuePerDram(bool Pure)
		{
			return this.GetValuePerDram() * (Pure ? this.GetPureLiquidValueMultipler() : 1f);
		}

		/// <summary>
		///     When the liquid is pure, multiple ValuePerDram by this additional factor for value calculations.
		/// </summary>
		// Token: 0x060039F6 RID: 14838 RVA: 0x0011EB28 File Offset: 0x0011CD28
		public virtual float GetPureLiquidValueMultipler()
		{
			return 1f;
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x0011EB2F File Offset: 0x0011CD2F
		public virtual string GetPreparedCookingIngredient()
		{
			return "";
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0011EB36 File Offset: 0x0011CD36
		public virtual string GetName()
		{
			return this.GetName(null);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x0011EB3F File Offset: 0x0011CD3F
		public virtual string GetName(LiquidVolume Liquid)
		{
			return "liquid";
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x0011EB46 File Offset: 0x0011CD46
		public virtual string GetAdjective(LiquidVolume Liquid)
		{
			return "liquidy";
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x0011EB4D File Offset: 0x0011CD4D
		public virtual string GetWaterRitualName()
		{
			return "liquid";
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x0011EB54 File Offset: 0x0011CD54
		public virtual string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "damp";
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x0011EB5B File Offset: 0x0011CD5B
		public virtual string GetSmearedName(LiquidVolume Liquid)
		{
			return "damp";
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x0011EB62 File Offset: 0x0011CD62
		public virtual string GetStainedName(LiquidVolume Liquid)
		{
			return this.GetName(Liquid);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x0011EB6B File Offset: 0x0011CD6B
		public virtual bool SafeContainer(GameObject GO)
		{
			return true;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0011EB6E File Offset: 0x0011CD6E
		[Obsolete("Replaced by Froze(LiquidVolume, GameObject), retained for mod compatibility")]
		public virtual bool Froze(LiquidVolume Liquid)
		{
			return true;
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0011EB74 File Offset: 0x0011CD74
		public virtual bool Froze(LiquidVolume Liquid, GameObject By)
		{
			if ((this.SlipperyWhenFrozen || this.SlipperyWhenWet || this.StickyWhenFrozen || this.StickyWhenWet) && Liquid != null)
			{
				GameObject parentObject = Liquid.ParentObject;
				if (parentObject != null)
				{
					Cell currentCell = parentObject.CurrentCell;
					if (currentCell != null)
					{
						currentCell.FlushNavigationCache();
					}
				}
			}
			return true;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x0011EBC4 File Offset: 0x0011CDC4
		public virtual bool Thawed(LiquidVolume Liquid, GameObject By)
		{
			if ((this.SlipperyWhenFrozen || this.SlipperyWhenWet || this.StickyWhenFrozen || this.StickyWhenWet) && Liquid != null)
			{
				GameObject parentObject = Liquid.ParentObject;
				if (parentObject != null)
				{
					Cell currentCell = parentObject.CurrentCell;
					if (currentCell != null)
					{
						currentCell.FlushNavigationCache();
					}
				}
			}
			return true;
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x0011EC11 File Offset: 0x0011CE11
		[Obsolete("Replaced by Vaporized(LiquidVolume, GameObject), retained for mod compatibility")]
		public virtual bool Vaporized(LiquidVolume Liquid)
		{
			return true;
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x0011EC14 File Offset: 0x0011CE14
		public virtual bool Vaporized(LiquidVolume Liquid, GameObject By)
		{
			if (!this.VaporObject.IsNullOrEmpty())
			{
				Cell currentCell = Liquid.ParentObject.GetCurrentCell();
				if (currentCell != null)
				{
					GameObject gameObject = GameObject.Create(this.VaporObject);
					Gas part = gameObject.GetPart<Gas>();
					if (part != null)
					{
						part.Density = Liquid.Amount(this.ID) / 20;
						part.Creator = By;
					}
					currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return this.Vaporized(Liquid);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x0011EC8D File Offset: 0x0011CE8D
		public virtual bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			return true;
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0011EC90 File Offset: 0x0011CE90
		public bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message)
		{
			bool flag = false;
			return this.Drank(Liquid, Volume, Target, Message, ref flag);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x0011ECAC File Offset: 0x0011CEAC
		public bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, IEvent E)
		{
			bool flag = false;
			bool flag2 = this.Drank(Liquid, Volume, Target, Message, ref flag);
			if (flag)
			{
				E.RequestInterfaceExit();
			}
			return flag2;
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x0011ECD2 File Offset: 0x0011CED2
		public virtual void SmearOnTick(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x0011ECD4 File Offset: 0x0011CED4
		public virtual void SmearOn(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x0011ECD8 File Offset: 0x0011CED8
		public void ApplyRelativeTemperature(LiquidVolume Liquid, GameObject Object, int Multiple = 1)
		{
			int temperature = Object.Temperature;
			if (this.Temperature != temperature)
			{
				int num = this.Temperature - temperature;
				int num2 = Liquid.MilliAmount(this.ID) / (20000 / num) * Multiple;
				if (num2 > 0)
				{
					int num3 = num2;
					GameObject gameObject = null;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num4 = 5;
					int? num5 = new int?(this.Temperature);
					Object.TemperatureChange(num3, gameObject, flag, flag2, flag3, flag4, num4, null, num5);
					return;
				}
				if (num2 < 0 && (Multiple != 1 || !Glotrot.IsIck(Liquid)))
				{
					int num6 = num2;
					GameObject gameObject2 = null;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					int num7 = 5;
					int? num8 = new int?(this.Temperature);
					int? num5 = null;
					Object.TemperatureChange(num6, gameObject2, flag5, flag6, flag7, flag8, num7, num8, num5);
				}
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x0011ED72 File Offset: 0x0011CF72
		public virtual void FillingContainer(GameObject Container, LiquidVolume Liquid)
		{
			this.ApplyRelativeTemperature(Liquid, Container, 1);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0011ED7D File Offset: 0x0011CF7D
		public virtual void ProcessTurns(LiquidVolume Liquid, GameObject Container, int Turns)
		{
			if (!Liquid.IsOpenVolume())
			{
				this.ApplyRelativeTemperature(Liquid, Container, Turns);
			}
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0011ED90 File Offset: 0x0011CF90
		public virtual void BeforeRender(LiquidVolume Liquid)
		{
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x0011ED92 File Offset: 0x0011CF92
		public virtual void BeforeRenderSecondary(LiquidVolume Liquid)
		{
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x0011ED94 File Offset: 0x0011CF94
		public virtual bool MixingWith(LiquidVolume Liquid, LiquidVolume NewLiquid, int Amount, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy, ref bool ExitInterface)
		{
			return true;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x0011ED97 File Offset: 0x0011CF97
		public virtual void MixedWith(LiquidVolume Liquid, LiquidVolume NewLiquid, int Amount, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy, ref bool ExitInterface)
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x0011ED99 File Offset: 0x0011CF99
		public virtual void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0011ED9B File Offset: 0x0011CF9B
		public virtual void RenderBackgroundSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x0011ED9D File Offset: 0x0011CF9D
		public virtual void BaseRenderPrimary(LiquidVolume Liquid)
		{
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0011ED9F File Offset: 0x0011CF9F
		public virtual void BaseRenderSecondary(LiquidVolume Liquid)
		{
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x0011EDA1 File Offset: 0x0011CFA1
		public virtual void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x0011EDA3 File Offset: 0x0011CFA3
		public virtual void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x0011EDA5 File Offset: 0x0011CFA5
		public virtual void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0011EDA7 File Offset: 0x0011CFA7
		public virtual void RenderSmearSecondary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x0011EDA9 File Offset: 0x0011CFA9
		public virtual void ObjectGoingProne(LiquidVolume Liquid, GameObject GO, bool UsePopups)
		{
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x0011EDAC File Offset: 0x0011CFAC
		public virtual void ObjectEnteredCell(LiquidVolume Liquid, IObjectCellInteractionEvent E)
		{
			if ((this.SlipperyWhenFrozen || this.SlipperyWhenWet || this.StickyWhenFrozen || this.StickyWhenWet) && Liquid.IsOpenVolume() && E.Object.HasPart<Body>())
			{
				if (((this.SlipperyWhenFrozen && Liquid.ParentObject.IsFrozen()) || (this.SlipperyWhenWet && !Liquid.IsWadingDepth() && !E.Object.Slimewalking && !Liquid.ParentObject.IsFrozen())) && !E.Object.MakeSave(this.SlipperySaveStat, this.GetSlipperySaveDifficulty(Liquid, E.Object), null, null, this.SlipperySaveVs, false, false, false, false, Liquid.ParentObject))
				{
					if (!this.SlipperyMessage.IsNullOrEmpty())
					{
						string text = GameText.VariableReplace(this.SlipperyMessage, E.Object, Liquid.ParentObject, false);
						if (!text.IsNullOrEmpty())
						{
							E.Object.Physics.EmitMessage(text, ' ', false, false, false, null, null);
						}
					}
					if (!this.SlipperyParticle.IsNullOrEmpty())
					{
						E.Object.ParticleText(this.SlipperyParticle, false);
					}
					E.Object.Move(Directions.GetRandomDirection(), true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
					return;
				}
				if ((this.StickyWhenFrozen && !E.Object.Slimewalking && Liquid.ParentObject.IsFrozen()) || (this.StickyWhenWet && !E.Object.Slimewalking && !Liquid.ParentObject.IsFrozen()))
				{
					int stickySaveDifficulty = this.GetStickySaveDifficulty(Liquid, E.Object);
					if (!E.Object.MakeSave(this.StickySaveStat, stickySaveDifficulty, null, null, this.StickySaveVs, false, false, false, false, Liquid.ParentObject))
					{
						if (!this.SlipperyMessage.IsNullOrEmpty())
						{
							string text2 = GameText.VariableReplace(this.SlipperyMessage, E.Object, Liquid.ParentObject, false);
							if (!text2.IsNullOrEmpty())
							{
								E.Object.Physics.EmitMessage(text2, ' ', false, false, false, null, null);
							}
						}
						if (!this.StickyParticle.IsNullOrEmpty())
						{
							E.Object.ParticleText(this.StickyParticle, false);
						}
						Stuck stuck = new Stuck(this.StickyDuration, stickySaveDifficulty, this.StickySaveVs, null, "stuck", "in", Liquid.ParentObject.ID, "Move", 0.25f, 1f, 1f, false, false);
						E.Object.ApplyEffect(stuck, null);
					}
				}
			}
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x0011F048 File Offset: 0x0011D248
		public virtual int GetSlipperySaveDifficulty(LiquidVolume Liquid, GameObject Subject = null)
		{
			int num = this.SlipperySaveTargetBase + Liquid.Amount(this.ID).DiminishingReturns(this.SlipperySaveTargetScale) - ((Subject != null) ? Subject.GetIntProperty("Stable", 0) : 0);
			return Math.Min(24, num);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x0011F090 File Offset: 0x0011D290
		public virtual int GetStickySaveDifficulty(LiquidVolume Liquid, GameObject Subject = null)
		{
			int num = this.StickySaveTargetBase + Liquid.Amount(this.ID).DiminishingReturns(this.StickySaveTargetScale);
			return Math.Min(24, num);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x0011F0C4 File Offset: 0x0011D2C4
		[Obsolete("Replaced by ObjectEnteredCell(LiquidVolume, ObjectEnteredCellEvent), retained for mod compatibility")]
		public virtual void ObjectEnteredCell(LiquidVolume Liquid, GameObject GO)
		{
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x0011F0C6 File Offset: 0x0011D2C6
		public virtual bool EnteredCell(LiquidVolume Liquid, EnteredCellEvent E)
		{
			return this.EnteredCell(Liquid, ref E.InterfaceExit);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x0011F0D5 File Offset: 0x0011D2D5
		[Obsolete("Replaced by EnteredCell(LiquidVolume, EnteredCellEvent), retained for mod compatibility")]
		public virtual bool EnteredCell(LiquidVolume Liquid, ref bool ExitInterface)
		{
			return true;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x0011F0D8 File Offset: 0x0011D2D8
		public virtual bool PourIntoCell(LiquidVolume Liquid, GameObject Pourer, Cell TargetCell, ref int PourAmount, bool CanPourOn, ref bool RequestInterfaceExit)
		{
			return true;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x0011F0DB File Offset: 0x0011D2DB
		public virtual bool Douse(LiquidVolume Liquid, GameObject Actor, ref int PourAmount, ref bool RequestInterfaceExit)
		{
			return true;
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x0011F0DE File Offset: 0x0011D2DE
		public virtual string SplashSound(LiquidVolume Liquid)
		{
			if (Liquid != null && Liquid.IsSwimmingDepth())
			{
				return "Sounds/Foley/fly_tileMove_water_swim";
			}
			if (Liquid != null && Liquid.IsWadingDepth())
			{
				return "Sounds/Foley/fly_tileMove_water_wade";
			}
			return "Sounds/Foley/fly_tileMove_water_puddle";
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x0011F107 File Offset: 0x0011D307
		public virtual void ObjectInCell(LiquidVolume Liquid, GameObject GO)
		{
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x0011F109 File Offset: 0x0011D309
		public virtual List<string> GetColors()
		{
			return BaseLiquid.DefaultColors;
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x0011F110 File Offset: 0x0011D310
		public virtual string GetColor()
		{
			return "b";
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x0011F118 File Offset: 0x0011D318
		public virtual int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if ((this.SlipperyWhenFrozen || this.SlipperyWhenWet || this.StickyWhenFrozen || this.StickyWhenWet) && Liquid.IsOpenVolume())
			{
				if (Smart && GO != null)
				{
					if ((this.SlipperyWhenFrozen && Liquid.ParentObject.IsFrozen()) || (this.SlipperyWhenWet && !Liquid.IsWadingDepth() && !Slimewalking && !Liquid.ParentObject.IsFrozen()))
					{
						return Math.Max(this.GetSlipperySaveDifficulty(Liquid, GO) / 3, 2);
					}
					if ((this.StickyWhenFrozen && !Slimewalking && Liquid.ParentObject.IsFrozen()) || (this.StickyWhenWet && !Slimewalking && !Liquid.ParentObject.IsFrozen()))
					{
						return Math.Max(this.GetStickySaveDifficulty(Liquid, GO) / 2, 2);
					}
				}
				if (!Slimewalking)
				{
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x0011F1F0 File Offset: 0x0011D3F0
		public virtual int GetHealingLocationValue(LiquidVolume Liquid, GameObject Actor)
		{
			return 0;
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x0011F1F3 File Offset: 0x0011D3F3
		public virtual void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x0011F1F5 File Offset: 0x0011D3F5
		public virtual string GetPaint(LiquidVolume Liquid)
		{
			if (!Liquid.IsWadingDepth())
			{
				return Liquid.ParentObject.GetTag("PaintedShallowLiquid", "shallow");
			}
			return Liquid.ParentObject.GetTag("PaintedLiquid", "deep");
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x0011F22A File Offset: 0x0011D42A
		public virtual string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (!Liquid.IsWadingDepth())
			{
				return Liquid.ParentObject.GetTag("PaintedShallowLiquidAtlas", "Liquids/Water/");
			}
			return Liquid.ParentObject.GetTag("PaintedLiquidAtlas", "Liquids/Water/");
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x0011F25F File Offset: 0x0011D45F
		public virtual string GetPaintExtension(LiquidVolume Liquid)
		{
			return Liquid.ParentObject.GetTag("PaintedLiquidExtension", ".png");
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x0011F278 File Offset: 0x0011D478
		public virtual int GetPaintGroup(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				if (this.DeepPaintGroup == -1)
				{
					return this.DeepPaintGroup = this.AllocatePaintGroup(Liquid);
				}
				return this.DeepPaintGroup;
			}
			else
			{
				if (this.ShallowPaintGroup == -1)
				{
					return this.ShallowPaintGroup = this.AllocatePaintGroup(Liquid);
				}
				return this.ShallowPaintGroup;
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x0011F2D0 File Offset: 0x0011D4D0
		protected virtual int AllocatePaintGroup(LiquidVolume Liquid)
		{
			string text = this.GetPaintAtlas(Liquid) + this.GetPaint(Liquid);
			int num = BaseLiquid.PaintGroups.IndexOf(text);
			if (num == -1)
			{
				num = BaseLiquid.PaintGroups.Count;
				BaseLiquid.PaintGroups.Add(text);
			}
			return num;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x0011F318 File Offset: 0x0011D518
		public virtual string GetPuddle(LiquidVolume Liquid)
		{
			return BaseLiquid.Puddles.GetRandomElementCosmetic<string>();
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0011F324 File Offset: 0x0011D524
		public static void AddPlayerMessage(string msg, string color = null, bool capitalize = true)
		{
			MessageQueue.AddPlayerMessage(msg, color, capitalize);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x0011F32E File Offset: 0x0011D52E
		public static void AddPlayerMessage(string msg, char color, bool capitalize = true)
		{
			MessageQueue.AddPlayerMessage(msg, color, capitalize);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0011F338 File Offset: 0x0011D538
		public string GetFreezeObjectForVolume(int Volume)
		{
			if (!this.FreezeObject3.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold3)
			{
				return this.FreezeObject3;
			}
			if (!this.FreezeObject2.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold2)
			{
				return this.FreezeObject2;
			}
			if (!this.FreezeObject1.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold1)
			{
				return this.FreezeObject1;
			}
			return null;
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0011F3A0 File Offset: 0x0011D5A0
		public string GetFreezeObjectForVolume(int Volume, out string Verb)
		{
			Verb = null;
			if (!this.FreezeObject3.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold3)
			{
				Verb = this.FreezeObjectVerb3;
				return this.FreezeObject3;
			}
			if (!this.FreezeObject2.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold2)
			{
				Verb = this.FreezeObjectVerb2;
				return this.FreezeObject2;
			}
			if (!this.FreezeObject1.IsNullOrEmpty() && Volume >= this.FreezeObjectThreshold1)
			{
				Verb = this.FreezeObjectVerb1;
				return this.FreezeObject1;
			}
			return null;
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0011F420 File Offset: 0x0011D620
		[WishCommand("tileliquids", null)]
		public static void TestLiquids()
		{
			Zone activeZone = The.ActiveZone;
			for (int i = 0; i < activeZone.Height; i++)
			{
				for (int j = 0; j < activeZone.Width; j++)
				{
					activeZone.Map[j][i].Clear(null, true, true, (GameObject x) => x.IsPlayer());
				}
			}
			StringMap<BaseLiquid> liquids = LiquidVolume.Liquids;
			int num = activeZone.Width / liquids.Count;
			int num2 = activeZone.Width % liquids.Count;
			string text = "";
			using (StringMap<BaseLiquid>.Enumerator enumerator = liquids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, BaseLiquid> liquid = enumerator.Current;
					if (text != "")
					{
						text += "\n";
					}
					text += liquid.Key;
					Action<GameObject> action = delegate(GameObject x)
					{
						x.LiquidVolume.InitialLiquid = liquid.Key + "-1000";
					};
					for (int k = 5; k < 20; k++)
					{
						if (k != 12)
						{
							Cell cell = activeZone.Map[num2][k];
							GameObject gameObject = GameObjectFactory.Factory.CreateObject("SlimePuddle", action);
							cell.Objects.Add(gameObject);
							gameObject.Physics.EnterCell(cell);
							gameObject.LiquidVolume.CheckImage();
						}
					}
					foreach (Cell cell2 in activeZone.Map[num2][12].GetLocalAdjacentCells())
					{
						if (cell2.Objects.Count == 0)
						{
							GameObject gameObject2 = GameObjectFactory.Factory.CreateObject("SlimePuddle", action);
							cell2.Objects.Add(gameObject2);
							gameObject2.Physics.EnterCell(cell2);
							gameObject2.LiquidVolume.CheckImage();
						}
					}
					num2 += num;
					if (num2 >= 80)
					{
						break;
					}
				}
			}
			MetricsManager.LogError(text);
		}

		// Token: 0x04001D19 RID: 7449
		public const int STANDARD_SAVE_CAP = 24;

		// Token: 0x04001D1A RID: 7450
		public static readonly List<string> PaintGroups = new List<string>();

		// Token: 0x04001D1B RID: 7451
		public string ID;

		// Token: 0x04001D1C RID: 7452
		public int FlameTemperature = 99999;

		// Token: 0x04001D1D RID: 7453
		public int VaporTemperature = 100;

		// Token: 0x04001D1E RID: 7454
		public int FreezeTemperature;

		// Token: 0x04001D1F RID: 7455
		public int BrittleTemperature = -100;

		// Token: 0x04001D20 RID: 7456
		public int Temperature = 25;

		// Token: 0x04001D21 RID: 7457
		public int PureElectricalConductivity = 80;

		// Token: 0x04001D22 RID: 7458
		public int MixedElectricalConductivity = 80;

		// Token: 0x04001D23 RID: 7459
		public int ThermalConductivity = 50;

		// Token: 0x04001D24 RID: 7460
		public int Combustibility;

		// Token: 0x04001D25 RID: 7461
		public int Adsorbence = 100;

		// Token: 0x04001D26 RID: 7462
		public int Fluidity = 50;

		// Token: 0x04001D27 RID: 7463
		public int Evaporativity;

		// Token: 0x04001D28 RID: 7464
		public int Staining;

		// Token: 0x04001D29 RID: 7465
		public int Cleansing;

		// Token: 0x04001D2A RID: 7466
		public int ShallowPaintGroup = -1;

		// Token: 0x04001D2B RID: 7467
		public int DeepPaintGroup = -1;

		// Token: 0x04001D2C RID: 7468
		public int FreezeObjectThreshold1;

		// Token: 0x04001D2D RID: 7469
		public int FreezeObjectThreshold2;

		// Token: 0x04001D2E RID: 7470
		public int FreezeObjectThreshold3;

		// Token: 0x04001D2F RID: 7471
		public int SlipperySaveTargetBase = 5;

		// Token: 0x04001D30 RID: 7472
		public int StickySaveTargetBase = 1;

		// Token: 0x04001D31 RID: 7473
		public int StickyDuration = 12;

		// Token: 0x04001D32 RID: 7474
		public double SlipperySaveTargetScale = 0.3;

		// Token: 0x04001D33 RID: 7475
		public double StickySaveTargetScale = 0.1;

		// Token: 0x04001D34 RID: 7476
		public bool StainOnlyWhenPure;

		// Token: 0x04001D35 RID: 7477
		public bool EnableCleaning;

		// Token: 0x04001D36 RID: 7478
		public bool InterruptAutowalk;

		// Token: 0x04001D37 RID: 7479
		public bool ConsiderDangerousToContact;

		// Token: 0x04001D38 RID: 7480
		public bool ConsiderDangerousToDrink;

		// Token: 0x04001D39 RID: 7481
		public bool Glows;

		// Token: 0x04001D3A RID: 7482
		public bool SlipperyWhenFrozen;

		// Token: 0x04001D3B RID: 7483
		public bool SlipperyWhenWet;

		// Token: 0x04001D3C RID: 7484
		public bool StickyWhenFrozen;

		// Token: 0x04001D3D RID: 7485
		public bool StickyWhenWet;

		// Token: 0x04001D3E RID: 7486
		public double Weight = 0.25;

		// Token: 0x04001D3F RID: 7487
		public string ConversionProduct;

		// Token: 0x04001D40 RID: 7488
		public string VaporObject;

		// Token: 0x04001D41 RID: 7489
		public string FreezeObject1;

		// Token: 0x04001D42 RID: 7490
		public string FreezeObject2;

		// Token: 0x04001D43 RID: 7491
		public string FreezeObject3;

		// Token: 0x04001D44 RID: 7492
		public string FreezeObjectVerb1;

		// Token: 0x04001D45 RID: 7493
		public string FreezeObjectVerb2;

		// Token: 0x04001D46 RID: 7494
		public string FreezeObjectVerb3;

		// Token: 0x04001D47 RID: 7495
		public string SlipperyMessage;

		// Token: 0x04001D48 RID: 7496
		public string SlipperyParticle;

		// Token: 0x04001D49 RID: 7497
		public string SlipperySaveStat = "Agility";

		// Token: 0x04001D4A RID: 7498
		public string SlipperySaveVs = "Slip Move";

		// Token: 0x04001D4B RID: 7499
		public string StickyMessage;

		// Token: 0x04001D4C RID: 7500
		public string StickyParticle;

		// Token: 0x04001D4D RID: 7501
		public string StickySaveStat = "Strength,Agility";

		// Token: 0x04001D4E RID: 7502
		public string StickySaveVs = "Stuck Restraint";

		// Token: 0x04001D4F RID: 7503
		public string CirculatoryLossTerm = "leaking";

		// Token: 0x04001D50 RID: 7504
		public string CirculatoryLossNoun = "leak";

		// Token: 0x04001D51 RID: 7505
		[NonSerialized]
		public static List<string> DefaultColors = new List<string>(2) { "b", "B" };

		// Token: 0x04001D52 RID: 7506
		[NonSerialized]
		public string _ColoredCirculatoryLossTerm;

		// Token: 0x04001D53 RID: 7507
		[NonSerialized]
		public string _ColoredCirculatoryLossNoun;

		// Token: 0x04001D54 RID: 7508
		public static readonly string[] Puddles = new string[] { "Liquids/Water/puddle_1.png", "Liquids/Water/puddle_2.png", "Liquids/Water/puddle_3.png", "Liquids/Water/puddle_4.png" };
	}
}
