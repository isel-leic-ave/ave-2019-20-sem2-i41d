%
% How to build
%
csc /t:library NBench.cs
csc /t:library Logger.cs
csc /t:library /r:Logger.dll LoggerDynamic.cs
csc /r:LoggerDynamic.dll /r:Logger.dll /r:NBench.dll App.cs