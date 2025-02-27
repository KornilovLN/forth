include lib_design.fs
include lib_common.fs
include lib_time.fs

\ === TEST ==========================================
\ 4 * x^3 + 9 * x^2 + 7 *x + 5 = ?
\ x * ( x * ( x * 4 + 9 ) + 7 ) + 5 = ?
\ x * ( x * ( U1 ) + 7 ) + 5 = ?
\ --- Variables -------------------------------------
VARIABLE V1
VARIABLE V2
VARIABLE V3
VARIABLE V4
VARIABLE X

VARIABLE RES

\ --- Initialization --------------------------------
4 V1 !
9 V2 !
7 V3 !
5 V4 !

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

CR AUTHOR
CR S" lib_commom.fs" PROJECT

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
DIFF_Time @ >MS . ." ." .
CR

CR
." --- Test: X POLI3 -------------------------" CR

GET_START_TIME
." Start time: " START_Time @ . CR

8 DUP POLI3 CR . ." POLI3 = " RES @ . CR
CR

GET_END_TIME  
." End time: " END_Time @ . CR

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
CR

." Time difference в миллисекундах: "
DIFF_Time @ >MS . ." ." .
CR


CR
." --- Test: 8 DUP DUP 4 * 9 + * 7 + * 5 + ---" 
CR

GET_START_TIME
." Start time: " START_Time @ . CR

CR 
8 DUP DUP 4 * 9 + * 7 + * 5 + 

GET_END_TIME  
." End time: " END_Time @ . CR

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
CR

." Time difference в миллисекундах: "
DIFF_Time @ >MS . ." ." .
CR

." Simple res = " . CR



bye