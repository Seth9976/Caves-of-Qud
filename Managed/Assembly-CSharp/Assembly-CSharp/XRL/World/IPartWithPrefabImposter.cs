using System;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.World
{
	// Token: 0x02000965 RID: 2405
	[Obsolete]
	public class IPartWithPrefabImposter : IPart
	{
		// Token: 0x06006353 RID: 25427 RVA: 0x001E82D0 File Offset: 0x001E64D0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == LeftCellEvent.ID || ID == OnDestroyObjectEvent.ID || ID == ZoneActivatedEvent.ID || ID == ZoneDeactivatedEvent.ID;
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x001E830E File Offset: 0x001E650E
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "VisibleOnly", this.VisibleOnly);
			E.AddEntry(this, "prefabID", this.prefabID);
			return base.HandleEvent(E);
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x001E833B File Offset: 0x001E653B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.SetIntProperty("HasImposter", 1, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x001E8354 File Offset: 0x001E6554
		public override bool FinalRender(RenderEvent E)
		{
			if (this.ImposterActive && !this.VisibleOnly)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.prefabID, new Vector2((float)this.X, (float)this.Y), 0, null));
			}
			return base.FinalRender(E);
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x001E83A4 File Offset: 0x001E65A4
		public override bool Render(RenderEvent E)
		{
			if (this.ImposterActive && this.VisibleOnly && E.Visible && E.Lit > LightLevel.None)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.prefabID, new Vector2((float)this.X, (float)this.Y), 0, null));
			}
			return true;
		}

		// Token: 0x04002EBA RID: 11962
		public string prefabID;

		// Token: 0x04002EBB RID: 11963
		public bool ImposterActive = true;

		// Token: 0x04002EBC RID: 11964
		public bool VisibleOnly = true;

		// Token: 0x04002EBD RID: 11965
		public int X;

		// Token: 0x04002EBE RID: 11966
		public int Y;
	}
}
