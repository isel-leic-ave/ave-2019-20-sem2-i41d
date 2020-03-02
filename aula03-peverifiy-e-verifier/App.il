
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern Point
{
  .ver 0:0:0:0
}
.assembly App
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module App.exe
// MVID: {B0BAAD84-4F81-4E07-80F8-FF539E3204FF}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x05020000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit App
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       98 (0x62)
    .maxstack  2
    .locals init (class [Point]Point V_0)
    IL_0000:  nop
    IL_0001:  ldc.i4.3
    IL_0002:  ldc.i4.7
    IL_0003:  newobj     instance void [Point]Point::.ctor(int32,
                                                           int32)
    IL_0008:  stloc.0
    IL_0009:  ldloc.0
    IL_000a:  callvirt   instance void [Point]Point::Print()
    IL_000f:  nop
    IL_0010:  ldstr      "x: "
    IL_0015:  ldloc.0
    IL_0016:  ldflda     int32 [Point]Point::x
    IL_001b:  call       instance string [mscorlib]System.Int32::ToString()
    IL_0020:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0025:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_002a:  nop
    IL_002b:  ldstr      "y: "
    IL_0030:  ldloc.0
    IL_0031:  ldflda     int32 [Point]Point::y
    IL_0036:  call       instance string [mscorlib]System.Int32::ToString()
    IL_003b:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0040:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0045:  nop
    IL_0046:  ldstr      "z: "
    IL_004b:  ldloc.0
    IL_004c:  ldflda     int32 [Point]Point::z
    IL_0051:  call       instance string [mscorlib]System.Int32::ToString()
    IL_0056:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_005b:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_0060:  nop
    IL_0061:  ret
  } // end of method App::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method App::.ctor

} // end of class App


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file D:\ISEL\pg5 ave - 2019-2020 - 2º sem\aulas\aula03-verifier\App.res
