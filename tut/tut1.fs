: ABOUT
cr
." <<< ----------------------------- >>>" cr
." <<< Работа с массивами целых      >>>" cr
." <<< Author: LN Kornilov           >>>" cr
." <<< Github: github.com/KornilovLN >>>" cr
." <<< ml: ln.KornilovStar@gmail.com >>>" cr
." <<< ----------------------------- >>>" cr
;
 
\ --- Оформителькие элементы ---------------

: DEFIS               \ выводит дефис
  45 EMIT
;

: DEFIS_LINE          \ Строка дефисов
  0 DO DEFIS LOOP
;

: TITLE_LINE          \ рамочное подчерк.
  ." <<< "
  29 DEFIS_LINE
  ."  >>>"
;

: TITLE_TUT_1
  TITLE_LINE  cr
." <<< Квадраты и кубы  целых  чисел >>>" cr
  TITLE_LINE  cr
;

: OUT_IND            \ Вывод индекса массива
  ." Ind[ "          \ выводим «Ind[»
  .                  \ печ то что на стеке    
  ." ]= "            \ выводим « ] = »
;

: CR_n               \ CR, если > n колон
  MOD 0 = IF 
    cr 
  THEN
;

: QUADRO            \ Возводит в квадрат
  DUP *
;

: CUBE              \ Возводит в куб
  DUP DUP * *
;

\ ------------------------------------------

8  CONSTANT U64
16 CONSTANT LIMIT_LOOP

\ Резервируем место под массив целых чисел
create AR2[ LIMIT_LOOP cells allot
create AR3[ LIMIT_LOOP cells allot

variable aARR        \ перем-ная для адресов 

: X^2]               \ ^2 кажд член массива 
  LIMIT_LOOP 0 DO    \ для каждой ячейки
    I QUADRO         \ вычис квадрат
    I U64 * aARR @ + \ адр=(смещ+нач адрес)
    !                \ запись
  LOOP
;

: X^3]               \ ^3 кажд член массива  
  LIMIT_LOOP 0 DO    \ для каждой ячейки
    I CUBE           \ вычис куб
    I U64 * aARR @ + \ адр = (смещ+нач адр)
    !                \ запись по адресу
  LOOP
;

: VIEW_ARR
  LIMIT_LOOP 0 DO      
    I 2 CR_n         \ CR после 2-х столбцов    
    I OUT_IND        \ Вывести так: Ind[ n ]    
    I U64 * aARR @ + \ адр = (смещ+нач адр)                    
    @                \ по этому адресу число
    .                \ печатаем его    
    ."    "          \ печатаем «    »
  LOOP
;

\ ==========================================

ABOUT
cr

TITLE_TUT_1

AR2[ aARR !         \ сохр адр массива AR2
X^2] VIEW_ARR       \ адр AR2 извл: aARR @ 
cr  

AR3[ aARR !         \ сохр адр массива AR3
X^3] VIEW_ARR       \ адр AR3 извл: aARR @ 
cr

TITLE_LINE
cr cr




\ ==========================================
