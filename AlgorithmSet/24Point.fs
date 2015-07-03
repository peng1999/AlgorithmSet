module _24Point

type ExprTree = 
    | Op of (double -> double -> double) * ExprTree * ExprTree
    | Num of double
    override this.ToString() = 
        match this with
        | Op(f, el, er) -> sprintf "(%A %A %A)" el f er
        | Num n -> n.ToString()

let ops = 
    [ ((+), true)
      ((-), false)
      ((*), true)
      ((/), false) ]

let split canRev nums = 
    let step splition _ = 
        [ for ms, ns in splition do
          for item in ns do
          let restn = ns |> List.partition ((<>) item)
          if List.length ms > 0 && item > List.head ms then
              yield item :: ms, ns ]
    
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
    | Op(op, a, b) -> op (compute a) (compute b)

//build:double list -> ExprTree list
let rec build = 
    function 
    | [ a ] -> [ Num a ]
    | nums -> 
        [ for op, canRev            in ops do
          for left, right           in split canRev nums do
          let buildLeft             =  build left
          let buildRight            =  build right
          for eachLeft, eachRight   in (buildLeft, buildRight) ||> List.zip do
          yield Op(op, eachLeft, eachRight) ]

let (.=) a b = abs (a - b) < 0.001
let solve = build >> List.filter (compute >> (.=) 24.)
