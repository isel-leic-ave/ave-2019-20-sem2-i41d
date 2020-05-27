
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly App
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 02 00 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module App.exe
// MVID: {8F23DC0F-FB15-4621-A147-12CA7FE734EF}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x0B970000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit Steve
       extends Bob
{
  .method public hidebysig virtual instance void 
          f() cil managed
  {
    // Code size       11 (0xb)
    .maxstack  8
    IL_0000:  ldstr      "I am Steve"
    IL_0005:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000a:  ret
  } // end of method Steve::f

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void Bob::.ctor()
    IL_0006:  ret
  } // end of method Steve::.ctor

} // end of class Steve

.class public auto ansi beforefieldinit Bob
       extends [mscorlib]System.Object
{
  .method public hidebysig newslot virtual 
          instance void  f() cil managed
  {
    // Code size       22 (0x16)
    .maxstack  8
    IL_0000:  ldstr      "I am Bob"
    IL_0005:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000a:  ldarg.0
    IL_000b:  call       instance int32 [mscorlib]System.Object::GetHashCode()
    IL_0010:  call       void [mscorlib]System.Console::WriteLine(int32)
    IL_0015:  ret
  } // end of method Bob::f

  .method public hidebysig static void  UseBob(int32 n,
                                               class Bob b) cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.1
    IL_0001:  callvirt   instance void Bob::f()
    IL_0006:  ret
  } // end of method Bob::UseBob

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method Bob::.ctor

} // end of class Bob

.class private auto ansi beforefieldinit App
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       17 (0x11)
    .maxstack  8
    IL_0000:  newobj     instance void Bob::.ctor()
    IL_0005:  callvirt   instance void Bob::f()
    IL_000a:  ldnull
    IL_000b:  call       instance void Bob::f()
    IL_0010:  ret
  } // end of method App::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  ret
  } // end of method App::.ctor

} // end of class App


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file D:\ISEL\pg5 ave - 2019-2020 - 2º sem\aulas\aula33-call-callvirt\App.res
