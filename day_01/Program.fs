open System

let from whom =
    sprintf "from %s" whom

let readLines filePath = System.IO.File.ReadLines(filePath)

[<EntryPoint>]
let main argv =
    let keepFull s = String.IsNullOrWhiteSpace(s) = false
    let input = 
        readLines "input.txt"
        |> Seq.filter keepFull
        |> Seq.map int
    let cache = Array.create 2020 0
    [ for s in input do cache.[s] <- s] |> ignore
    let checkFor2020 value =
        ((if cache.[2020 - value] + value = 2020 then true else false), (value, cache.[2020 - value]))
    let bFst v = fst v
    let mulTuple (_, (a, b)) = a * b

    let results =
        input |> Seq.map checkFor2020
        |> Seq.filter bFst
        |> Seq.map mulTuple
    let r = Seq.head results
    printf "Result: %i" r
    0 // return an integer exit code