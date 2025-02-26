: delay-5-sec ( -- ) \ задержка на 5 секунд
    5000 ms \ ждать 5 секунд
;

: print-time ( -- ) \ печать системного времени
    s" date +%T" system \ выполнить команду date +%T
    cr \ перевод строки
;

: main ( -- ) \ основная программа
    CR ." === Демонстрация  паузы ==="
    CR ." === Запуск на  5 секунд ===" CR
    print-time \ печать времени начала
    delay-5-sec \ задержка на 5 секунд
    ." === Завершение 5 секунд ===" CR
    print-time \ печать времени конца
    CR
;

main \ запуск программы

bye