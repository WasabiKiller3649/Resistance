# Resistance
Unity6 2Dヴァンサバライクゲーム

個人製作です。

開発環境
・Unity 6000.0.33f1
・C#
・Visual Studio 2019

使用した技術、デザインパターン
・ObujectPool
・Strategyパターン
・Stateパターン
・Coroutine
・DOTween（一部）
・ScriptableObject

特に見てほしいスクリプトファイルは、Assetフォルダ内のScriptsフォルダにあるファイル名「LastBoss～」から始まるものです。

工夫した点
・最終ボスの移動範囲をPlayerを中心としたリング状の範囲にすることで、「近すぎず、遠すぎない」距離感を実現しました。
・最終ボスの行動パターンを　待機状態　→　移動　→　攻撃　の単純なループにしつつ、攻撃を複数種類用意することで、
　Playerを飽きさせない工夫をしました。
・ScriptableObjectを併用してPlayerのステータスが動的に変わるレベルアップシステムを構築しました。
