include ../lib_str/lib_common.fs
include ../lib_str/lib_design.fs
include ../lib_str/lib_gorner.fs
include ../lib_str/lib_time.fs

\ === TEST POLI_UNIVERSALE ==========================
\ 4 * x^4 + 9 * x^3 + 7 * x^2 + 5 * x + 11 = ?
\ 4*4096 + 9*512 + 7*64 + 5*8 + 11 = 
\ 16384 + 4608 + 448 + 40 + 11 = 21491

\ --- Variables -------------------------------------
VARIABLE X
VARIABLE RES
VARIABLE COUNTER

\ --- Coeff for polinime ----------------------------
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

1000000 COUNTER !

\ --- Схема Горнера для коэффициентов из массива ----
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


: TIMES-RUN-POLI_UNIVERSALE ( n -- )
  0 DO
    X @ POLI_UNIVERSALE DROP \ Вычисляем без вывода результата
  LOOP ;


\ --- Test TIMES-RUN-POLI_UNIVERSALE ---------------
CR
." --- Test: X POLI_UNIVERSALE ---------------" CR
GET_START_TIME  
COUNTER @ TIMES-RUN-POLI_UNIVERSALE 
GET_END_TIME

." Cicles COUNTER = " COUNTER @ . CR

." Time difference в микросекундах: "
TIME_DIFF  DIFF_Time @ .  CR

." Time difference в миллисекундах: "
DIFF_Time @ >MS.PRINT  CR
\ -------------------------------------------------



bye

token_tree