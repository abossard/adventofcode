open System

let from whom =
    sprintf "from %s" whom

let readLines filePath = System.IO.File.ReadLines(filePath)

[<EntryPoint>]
let main argv =
    let input = readLines "day_00/input.txt"
    printfn "Hello world" 
    0 // return an integer exit code