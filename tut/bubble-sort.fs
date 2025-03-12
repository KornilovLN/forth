\ ------------------------------------------
\ Сортировка пузырьком на языке gforth
\ Компиляция и запуск: gforth bubble_sort
\ Github: github.com/KornilovLN
\ mail:   ln.KornilovStar@gmail.com 
\ ------------------------------------------

\ === Несколько авторских слов =============-
 
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
: NDUP 0 DO DUP LOOP ;  \ Неск. дублирований

: CR_n                  \ CR, если > n колон
  MOD 0 = IF 
    cr 
  THEN
;

: DEFIS 45 EMIT ;       \ выводит дефис

\ N DEFIS_LINE
: DEFIS_LINE            \ Стр. из N дефисов
  0 DO DEFIS LOOP
;

\ N TITLE_LINE
: TITLE_LINE            \ подчерк. из N '-'
  ." <<< "
  DEFIS_LINE
  ."  >>>"
;

\ N TITLE_TUT
: TITLE_TUT             
  dup
  TITLE_LINE  cr
  ." <<< Создаем  массив  и  сортируем >>>"
  cr
  TITLE_LINE  cr
;

: OUT
  @ .
;

: OUT_IND               \ Вывод индекса масс
  ." Ind[ "             \ выводим «Ind[»
  .                     \ и то, что на стеке    
  ." ]= "               \ выводим « ] = »
;

: SKOBA< ." < " ;       \ так себе, красоты 
: SKOBA> ." > " ;
: SKOBA{ ." { " ;
: SKOBA} ." } " ;

: MINtoMAX - 0> ;       \ условия сортировки
: MAXtoMIN - 0< ;

\ ==========================================

8  CONSTANT U64
33 CONSTANT LIMIT_AR

variable limit          \ var для емк-ти AR
LIMIT_AR limit !        \ в limit емкость AR

create AR[] limit @ cells allot
variable aAR            \ для адр массива 
AR[] aAR !              \ сохр адр масс AR

variable v1             \ var для алгоритма
variable v2             \ var для алгоритма
variable tmp            \ var для алгоритма

\ ------------------------------------------

: X]                  \ наполнение массива 
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

\ Для отладки убрать косые - комментарии !!!
\ На входе длина массива. limit @
: B-SORTING     
           
  DUP 1-        \ дубл. для внут. цик., - 1
  0 DO          \ for(int i=0;i<size-1;i++)
    DUP 

\ от SKOBA< до SKOBA>  -  debug only

    SKOBA<      \ красоту наводим: колонки
    I 10 <      \ I 10-ти меньше? 
    IF          \ ЕСЛИ
      ."  " I . \   добав. " "
    ELSE        \ ИНАЧЕ
      I .       \   печать без " " 
    THEN        \ ТОГДА
    SKOBA>      \ закрыли скобку

    I - 1-
    0 DO        \ for(intj=0;j<size-i-1;j++)
      DUP

      \ запомнть адреса ячеек масс. в v1 v2
      I U64 * aAR @ +       v1 ! 
      I U64 * aAR @ + 8 +   v2 ! 

      v1 @ @
      v2 @ @
      MINtoMAX       
      IF

        v2 @ @   tmp !  \ tmp = ar[j+1]       
        v1 @ @          \ stack <- ar[j]      
        tmp @   v1 @ !  \ ar[j] = tmp
                v2 @ !  \ ar[j+1] <- stack
          
       V2 @ @ .
     ELSE
       V1 @ @ .
   
      THEN      
      DROP 

    LOOP  
    cr
           
  LOOP

  DROP
;

\ count              \ на входе длина масс. 
: VIEW_AR
   0 DO      
    I 2 CR_n         \ CR после 2-х столбцов    
    I OUT_IND        \ Вывести так: Ind[ n ]    
    I U64 * aAR @ +  \ адр = (смещ+нач адр)                    
    @  .             \ по этому адресу число        
    ."    "          \ пробелы
  LOOP
;

\ ==========================================

ABOUT   cr

29 TITLE_TUT

cr
." Исходный массив: " 
limit @   X] 
\ .S:
cr  

limit @   VIEW_AR
\ .S:
cr cr

." Сортируем массив пузырьком.." 
cr cr 
\ .S:
limit @ B-SORTING
\ .S:
cr

." Отсортированный массив: "
cr
limit @   VIEW_AR
\ .S:
cr cr

29 TITLE_LINE
cr cr

\ ==========================================


