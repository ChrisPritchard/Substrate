
open Substrate
open GameCore.GameModel
open GameCore.GameLoop
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

let width, height  = 450, 450
let backColour = Color.White
let foreColour = Color.Black

[<EntryPoint>]
let main _ =

    let resolution = Windowed (width, height)
    let updateModel runState world =
        match world with
        | None -> Some <| initWorld width height
        | _ when wasJustPressed Keys.Escape runState -> None
        | Some world -> Some <| advanceWorld world

    let getView _ world =
        [
            yield Colour ((0, 0, width, height), backColour)
            yield! world.grid |> Map.toList |> List.map (fun ((x, y), _) -> 
                Colour ((x, y, 1, 1), foreColour))
        ]

    use game = new GameLoop<World>(resolution, [], updateModel, getView, None)
    game.Run()
    0