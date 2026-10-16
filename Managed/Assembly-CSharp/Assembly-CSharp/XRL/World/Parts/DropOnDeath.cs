using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EA4 RID: 3748
	[Serializable]
	public class DropOnDeath : IPart
	{
		// Token: 0x0600968D RID: 38541 RVA: 0x00368DF1 File Offset: 0x00366FF1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x0600968E RID: 38542 RVA: 0x00368E08 File Offset: 0x00367008
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.Blueprints.IsNullOrEmpty())
			{
				DroppedEvent droppedEvent = null;
				DropOnDeathEvent dropOnDeathEvent = null;
				foreach (ReadOnlySpan<char> readOnlySpan in this.Blueprints.DelimitedBy(','))
				{
					ReadOnlySpan<char> readOnlySpan2;
					ReadOnlySpan<char> readOnlySpan3;
					readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
					if (readOnlySpan2.Length != 0)
					{
						int num = 1;
						if (readOnlySpan3.Length > 0)
						{
							int num2;
							if (int.TryParse(readOnlySpan3, out num2))
							{
								num = num2;
							}
							else
							{
								num = new string(readOnlySpan3).RollCached();
							}
						}
						if (num > 0)
						{
							IInventory dropInventory = this.ParentObject.GetDropInventory();
							string text = new string(readOnlySpan2);
							for (int i = 0; i < num; i++)
							{
								GameObject gameObject = GameObject.Create(text);
								if (dropOnDeathEvent == null)
								{
									dropOnDeathEvent = DropOnDeathEvent.FromPool();
								}
								dropOnDeathEvent.Reset();
								dropOnDeathEvent.Actor = this.ParentObject;
								dropOnDeathEvent.Inventory = dropInventory;
								dropOnDeathEvent.Object = gameObject;
								dropOnDeathEvent.Type = "DropOnDeath";
								IObjectInventoryInteractionEvent objectInventoryInteractionEvent = dropOnDeathEvent;
								Cell cell = dropInventory as Cell;
								objectInventoryInteractionEvent.Direction = ((cell != null) ? this.ParentObject.CurrentCell.GetDirectionFromCell(cell, false) : null);
								if (gameObject.HandleEvent<DropOnDeathEvent>(dropOnDeathEvent, E))
								{
									dropInventory.AddObjectToInventory(gameObject, this.ParentObject, false, false, true, null, E);
									if (droppedEvent == null)
									{
										droppedEvent = DroppedEvent.FromPool();
									}
									droppedEvent.Reset();
									droppedEvent.Actor = this.ParentObject;
									droppedEvent.Item = gameObject;
									gameObject.HandleEvent<DroppedEvent>(droppedEvent, E);
								}
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003E62 RID: 15970
		public string Blueprints;
	}
}
