using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FE9 RID: 4073
	[Serializable]
	public class ActionForwarder : IPart
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x0600A24F RID: 41551 RVA: 0x003B52E2 File Offset: 0x003B34E2
		public Type PartType
		{
			get
			{
				if (this._PartType == null)
				{
					this._PartType = ModManager.ResolveType("XRL.World.Parts." + this.Part, false, true, true);
				}
				return this._PartType;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600A250 RID: 41552 RVA: 0x003B5310 File Offset: 0x003B3510
		public IPart Instance
		{
			get
			{
				if (this._Instance == null || !this._Instance.IsValid)
				{
					Cell cell = this.ParentObject.CurrentCell;
					if (!this.Direction.IsNullOrEmpty() && this.Direction != ".")
					{
						cell = cell.GetLocalCellFromDirection(this.Direction, true);
					}
					this._Instance = ((cell != null) ? cell.GetFirstObjectPart(this.PartType) : null);
				}
				return this._Instance;
			}
		}

		// Token: 0x0600A251 RID: 41553 RVA: 0x003B5389 File Offset: 0x003B3589
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == InventoryActionEvent.ID || ID == GetInventoryActionsEvent.ID;
		}

		// Token: 0x0600A252 RID: 41554 RVA: 0x003B53BF File Offset: 0x003B35BF
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			IPart instance = this.Instance;
			if (instance == null)
			{
				return base.HandleEvent(E);
			}
			return instance.HandleEvent(E);
		}

		// Token: 0x0600A253 RID: 41555 RVA: 0x003B53D9 File Offset: 0x003B35D9
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			IPart instance = this.Instance;
			if (instance == null)
			{
				return base.HandleEvent(E);
			}
			return instance.HandleEvent(E);
		}

		// Token: 0x0600A254 RID: 41556 RVA: 0x003B53F3 File Offset: 0x003B35F3
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			IPart instance = this.Instance;
			if (instance == null)
			{
				return base.HandleEvent(E);
			}
			return instance.HandleEvent(E);
		}

		// Token: 0x0600A255 RID: 41557 RVA: 0x003B540D File Offset: 0x003B360D
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			IPart instance = this.Instance;
			if (instance == null)
			{
				return base.HandleEvent(E);
			}
			return instance.HandleEvent(E);
		}

		// Token: 0x0600A256 RID: 41558 RVA: 0x003B5427 File Offset: 0x003B3627
		public override bool HandleEvent(InventoryActionEvent E)
		{
			IPart instance = this.Instance;
			if (instance == null)
			{
				return base.HandleEvent(E);
			}
			return instance.HandleEvent(E);
		}

		// Token: 0x04004347 RID: 17223
		public string Direction = "N";

		// Token: 0x04004348 RID: 17224
		public string Part;

		// Token: 0x04004349 RID: 17225
		[NonSerialized]
		private Type _PartType;

		// Token: 0x0400434A RID: 17226
		[NonSerialized]
		private IPart _Instance;
	}
}
