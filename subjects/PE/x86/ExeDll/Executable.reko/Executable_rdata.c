// Executable_rdata.c
// Generated by decompiling Executable.exe
// using Reko decompiler version 0.9.4.0.

#include "Executable.h"

<anonymous> * __imp__?slow_and_safe_increment@@YAHH@Z = &g_t2712; // 00402000
<anonymous> * __imp__?exported_critical_section@@3U_RTL_CRITICAL_SECTION@@A = &g_t26C2; // 00402004
<anonymous> * __imp__?exported_int@@3HA = &g_t26FC; // 00402008
<anonymous> * __imp__InitializeCriticalSection = &g_t2698; // 00402010
<anonymous> * __imp__IsDebuggerPresent = &g_t2B06; // 00402014
<anonymous> * __imp__InitializeSListHead = &g_t2AF0; // 00402018
<anonymous> * __imp__GetSystemTimeAsFileTime = &g_t2AD6; // 0040201C
<anonymous> * __imp__GetCurrentThreadId = &g_t2AC0; // 00402020
<anonymous> * __imp__GetCurrentProcessId = &g_t2AAA; // 00402024
<anonymous> * __imp__QueryPerformanceCounter = &g_t2A90; // 00402028
<anonymous> * __imp__IsProcessorFeaturePresent = &g_t2A74; // 0040202C
<anonymous> * __imp__TerminateProcess = &g_t2A60; // 00402030
<anonymous> * __imp__GetCurrentProcess = &g_t2A4C; // 00402034
<anonymous> * __imp__SetUnhandledExceptionFilter = &g_t2A2E; // 00402038
<anonymous> * __imp__UnhandledExceptionFilter = &g_t2A12; // 0040203C
<anonymous> * __imp__GetModuleHandleW = &g_t2B1A; // 00402040
<anonymous> * __imp__memset = &g_t274A; // 00402048
<anonymous> * __imp___except_handler4_common = &g_t2754; // 0040204C
<anonymous> * __imp___set_new_mode = &g_t28EC; // 00402054
<anonymous> * __imp___configthreadlocale = &g_t28D6; // 0040205C
<anonymous> * __imp____setusermatherr = &g_t27CE; // 00402064
<anonymous> * __imp___c_exit = &g_t289E; // 0040206C
<anonymous> * __imp___cexit = &g_t2894; // 00402070
<anonymous> * __imp___initialize_onexit_table = &g_t290C; // 00402074
<anonymous> * __imp___register_onexit_function = &g_t2928; // 00402078
<anonymous> * __imp___initterm = &g_t2840; // 0040207C
<anonymous> * __imp____p___argv = &g_t2886; // 00402080
<anonymous> * __imp___controlfp_s = &g_t2952; // 00402084
<anonymous> * __imp__terminate = &g_t2962; // 00402088
<anonymous> * __imp____p___argc = &g_t2878; // 0040208C
<anonymous> * __imp___exit = &g_t2862; // 00402090
<anonymous> * __imp__exit = &g_t285A; // 00402094
<anonymous> * __imp___get_initial_narrow_environment = &g_t281E; // 00402098
<anonymous> * __imp___initialize_narrow_environment = &g_t27FC; // 0040209C
<anonymous> * __imp___configure_narrow_argv = &g_t27E2; // 004020A0
<anonymous> * __imp___register_thread_local_exe_atexit_callback = &g_t28A8; // 004020A4
<anonymous> * __imp___set_app_type = &g_t27BE; // 004020A8
<anonymous> * __imp___seh_filter_exe = &g_t27AC; // 004020AC
<anonymous> * __imp___crt_atexit = &g_t2944; // 004020B0
<anonymous> * __imp___initterm_e = &g_t284C; // 004020B4
<anonymous> * __imp____p__commode = &g_t28FC; // 004020BC
<anonymous> * __imp___set_fmode = &g_t286A; // 004020C0
<anonymous> * __imp____stdio_common_vfprintf = &g_t2792; // 004020C4
<anonymous> * __imp____acrt_iob_func = &g_t2780; // 004020C8
<anonymous> * g_ptr4020D0 = fn00401BA4; // 004020D0
Eq_n g_t4020D4 = null; // 004020D4
Eq_n g_t4020DC = null; // 004020DC
Eq_n g_t4020E0 = null; // 004020E0
Eq_n g_t4020EC = null; // 004020EC
word32 g_a4024C8[1] = // 004024C8
	{
		0x00,
	};
word32 g_dw4025C8 = 10002; // 004025C8
word32 g_dw4025CC = 9922; // 004025CC
word32 g_dw4025D0 = 9980; // 004025D0
word32 g_dw4025D8 = 9880; // 004025D8
word32 g_dw4025DC = 11014; // 004025DC
word32 g_dw4025E0 = 0x2AF0; // 004025E0
word32 g_dw4025E4 = 10966; // 004025E4
word32 g_dw4025E8 = 0x2AC0; // 004025E8
word32 g_dw4025EC = 0x2AAA; // 004025EC
word32 g_dw4025F0 = 0x2A90; // 004025F0
word32 g_dw4025F4 = 10868; // 004025F4
word32 g_dw4025F8 = 0x2A60; // 004025F8
word32 g_dw4025FC = 10828; // 004025FC
word32 g_dw402600 = 0x2A2E; // 00402600
word32 g_dw402604 = 10770; // 00402604
word32 g_dw402608 = 11034; // 00402608
word32 g_dw402610 = 10058; // 00402610
word32 g_dw402614 = 10068; // 00402614
word32 g_dw40261C = 0x28EC; // 0040261C
word32 g_dw402624 = 10454; // 00402624
word32 g_dw40262C = 10190; // 0040262C
word32 g_dw402634 = 0x289E; // 00402634
word32 g_dw402638 = 10388; // 00402638
word32 g_dw40263C = 0x290C; // 0040263C
word32 g_dw402640 = 0x2928; // 00402640
word32 g_dw402644 = 0x2840; // 00402644
word32 g_dw402648 = 0x2886; // 00402648
word32 g_dw40264C = 0x2952; // 0040264C
word32 g_dw402650 = 0x2962; // 00402650
word32 g_dw402654 = 0x2878; // 00402654
word32 g_dw402658 = 0x2862; // 00402658
word32 g_dw40265C = 10330; // 0040265C
word32 g_dw402660 = 10270; // 00402660
word32 g_dw402664 = 0x27FC; // 00402664
word32 g_dw402668 = 10210; // 00402668
word32 g_dw40266C = 0x28A8; // 0040266C
word32 g_dw402670 = 10174; // 00402670
word32 g_dw402674 = 10156; // 00402674
word32 g_dw402678 = 0x2944; // 00402678
word32 g_dw40267C = 10316; // 0040267C
word32 g_dw402684 = 0x28FC; // 00402684
word32 g_dw402688 = 0x286A; // 00402688
word32 g_dw40268C = 10130; // 0040268C
word32 g_dw402690 = 10112; // 00402690
