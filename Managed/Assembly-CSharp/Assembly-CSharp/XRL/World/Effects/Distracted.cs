using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D2A RID: 3370
	[Serializable]
	public class Distracted : Effect
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060084D9 RID: 34009 RVA: 0x002FFFD8 File Offset: 0x002FE1D8
		// (set) Token: 0x060084DA RID: 34010 RVA: 0x00300003 File Offset: 0x002FE203
		public Distraction Distraction
		{
			get
			{
				Distraction distraction;
				if ((distraction = this._Distraction) == null)
				{
					distraction = (this._Distraction = this.Target.GetPart<Distraction>());
				}
				return distraction;
			}
			set
			{
				this._Distraction = value;
				this.Target = ((value != null) ? value.ParentObject : null);
			}
		}

		// Token: 0x060084DB RID: 34011 RVA: 0x0030001E File Offset: 0x002FE21E
		public Distracted()
		{
			this.DisplayName = "distracted by a decoy";
			this.Duration = 1;
		}

		// Token: 0x060084DC RID: 34012 RVA: 0x00300038 File Offset: 0x002FE238
		public Distracted(Distraction Distraction)
			: this()
		{
			this.Distraction = Distraction;
		}

		// Token: 0x060084DD RID: 34013 RVA: 0x00300047 File Offset: 0x002FE247
		public override string GetDetails()
		{
			return "Has a decoy object as " + base.Object.its + " target.";
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x00300063 File Offset: 0x002FE263
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x0030006A File Offset: 0x002FE26A
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID;
		}

		// Token: 0x060084E0 RID: 34016 RVA: 0x00300074 File Offset: 0x002FE274
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			if (!GameObject.Validate(ref this.Target))
			{
				this.Duration = 0;
			}
			else
			{
				int num = this.Duration + 1;
				this.Duration = num;
				if (num > this.Distraction.SaveTurns)
				{
					if (this.Distraction.MakeSave(base.Object))
					{
						this.Duration = 0;
					}
					else
					{
						this.Duration = 1;
					}
				}
				if (this.Duration > 0 && base.Object.Brain.Target != this.Target)
				{
					this.ChangeTarget();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060084E1 RID: 34017 RVA: 0x00300106 File Offset: 0x002FE306
		public override void Applied(GameObject Object)
		{
			this.ChangeTarget();
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x0030010E File Offset: 0x002FE30E
		public void ChangeTarget()
		{
			base.Object.Brain.Target = null;
			base.Object.Brain.WantToKill(this.Target, "out of distraction", true);
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x00300140 File Offset: 0x002FE340
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Target) && Object.Brain.Target == this.Target)
			{
				Object.Brain.Target = null;
				if (GameObject.Validate(this.Distraction.Original) && Object.IsHostileTowards(this.Distraction.Original))
				{
					Object.Brain.WantToKill(this.Distraction.Original, null, false);
				}
			}
		}

		// Token: 0x040039A2 RID: 14754
		public GameObject Target;

		// Token: 0x040039A3 RID: 14755
		[NonSerialized]
		private Distraction _Distraction;
	}
}
