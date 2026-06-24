# Resistance
Unity6 2Dヴァンサバライクゲーム

個人製作です。

開発環境<br>
・Unity 6000.0.33f1<br>
・C#<br>
・Visual Studio 2019<br>

使用した技術、デザインパターン<br>
・ObujectPool<br>
・Strategyパターン<br>
・Stateパターン<br>
・Coroutine<br>
・DOTween（一部）<br>
・ScriptableObject<br>

特に見てほしいスクリプトファイルは、Assetフォルダ内のScriptsフォルダにあるファイル名「LastBoss～」から始まるものです。<br>

工夫した点<br>
・最終ボスの移動範囲をPlayerを中心としたリング状の範囲にすることで、「近すぎず、遠すぎない」距離感を実現しました。<br>
・最終ボスの行動パターンを　待機状態　→　移動　→　攻撃　の単純なループにしつつ、攻撃を複数種類用意することで、<br>
　Playerを飽きさせない工夫をしました。<br>
・ScriptableObjectを併用してPlayerのステータスが動的に変わるレベルアップシステムを構築しました。<br>
