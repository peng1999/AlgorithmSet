// 在 http://fsharp.net 上了解有关 F# 的更多信息
// 请参阅“F# 教程”项目以获取更多帮助。
module Main

open _24Point

[<EntryPoint>]
let main argv = 
    printf "请输入几个数，以空格分隔："
    let answer = (System.Console.ReadLine()).Split([| ' ' |], System.StringSplitOptions.RemoveEmptyEntries)
                 |> List.ofArray
                 |> List.map double
                 |> solve
    if List.length answer > 0
    then answer |> List.map (printfn "%O = 24")
                |> ignore
    else printf "找不到解。"
    System.Console.ReadKey true |> ignore
    0 // 返回整数退出代码

