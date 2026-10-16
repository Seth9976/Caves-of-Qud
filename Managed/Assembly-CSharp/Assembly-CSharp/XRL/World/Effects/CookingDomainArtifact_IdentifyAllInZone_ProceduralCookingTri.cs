using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C56 RID: 3158
	[Serializable]
	public class CookingDomainArtifact_IdentifyAllInZone_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060080B4 RID: 32948 RVA: 0x002F4FA9 File Offset: 0x002F31A9
		public override string GetDescription()
		{
			return "@they identify all artifacts on the local map.";
		}

		// Token: 0x060080B5 RID: 32949 RVA: 0x002F4FB0 File Offset: 0x002F31B0
		public override string GetNotification()
		{
			return "";
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x002F4FB8 File Offset: 0x002F31B8
		public override void Apply(GameObject go)
		{
			Zone currentZone = go.CurrentZone;
			if (currentZone == null)
			{
				return;
			}
			Action<GameObject> <>9__2;
			Action<GameObject> <>9__1;
			currentZone.ForeachCell(delegate(Cell c)
			{
				Action<GameObject> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(GameObject o)
					{
						if (!o.Understood())
						{
							o.MakeUnderstood(false);
							if (go.IsPlayer())
							{
								IComponent<GameObject>.XDidYToZ(go, "flush", "with understanding of", o, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
							}
						}
						Action<GameObject> action2;
						if ((action2 = <>9__2) == null)
						{
							action2 = (<>9__2 = delegate(GameObject e)
							{
								if (!e.Understood())
								{
									e.MakeUnderstood(false);
									if (go.IsPlayer())
									{
										IComponent<GameObject>.XDidYToZ(go, "flush", "with understanding of", e, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
									}
								}
							});
						}
						o.ForeachInventoryEquipmentDefaultBehaviorAndCybernetics(action2);
					});
				}
				c.ForeachObject(action);
			});
		}
	}
}
