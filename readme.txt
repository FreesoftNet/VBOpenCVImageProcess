******************************************************************************
*                          VBOpenCVImageProcess                              *
*                          Copyright(C) 2020 by フリーソフトネット           *
*                                   リリース Ver 0.1.0  2020/04/08           *
*                                                                            *
******************************************************************************

1. はじめに
    この度はVBOpenCVImageProcess をダウンロードしていただきありがとうございます。
  VBOpenCVImageProcess はOpenCVを活用した画像処理ソフトウェアです。
  ２値化、フィルタリング、テンプレートマッチングといった画像処理が簡単に行えます。
  
  本ソフトウェアはフリーソフトです。配布は自由に行ってください。
  著作権ついては特に考えていなくて自由に修正・配布していただいて
  結構ですが、無保証です。このソフトを使用したことによる損害には対応できません。
  Windows 10標準でインストールされているVisual Basic 2012のコンパイラを使用して
  自由に修正しながら機能追加できます。

2. 動作環境
  OS         Microsoft .NET Framework 4.5が動作するOS(動作確認はWindows 10で行っています)
  Microsoft .NET Framework 4.5以上
  OpenCvSharp-2.4.10-x86
  OpenCV 2.4.10(x86)

3. 構成ファイル一覧
  VBOpenCVImageProcess_master.zip      zipファイル
  解凍後
    readme.txt                         このファイル


4. インストール
  (1) 本体のインストール
      VBOpenCVImageProcess_master.zipをお好きなフォルダで展開してください。

  (2) OpenCVSharpの取得
      https://github.com/shimat/opencvsharp/releases
      より
      OpenCvSharp-2.4.10-x86-20170126.zip
      をダウンロードして展開してください。
      展開したフォルダより
      OpenCvSharp.dll
      OpenCvSharp.CPlusPlus.dll
      をVBOpenCVImageProcess のフォルダにコピーしてください。

  (3) https://ja.osdn.net/projects/sfnet_opencvlibrary/downloads/opencv-win/2.4.10/opencv-2.4.10.exe/
      よりダウンロードして実行してください。
      ダウンロード後にファイルを実行して展開したフォルダの
      opencv\build\x86\vc12\bin
      の中にあるdllをすべてVBOpenCVImageProcessのフォルダにコピーしてください。

  (4) VBOpenCVImageProcess.exeを実行して動作するか確認してみてください。

5. アンインストール
   展開したフォルダ毎削除してください。

6. 免責事項
   このプログラムを使用したことにより発生した障害については一切責任を負いかねます。
   ソフトウェアのバグを発見した場合、ご要望、機能拡張に関しては、メールにて
   受け付けております。
   メールアドレス

7. 変更履歴

2020/04/08 Ver 0.1.0   新規作成
