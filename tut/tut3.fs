: ABOUT
cr
." <<< ----------------------------- >>>" cr
." <<< Vars, array, IF, LOOP в forth >>>" cr
." <<< Author: LN Kornilov           >>>" cr
." <<< Github: github.com/KornilovLN >>>" cr
." <<< ml: ln.KornilovStar@gmail.com >>>" cr
." <<< data:   24-12-2023   18:50:00 >>>" cr
." <<< ----------------------------- >>>" cr
;

: GLAVA 
  ." <<< Опыты с обмен. данными памяти >>>"
; 

\ ====== авторск. слова из стандартных =====

\ как преобразовать forth запись в обычную:
\ 3 2 4 2 6 5 * + / - *
\ следует сделать замену для схемы Горнера
\ слова  / -  заменить на  SWAP/  SWAP-

: SWAP/ SWAP / ;      \ для схемы Горнера
: SWAP- SWAP - ;      \ для схемы Горнера 

: .S:
  cr  ." Stack: "
  .S  cr
;

\ N NDUP
: NDUP 0 DO DUP LOOP ; \ Неск. дублирований

: CR_n                \ CR, если > n колон
  MOD 0 = IF 
    cr 
  THEN
;

: DEFIS 45 EMIT ;     \ выводит дефис

\ N DEFIS_LINE
: DEFIS_LINE          \ Строка из N дефисов
  0 DO DEFIS LOOP
;

\ N TITLE_LINE
: TITLE_LINE          \ рам. подчерк. N '-'
  ." <<< "
  DEFIS_LINE
  ."  >>>"
;

\ N TITLE_TUT
: TITLE_TUT           \ исп-з.подчерк. N '-'
  DUP
  TITLE_LINE  cr
  GLAVA
  cr
  TITLE_LINE  cr
;

: OUT
  @ .
;

: OUT_IND             \ Вывод индекса масс
  ." Ind[ "           \ выводим «Ind[»
  .                   \ и то, что на стеке    
  ." ]= "             \ выводим « ] = »
;

\ === Константы, переменные и массивы ======

8  CONSTANT U64
16 CONSTANT LIMIT_AR

variable limit        \ перем для емкости AR
LIMIT_AR limit !      \ в limit емкость AR

create AR[] limit @ cells allot
variable aAR          \ для адр массива 
AR[] aAR !            \ сохр адр массива AR

variable v1           \ перем для алгоритма
variable v2           \ перем для алгоритма
variable tmp          \ перем для алгоритма

variable Icopy        \ перем для алгоритма


21  CONSTANT  EDGCNT
10  CONSTANT  BEGCNT

\ Еще 2 переменные vv2 и vv1
variable vv2
EDGCNT vv2 !

variable vv1
BEGCNT vv1 !

\ и еще массив PARAMS[] с 2-мя числами U64
create PARAMS[] 2 cells allot
EDGCNT 0 U64 * PARAMS[] +  ! \ PARAMS[0]=18  
BEGCNT 1 U64 * PARAMS[] +  ! \ PARAMS[1]=10 

\ === Новые слова для опытов ===============

\ Определение функции сравнения и вывода

: EQVIVALENT =
  IF ." v1 == v2"
  THEN 
;

: BOLSHE > 
  IF ." v1 > v2" 
  ELSE ." v1 < v2" 
  THEN
;

: MENSHE <
  IF ." v1 < v2" 
  ELSE ." v1 > v2" 
  THEN
;

\ v1 v2 EXCHANGE  \ посредством  памяти tmp
: EXCHANGE    \ *v1 <-tmp-> *v2 
  DUP @       \ (v1 v2 -- v1 v2 v2@)
  tmp !       \ (v1 v2 v2@ tmp -- v1 v2)
  SWAP DUP @  \ (v1 v2 -- v2 v1 v1@)
  ROT !       \ (v2 v1 v1@ -- v1)
  tmp @       \ (v1 -- v1 tmp@)
  SWAP !      \ (tmp@ v1)
;

\ Определение функции вывода
: print ( addr -- ) \ приним адр стр и выв
  type
;

\ === Основная программа ===================

ABOUT   cr
29 TITLE_TUT

\ ------------------------------------------

." До обмена дан. меж v1 и v2 через tmp"
56 v1 !
73 v2 !
0  tmp !

cr
v1    OUT
v2    OUT
tmp   OUT   cr  cr

v1 v2 EXCHANGE

." После обмена. В tmp остался мусор"
cr
v1    OUT
v2    OUT
tmp   OUT   cr  cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Опыты с ветвлениями на примере v1, v2" cr
29 TITLE_LINE  cr

56 v1 !
73 v2 !

v1 @ v1 @  EQVIVALENT cr
v1 @ v2 @  BOLSHE     cr
v1 @ v2 @  MENSHE     cr  cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Опыты с a b compare-and-print" cr
29 TITLE_LINE  cr

: compare-and-print ( a b -- )
  over over = if        \ если a = b
    ." a = b" cr
  else
    over over > if      \ если a > b
      ." a > b" cr
    else
      ." a < b" cr      \ если a < b
    then
  then
