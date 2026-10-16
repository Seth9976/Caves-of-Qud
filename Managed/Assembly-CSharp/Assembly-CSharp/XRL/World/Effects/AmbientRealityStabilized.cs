using System;
using XRL.UI;
using XRL.World.ZoneParts;

namespace XRL.World.Effects
{
	// Token: 0x02000C30 RID: 3120
	[Serializable]
	public class AmbientRealityStabilized : Effect
	{
		// Token: 0x06007F03 RID: 32515 RVA: 0x002EE90F File Offset: 0x002ECB0F
		public AmbientRealityStabilized()
		{
			this.Duration = 1;
			this.DisplayName = "{{Y|astral friction}}";
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x002EE930 File Offset: 0x002ECB30
		public override string GetStateDescription()
		{
			return "{{Y|subject to astral friction}}";
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x002EE937 File Offset: 0x002ECB37
		public AmbientRealityStabilized(int Strength)
			: this()
		{
			this.Strength = Strength;
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x002EE946 File Offset: 0x002ECB46
		public AmbientRealityStabilized(int Strength, GameObject Owner)
			: this(Strength)
		{
			this.Owner = Owner;
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x002EE956 File Offset: 0x002ECB56
		public override int GetEffectType()
		{
			return 16777280;
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x002EE95D File Offset: 0x002ECB5D
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x002EE960 File Offset: 0x002ECB60
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x002EE978 File Offset: 0x002ECB78
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject @object = base.Object;
			AmbientStabilization ambientStabilization;
			if (@object == null)
			{
				ambientStabilization = null;
			}
			else
			{
				Zone currentZone = @object.CurrentZone;
				ambientStabilization = ((currentZone != null) ? currentZone.GetPart<AmbientStabilization>() : null);
			}
			AmbientStabilization ambientStabilization2 = ambientStabilization;
			if (ambientStabilization2 == null)
			{
				if (this.Duration > 0)
				{
					GameObject object2 = base.Object;
					if (object2 != null && object2.IsPlayer())
					{
						Popup.Show("The astral friction diffuses.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					GameObject object3 = base.Object;
					object obj = ((object3 != null) ? object3.GetEffect<RealityStabilized>() : null);
					this.Duration = 0;
					GameObject object4 = base.Object;
					if (object4 != null)
					{
						object4.RemoveEffect(this, true);
					}
					object obj2 = obj;
					if (obj2 != null)
					{
						obj2.Maintain();
					}
					return false;
				}
			}
			else
			{
				this.Strength = ambientStabilization2.Strength;
				this.ApplyRealityStabilization(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x002EEA2F File Offset: 0x002ECC2F
		public override bool Apply(GameObject Object)
		{
			return !Object.HasEffect<AmbientRealityStabilized>() && Object.FireEvent("ApplyAmbientRealityStabilized") && this.ApplyRealityStabilization(true);
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x002EEA54 File Offset: 0x002ECC54
		public bool ApplyRealityStabilization(bool Initial = false)
		{
			RealityStabilized realityStabilized = base.Object.GetEffect<RealityStabilized>();
			if (realityStabilized == null)
			{
				realityStabilized = new RealityStabilized(this.Strength, this.Owner, 1, 0, 0, 2, false);
				if (!base.Object.ForceApplyEffect(realityStabilized, null))
				{
					return false;
				}
			}
			else
			{
				realityStabilized.Strength = this.Strength;
				if (Initial)
				{
					realityStabilized.SynchronizeEffect();
				}
			}
			return true;
		}

		// Token: 0x040038B0 RID: 14512
		public int Strength;

		// Token: 0x040038B1 RID: 14513
		public int Visibility = 2;

		// Token: 0x040038B2 RID: 14514
		public bool Projective;

		// Token: 0x040038B3 RID: 14515
		public GameObject Owner;
	}
}
