using System;

namespace XRL.World.Parts
{
	// Token: 0x020010AB RID: 4267
	[Serializable]
	public class TauShard : IPart
	{
		// Token: 0x0600A9C1 RID: 43457 RVA: 0x003E1328 File Offset: 0x003DF528
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == ZoneActivatedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600A9C2 RID: 43458 RVA: 0x003E1350 File Offset: 0x003DF550
		public bool TauKilled
		{
			get
			{
				XRLGame game = The.Game;
				string text = ((game != null) ? game.GetStringGameState("TauElse", "") : null);
				return text != null && text.EndsWith("KilledByPlayer");
			}
		}

		// Token: 0x0600A9C3 RID: 43459 RVA: 0x003E1389 File Offset: 0x003DF589
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.TauKilled)
			{
				E.ReplacementObject = GameObjectFactory.Factory.CreateObject("TauDagger");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9C4 RID: 43460 RVA: 0x003E13AF File Offset: 0x003DF5AF
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.TauKilled)
			{
				this.ParentObject.ReplaceWith("TauDagger");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9C5 RID: 43461 RVA: 0x003E13D1 File Offset: 0x003DF5D1
		public override bool HandleEvent(TakenEvent E)
		{
			if (this.TauKilled)
			{
				this.ParentObject.ReplaceWith("TauDagger");
			}
			return base.HandleEvent(E);
		}
	}
}
