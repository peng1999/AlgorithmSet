// 在 http://fsharp.net 上了解有关 F# 的更多信息
// 请参阅“F# 教程”项目以获取更多帮助。
module Main

open _24Point

[<EntryPoint>]
let main argv = 
    solve [ 1.; 2.; 3.; 4. ]
    |>List.map (printfn "%O = 24")
    |>ignore
    System.Console.ReadKey true |> ignore
    0 // 返回整数退出代码
