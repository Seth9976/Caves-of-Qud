using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000C2F RID: 3119
	[Serializable]
	public class Adjusted : Effect
	{
		// Token: 0x06007EEE RID: 32494 RVA: 0x002EDB98 File Offset: 0x002EBD98
		public Adjusted()
		{
			this.DisplayName = "{{g|adjusted}}";
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x002EDBC2 File Offset: 0x002EBDC2
		public Adjusted(string Spec, int Duration)
			: this()
		{
			this.Spec = Spec;
			this.Duration = Duration;
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x002EDBD8 File Offset: 0x002EBDD8
		public Adjusted(string Spec, int Duration, GameObject Source)
			: this(Spec, Duration)
		{
			this.SourceID = Source.ID;
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x002EDBEE File Offset: 0x002EBDEE
		public Adjusted(string Spec, int Duration, string SourceID)
			: this(Spec, Duration)
		{
			this.SourceID = SourceID;
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x002EDC00 File Offset: 0x002EBE00
		public Adjusted(Adjusted Source)
		{
			this.Spec = Source.Spec;
			this.DisplayName = Source.DisplayName;
			this.Duration = Source.Duration;
			this.Stackable = Source.Stackable;
			this.Refreshes = Source.Refreshes;
			this.StatAdjustOrder = Source.StatAdjustOrder;
			this.StatAdjusts = Source.StatAdjusts;
			this.SaveAdjustOrder = Source.SaveAdjustOrder;
			this.SaveAdjusts = Source.SaveAdjusts;
			this.PropertyAdjustOrder = Source.PropertyAdjustOrder;
			this.PropertyAdjusts = Source.PropertyAdjusts;
			this.Animate = Source.Animate;
			this.AnimateRenderString = Source.AnimateRenderString;
			this.AnimateTile = Source.AnimateTile;
			this.AnimateColorString = Source.AnimateColorString;
			this.AnimateDetailColor = Source.AnimateDetailColor;
			this.AnimateFirstFrame = Source.AnimateFirstFrame;
			this.AnimateLastFrame = Source.AnimateLastFrame;
			this.SourceID = Source.SourceID;
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x002EDD0E File Offset: 0x002EBF0E
		public override bool SuppressInLookDisplay()
		{
			return this.Suppress;
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x002EDD16 File Offset: 0x002EBF16
		public override bool SuppressInStageDisplay()
		{
			return this.Suppress;
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x002EDD1E File Offset: 0x002EBF1E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x002EDD24 File Offset: 0x002EBF24
		public override int GetEffectType()
		{
			int num = 0;
			int num2 = 0;
			if (this.StatAdjustOrder != null)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (string text in this.StatAdjustOrder)
				{
					num2 += (Statistic.IsInverseBenefit(text) ? (-this.StatAdjusts[text]) : this.StatAdjusts[text]);
					if (Statistic.IsMental(text))
					{
						flag2 = true;
					}
					else
					{
						flag = true;
					}
					if (flag2)
					{
						num |= 2;
					}
					if (flag)
					{
						num |= 4;
					}
				}
			}
			if (this.SaveAdjustOrder != null)
			{
				foreach (int num3 in this.SaveAdjusts.Values)
				{
					num2 += num3;
				}
				num |= 128;
			}
			if (this.PropertyAdjustOrder != null)
			{
				foreach (string text2 in this.PropertyAdjustOrder)
				{
					num2 += (PropertyDescription.IsInverseBenefit(text2) ? (-this.PropertyAdjusts[text2]) : this.PropertyAdjusts[text2]);
					num |= PropertyDescription.GetPropertyEffectType(text2);
				}
			}
			if (num2 < 0)
			{
				num |= 33554432;
			}
			if (Math.Abs(num2) <= 3)
			{
				num |= 16777216;
			}
			return num;
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x002EDEB8 File Offset: 0x002EC0B8
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.StatAdjustOrder != null)
			{
				foreach (string text in this.StatAdjustOrder)
				{
					stringBuilder.Compound(Statistic.GetStatAdjustDescription(text, this.StatAdjusts[text], false), "\n");
				}
			}
			if (this.SaveAdjustOrder != null)
			{
				foreach (string text2 in this.SaveAdjustOrder)
				{
					SavingThrows.AppendSaveBonusDescription(stringBuilder, this.SaveAdjusts[text2], text2, false, false, true);
				}
			}
			if (this.PropertyAdjustOrder != null)
			{
				foreach (string text3 in this.PropertyAdjustOrder)
				{
					stringBuilder.Compound(PropertyDescription.GetPropertyAdjustDescription(text3, this.PropertyAdjusts[text3]), "\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x002EDFF8 File Offset: 0x002EC1F8
		public override bool SameAs(Effect e)
		{
			Adjusted adjusted = e as Adjusted;
			return !(adjusted.Spec != this.Spec) && adjusted.Stackable == this.Stackable && adjusted.Refreshes == this.Refreshes && adjusted.Animate == this.Animate && !(adjusted.AnimateRenderString != this.AnimateRenderString) && !(adjusted.AnimateTile != this.AnimateTile) && !(adjusted.AnimateColorString != this.AnimateColorString) && !(adjusted.AnimateDetailColor != this.AnimateDetailColor) && adjusted.AnimateFirstFrame == this.AnimateFirstFrame && adjusted.AnimateLastFrame == this.AnimateLastFrame && !(adjusted.SourceID != this.SourceID) && base.SameAs(e);
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x002EE0E4 File Offset: 0x002EC2E4
		public void ApplySpec()
		{
			if (this.Spec == null)
			{
				MetricsManager.LogError("spec was null");
				return;
			}
			string[] array = this.Spec.Split('|', StringSplitOptions.None);
			if (array.Length < 1)
			{
				MetricsManager.LogError("invalid spec '" + this.Spec + "'");
				return;
			}
			int i = 0;
			this.DisplayName = array[i++];
			bool flag;
			do
			{
				flag = false;
				if (i < array.Length && array[i] == "stackable")
				{
					this.Stackable = true;
					i++;
					flag = true;
				}
				if (i < array.Length && array[i] == "nostack")
				{
					this.Stackable = false;
					i++;
					flag = true;
				}
				if (i < array.Length && array[i] == "refresh")
				{
					this.Refreshes = true;
					i++;
					flag = true;
				}
				if (i < array.Length && array[i] == "norefresh")
				{
					this.Refreshes = false;
					i++;
					flag = true;
				}
				if (i < array.Length && array[i] == "suppress")
				{
					this.Suppress = true;
					i++;
					flag = true;
				}
				if (i < array.Length && array[i] == "nosuppress")
				{
					this.Suppress = false;
					i++;
					flag = true;
				}
			}
			while (flag);
			while (i < array.Length)
			{
				string[] array2 = array[i].Split(':', StringSplitOptions.None);
				if (array2.Length != 3)
				{
					MetricsManager.LogError("invalid spec part " + array[i]);
				}
				else
				{
					string text = array2[0];
					string text2 = array2[1];
					string text3 = array2[2];
					int num = 0;
					bool flag2 = false;
					if (!(text == "Mult") && !(text == "Stat") && !(text == "Save") && !(text == "Property"))
					{
						if (text == "Animate")
						{
							if (text2 == "FirstFrame" || text2 == "LastFrame")
							{
								flag2 = true;
							}
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (!int.TryParse(text3, out num))
						{
							MetricsManager.LogError("invalid spec part value " + text3);
							goto IL_0417;
						}
						if (text == "Mult")
						{
							num = (int)((float)base.Object.GetStat(text2).BaseValue * ((float)num / 100f));
						}
					}
					if (!(text == "Mult") && !(text == "Stat"))
					{
						if (!(text == "Save"))
						{
							if (!(text == "Property"))
							{
								if (!(text == "Animate"))
								{
									MetricsManager.LogError("spec part type must be Stat, Save, Property, or Animate, had '" + text + "'");
								}
								else
								{
									if (!(text2 == "RenderString"))
									{
										if (!(text2 == "Tile"))
										{
											if (!(text2 == "ColorString"))
											{
												if (!(text2 == "DetailColor"))
												{
													if (!(text2 == "FirstFrame"))
													{
														if (!(text2 == "LastFrame"))
														{
															MetricsManager.LogError("Animate spec part subtype must be RenderString, Tile, ColorString, DetailColor, FirstFrame, or LastFrame, had '" + text + "'");
															goto IL_0417;
														}
														this.AnimateLastFrame = num;
													}
													else
													{
														this.AnimateFirstFrame = num;
													}
												}
												else
												{
													this.AnimateDetailColor = text3;
												}
											}
											else
											{
												this.AnimateColorString = text3;
											}
										}
										else
										{
											this.AnimateTile = text3;
										}
									}
									else
									{
										this.AnimateRenderString = text3;
									}
									this.Animate = true;
								}
							}
							else
							{
								if (this.PropertyAdjustOrder == null)
								{
									this.PropertyAdjustOrder = new List<string>();
								}
								if (this.PropertyAdjusts == null)
								{
									this.PropertyAdjusts = new Dictionary<string, int>();
								}
								this.PropertyAdjustOrder.Add(text2);
								this.PropertyAdjusts.Add(text2, num);
							}
						}
						else
						{
							if (this.SaveAdjustOrder == null)
							{
								this.SaveAdjustOrder = new List<string>();
							}
							if (this.SaveAdjusts == null)
							{
								this.SaveAdjusts = new Dictionary<string, int>();
							}
							this.SaveAdjustOrder.Add(text2);
							this.SaveAdjusts.Add(text2, num);
						}
					}
					else
					{
						if (this.StatAdjustOrder == null)
						{
							this.StatAdjustOrder = new List<string>();
						}
						if (this.StatAdjusts == null)
						{
							this.StatAdjusts = new Dictionary<string, int>();
						}
						this.StatAdjustOrder.Add(text2);
						this.StatAdjusts.Add(text2, num);
					}
				}
				IL_0417:
				i++;
			}
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x002EE518 File Offset: 0x002EC718
		public override bool Render(RenderEvent E)
		{
			if (this.Animate && this.Duration > 0 && XRLCore.CurrentFrame >= this.AnimateFirstFrame && XRLCore.CurrentFrame < this.AnimateLastFrame)
			{
				if (!string.IsNullOrEmpty(this.AnimateRenderString))
				{
					E.Tile = null;
					E.RenderString = this.AnimateRenderString;
				}
				else if (!string.IsNullOrEmpty(this.AnimateTile))
				{
					E.Tile = this.AnimateTile;
				}
				if (!string.IsNullOrEmpty(this.AnimateColorString))
				{
					E.ColorString = this.AnimateColorString;
				}
				if (!string.IsNullOrEmpty(this.AnimateDetailColor))
				{
					E.DetailColor = this.AnimateDetailColor;
				}
			}
			return true;
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x002EE5C4 File Offset: 0x002EC7C4
		public override bool Apply(GameObject Object)
		{
			if (!this.Stackable && Object.HasEffect<Adjusted>())
			{
				foreach (Adjusted adjusted in Object.YieldEffects<Adjusted>())
				{
					if (adjusted != null && adjusted.Spec == this.Spec)
					{
						if (this.Refreshes && adjusted.Duration < this.Duration)
						{
							adjusted.Duration = this.Duration;
						}
						return false;
					}
				}
			}
			if (Object.FireEvent(Event.New("ApplyAdjusted", "Effect", this, "Duration", this.Duration)))
			{
				this.ApplyAdjustments();
				return true;
			}
			return false;
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x002EE684 File Offset: 0x002EC884
		private void ApplyAdjustments()
		{
			if (this.StatAdjustOrder == null && this.PropertyAdjustOrder == null && this.SaveAdjustOrder == null && this.PropertyAdjustOrder == null)
			{
				this.ApplySpec();
			}
			if (this.StatAdjustOrder != null)
			{
				foreach (string text in this.StatAdjustOrder)
				{
					base.StatShifter.SetStatShift(text, this.StatAdjusts[text], text == "Hitpoints");
				}
			}
			if (this.PropertyAdjustOrder != null)
			{
				foreach (string text2 in this.PropertyAdjustOrder)
				{
					base.Object.ModIntProperty(text2, this.PropertyAdjusts[text2], true);
				}
			}
		}

		// Token: 0x06007EFD RID: 32509 RVA: 0x002EE780 File Offset: 0x002EC980
		private void UnapplyAdjustments()
		{
			if (this.StatAdjustOrder != null)
			{
				base.StatShifter.RemoveStatShifts(base.Object);
			}
			if (this.PropertyAdjustOrder != null)
			{
				foreach (string text in this.PropertyAdjustOrder)
				{
					base.Object.ModIntProperty(text, -this.PropertyAdjusts[text], true);
				}
			}
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x002EE808 File Offset: 0x002ECA08
		public override void Remove(GameObject Object)
		{
			this.UnapplyAdjustments();
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x002EE810 File Offset: 0x002ECA10
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == ModifyDefendingSaveEvent.ID && this.SaveAdjustOrder != null);
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x002EE834 File Offset: 0x002ECA34
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.SaveAdjustOrder != null)
			{
				foreach (string text in this.SaveAdjustOrder)
				{
					if (SavingThrows.Applicable(text, E))
					{
						E.Roll += this.SaveAdjusts[text];
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x002EE8B4 File Offset: 0x002ECAB4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x002EE8D4 File Offset: 0x002ECAD4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyAdjustments();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyAdjustments();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400389E RID: 14494
		public string Spec;

		// Token: 0x0400389F RID: 14495
		public bool Stackable;

		// Token: 0x040038A0 RID: 14496
		public bool Refreshes = true;

		// Token: 0x040038A1 RID: 14497
		public bool Suppress;

		// Token: 0x040038A2 RID: 14498
		public List<string> StatAdjustOrder;

		// Token: 0x040038A3 RID: 14499
		public Dictionary<string, int> StatAdjusts;

		// Token: 0x040038A4 RID: 14500
		public List<string> SaveAdjustOrder;

		// Token: 0x040038A5 RID: 14501
		public Dictionary<string, int> SaveAdjusts;

		// Token: 0x040038A6 RID: 14502
		public List<string> PropertyAdjustOrder;

		// Token: 0x040038A7 RID: 14503
		public Dictionary<string, int> PropertyAdjusts;

		// Token: 0x040038A8 RID: 14504
		public bool Animate;

		// Token: 0x040038A9 RID: 14505
		public string AnimateRenderString;

		// Token: 0x040038AA RID: 14506
		public string AnimateTile;

		// Token: 0x040038AB RID: 14507
		public string AnimateColorString;

		// Token: 0x040038AC RID: 14508
		public string AnimateDetailColor;

		// Token: 0x040038AD RID: 14509
		public int AnimateFirstFrame = 25;

		// Token: 0x040038AE RID: 14510
		public int AnimateLastFrame = 45;

		// Token: 0x040038AF RID: 14511
		public string SourceID;
	}
}
