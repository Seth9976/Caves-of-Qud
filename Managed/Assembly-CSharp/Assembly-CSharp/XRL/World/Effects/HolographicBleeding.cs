using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D48 RID: 3400
	[Serializable]
	public class HolographicBleeding : Bleeding, ITierInitialized
	{
		// Token: 0x06008658 RID: 34392 RVA: 0x00305381 File Offset: 0x00303581
		public HolographicBleeding()
		{
		}

		// Token: 0x06008659 RID: 34393 RVA: 0x00305389 File Offset: 0x00303589
		public HolographicBleeding(string Damage = "1", int SaveTarget = 20, GameObject Owner = null, bool Stack = true, bool Internal = false, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false)
			: this()
		{
			this.Damage = Damage;
			this.SaveTarget = SaveTarget;
			this.Owner = Owner;
			this.Stack = Stack;
			this.Internal = Internal;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
		}

		// Token: 0x0600865A RID: 34394 RVA: 0x003053C6 File Offset: 0x003035C6
		public override int GetEffectType()
		{
			return 117440576;
		}

		// Token: 0x0600865B RID: 34395 RVA: 0x003053D0 File Offset: 0x003035D0
		public override void StartMessage(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_physicalRupture", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
				{
					base.DidX("begin", base.DisplayNameStripped + " from another wound", "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
					return;
				}
				base.DidX("begin", base.DisplayNameStripped, "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				return;
			}
			else
			{
				if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
				{
					base.DidX("begin", string.Concat(new string[] { "acting like ", Object.itis, " ", base.DisplayNameStripped, " from another wound" }), null, null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
					return;
				}
				base.DidX("begin", "acting like " + Object.itis + " " + base.DisplayNameStripped, null, null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				return;
			}
		}

		// Token: 0x0600865C RID: 34396 RVA: 0x00305554 File Offset: 0x00303754
		public override void StopMessage(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetIntProperty("Analgesia", 0) > 0)
				{
					if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
					{
						base.DidX("realize", "one of your wounds is an illusion", null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StopMessageUsePopup, null);
						return;
					}
					base.DidX("realize", "your wound is an illusion", null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StopMessageUsePopup, null);
					return;
				}
				else
				{
					if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
					{
						base.DidX("realize", "one of your wounds is an illusion, and the pain from it suddenly stops", null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StopMessageUsePopup, null);
						return;
					}
					base.DidX("realize", "your wound is an illusion, and the pain suddenly stops", null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StopMessageUsePopup, null);
					return;
				}
			}
			else
			{
				if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
				{
					base.DidX("stop", string.Concat(new string[] { "acting like ", Object.itis, " ", base.DisplayNameStripped, " so much" }), null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
					return;
				}
				base.DidX("stop", "acting like " + Object.itis + " " + base.DisplayNameStripped, null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				return;
			}
		}

		// Token: 0x0600865D RID: 34397 RVA: 0x00305716 File Offset: 0x00303916
		public override string DamageAttributes()
		{
			return "Bleeding Illusion Unavoidable";
		}

		// Token: 0x0600865E RID: 34398 RVA: 0x0030571D File Offset: 0x0030391D
		public override string SaveAttribute()
		{
			return "Intelligence";
		}

		// Token: 0x0600865F RID: 34399 RVA: 0x00305724 File Offset: 0x00303924
		public override string SaveVs()
		{
			return "Hologram Illusion";
		}

		// Token: 0x06008660 RID: 34400 RVA: 0x0030572B File Offset: 0x0030392B
		public override void SplashCreated(GameObject Object)
		{
			Object.AddPart<XRL.World.Parts.Temporary>(new XRL.World.Parts.Temporary(25), true, false);
			base.SplashCreated(Object);
		}
	}
}
