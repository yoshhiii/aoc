open System
open System.IO

let split_values (content: array<string>) =
    let left, right =
        content
        |> Array.map(fun s -> s.Split("\n"))
        |> Array.map(fun a -> 
            a |> Array.map(fun s ->
                s.Split("  ")
            )
        ) 
        |> Array.concat
        |> Array.fold(fun (left, right) s -> 
            let a = Int32.Parse(s[0])
            let b = Int32.Parse(s[1])
            (Array.append left [|a|], Array.append right [|b|])
        ) (Array.empty, Array.empty) 

    let left_sorted = Array.sort left
    let right_sorted = Array.sort right
    (left_sorted, right_sorted)

let partOne (filePath: string) =
    let content = 
        File.ReadAllLines filePath

    let left_sorted, right_sorted = split_values content

    let sum: int = 
        Array.zip left_sorted right_sorted
        |> Array.map(fun (a, b) -> abs(a - b))
        |> Array.sum
        
    printfn "%d" sum

let partTwo (filePath: string) = 
    let content = 
        File.ReadAllLines filePath

    let left_sorted, right_sorted = split_values content

    let sum = 
        left_sorted
        |> Array.map(fun value -> 
            let count = right_sorted |> Array.filter(fun x -> x = value) |> Array.length
            (value, count)
        )
        |> Array.map(fun (a, b) -> a * b)
        |> Array.sum

    printfn "%d" sum

// partOne "./input"
partTwo "./input"