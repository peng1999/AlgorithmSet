module _24Point

type ExprTree =
    | Op of (double -> double -> double) * ExprTree * ExprTree
    | Num of double
    
let ops =[((+),true)
          ((-),false)
          ((*),true)
          ((/),false)]

let split canRev nums =
    raise <| System.Exception()

let rec compute = 
    function
    | Num a -> a
    | Op (op, a, b) -> op (compute a) (compute b)
    
//build:double list -> ExprTree list
let rec build =
    function
    | [a] -> [Num a]
    | nums -> 
    [
        for op, canRev in ops do
        for left, right in split canRev nums do
        let buildLeft = build left
        let buildRight = build right
        for eachLeft, eachRight in (buildLeft, buildRight) ||> List.zip do
        yield Op(op, eachLeft, eachRight)
    ]

let (.=) a b = abs (a - b) < 0.001

let solve =
    build >> List.filter (compute >> (.=) 24.)

