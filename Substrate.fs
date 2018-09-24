module Substrate
open System.Drawing

type Config = {
    minCracks: int
    maxCracks: int
    backColour: Color
    crackColour: Color
} 

let defaultConfig = {
    minCracks = 0
    maxCracks = 200
    backColour = Color.White
    crackColour = Color.Black
}

let generate (width: int) height config =
    let bitmap = new Bitmap (width, height)
    use graphics = Graphics.FromImage bitmap
    let brush = new SolidBrush (config.backColour)
    graphics.FillRectangle (brush, 0, 0, width, height)

    bitmap