;

.S:   cr 
-900 109 compare-and-print 
.S:   cr 
DROP DROP

\ ------------------------------------------

29 TITLE_LINE  cr
." Опыты с match-example" cr
29 TITLE_LINE  cr

\ MATCH слово для примера
: match-example ( x -- )
  case
    1 of
      ." One"
    endof
    2 of
      ." Two"
    endof
    3 of
      ." Three"
    endof
      ." Default"
  endcase
;

cr
7 match-example   .S:   cr
1 match-example   .S:   cr
3 match-example   .S:   cr
2 match-example   .S:   cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Опыты с вложенными циклами" cr
29 TITLE_LINE  cr

: IS_I  0   DO  I . LOOP ;
: AS_I  0   DO  CR  1 - DUP  IS_I  LOOP ;

: QADRO DUP * ;
: IS_Q  0   DO  I QADRO .          LOOP ;
: AS_Q  0   DO  CR 1 - DUP  IS_Q   LOOP ;

9 4 AS_I    cr
11 10 AS_Q  cr  cr
DROP DROP

\ ------------------------------------------

29 TITLE_LINE  cr
." Опыты с вложенными циклами" cr
29 TITLE_LINE  cr  cr

\ Чтобы во внутреннем цикле получить текущее
\ значение счетчика объемлющего цикла,
\ используется слово J, например:
\ DO 
\   ..
\   I
\   .. 
\   DO 
\     ..
\     I
\     ..
\     J
\     ..
\   LOOP
\   ..
\   I
\   ..
\ LOOP
\
\ Первое вхождение слова I дает текущее
\ значение счетчика внешнего цикла.
\ Следующее вхождение I дает уже значение
\ счетчика внутреннего цикла.
\ Чтобы получить счетчик внешнего цикла,
\ надо использовать слово J.

\ Это внутр цикл получает на входе 2 числа
\ границу цикла и начальное знач счетчика
\ применение:
\ EDGE_CNT BEG_CNT INLOOP
: INLOOP
  ." inloop< "
  DO
    I 10 <                \ I 10-ти меньше? 
    IF                    \ ЕСЛИ
      ."  " I .           \   добав. " "
    ELSE                  \ ИНАЧЕ
      I .                 \   печать без " " 
    THEN                  \ ТОГДА
  LOOP
  ." >"
;

\ Это внешний цикл получает на входе 2 числа
\ границу цикла и начальное знач счетчика
\ применение:
\ EDGE_COUNT BEG_COUNT EXTLOOP
\ и формирует вх. параметры для внутр. цикла
: EXTLOOP
  ." extloop< "  cr  
  DO
    \ I  .
    
    I 10 <                \ I 10-ти меньше? 
    IF                    \ ЕСЛИ
      ."  " I .           \   добав. " "
    ELSE                  \ ИНАЧЕ
      I .                 \   печать без " " 
    THEN                  \ ТОГДА
    
    ."  "
\ вместо 2-х переменных
\   vv2 @ I -  vv1 @ I -  INLOOP  cr

\ можно применить массив с 2-мя параметрами 
    PARAMS[]     @  I -   
    PARAMS[] 8 + @  I - 
    INLOOP cr

  LOOP
  ." >"
;

\ Как прочитать 2 ячейки мвссива
PARAMS[]  1 NDUP    @ \ PARAMS[] PARAMS[] @
          SWAP 8 +  @ \ PARAMS[] 8 +      @   
INLOOP                \ применили в INLOOP
cr  cr

11 0 EXTLOOP   cr  cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Таблица умножения I * J" cr
29 TITLE_LINE  cr

: .MATRIX 
  11 0 DO
    11 0 DO
      I J *
       
      DUP                 \ дублируем
      10 <                \ 10-ти меньше?
       
      IF                  \ ЕСЛИ
        ."  " .           \   добав. " "
      ELSE                \ ИНАЧЕ
        .                 \   печать без " " 
      THEN                \ ТОГДА
      
  LOOP  cr
LOOP
;

.S:    
.MATRIX
.S:   cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Как применять схему Горнера?"          cr
." 3 2 4 2 6 5 * + / - *"                 cr
." (3 (2 (4 (2 (6 5 *) +) /) -) *)"       cr
." (((((6 * 5) + 2) / 4) - 2) * 3) = 18"  cr
." тепеь / - заменить на SWAP/ и SWAP-"   cr
." 3 2 4 2 6 5 * + SWAP/ SWAP- *"         cr
." Result = "
3 2 4 2 6 5 * + SWAP/ SWAP- * .           cr
29 TITLE_LINE  cr

cr

." Result = " .S:
3 2 4 2 6 5   .S:
*             .S:
+             .S:
SWAP/         .S:
SWAP-         .S:
*             .S:
.             .S:  

cr

\ ------------------------------------------

29 TITLE_LINE  cr
." Конец работы программы с циклами" cr
29 TITLE_LINE  cr  cr

\ ==========================================

bye




















