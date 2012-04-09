

@echo off
cd /d %~dp0

set zip="%cd%\Fizzler.zip"

del %zip%

xcopy /I /S bin\Release bin\Fizzler
cd bin
%Apps%\7-Zip\7z.exe a "%zip%" Fizzler\Fizzler*.dll Fizzler\HtmlAgilityPack.dll Fizzler\VisualFizzler.exe Fizzler\Metro\*.dll
cd ..
rmdir /S /Q bin\Fizzler


