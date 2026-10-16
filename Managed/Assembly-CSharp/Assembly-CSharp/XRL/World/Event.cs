using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using XRL.Core;
using XRL.UI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x02000932 RID: 2354
	[Serializable]
	public class Event : IEvent
	{
		// Token: 0x06005A70 RID: 23152 RVA: 0x001B6EA2 File Offset: 0x001B50A2
		public static void PinCurrentPool()
		{
			Event.pinLocation = Event.nPoolCounter;
			Event.pinStringLocation = Event.nStringBuilderPoolCounter;
			Event.pinGameObjectListLocation = Event.nGameObjectListPoolCounter;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x001B6EC4 File Offset: 0x001B50C4
		public static void ResetToPin()
		{
			for (int i = Event.pinLocation; i < Event.nPoolCounter; i++)
			{
				Event.EventPool[i].Clear();
			}
			Event.nPoolCounter = Event.pinLocation;
			Event.nStringBuilderPoolCounter = Event.pinStringLocation;
			Event.nGameObjectListPoolCounter = Event.pinGameObjectListLocation;
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x001B6F10 File Offset: 0x001B5110
		public static void ResetPool(bool resetMinEventPools = true)
		{
			if (Event.EventPool == null)
			{
				Event.EventPool = new Event[4000];
				for (int i = 0; i < 4000; i++)
				{
					Event.EventPool[i] = new Event(null, 3, 3, 3);
				}
			}
			if (Event.nPoolCounter > Event.PeakEventPool)
			{
				Event.PeakEventPool = Event.nPoolCounter;
			}
			for (int j = 0; j < Event.nPoolCounter; j++)
			{
				Event.EventPool[j].Clear();
			}
			Event.nPoolCounter = 0;
			Event.ResetStringbuilderPool();
			Event.ResetGameObjectListPool();
			Event.ResetCellListPool();
			if (resetMinEventPools)
			{
				MinEvent.ResetPools();
			}
			Event.bWarnedThisClear = false;
			if (Event.nExceedCounter > 0)
			{
				XRLCore.Log("Exceeded by " + Event.nExceedCounter.ToString());
				Event.nExceedCounter = 0;
			}
			if (XRLCore.Core != null && XRLCore.Core.Game != null)
			{
				XRLCore.Core.Game.lastFind = null;
				XRLCore.Core.Game.lastFindId = null;
			}
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x001B7004 File Offset: 0x001B5204
		public static StringBuilder NewStringBuilder(StringBuilder Default)
		{
			if (!XRLCore.IsCoreThread)
			{
				return new StringBuilder().Append(Default);
			}
			if (Event.StringBuilderPool == null)
			{
				Event.StringBuilderPool = new List<StringBuilder>(1000);
				for (int i = 0; i < 1000; i++)
				{
					Event.StringBuilderPool.Add(new StringBuilder(2048));
				}
			}
			while (Event.StringBuilderPool.Count <= Event.nStringBuilderPoolCounter)
			{
				Event.StringBuilderPool.Add(new StringBuilder(2048));
			}
			StringBuilder stringBuilder = Event.StringBuilderPool[Event.nStringBuilderPoolCounter];
			Event.nStringBuilderPoolCounter++;
			stringBuilder.Length = 0;
			if (Default != null)
			{
				stringBuilder.Append(Default);
			}
			return stringBuilder;
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x001B70B4 File Offset: 0x001B52B4
		public static StringBuilder NewStringBuilder(string Default = null)
		{
			if (!XRLCore.IsCoreThread)
			{
				return new StringBuilder(Default);
			}
			if (Event.StringBuilderPool == null)
			{
				Event.StringBuilderPool = new List<StringBuilder>(1000);
				for (int i = 0; i < 1000; i++)
				{
					Event.StringBuilderPool.Add(new StringBuilder(2048));
				}
			}
			while (Event.StringBuilderPool.Count <= Event.nStringBuilderPoolCounter)
			{
				Event.StringBuilderPool.Add(new StringBuilder(2048));
			}
			StringBuilder stringBuilder = Event.StringBuilderPool[Event.nStringBuilderPoolCounter];
			Event.nStringBuilderPoolCounter++;
			stringBuilder.Length = 0;
			if (Default != null)
			{
				stringBuilder.Append(Default);
			}
			return stringBuilder;
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x001B7160 File Offset: 0x001B5360
		public static void ResetStringbuilderPool()
		{
			if (Event.StringBuilderPool == null)
			{
				Event.StringBuilderPool = new List<StringBuilder>(1000);
				for (int i = 0; i < 1000; i++)
				{
					Event.StringBuilderPool.Add(new StringBuilder(2048));
				}
			}
			if (Event.nStringBuilderPoolCounter > Event.PeakStringPool)
			{
				Event.PeakStringPool = Event.nStringBuilderPoolCounter;
			}
			Event.nStringBuilderPoolCounter = 0;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x001B71C4 File Offset: 0x001B53C4
		public static void ResetTo(StringBuilder SB)
		{
			if (Event.StringBuilderPool == null || !XRLCore.IsCoreThread)
			{
				return;
			}
			while (Event.nStringBuilderPoolCounter > 0)
			{
				StringBuilder stringBuilder = Event.StringBuilderPool[--Event.nStringBuilderPoolCounter].Clear();
				if (SB == stringBuilder)
				{
					break;
				}
			}
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x001B7209 File Offset: 0x001B5409
		public static string FinalizeString(StringBuilder SB)
		{
			string text = SB.ToString();
			Event.ResetTo(SB);
			return text;
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x001B7218 File Offset: 0x001B5418
		public static List<GameObject> NewGameObjectList()
		{
			while (Event.GameObjectListPool.Count <= Event.nGameObjectListPoolCounter)
			{
				Event.GameObjectListPool.Add(new List<GameObject>(16));
			}
			List<GameObject> list = Event.GameObjectListPool[Event.nGameObjectListPoolCounter];
			Event.nGameObjectListPoolCounter++;
			if (list.Count > 0)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x001B7275 File Offset: 0x001B5475
		public static List<GameObject> NewGameObjectList(List<GameObject> List)
		{
			List<GameObject> list = Event.NewGameObjectList();
			list.AddRange(List);
			return list;
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x001B7284 File Offset: 0x001B5484
		public static List<GameObject> NewGameObjectList(List<GameObject> List, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return Event.NewGameObjectList(List);
			}
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in List)
			{
				if (Filter(gameObject))
				{
					list.Add(gameObject);
				}
			}
			return list;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x001B72EC File Offset: 0x001B54EC
		public static List<GameObject> NewGameObjectList(IEnumerable<GameObject> List)
		{
			List<GameObject> list = Event.NewGameObjectList();
			list.AddRange(List);
			return list;
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x001B72FC File Offset: 0x001B54FC
		public static void ResetGameObjectListPool()
		{
			for (int i = 0; i < Event.nGameObjectListPoolCounter; i++)
			{
				if (Event.GameObjectListPool[i].Count > 0)
				{
					Event.GameObjectListPool[i].Clear();
				}
			}
			Event.nGameObjectListPoolCounter = 0;
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x001B7344 File Offset: 0x001B5544
		public static List<Cell> NewCellList()
		{
			while (Event.CellListPool.Count <= Event.nCellListPoolCounter)
			{
				Event.CellListPool.Add(new List<Cell>(16));
			}
			List<Cell> list = Event.CellListPool[Event.nCellListPoolCounter];
			Event.nCellListPoolCounter++;
			if (list.Count > 0)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x001B73A1 File Offset: 0x001B55A1
		public static List<Cell> NewCellList(Cell Cell)
		{
			List<Cell> list = Event.NewCellList();
			list.Add(Cell);
			return list;
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x001B73AF File Offset: 0x001B55AF
		public static List<Cell> NewCellList(IEnumerable<Cell> Cells)
		{
			List<Cell> list = Event.NewCellList();
			list.AddRange(Cells);
			return list;
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x001B73C0 File Offset: 0x001B55C0
		public static void ResetCellListPool()
		{
			for (int i = 0; i < Event.nCellListPoolCounter; i++)
			{
				if (Event.CellListPool[i].Count > 0)
				{
					Event.CellListPool[i].Clear();
				}
			}
			Event.nCellListPoolCounter = 0;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x001B7408 File Offset: 0x001B5608
		public static void ShowTopEvents()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (KeyValuePair<string, int> keyValuePair in Event.TopEvents.OrderByDescending<KeyValuePair<string, int>, int>(delegate(KeyValuePair<string, int> entry)
			{
				KeyValuePair<string, int> keyValuePair2 = entry;
				return keyValuePair2.Value;
			}).Take<KeyValuePair<string, int>>(20).ToDictionary<KeyValuePair<string, int>, string, int>((KeyValuePair<string, int> pair) => pair.Key, (KeyValuePair<string, int> pair) => pair.Value))
			{
				stringBuilder.Append(keyValuePair.Key).Append(": ").Append(keyValuePair.Value)
					.Append("\n");
			}
			stringBuilder.Append("(total tracked: ").Append(Event.TopEvents.Count).Append(")");
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x001B7534 File Offset: 0x001B5734
		public static Event New(string ID, int ObjParams = 0, int StrParams = 0, int IntParams = 0)
		{
			if (Event.EventPool == null)
			{
				Event.ResetPool(true);
			}
			if (Event.TopEvents.ContainsKey(ID))
			{
				Dictionary<string, int> topEvents = Event.TopEvents;
				int num = topEvents[ID];
				topEvents[ID] = num + 1;
			}
			else
			{
				Event.TopEvents.Add(ID, 0);
			}
			if (Event.nPoolCounter >= 4000)
			{
				if (!Event.bWarnedThisClear)
				{
					Event.bWarnedThisClear = true;
					XRLCore.Log("exceeded event pool size " + 4000.ToString() + "\n" + new StackTrace(true).ToString());
				}
				Event.nExceedCounter++;
				return new Event(ID, ObjParams, StrParams, IntParams);
			}
			Event @event = Event.EventPool[Event.nPoolCounter];
			@event.ID = ID;
			if (@event.Parameters.Count > 0)
			{
				@event.Parameters.Clear();
			}
			Event.nPoolCounter++;
			return @event;
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x001B7615 File Offset: 0x001B5815
		public static Event New(string ID, string Name1, string Value1)
		{
			Event @event = Event.New(ID, 0, 1, 0);
			@event.SetParameterInternal(Name1, Value1);
			return @event;
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x001B7629 File Offset: 0x001B5829
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2)
		{
			Event @event = Event.New(ID, 0, 2, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x001B7647 File Offset: 0x001B5847
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 0, 3, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x001B7670 File Offset: 0x001B5870
		public static Event New(string ID, string Name1, int Value1)
		{
			Event @event = Event.New(ID, 0, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			return @event;
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x001B7684 File Offset: 0x001B5884
		public static Event New(string ID, string Name1, int Value1, string Name2, int Value2)
		{
			Event @event = Event.New(ID, 0, 0, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x001B76A2 File Offset: 0x001B58A2
		public static Event New(string ID, string Name1, int Value1, string Name2, int Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 0, 0, 3);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x001B76CB File Offset: 0x001B58CB
		public static Event New(string ID, string Name1, int Value1, string Name2, int Value2, string Name3, int Value3, string Name4, int Value4)
		{
			Event @event = Event.New(ID, 0, 0, 4);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x001B76FF File Offset: 0x001B58FF
		public static Event New(string ID, string Name1, object Value1)
		{
			Event @event = Event.New(ID, 1, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			return @event;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x001B7713 File Offset: 0x001B5913
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2)
		{
			Event @event = Event.New(ID, 2, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x001B7731 File Offset: 0x001B5931
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 3, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x001B775A File Offset: 0x001B595A
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, object Value3, string Name4, object Value4)
		{
			Event @event = Event.New(ID, 4, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x001B778E File Offset: 0x001B598E
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, object Value3, string Name4, int Value4)
		{
			Event @event = Event.New(ID, 4, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x001B77C2 File Offset: 0x001B59C2
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, object Value3, string Name4, object Value4, string Name5, object Value5)
		{
			Event @event = Event.New(ID, 5, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			@event.SetParameterInternal(Name5, Value5);
			return @event;
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x001B7801 File Offset: 0x001B5A01
		public static Event New(string ID, string Name1, string Value1, string Name2, int Value2)
		{
			Event @event = Event.New(ID, 0, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x001B781F File Offset: 0x001B5A1F
		public static Event New(string ID, string Name1, int Value1, string Name2, string Value2)
		{
			Event @event = Event.New(ID, 0, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x001B783D File Offset: 0x001B5A3D
		public static Event New(string ID, string Name1, string Value1, string Name2, object Value2)
		{
			Event @event = Event.New(ID, 1, 1, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x001B785B File Offset: 0x001B5A5B
		public static Event New(string ID, string Name1, object Value1, string Name2, string Value2)
		{
			Event @event = Event.New(ID, 1, 1, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001B7879 File Offset: 0x001B5A79
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2)
		{
			Event @event = Event.New(ID, 1, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001B7897 File Offset: 0x001B5A97
		public static Event New(string ID, string Name1, object Value1, string Name2, int Value2)
		{
			Event @event = Event.New(ID, 1, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			return @event;
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x001B78B5 File Offset: 0x001B5AB5
		public static Event New(string ID, string Name1, string Value1, string Name2, int Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 0, 1, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x001B78DE File Offset: 0x001B5ADE
		public static Event New(string ID, string Name1, string Value1, string Name2, int Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 0, 2, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x001B7907 File Offset: 0x001B5B07
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 0, 2, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x001B7930 File Offset: 0x001B5B30
		public static Event New(string ID, string Name1, int Value1, string Name2, string Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 0, 2, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x001B7959 File Offset: 0x001B5B59
		public static Event New(string ID, string Name1, int Value1, string Name2, int Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 0, 1, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x001B7982 File Offset: 0x001B5B82
		public static Event New(string ID, string Name1, int Value1, string Name2, string Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 0, 1, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x001B79AB File Offset: 0x001B5BAB
		public static Event New(string ID, string Name1, string Value1, string Name2, object Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 2, 1, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x001B79D4 File Offset: 0x001B5BD4
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 1, 2, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x001B79FD File Offset: 0x001B5BFD
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2, string Name3, object Value3, string Name4, int Value4)
		{
			Event @event = Event.New(ID, 1, 2, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x001B7A31 File Offset: 0x001B5C31
		public static Event New(string ID, string Name1, string Value1, string Name2, object Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 1, 2, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001B7A5A File Offset: 0x001B5C5A
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 2, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x001B7A83 File Offset: 0x001B5C83
		public static Event New(string ID, string Name1, int Value1, string Name2, int Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 1, 0, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001B7AAC File Offset: 0x001B5CAC
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 1, 0, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x001B7AD5 File Offset: 0x001B5CD5
		public static Event New(string ID, string Name1, object Value1, string Name2, int Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 1, 0, 2);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x001B7AFE File Offset: 0x001B5CFE
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 2, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001B7B27 File Offset: 0x001B5D27
		public static Event New(string ID, string Name1, object Value1, string Name2, int Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 2, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x001B7B50 File Offset: 0x001B5D50
		public static Event New(string ID, string Name1, string Value1, string Name2, int Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x001B7B79 File Offset: 0x001B5D79
		public static Event New(string ID, string Name1, int Value1, string Name2, string Value2, string Name3, object Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x001B7BA2 File Offset: 0x001B5DA2
		public static Event New(string ID, string Name1, object Value1, string Name2, int Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x001B7BCB File Offset: 0x001B5DCB
		public static Event New(string ID, string Name1, object Value1, string Name2, string Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x001B7BF4 File Offset: 0x001B5DF4
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2, string Name3, string Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x001B7C1D File Offset: 0x001B5E1D
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2, string Name3, string Value3, string Name4, object Value4)
		{
			Event @event = Event.New(ID, 2, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x001B7C51 File Offset: 0x001B5E51
		public static Event New(string ID, string Name1, int Value1, string Name2, object Value2, string Name3, object Value3, string Name4, string Value4)
		{
			Event @event = Event.New(ID, 2, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x001B7C85 File Offset: 0x001B5E85
		public static Event New(string ID, string Name1, string Value1, string Name2, object Value2, string Name3, int Value3)
		{
			Event @event = Event.New(ID, 1, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			return @event;
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x001B7CAE File Offset: 0x001B5EAE
		public static Event New(string ID, string Name1, string Value1, string Name2, string Value2, string Name3, string Value3, string Name4, string Value4)
		{
			Event @event = Event.New(ID, 0, 4, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x001B7CE2 File Offset: 0x001B5EE2
		public static Event New(string ID, string Name1, object Value1, string Name2, object Value2, string Name3, int Value3, string Name4, string Value4)
		{
			Event @event = Event.New(ID, 2, 1, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.SetParameterInternal(Name2, Value2);
			@event.SetParameterInternal(Name3, Value3);
			@event.SetParameterInternal(Name4, Value4);
			return @event;
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x001B7D16 File Offset: 0x001B5F16
		public static Event NewSilent(string ID, string Name1, string Value1)
		{
			Event @event = Event.New(ID, 0, 1, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.bSilent = true;
			return @event;
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x001B7D31 File Offset: 0x001B5F31
		public static Event NewSilent(string ID, string Name1, int Value1)
		{
			Event @event = Event.New(ID, 0, 0, 1);
			@event.SetParameterInternal(Name1, Value1);
			@event.bSilent = true;
			return @event;
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x001B7D4C File Offset: 0x001B5F4C
		public static Event NewSilent(string ID, string Name1, object Value1)
		{
			Event @event = Event.New(ID, 1, 0, 0);
			@event.SetParameterInternal(Name1, Value1);
			@event.bSilent = true;
			return @event;
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x001B7D68 File Offset: 0x001B5F68
		public Event(string _ID)
		{
			if (this.Parameters == null)
			{
				this.Parameters = new Dictionary<string, object>();
			}
			if (this.StringParameters == null)
			{
				this.StringParameters = new Dictionary<string, string>();
			}
			if (this.IntParameters == null)
			{
				this.IntParameters = new Dictionary<string, int>();
			}
			this.ID = _ID;
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x001B7DC8 File Offset: 0x001B5FC8
		public Event(string _ID, int ObjParams, int StrParams, int IntParams)
		{
			if (this.Parameters == null)
			{
				this.Parameters = new Dictionary<string, object>(ObjParams);
			}
			if (this.StringParameters == null)
			{
				this.StringParameters = new Dictionary<string, string>(StrParams);
			}
			if (this.IntParameters == null)
			{
				this.IntParameters = new Dictionary<string, int>(IntParams);
			}
			this.ID = _ID;
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x001B7E2A File Offset: 0x001B602A
		public Event(string _ID, string ParameterName, string Parameter)
			: this(_ID, 0, 1, 0)
		{
			this.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x001B7E3F File Offset: 0x001B603F
		public Event(string _ID, string ParameterName, int Parameter)
			: this(_ID, 0, 0, 1)
		{
			this.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x001B7E54 File Offset: 0x001B6054
		public Event(string _ID, string ParameterName, object Parameter)
			: this(_ID, 1, 0, 0)
		{
			this.SetParameterInternal(ParameterName, Parameter);
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x001B7E69 File Offset: 0x001B6069
		public Event(string _ID, string ParameterName1, string Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 1, 1, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x001B7E89 File Offset: 0x001B6089
		public Event(string _ID, string ParameterName1, string Parameter1, string ParameterName2, int Parameter2)
			: this(_ID, 1, 0, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x001B7EA9 File Offset: 0x001B60A9
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 1, 1, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x001B7EC9 File Offset: 0x001B60C9
		public Event(string _ID, string ParameterName1, string Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 0, 2, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x001B7EE9 File Offset: 0x001B60E9
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, string Parameter2)
			: this(_ID, 0, 1, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x001B7F09 File Offset: 0x001B6109
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 1, 0, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x001B7F29 File Offset: 0x001B6129
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, int Parameter2)
			: this(_ID, 1, 0, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x001B7F49 File Offset: 0x001B6149
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, int Parameter2)
			: this(_ID, 0, 0, 2)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x001B7F69 File Offset: 0x001B6169
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, int Parameter2, string ParameterName3, int Parameter3)
			: this(_ID, 0, 0, 3)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x001B7F94 File Offset: 0x001B6194
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, int Parameter2, string ParameterName3, int Parameter3, string ParameterName4, int Parameter4)
			: this(_ID, 0, 0, 4)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
			this.SetParameterInternal(ParameterName4, Parameter4);
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x001B7FCA File Offset: 0x001B61CA
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, string Parameter2, string ParameterName3, object Parameter3)
			: this(_ID, 1, 1, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x001B7FF5 File Offset: 0x001B61F5
		public Event(string _ID, string ParameterName1, int Parameter1, string ParameterName2, object Parameter2, string ParameterName3, object Parameter3)
			: this(_ID, 2, 0, 1)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x001B8020 File Offset: 0x001B6220
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, object Parameter2)
			: this(_ID, 2, 0, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x001B8040 File Offset: 0x001B6240
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, object Parameter2, string ParameterName3, string Parameter3)
			: this(_ID, 2, 1, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x001B806B File Offset: 0x001B626B
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, string Parameter2, string ParameterName3, object Parameter3)
			: this(_ID, 2, 1, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x001B8096 File Offset: 0x001B6296
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, int Parameter2, string ParameterName3, int Parameter3)
			: this(_ID, 1, 0, 2)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x001B80C1 File Offset: 0x001B62C1
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, object Parameter2, string ParameterName3, object Parameter3)
			: this(_ID, 3, 0, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x001B80EC File Offset: 0x001B62EC
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, object Parameter2, string ParameterName3, object Parameter3, string ParameterName4, object Parameter4)
			: this(_ID, 4, 0, 0)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
			this.SetParameterInternal(ParameterName4, Parameter4);
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x001B8122 File Offset: 0x001B6322
		public Event(string _ID, string ParameterName1, object Parameter1, string ParameterName2, string Parameter2, string ParameterName3, int Parameter3, string ParameterName4, int Parameter4)
			: this(_ID, 1, 1, 2)
		{
			this.SetParameterInternal(ParameterName1, Parameter1);
			this.SetParameterInternal(ParameterName2, Parameter2);
			this.SetParameterInternal(ParameterName3, Parameter3);
			this.SetParameterInternal(ParameterName4, Parameter4);
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x001B8158 File Offset: 0x001B6358
		public virtual bool HasFlag(string Name)
		{
			int num;
			return this.IntParameters.TryGetValue(Name, out num) && num > 0;
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x001B817C File Offset: 0x001B637C
		public virtual Event SetFlag(string Name, bool State)
		{
			int num2;
			if (State)
			{
				int num;
				if (this.IntParameters.TryGetValue(Name, out num))
				{
					if (num <= 0)
					{
						this.IntParameters[Name] = 1;
					}
				}
				else
				{
					this.IntParameters.Add(Name, 1);
				}
			}
			else if (this.IntParameters.TryGetValue(Name, out num2) && num2 > 0)
			{
				this.IntParameters.Remove(Name);
			}
			return this;
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x001B81E0 File Offset: 0x001B63E0
		public virtual Event SetSilent(bool Silent)
		{
			return this.SetFlag("IsSilent", Silent);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x001B81EE File Offset: 0x001B63EE
		public bool IsSilent()
		{
			return this.HasFlag("IsSilent");
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x001B81FB File Offset: 0x001B63FB
		// (set) Token: 0x06005AD0 RID: 23248 RVA: 0x001B8203 File Offset: 0x001B6403
		public bool bSilent
		{
			get
			{
				return this.IsSilent();
			}
			set
			{
				this.SetSilent(value);
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06005AD1 RID: 23249 RVA: 0x001B820D File Offset: 0x001B640D
		// (set) Token: 0x06005AD2 RID: 23250 RVA: 0x001B821F File Offset: 0x001B641F
		private bool ExitInterfaceSignal
		{
			get
			{
				return this.IntParameters.ContainsKey("__ExitInterfaceSignal");
			}
			set
			{
				if (value)
				{
					this.IntParameters["__ExitInterfaceSignal"] = 1;
					return;
				}
				this.IntParameters.Remove("__ExitInterfaceSignal");
			}
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x001B8247 File Offset: 0x001B6447
		public virtual void Clear()
		{
			this.Parameters.Clear();
			this.StringParameters.Clear();
			this.IntParameters.Clear();
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x001B826A File Offset: 0x001B646A
		public bool HasParameter(string Name)
		{
			return this.Parameters.ContainsKey(Name) || this.StringParameters.ContainsKey(Name) || this.IntParameters.ContainsKey(Name);
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x001B829D File Offset: 0x001B649D
		public bool HasObjectParameter(string Name)
		{
			return this.Parameters.ContainsKey(Name);
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x001B82AB File Offset: 0x001B64AB
		public bool HasStringParameter(string Name)
		{
			return this.StringParameters.ContainsKey(Name);
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x001B82B9 File Offset: 0x001B64B9
		public bool HasIntParameter(string Name)
		{
			return this.IntParameters.ContainsKey(Name);
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x001B82C8 File Offset: 0x001B64C8
		public int GetIntParameter(string Name, int Default = 0)
		{
			int num;
			if (this.IntParameters.TryGetValue(Name, out num))
			{
				return num;
			}
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj))
			{
				return Convert.ToInt32(obj);
			}
			string text;
			if (this.StringParameters.TryGetValue(Name, out text))
			{
				return Convert.ToInt32(text);
			}
			return Default;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x001B8316 File Offset: 0x001B6516
		public bool TryGetStringParameter(string Key, out string Value)
		{
			return this.StringParameters.TryGetValue(Key, out Value);
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x001B8328 File Offset: 0x001B6528
		public string GetStringParameter(string Name, string Default = null)
		{
			string text;
			if (this.StringParameters.TryGetValue(Name, out text))
			{
				return text;
			}
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj))
			{
				return (string)obj;
			}
			int num;
			if (this.IntParameters.TryGetValue(Name, out num))
			{
				return num.ToString();
			}
			return Default;
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x001B8378 File Offset: 0x001B6578
		public float GetFloatParameter(string Name, float Default = 0f)
		{
			int num;
			if (this.IntParameters.TryGetValue(Name, out num))
			{
				return BitConverter.Int32BitsToSingle(num);
			}
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj))
			{
				return Convert.ToSingle(obj);
			}
			string text;
			if (this.StringParameters.TryGetValue(Name, out text))
			{
				return Convert.ToSingle(text);
			}
			return Default;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x001B83CC File Offset: 0x001B65CC
		public GameObject GetGameObjectParameter(string Name)
		{
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj))
			{
				GameObject gameObject = obj as GameObject;
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x001B83F8 File Offset: 0x001B65F8
		public object GetParameter(string Name)
		{
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj))
			{
				return obj;
			}
			string text;
			if (this.StringParameters.TryGetValue(Name, out text))
			{
				return text;
			}
			int num;
			if (this.IntParameters.TryGetValue(Name, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x001B8444 File Offset: 0x001B6644
		public T GetParameter<T>()
		{
			string name = typeof(T).Name;
			object obj;
			if (this.Parameters.TryGetValue(name, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			string text;
			if (this.StringParameters.TryGetValue(name, out text) && text is T)
			{
				return text as T;
			}
			int num;
			if (this.IntParameters.TryGetValue(name, out num) && num is T)
			{
				return num as T;
			}
			return default(T);
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x001B84E8 File Offset: 0x001B66E8
		public T GetParameter<T>(string Name)
		{
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			string text;
			if (this.StringParameters.TryGetValue(Name, out text) && text is T)
			{
				return text as T;
			}
			int num;
			if (this.IntParameters.TryGetValue(Name, out num) && num is T)
			{
				return num as T;
			}
			return default(T);
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x001B857C File Offset: 0x001B677C
		public T GetParameter<T>(string Name, T Default)
		{
			object obj;
			if (this.Parameters.TryGetValue(Name, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			string text;
			if (this.StringParameters.TryGetValue(Name, out text) && text is T)
			{
				return text as T;
			}
			int num;
			if (this.IntParameters.TryGetValue(Name, out num) && num is T)
			{
				return num as T;
			}
			return Default;
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x001B8605 File Offset: 0x001B6805
		protected Event SetParameterInternal(string Name, string Value)
		{
			this.StringParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x001B8615 File Offset: 0x001B6815
		protected Event SetParameterInternal(string Name, int Value)
		{
			this.IntParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x001B8625 File Offset: 0x001B6825
		protected Event SetParameterInternal(string Name, object Value)
		{
			if (Value is int)
			{
				this.SetParameterInternal(Name, (int)Value);
			}
			if (Value is string)
			{
				this.SetParameterInternal(Name, (string)Value);
			}
			this.Parameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x001B8661 File Offset: 0x001B6861
		public virtual Event SetParameter(string Name, object Value)
		{
			this.Parameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x001B8671 File Offset: 0x001B6871
		public virtual Event SetParameter(string Name, string Value)
		{
			this.StringParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x001B8681 File Offset: 0x001B6881
		public virtual Event SetParameter(string Name, int Value)
		{
			this.IntParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x001B8691 File Offset: 0x001B6891
		public virtual Event SetFloatParameter(string Name, float Value)
		{
			this.IntParameters[Name] = BitConverter.SingleToInt32Bits(Value);
			return this;
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x001B86A6 File Offset: 0x001B68A6
		public virtual Event AddParameter(string Name, string Value)
		{
			this.StringParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x001B86B6 File Offset: 0x001B68B6
		public virtual Event AddParameter(string Name, int Value)
		{
			this.IntParameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x001B86C6 File Offset: 0x001B68C6
		public virtual Event AddParameter(string Name, object Value)
		{
			this.Parameters[Name] = Value;
			return this;
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x001B86D6 File Offset: 0x001B68D6
		public virtual Event ModParameter(string Name, string Value)
		{
			this.StringParameters[Name] = this.GetStringParameter(Name, "") + Value;
			return this;
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x001B86F7 File Offset: 0x001B68F7
		public virtual Event ModParameter(string Name, int Value)
		{
			this.IntParameters[Name] = this.GetIntParameter(Name, 0) + Value;
			return this;
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x001B8710 File Offset: 0x001B6910
		public virtual Event ModParameter(string Name, float Value)
		{
			this.SetFloatParameter(Name, this.GetFloatParameter(Name, 0f) + Value);
			return this;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x001B872C File Offset: 0x001B692C
		public Event Copy(string _ID = null)
		{
			Event @event = Event.New(_ID ?? this.ID, 0, 0, 0);
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				@event.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in this.StringParameters)
			{
				@event.StringParameters.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
			foreach (KeyValuePair<string, int> keyValuePair3 in this.IntParameters)
			{
				@event.IntParameters.Add(keyValuePair3.Key, keyValuePair3.Value);
			}
			return @event;
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x001B8854 File Offset: 0x001B6A54
		public virtual void RequestInterfaceExit()
		{
			this.ExitInterfaceSignal = true;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x001B885D File Offset: 0x001B6A5D
		public bool InterfaceExitRequested()
		{
			return this.ExitInterfaceSignal;
		}

		// Token: 0x06005AF1 RID: 23281 RVA: 0x001B8868 File Offset: 0x001B6A68
		public void PreprocessChildEvent(IEvent E)
		{
			if (this.ID == "TakeDamage")
			{
				IDamageEvent damageEvent = E as IDamageEvent;
				if (damageEvent != null && this.HasFlag("WillUseOutcomeMessageFragment"))
				{
					damageEvent.WillUseOutcomeMessageFragment = true;
					damageEvent.OutcomeMessageFragment = this.GetStringParameter("OutcomeMessageFragment", null);
				}
			}
		}

		// Token: 0x06005AF2 RID: 23282 RVA: 0x001B88B8 File Offset: 0x001B6AB8
		public void ProcessChildEvent(IEvent E)
		{
			if (E.InterfaceExitRequested())
			{
				this.ExitInterfaceSignal = true;
			}
			if (this.ID == "TakeDamage")
			{
				IDamageEvent damageEvent = E as IDamageEvent;
				if (damageEvent != null && damageEvent.WillUseOutcomeMessageFragment && damageEvent.OutcomeMessageFragment != null)
				{
					this.SetParameter("OutcomeMessageFragment", damageEvent.OutcomeMessageFragment);
				}
			}
		}

		// Token: 0x06005AF3 RID: 23283 RVA: 0x001B8912 File Offset: 0x001B6B12
		public void ProcessChildEvent(Event E)
		{
			if (E.ExitInterfaceSignal)
			{
				this.ExitInterfaceSignal = true;
			}
			this.SetFlag("DidSpecialEffect", E.HasFlag("DidSpecialEffect"));
		}

		// Token: 0x06005AF4 RID: 23284 RVA: 0x001B893C File Offset: 0x001B6B3C
		public void AddAICommand(string Command, int Priority = 1, GameObject Object = null, bool Inv = false, bool Self = false, GameObject TargetOverride = null)
		{
			List<AICommandList> list = this.GetParameter("List") as List<AICommandList>;
			if (list == null)
			{
				global::UnityEngine.Debug.Log("AddAICommand() called on " + this.ID + " event with no List parameter");
				return;
			}
			AICommandList aicommandList = new AICommandList(Command, Priority, Object, Inv, Self, TargetOverride, null);
			if (Priority > list.Capacity)
			{
				list.Capacity += Priority;
			}
			for (int i = 0; i < Priority; i++)
			{
				list.Add(aicommandList);
			}
		}

		// Token: 0x06005AF5 RID: 23285 RVA: 0x001B89B4 File Offset: 0x001B6BB4
		public bool AddInventoryAction(string Name, string Display = null, string Command = null, char Key = ' ', bool FireOnActor = false, int Default = 0, bool Override = false, bool WorksAtDistance = false, bool WorksTelekinetically = false, bool WorksTelepathically = false, bool AsMinEvent = false, GameObject FireOn = null)
		{
			EventParameterGetInventoryActions eventParameterGetInventoryActions = this.GetParameter("Actions") as EventParameterGetInventoryActions;
			if (eventParameterGetInventoryActions == null)
			{
				global::UnityEngine.Debug.Log("AddInventoryAction() called on " + this.ID + " event with no Actions parameter");
				return false;
			}
			return eventParameterGetInventoryActions.AddAction(Name, Key, FireOnActor, Display, Command, null, Default, 0, Override, WorksAtDistance, WorksTelekinetically, WorksTelepathically, AsMinEvent, FireOn);
		}

		// Token: 0x06005AF6 RID: 23286 RVA: 0x001B8A14 File Offset: 0x001B6C14
		public bool AddInventoryAction(string Name, char Key, bool FireOnActor, string Display, string Command, int Default = 0, bool Override = false, bool WorksAtDistance = false, bool WorksTelekinetically = false, bool WorksTelepathically = false, bool AsMinEvent = false, GameObject FireOn = null)
		{
			return this.AddInventoryAction(Name, Display, Command, Key, FireOnActor, Default, Override, WorksAtDistance, WorksTelekinetically, WorksTelepathically, AsMinEvent, FireOn);
		}

		// Token: 0x06005AF7 RID: 23287 RVA: 0x001B8A3C File Offset: 0x001B6C3C
		public void AppendToStringBuilder(string Name, string Text)
		{
			StringBuilder stringBuilder = this.GetParameter(Name) as StringBuilder;
			if (stringBuilder == null)
			{
				global::UnityEngine.Debug.Log("AppendToStringBuilder() trying to append to " + Name + ", not found");
				return;
			}
			stringBuilder.Append(Text);
		}

		// Token: 0x06005AF8 RID: 23288 RVA: 0x001B8A77 File Offset: 0x001B6C77
		public bool ActuateOn(GameObject obj)
		{
			return obj.FireEvent(this);
		}

		// Token: 0x04002CD1 RID: 11473
		[NonSerialized]
		private static int PeakEventPool = 0;

		// Token: 0x04002CD2 RID: 11474
		[NonSerialized]
		private static int PeakStringPool = 0;

		// Token: 0x04002CD3 RID: 11475
		[NonSerialized]
		private const int EventPoolSize = 4000;

		// Token: 0x04002CD4 RID: 11476
		[NonSerialized]
		private const int StringPoolInitialSize = 1000;

		// Token: 0x04002CD5 RID: 11477
		private static int pinGameObjectListLocation;

		// Token: 0x04002CD6 RID: 11478
		private static int pinStringLocation;

		// Token: 0x04002CD7 RID: 11479
		private static int pinLocation;

		// Token: 0x04002CD8 RID: 11480
		[NonSerialized]
		public static List<StringBuilder> StringBuilderPool = null;

		// Token: 0x04002CD9 RID: 11481
		[NonSerialized]
		public static int nStringBuilderPoolCounter = 0;

		// Token: 0x04002CDA RID: 11482
		[NonSerialized]
		public static List<List<GameObject>> GameObjectListPool = new List<List<GameObject>>();

		// Token: 0x04002CDB RID: 11483
		[NonSerialized]
		public static int nGameObjectListPoolCounter = 0;

		// Token: 0x04002CDC RID: 11484
		[NonSerialized]
		public static List<List<Cell>> CellListPool = new List<List<Cell>>();

		// Token: 0x04002CDD RID: 11485
		[NonSerialized]
		public static int nCellListPoolCounter = 0;

		// Token: 0x04002CDE RID: 11486
		public static bool bWarnedThisClear = false;

		// Token: 0x04002CDF RID: 11487
		public static Event[] EventPool = null;

		// Token: 0x04002CE0 RID: 11488
		public static int nPoolCounter = 0;

		// Token: 0x04002CE1 RID: 11489
		public static int nExceedCounter = 0;

		// Token: 0x04002CE2 RID: 11490
		public static Dictionary<string, int> TopEvents = new Dictionary<string, int>(200);

		// Token: 0x04002CE3 RID: 11491
		public string ID = "-";

		// Token: 0x04002CE4 RID: 11492
		protected Dictionary<string, object> Parameters;

		// Token: 0x04002CE5 RID: 11493
		protected Dictionary<string, string> StringParameters;

		// Token: 0x04002CE6 RID: 11494
		protected Dictionary<string, int> IntParameters;
	}
}
