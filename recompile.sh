#!/bin/sh
clear

buildLog=`dotnet build`

echo $buildLog > build_log.txt

dotnet run /bin/Debug/net8.0/ConsoleApp1