vbc /target:library /platform:x86 VBOpenCV.vb /reference:OpenCvSharp.dll /reference:OpenCvSharp.CPlusPlus.dll /reference:OpenCvSharp.Extensions.dll
vbc /target:winexe /platform:x86  /out:VBOpenCVImageProcess.exe /main:frmMain  frmMain.vb frmMain.Designer.vb frmSet.vb frmSet.Designer.vb frmOutput.vb frmOutput.designer.vb frmAbout.vb  frmAbout.Designer.vb AssemblyInfo.vb  /reference:VBOpenCV.dll
VBOpenCVImageProcess.exe
pause
