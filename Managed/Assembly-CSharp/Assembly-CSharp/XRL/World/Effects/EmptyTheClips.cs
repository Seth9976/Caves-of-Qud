using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D31 RID: 3377
	[Serializable]
	public class EmptyTheClips : Effect, ITierInitialized
	{
		// Token: 0x06008527 RID: 34087 RVA: 0x003010A8 File Offset: 0x002FF2A8
		public EmptyTheClips()
		{
			this.DisplayName = "Empty the Clips";
		}

		// Token: 0x06008528 RID: 34088 RVA: 0x003010BB File Offset: 0x002FF2BB
		public EmptyTheClips(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x003010CA File Offset: 0x002FF2CA
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 30);
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x003010DB File Offset: 0x002FF2DB
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x0600852B RID: 34091 RVA: 0x003010E2 File Offset: 0x002FF2E2
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600852C RID: 34092 RVA: 0x003010E5 File Offset: 0x002FF2E5
		public override string GetDetails()
		{
			return "Fires pistols twice as quickly.";
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x003010EC File Offset: 0x002FF2EC
		public override string GetDescription()
		{
			return "emptying the clips";
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x003010F4 File Offset: 0x002FF2F4
		public override bool Apply(GameObject Object)
		{
			EmptyTheClips emptyTheClips;
			if (Object.TryGetEffect<EmptyTheClips>(out emptyTheClips))
			{
				if (this.Duration > emptyTheClips.Duration)
				{
					emptyTheClips.Duration = this.Duration;
				}
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyEmptyTheClips", "Effect", this)))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_emptyTheClips", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			List<GameObject> missileWeapons = Object.GetMissileWeapons(null, new Predicate<MissileWeapon>(Pistol_EmptyTheClips.IsPistol));
			if (missileWeapons.IsNullOrEmpty<GameObject>())
			{
				if (Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You begin itching for a trigger.", 'G', true);
				}
			}
			else if (missileWeapons.Count == 1)
			{
				base.DidX("clasp", Object.its + missileWeapons[0].DisplayNameOnlyDirectAndStripped + " eagerly", null, null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			else
			{
				base.DidX("clasp", Object.its + " pistols eagerly", null, null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return true;
		}

		// Token: 0x0600852F RID: 34095 RVA: 0x0030120C File Offset: 0x002FF40C
		public override void Remove(GameObject Object)
		{
			base.DidX("loosen", Object.its + " pistol grip", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			base.Remove(Object);
		}

		// Token: 0x06008530 RID: 34096 RVA: 0x0030124C File Offset: 0x002FF44C
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 45 && num < 55)
				{
					E.Tile = null;
					E.RenderString = "\u001a";
					E.ColorString = "&B";
				}
			}
			return true;
		}
	}
}
