using System;
using HistoryKit;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001292 RID: 4754
	[Serializable]
	public class SultanMural : IPart
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600BC15 RID: 48149 RVA: 0x0044BD23 File Offset: 0x00449F23
		// (set) Token: 0x0600BC16 RID: 48150 RVA: 0x0044BD58 File Offset: 0x00449F58
		public HistoricEvent secretEvent
		{
			get
			{
				if (this._secretEvent == null && this.MuralEventID != -1L)
				{
					this._secretEvent = The.Game.sultanHistory.GetEvent(this.MuralEventID);
				}
				return this._secretEvent;
			}
			set
			{
				this._secretEvent = value;
				this.MuralEventID = ((value != null) ? value.id : (-1L));
			}
		}

		// Token: 0x0600BC17 RID: 48151 RVA: 0x0044BD74 File Offset: 0x00449F74
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC18 RID: 48152 RVA: 0x0044BD77 File Offset: 0x00449F77
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.MuralEventID);
			Writer.WriteOptimized(this.secretID);
		}

		// Token: 0x0600BC19 RID: 48153 RVA: 0x0044BD91 File Offset: 0x00449F91
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.MuralEventID = Reader.ReadOptimizedInt64();
			this.secretID = Reader.ReadOptimizedString();
		}

		// Token: 0x0600BC1A RID: 48154 RVA: 0x0044BDAB File Offset: 0x00449FAB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID;
		}

		// Token: 0x0600BC1B RID: 48155 RVA: 0x0044BDC1 File Offset: 0x00449FC1
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BC1C RID: 48156 RVA: 0x0044BDF7 File Offset: 0x00449FF7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC1D RID: 48157 RVA: 0x0044BE0C File Offset: 0x0044A00C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !string.IsNullOrEmpty(this.secretID))
			{
				JournalAPI.RevealSultanEventBySecretID(this.secretID, this.ParentObject.DisplayName);
			}
			return true;
		}

		// Token: 0x0600BC1E RID: 48158 RVA: 0x0044BE45 File Offset: 0x0044A045
		public bool HasUnrevealedSecret()
		{
			return this.secretID != null && JournalAPI.HasUnrevealedSultanEvent(this.secretID);
		}

		// Token: 0x04004B5E RID: 19294
		public long MuralEventID = -1L;

		// Token: 0x04004B5F RID: 19295
		public string secretID;

		// Token: 0x04004B60 RID: 19296
		[NonSerialized]
		private HistoricEvent _secretEvent;
	}
}
