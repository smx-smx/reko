// pySample_rdata.c
// Generated by decompiling pySample.dll
// using Reko decompiler version 0.9.4.0.

#include "pySample.h"

<anonymous> * __imp__GetSystemTimeAsFileTime = &g_t25CC; // 10002000
<anonymous> * __imp__GetCurrentProcessId = &g_t25B6; // 10002004
<anonymous> * __imp__GetCurrentThreadId = &g_t25A0; // 10002008
<anonymous> * __imp__GetTickCount = &g_t2590; // 1000200C
<anonymous> * __imp__DisableThreadLibraryCalls = &g_t255A; // 10002010
<anonymous> * __imp__IsDebuggerPresent = &g_t2546; // 10002014
<anonymous> * __imp__SetUnhandledExceptionFilter = &g_t2528; // 10002018
<anonymous> * __imp__UnhandledExceptionFilter = &g_t250C; // 1000201C
<anonymous> * __imp__GetCurrentProcess = &g_t24F8; // 10002020
<anonymous> * __imp__TerminateProcess = &g_t24E4; // 10002024
<anonymous> * __imp__InterlockedCompareExchange = &g_t24C6; // 10002028
<anonymous> * __imp__Sleep = &g_t24BE; // 1000202C
<anonymous> * __imp__InterlockedExchange = &g_t24A8; // 10002030
<anonymous> * __imp__QueryPerformanceCounter = &g_t2576; // 10002034
<anonymous> * __imp___crt_debugger_hook = &g_t241E; // 1000203C
<anonymous> * __imp____CppXcptFilter = &g_t240C; // 10002040
<anonymous> * __imp___unlock = &g_t2458; // 10002044
<anonymous> * __imp____dllonexit = &g_t2462; // 10002048
<anonymous> * __imp___lock = &g_t2470; // 1000204C
<anonymous> * __imp___onexit = &g_t2478; // 10002050
<anonymous> * __imp___except_handler4_common = &g_t248E; // 10002054
<anonymous> * __imp___adjust_fdiv = &g_t23FC; // 10002058
<anonymous> * __imp___amsg_exit = &g_t23EE; // 1000205C
<anonymous> * __imp___initterm_e = &g_t23E0; // 10002060
<anonymous> * __imp___initterm = &g_t23D4; // 10002064
<anonymous> * __imp___decode_pointer = &g_t23C2; // 10002068
<anonymous> * __imp__free = &g_t23BA; // 1000206C
<anonymous> * __imp___encoded_null = &g_t23AA; // 10002070
<anonymous> * __imp___malloc_crt = &g_t239C; // 10002074
<anonymous> * __imp___encode_pointer = &g_t238A; // 10002078
<anonymous> * __imp____clean_type_info_names_internal = &g_t2434; // 1000207C
<anonymous> * __imp__Py_InitModule4 = &g_t236A; // 10002084
<anonymous> * __imp__PyArg_ParseTuple = &g_t2356; // 10002088
<anonymous> * __imp___Py_NoneStruct = &g_t2344; // 1000208C
<anonymous> * __imp__Py_BuildValue = &g_t2334; // 10002090
Eq_n g_t10002098 = null; // 10002098
Eq_n g_t1000209C = null; // 1000209C
Eq_n g_t100020A0 = null; // 100020A0
Eq_n g_t100020A8 = null; // 100020A8
word32 g_dw100020CC = 0x00; // 100020CC
char g_str100020E0[] = "fdiv(a, b) = a / b"; // 100020E0
char g_str100020F4[] = "fdiv"; // 100020F4
char g_str100020FC[] = "div(a, b) = a / b"; // 100020FC
char g_str10002110[] = "div"; // 10002110
char g_str10002114[] = "dif(a, b) = a - b"; // 10002114
char g_str10002128[] = "dif"; // 10002128
char g_str1000212C[] = "sum(a, b) = a + b"; // 1000212C
char g_str10002140[] = "sum"; // 10002140
char g_str10002144[] = "ii:sum"; // 10002144
char g_str1000214C[] = "i"; // 1000214C
char g_str10002150[] = "ii:dif"; // 10002150
char g_str10002158[] = "ii:div"; // 10002158
char g_str10002160[] = "ff:fdiv"; // 10002160
char g_str10002168[] = "f"; // 10002168
char g_str1000216C[] = ":unused"; // 1000216C
char g_str10002174[] = "pySample"; // 10002174
word32 g_a100021D8[1] = // 100021D8
	{
		0x00,
	};
word32 g_dw1000229C = 0x25CC; // 1000229C
word32 g_dw100022A0 = 0x25B6; // 100022A0
word32 g_dw100022A4 = 0x25A0; // 100022A4
word32 g_dw100022A8 = 9616; // 100022A8
word32 g_dw100022AC = 0x255A; // 100022AC
word32 g_dw100022B0 = 0x2546; // 100022B0
word32 g_dw100022B4 = 0x2528; // 100022B4
word32 g_dw100022B8 = 9484; // 100022B8
word32 g_dw100022BC = 9464; // 100022BC
word32 g_dw100022C0 = 9444; // 100022C0
word32 g_dw100022C4 = 9414; // 100022C4
word32 g_dw100022C8 = 0x24BE; // 100022C8
word32 g_dw100022CC = 0x24A8; // 100022CC
word32 g_dw100022D0 = 9590; // 100022D0
word32 g_dw100022D8 = 0x241E; // 100022D8
word32 g_dw100022DC = 9228; // 100022DC
word32 g_dw100022E0 = 0x2458; // 100022E0
word32 g_dw100022E4 = 0x2462; // 100022E4
word32 g_dw100022E8 = 0x2470; // 100022E8
word32 g_dw100022EC = 9336; // 100022EC
word32 g_dw100022F0 = 0x248E; // 100022F0
word32 g_dw100022F4 = 9212; // 100022F4
word32 g_dw100022F8 = 0x23EE; // 100022F8
word32 g_dw100022FC = 0x23E0; // 100022FC
word32 g_dw10002300 = 0x23D4; // 10002300
word32 g_dw10002304 = 0x23C2; // 10002304
word32 g_dw10002308 = 0x23BA; // 10002308
word32 g_dw1000230C = 0x23AA; // 1000230C
word32 g_dw10002310 = 9116; // 10002310
word32 g_dw10002314 = 9098; // 10002314
word32 g_dw10002318 = 0x2434; // 10002318
word32 g_dw10002320 = 9066; // 10002320
word32 g_dw10002324 = 0x2356; // 10002324
word32 g_dw10002328 = 0x2344; // 10002328
word32 g_dw1000232C = 0x2334; // 1000232C
