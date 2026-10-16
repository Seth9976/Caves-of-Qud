using System;
using XRL.World;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x0200051B RID: 1307
	[Serializable]
	public class SifrahEffectChance
	{
		// Token: 0x060037FB RID: 14331 RVA: 0x001117D0 File Offset: 0x0010F9D0
		public SifrahEffectChance()
		{
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x001117D8 File Offset: 0x0010F9D8
		public SifrahEffectChance(string EffectName, int Chance, string Duration = null, string DisplayName = null, bool Stack = false, bool Force = false, Effect EffectInstance = null)
			: this()
		{
			this.EffectName = EffectName;
			this.Chance = Chance;
			this.Duration = Duration;
			this.DisplayName = DisplayName;
			this.Stack = Stack;
			this.Force = Force;
			this.EffectInstance = EffectInstance;
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x00111818 File Offset: 0x0010FA18
		public Effect GetEffectInstance()
		{
			if (this.EffectInstance == null)
			{
				this.EffectInstance = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + this.EffectName, false, false, true)) as Effect;
			}
			if (!this.Duration.IsNullOrEmpty())
			{
				this.EffectInstance.Duration = this.Duration.RollCached();
			}
			if (!this.DisplayName.IsNullOrEmpty())
			{
				this.EffectInstance.DisplayName = this.DisplayName;
			}
			return this.EffectInstance;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x0011189C File Offset: 0x0010FA9C
		public bool Apply(GameObject Subject)
		{
			Effect effectInstance = this.GetEffectInstance();
			if (this.Force)
			{
				Subject.ForceApplyEffect(effectInstance, null);
			}
			else
			{
				Subject.ApplyEffect(effectInstance, null);
			}
			return true;
		}

		// Token: 0x04001C0E RID: 7182
		public string EffectName;

		// Token: 0x04001C0F RID: 7183
		public int Chance;

		// Token: 0x04001C10 RID: 7184
		public string Duration;

		// Token: 0x04001C11 RID: 7185
		public string DisplayName;

		// Token: 0x04001C12 RID: 7186
		public bool Stack;

		// Token: 0x04001C13 RID: 7187
		public bool Force;

		// Token: 0x04001C14 RID: 7188
		public Effect EffectInstance;
	}
}
