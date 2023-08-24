@echo off

call MC7D2D MicroSplatHelper.dll /reference:"%PATH_7D2D_MANAGED%\Assembly-CSharp.dll" ^
  -recurse:Source\*.cs && echo Successfully compiled MicroSplatHelper.dll

pause