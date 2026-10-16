using System;
using System.Collections.Generic;
using Genkit;
using UnityEngine;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE5 RID: 3045
	public abstract class ConversationEvent
	{
		// Token: 0x06007A5D RID: 31325 RVA: 0x002DF684 File Offset: 0x002DD884
		public static int AllocateID()
		{
			return 0;
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x002DF687 File Offset: 0x002DD887
		public static int RegisterEvent<T>(string Seed = null, ConversationEvent.EventPoolCount Count = null, ConversationEvent.EventPoolReset Reset = null)
		{
			return ConversationEvent.RegisterEvent(typeof(T), Seed, Count, Reset);
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x002DF69C File Offset: 0x002DD89C
		public static int RegisterEvent(Type Event, string Seed = null, ConversationEvent.EventPoolCount Count = null, ConversationEvent.EventPoolReset Reset = null)
		{
			string name = Event.Name;
			int num = (int)Hash.FNV1A32(Seed ?? name, 2166136261U);
			if (num == 0)
			{
				throw new ArgumentException("ID hash is zero.");
			}
			if (!ConversationEvent.EventTypes.TryAdd(num, Event))
			{
				Type type = ConversationEvent.EventTypes[num];
				if (!(type == Event))
				{
					throw new ArgumentException(string.Concat(new string[] { "ID hash conflict between conversation events ", name, " and ", type.Name, "." }));
				}
				Debug.LogError("Duplicate conversation event registration for " + name + ".");
			}
			if (Count != null && !ConversationEvent.EventPoolCounts.TryAdd(name, Count))
			{
				Debug.LogError("Duplicate pool retrieval registration for " + name + ".");
			}
			if (Reset != null)
			{
				ConversationEvent.EventPoolResets.Add(Reset);
			}
			return num;
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x002DF774 File Offset: 0x002DD974
		public ConversationEvent()
		{
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x002DF77C File Offset: 0x002DD97C
		public ConversationEvent(int ID)
		{
			this.ID = ID;
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x002DF78B File Offset: 0x002DD98B
		public virtual bool HandlePartDispatch(IConversationPart Part)
		{
			Debug.LogError("Base HandlePartDispatch called for " + base.GetType().Name);
			return true;
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x002DF7A8 File Offset: 0x002DD9A8
		public virtual void Reset()
		{
			this.Element = null;
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x002DF7B1 File Offset: 0x002DD9B1
		public static void RegisterPoolReset(ConversationEvent.EventPoolReset R)
		{
			ConversationEvent.EventPoolResets.Add(R);
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x002DF7BE File Offset: 0x002DD9BE
		public static void RegisterPoolCount(string ClassName, ConversationEvent.EventPoolCount C)
		{
			if (ConversationEvent.EventPoolCounts.ContainsKey(ClassName))
			{
				Debug.LogError("duplicate pool retrieval registration for " + ClassName);
				return;
			}
			ConversationEvent.EventPoolCounts.Add(ClassName, C);
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x002DF7EC File Offset: 0x002DD9EC
		public static void ResetPools()
		{
			foreach (ConversationEvent.EventPoolReset eventPoolReset in ConversationEvent.EventPoolResets)
			{
				eventPoolReset();
			}
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x002DF83C File Offset: 0x002DDA3C
		protected static void ResetTo<T>(T Event, List<T> Pool, ref int Counter) where T : ConversationEvent
		{
			if (Pool == null)
			{
				return;
			}
			while (Counter > 0)
			{
				int num = Counter - 1;
				Counter = num;
				T t = Pool[num];
				t.Reset();
				if (Event == t)
				{
					break;
				}
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x002DF87C File Offset: 0x002DDA7C
		protected static T FromPool<T>(ref List<T> Pool, ref int Counter, int MaxPool = 8192) where T : ConversationEvent, new()
		{
			if (Pool == null)
			{
				Pool = new List<T>();
			}
			if (Counter >= Pool.Count)
			{
				if (Pool.Count >= MaxPool)
				{
					return new T();
				}
				int num = Counter - Pool.Count + 1;
				for (int i = 0; i < num; i++)
				{
					Pool.Add(new T());
				}
			}
			List<T> list = Pool;
			int num2 = Counter;
			Counter = num2 + 1;
			return list[num2];
		}

		// Token: 0x040037CF RID: 14287
		public readonly int ID;

		// Token: 0x040037D0 RID: 14288
		[ConversationEvent.ParameterAttribute(Required = true)]
		public IConversationElement Element;

		// Token: 0x040037D1 RID: 14289
		public static Dictionary<int, Type> EventTypes = new Dictionary<int, Type>();

		// Token: 0x040037D2 RID: 14290
		private static List<ConversationEvent.EventPoolReset> EventPoolResets = new List<ConversationEvent.EventPoolReset>();

		// Token: 0x040037D3 RID: 14291
		private static Dictionary<string, ConversationEvent.EventPoolCount> EventPoolCounts = new Dictionary<string, ConversationEvent.EventPoolCount>();

		// Token: 0x02001C0D RID: 7181
		[AttributeUsage(AttributeTargets.Field)]
		public class ParameterAttribute : Attribute, IComparable<ConversationEvent.ParameterAttribute>
		{
			// Token: 0x0600EFF6 RID: 61430 RVA: 0x005492DC File Offset: 0x005474DC
			public int CompareTo(ConversationEvent.ParameterAttribute Other)
			{
				if (Other != null)
				{
					if (this.Output)
					{
						if (Other.Output)
						{
							return 0;
						}
						if (Other.Reference)
						{
							return 1;
						}
						if (Other.Required)
						{
							return 1;
						}
					}
					else if (this.Reference)
					{
						if (Other.Reference)
						{
							return 0;
						}
						if (Other.Required)
						{
							return 1;
						}
						if (Other.Output)
						{
							return -1;
						}
					}
					else if (this.Required)
					{
						if (!Other.Required)
						{
							return -1;
						}
						if (Other.Reference)
						{
							return -1;
						}
						if (Other.Output)
						{
							return -1;
						}
					}
					return 0;
				}
				if (this.Required || this.Reference || this.Output)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x04006F10 RID: 28432
			public object Default;

			// Token: 0x04006F11 RID: 28433
			public bool Reference;

			// Token: 0x04006F12 RID: 28434
			public bool Output;

			// Token: 0x04006F13 RID: 28435
			public bool Required;

			// Token: 0x04006F14 RID: 28436
			public bool Exclude;

			// Token: 0x04006F15 RID: 28437
			public Type Input;

			// Token: 0x04006F16 RID: 28438
			public bool Get;
		}

		// Token: 0x02001C0E RID: 7182
		public enum Action
		{
			// Token: 0x04006F18 RID: 28440
			Custom,
			// Token: 0x04006F19 RID: 28441
			Send,
			// Token: 0x04006F1A RID: 28442
			Get,
			// Token: 0x04006F1B RID: 28443
			Check
		}

		// Token: 0x02001C0F RID: 7183
		public enum Instantiation
		{
			// Token: 0x04006F1D RID: 28445
			Custom,
			// Token: 0x04006F1E RID: 28446
			Pooling,
			// Token: 0x04006F1F RID: 28447
			Stack,
			// Token: 0x04006F20 RID: 28448
			Singleton
		}

		// Token: 0x02001C10 RID: 7184
		// (Invoke) Token: 0x0600EFF9 RID: 61433
		public delegate void EventPoolReset();

		// Token: 0x02001C11 RID: 7185
		// (Invoke) Token: 0x0600EFFD RID: 61437
		public delegate int EventPoolCount();
	}
}
