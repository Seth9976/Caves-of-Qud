using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Genkit;
using UnityEngine;

namespace XRL.World
{
	// Token: 0x02000938 RID: 2360
	public abstract class MinEvent : IEvent
	{
		// Token: 0x06005B23 RID: 23331 RVA: 0x001B8E30 File Offset: 0x001B7030
		public int GetID()
		{
			return this.ID;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x001B8E38 File Offset: 0x001B7038
		public static void ResetEvents()
		{
			if (MinEvent.EventTypes.IsNullOrEmpty<KeyValuePair<int, Type>>())
			{
				return;
			}
			MinEvent.EventTypes.Clear();
			MinEvent.EventPoolResets.Clear();
			MinEvent.EventPoolCounts.Clear();
			foreach (Type type in ModManager.GetTypesAssignableFrom(typeof(MinEvent), false))
			{
				if (!type.IsAbstract)
				{
					Type type2 = type;
					do
					{
						try
						{
							int count = MinEvent.EventTypes.Count;
							RuntimeHelpers.RunClassConstructor(type2.TypeHandle);
							if (MinEvent.EventTypes.Count == count)
							{
								ConstructorInfo typeInitializer = type2.TypeInitializer;
								if (typeInitializer != null)
								{
									typeInitializer.Invoke(null, null);
								}
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("MinEventReset:" + type.FullName, ex, "game_exception");
						}
						type2 = type2.BaseType;
					}
					while (type2 != null && type2 != typeof(MinEvent));
				}
			}
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x001B8F48 File Offset: 0x001B7148
		public static void InitializeEvents()
		{
			if (MinEvent.Initialized)
			{
				return;
			}
			MinEvent.Initialized = true;
			foreach (Type type in ModManager.GetTypesAssignableFrom(typeof(MinEvent), false))
			{
				if (!type.IsAbstract)
				{
					Type type2 = type;
					do
					{
						try
						{
							RuntimeHelpers.RunClassConstructor(type2.TypeHandle);
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("MinEventInit:" + type.FullName, ex, "game_exception");
						}
						type2 = type2.BaseType;
					}
					while (type2 != null && type2 != typeof(MinEvent));
				}
			}
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x001B9008 File Offset: 0x001B7208
		public static Type ResolveEvent(int ID)
		{
			MinEvent.InitializeEvents();
			return MinEvent.EventTypes.GetValue(ID, null);
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x001B901B File Offset: 0x001B721B
		public static int RegisterEvent<T>(string Seed = null, MinEvent.EventPoolCount Count = null, MinEvent.EventPoolReset Reset = null)
		{
			return MinEvent.RegisterEvent(typeof(T), Seed, Count, Reset);
		}

		// Token: 0x06005B28 RID: 23336 RVA: 0x001B9030 File Offset: 0x001B7230
		public static int RegisterEvent(Type Event, string Seed = null, MinEvent.EventPoolCount Count = null, MinEvent.EventPoolReset Reset = null)
		{
			string fullName = Event.FullName;
			int num = (int)Hash.FNV1A32(Seed ?? fullName, 2166136261U);
			if (num == 0)
			{
				throw new ArgumentException("ID hash is zero.");
			}
			if (!MinEvent.EventTypes.TryAdd(num, Event))
			{
				Type type = MinEvent.EventTypes[num];
				if (!(type == Event))
				{
					throw new ArgumentException(string.Concat(new string[] { "ID hash conflict between min events ", fullName, " and ", type.Name, "." }));
				}
				Debug.LogError("Duplicate min event registration for " + fullName + ".");
			}
			if (Count != null && !MinEvent.EventPoolCounts.TryAdd(fullName, Count))
			{
				Debug.LogError("Duplicate pool retrieval registration for " + fullName + ".");
			}
			if (Reset != null)
			{
				MinEvent.EventPoolResets.Add(Reset);
			}
			return num;
		}

		// Token: 0x06005B29 RID: 23337 RVA: 0x001B9108 File Offset: 0x001B7308
		public static void ResetPools()
		{
			if (MinEvent.EventPoolResets != null)
			{
				for (int i = 0; i < MinEvent.EventPoolResets.Count; i++)
				{
					MinEvent.EventPoolResets[i]();
				}
			}
		}

		// Token: 0x06005B2A RID: 23338 RVA: 0x001B9144 File Offset: 0x001B7344
		protected static void ResetTo<T>(T Event, List<T> Pool, ref int Counter) where T : MinEvent
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

		// Token: 0x06005B2B RID: 23339 RVA: 0x001B9184 File Offset: 0x001B7384
		protected static T FromPool<T>(ref List<T> Pool, ref int Counter, int MaxPool = 8192) where T : MinEvent, new()
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

		// Token: 0x06005B2C RID: 23340 RVA: 0x001B91EC File Offset: 0x001B73EC
		protected static T FromPool<T>(List<T> Pool, ref int Counter, int MaxPool = 8192) where T : MinEvent, new()
		{
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
			int num2 = Counter;
			Counter = num2 + 1;
			return Pool[num2];
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x001B9244 File Offset: 0x001B7444
		public virtual void RequestInterfaceExit()
		{
			this.InterfaceExit = true;
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x001B924D File Offset: 0x001B744D
		public bool InterfaceExitRequested()
		{
			return this.InterfaceExit;
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x001B9255 File Offset: 0x001B7455
		public virtual void PreprocessChildEvent(IEvent E)
		{
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x001B9257 File Offset: 0x001B7457
		public virtual void ProcessChildEvent(IEvent E)
		{
			if (E.InterfaceExitRequested())
			{
				this.InterfaceExit = true;
			}
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x001B9268 File Offset: 0x001B7468
		public virtual void Reset()
		{
			this.InterfaceExit = false;
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x001B9271 File Offset: 0x001B7471
		[Obsolete("Use ModSingletonEvent/ModPooledEvent and IModEventHandler.")]
		public virtual bool WantInvokeDispatch()
		{
			return false;
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x001B9274 File Offset: 0x001B7474
		public virtual int GetCascadeLevel()
		{
			return MinEvent.CascadeLevel;
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x001B927B File Offset: 0x001B747B
		public static bool CascadeTo(int Cascade, int Level)
		{
			return (Cascade & Level) != 0;
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x001B9283 File Offset: 0x001B7483
		public bool CascadeTo(int Level)
		{
			return MinEvent.CascadeTo(this.GetCascadeLevel(), Level);
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x001B9294 File Offset: 0x001B7494
		public static string GetTopPoolCountReport(int num = 20)
		{
			List<string> list = new List<string>(MinEvent.EventPoolCounts.Count);
			Dictionary<string, int> Counts = new Dictionary<string, int>(MinEvent.EventPoolCounts.Count);
			foreach (KeyValuePair<string, MinEvent.EventPoolCount> keyValuePair in MinEvent.EventPoolCounts)
			{
				list.Add(keyValuePair.Key);
				Counts.Add(keyValuePair.Key, keyValuePair.Value());
			}
			list.Sort(delegate(string a, string b)
			{
				int num3 = Counts[b].CompareTo(Counts[a]);
				if (num3 != 0)
				{
					return num3;
				}
				return a.CompareTo(b);
			});
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int i = 0;
			int num2 = Math.Min(list.Count, num + 1);
			while (i < num2)
			{
				stringBuilder.Append(list[i]).Append(": ").Append(Counts[list[i]])
					.Append('\n');
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x001B93B0 File Offset: 0x001B75B0
		public virtual bool Dispatch(IEventHandler Handler)
		{
			Debug.LogError("base HandlerDispatch called for " + base.GetType().Name);
			return true;
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x001B93CD File Offset: 0x001B75CD
		public bool ActuateOn(GameObject obj)
		{
			return !obj.WantEvent(this.GetID(), this.GetCascadeLevel()) || obj.HandleEvent(this);
		}

		// Token: 0x04002CF7 RID: 11511
		public const int CASCADE_NONE = 0;

		/// <summary>Allow cascade to equipped objects.</summary>
		// Token: 0x04002CF8 RID: 11512
		public const int CASCADE_EQUIPMENT = 1;

		/// <summary>Allow cascade to inventory objects.</summary>
		// Token: 0x04002CF9 RID: 11513
		public const int CASCADE_INVENTORY = 2;

		/// <summary>Allow cascade to slotted game objects, e.g. socketed energy cells.</summary>
		// Token: 0x04002CFA RID: 11514
		public const int CASCADE_SLOTS = 4;

		/// <summary>Allow cascade to component game objects, e.g. mine explosives.</summary>
		// Token: 0x04002CFB RID: 11515
		public const int CASCADE_COMPONENTS = 8;

		/// <summary>Prevent cascade to equipped thrown weapon.</summary>
		// Token: 0x04002CFC RID: 11516
		public const int CASCADE_EXCEPT_THROWN_WEAPON = 16;

		/// <summary>Prevent cascade to zone cells and their contents.</summary>
		// Token: 0x04002CFD RID: 11517
		public const int CASCADE_STOP_AT_ZONE = 32;

		/// <summary>Prevent cascade beyond registry.</summary>
		// Token: 0x04002CFE RID: 11518
		public const int CASCADE_STOP_AT_REGISTRY = 64;

		/// <summary>Allow cascade on world map.</summary>
		// Token: 0x04002CFF RID: 11519
		public const int CASCADE_WORLD_MAP_INVENTORY = 128;

		/// <summary>Event is presumed desired by game objects and should skip to dispatch.</summary>
		// Token: 0x04002D00 RID: 11520
		public const int CASCADE_DESIRED_OBJECT = 256;

		/// <summary>Event is presumed desired by game systems and should skip to dispatch.</summary>
		// Token: 0x04002D01 RID: 11521
		public const int CASCADE_DESIRED_SYSTEM = 512;

		// Token: 0x04002D02 RID: 11522
		public const int CASCADE_ALL = 15;

		// Token: 0x04002D03 RID: 11523
		public static readonly int CascadeLevel = 0;

		// Token: 0x04002D04 RID: 11524
		public int ID;

		// Token: 0x04002D05 RID: 11525
		public bool InterfaceExit;

		// Token: 0x04002D06 RID: 11526
		public static Dictionary<int, Type> EventTypes = new Dictionary<int, Type>();

		// Token: 0x04002D07 RID: 11527
		public static bool SuppressThreadWarning = false;

		// Token: 0x04002D08 RID: 11528
		private static List<MinEvent.EventPoolReset> EventPoolResets = new List<MinEvent.EventPoolReset>();

		// Token: 0x04002D09 RID: 11529
		private static Dictionary<string, MinEvent.EventPoolCount> EventPoolCounts = new Dictionary<string, MinEvent.EventPoolCount>();

		// Token: 0x04002D0A RID: 11530
		private static bool Initialized;

		// Token: 0x04002D0B RID: 11531
		public static bool UIHold = false;

		// Token: 0x02001A34 RID: 6708
		public enum Cache
		{
			// Token: 0x04006862 RID: 26722
			None,
			// Token: 0x04006863 RID: 26723
			Pool,
			// Token: 0x04006864 RID: 26724
			Singleton
		}

		// Token: 0x02001A35 RID: 6709
		// (Invoke) Token: 0x0600E688 RID: 59016
		public delegate void EventPoolReset();

		// Token: 0x02001A36 RID: 6710
		// (Invoke) Token: 0x0600E68C RID: 59020
		public delegate int EventPoolCount();
	}
}
