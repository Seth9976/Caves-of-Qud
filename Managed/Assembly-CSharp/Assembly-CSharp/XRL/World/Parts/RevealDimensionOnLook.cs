using System;
using Qud.API;
using XRL.Rules;
using XRL.World.Encounters;

namespace XRL.World.Parts
{
	// Token: 0x02001213 RID: 4627
	[Serializable]
	public class RevealDimensionOnLook : IPart
	{
		// Token: 0x0600B7EB RID: 47083 RVA: 0x0042E949 File Offset: 0x0042CB49
		public RevealDimensionOnLook()
		{
		}

		// Token: 0x0600B7EC RID: 47084 RVA: 0x0042E951 File Offset: 0x0042CB51
		public RevealDimensionOnLook(string SecretID)
		{
			this.SecretID = SecretID;
		}

		// Token: 0x0600B7ED RID: 47085 RVA: 0x0042E960 File Offset: 0x0042CB60
		public override bool SameAs(IPart p)
		{
			return !((p as RevealDimensionOnLook).SecretID != this.SecretID) && base.SameAs(p);
		}

		// Token: 0x0600B7EE RID: 47086 RVA: 0x0042E983 File Offset: 0x0042CB83
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B7EF RID: 47087 RVA: 0x0042E999 File Offset: 0x0042CB99
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.CheckConfiguration();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7F0 RID: 47088 RVA: 0x0042E9A9 File Offset: 0x0042CBA9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7F1 RID: 47089 RVA: 0x0042E9C0 File Offset: 0x0042CBC0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt && this.CheckConfiguration() && this.ParentObject.Understood())
			{
				JournalAPI.RevealObservation(this.SecretID, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B7F2 RID: 47090 RVA: 0x0042EA10 File Offset: 0x0042CC10
		private bool CheckConfiguration()
		{
			if (string.IsNullOrEmpty(this.SecretID))
			{
				XRLGame game = The.Game;
				DimensionManager dimensionManager = ((game != null) ? game.GetObjectGameState("DimensionManager") : null) as DimensionManager;
				if (dimensionManager != null)
				{
					int num = Stat.Random(0, dimensionManager.ExtraDimensions.Count + dimensionManager.PsychicFactions.Count - 1);
					if (num < dimensionManager.ExtraDimensions.Count)
					{
						ExtraDimension extraDimension = dimensionManager.ExtraDimensions[num];
						this.SecretID = extraDimension.SecretID;
					}
					else
					{
						num -= dimensionManager.ExtraDimensions.Count;
						PsychicFaction psychicFaction = dimensionManager.PsychicFactions[num];
						this.SecretID = psychicFaction.dimensionSecretID;
					}
				}
			}
			return !string.IsNullOrEmpty(this.SecretID);
		}

		// Token: 0x04004A03 RID: 18947
		public string SecretID;

		// Token: 0x04004A04 RID: 18948
		public bool LookedAt;
	}
}
