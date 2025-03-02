include ../lib_str/lib_design.fs
include ../lib_str/lib_common.fs
include ../lib_str/lib_time.fs

VARIABLE X
VARIABLE RES

\ === Универсальная версия для полинома произвольной степени ===

\ Создание массива для коэффициентов полинома
VARIABLE DEGREE                 \ Степень полинома
CREATE UCOEFS 10 CELLS ALLOT    \ Массив для полиномов до 9-й степени

\ Инициализация массива произвольными коэффициентами
: SET-COEF ( value index -- )
    CELLS UCOEFS + !
;

\ Вычисление полинома произвольной степени по схеме Горнера
: POLI_UNIVERSALE ( x -- result )
    \ Начинаем с коэффициента при старшей степени
    0 CELLS UCOEFS + @  \ Старший коэффициент
    
    \ Применяем схему Горнера в цикле
    DEGREE @ 0 
    DO
        OVER *                  \ Умножаем на x
        I 1+ CELLS UCOEFS + @   \ Берем следующий коэффициент
        +                       \ Добавляем к промежуточному результату
    LOOP
    
    \ На стеке остались x и результат
    SWAP DROP
; 


\ === Полином задается с коэффициентами в массиве ===
\ Создание коэффициентов полинома 3-й степени (4 коэффициента)
CREATE COEFS 4 CELLS ALLOT  \ array полинома 

\ Инициализация массива коэффициентами
: INIT-COEFS ( -- )
    4 0 CELLS COEFS + !  \ a[0] = 4 (коэф. при x^3)
    9 1 CELLS COEFS + !  \ a[1] = 9 (коэф. при x^2)
    7 2 CELLS COEFS + !  \ a[2] = 7 (коэф. при x^1)
    5 3 CELLS COEFS + !  \ a[3] = 5 (свободный член)
;

\ Вычисление полинома с коэффициентами из массива
: POLI3_ARRAY ( x -- )
        .S: \ x
    DUP .S: \ x x
    DUP .S: \ x x x
    
    0 CELLS COEFS + @ .S: \ x x x a[0]
    * .S: \ x x (x*a[0])
    
    1 CELLS COEFS + @ .S: \ x x (x*a[0]) a[1]
    + .S: \ x x (x*a[0]+a[1])
    
    * .S: \ x (x*(x*a[0]+a[1]))
    
    2 CELLS COEFS + @ .S: \ x (x*(x*a[0]+a[1])) a[2]
    + .S: \ x (x*(x*a[0]+a[1])+a[2])
    
    * .S: \ (x*(x*(x*a[0]+a[1])+a[2]))
    
    3 CELLS COEFS + @ .S: \ (x*(x*(x*a[0]+a[1])+a[2])) a[3]
    + .S: \ ((x*(x*(x*a[0]+a[1])+a[2]))+a[3])
    
    RES ! .S: \ сохраняем результат в RES
;

\ === TEST ==========================================
\ 4 * x^3 + 9 * x^2 + 7 *x + 5 = ?
\ x * ( x * ( x * 4 + 9 ) + 7 ) + 5 = ?
\ x * ( x * ( U1 ) + 7 ) + 5 = ?
\ --- Variables -------------------------------------
VARIABLE V1
VARIABLE V2
VARIABLE V3
VARIABLE V4

VARIABLE V5


\ --- Initialization --------------------------------
4 V1 !
9 V2 !
7 V3 !
5 V4 !
11 V5 !

8 X !

\ --- Схема Горнера --------------------------------
\ X POLINOME3
\ --------------------------------------------------
: POLINOME3 ( x -- )    \ Вычисляем полином 3-й ст.
      .S:               \ x
  DUP .S:               \ x x
  DUP .S:               \ x x x
  V1 @ .S:              \ x x x 4
  * .S:                 \ x x (x*4)
  V2 @ .S: + .S:        \ x x (x*4+9))
  * .S:                 \ x (x*(x*4+9))
  V3 @ .S: + .S:        \ x (x*(x*4+9)+7)
  * .S:                 \ (x*(x*(x*4+9)+7))
  V4 @ .S: + .S:        \ ((x*(x*(x*4+9)+7))+5) 
  RES  .S:              \ ((x*(x*(x*4+9)+7))+5) (address of RES)
  ! .S:                 \ ((x*(x*(x*4+9)+7))+5) -> RES
;  

: POLI3 ( x -- )    \ Вычисляем полином 3-й ст.
  DUP               \ x x
  DUP               \ x x x
  V1 @              \ x x x 4
  *                 \ x x (x*4)
  V2 @ +            \ x x (x*4+9))
  *                 \ x (x*(x*4+9))
  V3 @ +            \ x (x*(x*4+9)+7)
  *                 \ (x*(x*(x*4+9)+7))
  V4 @ +            \ ((x*(x*(x*4+9)+7))+5) 
  RES               \ ((x*(x*(x*4+9)+7))+5) (address of RES)
  !                 \ ((x*(x*(x*4+9)+7))+5) -> RES
