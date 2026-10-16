using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141D RID: 5149
	[Serializable]
	public class Invisibility : BaseMutation
	{
		// Token: 0x0600C84F RID: 51279 RVA: 0x0048E973 File Offset: 0x0048CB73
		public Invisibility()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C850 RID: 51280 RVA: 0x0048E986 File Offset: 0x0048CB86
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C851 RID: 51281 RVA: 0x0048E989 File Offset: 0x0048CB89
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C852 RID: 51282 RVA: 0x0048E99F File Offset: 0x0048CB9F
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("glass", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C853 RID: 51283 RVA: 0x0048E9C2 File Offset: 0x0048CBC2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C854 RID: 51284 RVA: 0x0048E9C5 File Offset: 0x0048CBC5
		public override string GetDescription()
		{
			return "You cannot be seen.";
		}

		// Token: 0x0600C855 RID: 51285 RVA: 0x0048E9CC File Offset: 0x0048CBCC
		public override string GetLevelText(int Level)
		{
			return this.GetDescription();
		}

		// Token: 0x0600C856 RID: 51286 RVA: 0x0048E9D4 File Offset: 0x0048CBD4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CustomRender");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C857 RID: 51287 RVA: 0x0048E9EC File Offset: 0x0048CBEC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CustomRender")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					LightLevel light = currentCell.GetLight();
					if (light == LightLevel.Darkvision || light == LightLevel.LitRadar || light == LightLevel.Radar || light == LightLevel.Dimvision || light == LightLevel.Interpolight || light == LightLevel.Omniscient)
					{
						this.ParentObject.Render.Visible = true;
					}
					else
					{
						this.ParentObject.Render.Visible = false;
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
