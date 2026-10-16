using System;
using System.Collections.Generic;

namespace XRL.World.AI
{
	// Token: 0x02000B6E RID: 2926
	public sealed class OpinionMap : Dictionary<int, OpinionList>, IComposite
	{
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600754C RID: 30028 RVA: 0x002BF936 File Offset: 0x002BDB36
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000975 RID: 2421
		public OpinionList this[GameObject Object]
		{
			get
			{
				if (!Object.HasID)
				{
					return null;
				}
				return base[Object.BaseID];
			}
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x002BF954 File Offset: 0x002BDB54
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(base.Count);
			foreach (KeyValuePair<int, OpinionList> keyValuePair in this)
			{
				Writer.WriteOptimized(keyValuePair.Key);
				keyValuePair.Value.Write(Writer);
			}
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x002BF9C4 File Offset: 0x002BDBC4
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadOptimizedInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadOptimizedInt32();
				OpinionList opinionList = new OpinionList();
				opinionList.Read(Reader);
				base.Add(num2, opinionList);
			}
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x002BFA00 File Offset: 0x002BDC00
		public void ClearExpired()
		{
			XRLGame game = The.Game;
			if (game == null)
			{
				return;
			}
			List<int> list = null;
			long timeTicks = game.TimeTicks;
			foreach (KeyValuePair<int, OpinionList> keyValuePair in this)
			{
				int num;
				OpinionList opinionList;
				keyValuePair.Deconstruct(out num, out opinionList);
				int num2 = num;
				OpinionList opinionList2 = opinionList;
				for (int i = opinionList2.Count - 1; i >= 0; i--)
				{
					IOpinion opinion = opinionList2[i];
					int duration = opinion.Duration;
					if (duration > 0 && timeTicks - opinion.Time >= (long)duration)
					{
						opinionList2.RemoveAt(i);
					}
				}
				if (opinionList2.Count == 0)
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(num2);
				}
			}
			if (list != null)
			{
				foreach (int num3 in list)
				{
					base.Remove(num3);
				}
			}
		}
	}
}
