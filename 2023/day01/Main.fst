module Main

open FStar.IO
open FStar
open FStar.String
open FStar.Char
open FStar.List
open FStar.Int
open FStar.Exn
open FStar.All
open Prims
open Utils

let nums (str: string) = 
  str 
  |> list_of_string
  |> filter_map try_digit_of_char
  |> map string_of_int

let first_and_last (nums: list string) : ML string = 
  match nums with 
  | [] -> "0"
  | [ head ] -> head ^ head
  | head :: tail -> 
    let last = List.last tail in
    head ^ last

let part1() = 
  file_readlines "p1"
  |> map nums
  |> map first_and_last
  |> map int_of_string
  |> list_sum_by id
  |> string_of_int
  |> print

// part 2 

let rec get_digits (acc) (xs: list char) : ML (list int) = 
    match xs with 
    | [] -> acc |> List.rev
    | head :: tail -> 
      match head with 
      |'1'|'2'|'3'|'4'|'5'|'6'|'7'|'8'|'9'  -> 
        let curr = head |> string_of_char |> int_of_string in
        get_digits (curr :: acc) tail
      | _ -> 
        match xs with 
        | 'o' :: 'n' :: 'e' :: tail -> get_digits (1 :: acc) ('e' :: tail)
        | 't' :: 'w' :: 'o' :: tail -> get_digits (2 :: acc) ('o':: tail)
        | 't' :: 'h' :: 'r' :: 'e' :: 'e' :: tail -> get_digits (3 :: acc) ('e' :: tail)
        | 'f' :: 'o' :: 'u' :: 'r' :: tail -> get_digits (4 :: acc) tail
        | 'f' :: 'i' :: 'v' :: 'e' :: tail -> get_digits (5 :: acc) ('e' :: tail)
        | 's' :: 'i' :: 'x' :: tail -> get_digits (6 :: acc) tail        
        | 's' :: 'e' :: 'v' :: 'e' :: 'n' :: tail -> get_digits (7 :: acc) ('n' :: tail)
        | 'e' :: 'i' :: 'g' :: 'h' :: 't' :: tail -> get_digits (8 :: acc) ('t'::tail)
        | 'n' :: 'i' :: 'n' :: 'e' :: tail -> get_digits (9 :: acc) ('e'::tail)
        | _ -> get_digits (acc) tail


let part2() = 
  // file_readlines "p2"
  file_readlines "input"
  |> map list_of_string
  |> map (get_digits [])
  |> map (map string_of_int)
  |> map first_and_last
  |> map int_of_string
  
  // |> map string_of_int 
  // |> print_list
  
  |> list_sum_by id
  |> string_of_int
  |> print

let () = 
  let _ = part2() in ()


