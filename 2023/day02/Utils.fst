// reinventing standard library stuff
module Utils

open FStar.All
open FStar.List
open FStar.IO
open FStar.Char
open FStar.String

let print (str : string) = 
  print_string (str ^ "\n")

let print_list (xs : list string) = 
  xs |> map print

private let rec read_input_lines (acc: list string) (f : fd_read) : ML (list string) = 
    try
        let currline = read_line f in 
        // skip empty line
        match currline with 
        | "" -> read_input_lines (acc) f
        | _ -> read_input_lines (currline :: acc) f
    with
    | _ -> acc

let file_readlines filename = 
    let f = open_read_file filename in
    let lines = read_input_lines [] f in
    let _ = close_read_file f in 
    lines


let int32_to_int (n: Int32.t) : int = Int32.v n
let int32_of_int (n: Int.int_t 32) = Int32.int_to_t n  
let mul (x: int) (y:int) : int = op_Multiply x y

let int_of_string (str: string) : ML int = 
  str |> Int32.of_string |> int32_to_int


let try_digit_of_char (chr: char) : ML (option (int)) = 
    match chr with 
    |'1'|'2'|'3'|'4'|'5'|'6'|'7'|'8'|'9'|'0'  -> 
        chr
        |> string_of_char
        |> Int32.of_string
        |> int32_to_int
        |> Some  
    | _ -> None

let list_sum_by fn items = 
    items |> fold_left (fun acc v -> acc + fn v) (0)

let max (x:int) (y:int) = if x > y then x else y


let rec trim_start (chars:list char) = 
  match chars with 
  | [] -> []
  | ' ' :: tail -> trim_start tail
  | head :: tail -> chars

let trim (str:string) = 
  str
  |> list_of_string
  |> List.rev
  |> trim_start
  |> List.rev
  |> trim_start
  |> string_of_list
    
let string_starts_with (prefix:string) (str:string) : ML bool = 
  let s1 = String.substring prefix 0 (String.length prefix) in
  let s2 = String.substring str 0 (String.length prefix) in
  String.compare s1 s2 = 0


