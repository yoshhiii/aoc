open System
open System.IO
open System.Collections.Generic

let cardinalDirections = [ (-1, 0); (1, 0); (0, -1); (0, 1) ]
let ordinalDirections = [ (-1, -1); (1, -1); (-1, 1); (1, 1) ]

type Direction =
    | Cardinal
    | Ordinal
    | All

let isValid (grid: char[,]) (row: int) (col: int) =
    row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1)

let search (grid: char[,]) (keyword: string) (searchDir: Direction) (startRow: int) (startCol: int) =
    let rows = grid.GetLength 0
    let cols = grid.GetLength 1
    let keywordLength = keyword.Length
    let mutable keywordCount = 0

    let directions = 
        match searchDir with
            | Direction.Cardinal -> cardinalDirections
            | Direction.Ordinal -> ordinalDirections
            | Direction.All -> cardinalDirections @ ordinalDirections

    let rec searchDirection (row: int) (col: int) (dr: int) (dc: int) (index: int) =
        if index = keywordLength then
            true
        elif not (isValid grid row col) || grid.[row, col] <> keyword.[index] then
            false
        else
            searchDirection (row + dr) (col + dc) dr dc (index + 1)

    let visited = Array2D.create rows cols false
    let queue = Queue<(int * int)>()

    queue.Enqueue((startRow, startCol))
    visited.[startRow, startCol] <- true

    while queue.Count > 0 do
        let (currentRow, currentCol) = queue.Dequeue()
        printfn "Visiting cell (%d, %d)" currentRow currentCol

        for (dr, dc) in directions do
            if searchDirection currentRow currentCol dr dc 0 then
                printfn
                    "Keyword '%s' found starting at (%d, %d) in direction (%d, %d)"
                    keyword
                    currentRow
                    currentCol
                    dr
                    dc

                keywordCount <- keywordCount + 1

            let newRow = currentRow + dr
            let newCol = currentCol + dc

            if isValid grid newRow newCol && not visited.[newRow, newCol] then
                visited.[newRow, newCol] <- true
                queue.Enqueue((newRow, newCol))

    keywordCount

let createGrid (data : array<string>) =
    let charArr =
        data 
        |> Array.map(fun s -> s.ToCharArray())
        
    let rows = charArr.Length
    let cols = charArr.[0].Length
    Array2D.init rows cols (fun i j -> data.[i].[j])

let partOne (filePath: string) =
    let grid = File.ReadAllLines filePath |> createGrid

    let keyword = "XMAS"
    let count = search grid keyword Direction.All 0 0
    printfn "%s found %d times" keyword count

let partTwo (filePath: string) = 
    let grid = File.ReadAllLines filePath |> createGrid

    let keyword = "MAS"
    let count = search grid keyword Direction.Ordinal 0 0
    printfn "%s found %d times" keyword count

// partOne "./input"
partTwo "./input_test"
