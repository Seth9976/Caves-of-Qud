using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D6E RID: 3438
	[Serializable]
	public class Phased : Effect, ITierInitialized
	{
		// Token: 0x06008821 RID: 34849 RVA: 0x0030C5F0 File Offset: 0x0030A7F0
		public Phased()
		{
			this.DisplayName = "{{g|phased}}";
			this.FrameOffset = Stat.Random(1, 60);
		}

		// Token: 0x06008822 RID: 34850 RVA: 0x0030C61C File Offset: 0x0030A81C
		public Phased(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008823 RID: 34851 RVA: 0x0030C62B File Offset: 0x0030A82B
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 100);
		}

		// Token: 0x06008824 RID: 34852 RVA: 0x0030C63C File Offset: 0x0030A83C
		public Phased(Phased Source)
			: this()
		{
			this.Duration = Source.Duration;
			this.FrameOffset = Source.FrameOffset;
			this.FlickerFrame = Source.FlickerFrame;
			this.Tile = Source.Tile;
			this.RenderString = Source.RenderString;
		}

		// Token: 0x06008825 RID: 34853 RVA: 0x0030C68B File Offset: 0x0030A88B
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008826 RID: 34854 RVA: 0x0030C68E File Offset: 0x0030A88E
		public override int GetEffectType()
		{
			return 256;
		}

		// Token: 0x06008827 RID: 34855 RVA: 0x0030C698 File Offset: 0x0030A898
		public override bool SameAs(Effect e)
		{
			Phased phased = e as Phased;
			return !(phased.Tile != this.Tile) && !(phased.RenderString != this.RenderString) && base.SameAs(e);
		}

		// Token: 0x06008828 RID: 34856 RVA: 0x0030C6DD File Offset: 0x0030A8DD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || (ID == EffectAppliedEvent.ID && !this.WasPhased) || ID == EnteredCellEvent.ID || ID == PooledEvent<RealityStabilizeEvent>.ID || ID == WasDerivedFromEvent.ID;
		}

		// Token: 0x06008829 RID: 34857 RVA: 0x0030C71C File Offset: 0x0030A91C
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (!this.WasPhased && E.Effect == this)
			{
				base.Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftOut", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You phase out.", null, true);
				}
				base.Object.FireEvent("AfterPhaseOut");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600882A RID: 34858 RVA: 0x0030C79A File Offset: 0x0030A99A
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.Derivation.ForceApplyEffect(new Phased(this), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600882B RID: 34859 RVA: 0x0030C7B8 File Offset: 0x0030A9B8
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0 && this.Duration != 9999 && base.Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You will phase back in in " + this.Duration.Things("round", null) + ".", null, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600882C RID: 34860 RVA: 0x0030C816 File Offset: 0x0030AA16
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Duration != 9999 && E.Cell.OnWorldMap())
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600882D RID: 34861 RVA: 0x0030C848 File Offset: 0x0030AA48
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(true))
			{
				GameObject @object = base.Object;
				@object.RemoveEffect(this, true);
				if (!@object.IsValid())
				{
					return false;
				}
				if (@object.GetPhase() == 1)
				{
					@object.TakeDamage("2d6".RollCached(), "from being forced into phase.", "Normality Phase Unavoidable", null, null, null, E.Effect.Owner, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					if (!@object.IsValid())
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600882E RID: 34862 RVA: 0x0030C8C9 File Offset: 0x0030AAC9
		public override string GetDetails()
		{
			return "Can't physically interact with creatures and objects unless they're also phased.\nCan pass through solids.";
		}

		// Token: 0x0600882F RID: 34863 RVA: 0x0030C8D0 File Offset: 0x0030AAD0
		public override bool Apply(GameObject Object)
		{
			this.WasPhased = Object.HasEffect<Phased>();
			if (!Object.FireEvent("ApplyPhased"))
			{
				return false;
			}
			this.Tile = Object.Render.Tile;
			this.RenderString = Object.Render.RenderString;
			base.FlushNavigationCaches();
			return true;
		}

		// Token: 0x06008830 RID: 34864 RVA: 0x0030C924 File Offset: 0x0030AB24
		public override void Remove(GameObject Object)
		{
			if (!Object.HasEffectOtherThan(typeof(Phased), this))
			{
				Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftIn", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You phase back in.", null, true);
				}
				if (Object.CurrentCell != null && !Object.OnWorldMap())
				{
					foreach (GameObject gameObject in Object.CurrentCell.GetObjectsWithPart("Physics"))
					{
						if (gameObject != Object && gameObject.Physics.Solid && (!gameObject.HasTagOrProperty("Flyover") || !Object.IsFlying))
						{
							List<Cell> list = new List<Cell>(8);
							Object.CurrentCell.GetAdjacentCells(1, list, false, true);
							Cell cell = null;
							for (int i = 0; i < list.Count; i++)
							{
								cell = list[i];
								for (int j = 0; j < list[i].Objects.Count; j++)
								{
									if ((!list[i].Objects[j].HasPart<Forcefield>() || list[i].Objects[j].GetPart<Forcefield>().Creator != Object || !list[i].Objects[j].GetPart<Forcefield>().MovesWithOwner) && list[i].Objects[j].Physics != null && list[i].Objects[j].Physics.Solid && (!list[i].Objects[j].HasTagOrProperty("Flyover") || !Object.IsFlying))
									{
										cell = null;
										break;
									}
								}
								if (cell != null)
								{
									break;
								}
							}
							if (cell != null)
							{
								Object.DirectMoveTo(cell, 0, false, false, false, null);
								break;
							}
							if (Object.IsPlayer())
							{
								Achievement.VIOLATE_PAULI.Unlock();
							}
							Object.DilationSplat();
							Object.Die(gameObject, null, "You violated the Pauli exclusion principle.", Object.It + " @@violated the Pauli exclusion principle.", true, null, null, false, false, null, null, null);
						}
					}
				}
				base.FlushNavigationCaches();
				Object.FireEvent("AfterPhaseIn");
				Object.Gravitate();
			}
			base.Remove(Object);
		}

		// Token: 0x06008831 RID: 34865 RVA: 0x0030CBC0 File Offset: 0x0030ADC0
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num;
				if (base.Object.HasTag("Astral"))
				{
					base.Object.Render.Tile = null;
					num = (XRLCore.CurrentFrame + this.FrameOffset) % 400;
					if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
					{
						if (this.FlickerFrame == 0)
						{
							base.Object.Render.RenderString = "_";
						}
						else if (this.FlickerFrame == 1)
						{
							base.Object.Render.RenderString = "-";
						}
						else if (this.FlickerFrame == 2)
						{
							base.Object.Render.RenderString = "|";
						}
						E.ColorString = "&K";
						if (this.FlickerFrame == 0)
						{
							this.FlickerFrame = 3;
						}
						this.FlickerFrame--;
					}
					else
					{
						base.Object.Render.RenderString = this.RenderString;
						base.Object.Render.Tile = this.Tile;
					}
					if (num < 4)
					{
						base.Object.Render.ColorString = "&Y";
						base.Object.Render.DetailColor = "k";
					}
					else if (num < 8)
					{
						base.Object.Render.ColorString = "&y";
						base.Object.Render.DetailColor = "K";
					}
					else if (num < 12)
					{
						base.Object.Render.ColorString = "&k";
						base.Object.Render.DetailColor = "y";
					}
					else
					{
						base.Object.Render.ColorString = "&K";
						base.Object.Render.DetailColor = "y";
					}
					if (!Options.DisableTextAnimationEffects)
					{
						this.FrameOffset += Stat.Random(0, 20);
					}
					if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
					{
						base.Object.Render.ColorString = "&K";
					}
					return true;
				}
				num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				num /= 2;
				num %= 6;
				if (num == 0)
				{
					E.ApplyColors("&k", 100);
				}
				if (num == 1)
				{
					E.ApplyColors("&K", 100);
				}
				if (num == 2)
				{
					E.ApplyColors("&c", 100);
				}
				if (num == 4)
				{
					E.ApplyColors("&y", 100);
				}
			}
			return true;
		}

		// Token: 0x06008832 RID: 34866 RVA: 0x0030CE45 File Offset: 0x0030B045
		public override bool allowCopyOnNoEffectDeepCopy()
		{
			return true;
		}

		// Token: 0x04003A3E RID: 14910
		private int FrameOffset;

		// Token: 0x04003A3F RID: 14911
		private int FlickerFrame;

		// Token: 0x04003A40 RID: 14912
		public string Tile;

		// Token: 0x04003A41 RID: 14913
		public string RenderString = "@";

		// Token: 0x04003A42 RID: 14914
		public bool WasPhased;
	}
}
