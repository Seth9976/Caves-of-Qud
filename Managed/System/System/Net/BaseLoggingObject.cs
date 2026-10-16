using System;

namespace System.Net
{
	// Token: 0x02000631 RID: 1585
	internal class BaseLoggingObject
	{
		// Token: 0x060031FD RID: 12797 RVA: 0x0000219B File Offset: 0x0000039B
		internal BaseLoggingObject()
		{
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void EnterFunc(string funcname)
		{
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LeaveFunc(string funcname)
		{
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToConsole()
		{
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void PrintLine(string msg)
		{
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArray(bool shouldClose)
		{
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void DumpArrayToFile(bool shouldClose)
		{
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush()
		{
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Flush(bool close)
		{
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void LoggingMonitorTick()
		{
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer)
		{
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Dump(IntPtr pBuffer, int offset, int length)
		{
		}
	}
}
