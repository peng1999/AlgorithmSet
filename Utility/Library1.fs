module Utility

let transformIntoColumns colNum list = 
    let rec go n row acc = 
        function 
        | [] -> row :: acc
        | a :: xs when colNum = n   -> go 0         [a]          (row :: acc)  xs
        | a :: xs                   -> go (n + 1)   (a :: row)  acc          xs
    go 0 [] [] list
