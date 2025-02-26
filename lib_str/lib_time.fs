\ пауза n миллисекунд
: PAUSE ( n -- )  
    ms
;

\ пустой цикл
: EMPTY_LOOP ( -- ) 
    0 DO 
    LOOP
;

\ выводит число без пробелов
: .NO-SPACE ( n -- ) 
    0 <# #S #> type
;

\ микросекунды в миллисекунды и дробную часть
: >MS ( microseconds -- ms fraction ) 
    1000 /MOD    \ разделить на 1000 и получить частное и остаток
;

\ Печать миллисекунд с дробной частью из входных микросекунд
: >MS.PRINT ( microseconds -- )
    1000 /MOD    
    .NO-SPACE ." ." .NO-SPACE
;


\ === Замер времени работы участка программы ================
VARIABLE START_Time
VARIABLE END_Time
VARIABLE DIFF_Time

\ Получить текущее время в микросекундах до вызова участка программы
: GET_START_TIME ( -- )
    utime DROP  
    START_Time !  
;

\ Получить текущее время в микросекундах после вызова участка программы
: GET_END_TIME ( -- )
    utime DROP
    END_Time !
;

\ Вычисляем разницу в микросекундах и записываем в DIFF_Time
: TIME_DIFF ( -- )
    END_Time @ START_Time @ - 
    DIFF_Time ! 
;







