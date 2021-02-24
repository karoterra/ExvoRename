﻿# ExvoRename

ExvoRenameは[A.I.VOICE](https://aivoice.jp/)付属のexVOICEをリネームするためのツールです。

動作には64bit版のWindows 7以降、及び .NET Framework 4.7.2以上のランタイムが必要です。

## 注意事項

ExvoRenameは無保証で提供されます。
ExvoRenameを使用したこと及び使用しなかったことによるいかなる損害について、開発者は何も保証しません。

## ダウンロード

Releaseページより`ExvoRename_xxx.zip`をダウンロードしてください。

## 使い方

1. `ExvoRename.exe`を実行します。
2. `場所`にリネームしたいexVOICEがあるフォルダを指定してください。
   - ここで指定するフォルダは`exVOICE_xxx収録ボイス一覧.pdf`があるフォルダです。
   - 直接テキストボックスに入力するか、`参照`ボタンを押してダイアログから選択してください。
3. `種類`のドロップダウンメニューからリネームしたいexVOICEの種類を選択してください。
4. `番号だけ`ボタンあるいは`番号とセリフ`ボタンを押すとwavファイルがリネームされます。
   - `番号だけ`：`<番号>.wav`
   - `番号とセリフ`：`<番号>_<セリフ>.wav`

現在以下のexVOICEに対応しています。
- 琴葉茜
- 琴葉葵
- 伊織弓鶴

## 開発環境

- Windows 10 Home 64 bit
- Visual Studio Community 2019

## 更新履歴

更新履歴はCHANGELOGを参照してください。

## ライセンス

このソフトウェアは、MITライセンスのもとで公開されます。詳細はLICENSEを参照してください。

## クレジット

このツールは以下のライブラリを使用しています。
- [Windows-API-Code-Pack-1.1](https://github.com/contre/Windows-API-Code-Pack-1.1)

これらのライブラリのライセンスはcopying以下を参照してください。
