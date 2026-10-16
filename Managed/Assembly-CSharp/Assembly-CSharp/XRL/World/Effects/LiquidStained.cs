using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D5B RID: 3419
	[Serializable]
	public class LiquidStained : Effect
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06008734 RID: 34612 RVA: 0x00309069 File Offset: 0x00307269
		public override string RemoveSound
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008735 RID: 34613 RVA: 0x0030906C File Offset: 0x0030726C
		public LiquidStained()
		{
			this.DisplayName = "stained by liquid";
			this.Duration = 1;
		}

		// Token: 0x06008736 RID: 34614 RVA: 0x00309086 File Offset: 0x00307286
		public LiquidStained(LiquidVolume Liquid)
		{
			this.Liquid = Liquid;
			this.Duration = 9999;
		}

		// Token: 0x06008737 RID: 34615 RVA: 0x003090A0 File Offset: 0x003072A0
		public LiquidStained(LiquidVolume From, int Drams, int Duration = 9999)
		{
			this.Liquid = From.Split(Drams);
			this.Duration = Duration;
		}

		// Token: 0x06008738 RID: 34616 RVA: 0x003090BC File Offset: 0x003072BC
		public LiquidStained(string LiquidSpec, int Drams, int Duration = 9999)
		{
			this.Liquid = new LiquidVolume(LiquidSpec, Drams);
			this.Duration = Duration;
		}

		// Token: 0x06008739 RID: 34617 RVA: 0x003090D8 File Offset: 0x003072D8
		public override int GetEffectType()
		{
			return 67109888;
		}

		// Token: 0x0600873A RID: 34618 RVA: 0x003090E0 File Offset: 0x003072E0
		public override bool SameAs(Effect e)
		{
			LiquidStained liquidStained = e as LiquidStained;
			return this.Liquid == null == (liquidStained.Liquid == null) && (this.Liquid == null || liquidStained.Liquid.SameAs(this.Liquid)) && base.SameAs(e);
		}

		// Token: 0x0600873B RID: 34619 RVA: 0x0030912E File Offset: 0x0030732E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600873C RID: 34620 RVA: 0x00309131 File Offset: 0x00307331
		public override bool CanApplyToStack()
		{
			return true;
		}

		// Token: 0x0600873D RID: 34621 RVA: 0x00309134 File Offset: 0x00307334
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Liquid = IPart.Load(null, Reader) as LiquidVolume;
		}

		// Token: 0x0600873E RID: 34622 RVA: 0x00309148 File Offset: 0x00307348
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			if (this.Liquid == null)
			{
				this.Liquid = new LiquidVolume();
			}
			IPart.Save(this.Liquid, Writer);
		}

		// Token: 0x0600873F RID: 34623 RVA: 0x00309169 File Offset: 0x00307369
		public override string GetDescription()
		{
			if (this.IsConcealedByLiquid())
			{
				return null;
			}
			if (this.Liquid == null)
			{
				return "stained by liquid";
			}
			return this.Liquid.StainedName.Color(this.Liquid.StainedColor);
		}

		// Token: 0x06008740 RID: 34624 RVA: 0x0030919E File Offset: 0x0030739E
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06008741 RID: 34625 RVA: 0x003091A1 File Offset: 0x003073A1
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Stained by ").Append((this.Liquid == null) ? "liquid" : this.Liquid.GetLiquidName()).Append('.');
			return stringBuilder.ToString();
		}

		// Token: 0x06008742 RID: 34626 RVA: 0x003091E0 File Offset: 0x003073E0
		public override bool Apply(GameObject Object)
		{
			if (this.Liquid == null)
			{
				return false;
			}
			this.Liquid.Update();
			LiquidStained effect = Object.GetEffect<LiquidStained>();
			if (effect != null && effect.Liquid != null)
			{
				effect.Liquid.MixWith(this.Liquid, null, null, null, false);
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyLiquidStained", "Liquid", this.Liquid)))
			{
				return false;
			}
			Object.FireEvent(Event.New("AppliedLiquidStained", "Liquid", this.Liquid));
			return true;
		}

		// Token: 0x06008743 RID: 34627 RVA: 0x00309270 File Offset: 0x00307470
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckAnythingToCleanEvent>.ID || ID == PooledEvent<CleanItemsEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x06008744 RID: 34628 RVA: 0x003092A6 File Offset: 0x003074A6
		public override bool HandleEvent(CheckAnythingToCleanEvent E)
		{
			return false;
		}

		// Token: 0x06008745 RID: 34629 RVA: 0x003092AC File Offset: 0x003074AC
		public override bool HandleEvent(CleanItemsEvent E)
		{
			E.RegisterObject(base.Object);
			E.RegisterType("stains");
			if (this.Liquid != null)
			{
				this.Liquid.Volume = 1;
				this.Liquid.FlowIntoCell(-1, base.Object.GetCurrentCell(), E.Actor, false, false, true);
			}
			base.Object.RemoveEffect(this, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008746 RID: 34630 RVA: 0x0030931A File Offset: 0x0030751A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.Liquid != null && !this.IsConcealedByLiquid())
			{
				this.Liquid.ProcessStain(E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008747 RID: 34631 RVA: 0x00309340 File Offset: 0x00307540
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (this.Liquid != null && E.IsRelevantObject(base.Object))
			{
				foreach (string text in this.Liquid.ComponentLiquids.Keys)
				{
					LiquidVolume.GetLiquid(text).StainElements(this.Liquid, E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008748 RID: 34632 RVA: 0x003093C8 File Offset: 0x003075C8
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && base.Object != null && base.Object.Render != null && this.Liquid != null && !this.IsConcealedByLiquid())
			{
				this.Liquid.RenderStain(E, base.Object);
			}
			return true;
		}

		// Token: 0x06008749 RID: 34633 RVA: 0x00309418 File Offset: 0x00307618
		public bool IsConcealedByLiquid()
		{
			if (this.Liquid == null)
			{
				return false;
			}
			LiquidCovered effect = base.Object.GetEffect<LiquidCovered>();
			if (effect == null || effect.Liquid == null)
			{
				return false;
			}
			if (this.Liquid.Primary == null)
			{
				return true;
			}
			if (effect.Liquid.ComponentLiquids.ContainsKey(this.Liquid.Primary))
			{
				if (this.Liquid.Secondary == null)
				{
					return true;
				}
				if (effect.Liquid.ComponentLiquids.ContainsKey(this.Liquid.Secondary))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600874A RID: 34634 RVA: 0x003094A4 File Offset: 0x003076A4
		public bool Cleanse(int Amount)
		{
			if (this.Liquid == null)
			{
				return false;
			}
			LiquidStained.CleanseAmounts.Clear();
			int num = 0;
			for (int i = 0; i < Amount; i++)
			{
				string randomElement = this.Liquid.ComponentLiquids.GetRandomElement<string>(ref num, null);
				if (randomElement == null)
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"LiquidStained on ",
						base.Object.DebugName,
						" had null selected liquid from ",
						this.Liquid.GetLiquidDebugDesignation(),
						" on cleanse dram ",
						(i + 1).ToString(),
						" of ",
						Amount.ToString()
					}));
					return false;
				}
				if (LiquidStained.CleanseAmounts.ContainsKey(randomElement))
				{
					Dictionary<string, int> cleanseAmounts = LiquidStained.CleanseAmounts;
					string text = randomElement;
					int num2 = cleanseAmounts[text];
					cleanseAmounts[text] = num2 + 1;
				}
				else
				{
					LiquidStained.CleanseAmounts.Add(randomElement, 1);
				}
			}
			this.Liquid.UseDrams(LiquidStained.CleanseAmounts);
			if (this.Liquid.Volume <= 0)
			{
				base.Object.RemoveEffect(this, true);
			}
			return true;
		}

		// Token: 0x0600874B RID: 34635 RVA: 0x003095BC File Offset: 0x003077BC
		public override void WasUnstackedFrom(GameObject obj)
		{
			base.WasUnstackedFrom(obj);
			if (this.Liquid != null)
			{
				LiquidStained effect = obj.GetEffect<LiquidStained>();
				if (effect != null && effect.Liquid != null && effect.Liquid.Volume == this.Liquid.Volume)
				{
					int count = base.Object.Count;
					int count2 = obj.Count;
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

		// Token: 0x04003A18 RID: 14872
		public LiquidVolume Liquid;

		// Token: 0x04003A19 RID: 14873
		[NonSerialized]
		private static Dictionary<string, int> CleanseAmounts = new Dictionary<string, int>();
	}
}
