using System;
using ConsoleLib.Console;
using UnityEngine;

namespace XRL.World.Effects
{
	// Token: 0x02000D4F RID: 3407
	public abstract class IEffectWithPrefabImposter : Effect
	{
		// Token: 0x060086A5 RID: 34469 RVA: 0x00306739 File Offset: 0x00304939
		public IEffectWithPrefabImposter()
		{
		}

		// Token: 0x060086A6 RID: 34470 RVA: 0x0030674F File Offset: 0x0030494F
		public void SetActive(bool State)
		{
			this.ImposterActive = State;
		}

		// Token: 0x060086A7 RID: 34471 RVA: 0x00306758 File Offset: 0x00304958
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x060086A8 RID: 34472 RVA: 0x0030676E File Offset: 0x0030496E
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "VisibleOnly", this.VisibleOnly);
			E.AddEntry(this, "PrefabID", this.PrefabID);
			return base.HandleEvent(E);
		}

		// Token: 0x060086A9 RID: 34473 RVA: 0x0030679B File Offset: 0x0030499B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Object.SetIntProperty("HasImposter", 1, false);
			base.Register(Object, Registrar);
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x003067B4 File Offset: 0x003049B4
		public override bool FinalRender(RenderEvent E)
		{
			if (this.ImposterActive && !this.VisibleOnly)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.PrefabID, new Vector2((float)this.X, (float)this.Y), 0, null));
			}
			return base.FinalRender(E);
		}

		// Token: 0x060086AB RID: 34475 RVA: 0x00306804 File Offset: 0x00304A04
		public override bool Render(RenderEvent E)
		{
			if (this.ImposterActive && this.VisibleOnly && E.Visible && E.Lit > LightLevel.None)
			{
				E.Imposters.Add(new ImposterExtra.ImposterInfo(this.PrefabID, new Vector2((float)this.X, (float)this.Y), 0, null));
			}
			return true;
		}

		// Token: 0x040039EC RID: 14828
		public string PrefabID;

		// Token: 0x040039ED RID: 14829
		public bool ImposterActive = true;

		// Token: 0x040039EE RID: 14830
		public bool VisibleOnly = true;

		// Token: 0x040039EF RID: 14831
		public int X;

		// Token: 0x040039F0 RID: 14832
		public int Y;
	}
}
