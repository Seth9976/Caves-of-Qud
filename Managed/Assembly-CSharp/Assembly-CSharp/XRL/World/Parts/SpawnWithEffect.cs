using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F03 RID: 3843
	[Serializable]
	public class SpawnWithEffect : IPart
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06009954 RID: 39252 RVA: 0x0037A780 File Offset: 0x00378980
		private Type effectType
		{
			get
			{
				if (this._effectType == null)
				{
					if (!this.Effect.Contains("."))
					{
						this._effectType = ModManager.ResolveType("XRL.World.Effects." + this.Effect, false, false, true);
					}
					else
					{
						this._effectType = ModManager.ResolveType(this.Effect, false, false, true);
					}
				}
				return this._effectType;
			}
		}

		// Token: 0x06009955 RID: 39253 RVA: 0x0037A7E7 File Offset: 0x003789E7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x06009956 RID: 39254 RVA: 0x0037A800 File Offset: 0x00378A00
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (!string.IsNullOrEmpty(this.Effect) && this.Chance.in100())
			{
				Type effectType = this.effectType;
				if (this.ParentObject.HasEffect(effectType))
				{
					this.ParentObject.GetEffect(effectType).Duration = 9999;
				}
				else
				{
					Effect effect = Activator.CreateInstance(effectType) as Effect;
					effect.Duration = 9999;
					this.ParentObject.ApplyEffect(effect, null);
				}
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x06009957 RID: 39255 RVA: 0x0037A88C File Offset: 0x00378A8C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x04003FE2 RID: 16354
		public string Effect;

		// Token: 0x04003FE3 RID: 16355
		public int Chance = 100;

		// Token: 0x04003FE4 RID: 16356
		[NonSerialized]
		private Type _effectType;
	}
}
