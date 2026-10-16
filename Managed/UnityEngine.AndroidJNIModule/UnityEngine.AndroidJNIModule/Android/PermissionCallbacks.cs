using System;
using System.Diagnostics;

namespace UnityEngine.Android
{
	// Token: 0x0200002B RID: 43
	public class PermissionCallbacks : AndroidJavaProxy
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x0000ABAC File Offset: 0x00008DAC
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> PermissionGranted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x0000AC1C File Offset: 0x00008E1C
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x0000AC54 File Offset: 0x00008E54
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> PermissionDenied;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002AA RID: 682 RVA: 0x0000AC8C File Offset: 0x00008E8C
		// (remove) Token: 0x060002AB RID: 683 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		[Obsolete("Unreliable. Query ShouldShowRequestPermissionRationale and use PermissionDenied event.", false)]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> PermissionDeniedAndDontAskAgain;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002AC RID: 684 RVA: 0x0000ACFC File Offset: 0x00008EFC
		// (remove) Token: 0x060002AD RID: 685 RVA: 0x0000AD34 File Offset: 0x00008F34
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> PermissionRequestDismissed;

		// Token: 0x060002AE RID: 686 RVA: 0x0000AD69 File Offset: 0x00008F69
		public PermissionCallbacks()
			: base("com.unity3d.player.IPermissionRequestCallbacks")
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AD78 File Offset: 0x00008F78
		public override IntPtr Invoke(string methodName, IntPtr javaArgs)
		{
			IntPtr intPtr;
			if (!(methodName == "onPermissionResult"))
			{
				intPtr = base.Invoke(methodName, javaArgs);
			}
			else
			{
				this.onPermissionResult(javaArgs);
				intPtr = IntPtr.Zero;
			}
			return intPtr;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		private void onPermissionResult(IntPtr javaArgs)
		{
			IntPtr objectArrayElement = AndroidJNISafe.GetObjectArrayElement(javaArgs, 0);
			int[] array = AndroidJNISafe.FromIntArray(AndroidJNISafe.GetObjectArrayElement(javaArgs, 1));
			int i = 0;
			while (i < array.Length)
			{
				string stringChars = AndroidJNISafe.GetStringChars(AndroidJNISafe.GetObjectArrayElement(objectArrayElement, i));
				switch (array[i])
				{
				case 0:
				{
					bool flag = this.PermissionRequestDismissed == null;
					if (flag)
					{
						goto IL_00A2;
					}
					this.PermissionRequestDismissed(stringChars);
					break;
				}
				case 1:
				{
					Action<string> permissionGranted = this.PermissionGranted;
					if (permissionGranted != null)
					{
						permissionGranted(stringChars);
					}
					break;
				}
				case 2:
					goto IL_00A2;
				case 3:
				{
					bool flag2 = this.PermissionDeniedAndDontAskAgain == null;
					if (flag2)
					{
						goto IL_00A2;
					}
					this.PermissionDeniedAndDontAskAgain(stringChars);
					break;
				}
				}
				IL_00B7:
				i++;
				continue;
				IL_00A2:
				Action<string> permissionDenied = this.PermissionDenied;
				if (permissionDenied != null)
				{
					permissionDenied(stringChars);
				}
				goto IL_00B7;
			}
		}

		// Token: 0x0200002C RID: 44
		private enum Result
		{
			// Token: 0x0400008F RID: 143
			Dismissed,
			// Token: 0x04000090 RID: 144
			Granted,
			// Token: 0x04000091 RID: 145
			Denied,
			// Token: 0x04000092 RID: 146
			DeniedDontAskAgain
		}
	}
}
