: ABOUT
cr
." <<< ----------------------------- >>>" cr
." <<< Сортировка пузырьком на forth >>>" cr
." <<< Author: LN Kornilov           >>>" cr
." <<< Github: github.com/KornilovLN >>>" cr
." <<< ml: ln.KornilovStar@gmail.com >>>" cr
." <<< ----------------------------- >>>" cr
;

: .S: cr ." Stack: ".S cr ;

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
  dup
  TITLE_LINE  cr
  ." <<< Создаем  массив  и  сортируем >>>"
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

\ ==========================================

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

\ ------------------------------------------

: X]                  \ иниц массива 
  DUP 0 DO            \ для каждой ячейки
    DUP               \ взять предельное зн. 
    I                 \ взять индекс
    -                 \ найти разницу 

    DUP 2 MOD 0=      \ Для четных * 3
    IF 
      3 *
    THEN

    I U64 * aAR @ +   \ адр=(смещ+нач адрес)
    !                 \ запись разн по адр
  LOOP
  DROP
;

\ 8  CONSTANT U64
\ 16 CONSTANT LIMIT_AR
\
\ variable limit        \ var для емкости AR
\ LIMIT_AR limit !      \ limit = LIMIT_AR
\
\ create AR[] limit @ cells allot
\ variable aAR          \ для адр массива 
\ AR[] aAR !            \ сохр адр AR[]


: SKOBA< ." < " ;
: SKOBA> ." > " ;
: SKOBA{ ." { " ;
: SKOBA} ." } " ;

: MINtoMAX - 0> ;
: MAXtoMIN - 0< ;

\ На входе длина массива. limit @
: B-SORTING    \ for(int i=0;i<size-1;i++)
           
  DUP 1-       \ дуб. для внутр. цикла и - 1
  0 DO

    SKOBA<      \ красоту наводим в колонках
    DUP I 10 <  \ I 10-ти меньше? 
    IF          \ ЕСЛИ
      ."  " I . \   добав. " "
    ELSE        \ ИНАЧЕ
      I .       \   печать без " " 
    THEN        \ ТОГДА
    SKOBA>      \

    SKOBA{
    I - 1-
    0 DO      \ for(int j=0;j<size-i-1;j++)
      DUP

      \ запомнть адреса в v1 v2
      I U64 * aAR @ +     v1 ! 
      I U64 * aAR @ + 8 + v2 ! 

      v1 @ @
      v2 @ @
      MINtoMAX       
      IF

        v2 @ @          \ v2@ 
        tmp             \ v2@ tmp 
        !               \   
        
        v1 @ @          \ v1@
        tmp @           \ v1 v1 tmp@
        v1 @            \ v1@ tmp@ v1
        !               \ v1@

        v2 @            \ V1 V2
        !               \   

        V2 @ @ .
      ELSE     
        V1 @ @ .   
      THEN      
      DROP  \ DROP

    LOOP  
    SKOBA}  cr
               
  LOOP

  DROP
;

\ count ( -- count)                 <Опыты>
: INNER
    0 DO  
      I . 
    LOOP
;

\ count+1 count ( -- count+1 count) <Опыты>
: BUBBLE    
    0 DO  
      CR  1 - DUP 
      DUP ." < " . ." > " 

      INNER  

    LOOP  
    DROP
;

\ count
: VIEW_AR
   0 DO      
    I 4 CR_n         \ CR после 4-х столбцов    
    I OUT_IND        \ Вывести так: Ind[ n ]    
    I U64 * aAR @ +  \ адр = (смещ+нач адр)                    
    @                \ по этому адресу число
    .                \ печатаем его    
    ."    "          \ печатаем «    »
  LOOP
;

\ ==========================================

ABOUT   cr

29 TITLE_TUT

." Исходный массив: " 
limit @   X] 
.S:   cr cr   

limit @   VIEW_AR
.S:   cr cr

." Сортируем массив пузырьком.."  
.S:
limit @ B-SORTING
.S:   cr cr

." Отсортированный массив: "
limit @   VIEW_AR
.S:   cr cr

29 TITLE_LINE
cr cr

\ ==========================================

bye

