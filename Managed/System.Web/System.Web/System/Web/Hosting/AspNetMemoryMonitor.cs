using System;
using Unity;

namespace System.Web.Hosting
{
	// Token: 0x0200075D RID: 1885
	public sealed class AspNetMemoryMonitor : IDisposable, IObservable<LowPhysicalMemoryInfo>, IObservable<RecycleLimitInfo>, IApplicationMonitor
	{
		// Token: 0x06004CF1 RID: 19697 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal AspNetMemoryMonitor()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		// (set) Token: 0x06004CF3 RID: 19699 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IObserver<LowPhysicalMemoryInfo> DefaultLowPhysicalMemoryObserver
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		// (set) Token: 0x06004CF5 RID: 19701 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IObserver<RecycleLimitInfo> DefaultRecycleLimitObserver
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Start()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Stop()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004CF9 RID: 19705 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IDisposable Subscribe(IObserver<LowPhysicalMemoryInfo> observer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IDisposable Subscribe(IObserver<RecycleLimitInfo> observer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
