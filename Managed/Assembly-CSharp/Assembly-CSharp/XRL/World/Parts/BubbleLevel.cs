using System;

namespace XRL.World.Parts
{
	// Token: 0x02001002 RID: 4098
	[Serializable]
	public class BubbleLevel : IPart
	{
		// Token: 0x0600A358 RID: 41816 RVA: 0x003BB286 File Offset: 0x003B9486
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A359 RID: 41817 RVA: 0x003BB2A4 File Offset: 0x003B94A4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Flip", "flip", BubbleLevel.COMMAND_NAME, null, 'f', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A35A RID: 41818 RVA: 0x003BB2DC File Offset: 0x003B94DC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == BubbleLevel.COMMAND_NAME)
			{
				this.FlipBubbleLevel(E.Actor, !E.Auto);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A35B RID: 41819 RVA: 0x003BB30D File Offset: 0x003B950D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A35C RID: 41820 RVA: 0x003BB310 File Offset: 0x003B9510
		public bool FlipBubbleLevel(GameObject Actor, bool FromDialog)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null && liquidVolume.Volume > 0)
			{
				base.PlayWorldSound("Sounds/Interact/sfx_interact_bubbleLevel_flip", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				base.DidX("plop", null, null, null, null, null, null, false, false, null, null, false, false, false, FromDialog, Actor.IsPlayer(), null);
			}
			else
			{
				base.DidX("make", "no sound", null, null, null, null, null, false, false, null, null, false, false, false, FromDialog, Actor.IsPlayer(), null);
			}
			return true;
		}

		// Token: 0x040043B2 RID: 17330
		public static readonly string COMMAND_NAME = "BubbleLevel";
	}
}
