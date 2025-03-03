\ следует сделать замену для схемы Горнера
\ слова  / -  заменить на  SWAP/  SWAP-
: SWAP/ SWAP / ;      \ для схемы Горнера
: SWAP- SWAP - ;      \ для схемы Горнера 

\ Поможеь выводить стек не разрушая его
: .S:  cr  ." Stack: "  .S  ;

\ Многократное дублирование
: NDUP ( n -- )
  0 DO DUP LOOP
; 

\ Вывод переменной V
\ на стеке переменная V
: OUT_VAR ( n -- ) \ вывод содержимого переменной
  @ .
;

\ : .2DIGITS ( n -- )
\     0 <# # # #> TYPE ;

: FRM-DATETIME
    TIME&DATE
    ."     "
    ." Дата:  "

    .
    ." - "  
    .
    ." - "
    .
    ."  "

    .
    ." : "
    .
    ." : "
    .
;

\ Вспомогательное слово для вывода двузначного числа с ведущим нулём при необходимости
: .2DIGITS ( n -- )
    10 < IF ." 0" THEN . ;    

\ --- Массивы ----------------------------------------

: array-clear ( addr len -- )
    0 DO
        DUP I CELLS + 0 SWAP !
    LOOP ;


: array-fill ( value addr len -- )
    0 DO
        2DUP  \ дублируем значение и адрес
        I CELLS + !  \ сохраняем значение по адресу addr+i*cell
    LOOP
    2DROP  \ убираем оставшиеся значение и адрес со стека
;
