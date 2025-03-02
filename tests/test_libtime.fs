include ../lib_str/lib_time.fs

\ --- Тестирование библиотеки рассчета --------------
\ --- времени работы участка программы --------------
\ --- Подключаем библиотеку lib_time.fs -------------

\ --- Variables -------------------------------------
VARIABLE WAITE1
VARIABLE WAITE2

\ --- Initialization --------------------------------
2000 WAITE1 !
400000000 WAITE2 !

\ --- Test ------------------------------------------
." ================ Starting test 1 =================" cr
." WAITE1 = " WAITE1 @ . cr

\ Процедура получает текущее время в var START_Time
GET_START_TIME
." Start time: " START_Time @ . cr

\ Имитация работы на этом участке
WAITE1 @ PAUSE

\ Процедура получает текущее время в var END_Time
GET_END_TIME  
." End time: " END_Time @ . cr

." Time difference в микросекундах: "
TIME_DIFF        \ Вычисляет разницу в микросекундах
DIFF_Time @ .    \ Выводит разницу микросекундах
cr

." Time difference в миллисекундах: "
DIFF_Time @ >MS.PRINT  cr   \ выведет 1000.121

cr

." ================ Starting test 2 =================" cr
." WAITE2 = " WAITE2 @ . cr

GET_START_TIME
." Start time: " START_Time @ . cr

WAITE2 @ EMPTY_LOOP

GET_END_TIME  
." End time: " END_Time @ . cr

." Time difference в микросекундах: "
TIME_DIFF       
DIFF_Time @ .   
cr

." Time difference в миллисекундах: "
DIFF_Time @ >MS . ." ." .    \ выведет 1000 .121
cr
\ --- End Test ------------------------------------------

cr
." ================ END all tests ===================" cr

bye