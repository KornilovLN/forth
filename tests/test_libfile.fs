include ../lib_str/lib_file.fs

." ==================================================================="

\ Слово для чтения файла в память и вывода его содержимого в консоль
\ : cat ( c-addr u -- n ) ... ; 
\ Использование:    s"filename.txt" cat cr
.S cr
s" ../tut/read_file.fs" cat
cr cr
.S cr


\ Вывод размера файла без чтения файла в память:
\ : get-file-size ( c-addr u -- ) ... ;
\ Использование:    s"filename.txt" get-file-size
s" ../tut/read_file.fs" get-file-size           \ в стеке размер файла       
.S cr
." Размер файла ../tut/read_file.fs = " . cr    \ снять со стека и вывести точкой
.S cr
." получен словом get-file-size" cr
.S cr
cr ." Строка " s" tttеееФФФ" type cr
.S cr

bye