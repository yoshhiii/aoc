open System
open System.IO

let cardinalDirections = [ (-1, 0); (1, 0); (0, -1); (0, 1) ]
let ordinalDirections = [ (-1, -1); (1, -1); (-1, 1); (1, 1) ]

type Direction =
    | Cardinal
    | Ordinal
    | All

let search
    (grid: char[,])
    (keyword: string)
    (searchDir: Direction)
    (startRow: int)
    (startCol: int)
    (startChar: Nullable<char>)
    =
    let rows = grid.GetLength 0
    let cols = grid.GetLength 1
    let keywordLength = keyword.Length

    let directions =
        match searchDir with
        | Direction.Cardinal -> cardinalDirections
        | Direction.Ordinal -> ordinalDirections
        | Direction.All -> cardinalDirections @ ordinalDirections

    let isValid (row: int) (col: int) =
        row >= 0 && row < rows && col >= 0 && col < cols

    let searchDirection (row: int) (col: int) (dr: int) (dc: int) =
        let rec loop (r: int) (c: int) (index: int) =
            if index = keywordLength then
                true
            elif not (isValid r c) || grid.[r, c] <> keyword.[index] then
                false
            else
                loop (r + dr) (c + dc) (index + 1)

        loop row col 0

    let findXPattern (i: int) (j: int) =
        let window = Array2D.init 3 3 (fun x y -> grid.[i + x, j + y])
        let topLeft = window.[0, 0]
        let topRight = window.[0, 2]
        let bottomLeft = window.[2, 0]
        let bottomRight = window.[2, 2]

        let patterns =
            [ (topLeft, bottomRight, topRight, bottomLeft)
              (topLeft, bottomRight, bottomLeft, topRight)
              (bottomLeft, topRight, topLeft, bottomRight)
              (bottomLeft, topRight, bottomRight, topLeft) ]

        patterns
        |> List.exists (fun (tl, br, tr, bl) ->
            tl = 'M' && br = 'S' && tr = 'M' && bl = 'S'
            || tl = 'M' && br = 'S' && tr = 'S' && bl = 'M'
            || tl = 'S' && br = 'M' && tr = 'S' && bl = 'M'
            || tl = 'S' && br = 'M' && tr = 'M' && bl = 'S')

    let keywordCount =
        if startChar.HasValue then
            [ for i in 0 .. rows - 3 do
                  for j in 0 .. cols - 3 do
                      if grid.[i + 1, j + 1] = startChar.Value && findXPattern i j then
                          yield 1 ]
            |> List.sum
        else
            let visited = Array2D.create rows cols false
            let queue = System.Collections.Generic.Queue<(int * int)>()
            queue.Enqueue((startRow, startCol))
            visited.[startRow, startCol] <- true

            let rec bfs count =
                if queue.Count = 0 then
                    count
                else
                    let (currentRow, currentCol) = queue.Dequeue()

                    let newCount =
                        directions
                        |> List.fold
                            (fun acc (dr, dc) ->
                                if searchDirection currentRow currentCol dr dc then
                                    acc + 1
                                else
                                    acc)
                            count

                    directions
                    |> List.iter (fun (dr, dc) ->
                        let newRow = currentRow + dr
                        let newCol = currentCol + dc

                        if isValid newRow newCol && not visited.[newRow, newCol] then
                            queue.Enqueue((newRow, newCol))
                            visited.[newRow, newCol] <- true)

                    bfs newCount

            bfs 0

    keywordCount

let createGrid (data: array<string>) =
    let charArr = data |> Array.map (fun s -> s.ToCharArray())

    let rows = charArr.Length
    let cols = charArr.[0].Length
    Array2D.init rows cols (fun i j -> data.[i].[j])

let partOne (filePath: string) =
    let grid = File.ReadAllLines filePath |> createGrid

    let keyword = "XMAS"
    let count = search grid keyword Direction.All 0 0 (Nullable())
    printfn "%s found %d times" keyword count

let partTwo (filePath: string) =
    let grid = File.ReadAllLines filePath |> createGrid

    let keyword = "MAS"
    let count = search grid keyword Direction.Ordinal 0 0 (Nullable 'A')
    printfn "%s found %d times" keyword count

partOne "./input"
partTwo "./input"
