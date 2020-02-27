Follow the procedure to build and run:

 1. Build the `Point.dll` component: `cl /clr /LD Point.cpp`
 2. Build the `App.exe` program, which depends of `Point.dll` as follows: `csc /r:Point.dll /platform:x86 App.cs`
 3. Run the resulting `App.exe` program: `App`