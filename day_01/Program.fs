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
        |> Seq.toArray
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
    let size = input |> Seq.length
    let sizeMul = (size |> float) ** (2 |> float) |> int 
    let multiplexed = Array.create sizeMul 0
    [for i in [0 .. size - 1] do [for ii in [0 .. size - 1] do multiplexed.[i * size + ii] <- input.[i] + input.[ii]]] |> ignore
    let indexCombinations = [for i in [0 .. (size*size) - 1] -> (i/size ,i%size)]
    let valueCombinations =
        indexCombinations |> Seq.map (fun i -> (input.[fst i] + input.[snd i], (input.[fst i], input.[snd i])))
        |> Seq.filter (fun i -> fst i < 2020)
        |> Seq.map (fun i -> ((if cache.[2020 - fst i] + fst i = 2020 then true else false), cache.[2020 - fst i] * fst (snd i) * snd (snd i)))
        |> Seq.filter (fst)
   
        (*|> Seq.map checkFor2020
        |> Seq.filter bFst
        |> Seq.map mulTuple*)
    let r2 = snd (Seq.head valueCombinations)
    printf "\nResult: %i" r2
    0 // return an integer exit code