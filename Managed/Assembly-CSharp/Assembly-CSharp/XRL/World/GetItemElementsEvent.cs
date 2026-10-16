using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x0200075A RID: 1882
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetItemElementsEvent : PooledEvent<GetItemElementsEvent>
	{
		// Token: 0x06004DAE RID: 19886 RVA: 0x0017D8CA File Offset: 0x0017BACA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x0017D8D3 File Offset: 0x0017BAD3
		public override void Reset()
		{
			base.Reset();
			this.Weights.Clear();
			this.Bag.Clear();
			this.CascadeLevel = 0;
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x0017D8F8 File Offset: 0x0017BAF8
		public override int GetCascadeLevel()
		{
			return this.CascadeLevel;
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0017D900 File Offset: 0x0017BB00
		public void Add(string Element, int Weight)
		{
			int num;
			if (!this.Weights.TryGetValue(Element, out num))
			{
				if (Weight != 0)
				{
					this.Weights.Add(Element, Weight);
				}
				return;
			}
			int num2 = num + Weight;
			if (num2 != 0)
			{
				this.Weights[Element] = num2;
				return;
			}
			this.Weights.Remove(Element);
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0017D950 File Offset: 0x0017BB50
		public bool IsRelevantCreature(GameObject Subject)
		{
			return GameObject.Validate(ref Subject);
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0017D959 File Offset: 0x0017BB59
		public bool IsRelevantObject(GameObject Subject)
		{
			return GameObject.Validate(ref Subject);
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0017D962 File Offset: 0x0017BB62
		public bool HandleFor(GameObject Item)
		{
			return !GameObject.Validate(ref Item) || Item.HandleEvent(this);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0017D978 File Offset: 0x0017BB78
		public BallBag<string> InitializeBag(Random Random = null)
		{
			BallBag<string> bag = this.Bag;
			bag.Clear();
			bag.Random = Random ?? Stat.Rnd;
			foreach (KeyValuePair<string, int> keyValuePair in this.Weights)
			{
				bag.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return bag;
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x0017D9F8 File Offset: 0x0017BBF8
		public static GetItemElementsEvent GetMythicFor(GameObject Item, int Range = 5, Random Random = null)
		{
			GetItemElementsEvent getItemElementsEvent = PooledEvent<GetItemElementsEvent>.FromPool();
			getItemElementsEvent.CascadeLevel = 17;
			getItemElementsEvent.HandleFor(Item);
			int num = 1;
			foreach (string text in Statistic.Attributes)
			{
				int statValue = Item.GetStatValue(text, 0);
				if (statValue > num)
				{
					num = statValue;
				}
			}
			if (Item.GetStatValue("Strength", 0) == num)
			{
				getItemElementsEvent.Add("might", 5);
			}
			if (Item.GetStatValue("Intelligence", 0) == num)
			{
				getItemElementsEvent.Add("scholarship", 5);
			}
			if (Item.GetStatValue("Ego", 0) == num)
			{
				getItemElementsEvent.Add("jewels", 5);
			}
			int num2 = 0;
			BallBag<string> ballBag = getItemElementsEvent.InitializeBag(Random);
			for (int i = ballBag.Count - 1; i >= 0; i--)
			{
				int weight = ballBag.GetWeight(i);
				if (weight > num2)
				{
					num2 = weight;
				}
			}
			for (int j = ballBag.Count - 1; j >= 0; j--)
			{
				if (ballBag.GetWeight(j) < num2 - Range)
				{
					ballBag.RemoveAt(j);
				}
			}
			return getItemElementsEvent;
		}

		// Token: 0x040025B2 RID: 9650
		public Dictionary<string, int> Weights = new Dictionary<string, int>();

		// Token: 0x040025B3 RID: 9651
		public BallBag<string> Bag = new BallBag<string>(0);

		// Token: 0x040025B4 RID: 9652
		public new int CascadeLevel;
	}
}
