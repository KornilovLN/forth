: COUNT-WORDS ( -- n )
    0              \ Инициализация счётчика
    WORDS          \ Начинаем перебор слов
    BEGIN
        >IN @      \ Получаем текущую позицию во входном потоке
        WORD       \ Читаем следующее слово
        COUNT      \ Получаем длину и адрес строки
        DROP       \ Игнорируем адрес строки
        SWAP 1+    \ Увеличиваем счётчик
        SWAP       \ Возвращаем счётчик на вершину стека
        >IN @ 0=   \ Проверяем, достигнут ли конец списка слов
    UNTIL
    DROP           \ Удаляем ненужное значение
;

COUNT-WORDS .      \ Выводим количество слов

bye