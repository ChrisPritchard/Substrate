
open Substrate

[<EntryPoint>]
let main argv =
    let out = match argv with [|outPath|] -> outPath | _ -> "./bin/result.bmp"
    let bitmap = generate 900 900 defaultConfig
    bitmap.Save out
    0
