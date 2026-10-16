using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000703 RID: 1795
	internal class EventDebugger
	{
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000CA401 File Offset: 0x000C8601
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000CA409 File Offset: 0x000C8609
		public IPanel panel { get; set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000CA412 File Offset: 0x000C8612
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000CA41A File Offset: 0x000C861A
		public bool isReplaying { get; private set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000CA423 File Offset: 0x000C8623
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000CA42B File Offset: 0x000C862B
		public float playbackSpeed { get; set; } = 1f;

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000CA434 File Offset: 0x000C8634
		// (set) Token: 0x060033C0 RID: 13248 RVA: 0x000CA43C File Offset: 0x000C863C
		public bool isPlaybackPaused { get; set; }

		// Token: 0x060033C1 RID: 13249 RVA: 0x000CA448 File Offset: 0x000C8648
		public void UpdateModificationCount()
		{
			bool flag = this.panel == null;
			if (!flag)
			{
				long num;
				bool flag2 = !this.m_ModificationCount.TryGetValue(this.panel, out num);
				if (flag2)
				{
					num = 0L;
				}
				num += 1L;
				this.m_ModificationCount[this.panel] = num;
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000CA49C File Offset: 0x000C869C
		public void BeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			this.AddBeginProcessEvent(evt, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000CA4AF File Offset: 0x000C86AF
		public void EndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddEndProcessEvent(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000CA4C3 File Offset: 0x000C86C3
		public void LogCall(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, long duration, IEventHandler mouseCapture)
		{
			this.AddCallObject(cbHashCode, cbName, evt, propagationHasStopped, immediatePropagationHasStopped, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000CA4DF File Offset: 0x000C86DF
		public void LogIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddIMGUICall(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000CA4F3 File Offset: 0x000C86F3
		public void LogExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			this.AddExecuteDefaultAction(evt, phase, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x00002152 File Offset: 0x00000352
		public static void LogPropagationPaths(EventBase evt, PropagationPaths paths)
		{
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000CA509 File Offset: 0x000C8709
		private void LogPropagationPathsInternal(EventBase evt, PropagationPaths paths)
		{
			this.AddPropagationPaths(evt, paths);
			this.UpdateModificationCount();
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000CA51C File Offset: 0x000C871C
		public List<EventDebuggerCallTrace> GetCalls(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerCallTrace> list;
			bool flag = !this.m_EventCalledObjects.TryGetValue(panel, out list);
			List<EventDebuggerCallTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerCallTrace> list3 = new List<EventDebuggerCallTrace>();
					foreach (EventDebuggerCallTrace eventDebuggerCallTrace in list)
					{
						bool flag3 = eventDebuggerCallTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerCallTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000CA5CC File Offset: 0x000C87CC
		public List<EventDebuggerDefaultActionTrace> GetDefaultActions(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerDefaultActionTrace> list;
			bool flag = !this.m_EventDefaultActionObjects.TryGetValue(panel, out list);
			List<EventDebuggerDefaultActionTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerDefaultActionTrace> list3 = new List<EventDebuggerDefaultActionTrace>();
					foreach (EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace in list)
					{
						bool flag3 = eventDebuggerDefaultActionTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerDefaultActionTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000CA67C File Offset: 0x000C887C
		public List<EventDebuggerPathTrace> GetPropagationPaths(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerPathTrace> list;
			bool flag = !this.m_EventPathObjects.TryGetValue(panel, out list);
			List<EventDebuggerPathTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerPathTrace> list3 = new List<EventDebuggerPathTrace>();
					foreach (EventDebuggerPathTrace eventDebuggerPathTrace in list)
					{
						bool flag3 = eventDebuggerPathTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerPathTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000CA72C File Offset: 0x000C892C
		public List<EventDebuggerTrace> GetBeginEndProcessedEvents(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerTrace> list;
			bool flag = !this.m_EventProcessedEvents.TryGetValue(panel, out list);
			List<EventDebuggerTrace> list2;
			if (flag)
			{
				list2 = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerTrace> list3 = new List<EventDebuggerTrace>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list3.Add(eventDebuggerTrace);
						}
					}
					list = list3;
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000CA7DC File Offset: 0x000C89DC
		public long GetModificationCount(IPanel panel)
		{
			bool flag = panel == null;
			long num;
			if (flag)
			{
				num = -1L;
			}
			else
			{
				long num2;
				bool flag2 = !this.m_ModificationCount.TryGetValue(panel, out num2);
				if (flag2)
				{
					num2 = -1L;
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000CA818 File Offset: 0x000C8A18
		public void ClearLogs()
		{
			this.UpdateModificationCount();
			bool flag = this.panel == null;
			if (flag)
			{
				this.m_EventCalledObjects.Clear();
				this.m_EventDefaultActionObjects.Clear();
				this.m_EventPathObjects.Clear();
				this.m_EventProcessedEvents.Clear();
				this.m_StackOfProcessedEvent.Clear();
				this.m_EventTypeProcessedCount.Clear();
			}
			else
			{
				this.m_EventCalledObjects.Remove(this.panel);
				this.m_EventDefaultActionObjects.Remove(this.panel);
				this.m_EventPathObjects.Remove(this.panel);
				this.m_EventProcessedEvents.Remove(this.panel);
				this.m_StackOfProcessedEvent.Remove(this.panel);
				Dictionary<long, int> dictionary;
				bool flag2 = this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
				if (flag2)
				{
					dictionary.Clear();
				}
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000CA900 File Offset: 0x000C8B00
		public void SaveReplaySessionFromSelection(string path, List<EventDebuggerEventRecord> eventList)
		{
			bool flag = string.IsNullOrEmpty(path);
			if (!flag)
			{
				EventDebuggerRecordList eventDebuggerRecordList = new EventDebuggerRecordList
				{
					eventList = eventList
				};
				string text = JsonUtility.ToJson(eventDebuggerRecordList);
				File.WriteAllText(path, text);
				Debug.Log("Saved under: " + path);
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000CA948 File Offset: 0x000C8B48
		public EventDebuggerRecordList LoadReplaySession(string path)
		{
			bool flag = string.IsNullOrEmpty(path);
			EventDebuggerRecordList eventDebuggerRecordList;
			if (flag)
			{
				eventDebuggerRecordList = null;
			}
			else
			{
				string text = File.ReadAllText(path);
				eventDebuggerRecordList = JsonUtility.FromJson<EventDebuggerRecordList>(text);
			}
			return eventDebuggerRecordList;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000CA975 File Offset: 0x000C8B75
		public IEnumerator ReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			bool flag = eventBases == null;
			if (flag)
			{
				yield break;
			}
			this.isReplaying = true;
			IEnumerator doReplay = this.DoReplayEvents(eventBases, refreshList);
			while (doReplay.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000CA992 File Offset: 0x000C8B92
		public void StopPlayback()
		{
			this.isReplaying = false;
			this.isPlaybackPaused = false;
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000CA9A5 File Offset: 0x000C8BA5
		private IEnumerator DoReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			EventDebugger.<>c__DisplayClass34_0 CS$<>8__locals1 = new EventDebugger.<>c__DisplayClass34_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sortedEvents = eventBases.OrderBy<EventDebuggerEventRecord, long>((EventDebuggerEventRecord e) => e.timestamp).ToList<EventDebuggerEventRecord>();
			int sortedEventsCount = CS$<>8__locals1.sortedEvents.Count;
			int i = 0;
			while (i < sortedEventsCount)
			{
				bool flag = !this.isReplaying;
				if (flag)
				{
					break;
				}
				EventDebuggerEventRecord eventBase = CS$<>8__locals1.sortedEvents[i];
				Event newEvent = new Event
				{
					button = eventBase.button,
					clickCount = eventBase.clickCount,
					modifiers = eventBase.modifiers,
					mousePosition = eventBase.mousePosition
				};
				bool flag2 = eventBase.eventTypeId == EventBase<MouseMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag2)
				{
					newEvent.type = EventType.MouseMove;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove));
					goto IL_0812;
				}
				bool flag3 = eventBase.eventTypeId == EventBase<MouseDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag3)
				{
					newEvent.type = EventType.MouseDown;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown));
					goto IL_0812;
				}
				bool flag4 = eventBase.eventTypeId == EventBase<MouseUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag4)
				{
					newEvent.type = EventType.MouseUp;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp));
					goto IL_0812;
				}
				bool flag5 = eventBase.eventTypeId == EventBase<ContextClickEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag5)
				{
					newEvent.type = EventType.ContextClick;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.ContextClick));
					goto IL_0812;
				}
				bool flag6 = eventBase.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag6)
				{
					newEvent.type = EventType.MouseEnterWindow;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseEnterWindow));
					goto IL_0812;
				}
				bool flag7 = eventBase.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag7)
				{
					newEvent.type = EventType.MouseLeaveWindow;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseLeaveWindow));
					goto IL_0812;
				}
				bool flag8 = eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag8)
				{
					newEvent.type = EventType.MouseMove;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove));
					goto IL_0812;
				}
				bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag9)
				{
					newEvent.type = EventType.MouseDown;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown));
					goto IL_0812;
				}
				bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag10)
				{
					newEvent.type = EventType.MouseUp;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp));
					goto IL_0812;
				}
				bool flag11 = eventBase.eventTypeId == EventBase<WheelEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag11)
				{
					newEvent.type = EventType.ScrollWheel;
					newEvent.delta = eventBase.delta;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.ScrollWheel));
					goto IL_0812;
				}
				bool flag12 = eventBase.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag12)
				{
					newEvent.type = EventType.KeyDown;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.KeyDown));
					goto IL_0812;
				}
				bool flag13 = eventBase.eventTypeId == EventBase<KeyUpEvent>.TypeId();
				if (flag13)
				{
					newEvent.type = EventType.KeyUp;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.KeyUp));
					goto IL_0812;
				}
				bool flag14 = eventBase.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
				if (flag14)
				{
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(NavigationMoveEvent.GetPooled(eventBase.navigationDirection, eventBase.deviceType, eventBase.modifiers));
					goto IL_0812;
				}
				bool flag15 = eventBase.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId();
				if (flag15)
				{
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(NavigationEventBase<NavigationSubmitEvent>.GetPooled(eventBase.deviceType, eventBase.modifiers));
					goto IL_0812;
				}
				bool flag16 = eventBase.eventTypeId == EventBase<NavigationCancelEvent>.TypeId();
				if (flag16)
				{
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(NavigationEventBase<NavigationCancelEvent>.GetPooled(eventBase.deviceType, eventBase.modifiers));
					goto IL_0812;
				}
				bool flag17 = eventBase.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
				if (flag17)
				{
					newEvent.type = EventType.ValidateCommand;
					newEvent.commandName = eventBase.commandName;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.ValidateCommand));
					goto IL_0812;
				}
				bool flag18 = eventBase.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
				if (flag18)
				{
					newEvent.type = EventType.ExecuteCommand;
					newEvent.commandName = eventBase.commandName;
					CS$<>8__locals1.<DoReplayEvents>g__SendEvent|2(UIElementsUtility.CreateEvent(newEvent, EventType.ExecuteCommand));
					goto IL_0812;
				}
				bool flag19 = eventBase.eventTypeId == EventBase<IMGUIEvent>.TypeId();
				if (flag19)
				{
					string text = "Skipped IMGUI event (";
					string eventBaseName = eventBase.eventBaseName;
					string text2 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord = eventBase;
					Debug.Log(text + eventBaseName + text2 + ((eventDebuggerEventRecord != null) ? eventDebuggerEventRecord.ToString() : null));
					IEnumerator awaitSkipped = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped.MoveNext())
					{
						yield return null;
					}
				}
				else
				{
					string text3 = "Skipped event (";
					string eventBaseName2 = eventBase.eventBaseName;
					string text4 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord2 = eventBase;
					Debug.Log(text3 + eventBaseName2 + text4 + ((eventDebuggerEventRecord2 != null) ? eventDebuggerEventRecord2.ToString() : null));
					IEnumerator awaitSkipped2 = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped2.MoveNext())
					{
						yield return null;
					}
				}
				IL_08BC:
				int num = i;
				i = num + 1;
				continue;
				IL_0812:
				if (refreshList != null)
				{
					refreshList(i, sortedEventsCount);
				}
				Debug.Log(string.Format("Replayed event {0} ({1}): {2}", eventBase.eventId.ToString(), eventBase.eventBaseName, newEvent));
				IEnumerator await = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
				while (await.MoveNext())
				{
					yield return null;
				}
				eventBase = null;
				newEvent = null;
				await = null;
				goto IL_08BC;
			}
			this.isReplaying = false;
			yield break;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000CA9C4 File Offset: 0x000C8BC4
		public Dictionary<string, EventDebugger.HistogramRecord> ComputeHistogram(List<EventDebuggerEventRecord> eventBases)
		{
			List<EventDebuggerTrace> list;
			bool flag = this.panel == null || !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
			Dictionary<string, EventDebugger.HistogramRecord> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				bool flag2 = list == null;
				if (flag2)
				{
					dictionary = null;
				}
				else
				{
					Dictionary<string, EventDebugger.HistogramRecord> dictionary2 = new Dictionary<string, EventDebugger.HistogramRecord>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventBases == null || eventBases.Count == 0 || eventBases.Contains(eventDebuggerTrace.eventBase);
						if (flag3)
						{
							string eventBaseName = eventDebuggerTrace.eventBase.eventBaseName;
							long num = eventDebuggerTrace.duration;
							long num2 = 1L;
							EventDebugger.HistogramRecord histogramRecord;
							bool flag4 = dictionary2.TryGetValue(eventBaseName, out histogramRecord);
							if (flag4)
							{
								num += histogramRecord.duration;
								num2 += histogramRecord.count;
							}
							dictionary2[eventBaseName] = new EventDebugger.HistogramRecord
							{
								count = num2,
								duration = num
							};
						}
					}
					dictionary = dictionary2;
				}
			}
			return dictionary;
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000CAAF0 File Offset: 0x000C8CF0
		public Dictionary<long, int> eventTypeProcessedCount
		{
			get
			{
				Dictionary<long, int> dictionary;
				return this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary) ? dictionary : null;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x000CAB16 File Offset: 0x000C8D16
		// (set) Token: 0x060033D7 RID: 13271 RVA: 0x000CAB1E File Offset: 0x000C8D1E
		public bool suspended { get; set; }

		// Token: 0x060033D8 RID: 13272 RVA: 0x000CAB28 File Offset: 0x000C8D28
		public EventDebugger()
		{
			this.m_EventCalledObjects = new Dictionary<IPanel, List<EventDebuggerCallTrace>>();
			this.m_EventDefaultActionObjects = new Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>>();
			this.m_EventPathObjects = new Dictionary<IPanel, List<EventDebuggerPathTrace>>();
			this.m_StackOfProcessedEvent = new Dictionary<IPanel, Stack<EventDebuggerTrace>>();
			this.m_EventProcessedEvents = new Dictionary<IPanel, List<EventDebuggerTrace>>();
			this.m_EventTypeProcessedCount = new Dictionary<IPanel, Dictionary<long, int>>();
			this.m_ModificationCount = new Dictionary<IPanel, long>();
			this.m_Log = true;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000CAB9C File Offset: 0x000C8D9C
		private void AddCallObject(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, cbHashCode, cbName, propagationHasStopped, immediatePropagationHasStopped, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerCallTrace);
				}
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000CAC18 File Offset: 0x000C8E18
		private void AddExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace = new EventDebuggerDefaultActionTrace(this.panel, evt, phase, duration, mouseCapture);
					List<EventDebuggerDefaultActionTrace> list;
					bool flag = !this.m_EventDefaultActionObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerDefaultActionTrace>();
						this.m_EventDefaultActionObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerDefaultActionTrace);
				}
			}
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000CAC90 File Offset: 0x000C8E90
		private void AddPropagationPaths(EventBase evt, PropagationPaths paths)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerPathTrace eventDebuggerPathTrace = new EventDebuggerPathTrace(this.panel, evt, new PropagationPaths(paths));
					List<EventDebuggerPathTrace> list;
					bool flag = !this.m_EventPathObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerPathTrace>();
						this.m_EventPathObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerPathTrace);
				}
			}
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000CAD08 File Offset: 0x000C8F08
		private void AddIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace eventDebuggerCallTrace = new EventDebuggerCallTrace(this.panel, evt, 0, "OnGUI", false, false, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(eventDebuggerCallTrace);
				}
			}
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000CAD84 File Offset: 0x000C8F84
		private void AddBeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				EventDebuggerTrace eventDebuggerTrace = new EventDebuggerTrace(this.panel, evt, -1L, mouseCapture);
				Stack<EventDebuggerTrace> stack;
				bool flag = !this.m_StackOfProcessedEvent.TryGetValue(this.panel, out stack);
				if (flag)
				{
					stack = new Stack<EventDebuggerTrace>();
					this.m_StackOfProcessedEvent.Add(this.panel, stack);
				}
				List<EventDebuggerTrace> list;
				bool flag2 = !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
				if (flag2)
				{
					list = new List<EventDebuggerTrace>();
					this.m_EventProcessedEvents.Add(this.panel, list);
				}
				list.Add(eventDebuggerTrace);
				stack.Push(eventDebuggerTrace);
				Dictionary<long, int> dictionary;
				bool flag3 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
				if (!flag3)
				{
					int num;
					bool flag4 = !dictionary.TryGetValue(eventDebuggerTrace.eventBase.eventTypeId, out num);
					if (flag4)
					{
						num = 0;
					}
					dictionary[eventDebuggerTrace.eventBase.eventTypeId] = num + 1;
				}
			}
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000CAE84 File Offset: 0x000C9084
		private void AddEndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool flag = false;
				Stack<EventDebuggerTrace> stack;
				bool flag2 = this.m_StackOfProcessedEvent.TryGetValue(this.panel, out stack);
				if (flag2)
				{
					bool flag3 = stack.Count > 0;
					if (flag3)
					{
						EventDebuggerTrace eventDebuggerTrace = stack.Peek();
						bool flag4 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag4)
						{
							stack.Pop();
							eventDebuggerTrace.duration = duration;
							bool flag5 = eventDebuggerTrace.eventBase.target == null;
							if (flag5)
							{
								eventDebuggerTrace.eventBase.target = evt.target;
							}
							flag = true;
						}
					}
				}
				bool flag6 = !flag;
				if (flag6)
				{
					EventDebuggerTrace eventDebuggerTrace2 = new EventDebuggerTrace(this.panel, evt, duration, mouseCapture);
					List<EventDebuggerTrace> list;
					bool flag7 = !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
					if (flag7)
					{
						list = new List<EventDebuggerTrace>();
						this.m_EventProcessedEvents.Add(this.panel, list);
					}
					list.Add(eventDebuggerTrace2);
					Dictionary<long, int> dictionary;
					bool flag8 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
					if (!flag8)
					{
						int num;
						bool flag9 = !dictionary.TryGetValue(eventDebuggerTrace2.eventBase.eventTypeId, out num);
						if (flag9)
						{
							num = 0;
						}
						dictionary[eventDebuggerTrace2.eventBase.eventTypeId] = num + 1;
					}
				}
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000CAFE8 File Offset: 0x000C91E8
		public static string GetObjectDisplayName(object obj, bool withHashCode = true)
		{
			bool flag = obj == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				Type type = obj.GetType();
				string text2 = EventDebugger.GetTypeDisplayName(type);
				bool flag2 = obj is VisualElement;
				if (flag2)
				{
					VisualElement visualElement = obj as VisualElement;
					bool flag3 = !string.IsNullOrEmpty(visualElement.name);
					if (flag3)
					{
						text2 = text2 + "#" + visualElement.name;
					}
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + obj.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000CB090 File Offset: 0x000C9290
		public static string GetTypeDisplayName(Type type)
		{
			return type.IsGenericType ? (type.Name.TrimEnd(new char[] { '`', '1' }) + "<" + type.GetGenericArguments()[0].Name + ">") : type.Name;
		}

		// Token: 0x04001A8F RID: 6799
		private Dictionary<IPanel, List<EventDebuggerCallTrace>> m_EventCalledObjects;

		// Token: 0x04001A90 RID: 6800
		private Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>> m_EventDefaultActionObjects;

		// Token: 0x04001A91 RID: 6801
		private Dictionary<IPanel, List<EventDebuggerPathTrace>> m_EventPathObjects;

		// Token: 0x04001A92 RID: 6802
		private Dictionary<IPanel, List<EventDebuggerTrace>> m_EventProcessedEvents;

		// Token: 0x04001A93 RID: 6803
		private Dictionary<IPanel, Stack<EventDebuggerTrace>> m_StackOfProcessedEvent;

		// Token: 0x04001A94 RID: 6804
		private Dictionary<IPanel, Dictionary<long, int>> m_EventTypeProcessedCount;

		// Token: 0x04001A95 RID: 6805
		private readonly Dictionary<IPanel, long> m_ModificationCount;

		// Token: 0x04001A96 RID: 6806
		private readonly bool m_Log;

		// Token: 0x02000704 RID: 1796
		internal struct HistogramRecord
		{
			// Token: 0x04001A98 RID: 6808
			public long count;

			// Token: 0x04001A99 RID: 6809
			public long duration;
		}
	}
}
