using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;

namespace HistoryKit
{
	// Token: 0x0200036B RID: 875
	[Serializable]
	public class HistoricEntityList : IEnumerable<HistoricEntity>, IEnumerable
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0009FA5F File Offset: 0x0009DC5F
		public int Count
		{
			get
			{
				return this.entities.Count;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0009FA6C File Offset: 0x0009DC6C
		public IEnumerator<HistoricEntity> GetEnumerator()
		{
			return this.entities.GetEnumerator();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0009FA7E File Offset: 0x0009DC7E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.entities.GetEnumerator();
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0009FA90 File Offset: 0x0009DC90
		public HistoricEntity GetRandomElement(Random R = null)
		{
			if (this.entities.Count == 0)
			{
				return null;
			}
			if (R == null)
			{
				R = Stat.Rand;
			}
			return this.entities[R.Next(0, this.entities.Count)];
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0009FAC8 File Offset: 0x0009DCC8
		public virtual HistoricEntity GetEntity(string ID)
		{
			for (int i = 0; i < this.entities.Count; i++)
			{
				if (this.entities[i].id == ID)
				{
					return this.entities[i];
				}
			}
			return null;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0009FB14 File Offset: 0x0009DD14
		public virtual bool TryGetEntity(string ID, out HistoricEntity Entity)
		{
			for (int i = 0; i < this.entities.Count; i++)
			{
				if (this.entities[i].id == ID)
				{
					Entity = this.entities[i];
					return true;
				}
			}
			Entity = null;
			return false;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0009FB64 File Offset: 0x0009DD64
		public HistoricEntity GetEntityWithProperty(string Key)
		{
			foreach (HistoricEntity historicEntity in this.entities)
			{
				if (historicEntity.HasEntityProperty(Key, -1L))
				{
					return historicEntity;
				}
			}
			return null;
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0009FBC4 File Offset: 0x0009DDC4
		public HistoricEntityList ForEach(HistoricEntityList.entityForeachDelegate matchingDelegate)
		{
			HistoricEntityList historicEntityList = new HistoricEntityList();
			for (int i = 0; i < this.entities.Count; i++)
			{
				matchingDelegate(this.entities[i]);
				historicEntityList.entities.Add(this.entities[i]);
			}
			return historicEntityList;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0009FC18 File Offset: 0x0009DE18
		public HistoricEntityList GetEntitiesByDelegate(HistoricEntityList.entityMatchingDelegate matchingDelegate)
		{
			HistoricEntityList historicEntityList = new HistoricEntityList();
			for (int i = 0; i < this.entities.Count; i++)
			{
				if (matchingDelegate(this.entities[i]))
				{
					historicEntityList.entities.Add(this.entities[i]);
				}
			}
			return historicEntityList;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0009FC70 File Offset: 0x0009DE70
		public HistoricEntityList GetEntitiesWithProperty(string property)
		{
			return this.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).properties.ContainsKey(property));
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0009FC9C File Offset: 0x0009DE9C
		public HistoricEntityList GetEntitiesWherePropertyEquals(string property, string value)
		{
			return this.GetEntitiesByDelegate(delegate(HistoricEntity entity)
			{
				HistoricEntitySnapshot snapshotAtYear = entity.GetSnapshotAtYear(entity.lastYear);
				return snapshotAtYear.properties.ContainsKey(property) && snapshotAtYear.properties[property] == value;
			});
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0009FCD0 File Offset: 0x0009DED0
		public HistoricEntityList GetEntitiesWithListProperty(string property)
		{
			return this.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).listProperties.ContainsKey(property));
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0009FCFC File Offset: 0x0009DEFC
		public HistoricEntityList GetEntitiesWithListPropertyThatContains(string property, string value)
		{
			return this.GetEntitiesByDelegate(delegate(HistoricEntity entity)
			{
				HistoricEntitySnapshot snapshotAtYear = entity.GetSnapshotAtYear(entity.lastYear);
				return snapshotAtYear.listProperties.ContainsKey(property) && snapshotAtYear.listProperties[property].Contains(value);
			});
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0009FD30 File Offset: 0x0009DF30
		public string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.entities.Count; i++)
			{
				HistoricEntitySnapshot snapshotAtYear = this.entities[i].GetSnapshotAtYear(this.entities[i].lastYear);
				stringBuilder.AppendLine("*** " + snapshotAtYear.properties["name"] + " ***");
				for (int j = 0; j < this.entities[i].events.Count; j++)
				{
					if (this.entities[i].events[j].HasEventProperty("gospel"))
					{
						stringBuilder.AppendLine("  @" + this.entities[i].events[j].year.ToString() + "  " + this.entities[i].events[j].GetEventProperty("gospel", null));
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001084 RID: 4228
		public List<HistoricEntity> entities = new List<HistoricEntity>();

		// Token: 0x0200176B RID: 5995
		// (Invoke) Token: 0x0600DD60 RID: 56672
		public delegate bool entityMatchingDelegate(HistoricEntity entity);

		// Token: 0x0200176C RID: 5996
		// (Invoke) Token: 0x0600DD64 RID: 56676
		public delegate void entityForeachDelegate(HistoricEntity entity);
	}
}
