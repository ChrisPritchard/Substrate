module Substrate

type World = {
    size: int * int
    grid: Map<int * int, int>
    cracks: Crack list
} and Crack = {
    x: float
    y: float
    t: float
}

let private random = new System.Random (1)

let crackJiggle () = (random.NextDouble() * 4.1) - 2.1
let PI = System.Math.PI
let PID = PI/180.

let maxCracks = 200

let z = 0.33
let fuzz n = n + (random.NextDouble() * (z*2.)) - z |> int

let makeCrack grid =
    let index = random.Next <| Map.count grid
    let (x, y), t = grid |> Map.toSeq |> Seq.item index

    let T = 
        if random.NextDouble() > 0.5 
        then float t + 90. + crackJiggle ()
        else float t - 90. + crackJiggle ()
    let X = float x + 0.61 * cos (T*PID)
    let Y = float y + 0.61 * sin (T*PID)

    { x = X; y = Y; t = T % 360. }

let makeCracks count current grid = 
    match List.length current with
    | n when n >= maxCracks -> []
    | _ -> 
        [1..count] |> List.map (fun _ -> makeCrack grid)

let moveCrack grid maxX maxY crack = 
    let X = crack.x + 0.42 * cos (crack.t*PID)
    let Y = crack.y + 0.42 * sin (crack.t*PID)

    let x, y = fuzz X, fuzz Y
    
    if x < 0 || y < 0 || x >= maxX || y >= maxY then 
        grid, None
    else if grid |> Map.containsKey (x, y) |> not then 
        Map.add (x, y) (int crack.t) grid, Some { crack with x = X; y = Y }
    else
        let current = float grid.[x, y]
        if abs (current - crack.t) < 5. then 
            Map.add (x, y) (int crack.t) grid, Some { crack with x = X; y = Y }
        //else if abs (current - crack.t) > 2. then grid, makeCrack grid
        else 
            grid, None

let initWorld width height cracks = 
    let initialGrid = 
        [1..pown cracks 3] 
        |> List.map (fun _ -> 
            (random.Next width, random.Next height), random.Next 360) 
        |> Map.ofList
    {
        size = width, height
        grid = initialGrid
        cracks = makeCracks cracks [] initialGrid
    }

let advanceWorld crackIncreaseRate world =
    let maxX, maxY = world.size
    let nextGrid, nextCracks = 
        world.cracks 
        |> Seq.fold (fun (grid, cracks) crack -> 
            let nextGrid, newCrack = moveCrack grid maxX maxY crack
            match newCrack with
            | None -> nextGrid, (makeCracks crackIncreaseRate world.cracks grid) @ cracks
            | Some c -> nextGrid, c::cracks) (world.grid, [])
    { world with 
        grid = nextGrid
        cracks = nextCracks }