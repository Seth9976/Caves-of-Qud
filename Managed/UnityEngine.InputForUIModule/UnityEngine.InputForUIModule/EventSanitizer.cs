using System;
using System.Collections.Generic;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002F RID: 47
	internal struct EventSanitizer
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00005768 File Offset: 0x00003968
		public void Reset()
		{
			this._sanitizers = new EventSanitizer.IEventSanitizer[0];
			foreach (EventSanitizer.IEventSanitizer eventSanitizer in this._sanitizers)
			{
				eventSanitizer.Reset();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000057A4 File Offset: 0x000039A4
		public void BeforeProviderUpdate()
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer eventSanitizer in this._sanitizers)
			{
				eventSanitizer.BeforeProviderUpdate();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000057E8 File Offset: 0x000039E8
		public void AfterProviderUpdate()
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer eventSanitizer in this._sanitizers)
			{
				eventSanitizer.AfterProviderUpdate();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000582C File Offset: 0x00003A2C
		public void Inspect(in Event ev)
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer eventSanitizer in this._sanitizers)
			{
				eventSanitizer.Inspect(in ev);
			}
		}

		// Token: 0x040000E9 RID: 233
		private EventSanitizer.IEventSanitizer[] _sanitizers;

		// Token: 0x02000030 RID: 48
		private interface IEventSanitizer
		{
			// Token: 0x06000115 RID: 277
			void Reset();

			// Token: 0x06000116 RID: 278
			void BeforeProviderUpdate();

			// Token: 0x06000117 RID: 279
			void AfterProviderUpdate();

			// Token: 0x06000118 RID: 280
			void Inspect(in Event ev);
		}

		// Token: 0x02000031 RID: 49
		private struct ClickCountEventSanitizer : EventSanitizer.IEventSanitizer
		{
			// Token: 0x06000119 RID: 281 RVA: 0x00005870 File Offset: 0x00003A70
			public void Reset()
			{
				this._activeButtons = new List<PointerEvent>();
				this.lastPushedIndex = 0;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00003550 File Offset: 0x00001750
			public void BeforeProviderUpdate()
			{
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00003550 File Offset: 0x00001750
			public void AfterProviderUpdate()
			{
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00005888 File Offset: 0x00003A88
			public void Inspect(in Event ev)
			{
				Event @event = ev;
				bool flag = @event.type != Event.Type.PointerEvent;
				if (!flag)
				{
					@event = ev;
					PointerEvent asPointerEvent = @event.asPointerEvent;
					PointerEvent.Type type = asPointerEvent.type;
					PointerEvent.Type type2 = type;
					if (type2 != PointerEvent.Type.ButtonPressed)
					{
						if (type2 == PointerEvent.Type.ButtonReleased)
						{
							PointerEvent pointerEvent = asPointerEvent;
							for (int i = 0; i < this._activeButtons.Count; i++)
							{
								PointerEvent pointerEvent2 = this._activeButtons[i];
								bool flag2 = pointerEvent2.eventSource != pointerEvent.eventSource || pointerEvent2.pointerIndex != pointerEvent.pointerIndex;
								if (!flag2)
								{
									bool flag3 = i == this.lastPushedIndex;
									if (flag3)
									{
										bool flag4 = pointerEvent2.clickCount != pointerEvent.clickCount;
										if (flag4)
										{
											Debug.LogWarning(string.Format("ButtonReleased click count doesn't match ButtonPressed click count, where '{0}' and '{1}'", pointerEvent2, pointerEvent));
										}
									}
									else
									{
										bool flag5 = pointerEvent.clickCount != 1;
										if (flag5)
										{
											Debug.LogWarning(string.Format("ButtonReleased for not the last pressed button should have click count == 1, but got '{0}'", pointerEvent));
										}
									}
									this._activeButtons.RemoveAt(i);
									return;
								}
							}
							Debug.LogWarning(string.Format("Can't find corresponding ButtonPressed for '{0}'", ev));
						}
					}
					else
					{
						this.lastPushedIndex = this._activeButtons.Count;
						this._activeButtons.Add(asPointerEvent);
					}
				}
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00005A13 File Offset: 0x00003C13
			void EventSanitizer.IEventSanitizer.Inspect(in Event ev)
			{
				this.Inspect(in ev);
			}

			// Token: 0x040000EA RID: 234
			private List<PointerEvent> _activeButtons;

			// Token: 0x040000EB RID: 235
			private int lastPushedIndex;
		}

		// Token: 0x02000032 RID: 50
		private class DefaultEventSystemSanitizer : EventSanitizer.IEventSanitizer
		{
			// Token: 0x0600011E RID: 286 RVA: 0x00003550 File Offset: 0x00001750
			public void Reset()
			{
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00005A1C File Offset: 0x00003C1C
			public void BeforeProviderUpdate()
			{
				this.m_MouseEventCount = 0;
				this.m_PenOrTouchEventCount = 0;
			}

			// Token: 0x06000120 RID: 288 RVA: 0x00005A30 File Offset: 0x00003C30
			public void AfterProviderUpdate()
			{
				bool flag = this.m_MouseEventCount > 0 && this.m_PenOrTouchEventCount > 0;
				if (flag)
				{
					Debug.LogError("PointerEvents of source Mouse and [Pen or Touch] received in the same update. This is likely an error, and Mouse events should be discarded.");
				}
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00005A64 File Offset: 0x00003C64
			public void Inspect(in Event ev)
			{
				Event @event = ev;
				bool flag = @event.type == Event.Type.PointerEvent;
				if (flag)
				{
					@event = ev;
					PointerEvent asPointerEvent = @event.asPointerEvent;
					bool flag2 = asPointerEvent.type == PointerEvent.Type.ButtonPressed && asPointerEvent.button == PointerEvent.Button.None;
					if (flag2)
					{
						Debug.LogError("PointerEvent of type ButtonPressed must have button property set to a value other than None.");
					}
					bool flag3 = asPointerEvent.type == PointerEvent.Type.ButtonReleased && asPointerEvent.button == PointerEvent.Button.None;
					if (flag3)
					{
						Debug.LogError("PointerEvent of type ButtonReleased must have button property set to a value other than None.");
					}
					bool flag4 = asPointerEvent.eventSource == EventSource.Mouse;
					if (flag4)
					{
						this.m_MouseEventCount++;
						bool flag5 = !asPointerEvent.isPrimaryPointer;
						if (flag5)
						{
							Debug.LogError("PointerEvent of source Mouse is expected to have isPrimaryPointer set to true.");
						}
						bool flag6 = asPointerEvent.pointerIndex != 0;
						if (flag6)
						{
							Debug.LogError("PointerEvent of source Mouse is expected to have pointerIndex set to 0.");
						}
					}
					else
					{
						bool flag7 = asPointerEvent.eventSource == EventSource.Touch;
						if (flag7)
						{
							this.m_PenOrTouchEventCount++;
							bool flag8 = asPointerEvent.button != PointerEvent.Button.None && asPointerEvent.button != PointerEvent.Button.Primary;
							if (flag8)
							{
								Debug.LogError("PointerEvent of source Touch is expected to have button set to None or FingerInTouch.");
							}
						}
						else
						{
							bool flag9 = asPointerEvent.eventSource == EventSource.Pen;
							if (flag9)
							{
								this.m_PenOrTouchEventCount++;
								bool flag10 = asPointerEvent.button != PointerEvent.Button.None && asPointerEvent.button != PointerEvent.Button.Primary && asPointerEvent.button != PointerEvent.Button.PenBarrelButton && asPointerEvent.button != PointerEvent.Button.PenEraserInTouch;
								if (flag10)
								{
									Debug.LogError("PointerEvent of source Pen is expected to have button set to None, PenTipInTouch, PenBarrelButton, or PenEraserInTouch.");
								}
							}
						}
					}
				}
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00005BEF File Offset: 0x00003DEF
			void EventSanitizer.IEventSanitizer.Inspect(in Event ev)
			{
				this.Inspect(in ev);
			}

			// Token: 0x040000EC RID: 236
			private int m_MouseEventCount;

			// Token: 0x040000ED RID: 237
			private int m_PenOrTouchEventCount;
		}
	}
}
