using System;
using System.Collections.Generic;
using XRL.World.Anatomy;
using XRL.World.ObjectBuilders;

namespace XRL.World.Parts
{
	// Token: 0x020011AB RID: 4523
	[Serializable]
	public class MechaPlayer : IPart
	{
		// Token: 0x0600B3B9 RID: 46009 RVA: 0x004190A6 File Offset: 0x004172A6
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B3BA RID: 46010 RVA: 0x004190A9 File Offset: 0x004172A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B3BB RID: 46011 RVA: 0x004190C0 File Offset: 0x004172C0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			GameObject thePlayer = IComponent<GameObject>.ThePlayer;
			if (thePlayer == null)
			{
				return true;
			}
			this.ParentObject.Render.DisplayName = thePlayer.Render.DisplayName;
			this.ParentObject.Render.Tile = thePlayer.Render.Tile;
			this.ParentObject.Render.RenderString = thePlayer.Render.RenderString;
			this.ParentObject.Render.DetailColor = thePlayer.Render.DetailColor;
			Body body = this.ParentObject.Body;
			body.ForeachPart((BodyPart p) => p.FireEvent(Event.New("CommandForceUnequipObject", "BodyPart", p, "IsSilent", 1)));
			try
			{
				this.ParentObject.DeepCopyInventoryObjectMap = new Dictionary<GameObject, GameObject>();
				Body body2 = thePlayer.Body.DeepCopy(this.ParentObject, null) as Body;
				this.ParentObject.DeepCopyInventoryObjectMap = null;
				this.ParentObject.RemovePart(body);
				this.ParentObject.AddPart<Body>(body2, true, false);
				body = body2;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error copying player body, setting anatomy instead", ex);
				if (thePlayer.Body.Anatomy != body.Anatomy)
				{
					body.Anatomy = thePlayer.Body.Anatomy;
				}
			}
			body.HandleEvent(StripContentsEvent.FromPool(this.ParentObject, this.KeepNatural, true));
			body.MarkAllNative();
			Description part = thePlayer.GetPart<Description>();
			if (this.InheritDescription && part != null)
			{
				this.ParentObject.RequirePart<Description>(false)._Short = part._Short;
			}
			Roboticized.Roboticize(this.ParentObject, this.NamePrefix, this.DescriptionPostfix);
			this.ParentObject.Brain.DoReequip = true;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3BC RID: 46012 RVA: 0x0041928C File Offset: 0x0041748C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B3BD RID: 46013 RVA: 0x0041928F File Offset: 0x0041748F
		public override bool Render(RenderEvent E)
		{
			E.ApplyColors("&c", MechaPlayer.ICON_COLOR_PRIORITY);
			return base.Render(E);
		}

		// Token: 0x04004880 RID: 18560
		public static readonly int ICON_COLOR_PRIORITY = 90;

		// Token: 0x04004881 RID: 18561
		public string NamePrefix = "{{c|mechanical}}";

		// Token: 0x04004882 RID: 18562
		public string DescriptionPostfix = "There is a low, persistent hum emanating outward.";

		// Token: 0x04004883 RID: 18563
		public bool KeepNatural = true;

		// Token: 0x04004884 RID: 18564
		public bool InheritDescription = true;
	}
}
