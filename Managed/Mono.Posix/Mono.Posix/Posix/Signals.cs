using System;

namespace Mono.Posix
{
	// Token: 0x0200009B RID: 155
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.Signum")]
	public enum Signals
	{
		// Token: 0x0400051C RID: 1308
		SIGHUP,
		// Token: 0x0400051D RID: 1309
		SIGINT,
		// Token: 0x0400051E RID: 1310
		SIGQUIT,
		// Token: 0x0400051F RID: 1311
		SIGILL,
		// Token: 0x04000520 RID: 1312
		SIGTRAP,
		// Token: 0x04000521 RID: 1313
		SIGABRT,
		// Token: 0x04000522 RID: 1314
		SIGBUS,
		// Token: 0x04000523 RID: 1315
		SIGFPE,
		// Token: 0x04000524 RID: 1316
		SIGKILL,
		// Token: 0x04000525 RID: 1317
		SIGUSR1,
		// Token: 0x04000526 RID: 1318
		SIGSEGV,
		// Token: 0x04000527 RID: 1319
		SIGUSR2,
		// Token: 0x04000528 RID: 1320
		SIGPIPE,
		// Token: 0x04000529 RID: 1321
		SIGALRM,
		// Token: 0x0400052A RID: 1322
		SIGTERM,
		// Token: 0x0400052B RID: 1323
		SIGCHLD,
		// Token: 0x0400052C RID: 1324
		SIGCONT,
		// Token: 0x0400052D RID: 1325
		SIGSTOP,
		// Token: 0x0400052E RID: 1326
		SIGTSTP,
		// Token: 0x0400052F RID: 1327
		SIGTTIN,
		// Token: 0x04000530 RID: 1328
		SIGTTOU,
		// Token: 0x04000531 RID: 1329
		SIGURG,
		// Token: 0x04000532 RID: 1330
		SIGXCPU,
		// Token: 0x04000533 RID: 1331
		SIGXFSZ,
		// Token: 0x04000534 RID: 1332
		SIGVTALRM,
		// Token: 0x04000535 RID: 1333
		SIGPROF,
		// Token: 0x04000536 RID: 1334
		SIGWINCH,
		// Token: 0x04000537 RID: 1335
		SIGIO,
		// Token: 0x04000538 RID: 1336
		SIGSYS
	}
}
