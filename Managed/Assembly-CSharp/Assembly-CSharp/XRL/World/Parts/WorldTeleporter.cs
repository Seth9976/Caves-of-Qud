using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001299 RID: 4761
	[Serializable]
	public class WorldTeleporter : IPart
	{
		// Token: 0x0600BC40 RID: 48192 RVA: 0x0044D275 File Offset: 0x0044B475
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC41 RID: 48193 RVA: 0x0044D278 File Offset: 0x0044B478
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BC42 RID: 48194 RVA: 0x0044D27B File Offset: 0x0044B47B
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckAgainstPlayer();
			base.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0600BC43 RID: 48195 RVA: 0x0044D28B File Offset: 0x0044B48B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectEnteredCell");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC44 RID: 48196 RVA: 0x0044D2AC File Offset: 0x0044B4AC
		private void CheckAgainstPlayer()
		{
			if (The.Player.Stat("Level", 0) > this.MaxLevel && this.ParentObject.CurrentCell != null)
			{
				this.ParentObject.CurrentCell.AddObject("Shale", null, null, null, null);
				this.ParentObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x0600BC45 RID: 48197 RVA: 0x0044D308 File Offset: 0x0044B508
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				this.CheckAgainstPlayer();
			}
			else if (E.ID == "ObjectEnteredCell")
			{
				if (this.ParentObject.IsNowhere())
				{
					return true;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter == this.ParentObject)
				{
					return true;
				}
				if (gameObjectParameter == null)
				{
					return true;
				}
				if (gameObjectParameter.Stat("Level", 0) > this.MaxLevel)
				{
					return true;
				}
				Render render = gameObjectParameter.Render;
				if (render == null || render.RenderLayer == 0)
				{
					return true;
				}
				if (this.TargetZone[0] == '$')
				{
					this.TargetZone = The.Game.GetStringGameState(this.TargetZone, "");
				}
				gameObjectParameter.ApplyEffect(new Lost(9999, this.TargetZone, null, 10, true), null);
				Zone zone = The.ZoneManager.GetZone(this.TargetZone);
				Cell cell = null;
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						Cell cell2 = zone.GetCell(j, i);
						using (List<GameObject>.Enumerator enumerator = cell2.GetObjectsInCell().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.Blueprint == this.TargetObject)
								{
									foreach (Cell cell3 in cell2.GetAdjacentCells(true))
									{
										if (cell3.IsEmpty())
										{
											cell = cell3;
											goto IL_01A6;
										}
									}
								}
							}
						}
					}
				}
				IL_01A6:
				if (cell == null)
				{
					return true;
				}
				The.ZoneManager.SetActiveZone(this.TargetZone);
				gameObjectParameter.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear");
				The.ZoneManager.ProcessGoToPartyLeader();
				if (gameObjectParameter.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You are sucked through the surface of the sphere!", 'C', true);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B70 RID: 19312
		public string TargetZone = "";

		// Token: 0x04004B71 RID: 19313
		public string TargetObject = "";

		// Token: 0x04004B72 RID: 19314
		public int MaxLevel = -1;
	}
}
