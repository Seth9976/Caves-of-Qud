using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020F RID: 527
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x00046D60 File Offset: 0x00044F60
		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00046D95 File Offset: 0x00044F95
		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00046DA4 File Offset: 0x00044FA4
		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00046DC8 File Offset: 0x00044FC8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00046E00 File Offset: 0x00045000
		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00046E28 File Offset: 0x00045028
		public static bool operator ==(EventDispatcherGate left, EventDispatcherGate right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00046E44 File Offset: 0x00045044
		public static bool operator !=(EventDispatcherGate left, EventDispatcherGate right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040009B8 RID: 2488
		private readonly EventDispatcher m_Dispatcher;
	}
}
