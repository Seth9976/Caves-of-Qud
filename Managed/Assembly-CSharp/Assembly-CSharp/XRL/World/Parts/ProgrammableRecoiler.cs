using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001081 RID: 4225
	[Serializable]
	public class ProgrammableRecoiler : IProgrammableRecoiler
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600A821 RID: 43041 RVA: 0x003D7C14 File Offset: 0x003D5E14
		// (set) Token: 0x0600A822 RID: 43042 RVA: 0x003D7C1C File Offset: 0x003D5E1C
		public string NameElide
		{
			get
			{
				return this._NameElide;
			}
			set
			{
				this._NameElide = value;
				this.NameElideWork = null;
			}
		}

		// Token: 0x0600A823 RID: 43043 RVA: 0x003D7C2C File Offset: 0x003D5E2C
		public override void ProgrammedForLocation(Zone Z, Cell C)
		{
			this.NameCache = null;
			this.NameCacheTick = 0;
		}

		// Token: 0x0600A824 RID: 43044 RVA: 0x003D7C3C File Offset: 0x003D5E3C
		public override bool SameAs(IPart p)
		{
			return !((p as ProgrammableRecoiler)._NameElide != this._NameElide) && base.SameAs(p);
		}

		// Token: 0x0600A825 RID: 43045 RVA: 0x003D7C5F File Offset: 0x003D5E5F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == GetInventoryActionsEvent.ID && (this.Reprogrammable || this.TimesProgrammed < 1)) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A826 RID: 43046 RVA: 0x003D7C98 File Offset: 0x003D5E98
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if ((this.Reprogrammable || this.TimesProgrammed < 1) && this.IsObjectActivePartSubject(IComponent<GameObject>.ThePlayer))
			{
				E.AddAction("Imprint", "imprint", ProgrammableRecoiler.COMMAND_NAME, null, 'i', false, (this.TimesProgrammed < 1) ? 200 : 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A827 RID: 43047 RVA: 0x003D7D00 File Offset: 0x003D5F00
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == ProgrammableRecoiler.COMMAND_NAME && base.ProgramRecoiler(E.Actor, E))
			{
				E.Actor.UseEnergy(1000, "Item Recoiler Imprint", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A828 RID: 43048 RVA: 0x003D7D5C File Offset: 0x003D5F5C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				if (The.ZoneManager != null && (this.NameCache == null || The.ZoneManager.NameUpdateTick > this.NameCacheTick))
				{
					Teleporter teleporter;
					if (this.ParentObject.TryGetPart<Teleporter>(out teleporter))
					{
						string destinationZone = teleporter.DestinationZone;
						if (!destinationZone.IsNullOrEmpty())
						{
							string text = ProgrammableRecoiler.FindZoneName(destinationZone);
							if (!text.IsNullOrEmpty())
							{
								text += " ";
							}
							this.NameCache = text;
						}
					}
					this.NameCacheTick = The.ZoneManager.NameUpdateTick;
				}
				if (!this.NameCache.IsNullOrEmpty())
				{
					string text2 = E.DB.PrimaryBase;
					int num = 10;
					if (text2 == null)
					{
						text2 = "recoiler";
					}
					else
					{
						num = E.DB[text2];
						E.DB.Remove(text2);
					}
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(this.NameCache);
					if (!this.NameElide.IsNullOrEmpty())
					{
						if (this.NameElideWork == null)
						{
							this.NameElideWork = this.NameElide + " ";
						}
						if (text2.Contains(this.NameElideWork))
						{
							text2 = text2.Replace(this.NameElideWork, "");
						}
						if (text2.Contains(this.NameElide))
						{
							text2 = text2.Replace(this.NameElide, "");
						}
					}
					stringBuilder.Append(text2);
					E.AddBase(stringBuilder.ToString(), num - 10, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A829 RID: 43049 RVA: 0x003D7EE0 File Offset: 0x003D60E0
		private static string FindZoneName(string ZoneID)
		{
			string text = null;
			if (The.ZoneManager.GetZoneHasProperName(ZoneID))
			{
				text = The.ZoneManager.GetZoneBaseDisplayName(ZoneID, true);
			}
			if (text.IsNullOrEmpty())
			{
				text = The.ZoneManager.GetZoneNameContext(ZoneID) ?? The.ZoneManager.GetZoneReferenceDisplayName(ZoneID);
			}
			if (text.StartsWith("the ") || text.StartsWith("The "))
			{
				text = text.Substring(4);
			}
			else if (text.StartsWith("an ") || text.StartsWith("An "))
			{
				text = text.Substring(3);
			}
			else if (text.StartsWith("a ") || text.StartsWith("A "))
			{
				text = text.Substring(2);
			}
			if (text.EndsWith(", surface"))
			{
				text = text.Substring(0, text.Length - ", surface".Length);
			}
			return text;
		}

		// Token: 0x04004563 RID: 17763
		public static readonly string COMMAND_NAME = "ImprintRecoiler";

		// Token: 0x04004564 RID: 17764
		public string _NameElide;

		// Token: 0x04004565 RID: 17765
		[NonSerialized]
		private int NameCacheTick;

		// Token: 0x04004566 RID: 17766
		[NonSerialized]
		private string NameCache;

		// Token: 0x04004567 RID: 17767
		[NonSerialized]
		private string NameElideWork;
	}
}
