
open Substrate
open System.Drawing

let width, height  = 900, 900
let backColour = Color.White
let foreColour = Color.Black

let iterations = 1000

[<EntryPoint>]
let main _ =
    let bitmap = new Bitmap (width, height)
    use graphics = Graphics.FromImage bitmap
    let brush = new SolidBrush (backColour)
    graphics.FillRectangle (brush, 0, 0, width, height)

    let world = initWorld width height
    let finalWorld = 
        [1..iterations] |> Seq.fold (fun world _ -> advanceWorld world) world
    
    let brush = new SolidBrush (foreColour)
    finalWorld.grid |> Map.iter (fun (x, y) _ -> graphics.FillRectangle(brush, x, y, 1, 1))

    bitmap.Save "./bin/result.bmp"
    0
