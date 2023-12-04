module Main

open FStar.IO
open FStar
open FStar.String
open FStar.Char
open FStar.List
open FStar.Int
open FStar.Exn
open FStar.All
open FStar.Printf
open Prims
open Utils

// part1 


type rgb = {
  red: int;
  green: int;
  blue: int;
  }

let defRgb = {red= 0; green= 0; blue= 0;}
let mkRgb r g b = {red = r; green = g; blue = b;}
let printRgb (g:rgb) = sprintf "rgb %d %d %d " g.red g.green g.blue

let parse1 (c: rgb) (str:string) : ML rgb =
  match String.split [ ' ' ] str |> map trim with 
  | [ head ; "green" ] -> 
    let updated = max (int_of_string head) c.green in
    { c with green = updated  }
  | [ head ; "red" ] -> 
    let updated = max (int_of_string head) c.red in
    { c with red = updated  }
  | [ head ; "blue" ] -> 
    let updated = max (int_of_string head) c.blue in
    { c with blue = updated  }
  | _ -> c

let parse2 (c: rgb) (str:string ) = 
  str
  |> String.split [',']
  |> map trim
  |> List.fold_left parse1 c

let parse3 (c: rgb) (str:string) = 
  str
  |> String.split [';']
  |> map trim
  |> List.fold_left parse2 c

let parseGame (str:string) =
  let splits = String.split [ ':' ] str in
  let gameStr = (List.nth splits 0) in
  let gameId = 
    String.substring (gameStr) 5 (String.length gameStr - 5) in 
  gameId |> int_of_string, parse3 defRgb (List.nth splits 1)

let canPlay (cubes: rgb) (game: rgb) = 
  cubes.red >= game.red
  && cubes.green >= game.green
  && cubes.blue >= game.blue

let part1Cubes = { red=12; green=13; blue=14 }

// let part1() = 
//   file_readlines "input"
//   |> map parseGame
//   |> map (fun (gid, cubes) -> 
//     let c = canPlay part1Cubes cubes in 
//     let n = if c then gid else 0 in
//     n
//   )
//   |> list_sum_by id
//   |> string_of_int |> print

// part 2 


let part2() = 
  file_readlines "input"
  |> map parseGame
  |> map (fun (gid, cubes) -> 
    mul (mul cubes.red cubes.green) cubes.blue
  )
  |> list_sum_by id
  |> string_of_int |> print


let main = 
  let _ = part2() in ()






