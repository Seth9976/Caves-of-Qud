using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020010A3 RID: 4259
	[Serializable]
	public class SultanLoot : IPart
	{
		// Token: 0x0600A98D RID: 43405 RVA: 0x003DFC5A File Offset: 0x003DDE5A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600A98E RID: 43406 RVA: 0x003DFC70 File Offset: 0x003DDE70
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.generated)
			{
				this.generated = true;
				this.ParentObject.Inventory.AddObject(this.generateFace(), null, false, false, true, null);
				this.generateRelics().ForEach(delegate(GameObject r)
				{
					this.ParentObject.Inventory.AddObject(r, null, false, false, true, null);
				});
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A98F RID: 43407 RVA: 0x003DFCC6 File Offset: 0x003DDEC6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A990 RID: 43408 RVA: 0x003DFCCC File Offset: 0x003DDECC
		public GameObject generateFace()
		{
			GameObject gameObject = null;
			if (this.Period == 1)
			{
				gameObject = GameObject.Create("The Kesil Face");
			}
			if (this.Period == 2)
			{
				gameObject = GameObject.Create("The Shemesh Face");
			}
			if (this.Period == 3)
			{
				gameObject = GameObject.Create("The Earth Face");
			}
			if (this.Period == 4)
			{
				gameObject = GameObject.Create("The Levant Face");
			}
			if (this.Period == 5)
			{
				gameObject = GameObject.Create("The Olive Face");
			}
			if (this.Period == 6)
			{
				gameObject = GameObject.Create("The Nil Face");
			}
			return gameObject;
		}

		// Token: 0x0600A991 RID: 43409 RVA: 0x003DFD54 File Offset: 0x003DDF54
		public List<GameObject> generateRelics()
		{
			List<GameObject> list = new List<GameObject>();
			using (List<string>.Enumerator enumerator = HistoryAPI.GetSultanForPeriod(this.Period).GetList("items").GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string id = enumerator.Current;
					HistoricEntity historicEntity = XRLCore.Core.Game.sultanHistory.entities.Where<HistoricEntity>((HistoricEntity e) => e.GetCurrentSnapshot().Name == id).FirstOrDefault<HistoricEntity>();
					if (historicEntity != null)
					{
						list.Add(RelicGenerator.GenerateRelic(historicEntity.GetCurrentSnapshot(), null, false));
					}
				}
			}
			return list;
		}

		// Token: 0x040045ED RID: 17901
		public int Period = 1;

		// Token: 0x040045EE RID: 17902
		public bool generated;
	}
}
