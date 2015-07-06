module _24Point

type Operator = string * (double -> double -> double)

type ExprTree = 
    | Op of Operator * ExprTree * ExprTree
    | Num of double
    override this.ToString() = 
        match this with
        | Op((f, _), el, er) -> sprintf "(%O %s %O)" el f er
        | Num n -> n.ToString()

let ops = 
    [ (("+",(+)), true)
      (("-",(-)), false)
      (("*",(*)), true)
      (("/",(/)), false) ]

let split canRev nums = 
    let step (splition:_ list) _ = 
        [ for ms, ns in splition do
          for item in ns do
          let restn, _ = ns |> List.partition ((<>) item)
          if (if List.length ms > 0 then item > List.head ms else true) then 
              yield item :: ms, restn ]
    
    let scaner = if not canRev then nums
                 else nums
                      |> Seq.take (List.length nums / 2)
                      |> Seq.toList
    
    //start:(double list * double list) list
    let start = [ ([], nums) ]
    List.scan step start scaner |> List.concat

let rec compute = 
    function 
    | Num a -> a
    | Op((_, op), a, b) -> op (compute a) (compute b)

//build:double list -> ExprTree list
let rec build = 
    function 
    | [ a ] -> [ Num a ]
    | nums -> 
        [ for op, canRev  in ops do
          let debug=split canRev nums
          for left, right in split canRev nums do
          let canYield=[ left; right ]
                       |> List.map List.length
                       |> List.forall ((<) 0)
          if canYield then 
              //let buildLeft  = build left
              //let buildRight = build right
              //for eachLeft, eachRight in (buildLeft, buildRight) ||> List.zip do
              for eachLeft in build left do
              for eachRight in build right do
              yield Op(op, eachLeft, eachRight) ]

let (.=) a b = abs (a - b) < 0.001
let solve = build >> List.filter (compute >> (.=) 24.)

