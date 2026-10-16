using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Events
{
	// Token: 0x0200033A RID: 826
	internal class PropertyHelper<TBase>
	{
		// Token: 0x060027B7 RID: 10167 RVA: 0x0004C23C File Offset: 0x0004A43C
		public bool SetProperty<TData>(TBase instance, ref TData currentPropertyValue, TData newValue, [CallerMemberName] string propertyName = "")
		{
			bool flag = object.Equals(currentPropertyValue, newValue);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				currentPropertyValue = newValue;
				this.NotifyValueChange(instance, propertyName);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0004C27E File Offset: 0x0004A47E
		public void NotifyValueChange(TBase instance, [CallerMemberName] string propertyName = "")
		{
			this.propertyChangedEvent.Notify(instance, propertyName);
		}

		// Token: 0x04000B26 RID: 2854
		public PropertyHelper<TBase>.PropertyChangedEvent propertyChangedEvent = new PropertyHelper<TBase>.PropertyChangedEvent();

		// Token: 0x0200033B RID: 827
		private interface IEventHolder
		{
			// Token: 0x060027BA RID: 10170
			void Invoke(TBase setting, string property);

			// Token: 0x060027BB RID: 10171
			bool IsEmpty();
		}

		// Token: 0x0200033C RID: 828
		private class EventHolder<T> : PropertyHelper<TBase>.IEventHolder where T : class, TBase
		{
			// Token: 0x14000023 RID: 35
			// (add) Token: 0x060027BC RID: 10172 RVA: 0x0004C2A4 File Offset: 0x0004A4A4
			// (remove) Token: 0x060027BD RID: 10173 RVA: 0x0004C2DC File Offset: 0x0004A4DC
			[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Action<T, string> propertyEvent;

			// Token: 0x060027BE RID: 10174 RVA: 0x0004C311 File Offset: 0x0004A511
			void PropertyHelper<TBase>.IEventHolder.Invoke(TBase setting, string property)
			{
				Action<T, string> action = this.propertyEvent;
				if (action != null)
				{
					action(setting as T, property);
				}
			}

			// Token: 0x060027BF RID: 10175 RVA: 0x0004C337 File Offset: 0x0004A537
			bool PropertyHelper<TBase>.IEventHolder.IsEmpty()
			{
				return this.propertyEvent == null;
			}
		}

		// Token: 0x0200033D RID: 829
		public struct PropertyChangedEvent
		{
			// Token: 0x060027C1 RID: 10177 RVA: 0x0004C342 File Offset: 0x0004A542
			public PropertyChangedEvent()
			{
				this.m_Subscriptions = new Dictionary<Type, PropertyHelper<TBase>.IEventHolder>();
			}

			// Token: 0x060027C2 RID: 10178 RVA: 0x0004C350 File Offset: 0x0004A550
			public void Subscribe<TChild>(Action<TChild, string> callback) where TChild : class, TBase
			{
				PropertyHelper<TBase>.IEventHolder eventHolder;
				bool flag = this.m_Subscriptions.TryGetValue(typeof(TChild), out eventHolder);
				PropertyHelper<TBase>.EventHolder<TChild> eventHolder2;
				if (flag)
				{
					eventHolder2 = eventHolder as PropertyHelper<TBase>.EventHolder<TChild>;
				}
				else
				{
					eventHolder2 = new PropertyHelper<TBase>.EventHolder<TChild>();
					this.m_Subscriptions.Add(typeof(TChild), eventHolder2);
				}
				eventHolder2.propertyEvent += callback;
			}

			// Token: 0x060027C3 RID: 10179 RVA: 0x0004C3B0 File Offset: 0x0004A5B0
			public void Unsubscribe<TChild>(Action<TChild, string> callback) where TChild : class, TBase
			{
				PropertyHelper<TBase>.IEventHolder eventHolder;
				bool flag = !this.m_Subscriptions.TryGetValue(typeof(TChild), out eventHolder);
				if (!flag)
				{
					PropertyHelper<TBase>.EventHolder<TChild> eventHolder2 = eventHolder as PropertyHelper<TBase>.EventHolder<TChild>;
					eventHolder2.propertyEvent -= callback;
					bool flag2 = eventHolder.IsEmpty();
					if (flag2)
					{
						this.m_Subscriptions.Remove(typeof(TChild));
					}
				}
			}

			// Token: 0x060027C4 RID: 10180 RVA: 0x0004C410 File Offset: 0x0004A610
			public void Notify(TBase instance, [CallerMemberName] string propertyName = "")
			{
				PropertyHelper<TBase>.IEventHolder eventHolder;
				bool flag = this.m_Subscriptions.TryGetValue(instance.GetType(), out eventHolder);
				if (flag)
				{
					eventHolder.Invoke(instance, propertyName);
				}
			}

			// Token: 0x04000B28 RID: 2856
			private Dictionary<Type, PropertyHelper<TBase>.IEventHolder> m_Subscriptions;
		}
	}
}
