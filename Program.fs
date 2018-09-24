
open Substrate
open System.Drawing

let width, height  = 900, 900
let backColour = Color.White

[<EntryPoint>]
let main _ =
    let bitmap = new Bitmap (width, height)
    use graphics = Graphics.FromImage bitmap
    let brush = new SolidBrush (backColour)
    graphics.FillRectangle (brush, 0, 0, width, height)

    bitmap.Save "./bin/result.bmp"
    0
