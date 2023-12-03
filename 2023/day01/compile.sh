#!/usr/bin/env bash

set -euo pipefail
__SOURCE_DIRECTORY__=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# fstar.exe --codegen OCaml --extract Utils Main.fst &>/dev/null
fstar.exe --codegen OCaml --extract Main --extract Utils Main.fst &>/dev/null
ocamlfind opt -package fstar.lib -linkpkg -g ./Utils.ml ./Main.ml -o ./main &>/dev/null

./main

# rm *.cmx
# rm *.cmi
# rm *.o
# rm *.ml


