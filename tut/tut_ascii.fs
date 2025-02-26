: ABOUT
cr
." <<< ----------------------------- >>>" cr
." <<< ASCII pseudo-graphics table   >>>" cr
." <<< Author: LN Kornilov           >>>" cr
." <<< Github: github.com/KornilovLN >>>" cr
." <<< ml: ln.KornilovStar@gmail.com >>>" cr
." <<< data:   24-12-2023   18:50:00 >>>" cr
." <<< ----------------------------- >>>" cr
;

: GLAVA 
  ." <<< Работа: ASCII pseudo-graphics >>>"
; 

\ ====== авторск. слова из стандартных =====

\ как преобразовать forth запись в обычную:
\ 3 2 4 2 6 5 * + / - *
\ следует сделать замену для схемы Горнера
\ слова  / -  заменить на  SWAP/  SWAP-
: SWAP/ SWAP / ;      \ для схемы Горнера
: SWAP- SWAP - ;      \ для схемы Горнера 

: .S:  cr  ." Stack: "  .S  cr ;

\ N NDUP
: NDUP 0 DO DUP LOOP ; \ Неск. дублирований

: CR_n                \ CR, если > n колон
  MOD 0 = IF 
    cr 
  THEN
;

: DEFIS 45 EMIT ;     \ выводит дефис

\ N DEFISES
: DEFISES             \ Строка из N дефисов
  0 DO DEFIS LOOP
;

\ N TITLE_LINE
: TITLE_LINE          \ рам. подчерк. N '-'
  ." <<< "
  DEFISES
  ."  >>>"
;

\ N TITLE
: TITLE               \ исп-з.подчерк. N '-'
  DUP
  TITLE_LINE  cr
  GLAVA
  cr
  TITLE_LINE  cr
;

\ V                   \ на стеке переменная
: OUT                 \ выв содержимого
  @ .
;

: OUT_IND             \ Вывод индекса масс
  ." Ind[ "           \ выводим «Ind[»
  .                   \ и то, что на стеке    
  ." ]= "             \ выводим « ] = »
;

\ === Константы, переменные и массивы ======

32 CONSTANT BEG_CHARS \ с какого кода начать
6  CONSTANT REICH     \ вывод 6-ти рядов

0  CONSTANT HEX_FMT
1  CONSTANT DEC_FMT

variable fmt

: SET_HEX_FMT
  HEX_FMT fmt !
;

: SET_DEC_FMT
  DEC_FMT fmt !
;

\ === Новые слова для опытов ===============

\ Определение функции сравнения и вывода
\ v1 v2 EXCHANGE  \ посредством  памяти tmp
variable tmp
: EXCHANGE    \ *v1 <-tmp-> *v2 
  DUP @       \ (v1 v2 -- v1 v2 v2@)
  tmp !       \ (v1 v2 v2@ tmp -- v1 v2)
  SWAP DUP @  \ (v1 v2 -- v2 v1 v1@)
  ROT !       \ (v2 v1 v1@ -- v1)
  tmp @       \ (v1 -- v1 tmp@)
  SWAP !      \ (tmp@ v1)
;

: OUT_VAL_DEC
  DUP           \ дублируем
  10 >          \ 10-ти меньше?       
  IF  

    DUP
    100 <
    IF
      ."   " .  \ ЕСЛИ, добав. "  "
    ELSE
      ."  " .   \ ЕСЛИ, добав. " "
    THEN    
    
  ELSE          \ ИНАЧЕ не добавлять " "
    . 
  THEN          \ ТОГДА
;

: EMIT_VAL_DEC
  ."   " EMIT ."   " 
;

: OUT_VAL_HEX
  HEX  .  
;

: EMIT_VAL_HEX
  EMIT ."   "
;

: VAL_?FMT
  fmt @ 0= 
  IF    OUT_VAL_HEX 
  ELSE  OUT_VAL_DEC THEN
;

: EMIT_?FMT
  fmt @ 0= 
  IF    EMIT_VAL_HEX 
  ELSE  EMIT_VAL_DEC THEN
;

: ASCII_TABLE 
  REICH 0  DO       \ вывод 6 рядков
    2 0  DO         \ код, потом ASCII

      I 0=          
      IF
        16 0 DO     \ рядок кода   
          BEG_CHARS I K 16 * + +
          VAL_?FMT
        LOOP  cr
      ELSE
        16 0 DO     \ рядок ASCII   
          BEG_CHARS I K 16 * + +
          EMIT_?FMT  
        LOOP  cr
      THEN

  LOOP  cr
LOOP
;

\ === Основная программа ===================

ABOUT   cr
29 TITLE

cr
29 TITLE_LINE  cr
." <<< " 
." ASCII table                   >>>"  cr
29 TITLE_LINE  cr
cr

\ ------------------------------------------

SET_HEX_FMT   \ будет печатать HEX формат
\ SET_DEC_FMT   \ будет печатать 10. формат
           
ASCII_TABLE   \ ASCII от 32. до 127. кода

DECIMAL       \ возврат к DEC в forth

\ ------------------------------------------

29 TITLE_LINE  cr
." Конец работы программы ASCI table" cr
29 TITLE_LINE  cr  cr

\ ==========================================

bye




