;  



\ --- Test: X POLINOME3 ------------------------
: TEST_POLINOME3 ( n -- )
CR 
X @ DUP                     \ x понадобится для полинома и для вывода
POLINOME3                   \ Вычисляем полином 3-й ст.
CR
CR .                        \ Выводим x ранее сохраненный с пом DUP
." POLINOME3 = " RES @ .    \ Выводим заголовок и результат
CR
;

\ === AUTHOR & PROJECT ==========================================
CR AUTHOR
CR S" lib_commom.fs" PROJECT
\ ===============================================================

CR
." --- Test: X POLINOME3 ---------------------" CR

GET_START_TIME
." Start time: " START_Time @ .

TEST_POLINOME3
CR

GET_END_TIME  
." End time: " END_Time @ . CR

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
CR

." Time difference в миллисекундах: "
\ DIFF_Time @ >MS . ." ." .
DIFF_Time @ >MS.PRINT 
CR


: TIMES-RUN-POLI ( n -- )
  0 DO
    CR 8 DUP POLI3 CR . ." POLI3 = " RES @ .
  LOOP ;

: TIMES-RUN ( n -- )
  0 DO
    8 DUP DUP 4 * 9 + * 7 + * 5 + DROP \ .
  LOOP ;

CR
." --- Test: X POLI3 -------------------------" CR
GET_START_TIME
." Start time: " START_Time @ . CR

100 TIMES-RUN-POLI
CR

GET_END_TIME  
." End time: " END_Time @ . CR

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
CR

." Time difference в миллисекундах: "
\ DIFF_Time @ >MS . ." ." .
DIFF_Time @ >MS.PRINT 
CR


CR
." --- Test: 8 DUP DUP 4 * 9 + * 7 + * 5 + ---" 
CR
GET_START_TIME
." Start time: " START_Time @ . CR

\ ." Счет без вывода 10000000000 раз -> 13,176456 nsec  на 1 расчет
\ ." Счет без вывода 1000000000 раз -> 13,178973 nsec   на 1 расчет
\ ." Счет без вывода 1000000 раз    -> 14,507 nsec      на 1 расчет
\ ." Счет без вывода 100000 раз     -> 15.58 nsec     на 1 расчет
\ ." Счет без вывода 10000 раз      -> 22.6 nsec      на 1 расчет
\ ." Счет без вывода 1000 раз       -> 48 nsec        на 1 расчет
\ ." Счет без вывода 100 раз        -> 42 nsec        на 1 расчет
\ ." Счет без вывода 10 раз         -> 45 nsec        на 1 расчет
\ ." Счет без вывода 1 раз          -> 51 nsec        на 1 расчет
\ 51 - 13 = 38 nsec накладные расходы на вывод
1000000 TIMES-RUN 

GET_END_TIME  
." End time: " END_Time @ . CR

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
CR

." Time difference в миллисекундах: "
\ DIFF_Time @ >MS . ." ." .
DIFF_Time @ >MS.PRINT 
CR

\ --- Test INIT-COEFS, POLI3_ARRAY ---------------
.S:
INIT-COEFS
.S:
X @ POLI3_ARRAY \ Вычисляем полином 3-й ст. и сохраняем в RES
.S:
." Gorner calc polinome 3 (coef in array  COEFS) ->  RES = " RES @ .
.S:
CR

\ --- Универсальная версия полинома произвольной степени ---
4 DEGREE !          \ Устанавливаем степень полинома = 4   
V1 @ 0 SET-COEF     \ Устанавливаем коэффициенты полинома в 
V2 @ 1 SET-COEF     \ массиве COEFS (всего 5 коэффициентов)
V3 @ 2 SET-COEF     \ 0-й элемент массива - коэффициент при x^0
V4 @ 3 SET-COEF     \ 3-й элемент массива - коэффициент при x^3
V5 @ 4 SET-COEF     \ 4-й элемент массива - коэффициент при x^4
.S:
X @ POLI_UNIVERSALE \ Вычисляем полином 4-й ст. от X и выводим результат
.S:
." Gorner calc polinome 4 (universal) -> " .
.S:
CR

4 DEGREE !          \ Устанавливаем степень полинома = 4   
4 0 SET-COEF        \ Устанавливаем коэффициенты полинома в 
9 1 SET-COEF        \ массиве COEFS (всего 5 коэффициентов)
7 2 SET-COEF        \ 0-й элемент массива - коэффициент при x^0
5 3 SET-COEF        \ 3-й элемент массива - коэффициент при x^3
11 4 SET-COEF       \ 4-й элемент массива - коэффициент при x^4
.S:
X @ POLI_UNIVERSALE \ Вычисляем полином 4-й ст. от X и выводим результат
.S:
." Gorner calc polinome 4 (universal) -> " .
.S:
CR

bye