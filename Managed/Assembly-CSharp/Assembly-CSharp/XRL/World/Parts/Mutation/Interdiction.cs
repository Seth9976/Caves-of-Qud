using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141C RID: 5148
	[Serializable]
	public class Interdiction : BaseMutation
	{
		// Token: 0x0600C842 RID: 51266 RVA: 0x0048E614 File Offset: 0x0048C814
		public Interdiction()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C843 RID: 51267 RVA: 0x0048E62F File Offset: 0x0048C82F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C844 RID: 51268 RVA: 0x0048E644 File Offset: 0x0048C844
		public void StopInterdiction()
		{
			if (this.interdictTarget != null)
			{
				Interdicted effect = this.interdictTarget.GetEffect<Interdicted>((Interdicted fx) => this.ParentObject.IDMatch(fx.interdictorId));
				this.interdictTarget.RemoveEffect(effect, true);
				this.interdictTarget = null;
			}
		}

		// Token: 0x0600C845 RID: 51269 RVA: 0x0048E688 File Offset: 0x0048C888
		public void BeginInterdiction(GameObject target)
		{
			this.StopInterdiction();
			if (!this.ParentObject.IsEMPed() && target.PhaseMatches(3) && this.ParentObject.HasLOSTo(target, true, false, false, true, null))
			{
				target.ApplyEffect(new Interdicted(this.ParentObject.ID, this.SpeedPenalty), null);
				this.interdictTarget = target;
				base.DidXToY("lock", "onto", target, null, null, null, null, null, target, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0600C846 RID: 51270 RVA: 0x0048E710 File Offset: 0x0048C910
		public void CheckInterdiction()
		{
			if (this.interdictTarget != null)
			{
				if (this.ParentObject.IsEMPed())
				{
					this.StopInterdiction();
				}
				if (!this.interdictTarget.PhaseMatches(3))
				{
					this.StopInterdiction();
				}
				if (!this.ParentObject.HasLOSTo(this.interdictTarget, true, false, false, true, null))
				{
					this.StopInterdiction();
				}
			}
		}

		// Token: 0x0600C847 RID: 51271 RVA: 0x0048E76C File Offset: 0x0048C96C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeTakeAction")
			{
				this.CheckInterdiction();
				if (this.ParentObject.Target != null && (this.interdictTarget != this.ParentObject.Target || !this.interdictTarget.HasEffect<Interdicted>((Interdicted FX) => FX.interdictorId == this.ParentObject.ID)))
				{
					this.BeginInterdiction(this.ParentObject.Target);
				}
			}
			return true;
		}

		// Token: 0x0600C848 RID: 51272 RVA: 0x0048E7DC File Offset: 0x0048C9DC
		public override bool Render(RenderEvent E)
		{
			if (this.interdictTarget != null)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600C849 RID: 51273 RVA: 0x0048E7F0 File Offset: 0x0048C9F0
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.interdictTarget != null && GameObject.Validate(this.ParentObject))
			{
				List<Tuple<Cell, char>> lineTo = this.ParentObject.GetLineTo(this.interdictTarget, true, false);
				if (lineTo == null || lineTo.Count <= 0)
				{
					return;
				}
				if (this.lastms == 0L)
				{
					this.lastms = IComponent<GameObject>.frameTimerMS;
				}
				else if (IComponent<GameObject>.frameTimerMS - this.lastms > 200L)
				{
					this.glowstep += 1L;
				}
				if (this.glowstep >= (long)lineTo.Count)
				{
					this.glowstep = 0L;
				}
				for (int i = 0; i < lineTo.Count; i++)
				{
					char c = 'c';
					if ((long)i == this.glowstep)
					{
						c = 'C';
					}
					Tuple<Cell, char> tuple = lineTo[i];
					buffer.Goto(tuple.Item1.X, tuple.Item1.Y);
					buffer.Buffer[tuple.Item1.X, tuple.Item1.Y].SetBackground(c);
					buffer.Buffer[tuple.Item1.X, tuple.Item1.Y].Detail = ColorUtility.ColorMap[c];
				}
			}
		}

		// Token: 0x0600C84A RID: 51274 RVA: 0x0048E92C File Offset: 0x0048CB2C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C84B RID: 51275 RVA: 0x0048E935 File Offset: 0x0048CB35
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C84C RID: 51276 RVA: 0x0048E93F File Offset: 0x0048CB3F
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004E74 RID: 20084
		public GameObject interdictTarget;

		// Token: 0x04004E75 RID: 20085
		public int SpeedPenalty = 10;

		// Token: 0x04004E76 RID: 20086
		[NonSerialized]
		private long lastms;

		// Token: 0x04004E77 RID: 20087
		[NonSerialized]
		private long glowstep;
	}
}
