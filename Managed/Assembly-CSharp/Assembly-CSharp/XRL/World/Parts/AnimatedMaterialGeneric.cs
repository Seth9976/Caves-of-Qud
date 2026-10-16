using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012C0 RID: 4800
	[Serializable]
	public class AnimatedMaterialGeneric : IPart
	{
		// Token: 0x0600BD87 RID: 48519 RVA: 0x004568EC File Offset: 0x00454AEC
		public AnimatedMaterialGeneric()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BD88 RID: 48520 RVA: 0x0045695C File Offset: 0x00454B5C
		public override bool SameAs(IPart p)
		{
			AnimatedMaterialGeneric animatedMaterialGeneric = p as AnimatedMaterialGeneric;
			return animatedMaterialGeneric.AnimationLength == this.AnimationLength && animatedMaterialGeneric.LowFrameOffset == this.LowFrameOffset && animatedMaterialGeneric.HighFrameOffset == this.HighFrameOffset && !(animatedMaterialGeneric.ColorStringAnimationFrames != this.ColorStringAnimationFrames) && !(animatedMaterialGeneric.BackgroundStringAnimationFrames != this.BackgroundStringAnimationFrames) && !(animatedMaterialGeneric.DetailColorAnimationFrames != this.DetailColorAnimationFrames) && !(animatedMaterialGeneric.TileColorAnimationFrames != this.TileColorAnimationFrames) && !(animatedMaterialGeneric.RenderStringAnimationFrames != this.RenderStringAnimationFrames) && !(animatedMaterialGeneric.TileAnimationFrames != this.TileAnimationFrames) && !(animatedMaterialGeneric.RequiresOperationalActivePart != this.RequiresOperationalActivePart) && !(animatedMaterialGeneric.RequiresUnpoweredActivePart != this.RequiresUnpoweredActivePart) && animatedMaterialGeneric.RequiresAnyUnpoweredActivePart == this.RequiresAnyUnpoweredActivePart && animatedMaterialGeneric.ActivePartStatusIgnoreCharge == this.ActivePartStatusIgnoreCharge && animatedMaterialGeneric.ActivePartStatusIgnoreBreakage == this.ActivePartStatusIgnoreBreakage && animatedMaterialGeneric.ActivePartStatusIgnoreRust == this.ActivePartStatusIgnoreRust && animatedMaterialGeneric.ActivePartStatusIgnoreEMP == this.ActivePartStatusIgnoreEMP && animatedMaterialGeneric.ActivePartStatusIgnoreRealityStabilization == this.ActivePartStatusIgnoreRealityStabilization && animatedMaterialGeneric.ActivePartStatusIgnoreLocallyDefinedFailure == this.ActivePartStatusIgnoreLocallyDefinedFailure && animatedMaterialGeneric.ActivePartStatusIgnoreSubject == this.ActivePartStatusIgnoreSubject && !(animatedMaterialGeneric.RequiresEvent != this.RequiresEvent) && !(animatedMaterialGeneric.RequiresInverseEvent != this.RequiresInverseEvent) && !(animatedMaterialGeneric.RequiresEffect != this.RequiresEffect) && !(animatedMaterialGeneric.RequiresInverseEffect != this.RequiresInverseEffect) && animatedMaterialGeneric.HFlipPermyriadChancePerFrame == this.HFlipPermyriadChancePerFrame && animatedMaterialGeneric.HFlipChanceMultiplyByWindSpeed == this.HFlipChanceMultiplyByWindSpeed && animatedMaterialGeneric.VFlipPermyriadChancePerFrame == this.VFlipPermyriadChancePerFrame && animatedMaterialGeneric.VFlipChanceMultiplyByWindSpeed == this.VFlipChanceMultiplyByWindSpeed && !(animatedMaterialGeneric.AltRenderString != this.AltRenderString) && animatedMaterialGeneric.AltRenderStringPermyriadChancePerFrame == this.AltRenderStringPermyriadChancePerFrame && animatedMaterialGeneric.AltRenderStringChanceMultiplyByWindSpeed == this.AltRenderStringChanceMultiplyByWindSpeed && animatedMaterialGeneric.MustBeUnderstood == this.MustBeUnderstood && base.SameAs(p);
		}

		// Token: 0x0600BD89 RID: 48521 RVA: 0x00456BA8 File Offset: 0x00454DA8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BD8A RID: 48522 RVA: 0x00456BAB File Offset: 0x00454DAB
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.TurnProcess();
		}

		// Token: 0x0600BD8B RID: 48523 RVA: 0x00456BB3 File Offset: 0x00454DB3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PowerSwitchFlippedEvent.ID;
		}

		// Token: 0x0600BD8C RID: 48524 RVA: 0x00456BF1 File Offset: 0x00454DF1
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.FlushNonEffectStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD8D RID: 48525 RVA: 0x00456C00 File Offset: 0x00454E00
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.FlushNonEffectStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD8E RID: 48526 RVA: 0x00456C0F File Offset: 0x00454E0F
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.FlushNonEffectStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD8F RID: 48527 RVA: 0x00456C1E File Offset: 0x00454E1E
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.FlushStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD90 RID: 48528 RVA: 0x00456C2D File Offset: 0x00454E2D
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.FlushStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD91 RID: 48529 RVA: 0x00456C3C File Offset: 0x00454E3C
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.FlushNonEffectStateCaches();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD92 RID: 48530 RVA: 0x00456C4B File Offset: 0x00454E4B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BD93 RID: 48531 RVA: 0x00456C50 File Offset: 0x00454E50
		private ActivePartStatus? StatusOf(IActivePart p)
		{
			if (p == null)
			{
				return null;
			}
			return new ActivePartStatus?(p.GetActivePartStatus(false, this.ActivePartStatusIgnoreCharge, false, false, this.ActivePartStatusIgnoreBreakage, this.ActivePartStatusIgnoreRust, this.ActivePartStatusIgnoreEMP, this.ActivePartStatusIgnoreRealityStabilization, this.ActivePartStatusIgnoreSubject, this.ActivePartStatusIgnoreLocallyDefinedFailure, 1, null, false, 0L, null));
		}

		// Token: 0x0600BD94 RID: 48532 RVA: 0x00456CB8 File Offset: 0x00454EB8
		private ActivePartStatus? StatusOf(string PartName)
		{
			return this.StatusOf(this.ParentObject.GetPart(PartName) as IActivePart);
		}

		// Token: 0x0600BD95 RID: 48533 RVA: 0x00456CD4 File Offset: 0x00454ED4
		public override bool Render(RenderEvent E)
		{
			if (!this.RequiresOperationalActivePart.IsNullOrEmpty())
			{
				if (this.HasOperationalActivePart == null)
				{
					ActivePartStatus? activePartStatus = this.StatusOf(this.RequiresOperationalActivePart);
					ActivePartStatus activePartStatus2 = ActivePartStatus.Operational;
					this.HasOperationalActivePart = new bool?((activePartStatus.GetValueOrDefault() == activePartStatus2) & (activePartStatus != null));
				}
				bool? flag = this.HasOperationalActivePart;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (!this.RequiresUnpoweredActivePart.IsNullOrEmpty())
			{
				if (this.HasUnpoweredActivePart == null)
				{
					ActivePartStatus? activePartStatus = this.StatusOf(this.RequiresUnpoweredActivePart);
					ActivePartStatus activePartStatus2 = ActivePartStatus.Unpowered;
					this.HasUnpoweredActivePart = new bool?((activePartStatus.GetValueOrDefault() == activePartStatus2) & (activePartStatus != null));
				}
				bool? flag = this.HasUnpoweredActivePart;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (this.RequiresAnyUnpoweredActivePart)
			{
				bool? flag;
				bool flag2;
				if (this.HasAnyUnpoweredActivePart == null)
				{
					flag = this.HasUnpoweredActivePart;
					flag2 = true;
					if ((flag.GetValueOrDefault() == flag2) & (flag != null))
					{
						this.HasAnyUnpoweredActivePart = new bool?(true);
					}
					else
					{
						int i = 0;
						int count = this.ParentObject.PartsList.Count;
						while (i < count)
						{
							IActivePart activePart = this.ParentObject.PartsList[i] as IActivePart;
							if (activePart != null)
							{
								ActivePartStatus? activePartStatus = this.StatusOf(activePart);
								ActivePartStatus activePartStatus2 = ActivePartStatus.Unpowered;
								if ((activePartStatus.GetValueOrDefault() == activePartStatus2) & (activePartStatus != null))
								{
									this.HasAnyUnpoweredActivePart = new bool?(true);
									break;
								}
							}
							i++;
						}
					}
				}
				flag = this.HasAnyUnpoweredActivePart;
				flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (!this.RequiresEvent.IsNullOrEmpty())
			{
				if (this.HasEvent == null)
				{
					this.HasEvent = new bool?(this.ParentObject.FireEvent(this.RequiresEvent));
				}
				bool? flag = this.HasEvent;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (!this.RequiresInverseEvent.IsNullOrEmpty())
			{
				if (this.HasInverseEvent == null)
				{
					this.HasInverseEvent = new bool?(!this.ParentObject.FireEvent(this.RequiresInverseEvent));
				}
				bool? flag = this.HasInverseEvent;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (!this.RequiresEffect.IsNullOrEmpty())
			{
				if (this.HasEffect == null)
				{
					this.HasEffect = new bool?(this.ParentObject.HasEffect(this.RequiresEffect));
				}
				bool? flag = this.HasEffect;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (!this.RequiresInverseEffect.IsNullOrEmpty())
			{
				if (this.HasInverseEffect == null)
				{
					this.HasInverseEffect = new bool?(!this.ParentObject.HasEffect(this.RequiresInverseEffect));
				}
				bool? flag = this.HasInverseEffect;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			if (this.MustBeUnderstood)
			{
				if (this.IsUnderstood == null)
				{
					this.IsUnderstood = new bool?(this.ParentObject.Understood());
				}
				bool? flag = this.IsUnderstood;
				bool flag2 = true;
				if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
				{
					return true;
				}
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			int num = (XRLCore.GetCurrentFrameAtFPS(60, this.SpeedMultiplier) + this.FrameOffset) % this.AnimationLength;
			this.FrameOffset = 0;
			if (num != this.lastFrame)
			{
				num += Stat.RandomCosmetic(this.LowFrameOffset, this.HighFrameOffset);
				this.lastFrame = num;
			}
			if (this.ColorStringAnimationTimes == null && !this.ColorStringAnimationFrames.IsNullOrEmpty())
			{
				if (this.ColorStringAnimationFrames == "0=default" || this.ColorStringAnimationFrames == "disable")
				{
					this.ColorStringAnimationTimes = new List<int>();
					this.ColorStringAnimationColors = new List<string>();
				}
				else
				{
					string[] array = this.ColorStringAnimationFrames.Split(',', StringSplitOptions.None);
					this.ColorStringAnimationTimes = new List<int>(array.Length);
					this.ColorStringAnimationColors = new List<string>(array.Length);
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array3 = array2[j].Split('=', StringSplitOptions.None);
						this.ColorStringAnimationTimes.Add(int.Parse(array3[0]));
						this.ColorStringAnimationColors.Add(array3[1]);
					}
				}
			}
			if (this.TileColorAnimationTimes == null && !this.TileColorAnimationFrames.IsNullOrEmpty())
			{
				if (this.TileColorAnimationFrames == "0=default" || this.TileColorAnimationFrames == "disable")
				{
					this.TileColorAnimationTimes = new List<int>();
					this.TileColorAnimationColors = new List<string>();
				}
				else
				{
					string[] array4 = this.TileColorAnimationFrames.Split(',', StringSplitOptions.None);
					this.TileColorAnimationTimes = new List<int>(array4.Length);
					this.TileColorAnimationColors = new List<string>(array4.Length);
					string[] array2 = array4;
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array5 = array2[j].Split('=', StringSplitOptions.None);
						this.TileColorAnimationTimes.Add(int.Parse(array5[0]));
						this.TileColorAnimationColors.Add(array5[1]);
					}
				}
			}
			if (E.ColorsVisible)
			{
				if (this.TileColorAnimationTimes != null && Globals.RenderMode == RenderModeType.Tiles)
				{
					string text4 = null;
					int num2 = 0;
					while (num2 < this.TileColorAnimationTimes.Count && num >= this.TileColorAnimationTimes[num2])
					{
						text4 = this.TileColorAnimationColors[num2];
						num2++;
					}
					if (text4 != null)
					{
						if (text4 == "default")
						{
							text = null;
						}
						else if (text4 == "base")
						{
							text = this.ParentObject.Render.GetRenderColor();
						}
						else if (text4 == "liquid")
						{
							text = "&" + this.ParentObject.GetLiquidColor("K");
						}
						else
						{
							text = text4;
						}
					}
				}
				else if (this.ColorStringAnimationTimes != null)
				{
					string text5 = null;
					int num3 = 0;
					while (num3 < this.ColorStringAnimationTimes.Count && num >= this.ColorStringAnimationTimes[num3])
					{
						text5 = this.ColorStringAnimationColors[num3];
						num3++;
					}
					if (text5 != null)
					{
						if (text5 == "default")
						{
							text = null;
						}
						else if (text5 == "base")
						{
							text = this.ParentObject.Render.GetRenderColor();
						}
						else if (text5 == "liquid")
						{
							text = "&" + this.ParentObject.GetLiquidColor("K");
						}
						else
						{
							text = text5;
						}
					}
				}
			}
			if (this.BackgroundStringAnimationTimes == null && !this.BackgroundStringAnimationFrames.IsNullOrEmpty())
			{
				string[] array6 = this.BackgroundStringAnimationFrames.Split(',', StringSplitOptions.None);
				this.BackgroundStringAnimationTimes = new List<int>(array6.Length);
				this.BackgroundStringAnimationColors = new List<string>(array6.Length);
				string[] array2 = array6;
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array7 = array2[j].Split('=', StringSplitOptions.None);
					this.BackgroundStringAnimationTimes.Add(int.Parse(array7[0]));
					this.BackgroundStringAnimationColors.Add(array7[1]);
				}
			}
			if (this.BackgroundStringAnimationTimes != null && E.ColorsVisible)
			{
				string text6 = null;
				int num4 = 0;
				int count2 = this.BackgroundStringAnimationTimes.Count;
				while (num4 < count2 && num >= this.BackgroundStringAnimationTimes[num4])
				{
					text6 = this.BackgroundStringAnimationColors[num4];
					num4++;
				}
				if (text6 != null && text6 != "default")
				{
					text2 = text6;
				}
			}
			if (this.DetailColorAnimationTimes == null && !this.DetailColorAnimationFrames.IsNullOrEmpty())
			{
				string[] array8 = this.DetailColorAnimationFrames.Split(',', StringSplitOptions.None);
				this.DetailColorAnimationTimes = new List<int>(array8.Length);
				this.DetailColorAnimationColors = new List<string>(array8.Length);
				string[] array2 = array8;
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array9 = array2[j].Split('=', StringSplitOptions.None);
					this.DetailColorAnimationTimes.Add(int.Parse(array9[0]));
					this.DetailColorAnimationColors.Add(array9[1]);
				}
			}
			if (this.DetailColorAnimationTimes != null && E.ColorsVisible)
			{
				string text7 = null;
				int num5 = 0;
				int count3 = this.DetailColorAnimationTimes.Count;
				while (num5 < count3 && num >= this.DetailColorAnimationTimes[num5])
				{
					text7 = this.DetailColorAnimationColors[num5];
					num5++;
				}
				if (text7 != null)
				{
					if (text7 == "default")
					{
						text3 = null;
					}
					else if (text7 == "base")
					{
						text3 = this.ParentObject.Render.DetailColor;
					}
					else if (text7 == "liquid")
					{
						text3 = this.ParentObject.GetLiquidColor("K");
					}
					else
					{
						text3 = text7;
					}
				}
			}
			if (this.RenderStringAnimationTimes == null && !this.RenderStringAnimationFrames.IsNullOrEmpty())
			{
				string[] array10 = this.RenderStringAnimationFrames.Split(',', StringSplitOptions.None);
				this.RenderStringAnimationTimes = new List<int>(array10.Length);
				this.RenderStringAnimationColors = new List<string>(array10.Length);
				string[] array2 = array10;
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array11 = array2[j].Split('=', StringSplitOptions.None);
					this.RenderStringAnimationTimes.Add(int.Parse(array11[0]));
					this.RenderStringAnimationColors.Add(array11[1]);
				}
			}
			if (this.RenderStringAnimationTimes != null)
			{
				string text8 = null;
				int num6 = 0;
				int count4 = this.RenderStringAnimationTimes.Count;
				while (num6 < count4 && num >= this.RenderStringAnimationTimes[num6])
				{
					text8 = this.RenderStringAnimationColors[num6];
					num6++;
				}
				if (text8 != null)
				{
					if (text8 == "default")
					{
						E.RenderString = this.ParentObject.Render.RenderString;
					}
					else if (text8 == "alt")
					{
						this.AltRenderStringActive = !this.AltRenderStringActive;
					}
					else
					{
						E.RenderString = text8;
					}
				}
			}
			if (this.TileAnimationTimes == null && !this.TileAnimationFrames.IsNullOrEmpty())
			{
				string[] array12 = this.TileAnimationFrames.Split(',', StringSplitOptions.None);
				this.TileAnimationTimes = new List<int>(array12.Length);
				this.TileAnimationColors = new List<string>(array12.Length);
				string[] array2 = array12;
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array13 = array2[j].Split('=', StringSplitOptions.None);
					this.TileAnimationTimes.Add(int.Parse(array13[0]));
					this.TileAnimationColors.Add(array13[1]);
				}
			}
			if (this.TileAnimationTimes != null)
			{
				string text9 = null;
				int num7 = 0;
				int count5 = this.TileAnimationTimes.Count;
				while (num7 < count5 && num >= this.TileAnimationTimes[num7])
				{
					text9 = this.TileAnimationColors[num7];
					num7++;
				}
				if (text9 != null)
				{
					if (text9 == "default")
					{
						E.Tile = this.ParentObject.Render.Tile;
					}
					else if (text9 == "hflip")
					{
						this.HFlipActive = !this.HFlipActive;
					}
					else if (text9 == "vflip")
					{
						this.VFlipActive = !this.VFlipActive;
					}
					else
					{
						E.Tile = text9;
					}
				}
			}
			if (this.HFlipPermyriadChancePerFrame > 0)
			{
				int num8 = this.HFlipPermyriadChancePerFrame * Math.Abs(num - this.LastFrame);
				if (this.HFlipChanceMultiplyByWindSpeed && this.ParentObject.CurrentZone != null)
				{
					num8 *= this.ParentObject.CurrentZone.CurrentWindSpeed;
				}
				if (num8.in10000())
				{
					this.HFlipActive = !this.HFlipActive;
				}
			}
			if (this.VFlipPermyriadChancePerFrame > 0)
			{
				int num9 = this.VFlipPermyriadChancePerFrame * Math.Abs(num - this.LastFrame);
				if (this.VFlipChanceMultiplyByWindSpeed && this.ParentObject.CurrentZone != null)
				{
					num9 *= this.ParentObject.CurrentZone.CurrentWindSpeed;
				}
				if (num9.in10000())
				{
					this.VFlipActive = !this.VFlipActive;
				}
			}
			if (this.AltRenderStringPermyriadChancePerFrame > 0)
			{
				int num10 = this.AltRenderStringPermyriadChancePerFrame * Math.Abs(num - this.LastFrame);
				if (this.AltRenderStringChanceMultiplyByWindSpeed && this.ParentObject.CurrentZone != null)
				{
					num10 *= this.ParentObject.CurrentZone.CurrentWindSpeed;
				}
				if (num10.in10000())
				{
					this.AltRenderStringActive = !this.AltRenderStringActive;
				}
			}
			if (this.HFlipActive)
			{
				E.HFlip = !E.HFlip;
			}
			if (this.VFlipActive)
			{
				E.VFlip = !E.VFlip;
			}
			if (this.AltRenderStringActive)
			{
				E.RenderString = this.AltRenderString;
			}
			if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty() || !text3.IsNullOrEmpty())
			{
				E.ApplyColors(text, text2, text3, this.ForegroundPriority, this.BackgroundPriority, this.DetailPriority);
			}
			this.LastFrame = num;
			return true;
		}

		// Token: 0x0600BD96 RID: 48534 RVA: 0x004579F8 File Offset: 0x00455BF8
		private void TurnProcess()
		{
			this.FlushNonEffectStateCaches();
		}

		// Token: 0x0600BD97 RID: 48535 RVA: 0x00457A00 File Offset: 0x00455C00
		private void FlushStateCaches()
		{
			this.HasEffect = null;
			this.HasInverseEffect = null;
			this.FlushNonEffectStateCaches();
		}

		// Token: 0x0600BD98 RID: 48536 RVA: 0x00457A20 File Offset: 0x00455C20
		private void FlushNonEffectStateCaches()
		{
			this.HasOperationalActivePart = null;
			this.HasUnpoweredActivePart = null;
			this.HasAnyUnpoweredActivePart = null;
			this.HasEvent = null;
			this.HasInverseEvent = null;
			this.IsUnderstood = null;
		}

		// Token: 0x04004C02 RID: 19458
		public static readonly int DEFAULT_ICON_COLOR_PRIORITY = 40;

		// Token: 0x04004C03 RID: 19459
		public int FrameOffset;

		// Token: 0x04004C04 RID: 19460
		public float SpeedMultiplier = 1.7f;

		// Token: 0x04004C05 RID: 19461
		public int AnimationLength = 60;

		// Token: 0x04004C06 RID: 19462
		public int LowFrameOffset = 1;

		// Token: 0x04004C07 RID: 19463
		public int HighFrameOffset = 1;

		// Token: 0x04004C08 RID: 19464
		public int ForegroundPriority = AnimatedMaterialGeneric.DEFAULT_ICON_COLOR_PRIORITY;

		// Token: 0x04004C09 RID: 19465
		public int DetailPriority = AnimatedMaterialGeneric.DEFAULT_ICON_COLOR_PRIORITY;

		// Token: 0x04004C0A RID: 19466
		public int BackgroundPriority = AnimatedMaterialGeneric.DEFAULT_ICON_COLOR_PRIORITY;

		// Token: 0x04004C0B RID: 19467
		public string ColorStringAnimationFrames;

		// Token: 0x04004C0C RID: 19468
		[NonSerialized]
		private List<int> ColorStringAnimationTimes;

		// Token: 0x04004C0D RID: 19469
		[NonSerialized]
		private List<string> ColorStringAnimationColors;

		// Token: 0x04004C0E RID: 19470
		public string BackgroundStringAnimationFrames;

		// Token: 0x04004C0F RID: 19471
		[NonSerialized]
		private List<int> BackgroundStringAnimationTimes;

		// Token: 0x04004C10 RID: 19472
		[NonSerialized]
		private List<string> BackgroundStringAnimationColors;

		// Token: 0x04004C11 RID: 19473
		public string DetailColorAnimationFrames;

		// Token: 0x04004C12 RID: 19474
		[NonSerialized]
		private List<int> DetailColorAnimationTimes;

		// Token: 0x04004C13 RID: 19475
		[NonSerialized]
		private List<string> DetailColorAnimationColors;

		// Token: 0x04004C14 RID: 19476
		public string TileColorAnimationFrames;

		// Token: 0x04004C15 RID: 19477
		[NonSerialized]
		private List<int> TileColorAnimationTimes;

		// Token: 0x04004C16 RID: 19478
		[NonSerialized]
		private List<string> TileColorAnimationColors;

		// Token: 0x04004C17 RID: 19479
		public string RenderStringAnimationFrames;

		// Token: 0x04004C18 RID: 19480
		[NonSerialized]
		private List<int> RenderStringAnimationTimes;

		// Token: 0x04004C19 RID: 19481
		[NonSerialized]
		private List<string> RenderStringAnimationColors;

		// Token: 0x04004C1A RID: 19482
		public string TileAnimationFrames;

		// Token: 0x04004C1B RID: 19483
		[NonSerialized]
		private List<int> TileAnimationTimes;

		// Token: 0x04004C1C RID: 19484
		[NonSerialized]
		private List<string> TileAnimationColors;

		// Token: 0x04004C1D RID: 19485
		public string RequiresOperationalActivePart;

		// Token: 0x04004C1E RID: 19486
		[NonSerialized]
		private bool? HasOperationalActivePart;

		// Token: 0x04004C1F RID: 19487
		public string RequiresUnpoweredActivePart;

		// Token: 0x04004C20 RID: 19488
		[NonSerialized]
		private bool? HasUnpoweredActivePart;

		// Token: 0x04004C21 RID: 19489
		public bool RequiresAnyUnpoweredActivePart;

		// Token: 0x04004C22 RID: 19490
		[NonSerialized]
		private bool? HasAnyUnpoweredActivePart;

		// Token: 0x04004C23 RID: 19491
		public bool ActivePartStatusIgnoreCharge;

		// Token: 0x04004C24 RID: 19492
		public bool ActivePartStatusIgnoreBreakage;

		// Token: 0x04004C25 RID: 19493
		public bool ActivePartStatusIgnoreRust;

		// Token: 0x04004C26 RID: 19494
		public bool ActivePartStatusIgnoreEMP;

		// Token: 0x04004C27 RID: 19495
		public bool ActivePartStatusIgnoreRealityStabilization;

		// Token: 0x04004C28 RID: 19496
		public bool ActivePartStatusIgnoreSubject;

		// Token: 0x04004C29 RID: 19497
		public bool ActivePartStatusIgnoreLocallyDefinedFailure;

		// Token: 0x04004C2A RID: 19498
		public string RequiresEvent;

		// Token: 0x04004C2B RID: 19499
		public string RequiresInverseEvent;

		// Token: 0x04004C2C RID: 19500
		[NonSerialized]
		private bool? HasEvent;

		// Token: 0x04004C2D RID: 19501
		[NonSerialized]
		private bool? HasInverseEvent;

		// Token: 0x04004C2E RID: 19502
		public string RequiresEffect;

		// Token: 0x04004C2F RID: 19503
		public string RequiresInverseEffect;

		// Token: 0x04004C30 RID: 19504
		[NonSerialized]
		private bool? HasEffect;

		// Token: 0x04004C31 RID: 19505
		[NonSerialized]
		private bool? HasInverseEffect;

		// Token: 0x04004C32 RID: 19506
		public int HFlipPermyriadChancePerFrame;

		// Token: 0x04004C33 RID: 19507
		public bool HFlipChanceMultiplyByWindSpeed;

		// Token: 0x04004C34 RID: 19508
		public int VFlipPermyriadChancePerFrame;

		// Token: 0x04004C35 RID: 19509
		public bool VFlipChanceMultiplyByWindSpeed;

		// Token: 0x04004C36 RID: 19510
		public bool HFlipActive;

		// Token: 0x04004C37 RID: 19511
		public bool VFlipActive;

		// Token: 0x04004C38 RID: 19512
		public string AltRenderString;

		// Token: 0x04004C39 RID: 19513
		public int AltRenderStringPermyriadChancePerFrame;

		// Token: 0x04004C3A RID: 19514
		public bool AltRenderStringChanceMultiplyByWindSpeed;

		// Token: 0x04004C3B RID: 19515
		public bool AltRenderStringActive;

		// Token: 0x04004C3C RID: 19516
		public int LastFrame;

		// Token: 0x04004C3D RID: 19517
		public bool MustBeUnderstood;

		// Token: 0x04004C3E RID: 19518
		[NonSerialized]
		private bool? IsUnderstood;

		// Token: 0x04004C3F RID: 19519
		private int lastFrame = int.MinValue;
	}
}
