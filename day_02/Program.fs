// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Text.RegularExpressions

// Define a function to construct a message to print

let readLines filePath = System.IO.File.ReadLines(filePath)
let keepFull s = String.IsNullOrWhiteSpace(s) = false

type Policy = { Min: int; Max: int; Letter: char }
type Entry = { Policy: Policy; Password: string }

let parseEntry inputStr =
    let pattern = "(?<Min>\d+)-(?<Max>\d+) (?<Letter>\D+): (?<Password>.*)"
    let matched = Regex.Match(inputStr, pattern)
    {
        Policy = {
            Min = int matched.Groups.["Min"].Value
            Max = int matched.Groups.["Max"].Value
            Letter = char matched.Groups.["Letter"].Value
        }
        Password = matched.Groups.["Password"].Value
    }    

let letterFrequency (str: string) =
    str.ToCharArray()
    |> Seq.countBy id
    |> Seq.sort
    
let mapResults entry =
    (letterFrequency entry.Password, entry)

let adheresToRule entry =
    let policy = (snd entry).Policy
    let findEntry list =
        list
        |> Seq.filter (fun x -> fst x = (snd entry).Policy.Letter)
    let count = defaultArg ((fst entry) |> findEntry |> Seq.map snd |> Seq.tryHead) -1
    (count >= policy.Min && count <= policy.Max, snd entry)

[<EntryPoint>]
let main argv =
    let lines =
        readLines "input.txt"
        |> Seq.filter keepFull
        |> Seq.map parseEntry
        |> Seq.map mapResults
        |> Seq.map adheresToRule
        |> Seq.filter fst
    printf "Trues: %i" (lines |> Seq.length)
    0
